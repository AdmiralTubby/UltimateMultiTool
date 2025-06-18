namespace AbadUltimateTool;

partial class DiffViewer
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
        splitMain = new System.Windows.Forms.SplitContainer();
        rtbLeft = new System.Windows.Forms.RichTextBox();
        rtbRight = new System.Windows.Forms.RichTextBox();
        ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
        splitMain.Panel1.SuspendLayout();
        splitMain.Panel2.SuspendLayout();
        splitMain.SuspendLayout();
        SuspendLayout();
        // 
        // splitMain
        // 
        splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
        splitMain.Location = new System.Drawing.Point(0, 0);
        splitMain.Name = "splitMain";
        // 
        // splitMain.Panel1
        // 
        splitMain.Panel1.Controls.Add(rtbLeft);
        // 
        // splitMain.Panel2
        // 
        splitMain.Panel2.Controls.Add(rtbRight);
        splitMain.Size = new System.Drawing.Size(1904, 1041);
        splitMain.SplitterDistance = 932;
        splitMain.TabIndex = 0;
        // 
        // rtbLeft
        // 
        rtbLeft.Dock = System.Windows.Forms.DockStyle.Fill;
        rtbLeft.Location = new System.Drawing.Point(0, 0);
        rtbLeft.Name = "rtbLeft";
        rtbLeft.Size = new System.Drawing.Size(932, 1041);
        rtbLeft.TabIndex = 0;
        rtbLeft.Text = "";
        // 
        // rtbRight
        // 
        rtbRight.Dock = System.Windows.Forms.DockStyle.Fill;
        rtbRight.Location = new System.Drawing.Point(0, 0);
        rtbRight.Name = "rtbRight";
        rtbRight.Size = new System.Drawing.Size(968, 1041);
        rtbRight.TabIndex = 0;
        rtbRight.Text = "";
        rtbRight.TextChanged += rtbRight_TextChanged;
        // 
        // DiffViewer
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(1904, 1041);
        Controls.Add(splitMain);
        Name = "DiffViewer";
        Text = "DiffViewer";
        splitMain.Panel1.ResumeLayout(false);
        splitMain.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
        splitMain.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.SplitContainer splitMain;
    private System.Windows.Forms.RichTextBox rtbLeft;
    private System.Windows.Forms.RichTextBox rtbRight;
}