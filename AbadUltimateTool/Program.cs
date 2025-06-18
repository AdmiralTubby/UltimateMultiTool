using System;
using System.Windows.Forms;

namespace AbadUltimateTool;

static class Program
{

    // here we go
    class StartupContext : ApplicationContext
    {
        public StartupContext()
        {
            var splash = new SplashScreen();
            splash.FormClosed += SplashClosed;
            splash.Show();
        }

        private void SplashClosed(object sender, EventArgs e)
        {
            var main = new UltimateMultiTool();
            main.FormClosed += (s, _) => ExitThread();   // terminate splash here (don't get wet)
            main.Show();
        }
    }

    [STAThread]

    static void Main()
    {
        ExcludeFolderStore.Load(); // load up all those nice strings

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        // show splash screen (like a baws)
        using (var splash = new SplashScreen())
        {
            splash.ShowDialog();
            //Application.Run(splash);
        }

        // open the real form (like a baws)
        Application.Run(new UltimateMultiTool());

    }
}
