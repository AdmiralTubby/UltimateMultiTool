using System;
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
        rtbLeft.VScroll += SyncScroll;
        rtbRight.VScroll += SyncScroll;

        Text = $"Diff – {Path.GetFileName(leftPath)}";
        KeyPreview = true;          // form gets PgUp/PgDn

        Shown += async (_, __) =>
        {
            UseWaitCursor = true;

            // heavy work off the UI thread
            var data = await Task.Run(() =>
            {
                var left = TryLoad(leftPath);   // (lines , msg)
                var right = TryLoad(rightPath);  // (lines , msg)

                return (leftLines: left.lines,
                        rightLines: right.lines,
                        leftMsg: left.msg,
                        rightMsg: right.msg);
            });

            // back on UI thread
            if (data.leftMsg != null) rtbLeft.Text = data.leftMsg;
            if (data.rightMsg != null) rtbRight.Text = data.rightMsg;

            if (data.leftLines != null && data.rightLines != null)
                ShowDiff(data.leftLines, data.rightLines);

            UseWaitCursor = false;
        };
    }

    // ──────────────── file loading helper ───────────────────────────────
    private (string[] lines, string msg) TryLoad(string path)
    {
        var fi = new FileInfo(path);
        if (!fi.Exists) return (null, "<file missing>");
        if (fi.Length > HardLimitBytes) return (null, "<file too large>");

        byte[] bytes = File.ReadAllBytes(path);
        if (bytes.Any(b => b == 0)) return (null, "<binary file – diff not shown>");

        return (File.ReadAllLines(path), null);
    }

    // ──────────────── diff & rendering ──────────────────────────────────
    private void ShowDiff(string[] left, string[] right)
    {
        int max = Math.Max(left.Length, right.Length);

        // 1) collect changed line numbers
        var diffLines = new System.Collections.Generic.List<int>(1024);
        for (int i = 0; i < max; i++)
        {
            string l = i < left.Length ? left[i] : string.Empty;
            string r = i < right.Length ? right[i] : string.Empty;
            if (!string.Equals(l, r, StringComparison.Ordinal)) diffLines.Add(i);
        }

        if (diffLines.Count == 0)
        {
            rtbLeft.Text = string.Join(Environment.NewLine, left);
            rtbRight.Text = string.Join(Environment.NewLine, right);
            return;
        }

        var diffSet = diffLines.ToHashSet();
        rtbLeft.SuspendLayout();
        rtbRight.SuspendLayout();

        int lastWritten = -1;

        foreach (int idx in diffLines)
        {
            int start = Math.Max(idx - Context, lastWritten + 1);
            int end = Math.Min(idx + Context, max - 1);

            if (start > lastWritten + 1)
            {
                AppendGap(rtbLeft);
                AppendGap(rtbRight);
            }

            for (int i = start; i <= end; i++)
            {
                string l = i < left.Length ? left[i] : string.Empty;
                string r = i < right.Length ? right[i] : string.Empty;
                bool highlight = diffSet.Contains(i);

                AppendLine(rtbLeft, l, highlight);
                AppendLine(rtbRight, r, highlight);
            }

            lastWritten = end;
        }

        rtbLeft.ResumeLayout();
        rtbRight.ResumeLayout();

        // for PgUp / PgDn navigation
        _diffPositions = diffLines.Distinct().OrderBy(i => i).ToArray();
        _currentDiffIx = 0;
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

    // ──────────────── PgUp / PgDn navigation ────────────────────────────
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

    // ──────────────── synced scrolling (WM_VSCROLL) ─────────────────────
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

            var dst = src == rtbLeft ? rtbRight : (src == rtbRight ? rtbLeft : null);
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

        // index (0-based) of the topmost visible text line in the source box
        int firstVisible = SendMessage(src.Handle, EM_GETFIRSTVISIBLELINE, 0, 0);

        // index of top line currently visible in the other box
        int dstVisible = SendMessage(dst.Handle, EM_GETFIRSTVISIBLELINE, 0, 0);
        int delta = firstVisible - dstVisible;
        if (delta == 0) return;                     // already aligned

        // scroll destination box by the same amount
        SendMessage(dst.Handle, EM_LINESCROLL, 0, delta);
    }


    private void rtbRight_TextChanged(object sender, EventArgs e)
    {

    }
}
