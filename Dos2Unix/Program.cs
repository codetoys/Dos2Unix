using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace Dos2Unix
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
			//Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
			//MessageBox.Show(Thread.CurrentThread.CurrentCulture.Name, Thread.CurrentThread.CurrentUICulture.Name);
			Application.Run(new Form1());
        }
    }
}