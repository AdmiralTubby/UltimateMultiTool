
namespace AbadUltimateTool;

partial class UltimateMultiTool
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UltimateMultiTool));
        mainTabControl = new System.Windows.Forms.TabControl();
        tabPage1 = new System.Windows.Forms.TabPage();
        cmdLoadPalettes = new System.Windows.Forms.Button();
        tabPalettes = new System.Windows.Forms.TabControl();
        paletteTab1 = new System.Windows.Forms.TabPage();
        paletteTab2 = new System.Windows.Forms.TabPage();
        tabPage2 = new System.Windows.Forms.TabPage();
        cmdExportRange = new System.Windows.Forms.Button();
        cmdExportIps = new System.Windows.Forms.Button();
        cmdLoadIps = new System.Windows.Forms.Button();
        tabIps = new System.Windows.Forms.TabControl();
        tabPage5 = new System.Windows.Forms.TabPage();
        tabPage6 = new System.Windows.Forms.TabPage();
        tabPage3 = new System.Windows.Forms.TabPage();
        cmdEditExcl = new System.Windows.Forms.Button();
        cmdNeedHelp = new System.Windows.Forms.Button();
        cmdSyncSelected = new System.Windows.Forms.Button();
        gridDiff = new System.Windows.Forms.DataGridView();
        cmdRefreshDiff = new System.Windows.Forms.Button();
        cmdCompare = new System.Windows.Forms.Button();
        cmdIndexSrc = new System.Windows.Forms.Button();
        cmdViewDiff = new System.Windows.Forms.Button();
        mainTabControl.SuspendLayout();
        tabPage1.SuspendLayout();
        tabPalettes.SuspendLayout();
        tabPage2.SuspendLayout();
        tabIps.SuspendLayout();
        tabPage3.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)gridDiff).BeginInit();
        SuspendLayout();
        // 
        // mainTabControl
        // 
        mainTabControl.Controls.Add(tabPage1);
        mainTabControl.Controls.Add(tabPage2);
        mainTabControl.Controls.Add(tabPage3);
        mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
        mainTabControl.Location = new System.Drawing.Point(0, 0);
        mainTabControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        mainTabControl.Name = "mainTabControl";
        mainTabControl.SelectedIndex = 0;
        mainTabControl.Size = new System.Drawing.Size(1904, 1041);
        mainTabControl.TabIndex = 0;
        // 
        // tabPage1
        // 
        tabPage1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
        tabPage1.Controls.Add(cmdLoadPalettes);
        tabPage1.Controls.Add(tabPalettes);
        tabPage1.Location = new System.Drawing.Point(4, 24);
        tabPage1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        tabPage1.Name = "tabPage1";
        tabPage1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
        tabPage1.Size = new System.Drawing.Size(1896, 1013);
        tabPage1.TabIndex = 0;
        tabPage1.Text = "Palette Color Visualizer";
        tabPage1.Click += tabPage1_Click;
        // 
        // cmdLoadPalettes
        // 
        cmdLoadPalettes.Cursor = System.Windows.Forms.Cursors.Hand;
        cmdLoadPalettes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
        cmdLoadPalettes.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
        cmdLoadPalettes.Location = new System.Drawing.Point(9, 29);
        cmdLoadPalettes.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        cmdLoadPalettes.Name = "cmdLoadPalettes";
        cmdLoadPalettes.Size = new System.Drawing.Size(173, 39);
        cmdLoadPalettes.TabIndex = 1;
        cmdLoadPalettes.Text = "Load Palettes...";
        cmdLoadPalettes.UseVisualStyleBackColor = true;
        cmdLoadPalettes.Click += cmdLoadPalettes_Click;
        // 
        // tabPalettes
        // 
        tabPalettes.Controls.Add(paletteTab1);
        tabPalettes.Controls.Add(paletteTab2);
        tabPalettes.Location = new System.Drawing.Point(189, 3);
        tabPalettes.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        tabPalettes.Name = "tabPalettes";
        tabPalettes.SelectedIndex = 0;
        tabPalettes.Size = new System.Drawing.Size(1700, 1000);
        tabPalettes.TabIndex = 0;
        // 
        // paletteTab1
        // 
        paletteTab1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
        paletteTab1.Location = new System.Drawing.Point(4, 24);
        paletteTab1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        paletteTab1.Name = "paletteTab1";
        paletteTab1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
        paletteTab1.Size = new System.Drawing.Size(1692, 972);
        paletteTab1.TabIndex = 0;
        paletteTab1.Text = "paletteTab1";
        // 
        // paletteTab2
        // 
        paletteTab2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
        paletteTab2.Location = new System.Drawing.Point(4, 24);
        paletteTab2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        paletteTab2.Name = "paletteTab2";
        paletteTab2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
        paletteTab2.Size = new System.Drawing.Size(1692, 972);
        paletteTab2.TabIndex = 1;
        paletteTab2.Text = "paletteTab2";
        // 
        // tabPage2
        // 
        tabPage2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
        tabPage2.Controls.Add(cmdExportRange);
        tabPage2.Controls.Add(cmdExportIps);
        tabPage2.Controls.Add(cmdLoadIps);
        tabPage2.Controls.Add(tabIps);
        tabPage2.Location = new System.Drawing.Point(4, 24);
        tabPage2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        tabPage2.Name = "tabPage2";
        tabPage2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
        tabPage2.Size = new System.Drawing.Size(1896, 1013);
        tabPage2.TabIndex = 1;
        tabPage2.Text = "IP List";
        // 
        // cmdExportRange
        // 
        cmdExportRange.Cursor = System.Windows.Forms.Cursors.Hand;
        cmdExportRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
        cmdExportRange.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
        cmdExportRange.Location = new System.Drawing.Point(10, 188);
        cmdExportRange.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        cmdExportRange.Name = "cmdExportRange";
        cmdExportRange.Size = new System.Drawing.Size(173, 39);
        cmdExportRange.TabIndex = 4;
        cmdExportRange.Text = "Export Available Range...";
        cmdExportRange.UseVisualStyleBackColor = true;
        cmdExportRange.Click += cmdExportRange_Click;
        // 
        // cmdExportIps
        // 
        cmdExportIps.Cursor = System.Windows.Forms.Cursors.Hand;
        cmdExportIps.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
        cmdExportIps.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
        cmdExportIps.Location = new System.Drawing.Point(9, 111);
        cmdExportIps.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        cmdExportIps.Name = "cmdExportIps";
        cmdExportIps.Size = new System.Drawing.Size(173, 39);
        cmdExportIps.TabIndex = 3;
        cmdExportIps.Text = "Export IPs to CSV...";
        cmdExportIps.UseVisualStyleBackColor = true;
        cmdExportIps.Click += cmdExportIps_Click;
        // 
        // cmdLoadIps
        // 
        cmdLoadIps.Cursor = System.Windows.Forms.Cursors.Hand;
        cmdLoadIps.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
        cmdLoadIps.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
        cmdLoadIps.Location = new System.Drawing.Point(9, 32);
        cmdLoadIps.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        cmdLoadIps.Name = "cmdLoadIps";
        cmdLoadIps.Size = new System.Drawing.Size(173, 39);
        cmdLoadIps.TabIndex = 2;
        cmdLoadIps.Text = "Scan IPs...";
        cmdLoadIps.UseVisualStyleBackColor = true;
        cmdLoadIps.Click += cmdLoadIps_Click;
        // 
        // tabIps
        // 
        tabIps.Controls.Add(tabPage5);
        tabIps.Controls.Add(tabPage6);
        tabIps.Location = new System.Drawing.Point(190, 7);
        tabIps.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        tabIps.Name = "tabIps";
        tabIps.SelectedIndex = 0;
        tabIps.Size = new System.Drawing.Size(1700, 1000);
        tabIps.TabIndex = 1;
        // 
        // tabPage5
        // 
        tabPage5.BackColor = System.Drawing.SystemColors.ControlDarkDark;
        tabPage5.Location = new System.Drawing.Point(4, 24);
        tabPage5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        tabPage5.Name = "tabPage5";
        tabPage5.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
        tabPage5.Size = new System.Drawing.Size(1692, 972);
        tabPage5.TabIndex = 0;
        tabPage5.Text = "tabPage5";
        // 
        // tabPage6
        // 
        tabPage6.BackColor = System.Drawing.SystemColors.ControlDarkDark;
        tabPage6.Location = new System.Drawing.Point(4, 24);
        tabPage6.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        tabPage6.Name = "tabPage6";
        tabPage6.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
        tabPage6.Size = new System.Drawing.Size(1692, 972);
        tabPage6.TabIndex = 1;
        tabPage6.Text = "tabPage6";
        // 
        // tabPage3
        // 
        tabPage3.BackColor = System.Drawing.SystemColors.ControlDarkDark;
        tabPage3.Controls.Add(cmdViewDiff);
        tabPage3.Controls.Add(cmdEditExcl);
        tabPage3.Controls.Add(cmdNeedHelp);
        tabPage3.Controls.Add(cmdSyncSelected);
        tabPage3.Controls.Add(gridDiff);
        tabPage3.Controls.Add(cmdRefreshDiff);
        tabPage3.Controls.Add(cmdCompare);
        tabPage3.Controls.Add(cmdIndexSrc);
        tabPage3.Location = new System.Drawing.Point(4, 24);
        tabPage3.Name = "tabPage3";
        tabPage3.Padding = new System.Windows.Forms.Padding(3);
        tabPage3.Size = new System.Drawing.Size(1896, 1013);
        tabPage3.TabIndex = 2;
        tabPage3.Text = "File Diff";
        tabPage3.Click += tabPage3_Click;
        // 
        // cmdEditExcl
        // 
        cmdEditExcl.Cursor = System.Windows.Forms.Cursors.Hand;
        cmdEditExcl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
        cmdEditExcl.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
        cmdEditExcl.Location = new System.Drawing.Point(9, 290);
        cmdEditExcl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        cmdEditExcl.Name = "cmdEditExcl";
        cmdEditExcl.Size = new System.Drawing.Size(173, 39);
        cmdEditExcl.TabIndex = 8;
        cmdEditExcl.Text = "Add exclusions...";
        cmdEditExcl.UseVisualStyleBackColor = true;
        cmdEditExcl.Click += cmdEditExcl_Click;
        // 
        // cmdNeedHelp
        // 
        cmdNeedHelp.BackColor = System.Drawing.SystemColors.ControlLightLight;
        cmdNeedHelp.Cursor = System.Windows.Forms.Cursors.Hand;
        cmdNeedHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
        cmdNeedHelp.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
        cmdNeedHelp.Location = new System.Drawing.Point(9, 459);
        cmdNeedHelp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        cmdNeedHelp.Name = "cmdNeedHelp";
        cmdNeedHelp.Size = new System.Drawing.Size(173, 39);
        cmdNeedHelp.TabIndex = 7;
        cmdNeedHelp.Text = "Need some help?";
        cmdNeedHelp.UseVisualStyleBackColor = false;
        cmdNeedHelp.Click += cmdNeedHelp_Click;
        // 
        // cmdSyncSelected
        // 
        cmdSyncSelected.Cursor = System.Windows.Forms.Cursors.Hand;
        cmdSyncSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
        cmdSyncSelected.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
        cmdSyncSelected.Location = new System.Drawing.Point(9, 229);
        cmdSyncSelected.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        cmdSyncSelected.Name = "cmdSyncSelected";
        cmdSyncSelected.Size = new System.Drawing.Size(173, 39);
        cmdSyncSelected.TabIndex = 6;
        cmdSyncSelected.Text = "Sync files...";
        cmdSyncSelected.UseVisualStyleBackColor = true;
        cmdSyncSelected.Click += cmdSyncSelected_Click;
        // 
        // gridDiff
        // 
        gridDiff.AllowUserToAddRows = false;
        gridDiff.AllowUserToDeleteRows = false;
        gridDiff.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        gridDiff.Location = new System.Drawing.Point(193, 6);
        gridDiff.Name = "gridDiff";
        gridDiff.ReadOnly = true;
        gridDiff.Size = new System.Drawing.Size(1700, 1000);
        gridDiff.TabIndex = 5;
        gridDiff.CellDoubleClick += gridDiff_CellDoubleClick;
        // 
        // cmdRefreshDiff
        // 
        cmdRefreshDiff.Cursor = System.Windows.Forms.Cursors.Hand;
        cmdRefreshDiff.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
        cmdRefreshDiff.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
        cmdRefreshDiff.Location = new System.Drawing.Point(9, 166);
        cmdRefreshDiff.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        cmdRefreshDiff.Name = "cmdRefreshDiff";
        cmdRefreshDiff.Size = new System.Drawing.Size(173, 39);
        cmdRefreshDiff.TabIndex = 4;
        cmdRefreshDiff.Text = "Refresh...";
        cmdRefreshDiff.UseVisualStyleBackColor = true;
        cmdRefreshDiff.Click += cmdRefreshDiff_Click;
        // 
        // cmdCompare
        // 
        cmdCompare.Cursor = System.Windows.Forms.Cursors.Hand;
        cmdCompare.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
        cmdCompare.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
        cmdCompare.Location = new System.Drawing.Point(9, 99);
        cmdCompare.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        cmdCompare.Name = "cmdCompare";
        cmdCompare.Size = new System.Drawing.Size(173, 39);
        cmdCompare.TabIndex = 3;
        cmdCompare.Text = "Compare...";
        cmdCompare.UseVisualStyleBackColor = true;
        cmdCompare.Click += cmdCompare_Click;
        // 
        // cmdIndexSrc
        // 
        cmdIndexSrc.Cursor = System.Windows.Forms.Cursors.Hand;
        cmdIndexSrc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
        cmdIndexSrc.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
        cmdIndexSrc.Location = new System.Drawing.Point(9, 30);
        cmdIndexSrc.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        cmdIndexSrc.Name = "cmdIndexSrc";
        cmdIndexSrc.Size = new System.Drawing.Size(173, 39);
        cmdIndexSrc.TabIndex = 2;
        cmdIndexSrc.Text = "Index Source...";
        cmdIndexSrc.UseVisualStyleBackColor = true;
        cmdIndexSrc.Click += cmdIndexSrc_Click;
        // 
        // cmdViewDiff
        // 
        cmdViewDiff.Cursor = System.Windows.Forms.Cursors.Hand;
        cmdViewDiff.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
        cmdViewDiff.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
        cmdViewDiff.Location = new System.Drawing.Point(9, 354);
        cmdViewDiff.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        cmdViewDiff.Name = "cmdViewDiff";
        cmdViewDiff.Size = new System.Drawing.Size(173, 39);
        cmdViewDiff.TabIndex = 9;
        cmdViewDiff.Text = "View differences...";
        cmdViewDiff.UseVisualStyleBackColor = true;
        cmdViewDiff.Click += cmdViewDiff_Click;
        // 
        // UltimateMultiTool
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
        BackColor = System.Drawing.SystemColors.ControlDarkDark;
        ClientSize = new System.Drawing.Size(1904, 1041);
        Controls.Add(mainTabControl);
        Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
        Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        Name = "UltimateMultiTool";
        Opacity = 0D;
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "UltimateMultiTool";
        WindowState = System.Windows.Forms.FormWindowState.Maximized;
        mainTabControl.ResumeLayout(false);
        tabPage1.ResumeLayout(false);
        tabPalettes.ResumeLayout(false);
        tabPage2.ResumeLayout(false);
        tabIps.ResumeLayout(false);
        tabPage3.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)gridDiff).EndInit();
        ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Timer fadeTimerForm;
    private System.Windows.Forms.TabControl mainTabControl;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.TabControl tabPalettes;
    private System.Windows.Forms.TabPage paletteTab1;
    private System.Windows.Forms.TabPage paletteTab2;
    private System.Windows.Forms.Button cmdLoadPalettes;
    private System.Windows.Forms.Button cmdExportIps;
    private System.Windows.Forms.Button cmdLoadIps;
    private System.Windows.Forms.TabControl tabIps;
    private System.Windows.Forms.TabPage tabPage5;
    private System.Windows.Forms.TabPage tabPage6;
    private System.Windows.Forms.Button cmdExportRange;
    private System.Windows.Forms.TabPage tabPage3;
    private System.Windows.Forms.Button cmdIndexSrc;
    private System.Windows.Forms.Button cmdCompare;
    private System.Windows.Forms.Button cmdRefreshDiff;
    private System.Windows.Forms.DataGridView gridDiff;
    private System.Windows.Forms.Button cmdSyncSelected;
    private System.Windows.Forms.Button cmdNeedHelp;
    private System.Windows.Forms.Button cmdEditExcl;
    private System.Windows.Forms.Button cmdViewDiff;
}

