
namespace AbadUltimateTool;

partial class SplashScreen
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
        components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
        SplashStartButton = new System.Windows.Forms.Button();
        pictureBox3 = new System.Windows.Forms.PictureBox();
        pictureBox1 = new System.Windows.Forms.PictureBox();
        pictureBox2 = new System.Windows.Forms.PictureBox();
        bindingSource1 = new System.Windows.Forms.BindingSource(components);
        ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
        ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
        SuspendLayout();
        // 
        // SplashStartButton
        // 
        SplashStartButton.Cursor = System.Windows.Forms.Cursors.Hand;
        SplashStartButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(64, 64, 64);
        SplashStartButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        SplashStartButton.Image = Properties.Resources.StartGif;
        SplashStartButton.Location = new System.Drawing.Point(315, 315);
        SplashStartButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        SplashStartButton.Name = "SplashStartButton";
        SplashStartButton.Size = new System.Drawing.Size(301, 81);
        SplashStartButton.TabIndex = 3;
        SplashStartButton.UseVisualStyleBackColor = true;
        SplashStartButton.Click += SplashStartButton_Click;
        // 
        // pictureBox3
        // 
        pictureBox3.Image = Properties.Resources.LogoMirrored;
        pictureBox3.InitialImage = (System.Drawing.Image)resources.GetObject("pictureBox3.InitialImage");
        pictureBox3.Location = new System.Drawing.Point(14, 14);
        pictureBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        pictureBox3.Name = "pictureBox3";
        pictureBox3.Size = new System.Drawing.Size(200, 166);
        pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        pictureBox3.TabIndex = 2;
        pictureBox3.TabStop = false;
        // 
        // pictureBox1
        // 
        pictureBox1.Image = Properties.Resources.Logo;
        pictureBox1.InitialImage = (System.Drawing.Image)resources.GetObject("pictureBox1.InitialImage");
        pictureBox1.Location = new System.Drawing.Point(720, 14);
        pictureBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new System.Drawing.Size(200, 166);
        pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        pictureBox1.TabIndex = 0;
        pictureBox1.TabStop = false;
        // 
        // pictureBox2
        // 
        pictureBox2.Image = Properties.Resources.LogoTool;
        pictureBox2.Location = new System.Drawing.Point(245, -63);
        pictureBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        pictureBox2.Name = "pictureBox2";
        pictureBox2.Size = new System.Drawing.Size(444, 285);
        pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        pictureBox2.TabIndex = 1;
        pictureBox2.TabStop = false;
        // 
        // SplashScreen
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.Color.FromArgb(64, 64, 64);
        ClientSize = new System.Drawing.Size(933, 462);
        ControlBox = false;
        Controls.Add(SplashStartButton);
        Controls.Add(pictureBox3);
        Controls.Add(pictureBox1);
        Controls.Add(pictureBox2);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
        Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "SplashScreen";
        Opacity = 0D;
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
        ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
        ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.PictureBox pictureBox2;
    private System.Windows.Forms.PictureBox pictureBox3;
    private System.Windows.Forms.Button SplashStartButton;
    private System.Windows.Forms.BindingSource bindingSource1;
}