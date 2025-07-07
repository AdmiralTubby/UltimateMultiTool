using System;
using System.Threading;
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
    public static void ShowDiffInThread(string left, string right)
    {
        var t = new Thread(_ =>
        {
            ApplicationConfiguration.Initialize();     // ensure visual styles
            Application.Run(new DiffViewer(left, right));
        });
        t.SetApartmentState(ApartmentState.STA);
        t.IsBackground = true;
        t.Start();
    }


}
