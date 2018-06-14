using System;
using System.Windows.Forms;

[assembly:log4net.Config.XmlConfigurator(Watch =true)]

namespace filebrowser
{
    static class Program
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger();
      
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#if DEBUG
            Application.Run(new Client());
            //Application.Run(new Login());
#else
            Application.Run(new Login());
#endif

        }
    }
}
