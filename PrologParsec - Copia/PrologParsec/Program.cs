using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace PrologParsec
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
            try
            {
                Application.Run(new MDIParent1());
            }
            catch (IndexOutOfRangeException ex)
            {
            }


        }
    }
}