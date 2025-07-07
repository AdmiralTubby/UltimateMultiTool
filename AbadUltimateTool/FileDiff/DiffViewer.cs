using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AbadUltimateTool;

public partial class DiffViewer : Form
{
    // ──────────────── constants ──────────────────────────────────────────
    private const int HardLimitBytes = 50 * 1024 * 1024;   // 50 MB hard stop
    private const int Context = 25;                 // number of lines to keep
    private const string GapMarker = "------------------------------------------------------------";       // dash gap
    // Win32 messages used for line-synchronisation
    private const int EM_GETFIRSTVISIBLELINE = 0x00CE;
    private const int EM_LINESCROLL = 0x00B6;

    [DllImport("user32.dll")]
    private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

    // ──────────────── navigation state (PgUp / PgDn) ─────────────────────
    private int[] _diffPositions = Array.Empty<int>();
    private int _currentDiffIx = 0;

    // ──────────────── ctor ───────────────────────────────────────────────
    public DiffViewer(string leftPath, string rightPath)
    {
        InitializeComponent();

        // keep the two boxes in sync while scrolling
        rtbLeft.VScroll += SyncScroll;
        rtbRight.VScroll += SyncScroll;

        Text = $"Diff – {Path.GetFileName(leftPath)}";
        KeyPreview = true;                            // form gets PgUp / PgDn

        Shown += async (_, __) =>
        {
            UseWaitCursor = true;                     // busy cursor while we work

            // ---- STEP 0  load files on a pool thread --------------------
            var (leftLines, rightLines, leftMsg, rightMsg) = await Task.Run(() =>
            {
                var l = TryLoad(leftPath);            // (lines , msg)
                var r = TryLoad(rightPath);
                return (l.lines, r.lines, l.msg, r.msg);
            });

            // show any “<file missing> …” placeholder text
            if (leftMsg != null) rtbLeft.Text = leftMsg;
            if (rightMsg != null) rtbRight.Text = rightMsg;

            // ---- STEP 1  large-diff guard --------------------------------
            const long SoftLimit = 1 * 1024 * 1024;  // 1 MB
            bool bigFile = new FileInfo(leftPath).Length > SoftLimit ||
                               new FileInfo(rightPath).Length > SoftLimit;
            bool manyChanges = bigFile &&
                               RoughChangeRatio(leftLines, rightLines) > 0.30;

            if (manyChanges)
            {
                var ans = MessageBox.Show(this,
                    "More than 30 % of the lines differ in a file over 1 MB.\n" +
                    "Diffing may feel slow.  Continue?",
                    "Large diff warning",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);

                if (ans == DialogResult.No) { Close(); return; }
            }

            // build diff on a pool thread
            var cooked = await Task.Run(() => BuildDiffLines(leftLines, rightLines));

            // single UI pass to paint it
            RenderDiffLines(cooked);
            UseWaitCursor = false;
        };
    }

    // ──────────────── diff & rendering ──────────────────────────────────
    private List<string> BuildDiffLines(string[] left, string[] right)
    {
        var result = new List<string>(2048);
        int maxLines = Math.Max(left.Length, right.Length);

        // quick collect changed line numbers
        var diffLines = new List<int>(1024);
        for (int i = 0; i < maxLines; i++)
        {
            string l = i < left.Length ? left[i] : string.Empty;
            string r = i < right.Length ? right[i] : string.Empty;
            if (!l.Equals(r, StringComparison.Ordinal)) diffLines.Add(i);
        }

        // nothing differs – show whole file, skip all fancy stuff
        if (diffLines.Count == 0)
        {
            result.AddRange(left);
            _diffPositions = Array.Empty<int>();     // navigation disabled
            return result;
        }

        var diffSet = diffLines.ToHashSet();
        int lastWritten = -1;

        foreach (int idx in diffLines)
        {
            int start = Math.Max(idx - Context, lastWritten + 1);
            int end = Math.Min(idx + Context, maxLines - 1);

            if (start > lastWritten + 1) result.Add(GapMarker);   // visual break

            for (int i = start; i <= end; i++)
            {
                string l = i < left.Length ? left[i] : string.Empty;
                string r = i < right.Length ? right[i] : string.Empty;
                bool changed = diffSet.Contains(i);

                // prefix 0x00 / 0x01 so RenderDiffLines knows what to highlight
                result.Add((changed ? "\u0001" : "\u0000") + l);   // left
                result.Add((changed ? "\u0001" : "\u0000") + r);   // right
            }
            lastWritten = end;
        }

        // ☞ store positions for PgUp / PgDn
        _diffPositions = diffLines.Distinct().OrderBy(i => i).ToArray();
        _currentDiffIx = 0;
        return result;
    }

    private void RenderDiffLines(List<string> cooked)
    {
        rtbLeft.SuspendLayout();
        rtbRight.SuspendLayout();

        bool writeLeftNext = true;                    // interleaved sequence

        foreach (string s in cooked)
        {
            if (s == GapMarker)
            {
                AppendGap(rtbLeft);
                AppendGap(rtbRight);
                continue;
            }

            bool highlight = s[0] == '\u0001';
            string text = s.Substring(1);

            if (writeLeftNext)
                AppendLine(rtbLeft, text, highlight);
            else
                AppendLine(rtbRight, text, highlight);

            writeLeftNext = !writeLeftNext;           // L → R → L …
        }

        rtbLeft.ResumeLayout();
        rtbRight.ResumeLayout();
    }

