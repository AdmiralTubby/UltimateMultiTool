
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
        this.components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
        this.SplashStartButton = new System.Windows.Forms.Button();
        this.pictureBox3 = new System.Windows.Forms.PictureBox();
        this.pictureBox1 = new System.Windows.Forms.PictureBox();
        this.pictureBox2 = new System.Windows.Forms.PictureBox();
        this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
        this.button1 = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
        this.SuspendLayout();
        // 
        // SplashStartButton
        // 
        this.SplashStartButton.Cursor = System.Windows.Forms.Cursors.Hand;
        this.SplashStartButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
        this.SplashStartButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.SplashStartButton.Image = global::AbadUltimateTool.Properties.Resources.StartGif;
        this.SplashStartButton.Location = new System.Drawing.Point(270, 273);
        this.SplashStartButton.Name = "SplashStartButton";
        this.SplashStartButton.Size = new System.Drawing.Size(258, 70);
        this.SplashStartButton.TabIndex = 3;
        this.SplashStartButton.UseVisualStyleBackColor = true;
        this.SplashStartButton.Click += new System.EventHandler(this.SplashStartButton_Click);
        // 
        // pictureBox3
        // 
        this.pictureBox3.Image = global::AbadUltimateTool.Properties.Resources.LogoMirrored;
        this.pictureBox3.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.InitialImage")));
        this.pictureBox3.Location = new System.Drawing.Point(12, 12);
        this.pictureBox3.Name = "pictureBox3";
        this.pictureBox3.Size = new System.Drawing.Size(171, 144);
        this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        this.pictureBox3.TabIndex = 2;
        this.pictureBox3.TabStop = false;
        // 
        // pictureBox1
        // 
        this.pictureBox1.Image = global::AbadUltimateTool.Properties.Resources.Logo;
        this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
        this.pictureBox1.Location = new System.Drawing.Point(617, 12);
        this.pictureBox1.Name = "pictureBox1";
        this.pictureBox1.Size = new System.Drawing.Size(171, 144);
        this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        this.pictureBox1.TabIndex = 0;
        this.pictureBox1.TabStop = false;
        // 
        // pictureBox2
        // 
        this.pictureBox2.Image = global::AbadUltimateTool.Properties.Resources.LogoTool;
        this.pictureBox2.Location = new System.Drawing.Point(210, -55);
        this.pictureBox2.Name = "pictureBox2";
        this.pictureBox2.Size = new System.Drawing.Size(381, 247);
        this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        this.pictureBox2.TabIndex = 1;
        this.pictureBox2.TabStop = false;
        // 
        // button1
        // 
        this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
        this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
        this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.button1.Location = new System.Drawing.Point(703, 162);
        this.button1.Name = "button1";
        this.button1.Size = new System.Drawing.Size(50, 39);
        this.button1.TabIndex = 4;
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new System.EventHandler(this.playMusic);
        // 
        // SplashScreen
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
        this.ClientSize = new System.Drawing.Size(800, 400);
        this.ControlBox = false;
        this.Controls.Add(this.button1);
        this.Controls.Add(this.SplashStartButton);
        this.Controls.Add(this.pictureBox3);
        this.Controls.Add(this.pictureBox1);
        this.Controls.Add(this.pictureBox2);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "SplashScreen";
        this.Opacity = 0D;
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.PictureBox pictureBox2;
    private System.Windows.Forms.PictureBox pictureBox3;
    private System.Windows.Forms.Button SplashStartButton;
    private System.Windows.Forms.BindingSource bindingSource1;
    private System.Windows.Forms.Button button1;
}