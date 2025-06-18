using AbadUltimateTool.FileDiff;
using AbadUltimateTool.IpParser;
using AbadUltimateTool.Palette;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace AbadUltimateTool;


public partial class UltimateMultiTool : Form
{
    // 25 ticks * 50 ms will be around 1000 ms
    private readonly Timer _fadeTimer = new Timer { Interval = 25 };
    private const double FadeStep = 0.025;   // 0 to 1 in 40 steps
    private DirectoryIndex _srcIndex;   // snapshot of the reference folder
    private string _lastTargetDir; // remembers last compared folder

    public UltimateMultiTool()
    {
        InitializeComponent();

        _fadeTimer.Tick += FadeInTick;

        Shown += (s, e) =>
        {

            _fadeTimer.Start();

        };

        gridDiff.DataBindingComplete += (_, __) =>
        {
            gridDiff.AllowUserToResizeColumns = true;
            gridDiff.AllowUserToResizeRows = true;

            //  let the grid measure itself once
            gridDiff.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);

            // freeze current widths so manual dragging stays put
            foreach (DataGridViewColumn c in gridDiff.Columns)
                c.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

            // convert the long-text columns into flexible "Fill" columns
            void makeFill(string name, int weight)
            {
                var col = gridDiff.Columns[name];
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                col.FillWeight = weight;          // relative share of leftover width
            }

            makeFill("File", 20);   // 30 %
            makeFill("SourcePath", 40);   // 35 %
            makeFill("TargetPath", 40);   // 35 %

            // narrow columns keep the widths that AutoResizeColumns found

        };


    }

    private void FadeInTick(object sender, EventArgs e)
    {
        if (Opacity < 1.0)
        {
            this.Opacity = Math.Min(this.Opacity + FadeStep, 1.0);
        }
        else
        {
            _fadeTimer.Stop();               // finished fading
        }
    }

    private void tabPage1_Click(object sender, EventArgs e)
    {

    }

    // ── 4.1  “Load Palettes…” button ──
    // Form1.cs  –  same file, just wrap the await block

    private async void cmdLoadPalettes_Click(object sender, EventArgs e)
    {
        using (var dlg = new FolderBrowserDialog())
        {
            dlg.Description = "Select the root folder that contains Palette*.ini files.";
            if (dlg.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            try
            {
                // show the wait cursor while the files are being parsed so the user can see that the program is doing something
                // I'm too lazy to make a progress bar, soz
                Application.UseWaitCursor = true;
                // background thread: crawl + parse
                var palettes = await Task.Run(() =>
                    PaletteParser.FindPaletteFiles(dlg.SelectedPath)
                                 .Select(p => (Path: p, Entries: PaletteParser.Parse(p)))
                                 .Where(p => p.Entries.Count > 0)
                                 .ToList());

                if (palettes.Count == 0) // if no palettes have been found, tell the user that they are an idiot                
                {
                    MessageBox.Show(this,
                        "No Palette*.ini files were found under the folder you selected.\n\n" +
                        "Restricted system folders were skipped automatically.",
                        "Nothing to display",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                // populate the UI (UI thread)
                tabPalettes.SuspendLayout();
                tabPalettes.TabPages.Clear();

                foreach (var p in palettes)
                {
                    tabPalettes.TabPages.Add(
                        PaletteTabFactory.Create(p.Path, p.Entries));
                }

                tabPalettes.ResumeLayout();
            }
            catch (Exception ex)
            {
                // Anything we did NOT explicitly filter above lands here.
                MessageBox.Show(this,
                    "The scan was aborted because an unexpected error occurred:\n\n"
                    + ex.Message,
                    "Palette scan failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            finally
            {
                // restore the normal cursor 
                Application.UseWaitCursor = false;
            }
        }
    }

    private async void cmdLoadIps_Click(object sender, EventArgs e)
    {
        using (var dlg = new FolderBrowserDialog())
        {
            dlg.Description = "Select root folder to scan for IP addresses.";
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            Application.UseWaitCursor = true;
            try
            {
                // everything that needs dlg.SelectedPath stays in-scope here
                var list = await Task.Run(() =>
                    IpParser.IpParser.ScanFolder(dlg.SelectedPath).ToList());

                tabIps.SuspendLayout();
                tabIps.TabPages.Clear();

                tabIps.TabPages.Add(IpTabFactory.BuildAllIpsPage(list));
                foreach (var grp in list.GroupBy(i => i.FilePath))
                    tabIps.TabPages.Add(IpTabFactory.BuildFilePage(grp.Key, grp));
                //tabIps.TabPages.Insert(0, IpTabFactory.BuildRangePage(list));
                // Build Ranges tab and make it the first page
                var rangesPage = IpTabFactory.BuildRangePage(list);
                tabIps.TabPages.Insert(0, rangesPage);

                // hook double-click on the grid inside that page
                var rangesGrid = rangesPage.Controls.OfType<DataGridView>().First();
                rangesGrid.CellDoubleClick += RangeGrid_CellDoubleClick;
                tabIps.ResumeLayout();
                _lastIpScan = list;                     // cache for export
            }
            finally
            {
                Application.UseWaitCursor = false;
            }
        }
    }

    private List<IpUsage> _lastIpScan;   // field in Form1

    private void cmdExportIps_Click(object sender, EventArgs e)
    {
        if (_lastIpScan == null || _lastIpScan.Count == 0)
        {
            MessageBox.Show(this, "No IP data to export.", "Export",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        string csvPath;                                 // declare outside

        using (var sfd = new SaveFileDialog())
        {
            sfd.Filter = "CSV file|*.csv";
            sfd.FileName = "IpInventory.csv";
            sfd.OverwritePrompt = true;

            if (sfd.ShowDialog(this) != DialogResult.OK) return;
            csvPath = sfd.FileName;                      // remember it
        }   // sfd disposed here

        try
        {
            CsvExporter.WriteAll(csvPath, _lastIpScan);  // use the stored path
            MessageBox.Show(this, "Export complete.", "Export",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Export failed",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    private List<string> _lastUnused;    // new – unused IPs of the last range tab
    private void cmdExportRange_Click(object sender, EventArgs e)
    {
        if (_lastIpScan == null || _lastIpScan.Count == 0)
        {
            MessageBox.Show(this, "Run a scan first.", "Export",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        // collect distinct /24 networks
        var nets = _lastIpScan.Select(u => IpRange.Network24(u.Ip))
                              .Distinct()
                              .OrderBy(s => s)
                              .ToList();

        using (var dlg = new RangeSelectForm(nets))
        {
            if (dlg.ShowDialog(this) != DialogResult.OK || dlg.Selected == null)
                return;

            string net24 = dlg.Selected;
            var unusedList = IpRange.Unused(net24, _lastIpScan.Select(u => u.Ip));

            using (var folderDlg = new FolderBrowserDialog())
            {
                folderDlg.Description = "Choose folder to save CSV";
                if (folderDlg.ShowDialog(this) != DialogResult.OK) return;

                CsvExporter.WriteUnusedPrompt(folderDlg.SelectedPath,
                                              net24,
                                              unusedList);

                MessageBox.Show(this, "Export complete.", "Export",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }

    private void RangeGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;                       // header row

        var grid = (DataGridView)sender;
        string net24 = grid.Rows[e.RowIndex].Cells[0].Value.ToString(); // "192.168.32"

        var unused = IpRange.Unused(net24, _lastIpScan.Select(u => u.Ip));
        _lastUnused = unused;                             // cache for Export Range…

        // build new tab only once
        if (tabIps.TabPages.Cast<TabPage>().Any(p => p.Text == "Range " + net24))
        {
            tabIps.SelectedTab =
                tabIps.TabPages.Cast<TabPage>().First(p => p.Text == "Range " + net24);
            return;
        }

        var page = new TabPage("Range " + net24);
        var g = new DataGridView
        {
            Dock = DockStyle.Fill,
            ReadOnly = true,
            RowHeadersVisible = false,
            AutoGenerateColumns = true,
            DataSource = unused.Select(ip => new { Ip = ip }).ToList()
        };
        page.Controls.Add(g);
        tabIps.TabPages.Add(page);
        tabIps.SelectedTab = page;
    }

    private sealed class RangeSelectForm : Form
    {
        public string Selected { get; private set; }

        public RangeSelectForm(IEnumerable<string> nets)
        {
            Text = "Select IP Range";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = MinimizeBox = false;
            ClientSize = new System.Drawing.Size(180, 220);

            var list = new ListBox
            {
                Dock = DockStyle.Fill,
                DataSource = nets.ToList()
            };
            Controls.Add(list);

            var ok = new Button
            {
                Text = "OK",
                Dock = DockStyle.Bottom,
                Height = 28
            };
            ok.Click += (s, e) =>
            {
                Selected = list.SelectedItem as string;
                DialogResult = DialogResult.OK;
            };
            Controls.Add(ok);
        }
    }

    private void tabPage3_Click(object sender, EventArgs e)
    {

    }

    private async void cmdIndexSrc_Click(object sender, EventArgs e)
    {
        using var dlg = new FolderBrowserDialog { Description = "Select source folder to index" };
        if (dlg.ShowDialog(this) != DialogResult.OK) return;

        Application.UseWaitCursor = true;
        try
        {
            _srcIndex = await Task.Run(() => DirectoryIndex.Build(dlg.SelectedPath));
            _lastTargetDir = null;
            gridDiff.DataSource = null;
            MessageBox.Show(this, "Source indexed.", "File Diff", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }
        finally { Application.UseWaitCursor = false; }
    }


    private async void cmdCompare_Click(object sender, EventArgs e)
    {
        if (_srcIndex == null)
        {
            MessageBox.Show(this, "Index a source folder first.", "File Diff",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        using var dlg = new FolderBrowserDialog { Description = "Select target folder to compare" };
        if (dlg.ShowDialog(this) != DialogResult.OK) return;

        await RunComparisonAsync(dlg.SelectedPath);
    }


    //  Refresh = rebuild source-index  +  rerun comparison for last target
    private async void cmdRefreshDiff_Click(object sender, EventArgs e)
    {
        if (_srcIndex == null || _lastTargetDir == null)
        {
            MessageBox.Show(this,
                "No previous comparison to refresh.",
                "File Diff",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return;
        }

        Application.UseWaitCursor = true;
        try
        {
            // re-index the source folder
            _srcIndex = DirectoryIndex.Build(_srcIndex.Root);

            // rerun the comparison 
            await RunComparisonAsync(_lastTargetDir);
        }
        finally
        {
            Application.UseWaitCursor = false;
        }
    }


    private async Task RunComparisonAsync(string targetDir)
    {
        Application.UseWaitCursor = true;
        try
        {
            // build an index for the target folder on a background thread
            var tgt = await Task.Run(() => DirectoryIndex.Build(targetDir));

            // helper converts a status word into a sort priority (0..4)
            static int Rank(string status) => status switch
            {
                "Missing in source" => 0,
                "Missing in target" => 1,
                "Newer" => 2,
                "Older" => 3,
                _ => 4        // "Same" or anything else
            };

            var rows = new List<DiffRow>();

            // union of file names from both sides
            var all = new HashSet<string>(_srcIndex.FileNames,
                                          StringComparer.OrdinalIgnoreCase);
            all.UnionWith(tgt.FileNames);

            foreach (var name in all)
            {
                _srcIndex.TryGet(name, out var srcUtc);
                tgt.TryGet(name, out var tgtUtc);

                // decide what to call the row
                string status;
                if (srcUtc == default) status = "Missing in source";
                else if (tgtUtc == default) status = "Missing in target";
                else if (srcUtc > tgtUtc) status = "Newer";
                else if (srcUtc < tgtUtc) status = "Older";
                else status = "Same";

                rows.Add(new DiffRow
                {
                    File = name,
                    Status = status,
                    SourceMTime = srcUtc == default ? (DateTime?)null : srcUtc.ToLocalTime(),
                    TargetMTime = tgtUtc == default ? (DateTime?)null : tgtUtc.ToLocalTime(),
                    SourcePath = _srcIndex.GetFullPath(name),
                    TargetPath = tgt.GetFullPath(name)
                });
            }

            // first order by status rank, then alphabetically by file name
            gridDiff.DataSource = rows
                .OrderBy(r => Rank(r.Status))
                .ThenBy(r => r.File)
                .ToList();

            _lastTargetDir = targetDir;   // remember for “Refresh”
        }
        finally
        {
            Application.UseWaitCursor = false;
        }
    }


    private void gridDiff_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        // row < 0  -> header row   col < 0  -> header column
        if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

        var diff = (DiffRow)gridDiff.Rows[e.RowIndex].DataBoundItem;
        string clickedCol = gridDiff.Columns[e.ColumnIndex].Name;

        // open explorer only if the click was in one of the path columns
        string path = clickedCol switch
        {
            "SourcePath" => diff.SourcePath,
            "TargetPath" => diff.TargetPath,
            _ => null                        // any other column are ignored
        };

        if (path == null || !File.Exists(path)) return;

        System.Diagnostics.Process.Start("explorer.exe", $"/select,\"{path}\"");
    }

    //  copy every selected row where the source is newer (older/Missing target)
    //  copy every selected row.  ffor files missing in target we ask where to save them 

    private async void cmdSyncSelected_Click(object sender, EventArgs e)
    {
        // rows explicitly highlighted by the user
        var rows = gridDiff.SelectedRows
                           .Cast<DataGridViewRow>()
                           .Select(r => r.DataBoundItem as DiffRow)
                           .Where(r => r?.SourcePath != null)
                           .ToList();

        if (rows.Count == 0)
        {
            MessageBox.Show(this, "Select one or more files first.",
                            "File Diff", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        if (MessageBox.Show(this,
                $"Copy {rows.Count} file(s) from source to target?",
                "Confirm copy",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            return;

        int copied = 0;
        bool abort = false;

        Application.UseWaitCursor = true;
        try
        {
            await Task.Run(() =>
            {
                foreach (var r in rows)
                {
                    string dest = r.TargetPath;

                    if (!File.Exists(dest))
                    {
                        // ask the user what to do for this missing target
                        var decision = (DialogResult)Invoke(new Func<DialogResult>(() =>
                            MessageBox.Show(this,
                                $"Target file for “{r.File}” is missing.\n" +
                                "Yes    – choose location and copy this file\n" +
                                "No     – skip this file\n" +
                                "Cancel - abort the whole operation",
                                "Missing target",
                                MessageBoxButtons.YesNoCancel,
                                MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button2)));

                        if (decision == DialogResult.No)   // skip this file
                            continue;

                        if (decision == DialogResult.Cancel)   // abort everything
                        {
                            abort = true;
                            break;
                        }

                        // decision = Yes, then let user pick destination
                        dest = (string)Invoke(new Func<string>(() =>
                        {
                            using var sfd = new SaveFileDialog
                            {
                                Title = $"Select destination for “{r.File}”",
                                FileName = r.File,
                                Filter = "All files|*.*"
                            };
                            return sfd.ShowDialog(this) == DialogResult.OK ? sfd.FileName : null;
                        }));

                        if (dest == null)         // user cancelled here, then skip file
                            continue;
                    }

                    Directory.CreateDirectory(Path.GetDirectoryName(dest));
                    File.Copy(r.SourcePath, dest, overwrite: true);
                    copied++;
                }
            });

            // ----- final summary ---------------------------------------------------
            if (abort)
                MessageBox.Show(this, "Copy cancelled.", "File Diff",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (copied == 0)
                MessageBox.Show(this, "No file was copied.", "File Diff",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show(this, "Copy completed.", "File Diff",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (!abort && _lastTargetDir != null)
                await RunComparisonAsync(_lastTargetDir);   // refresh grid
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, ex.Message, "Copy failed. You must have done something really bad",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            Application.UseWaitCursor = false;
        }
    }

    private void cmdNeedHelp_Click(object sender, EventArgs e)
    {
        MessageBox.Show(this,
                                $"skriv noget her som forklarer\n" +
                                "hvordan det lige er, at man skal\n" +
                                "bruge fanen, I guess.\n" +
                                "",
                                "You needed help",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information,
                                MessageBoxDefaultButton.Button2);
    }

    private void cmdViewDiff_Click(object sender, EventArgs e)
    {
        const long SoftLimit = 2 * 1024 * 1024;           // 2 MB

        var rows = gridDiff.SelectedRows
                           .Cast<DataGridViewRow>()
                           .Select(r => r.DataBoundItem as DiffRow)
                           .Where(r => r != null
                                       && File.Exists(r.SourcePath)
                                       && File.Exists(r.TargetPath))
                           .ToList();

        if (rows.Count == 0)
        {
            MessageBox.Show(this,
                "Select at least one row that has both Source and Target files.",
                "View differences",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return;
        }

        foreach (var row in rows)
        {
            long bigger = Math.Max(new FileInfo(row.SourcePath).Length,
                                   new FileInfo(row.TargetPath).Length);

            if (bigger > SoftLimit)
            {
                double mb = bigger / (1024.0 * 1024.0);

                var ans = MessageBox.Show(this,
                    $"The file pair \"{row.File}\" totals {mb:F1} MB.\n\n" +
                    "Diffing large files may feel slow.\n\nContinue?",
                    "Large file warning",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);

                if (ans == DialogResult.No)
                    continue;      // skip this pair, proceed with next

                if (ans == DialogResult.Cancel)
                    break;         // abort entire loop
            }

            new DiffViewer(row.SourcePath, row.TargetPath).Show(this); // modeless
        }
    }

    // -------------------------------------------------------------
    //  Opens the exclusion-list editor. If the user pressed Save
    //  in that dialog, the singleton ExcludeFolderStore has already
    //  been updated and persisted → re-run the comparison so the
    //  grid reflects the new skip rules.
    // -------------------------------------------------------------
    private void cmdEditExcl_Click(object sender, EventArgs e)
    {
        using var dlg = new ExcludeEditor();
        dlg.ShowDialog(this);          // modal – user adds/removes, maybe saves

        // refresh only if we already have a comparison on screen
        if (_srcIndex != null && _lastTargetDir != null)
            _ = RunComparisonAsync(_lastTargetDir);   // async, fire-and-forget
    }


}
