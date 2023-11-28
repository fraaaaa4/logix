using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using Microsoft.Win32;


namespace PrologParsec
{
   

    partial class AboutBox1 : Form
    {
        

        public AboutBox1()
        {
            InitializeComponent();

            //  Initialize the AboutBox to display the product information from the assembly information.
            //  Change assembly information settings for your application through either:
            //  - Project->Properties->Application->Assembly Information
            //  - AssemblyInfo.cs
            this.Text = String.Format("About {0}", Properties.Resources.AppName + " " + Properties.Resources.AppEdition);

            WindowsCheck();
            
        }

        private void WindowsCheck()
        {
            //check windows version
            Version version = NtDll.RtlGetVersion();
            if (version.Major==  4 || version.Major==  5 && (version.Minor== 0 || version.Minor==  1 || version.Minor==  2) || (version.Major==  6 && (version.Minor==  0 || version.Minor==  1) ))
            {
                webBrowser1.Visible = false;
                pictureBox2.Visible = true;
            }
            else if (version.Major== 6 && (version.Minor==  2 || version.Minor== 3) || version.Major==  10)
            {
                webBrowser1.Visible = true;
                pictureBox2.Visible = false;
            }
            else
            {
                webBrowser1.Visible = false;
                pictureBox2.Visible = true;
            }

            //debugging Windows version
            label7.Text = "Windows NT " + version.Major;
            label9.Text = "Version " + version.Major + "." + version.Minor + " build " + version.Build;

            //Windows 9x check
            if (label7.Text.Equals(string.Empty) || label9.Text.Equals(string.Empty))
            {
                label7.Text = "Windows 9x/2000";
                label9.Text = "4.10 or 4.90/NT 5.0";
            }

            //windows version check
            if (version.Major ==  5 == true && version.Minor==  0 == true)
            {
                pictureBox3.Image = Properties.Resources.ME_about;
            } else if (version.Major ==  5 && (version.Minor==  1 || version.Minor==  2))
            {
                pictureBox3.Image = Properties.Resources.XP_about;
            }
            else if (version.Major ==  6 && version.Minor==  0)
            {
                pictureBox3.Image = Properties.Resources.vista_about;
            }
            else if (version.Major==  6 && version.Minor==  1)
            {
                pictureBox3.Image = Properties.Resources._7_about;
            }
            else if (version.Major==  6 && version.Minor==  2)
            {
                pictureBox3.Image = Properties.Resources._8_about;
            }
            else if ((version.Major==  6 && version.Minor==  3) || (version.Major==  10))
            {
                pictureBox3.Image = Properties.Resources._8_about;
            }
            else
            {
                pictureBox3.Image = Properties.Resources.ME_about;
            }

        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                // Get all Title attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                // If there is at least one Title attribute
                if (attributes.Length > 0)
                {
                    // Select the first one
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    // If it is not an empty string, return it
                    if (titleAttribute.Title != "")
                        return titleAttribute.Title;
                }
                // If there was no Title attribute, or if the Title attribute was the empty string, return the .exe name
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                // Get all Description attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                // If there aren't any Description attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Description attribute, return its value
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                // Get all Product attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                // If there aren't any Product attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Product attribute, return its value
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                // Get all Copyright attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                // If there aren't any Copyright attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Copyright attribute, return its value
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                // Get all Company attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                // If there aren't any Company attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Company attribute, return its value
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void tableLayoutPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBoxDescription_TextChanged(object sender, EventArgs e)
        {

        }

        private void AboutBox1_Load(object sender, EventArgs e)
        {
            string curDir = Directory.GetCurrentDirectory();
            this.webBrowser1.Url = new Uri(String.Format("file:///{0}/aurora/" + Properties.Settings.Default.auroraFile, curDir));

            label1.Text = Properties.Resources.AppName;
            label2.Text = Properties.Resources.AppEdition + " Edition";
            label3.Text = Properties.Resources.AppDescription;
            label4.Text = Properties.Resources.AppDescription2;
            label5.Text = "Version " + Properties.Resources.AppVersion;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
