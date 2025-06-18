// File: Palette/PaletteTabFactory.cs
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AbadUltimateTool.Palette;

internal static class PaletteTabFactory
{



    public static TabPage Create(string filePath, List<PaletteEntry> entries)
    {
        // tab caption
        var page = new TabPage(System.IO.Path.GetFileNameWithoutExtension(filePath))
        {
            ToolTipText = filePath
        };

        // path label
        var lblPath = new Label
        {
            Text = filePath,
            Dock = DockStyle.Top,
            AutoSize = false,
            Height = 22,                      // single line
            TextAlign = ContentAlignment.MiddleLeft,
            AutoEllipsis = true,                    // "C:\TopSecretFolder…\file.ini"
            Padding = new Padding(4, 0, 4, 0),
            BackColor = Color.FromArgb(30, 30, 30),
            ForeColor = Color.Gainsboro
        };

        // palette grid 
        var grid = BuildGrid(entries);
        grid.Dock = DockStyle.Fill;                    // already set but explicit

        // Add controls in correct z-order 
        page.Controls.Add(grid);   // add fill first so it ends up *below* the label
        page.Controls.Add(lblPath);

        return page;
    }

    // configure the data into a grid to display the values found in the pallette file
    private static DataGridView BuildGrid(IReadOnlyList<PaletteEntry> entries)
    {

        var grid = new DataGridView
        {
            Dock = DockStyle.Fill,
            ReadOnly = true,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            RowHeadersVisible = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        };

        // Column 0 IDX
        grid.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "IDX",
            HeaderText = "IDX",
            DataPropertyName = "Idx"
        });

        // Column 1 RGB text
        grid.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "RGB",
            HeaderText = "RGB",
            DataPropertyName = "RgbText"
        });

        // Column 2 colour swatch (background only)
        grid.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Colour",
            HeaderText = "Colour",
            ReadOnly = true
        });

        // Column 3 – comment
        grid.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Comment",
            HeaderText = "Comment",
            DataPropertyName = "Comment",
            AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        });


        var data = entries.Select(e => new
        {
            e.Idx,
            RgbText = $"{e.R},{e.G},{e.B}",
            e.Comment,
            Swatch = e.ToColor()
        }).ToList();

        grid.DataSource = data;

        // runtime painting of the swatch cell 
        grid.CellFormatting += (s, e) =>
        {
            if (grid.Columns[e.ColumnIndex].Name == "Colour" && e.RowIndex >= 0)
            {
                var c = data[e.RowIndex].Swatch;
                e.CellStyle.BackColor = c;

                // Choose white text if colour is dark
                e.CellStyle.ForeColor =
                    c.GetBrightness() < 0.4f ? Color.White : Color.Black;

                e.Value = string.Empty;     // no text inside the swatch cell
                e.FormattingApplied = true;
            }
        };

        return grid;
    }
}
