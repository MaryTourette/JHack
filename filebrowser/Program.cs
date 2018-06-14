using System;
using System.Windows.Forms;

namespace filebrowser
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#if DEBUG
            //Application.Run(new Form1());
            Application.Run(new Login());
#else
            Application.Run(new Login());
#endif

        }
    }
}
