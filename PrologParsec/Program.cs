using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;


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
                Thread thread = new Thread(OpenForm);
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                
            }
            catch (ArgumentException)
            {
                
            }


        }

        public static void OpenForm()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (Properties.Settings.Default.gramlexExclusive == false)
                Application.Run(new MDIParent1());
            else
                Application.Run(new LogixPE());
        }
    }
}