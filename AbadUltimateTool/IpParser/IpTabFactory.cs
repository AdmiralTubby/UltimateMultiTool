// File: IpScan/IpTabFactory.cs

using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AbadUltimateTool.IpParser;

internal static class IpTabFactory
{
    // ---------- All-IPs overview -----------------------------------------------
    public static TabPage BuildAllIpsPage(IEnumerable<IpUsage> items)
    {
        return BuildPage("All IPs", null, items.Select(i => new
        {
            i.Ip,
            i.Type,
            i.Name,
            File = i.FilePath
        }));
    }

    // ---------- Ranges root tab -------------------------------------------------
    public static TabPage BuildRangePage(IEnumerable<IpUsage> items)
    {
        var nets = items
            .Select(u => IpRange.Network24(u.Ip))
            .Distinct()
            .OrderBy(s => s)
            .Select(n => new { Network = n });

        return BuildPage("Ranges", null, nets);
    }

    // ---------- Per-file tab ----------------------------------------------------
    public static TabPage BuildFilePage(string path, IEnumerable<IpUsage> items)
    {
        var data = items.Select(i => new
        {
            i.Ip,
            i.Type,
            i.Name
        });
        return BuildPage(System.IO.Path.GetFileName(path), path, data);
    }

    // ---------- Common helper ---------------------------------------------------
    private static TabPage BuildPage(string title, string tooltip, IEnumerable<object> data)
    {
        var page = new TabPage(title);
        if (!string.IsNullOrEmpty(tooltip))
            page.ToolTipText = tooltip;

        if (tooltip != null)
        {
            var lbl = new Label
            {
                Text = tooltip,
                Dock = DockStyle.Top,
                Height = 22,
                AutoEllipsis = true,
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.Gainsboro,
                Padding = new Padding(4, 0, 4, 0)
            };
            page.Controls.Add(lbl);
        }

        var grid = new DataGridView
        {
            Dock = DockStyle.Fill,
            ReadOnly = true,
            RowHeadersVisible = false,
            AutoGenerateColumns = true,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
            BackgroundColor = Color.FromArgb(40, 40, 40)
        };

        grid.DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
        grid.DefaultCellStyle.ForeColor = Color.White;
        grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(60, 60, 60);
        grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

        grid.DataSource = data.ToList();
        page.Controls.Add(grid);
        return page;
    }
}
