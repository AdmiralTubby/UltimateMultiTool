using System;
using System.Media;
using System.Windows.Forms;

namespace AbadUltimateTool;

public partial class SplashScreen : Form
{

    public int FirstTimePress = 0;

    // 40 ticks * 50 ms will be around 2000 ms
    private readonly Timer _fadeTimer = new Timer { Interval = 50 };
    private const double FadeStep = 0.025;   // 0 to 1 in 40 steps

    public SplashScreen()
    {
        InitializeComponent();


        _fadeTimer.Tick += FadeInTick;

        Shown += (s, e) =>
        {

            _fadeTimer.Start();

        };

    }


    private void FadeInTick(object sender, EventArgs e)
    {
        if (Opacity < 1.0)
        {
            SplashStartButton.Visible = false;
            this.Opacity = Math.Min(this.Opacity + FadeStep, 1.0);
        }
        else
        {
            _fadeTimer.Stop();               // finished fading
            SplashStartButton.Visible = true;
        }
    }

    private void SplashStartButton_Click(object sender, EventArgs e)
    {
 
        this.Close();

    }


}
