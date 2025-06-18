namespace AbadUltimateTool;

partial class ExcludeEditor
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
        lstNames = new System.Windows.Forms.ListBox();
        txtNew = new System.Windows.Forms.TextBox();
        btnAdd = new System.Windows.Forms.Button();
        btnRemove = new System.Windows.Forms.Button();
        btnClose = new System.Windows.Forms.Button();
        btnSave = new System.Windows.Forms.Button();
        SuspendLayout();
        // 
        // lstNames
        // 
        lstNames.FormattingEnabled = true;
        lstNames.ItemHeight = 15;
        lstNames.Location = new System.Drawing.Point(43, 96);
        lstNames.Name = "lstNames";
        lstNames.Size = new System.Drawing.Size(304, 439);
        lstNames.TabIndex = 0;
        // 
        // txtNew
        // 
        txtNew.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtNew.Cursor = System.Windows.Forms.Cursors.IBeam;
        txtNew.Location = new System.Drawing.Point(44, 24);
        txtNew.MaxLength = 40;
        txtNew.Name = "txtNew";
        txtNew.PlaceholderText = "Write new exceptions here (one at a time)";
        txtNew.Size = new System.Drawing.Size(303, 23);
        txtNew.TabIndex = 1;
        // 
        // btnAdd
        // 
        btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
        btnAdd.Location = new System.Drawing.Point(44, 53);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new System.Drawing.Size(111, 23);
        btnAdd.TabIndex = 2;
        btnAdd.Text = "Add exceptions";
        btnAdd.UseVisualStyleBackColor = true;
        btnAdd.Click += btnAdd_Click;
        // 
        // btnRemove
        // 
        btnRemove.Cursor = System.Windows.Forms.Cursors.Hand;
        btnRemove.Location = new System.Drawing.Point(44, 543);
        btnRemove.Name = "btnRemove";
        btnRemove.Size = new System.Drawing.Size(143, 46);
        btnRemove.TabIndex = 3;
        btnRemove.Text = "Remove selected exceptions";
        btnRemove.UseVisualStyleBackColor = true;
        btnRemove.Click += btnRemove_Click;
        // 
        // btnClose
        // 
        btnClose.BackColor = System.Drawing.SystemColors.ButtonFace;
        btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
        btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Firebrick;
        btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
        btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
        btnClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
        btnClose.Location = new System.Drawing.Point(207, 555);
        btnClose.Name = "btnClose";
        btnClose.Size = new System.Drawing.Size(120, 23);
        btnClose.TabIndex = 4;
        btnClose.Text = "Close dialogue";
        btnClose.UseVisualStyleBackColor = false;
        btnClose.Click += btnClose_Click;
        // 
        // btnSave
        // 
        btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
        btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Chartreuse;
        btnSave.Location = new System.Drawing.Point(236, 53);
        btnSave.Name = "btnSave";
        btnSave.Size = new System.Drawing.Size(111, 23);
        btnSave.TabIndex = 5;
        btnSave.Text = "Save";
        btnSave.UseVisualStyleBackColor = true;
        btnSave.Click += btnSave_Click;
        // 
        // ExcludeEditor
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(375, 607);
        ControlBox = false;
        Controls.Add(btnSave);
        Controls.Add(btnClose);
        Controls.Add(btnRemove);
        Controls.Add(btnAdd);
        Controls.Add(txtNew);
        Controls.Add(lstNames);
        Name = "ExcludeEditor";
        Text = "ExcludeEditor";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private System.Windows.Forms.ListBox lstNames;
    private System.Windows.Forms.TextBox txtNew;
    private System.Windows.Forms.Button btnAdd;
    private System.Windows.Forms.Button btnRemove;
    private System.Windows.Forms.Button btnClose;
    private System.Windows.Forms.Button btnSave;
}