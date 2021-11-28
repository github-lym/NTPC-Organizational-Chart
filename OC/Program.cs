using System;
using System.Collections.Generic;
//using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace OC
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Boolean bCreatedNew;
            //Create a new mutex using specific mutex name
            Mutex m = new Mutex(false, "OC", out bCreatedNew);

            if (bCreatedNew)
                Application.Run(new Form1());
        }
    }
}