    private static void AppendGap(RichTextBox rtb)
    {
        int start = rtb.TextLength;
        rtb.AppendText(GapMarker + Environment.NewLine);
        rtb.Select(start, GapMarker.Length);
        rtb.SelectionFont = new Font(rtb.Font, FontStyle.Italic);
        rtb.SelectionColor = Color.DarkGray;
        rtb.Select(rtb.TextLength, 0);
    }

    private static void AppendLine(RichTextBox rtb, string text, bool highlight)
    {
        int start = rtb.TextLength;
        rtb.AppendText(text + Environment.NewLine);
        if (highlight)
        {
            rtb.Select(start, text.Length);
            rtb.SelectionBackColor = Color.Khaki;
            rtb.Select(rtb.TextLength, 0);
        }
    }

    private (string[] lines, string msg) TryLoad(string path)
    {
        var fi = new FileInfo(path);
        if (!fi.Exists) return (null, "<file missing>");
        if (fi.Length > HardLimitBytes)
            return (null, "<file too large>");

        byte[] bytes = File.ReadAllBytes(path);
        if (bytes.Any(b => b == 0))
            return (null, "<binary file – diff not shown>");

        return (File.ReadAllLines(path), null);
    }

    //──────────────────────────────────────────────────────────────────────
    //  PgUp / PgDn navigation
    //──────────────────────────────────────────────────────────────────────
    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);
        if (_diffPositions.Length == 0) return;

        if (e.KeyCode == Keys.PageDown)
        {
            _currentDiffIx = Math.Min(_currentDiffIx + 1, _diffPositions.Length - 1);
            ScrollBothTo(_diffPositions[_currentDiffIx]);
            e.Handled = true;
        }
        else if (e.KeyCode == Keys.PageUp)
        {
            _currentDiffIx = Math.Max(_currentDiffIx - 1, 0);
            ScrollBothTo(_diffPositions[_currentDiffIx]);
            e.Handled = true;
        }
    }

    private void ScrollBothTo(int line)
    {
        ScrollTo(rtbLeft, line);
        ScrollTo(rtbRight, line);
    }

    private static void ScrollTo(RichTextBox rtb, int line)
    {
        int index = rtb.GetFirstCharIndexFromLine(line);
        if (index >= 0)
        {
            rtb.SelectionStart = index;
            rtb.ScrollToCaret();
        }
    }

    //──────────────────────────────────────────────────────────────────────
    //  Synced scrolling
    //──────────────────────────────────────────────────────────────────────
    private const int WM_VSCROLL = 0x0115;
    private const int SB_VERT = 1;
    private const int SB_THUMBPOSITION = 4;

    [DllImport("user32.dll")]
    private static extern int GetScrollPos(IntPtr hWnd, int nBar);

    [DllImport("user32.dll")]
    private static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool redraw);

    protected override void WndProc(ref Message m)
    {
        base.WndProc(ref m);

        if (m.Msg == WM_VSCROLL && (m.WParam.ToInt32() & 0xFFFF) == SB_THUMBPOSITION)
        {
            var src = Control.FromHandle(m.HWnd) as RichTextBox;
            if (src == null) return;

            var dst = src == rtbLeft ? rtbRight
                     : src == rtbRight ? rtbLeft
                     : null;
            if (dst == null) return;

            int pos = GetScrollPos(src.Handle, SB_VERT);
            SetScrollPos(dst.Handle, SB_VERT, pos, true);
            dst.Invalidate();
        }
    }

    private void SyncScroll(object? sender, EventArgs e)
    {
        var src = sender as RichTextBox;
        if (src == null) return;

        var dst = src == rtbLeft ? rtbRight : rtbLeft;

        int firstVisible = SendMessage(src.Handle, EM_GETFIRSTVISIBLELINE, 0, 0);
        int dstVisible = SendMessage(dst.Handle, EM_GETFIRSTVISIBLELINE, 0, 0);

        int delta = firstVisible - dstVisible;
        if (delta == 0) return;                          // already aligned
        SendMessage(dst.Handle, EM_LINESCROLL, 0, delta);
    }

    //──────────────────────────────────────────────────────────────────────
    //  Helper: “how many lines differ?”
    //──────────────────────────────────────────────────────────────────────
    private static double RoughChangeRatio(string[] a, string[] b)
    {
        int max = Math.Max(a.Length, b.Length);
        int diff = 0;
        for (int i = 0; i < max; i++)
        {
            if (!(i < a.Length && i < b.Length &&
                  a[i].Equals(b[i], StringComparison.Ordinal)))
                diff++;
        }
        return (double)diff / max;
    }

    private void rtbRight_TextChanged(object? sender, EventArgs e)
    {
        // does nothing – only here so the Designer compiles
    }
}
