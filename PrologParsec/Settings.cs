using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using Microsoft.Win32;
using mshtml;
using Transitions;
using System.Collections.Generic;

namespace PrologParsec
{
    public partial class Settings : Form
    {
        MDIParent1 mdiparent = new MDIParent1();
        private WebBrowser webBrowser; //web browser reference
        private SplitContainer split1;
        private ToolStripMenuItem context; private ContextMenuStrip context2; //start page add-remove
        private ToolStripMenuItem contextWeb; private ToolStripMenuItem contextWeb2; private ContextMenuStrip rightclick; private MenuStrip menubar;
        private int auroraHeight; private FarsiLibrary.Win.FATabStrip startPage; private FarsiLibrary.Win.FATabStripItem startPageTab; //tabs reference
        private bool Windows9x; private bool resize9x;


        public Settings(WebBrowser webBrowser, FarsiLibrary.Win.FATabStrip startPage, FarsiLibrary.Win.FATabStripItem startPageTab, SplitContainer split1, ToolStripMenuItem context, ContextMenuStrip context2, ToolStripMenuItem contextWeb, ContextMenuStrip rightclick,ToolStripMenuItem contextWeb2, MenuStrip menubar) //constructor
        {
            InitializeComponent();
            this.split1 = split1;
            this.webBrowser = webBrowser; //construct reference for aurora web browser
            this.startPage = startPage; this.startPageTab = startPageTab; //construct reference for start page tab
            this.context = context; this.context2 = context2;
            if (Properties.Settings.Default.auroraCustom) //custom aurora handler - if it's custom then load the page in the preview
                webBrowser1.Navigate(Properties.Settings.Default.auroraCustomURL);
            this.contextWeb = contextWeb; this.contextWeb2 = contextWeb2; this.rightclick = rightclick; this.menubar = menubar; //search on the internet 
            
        }

        static string productName; static string currentVersion; static string buildLab;
        static string GetWindowsVersion()
        {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion")) //using the registry key
            {
                if (key != null)
                {
                    productName = key.GetValue("ProductName") as string; //windows product name
                    currentVersion = key.GetValue("CurrentVersion") as string; //current version
                    buildLab = key.GetValue("BuildLab") as string; //build number

                    return currentVersion;
                }
                else
                {
                    return "No.";
                }
            }
        }

        static string GetWindowsProductName()
        {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion"))
            {
                if (key != null)
                {
                    productName = key.GetValue("ProductName") as string;
                    currentVersion = key.GetValue("CurrentVersion") as string;
                    buildLab = key.GetValue("BuildLab") as string;

                    return productName;
                }
                else
                {
                    return "No.";
                }
            }
        }

        static string GetBuildLab()
        {
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion"))
            {
                if (key != null)
                {
                    productName = key.GetValue("ProductName") as string;
                    currentVersion = key.GetValue("CurrentVersion") as string;
                    buildLab = key.GetValue("BuildLab") as string;

                    return buildLab;
                }
                else
                {
                    return "No.";
                }
            }
        }
/*
        private void WindowsCheck()
        {
            //check windows version
            string version = GetWindowsVersion();
            if (version.Equals("5.0") || version.Equals("5.1") || version.Equals("5.2") || version.Equals("6.0") || version.Equals("6.1"))
            {
                comboBox1.Enabled = false;
                label35.Enabled = false;
                label9.Enabled = false;
                checkBox2.Checked = true;
                //webBrowser1.Visible = false;
                Windows9x = true;
                checkBox2.Enabled = false;
                label8.Text = "The Aurora pane is unsupported on this version of Windows.";
            }
            else if (version == "6.1" || version == "6.2" || version == "6.3")
            {
                checkBox1.Enabled = true;
                if (checkBox1.Checked)
                    webBrowser1.Visible = true;
                else
                    webBrowser1.Visible = false;
            }
            else
            {
                checkBox1.Enabled = false;
                checkBox1.Checked = false;
                webBrowser1.Visible = false;
                label8.Text = "The Aurora pane is unsupported on this version of Windows.";
                Windows9x = true;
            }

            //transitions problem
            if (version.Equals("5.0") && !version.Equals("5.1") && !version.Equals("5.2") && !version.Equals("6.0") && !version.Equals("6.1") && !version.Equals("6.2") && !version.Equals("6.3"))
            {
                resize9x = true;
                checkBox2.Enabled = false;
                MessageBox.Show(resize9x + "2000");
            }
            else if (version.Equals("5.1") || version.Equals("5.2") || version.Equals("6.0") || version.Equals("6.1") || version.Equals("6.2") || version.Equals("6.3"))
            {
                resize9x = false;
                MessageBox.Show(resize9x + "XP");
            }
            else
            {
                resize9x = true;
                checkBox2.Enabled = false;
            }

          
           
        }
 * */

        public Settings()
        {
            InitializeComponent();
            reloadAurora(webBrowser); //reload the web browser navigation each time the form starts
            Windows9xMode();
            tabChange();
            
           
        }

        public void Windows9xMode()
        {
            string version = GetWindowsVersion();

            if (version == "4.0" || version == "5.0" || version == "5.1" || version == "5.2" || version == "6.0" || version == "6.1")
            {
                checkBox2.Checked = true;
                checkBox2.Visible = false;
                comboBox1.Visible = false;
                label35.Visible = false;
                label9.Visible = false;

            }
            else if (version == "6.2" || version == "6.3")
            {
                webBrowser1.Visible = true;
                pictureBox2.Visible = false;
            }
            else
            {
                checkBox1.Checked = false;
                checkBox1.Visible = false;
                checkBox2.Visible = false;
                checkBox2.Checked = false;
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
                webBrowser1.Visible = false;
                comboBox1.Visible = false;
                label35.Visible = false;
                label9.Visible = false;
                resize9x = true; Windows9x = true;
                MessageBox.Show("9x mode");

            }

            if (version == "5.0" || version == "")
            {
                resize9x = true; Windows9x = true; tabControl1.TabPages.Remove(tabPage12);
            }
        }
        
       

        private void checkWeb()
        {
            switch (Properties.Settings.Default.searchWebPathName)
            {
                case "Google":
                    comboBox17.SelectedIndex = 0;
                    break;

                case "Bing":
                    comboBox17.SelectedIndex = 1;
                    break;

                case "DuckDuckGo!":
                    comboBox17.SelectedIndex = 2;
                    break;

                case "Yahoo":
                    comboBox17.SelectedIndex = 3;
                    break;

                case "StackOverflow":
                    comboBox17.SelectedIndex = 4;
                    break;

                case "Ecosia":
                    comboBox17.SelectedIndex = 5;
                    break;

                case "Internet Archive":
                    comboBox17.SelectedIndex = 6;
                    break;

                case "SWI-Prolog":
                    comboBox17.SelectedIndex = 7;
                    break;

                case "Common LISP Wiki":
                    comboBox17.SelectedIndex = 8;
                    break;
            }

            textBox7.Text = Properties.Settings.Default.searchWebPath;

            if (!Properties.Settings.Default.searchWebAuto)
            {
                label79.Enabled = true;
                label78.Enabled = true;
                comboBox17.Enabled = true;
            }
        }
        public void check()
        {

            checkAnimationsBox(); checkAnimationTimeout(); checkWeb();
            // debug MessageBox.Show(Properties.Settings.Default.auroraFile.ToString());
            if (!Properties.Settings.Default.auroraCustom) //if there isn't a custom aurora activated then load the built in auroras
            {
                switch (Properties.Settings.Default.auroraFile) //switch for aurora colors
                {
                    case "aurora.svg": //blue and purple aurora
                        comboBox1.Text = "Blue and purple";
                        break;
                    case "aurora_black.svg": //black aurora
                        comboBox1.Text = "Black";
                        break;
                    case "aurora_blue.svg": //light blue aurora
                        comboBox1.Text = "Light blue";
                        break;
                    case "aurora_brown.svg": //yellow aurora
                        comboBox1.Text = "Yellow";
                        break;
                    case "aurora_green.svg": //green aurora
                        comboBox1.Text = "Green";
                        break;
                    case "aurora_grey.svg": //grey aurora
                        comboBox1.Text = "Grey";
                        break;
                    case "aurora_lime.svg": //lime aurora
                        comboBox1.Text = "Lime";
                        break;
                    case "aurora_orange.svg": //orange aurora
                        comboBox1.Text = "Orange";
                        break;
                    case "aurora_pink.svg": //pink aurora
                        comboBox1.Text = "Pink";
                        break;
                    case "aurora_red.svg": //red aurora
                        comboBox1.Text = "Red";
                        break;
                    case "aurora_solar.svg": //solarized aurora
                        comboBox1.Text = "Solarized";
                        break;

                }

                
            }

           
            if (Properties.Settings.Default.auroraStartPage && !Windows9x){ //verify if the aurora is activated or not
                checkBox1.Checked = true;
                label8.Text = "Personalise the appearance of the Aurora effect in the start page";
                webBrowser1.Visible = true;
                }else{
                checkBox1.Checked = false;
                label8.Text = "The Aurora effect is disabled. Any change will be applied next time you enable it.";
                webBrowser1.Visible = false;
            }

            if (resize9x)
                checkBox2.Enabled = false;

            //if (webBrowser.Visible)
            //    checkBox1.Checked = true;
            //else
             //   checkBox1.Checked = false;

            if (Properties.Settings.Default.auroraCustom) //verify if there is a custom aurora or not
                checkBox2.Checked = true;
            else
                checkBox2.Checked = false;

            if (!Windows9x)
            {
                auroraCustomCheck(); //enable and disable UI elements given the fact it is enabled a custom aurora or not

                textBox1.Text = Properties.Settings.Default.auroraCustomURL; //load the custom aurora url

            }
            darkModeColors dark = new darkModeColors(); //set up dark mode class
            if (Properties.Settings.Default.appTheme == "dark") //if the theme selected is dark
                radioButton20.Checked = true;
            else if (Properties.Settings.Default.appTheme == "system") //if the theme selected is msstyle
                radioButton22.Checked = true;
            // else if (Properties.Settings.Default.appTheme == "light") //if the theme selected is light
            //radioButton21.Checked = true;

            if (Properties.Settings.Default.startPageVisible) //verify if the start page is enabled
                checkBox3.Checked = true;
            else
                checkBox3.Checked = false;

            if (Properties.Settings.Default.startPageStartup) //verify if the start page is enabled at startup or not
            {
                checkBox4.Checked = true;
                checkBox6.Checked = true;
                checkBox21.Enabled = false;
            }
            else
            {
                checkBox4.Checked = false;
                checkBox6.Checked = false;
                checkBox21.Enabled = true;
            }

            if (Properties.Settings.Default.textPropertiesVisible)
                checkBox21.Checked = true;
            else
                checkBox21.Checked = false;

                if (checkBox4.Checked) //check if the two checkboxes have the same value
                    checkBox6.Checked = true;
                else
                    checkBox6.Checked = false;


            if (Properties.Settings.Default.untitledFileStartup) //Verify if creating a new untitled file at startup is enabled or not.
                checkBox5.Checked = true;
            else
                checkBox5.Checked = false;

            Windows9xMode();

            //check custom icons
            if (Properties.Settings.Default.customIcons)
            {
                checkBox11.Checked = true;
                label63.Text = "Logix will set as its icon pack the one you specify, ignoring your Windows version.";
                comboBox16.Enabled = true;
                pictureBox3.Enabled = true;
                label61.Enabled = true;
                label62.Enabled = true;
                label63.Enabled = true;
                groupBox17.Enabled = true;
                if (Properties.Settings.Default.fluentStyle)
                {
                    comboBox16.SelectedIndex = 0;
                }
                else if (Properties.Settings.Default.lunaStyle)
                {
                    comboBox16.SelectedIndex = 1;
                    pictureBox3.Image = Properties.Resources.AeroIcons;
                }
                else if (Properties.Settings.Default.classicStyle)
                {
                    comboBox16.SelectedIndex = 2;
                    pictureBox3.Image = Properties.Resources.classicIcons;
                }
                else if (Properties.Settings.Default.ClassicNineStyle)
                {
                    comboBox16.SelectedIndex = 3;
                    pictureBox3.Image = Properties.Resources.OS9;
                }
            } 
            else
            {
                checkBox11.Checked = false;
                label63.Text = "Logix will automatically change its icon pack based on the Windows version you're currently running.";
                comboBox16.Enabled = false;
                pictureBox3.Enabled = false;
                label61.Enabled = false;
                label62.Enabled = false;
                groupBox17.Enabled = false;
            }

            //check save file
            if (Properties.Settings.Default.syntaxFileExtension)
                checkBox12.Checked = true;
            else
                checkBox12.Checked = false;

            if (Properties.Settings.Default.syntaxFileExtensionOpen)
                checkBox13.Checked = true;
            else
                checkBox13.Checked = false;

            //open/save
            comboBox14.SelectedIndex = Properties.Settings.Default.syntaxFileExtensionIndexOpen - 1;
            comboBox15.SelectedIndex = Properties.Settings.Default.syntaxFileExtensionIndex - 1;

            //web search
            if (Properties.Settings.Default.searchWeb)
                checkBox14.Checked = true;
            else
                checkBox14.Checked = false;

            if (Properties.Settings.Default.searchWebCustom)
                checkBox16.Checked = true;
            else
                checkBox16.Checked = false;


        //check web search
            if (Properties.Settings.Default.searchWeb)
            {
                checkBox14.Checked = true;
            }
            else
            {
                checkBox14.Checked = false;
                checkBox15.Enabled = false;
                checkBox16.Enabled = false;
                label80.Enabled = false;
                label79.Enabled = false;
                label78.Enabled = false;
                comboBox17.Enabled = false;

                label77.Enabled = false;
                label75.Enabled = false;
                label76.Enabled = false;
                textBox7.Enabled = false;
                button29.Enabled = false;
            }

            if (Properties.Settings.Default.searchWebAuto)
            {
                checkBox15.Checked = true;
                checkBox16.Enabled = false;
            }
            else
            {
                checkBox15.Checked = false;
                checkBox16.Enabled = true;
            }
            if (Properties.Settings.Default.searchWebCustom)
            {
                checkBox16.Checked = true;
                checkBox15.Enabled = false;
            }
            else
            {
                checkBox16.Checked = false;
                checkBox15.Enabled = true;
            }

            if (!checkBox14.Checked)
            {
                checkBox15.Enabled = false;
                checkBox16.Enabled = false;
            }
            else
            {
                checkBox15.Enabled = true;
                checkBox16.Enabled = true;
            }
            
            //select syntax open file
            if (Properties.Settings.Default.syntaxOpenFileAuto)
                checkBox17.Checked = true;
            else
                checkBox17.Checked = false;

            //check default syntax mode
            switch (Properties.Settings.Default.syntaxOpenDefaultMode)
            {
                case "Previous one":
                    comboBox18.SelectedIndex = 5;
                    break;

                case "None":
                    comboBox18.SelectedIndex = 0;
                    break;

                case "Prolog":
                    comboBox18.SelectedIndex = 1;
                    break;

                case "Lisp":
                    comboBox18.SelectedIndex = 2;
                    break;

                case "yacc":
                    comboBox18.SelectedIndex = 3;
                    break;

                case "jflex":
                    comboBox18.SelectedIndex = 4;
                    break;
            }

            //window states
            if (Properties.Settings.Default.autoWindowSize)
            {
                checkBox19.Checked = true;
            }
            else
            {
                checkBox19.Checked = false;
            }
            textBox8.Text = Properties.Settings.Default.windowLeft.ToString();
            textBox9.Text = Properties.Settings.Default.windowTop.ToString();
            textBox10.Text = Properties.Settings.Default.windowHeight.ToString();
            textBox11.Text = Properties.Settings.Default.windowWidth.ToString();

            if (Properties.Settings.Default.autoWindowPosition)
            {
                checkBox18.Checked = true;
            }
            else
            {
                checkBox18.Checked = false;
            }

            if (Properties.Settings.Default.autoWindowState)
            {
                checkBox20.Checked = true;
            }
            else
            {
                checkBox20.Checked = false;
            }

            switch (Properties.Settings.Default.windowState)
            {
                case "Normal":
                    comboBox20.SelectedIndex = 0;
                    break;

                case "Maximized":
                    comboBox20.SelectedIndex = 1;
                    break;
            }

            switch (Properties.Settings.Default.startWindowPlace)
            {
                case "CenterScreen":
                    comboBox19.SelectedIndex = 0;
                    break;

                case "WindowsDefaultLocation":
                    comboBox19.SelectedIndex = 1;
                    break;

                case "WindowsDefaultBounds":
                    comboBox19.SelectedIndex = 2;
                    break;

                case "Manual":
                    comboBox19.SelectedIndex = 3;
                    break;
            }

            //flex
            if (Properties.Settings.Default.jFlexIntegration)
                checkBox22.Checked = true;
            else
                checkBox22.Checked = false;

            textBox12.Text = Properties.Settings.Default.jFlexPath;
            if (Properties.Settings.Default.jFlexGUI)
                checkBox23.Checked = true;
            else
                checkBox23.Checked = false;

            //flex
            if (Properties.Settings.Default.bYaccIntegration)
                checkBox25.Checked = true;
            else
                checkBox25.Checked = false;

            textBox13.Text = Properties.Settings.Default.bYaccPath;
            if (Properties.Settings.Default.bYaccGUI)
                checkBox24.Checked = true;
            else
                checkBox24.Checked = false;

        }

        private void checkColumnLimit(){
        //check column limit
            textBox2.Text = Properties.Settings.Default.columnLineLimit.ToString();
            textBox3.Text = Properties.Settings.Default.antoniottiStandardText;
            textBox4.Text = Properties.Settings.Default.antoniottiCrazyText;
            textBox5.Text = Properties.Settings.Default.antoniottiCrazyTitle;
            textBox6.Text = Properties.Settings.Default.antoniottiCrazyTextDuo;


        }
        private void Settings_Load(object sender, EventArgs e)
        {
            auroraHeight = 285; //set the default height for settings form in Aurora tab
            //load start page preview text
            label28.Text = Properties.Resources.AppName + " " + Properties.Resources.AppEdition + " " + Properties.Resources.AppVersion;
            label16.Text = Properties.Resources.AppName; label15.Text = Properties.Resources.AppEdition + " Edition"; label14.Text = Properties.Resources.AppDescription;
            //label20.Text = Properties.Settings.Default.startPageStartup.ToString(); //DEBUG - load the property value at runtime
            if (startPage.Items.Contains(startPageTab) && checkBox3.Checked == true) startPage.RemoveTab(startPageTab);

            loadHighlightPrologColors(); //load syntax colors
            loadFontStyleProlog(); //load font styles syntax

            check(); tabChange();
            checkColumnLimit();

            Windows9xMode();

            
            
        }

        private void reloadAurora(WebBrowser wb)
        {
            string curDir = Directory.GetCurrentDirectory(); //current app directory
            if (!Properties.Settings.Default.auroraCustom) //if custom aurora is disabled
            {
                this.webBrowser1.Url = new Uri(String.Format("file:///{0}/aurora/" + Properties.Settings.Default.auroraFile, curDir)); //show the aurora preview in settings
                wb.Url = new Uri(String.Format("file:///{0}/aurora/" + Properties.Settings.Default.auroraFile, curDir)); //show the aurora in mdiparent
            }
            else
            {
                webBrowser1.Navigate(textBox1.Text); //show the webpage in settings
                wb.Navigate(textBox1.Text); //show the webpage in mdiparent
            }

            Windows9xMode();
        }



        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
          
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) //aurora is enabled: show the aurora
            {
                label8.Text = "Personalise the appearance of the Aurora effect in the start page.";
                webBrowser.Show();
                Properties.Settings.Default.auroraStartPage = true;
                Properties.Settings.Default.Save();
                split1.Panel2Collapsed = false;
            }
            else
            {
                label8.Text = "The Aurora effect is disabled. Any change will be applied next time you enable it."; //aurora is disabled: tell the user about it
                webBrowser.Hide();
                Properties.Settings.Default.auroraStartPage = false;
                Properties.Settings.Default.Save();
                split1.Panel2Collapsed = true;
            }


            Windows9xMode();
        }


        private void radioButton22_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.system; //load system theme image in preview
            Properties.Settings.Default.appTheme = "system"; //load msstyle theme
            Properties.Settings.Default.Save();
            systemTheme(); //apply msstyle theme
        }

        private void radioButton21_CheckedChanged(object sender, EventArgs e)
        {
                pictureBox1.Image = Properties.Resources.light; //load light theme image in preview
            Properties.Settings.Default.appTheme = "light"; //load light theme
            Properties.Settings.Default.Save();
            //lightMode();
        }

        private void radioButton20_CheckedChanged(object sender, EventArgs e)
        {
                pictureBox1.Image = Properties.Resources.dark; //load dark theme image in preview
            Properties.Settings.Default.appTheme = "dark"; //load dark theme
            Properties.Settings.Default.Save();
            darkMode(); //apply dark mode
        }

        

        public void darkMode()
        {
            darkModeColors dark = new darkModeColors(); //load dark mode class
            Settings.ActiveForm.BackColor = dark.WindowColor; //apply back color
            

            //tabs
            tabPage1.BackColor = dark.WindowColor;
            tabPage2.BackColor = dark.WindowColor;
            tabPage3.BackColor = dark.WindowColor;
            tabPage4.BackColor = dark.WindowColor;
            tabPage5.BackColor = dark.WindowColor;
            tabPage6.BackColor = dark.WindowColor;
            tabPage7.BackColor = dark.WindowColor;
            tabPage8.BackColor = dark.WindowColor;
            tabPage9.BackColor = dark.WindowColor;

            //theming page
            label1.ForeColor = dark.LabelFG;
            label3.ForeColor = dark.LabelFG;
            label4.ForeColor = dark.LabelFG;
            label6.ForeColor = dark.LabelFG;
            label8.ForeColor = dark.LabelFG; //aurora description

            comboBox1.BackColor = dark.TextBoxBG; //aurora choose
            comboBox1.ForeColor = dark.TextBoxFG; //aurora choose
            radioButton20.ForeColor = dark.LabelFG; //system
            radioButton21.ForeColor = dark.LabelFG; //light
            radioButton22.ForeColor = dark.LabelFG; //dark

}

        

        public void systemTheme()
        {
            Settings.ActiveForm.BackColor = SystemColors.Window;
            //see dark theme for reference
            //tabs
            tabPage1.BackColor = Color.Transparent;
            tabPage2.BackColor = Color.Transparent;
            tabPage3.BackColor = Color.Transparent;
            tabPage4.BackColor = Color.Transparent;
            tabPage5.BackColor = Color.Transparent;
            tabPage6.BackColor = Color.Transparent;
            tabPage7.BackColor = Color.Transparent;
            tabPage8.BackColor = Color.Transparent;
            tabPage9.BackColor = Color.Transparent;

            label1.ForeColor = SystemColors.ControlText;
            label3.ForeColor = SystemColors.ControlText;
            label4.ForeColor = SystemColors.ControlText;
            label6.ForeColor = SystemColors.ControlText;
            label8.ForeColor = SystemColors.ControlText;

            comboBox1.BackColor = SystemColors.Window;
            comboBox1.ForeColor = SystemColors.WindowText;
            radioButton20.ForeColor = SystemColors.ControlText;
            radioButton21.ForeColor = SystemColors.ControlText;
            radioButton22.ForeColor = SystemColors.ControlText;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            darkMode(); //DEBUG method - force enable dark mode
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //aurora colors
            switch (comboBox1.Text)
            {
                case "Blue and purple": //blue aurora
                    Properties.Settings.Default.auroraFile = "aurora.svg";
                    Properties.Settings.Default.Save();
                    reloadAurora(webBrowser); //reload page
                    break;
                case "Light blue": //light blue aurora
                    Properties.Settings.Default.auroraFile = "aurora_blue.svg";
                    Properties.Settings.Default.Save();
                    reloadAurora(webBrowser);
                    break;
                case "Red": //Red aurora
                    Properties.Settings.Default.auroraFile = "aurora_red.svg";
                    Properties.Settings.Default.Save();
                    reloadAurora(webBrowser);
                    break;
                case "Yellow": //yellow aurora
                    Properties.Settings.Default.auroraFile = "aurora_brown.svg";
                    Properties.Settings.Default.Save();
                    reloadAurora(webBrowser);
                    break;
                case "Green": //green aurora
                    Properties.Settings.Default.auroraFile = "aurora_green.svg";
                    Properties.Settings.Default.Save();
                    reloadAurora(webBrowser);
                    break;
                case "Black": //black aurora
                    Properties.Settings.Default.auroraFile = "aurora_black.svg";
                    Properties.Settings.Default.Save();
                    reloadAurora(webBrowser);
                    break;
                case "Grey": //grey aurora
                    Properties.Settings.Default.auroraFile = "aurora_grey.svg";
                    Properties.Settings.Default.Save();
                    reloadAurora(webBrowser);
                    break;
                case "Orange": //orange aurora
                    Properties.Settings.Default.auroraFile = "aurora_orange.svg";
                    Properties.Settings.Default.Save();
                    reloadAurora(webBrowser);
                    break;
                case "Pink": //pink aurora
                    Properties.Settings.Default.auroraFile = "aurora_pink.svg";
                    Properties.Settings.Default.Save();
                    reloadAurora(webBrowser);
                    break;
                case "Lime": //lime aurora
                    Properties.Settings.Default.auroraFile = "aurora_lime.svg";
                    Properties.Settings.Default.Save();
                    reloadAurora(webBrowser);
                    break;
                case "Solarized": //solarized aurora
                    Properties.Settings.Default.auroraFile = "aurora_solar.svg";
                    Properties.Settings.Default.Save();
                    reloadAurora(webBrowser);
                    break;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            auroraOpen(); //browse for a file to load a custom document 
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            auroraCustomCheck(); //enable custom aurora
            
        }

        public void auroraCustomCheck()
        { 
            if (checkBox2.Checked) //if custom aurora is enabled, enable the controls to load custom URLs
            {
                label11.Enabled = true;
                label12.Enabled = true;
                textBox1.Enabled = true;
                button4.Enabled = true;
                button1.Enabled = true;
                comboBox1.Enabled = false;
                label35.Enabled = false;
                label9.Enabled = false;
                Properties.Settings.Default.auroraCustom = true; //save the property
                Properties.Settings.Default.Save();
                
            }
            else //if not, disable them
            {
                label11.Enabled = false;
                label12.Enabled = false;
                textBox1.Enabled = false;
                button4.Enabled = false;
                button1.Enabled = false;
                comboBox1.Enabled = true;
                label35.Enabled = true;
                label9.Enabled = true;
                Properties.Settings.Default.auroraCustom = false; //save the property
                Properties.Settings.Default.Save();
               
            }

           
        }

        private void auroraOpen()
        {
            //if (isTextModified)
            // {
            //    SaveQuestion();
            // }
            //open prolog files
            OpenFileDialog openFileDialog = new OpenFileDialog(); //create a new open dialog
            openFileDialog.Filter = "SVG images (*.svg)|*.svg|XML documents (*.html)|*.html|JPEG images (*.jpg)|*.jpg|PNG images (*.png)|*.png|All files (*.*)|*.*"; //set up file extensions
            openFileDialog.FilterIndex = 1; //filter the index
            openFileDialog.RestoreDirectory = true; //remember the last directory you've opened
            string filePath; string fileContent; string currentFilePath; //strings that are needed.
            if (openFileDialog.ShowDialog() == DialogResult.OK) //if you select a file..
            {
                //read file
                filePath = openFileDialog.FileName; //save the path
                fileContent = File.ReadAllText(filePath); //Save the content
                currentFilePath = openFileDialog.FileName; //get the current file name
                currentFilePath = Path.GetFileName(currentFilePath);
                try
                {
                    textBox1.Text = filePath; //put the window title
                    webBrowser1.Navigate(filePath); //navigate to the custom document 
                    webBrowser.Navigate(filePath);
                }
                catch (NullReferenceException e)
                {
                    
                }
            }
            
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) //if you press enter when writing in the custom url textbox
            {
                webBrowser.Navigate(textBox1.Text); //navigate to the address
                webBrowser1.Navigate(textBox1.Text);
                Properties.Settings.Default.auroraCustomURL = textBox1.Text; //save it so next time it starts it can be automatically loaded
                Properties.Settings.Default.Save();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            webBrowser.Navigate(textBox1.Text); //refresh
                webBrowser1.Navigate(textBox1.Text); //refresh
            }

        private void button1_Click_2(object sender, EventArgs e)
        {
            webBrowser.Navigate(textBox1.Text); //refresh
            webBrowser1.Navigate(textBox1.Text); //refresh
        
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.auroraCustomURL = textBox1.Text; //save the custom url
            Properties.Settings.Default.auroraCustom = checkBox2.Checked; //save if you've set up a custom aurora

            Properties.Settings.Default.animationEnable = checkBox10.Checked;
            Properties.Settings.Default.animationResizeEnable = checkBox7.Checked;
            Properties.Settings.Default.animationDockBottomEnable = checkBox8.Checked;
            Properties.Settings.Default.animationSettingResizeEnable = checkBox9.Checked;

            applyAnimationTimeout();
            Properties.Settings.Default.Save(); //fallback method - always save
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            tabChange();
            


        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(button6, -65, -95); //add elements to the start page menu
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked){ //if start page is enabled
              //if (!startPage.Items.Contains(startPageTab)) startPage.AddTab(startPageTab); //add it as a tab in mdiparent
              //if (startPage.Items.Contains(startPageTab) && checkBox3.Checked == true) startPage.RemoveTab(startPageTab);
                context.Visible = true;
              
                Properties.Settings.Default.startPageVisible = true; //set the property
                Properties.Settings.Default.Save();
            }
            else
            {
                startPage.RemoveTab(startPageTab); //else remove the page in mdiparent
                context.Visible = false;
                Properties.Settings.Default.startPageVisible = false; //set the property
            Properties.Settings.Default.Save();
            }
           
                
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            //label20.Text = Properties.Settings.Default.startPageStartup.ToString(); //DEBUG - verify the value at runtime
            if (checkBox4.Checked) //if open at startup is true
            {
                Properties.Settings.Default.startPageStartup = true; //set the property
                Properties.Settings.Default.Save();
                checkBox6.Checked = true;
            }
            else
            {
                Properties.Settings.Default.startPageStartup = false; //set the property
                Properties.Settings.Default.Save();
                checkBox6.Checked = false;
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked && checkBox6.Checked == false)
            {//save if you want an untitled file on startup or not
                Properties.Settings.Default.untitledFileStartup = true;
                checkBox21.Enabled = true;
            }
            else
            {
                Properties.Settings.Default.untitledFileStartup = false;
                checkBox21.Enabled = false;
                checkBox21.Checked = false;
            } Properties.Settings.Default.Save();
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            //label20.Text = Properties.Settings.Default.startPageStartup.ToString(); //DEBUG - verify the value at runtime
            if (checkBox6.Checked) //if open at startup is true
            {
                Properties.Settings.Default.startPageStartup = true; //set the property
                Properties.Settings.Default.Save();
                checkBox4.Checked = true;
                checkBox21.Enabled = false;
                checkBox21.Checked = false;
            }
            else
            {
                Properties.Settings.Default.startPageStartup = false; //set the property
                Properties.Settings.Default.Save();
                checkBox4.Checked = false;
               if (checkBox5.Checked) checkBox21.Enabled = true;
            }
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabChange();
            
        }

        private void tabChange()
        {
            int iformwidth; int iformheight;
            //check();

            if (tabControl1.SelectedIndex == 4 && tabControl2.SelectedIndex == 0)
            {
                iformheight = 506; //change the height to be the one for the start page appearance
                iformwidth = 606;

            }
            else if (tabControl2.SelectedIndex == 1 && tabControl3.SelectedIndex == 1)
            {
                iformwidth = 535; //change the height to be the one for highlight
                iformheight = 479;
            }
            else if (tabControl2.SelectedIndex == 0 && tabControl1.SelectedIndex == 1)
            {
                iformheight = 433; //theming
                iformwidth = 600;
            }
            else if (tabControl2.SelectedIndex == 1 && tabControl3.SelectedIndex == 4)
            {
                iformwidth = 449; //column limit
                iformheight = 380;
            }
            else if (tabControl2.SelectedIndex == 1 && tabControl3.SelectedIndex == 3)
            {
                iformwidth = 413; //open/save
                iformheight = 471;
            }
            else if (tabControl2.SelectedIndex == 1 && tabControl3.SelectedIndex == 5)
            {
                iformwidth = 552; //web search
                iformheight = 307;
            }
            else if (tabControl2.SelectedIndex == 1 && tabControl3.SelectedIndex == 0)
            {
                iformheight = 681; //startup
                iformwidth = 434;
            }
            else if (tabControl2.SelectedIndex == 2 && tabControl3.SelectedIndex == 0)
            {
                iformheight = 256;  //jflex
                iformwidth = 516;
            }
            else if (tabControl2.SelectedIndex == 2 && tabControl3.SelectedIndex == 1)
            {
                iformheight = 256;  //byacc
                iformwidth = 516;
            } else
            {
                iformheight = 285; //change the height to be the one for the aurora appearance
                iformwidth = 606;
            }

            // We animate it with an ease-in-ease-out transition...
            if (Properties.Settings.Default.animationSettingResizeEnable && !resize9x)
            {
                Transition.run(this, "Width", iformwidth, new TransitionType_EaseInEaseOut(Properties.Settings.Default.animationSettingResize));
                Transition.run(this, "Height", iformheight, new TransitionType_EaseInEaseOut(Properties.Settings.Default.animationSettingResize));
            }
            else
            {
                this.Size = new Size(iformwidth, iformheight);
            }

        }
        private Color colorChoose()
        {
            Color c;
            ColorDialog clrDialog = new ColorDialog();

            //show the colour dialog and check that user clicked ok
            if (clrDialog.ShowDialog() == DialogResult.OK)
            {
                c = clrDialog.Color;
                return c;
            }
            else return Color.Purple;
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Color c = colorChoose(button8.BackColor);
            Properties.Settings.Default.anonForeColor = c;
            Properties.Settings.Default.Save();
        }

        private Color colorChoose(Color startColor)
        {
            ColorDialog color = new ColorDialog();
            Color returnColor = color.Color;
            color.Color = startColor;

            if (color.ShowDialog() == DialogResult.OK)
            {
                returnColor = color.Color;
                return returnColor;
            } else
                return returnColor;
        }
        private void loadHighlightPrologColors()
        {
            //point: 18 and 17
            button18.BackColor = Properties.Settings.Default.pointForeColor;
            button17.BackColor = Properties.Settings.Default.pointBackColor;

            //comma: 12 and 11
            button12.BackColor = Properties.Settings.Default.commaForeColor;
            button11.BackColor = Properties.Settings.Default.commaBackColor;
            //dash: 22 and 21
            button22.BackColor = Properties.Settings.Default.keywordForeColor;
            button21.BackColor = Properties.Settings.Default.keywordBackColor;

            //underscore: 8 and 7
            button8.BackColor = Properties.Settings.Default.anonForeColor;
            button7.BackColor = Properties.Settings.Default.anonBackColor;

            //equals: 24 and 23
            button24.BackColor = Properties.Settings.Default.equalsForeColor;
            button23.BackColor = Properties.Settings.Default.equalsBackColor;

            //question mark: 16 and 15
            button16.BackColor = Properties.Settings.Default.questionForeColor;
            button15.BackColor = Properties.Settings.Default.questionBackColor;

            //comments: 10 and 9
            button10.BackColor = Properties.Settings.Default.commentForeColor;
            button9.BackColor = Properties.Settings.Default.commentBackColor;

            //system: 20 and 19
            button20.BackColor = Properties.Settings.Default.systemParForeColor;
            button19.BackColor = Properties.Settings.Default.systemParBackColor;

            //brackets: 14 and 13
            button14.BackColor = Properties.Settings.Default.parentesiForeColor;
            button13.BackColor = Properties.Settings.Default.parentesiBackColor;

            transparentCheckProlog(); //check for transparent color
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Color c = colorChoose(button18.BackColor);
            Properties.Settings.Default.pointForeColor = c;
            loadHighlightPrologColors();
            Properties.Settings.Default.Save();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Color c = colorChoose(button17.BackColor);
            Properties.Settings.Default.pointBackColor = c;
            loadHighlightPrologColors();
            Properties.Settings.Default.Save();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Color c = colorChoose(button12.BackColor);
            Properties.Settings.Default.commaForeColor = c;
            loadHighlightPrologColors();
            Properties.Settings.Default.Save();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Color c = colorChoose(button11.BackColor);
            Properties.Settings.Default.commaBackColor = c;
            loadHighlightPrologColors();
            Properties.Settings.Default.Save();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            Color c = colorChoose(button21.BackColor);
            Properties.Settings.Default.keywordBackColor = c;
            loadHighlightPrologColors();
            Properties.Settings.Default.Save();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            Color c = colorChoose(button7.BackColor);
            Properties.Settings.Default.anonBackColor = c;
            loadHighlightPrologColors();
            Properties.Settings.Default.Save();
        }

        private void button23_Click(object sender, EventArgs e)
        {

        }

        private void button23_Click_1(object sender, EventArgs e)
        {
            Color c = colorChoose(button23.BackColor);
            Properties.Settings.Default.equalsBackColor = c;
            loadHighlightPrologColors();
            Properties.Settings.Default.Save();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Color c = colorChoose(button15.BackColor);
            Properties.Settings.Default.questionBackColor = c;
            loadHighlightPrologColors();
            Properties.Settings.Default.Save();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Color c = colorChoose(button9.BackColor);
            Properties.Settings.Default.commentBackColor = c;
            loadHighlightPrologColors();
            Properties.Settings.Default.Save();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            Color c = colorChoose(button19.BackColor);
            Properties.Settings.Default.systemParBackColor = c;
            loadHighlightPrologColors();
            Properties.Settings.Default.Save();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Color c = colorChoose(button13.BackColor);
            Properties.Settings.Default.parentesiBackColor = c;
            loadHighlightPrologColors();
            Properties.Settings.Default.Save();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            Color c = colorChoose(button22.BackColor);
            Properties.Settings.Default.keywordForeColor = c;
            loadHighlightPrologColors();
            Properties.Settings.Default.Save();
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            Color c = colorChoose(button8.BackColor);
            Properties.Settings.Default.anonForeColor = c;
            loadHighlightPrologColors();
            Properties.Settings.Default.Save();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            Color c = colorChoose(button24.BackColor);
            Properties.Settings.Default.equalsForeColor = c;
            loadHighlightPrologColors();
            Properties.Settings.Default.Save();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Color c = colorChoose(button16.BackColor);
            Properties.Settings.Default.questionForeColor = c;
            loadHighlightPrologColors();
            Properties.Settings.Default.Save();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Color c = colorChoose(button10.BackColor);
            Properties.Settings.Default.commentForeColor = c;
            loadHighlightPrologColors();
            Properties.Settings.Default.Save();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Color c = colorChoose(button20.BackColor);
            Properties.Settings.Default.systemParForeColor = c;
            loadHighlightPrologColors();
            Properties.Settings.Default.Save();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Color c = colorChoose(button14.BackColor);
            Properties.Settings.Default.parentesiForeColor = c;
            loadHighlightPrologColors();
            Properties.Settings.Default.Save();
        }

        private void SetCheckedStatus(ToolStripMenuItem item, Color color)
        {
            if (color == Color.Transparent)
                item.Checked = true;
            else
                item.Checked = false;
        }

        private void transparentCheckProlog()
        {
            SetCheckedStatus(transparentToolStripMenuItem100, Properties.Settings.Default.pointForeColor);
            SetCheckedStatus(transparentToolStripMenuItem1, Properties.Settings.Default.pointBackColor);

            SetCheckedStatus(transparentToolStripMenuItem2, Properties.Settings.Default.commaForeColor);
            SetCheckedStatus(transparentToolStripMenuItem3, Properties.Settings.Default.commaBackColor);

            SetCheckedStatus(transparentToolStripMenuItem4, Properties.Settings.Default.keywordForeColor);
            SetCheckedStatus(transparentToolStripMenuItem5, Properties.Settings.Default.keywordBackColor);

            SetCheckedStatus(transparentToolStripMenuItem6, Properties.Settings.Default.anonForeColor);
            SetCheckedStatus(transparentToolStripMenuItem7, Properties.Settings.Default.anonBackColor);

            SetCheckedStatus(transparentToolStripMenuItem8, Properties.Settings.Default.equalsForeColor);
            SetCheckedStatus(transparentToolStripMenuItem10, Properties.Settings.Default.equalsBackColor);

            SetCheckedStatus(transparentToolStripMenuItem9, Properties.Settings.Default.questionForeColor);
            SetCheckedStatus(transparentToolStripMenuItem11, Properties.Settings.Default.questionBackColor);

            SetCheckedStatus(transparentToolStripMenuItem12, Properties.Settings.Default.commentForeColor);
            SetCheckedStatus(transparentToolStripMenuItem13, Properties.Settings.Default.commentBackColor);

            SetCheckedStatus(transparentToolStripMenuItem14, Properties.Settings.Default.systemParForeColor);
            SetCheckedStatus(transparentToolStripMenuItem15, Properties.Settings.Default.systemParBackColor);

            SetCheckedStatus(transparentToolStripMenuItem16, Properties.Settings.Default.systemParForeColor);
            SetCheckedStatus(transparentToolStripMenuItem17, Properties.Settings.Default.systemParBackColor);
            
        }

        private void transparentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (transparentToolStripMenuItem.Checked)
            {
                Properties.Settings.Default.pointForeColor = Color.Black;
                Properties.Settings.Default.Save();
                transparentToolStripMenuItem.Checked = false;
                loadHighlightPrologColors();
            }
            else
            {
                Properties.Settings.Default.pointForeColor = Color.Transparent;
                Properties.Settings.Default.Save();
                transparentToolStripMenuItem.Checked = true;
                loadHighlightPrologColors();
            }
        }

        private void transparentToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (transparentToolStripMenuItem1.Checked)
            {
                Properties.Settings.Default.pointBackColor = Color.Black;
                Properties.Settings.Default.Save();
                transparentToolStripMenuItem1.Checked = false;
                loadHighlightPrologColors();
            }
            else
            {
                Properties.Settings.Default.pointBackColor = Color.Transparent;
                Properties.Settings.Default.Save();
                transparentToolStripMenuItem1.Checked = true;
                loadHighlightPrologColors();
            }
        }

        private void transparentToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (transparentToolStripMenuItem2.Checked)
            {
                Properties.Settings.Default.commaForeColor = Color.Black;
                Properties.Settings.Default.Save();
                transparentToolStripMenuItem2.Checked = false;
                loadHighlightPrologColors();
            }
            else
            {
                Properties.Settings.Default.commaForeColor = Color.Transparent;
                Properties.Settings.Default.Save();
                transparentToolStripMenuItem2.Checked = true;
                loadHighlightPrologColors();
            }
        }

        private void transparentToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (transparentToolStripMenuItem4.Checked)
            {
                Properties.Settings.Default.keywordForeColor = Color.Black;
                Properties.Settings.Default.Save();
                transparentToolStripMenuItem4.Checked = false;
                loadHighlightPrologColors();
            }
            else
            {
                Properties.Settings.Default.keywordForeColor = Color.Transparent;
                Properties.Settings.Default.Save();
                transparentToolStripMenuItem4.Checked = true;
                loadHighlightPrologColors();
            }
        }

        private void transparentToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            if (transparentToolStripMenuItem6.Checked)
            {
                Properties.Settings.Default.anonForeColor = Color.Black;
                Properties.Settings.Default.Save();
                transparentToolStripMenuItem6.Checked = false;
                loadHighlightPrologColors();
            }
            else
            {
                Properties.Settings.Default.anonForeColor = Color.Transparent;
                Properties.Settings.Default.Save();
                transparentToolStripMenuItem6.Checked = true;
                loadHighlightPrologColors();
            }
        }

        private void transparentToolStripMenuItem8_Click(object sender, EventArgs e)
        {
            if (transparentToolStripMenuItem8.Checked)
            {
                Properties.Settings.Default.equalsForeColor = Color.Black;
                Properties.Settings.Default.Save();
                transparentToolStripMenuItem8.Checked = false;
                loadHighlightPrologColors();
            }
            else
            {
                Properties.Settings.Default.equalsForeColor = Color.Transparent;
                Properties.Settings.Default.Save();
                transparentToolStripMenuItem8.Checked = true;
                loadHighlightPrologColors();
            }
        }

        private void transparentToolStripMenuItem9_Click(object sender, EventArgs e)
        {
            if (transparentToolStripMenuItem9.Checked)
            {
                Properties.Settings.Default.questionForeColor = Color.Black;
                Properties.Settings.Default.Save();
                transparentToolStripMenuItem9.Checked = false;
                loadHighlightPrologColors();
            }
            else
            {
                Properties.Settings.Default.questionForeColor = Color.Transparent;
                Properties.Settings.Default.Save();
                transparentToolStripMenuItem9.Checked = true;
                loadHighlightPrologColors();
            }
        }

        private void commentsFore_Opening(object sender, CancelEventArgs e)
        {

        }

        private void transparentToolStripMenuItem12_Click(object sender, EventArgs e)
        {
            if (transparentToolStripMenuItem12.Checked)
            {
                Properties.Settings.Default.commentForeColor = Color.Black;
                Properties.Settings.Default.Save();
                transparentToolStripMenuItem12.Checked = false;
                loadHighlightPrologColors();
            }
            else
            {
                Properties.Settings.Default.commentForeColor = Color.Transparent;
                Properties.Settings.Default.Save();
                transparentToolStripMenuItem12.Checked = true;
                loadHighlightPrologColors();
            }
        }

        private void transparentToolStripMenuItem14_Click(object sender, EventArgs e)
        {
            if (transparentToolStripMenuItem14.Checked)
            {
                Properties.Settings.Default.systemParForeColor = Color.Black;
                Properties.Settings.Default.Save();
                transparentToolStripMenuItem14.Checked = false;
                loadHighlightPrologColors();
            }
            else
            {
                Properties.Settings.Default.systemParForeColor = Color.Transparent;
                Properties.Settings.Default.Save();
                transparentToolStripMenuItem14.Checked = true;
                loadHighlightPrologColors();
            }
        }

        private void transparentToolStripMenuItem16_Click(object sender, EventArgs e)
        {
            if (transparentToolStripMenuItem16.Checked)
            {
                Properties.Settings.Default.parentesiForeColor = Color.Black;
                Properties.Settings.Default.Save();
                transparentToolStripMenuItem16.Checked = false;
                loadHighlightPrologColors();
            }
            else
            {
                Properties.Settings.Default.parentesiForeColor = Color.Transparent;
                Properties.Settings.Default.Save();
                transparentToolStripMenuItem16.Checked = true;
                loadHighlightPrologColors();
            }
        }

        private void transparentToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (transparentToolStripMenuItem3.Checked)
            {
                Properties.Settings.Default.commaBackColor = Color.Black;
                Properties.Settings.Default.Save();
                transparentToolStripMenuItem3.Checked = false;
                loadHighlightPrologColors();
            }
            else
            {
                Properties.Settings.Default.commaBackColor = Color.Transparent;
                Properties.Settings.Default.Save();
                transparentToolStripMenuItem3.Checked = true;
                loadHighlightPrologColors();
            }
        }

        private void transparentToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            if (transparentToolStripMenuItem5.Checked)
            {
                Properties.Settings.Default.keywordBackColor = Color.Black;
                Properties.Settings.Default.Save();
                transparentToolStripMenuItem5.Checked = false;
                loadHighlightPrologColors();
            }
            else
            {
                Properties.Settings.Default.keywordBackColor = Color.Transparent;
                Properties.Settings.Default.Save();
                transparentToolStripMenuItem5.Checked = true;
                loadHighlightPrologColors();
            }
        }

        private void transparentToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            if (transparentToolStripMenuItem7.Checked)
            {
                Properties.Settings.Default.anonBackColor = Color.Black;
                Properties.Settings.Default.Save();
                transparentToolStripMenuItem7.Checked = false;
                loadHighlightPrologColors();
            }
            else
            {
                Properties.Settings.Default.anonBackColor = Color.Transparent;
                Properties.Settings.Default.Save();
                transparentToolStripMenuItem7.Checked = true;
                loadHighlightPrologColors();
            }
        }

        private void transparentToolStripMenuItem10_Click(object sender, EventArgs e)
        {
            if (transparentToolStripMenuItem10.Checked)
            {
                Properties.Settings.Default.equalsBackColor = Color.Black;
                Properties.Settings.Default.Save();
                transparentToolStripMenuItem10.Checked = false;
                loadHighlightPrologColors();
            }
            else
            {
                Properties.Settings.Default.equalsBackColor = Color.Transparent;
                Properties.Settings.Default.Save();
                transparentToolStripMenuItem10.Checked = true;
                loadHighlightPrologColors();
            }
        }

        private void transparentToolStripMenuItem11_Click(object sender, EventArgs e)
        {
            if (transparentToolStripMenuItem11.Checked)
            {
                Properties.Settings.Default.questionBackColor = Color.Black;
                Properties.Settings.Default.Save();
                transparentToolStripMenuItem11.Checked = false;
                loadHighlightPrologColors();
            }
            else
            {
                Properties.Settings.Default.questionBackColor = Color.Transparent;
                Properties.Settings.Default.Save();
                transparentToolStripMenuItem11.Checked = true;
                loadHighlightPrologColors();
            }
        }

        private void commentsBack_Opening(object sender, CancelEventArgs e)
        {

        }

        private void transparentToolStripMenuItem13_Click(object sender, EventArgs e)
        {
            if (transparentToolStripMenuItem13.Checked)
            {
                Properties.Settings.Default.commentBackColor = Color.Black;
                Properties.Settings.Default.Save();
                transparentToolStripMenuItem13.Checked = false;
                loadHighlightPrologColors();
            }
            else
            {
                Properties.Settings.Default.commentBackColor = Color.Transparent;
                Properties.Settings.Default.Save();
                transparentToolStripMenuItem13.Checked = true;
                loadHighlightPrologColors();
            }
        }

        private void transparentToolStripMenuItem15_Click(object sender, EventArgs e)
        {
            if (transparentToolStripMenuItem15.Checked)
            {
                Properties.Settings.Default.systemParBackColor = Color.Black;
                Properties.Settings.Default.Save();
                transparentToolStripMenuItem15.Checked = false;
                loadHighlightPrologColors();
            }
            else
            {
                Properties.Settings.Default.systemParBackColor = Color.Transparent;
                Properties.Settings.Default.Save();
                transparentToolStripMenuItem15.Checked = true;
                loadHighlightPrologColors();
            }
        }

        private void transparentToolStripMenuItem17_Click(object sender, EventArgs e)
        {
            if (transparentToolStripMenuItem17.Checked)
            {
                Properties.Settings.Default.parentesiBackColor = Color.Black;
                Properties.Settings.Default.Save();
                transparentToolStripMenuItem17.Checked = false;
                loadHighlightPrologColors();
            }
            else
            {
                Properties.Settings.Default.parentesiBackColor = Color.Transparent;
                Properties.Settings.Default.Save();
                transparentToolStripMenuItem17.Checked = true;
                loadHighlightPrologColors();
            }
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox7.SelectedText)
            {
                case "Regular":
                    Properties.Settings.Default.pointFontStyle = FontStyle.Regular;
                    break;
                case "Bold":
                    Properties.Settings.Default.pointFontStyle = FontStyle.Bold;
                    break;
                case "Italic":
                    Properties.Settings.Default.pointFontStyle = FontStyle.Italic;
                    break;
                case "Strikethrough":
                    Properties.Settings.Default.pointFontStyle = FontStyle.Strikeout;
                    break;
                case "Underline":
                    Properties.Settings.Default.pointFontStyle = FontStyle.Underline;
                    break;
            }
        }

        private void loadFontStylesCombobox(FontStyle fs, ComboBox cb)
        {
            if (fs == FontStyle.Regular)
                cb.SelectedIndex = 0;
            else if (fs == FontStyle.Bold)
                cb.SelectedIndex = 1;
            else if (fs == FontStyle.Italic)
                cb.SelectedIndex = 2;
            else if (fs == FontStyle.Underline)
                cb.SelectedIndex = 3;
            else if (fs == FontStyle.Strikeout)
                cb.SelectedIndex = 4;
        }

        private void loadFontStyleProlog()
        {
            loadFontStylesCombobox(Properties.Settings.Default.pointFontStyle, comboBox7);
            loadFontStylesCombobox(Properties.Settings.Default.commaFontStyle, comboBox4);
            loadFontStylesCombobox(Properties.Settings.Default.keywordFontStyle, comboBox9);
            loadFontStylesCombobox(Properties.Settings.Default.anonFontStyle, comboBox2);
            loadFontStylesCombobox(Properties.Settings.Default.equalsFontStyle, comboBox10);
            loadFontStylesCombobox(Properties.Settings.Default.questionFontStyle, comboBox6);
            loadFontStylesCombobox(Properties.Settings.Default.commentFontStyle, comboBox3);
            loadFontStylesCombobox(Properties.Settings.Default.systemParFontStyle, comboBox8);
            loadFontStylesCombobox(Properties.Settings.Default.parentesiFontStyle, comboBox5);
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox4.SelectedText)
            {
                case "Regular":
                    Properties.Settings.Default.commaFontStyle = FontStyle.Regular;
                    break;
                case "Bold":
                    Properties.Settings.Default.commaFontStyle = FontStyle.Bold;
                    break;
                case "Italic":
                    Properties.Settings.Default.commaFontStyle = FontStyle.Italic;
                    break;
                case "Strikethrough":
                    Properties.Settings.Default.commaFontStyle = FontStyle.Strikeout;
                    break;
                case "Underline":
                    Properties.Settings.Default.commaFontStyle = FontStyle.Underline;
                    break;
            } Properties.Settings.Default.Save();
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox9.SelectedText)
            {
                case "Regular":
                    Properties.Settings.Default.keywordFontStyle = FontStyle.Regular;
                    break;
                case "Bold":
                    Properties.Settings.Default.keywordFontStyle = FontStyle.Bold;
                    break;
                case "Italic":
                    Properties.Settings.Default.keywordFontStyle = FontStyle.Italic;
                    break;
                case "Strikethrough":
                    Properties.Settings.Default.keywordFontStyle = FontStyle.Strikeout;
                    break;
                case "Underline":
                    Properties.Settings.Default.keywordFontStyle = FontStyle.Underline;
                    break;
            } Properties.Settings.Default.Save();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedText)
            {
                case "Regular":
                    Properties.Settings.Default.anonFontStyle = FontStyle.Regular;
                    break;
                case "Bold":
                    Properties.Settings.Default.anonFontStyle = FontStyle.Bold;
                    break;
                case "Italic":
                    Properties.Settings.Default.anonFontStyle = FontStyle.Italic;
                    break;
                case "Strikethrough":
                    Properties.Settings.Default.anonFontStyle = FontStyle.Strikeout;
                    break;
                case "Underline":
                    Properties.Settings.Default.anonFontStyle = FontStyle.Underline;
                    break;
            } Properties.Settings.Default.Save();
        }

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox10.SelectedText)
            {
                case "Regular":
                    Properties.Settings.Default.equalsFontStyle = FontStyle.Regular;
                    break;
                case "Bold":
                    Properties.Settings.Default.equalsFontStyle = FontStyle.Bold;
                    break;
                case "Italic":
                    Properties.Settings.Default.equalsFontStyle = FontStyle.Italic;
                    break;
                case "Strikethrough":
                    Properties.Settings.Default.equalsFontStyle = FontStyle.Strikeout;
                    break;
                case "Underline":
                    Properties.Settings.Default.equalsFontStyle = FontStyle.Underline;
                    break;
            } Properties.Settings.Default.Save();
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox6.SelectedText)
            {
                case "Regular":
                    Properties.Settings.Default.questionFontStyle = FontStyle.Regular;
                    break;
                case "Bold":
                    Properties.Settings.Default.questionFontStyle = FontStyle.Bold;
                    break;
                case "Italic":
                    Properties.Settings.Default.questionFontStyle = FontStyle.Italic;
                    break;
                case "Strikethrough":
                    Properties.Settings.Default.questionFontStyle = FontStyle.Strikeout;
                    break;
                case "Underline":
                    Properties.Settings.Default.questionFontStyle = FontStyle.Underline;
                    break;
            } Properties.Settings.Default.Save();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox3.SelectedText)
            {
                case "Regular":
                    Properties.Settings.Default.commentFontStyle = FontStyle.Regular;
                    break;
                case "Bold":
                    Properties.Settings.Default.commentFontStyle = FontStyle.Bold;
                    break;
                case "Italic":
                    Properties.Settings.Default.commentFontStyle = FontStyle.Italic;
                    break;
                case "Strikethrough":
                    Properties.Settings.Default.commentFontStyle = FontStyle.Strikeout;
                    break;
                case "Underline":
                    Properties.Settings.Default.commentFontStyle = FontStyle.Underline;
                    break;
            } Properties.Settings.Default.Save();
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox8.SelectedText)
            {
                case "Regular":
                    Properties.Settings.Default.systemParFontStyle = FontStyle.Regular;
                    break;
                case "Bold":
                    Properties.Settings.Default.systemParFontStyle = FontStyle.Bold;
                    break;
                case "Italic":
                    Properties.Settings.Default.systemParFontStyle = FontStyle.Italic;
                    break;
                case "Strikethrough":
                    Properties.Settings.Default.systemParFontStyle = FontStyle.Strikeout;
                    break;
                case "Underline":
                    Properties.Settings.Default.systemParFontStyle = FontStyle.Underline;
                    break;
            }
            Properties.Settings.Default.Save();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox5.SelectedText)
            {
                case "Regular":
                    Properties.Settings.Default.parentesiFontStyle = FontStyle.Regular;
                    break;
                case "Bold":
                    Properties.Settings.Default.parentesiFontStyle = FontStyle.Bold;
                    break;
                case "Italic":
                    Properties.Settings.Default.parentesiFontStyle = FontStyle.Italic;
                    break;
                case "Strikethrough":
                    Properties.Settings.Default.parentesiFontStyle = FontStyle.Strikeout;
                    break;
                case "Underline":
                    Properties.Settings.Default.parentesiFontStyle = FontStyle.Underline;
                    break;
            }
            Properties.Settings.Default.Save();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            prologDefault();
        }

        private void prologDefault()
        {

            //keyword
            Properties.Settings.Default.keywordForeColor = Color.Teal;
            Properties.Settings.Default.keywordBackColor = Color.Transparent;
            Properties.Settings.Default.keywordFontStyle = FontStyle.Bold;

            //comma
            Properties.Settings.Default.commaForeColor = Color.Gold;
            Properties.Settings.Default.commaBackColor = Color.Transparent;
            Properties.Settings.Default.commaFontStyle = FontStyle.Bold;

            //point
            Properties.Settings.Default.pointForeColor = Color.Goldenrod;
            Properties.Settings.Default.pointBackColor = Color.Transparent;
            Properties.Settings.Default.pointFontStyle = FontStyle.Bold;

            //question
            Properties.Settings.Default.questionForeColor = Color.LimeGreen;
            Properties.Settings.Default.questionBackColor = Color.Transparent;
            Properties.Settings.Default.questionFontStyle = FontStyle.Regular;

            //systempar
            Properties.Settings.Default.systemParForeColor = Color.Coral;
            Properties.Settings.Default.systemParBackColor = Color.Transparent;
            Properties.Settings.Default.systemParFontStyle = FontStyle.Regular;

            //equals
            Properties.Settings.Default.equalsForeColor = Color.SteelBlue;
            Properties.Settings.Default.equalsBackColor = Color.Transparent;
            Properties.Settings.Default.equalsFontStyle = FontStyle.Regular;

            //anon
            Properties.Settings.Default.anonForeColor = Color.DarkOrchid;
            Properties.Settings.Default.anonBackColor = Color.Transparent;
            Properties.Settings.Default.anonFontStyle = FontStyle.Regular;

            //comment
            Properties.Settings.Default.commentForeColor = Color.Gray;
            Properties.Settings.Default.commentBackColor = Color.Transparent;
            Properties.Settings.Default.commentFontStyle = FontStyle.Italic;

            //brackets
            Properties.Settings.Default.parentesiForeColor = Color.Red;
            Properties.Settings.Default.parentesiBackColor = Color.Transparent;
            Properties.Settings.Default.parentesiFontStyle = FontStyle.Bold;

            //save
            Properties.Settings.Default.Save();

            //load back everything in the mdi
            loadFontStyleProlog();
            loadHighlightPrologColors();
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            
        }

        private void checkAnimationsBox()
        {
            checkBox10.Checked = Properties.Settings.Default.animationEnable; //enable or disable all animations
            checkBox7.Checked = Properties.Settings.Default.animationResizeEnable; //enable or disable resizing animations
            checkBox8.Checked = Properties.Settings.Default.animationDockBottomEnable;
            checkBox9.Checked = Properties.Settings.Default.animationSettingResizeEnable;
            //MessageBox.Show("Animation + " +Properties.Settings.Default.animationSettingResize.ToString() + " resize + " + Properties.Settings.Default.animationResizeLeft.ToString() + " resize setting + " + Properties.Settings.Default.animationDockBottom.ToString());
        }

        private void checkAnimationTimeout()
        {
            numericUpDown1.Value = Properties.Settings.Default.animationResizeTop; // resize timing
            numericUpDown2.Value = Properties.Settings.Default.animationDockBottom; // resize timing
            numericUpDown3.Value = Properties.Settings.Default.animationSettingResize;//animationSettingResize; // resize setting timing
        }

        private void applyAnimationTimeout()
        {
            Properties.Settings.Default.animationResizeTop = (int)numericUpDown1.Value; // assign value to the correct NumericUpDown
           Properties.Settings.Default.animationDockBottom = (int)numericUpDown2.Value; // assign value to the correct NumericUpDown
            Properties.Settings.Default.animationSettingResize = (int)numericUpDown3.Value; // assign value to the correct NumericUpDown
            Properties.Settings.Default.Save();

        }


        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
          
            
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox7.Checked)
            {
                Properties.Settings.Default.animationResizeEnable = false;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.animationResizeEnable = true;
                Properties.Settings.Default.Save();
            }
          
            
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox8.Checked)
            {
                Properties.Settings.Default.animationDockBottomEnable = false;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.animationDockBottomEnable = true;
                Properties.Settings.Default.Save();
            }
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox9.Checked)
            {
                Properties.Settings.Default.animationSettingResizeEnable = false;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.animationSettingResizeEnable = true;
                Properties.Settings.Default.Save();
            }
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            
            if (!checkBox10.Checked)
            {
                groupBox14.Enabled = false;
                checkBox7.Checked = false;
                groupBox15.Enabled = false;
                checkBox8.Checked = false;
                groupBox16.Enabled = false;
                checkBox9.Checked = false;
            }
            else
            {
                groupBox14.Enabled = true;
                groupBox15.Enabled = true;
                groupBox16.Enabled = true;
            }
           /*
                if (checkBox7.Checked)
                {
                    Properties.Settings.Default.animationResizeEnable = true;
                }
                else
                {
                    Properties.Settings.Default.animationResizeEnable = false;
                }

                if (!checkBox8.Checked)
                {
                    Properties.Settings.Default.animationDockBottomEnable = false;
                }
                else
                {
                    Properties.Settings.Default.animationDockBottomEnable = true;
                }

                if (!checkBox9.Checked)
                {
                    Properties.Settings.Default.animationSettingResizeEnable = false;
                }
                else
                {
                    Properties.Settings.Default.animationSettingResizeEnable = true;
               }
            * */
               Properties.Settings.Default.Save();
            
            
        }

        private void button26_Click(object sender, EventArgs e)
        {
            applyAnimationTimeout();
            checkAnimationTimeout();
        }

        private void numericUpDown2_ValueChanged_1(object sender, EventArgs e)
        {

        }

       

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox11.Checked)
            {
                Properties.Settings.Default.customIcons = true;
                label63.Text = "Logix will set as its icon pack the one you specify, ignoring your Windows version.";
                comboBox16.Enabled = true;
                label64.Enabled = true;
                pictureBox3.Enabled = true;
                label61.Enabled = true;
                label62.Enabled = true;
                label63.Enabled = true;
                groupBox17.Enabled = true;

            }
            else
            {
                Properties.Settings.Default.customIcons = false;
                label63.Text = "Logix will automatically change its icon pack based on the Windows version you're currently running.";
                comboBox16.Enabled = false;
                label64.Enabled = false;
                pictureBox3.Enabled = false;
                label61.Enabled = false;
                label62.Enabled = false;
                label63.Enabled = false;
                groupBox17.Enabled = false;
            }
            Properties.Settings.Default.Save();
            mdiparent.WindowsCheck();
        }

        

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button27_Click(object sender, EventArgs e)
        {
            try
            {
               
                    //go to line manager
                    if (System.Text.RegularExpressions.Regex.IsMatch(textBox2.Text, "[^0-9]"))
                    {
                        //roundedPanel1.Height = 56;
                        ToolTip hint = new ToolTip();
                        hint.IsBalloon = true;
                        hint.ToolTipTitle = "Please enter only numbers";
                        hint.ToolTipIcon = ToolTipIcon.Error;
                        hint.Show(string.Empty, textBox2, -10, -10, 0);
                        hint.Show("Letters and symbols are not allowed.", textBox2);
                        //label15.Text = "Please enter only numbers";
                        //label15.Left = (this.roundedPanel1.Width - label15.Width) / 2;
                        textBox1.Text = textBox2.Text.Remove(textBox2.Text.Length - 1);
                        //check if the number of lines inserted is right
                    }
                    else
                        Properties.Settings.Default.columnLineLimit = int.Parse(textBox2.Text);
                
            }
            catch (Exception ea)
            {

                ToolTip hint = new ToolTip();
                hint.IsBalloon = true;
                hint.ToolTipTitle = "Please enter only numbers";
                hint.ToolTipIcon = ToolTipIcon.Error;
                hint.Show(string.Empty, textBox2, 0);
                    hint.Show("Letters and symbols are not allowed.", textBox2, 15, 17);

            }


            Properties.Settings.Default.antoniottiCrazyTextDuo = textBox6.Text;
            Properties.Settings.Default.antoniottiCrazyTitle = textBox5.Text;
            Properties.Settings.Default.antoniottiCrazyText = textBox4.Text;
            Properties.Settings.Default.antoniottiStandardText = textBox3.Text;
            Properties.Settings.Default.Save();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    //go to line manager
                    if (System.Text.RegularExpressions.Regex.IsMatch(textBox2.Text, "[^0-9]"))
                    {
                        //roundedPanel1.Height = 56;
                        ToolTip hint = new ToolTip();
                        hint.IsBalloon = true;
                        hint.ToolTipTitle = "Please enter only numbers";
                        hint.ToolTipIcon = ToolTipIcon.Error;
                        hint.Show(string.Empty, textBox2, -10, -10, 0);
                        hint.Show("Letters and symbols are not allowed.", textBox2);
                        //label15.Text = "Please enter only numbers";
                        //label15.Left = (this.roundedPanel1.Width - label15.Width) / 2;
                        textBox1.Text = textBox2.Text.Remove(textBox2.Text.Length - 1);
                        //check if the number of lines inserted is right
                    }
                    else
                        Properties.Settings.Default.columnLineLimit = int.Parse(textBox2.Text);
                }
            }
            catch (Exception ea)
            {

                ToolTip hint = new ToolTip();
                hint.IsBalloon = true;
                hint.ToolTipTitle = "Please enter only numbers";
                hint.ToolTipIcon = ToolTipIcon.Error;
                hint.Show(string.Empty, textBox2, 0);
                    hint.Show("Letters and symbols are not allowed.", textBox2, 15, 17);
                

            }

            Properties.Settings.Default.Save();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.antoniottiStandardText = textBox3.Text;
            Properties.Settings.Default.Save();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.antoniottiCrazyText = textBox4.Text;
            Properties.Settings.Default.Save();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.antoniottiCrazyTitle = textBox5.Text;
            Properties.Settings.Default.Save();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.antoniottiCrazyTextDuo = textBox6.Text;
            Properties.Settings.Default.Save();
        }

        private void button28_Click(object sender, EventArgs e)
        {
            string statusBarText = "Exceeded column limit";
            string crazystatusText = "HERESY!!!";
            string crazytitleText = "Thou hast sinned.";
            string crazytextText = "Thou hast traspassed the holy limit.";

            Properties.Settings.Default.antoniottiStandardText = statusBarText;
            Properties.Settings.Default.antoniottiCrazyText = crazystatusText;
            Properties.Settings.Default.antoniottiCrazyTitle = crazytitleText;
            Properties.Settings.Default.antoniottiCrazyTextDuo = crazytextText;
            Properties.Settings.Default.Save();
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox12.Checked)
            {
                Properties.Settings.Default.syntaxFileExtension = true;
                label69.Enabled = false;
                comboBox15.Enabled = false;
            }
            else
            {
                Properties.Settings.Default.syntaxFileExtension = false;
                label69.Enabled = true;
                comboBox15.Enabled = true;
            }
            Properties.Settings.Default.Save();
        }

        private void comboBox15_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.syntaxFileExtensionIndex = comboBox15.SelectedIndex + 1;
            Properties.Settings.Default.Save();
        }

        private void comboBox14_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.syntaxFileExtensionIndexOpen = comboBox14.SelectedIndex + 1;
            Properties.Settings.Default.Save();
        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox13.Checked)
            {
                Properties.Settings.Default.syntaxFileExtensionOpen = true;
                label70.Enabled = false;
                comboBox14.Enabled = false;
            }
            else
            {
                Properties.Settings.Default.syntaxFileExtensionOpen = false;
                label70.Enabled = true;
                comboBox14.Enabled = true;
            }
            Properties.Settings.Default.Save();
        }

        private void comboBox16_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox16.Text){
                case "Fluent Set":
                Properties.Settings.Default.fluentStyle = true;
                Properties.Settings.Default.lunaStyle = false;
                Properties.Settings.Default.classicStyle = false;
                Properties.Settings.Default.ClassicNineStyle = false;
                pictureBox3.Image = Properties.Resources.fluentIcons;

                label73.Text = "This icon set uses Segoe Fluent Icons for its icons. Suits best Windows 11.";
                break;

                case "Aero/Luna Icons":
                Properties.Settings.Default.fluentStyle = false;
                Properties.Settings.Default.lunaStyle = true;
                Properties.Settings.Default.classicStyle = false;
                Properties.Settings.Default.ClassicNineStyle = false;
                pictureBox3.Image = Properties.Resources.AeroIcons;
                label73.Text = "This icon set uses Office 2010 icons for its icon set. Suits best Windows XP/Vista/7.";
                break;

                case "Classic Style":
                Properties.Settings.Default.fluentStyle = false;
                Properties.Settings.Default.lunaStyle = false;
                Properties.Settings.Default.classicStyle = true;
                Properties.Settings.Default.ClassicNineStyle = false;
                pictureBox3.Image = Properties.Resources.classicIcons;
                label73.Text = "This icon set mimicks the 9x Explorer bar look. Suits best Windows 2000.";

                break;

                case "Mac OS 9/Gnome Classic":
                Properties.Settings.Default.fluentStyle = false;
                Properties.Settings.Default.lunaStyle = false;
                Properties.Settings.Default.classicStyle = false;
                Properties.Settings.Default.ClassicNineStyle = true;
                pictureBox3.Image = Properties.Resources.OS9;
                label73.Text = "This icon set uses icons from the nineicons-redux Gnome theme. Suits best Linux and/or Windows 2000.";
                break;
            }
            Properties.Settings.Default.Save();
        }

        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox14.Checked)
            {
                context.Visible = true;
                contextWeb.Visible = true;
                contextWeb2.Visible = true;
                Properties.Settings.Default.searchWeb = true;
                checkBox15.Enabled = true;
                checkBox16.Enabled = true;
                    label80.Enabled = true;
                    if (Properties.Settings.Default.searchWebAuto && Properties.Settings.Default.searchWeb)
                    {
                        label79.Enabled = false;
                        label78.Enabled = false;
                        comboBox17.Enabled = false;
                        checkBox16.Enabled = false;
                        label77.Enabled = false;
                        label76.Enabled = false;
                        label75.Enabled = false;
                        textBox7.Enabled = false;
                        button29.Enabled = false;
                    }
                    else
                    {
                        label79.Enabled = true;
                        label78.Enabled = true;
                        comboBox17.Enabled = true;
                        checkBox16.Enabled = true;
                        
                    }


                    if (Properties.Settings.Default.searchWebCustom && Properties.Settings.Default.searchWeb)
                    {
                        label77.Enabled = true;
                        label75.Enabled = true;
                        label76.Enabled = true;
                        textBox7.Enabled = true;
                        button29.Enabled = true;
                        checkBox15.Enabled = false;
                        label80.Enabled = false;
                        label79.Enabled = false;
                        label78.Enabled = false;
                        comboBox17.Enabled = false;
                    }
                    else
                    {
                        label77.Enabled = false;
                        label75.Enabled = false;
                        label76.Enabled = false;
                        textBox7.Enabled = false;
                        button29.Enabled = false;
                    }

            }
            else if (!checkBox14.Checked)
            {
                context.Visible = false;
                contextWeb.Visible = false;
                contextWeb2.Visible = false;
                Properties.Settings.Default.searchWeb = false;
                checkBox15.Enabled = false;
                checkBox16.Enabled = false;
                label80.Enabled = false;
                label79.Enabled = false;
                label78.Enabled = false;
                comboBox17.Enabled = false;
                
                label77.Enabled = false;
                label75.Enabled = false;
                label76.Enabled = false;
                textBox7.Enabled = false;
                button29.Enabled = false;
            }
            Properties.Settings.Default.Save();
        }

        private void checkBox16_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox16.Checked && Properties.Settings.Default.searchWeb)
            {
                Properties.Settings.Default.searchWebCustom = true;
                label77.Enabled = true;
                label75.Enabled = true;
                label76.Enabled = true;
                textBox7.Enabled = true;
                button29.Enabled = true;
                label80.Enabled = false;
                checkBox15.Enabled = false;
                label79.Enabled = false;
                label78.Enabled = false;
                comboBox17.Enabled = false;
            }
            else if (!checkBox16.Checked || !Properties.Settings.Default.searchWeb)
            {
                Properties.Settings.Default.searchWebCustom = false;
                label77.Enabled = false;
                label75.Enabled = false;
                label76.Enabled = false;
                textBox7.Enabled = false;
                button29.Enabled = false;
                label80.Enabled = true;
                checkBox15.Enabled = true;
                label79.Enabled = true;
                label78.Enabled = true;
                comboBox17.Enabled = true;
            }
        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox15.Checked && Properties.Settings.Default.searchWeb)
            {
                Properties.Settings.Default.searchWebAuto = false;
                label79.Enabled = true;
                label78.Enabled = true;
                comboBox17.Enabled = true;
                checkBox16.Enabled = true;
                if (Properties.Settings.Default.searchWebCustom)
                {
                    label77.Enabled = true;
                    label75.Enabled = true;
                    label76.Enabled = true;
                    textBox7.Enabled = true;
                    button29.Enabled = true;
                    checkBox16.Checked = true;
                  
                }
                else
                {
                    label77.Enabled = false;
                    label75.Enabled = false;
                    label76.Enabled = false;
                    textBox7.Enabled = false;
                    button29.Enabled = false;
                }
            }
            else if (checkBox15.Checked || !Properties.Settings.Default.searchWeb)
            {
                label79.Enabled = false;
                Properties.Settings.Default.searchWebAuto = true;
                label78.Enabled = false;
                comboBox17.Enabled = false;
                checkBox16.Enabled = false;
                label77.Enabled = false;
                label75.Enabled = false;
                label76.Enabled = false;
                textBox7.Enabled = false;
                button29.Enabled = false;
            }
        }

        private void comboBox17_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox17.Text)
            {
                case "Google":
                    Properties.Settings.Default.searchWebPath = "https://www.google.com/search?q=";
                    Properties.Settings.Default.searchWebPathName = "Google";
                    break;

                case "Bing":
                    Properties.Settings.Default.searchWebPath = "https://www.bing.com/search?form=&q=";
                    Properties.Settings.Default.searchWebPathName = "Bing";
                    break;

                case "DuckDuckGo!":
                    Properties.Settings.Default.searchWebPath = "https://duckduckgo.com/?t=h_&q=";
                    Properties.Settings.Default.searchWebPathName = "DuckDuckGo!";
                    break;

                case "Yahoo":
                    Properties.Settings.Default.searchWebPath = "https://search.yahoo.com/search?p=";
                    Properties.Settings.Default.searchWebPathName = "Yahoo";
                    break;

                case "StackOverflow":
                    Properties.Settings.Default.searchWebPath = "https://stackoverflow.com/search?q=";
                    Properties.Settings.Default.searchWebPathName = "StackOverflow";
                    break;

                case "Ecosia":
                    Properties.Settings.Default.searchWebPath = "https://www.ecosia.org/search?method=index&q=";
                    Properties.Settings.Default.searchWebPathName = "Ecosia";
                    break;

                case "Internet Archive":
                    Properties.Settings.Default.searchWebPath = "https://web.archive.org/web/";
                    Properties.Settings.Default.searchWebPathName = "Internet Archive";
                    break;

                case "SWI-Prolog":
                    Properties.Settings.Default.searchWebPath = "https://www.swi-prolog.org/search?for=";
                    Properties.Settings.Default.searchWebPathName = "SWI-Prolog";
                    break;

                case "Common LISP Wiki":
                    Properties.Settings.Default.searchWebPath = "https://www.cliki.net/site/search?query=";
                    Properties.Settings.Default.searchWebPathName = "Common LISP Wiki";
                    break;

                default:
                    Properties.Settings.Default.Save();
                    break;
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
           

            
            try
            {
               
                    //internet address manager
                    if (System.Text.RegularExpressions.Regex.IsMatch(textBox7.Text, "^(http|https)://"))
                    {
                        //roundedPanel1.Height = 56;
                        ToolTip hint = new ToolTip();
                        hint.IsBalloon = true;
                        hint.ToolTipTitle = "Not a valid Internet address";
                        hint.ToolTipIcon = ToolTipIcon.Error;
                        hint.Show(string.Empty, textBox7, -10, -10, 0);
                        hint.Show("Enter a web address (must start with either http:// or https://.", textBox2);
                        //label15.Text = "Please enter only numbers";
                        //label15.Left = (this.roundedPanel1.Width - label15.Width) / 2;
                        textBox1.Text = textBox2.Text.Remove(textBox2.Text.Length - 1);
                        //check if the number of lines inserted is right
                    }
                    else
                         Properties.Settings.Default.searchWebPath = textBox7.Text;
                
            }
            catch (Exception ea)
            {

                ToolTip hint = new ToolTip();
                hint.IsBalloon = true;
                hint.ToolTipTitle = "Not a valid Internet address";
                hint.ToolTipIcon = ToolTipIcon.Error;
                hint.Show(string.Empty, textBox7, 0);
                    hint.Show("Enter a web address (must start with either http:// or https://.", textBox7, -15, -17);

            }
            Properties.Settings.Default.Save();
        }

        private void checkBox17_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox17.Checked)
            {
                Properties.Settings.Default.syntaxOpenFileAuto = true;
                label82.Enabled = false;
                label83.Enabled = false;
                comboBox18.Enabled = false;
            }
            else
            {
                Properties.Settings.Default.syntaxOpenFileAuto = false;
                label82.Enabled = true;
                label83.Enabled = true;
                comboBox18.Enabled = true;
            }
            Properties.Settings.Default.Save();
        }

        private void comboBox18_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox18.Text)
            {
                case "plain text":
                    Properties.Settings.Default.syntaxOpenDefaultMode = "None";
                    break;

                case "Prolog":
                    Properties.Settings.Default.syntaxOpenDefaultMode = "Prolog";
                    break;

                case "LISP":
                    Properties.Settings.Default.syntaxOpenDefaultMode = "Lisp";
                    break;

                case "Yacc/J":
                    Properties.Settings.Default.syntaxOpenDefaultMode = "yacc";
                    break;

                case "JFlex":
                    Properties.Settings.Default.syntaxOpenDefaultMode = "jflex";
                    break;

                case "previous one":
                    Properties.Settings.Default.syntaxOpenDefaultMode = "Previous one";
                    break;
            }

            Properties.Settings.Default.Save();
        }

        private void checkBox18_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox18.Checked)
            {
                Properties.Settings.Default.autoWindowPosition = false;
                label85.Enabled = true; label86.Enabled = true; label87.Enabled = true; comboBox19.Enabled = true; textBox8.Enabled = true; textBox9.Enabled = true; button30.Enabled = true;
            }
            else if (checkBox18.Checked)
            {
                Properties.Settings.Default.autoWindowPosition = true;
                label85.Enabled = true; label86.Enabled = false; label87.Enabled = false; comboBox19.Enabled = false; textBox8.Enabled = false; textBox9.Enabled = false; button30.Enabled = false;
            }
            Properties.Settings.Default.Save();
        }

        private void comboBox19_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox19.Text)
            {
                case "Center to the screen":
                    Properties.Settings.Default.startWindowPlace = "CenterScreen";
                    label86.Enabled = false; label87.Enabled = false; textBox8.Enabled = false; textBox9.Enabled = false; button30.Enabled = false;
                    break;

                    case "Windows default position (top left)":
                    Properties.Settings.Default.startWindowPlace = "WindowsDefaultLocation";
                    label86.Enabled = false; label87.Enabled = false; textBox8.Enabled = false; textBox9.Enabled = false; button30.Enabled = false;
                    break;

                case "Windows default bounds":
                    Properties.Settings.Default.startWindowPlace = "WindowsDefaultBounds";
                    label86.Enabled = false; label87.Enabled = false; textBox8.Enabled = false; textBox9.Enabled = false; button30.Enabled = false;
                    break;

                case "Manual (specify location)":
                    Properties.Settings.Default.startWindowPlace = "Manual";
                    label86.Enabled = true; label87.Enabled = true; textBox8.Enabled = true; textBox9.Enabled = true; button30.Enabled = true;
                    break;
            }

            Properties.Settings.Default.Save();
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            //screen dimensions
            int screenWidth = int.Parse(getScreenWidth());
            try
            {

                //go to line manager
                if (int.Parse(textBox8.Text) > screenWidth)
                {
                    ToolTip hint = new ToolTip();
                    hint.IsBalloon = true;
                    hint.ToolTipTitle = "You're positioning your window outside your monitor";
                    hint.ToolTipIcon = ToolTipIcon.Error;
                    hint.Show(string.Empty, textBox8, -10, -10, 0);
                    hint.Show("Please enter a number lower than " + screenWidth + 1 + ".", textBox8);
                }
                else
                    Properties.Settings.Default.windowLeft = int.Parse(textBox8.Text);
                Properties.Settings.Default.Save();

            }
            catch (Exception ea)
            {

                ToolTip hint = new ToolTip();
                hint.IsBalloon = true;
                hint.ToolTipTitle = "You're positioning your window outside your monitor";
                hint.ToolTipIcon = ToolTipIcon.Error;
                hint.Show(string.Empty, textBox8, 0);
                hint.Show("Please enter a number lower than " + screenWidth + 1 + ".", textBox8, 15, 17);

            }

     
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            //screen dimensions
            int screenHeight = int.Parse(getScreenHeight());
            try
            {

                //go to line manager
                if (int.Parse(textBox9.Text) > screenHeight)
                {
                    ToolTip hint = new ToolTip();
                    hint.IsBalloon = true;
                    hint.ToolTipTitle = "You're positioning your window outside your monitor";
                    hint.ToolTipIcon = ToolTipIcon.Error;
                    hint.Show(string.Empty, textBox9, -10, -10, 0);
                    hint.Show("Please enter a number lower than " + (screenHeight + 1) + ".", textBox9);
                }
                else 
                    Properties.Settings.Default.windowTop = int.Parse(textBox9.Text);
                Properties.Settings.Default.Save();

            }
            catch (Exception ea)
            {

                ToolTip hint = new ToolTip();
                hint.IsBalloon = true;
                hint.ToolTipTitle = "You're positioning your window outside your monitor";
                hint.ToolTipIcon = ToolTipIcon.Error;
                hint.Show(string.Empty, textBox9, 0);
                hint.Show("Please enter a number lower than " + (screenHeight + 1) + ".", textBox9, 15, 17);

            }

           
        }

        private void checkBox19_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox19.Checked)
            {
                Properties.Settings.Default.autoWindowSize = false;
                label89.Enabled = true; label88.Enabled = true; textBox11.Enabled = true; textBox10.Enabled = true; button31.Enabled = true;
            }
            else if (checkBox19.Checked)
            {
                Properties.Settings.Default.autoWindowSize = true;
                label89.Enabled = false; label88.Enabled = false; textBox11.Enabled = false; textBox10.Enabled = false; button31.Enabled = false;
            }
            Properties.Settings.Default.Save();
        }

        private string getScreenWidth()
        {
            return Screen.PrimaryScreen.Bounds.Width.ToString();
        }

        private string getScreenHeight()
        {
            return Screen.PrimaryScreen.Bounds.Height.ToString();
        }
        private void textBox11_TextChanged(object sender, EventArgs e)
        {
         
            //screen dimensions
            int screenWidth = int.Parse(getScreenWidth());
            try
            {

                //go to line manager
                if (int.Parse(textBox11.Text) > screenWidth)
                {
                    ToolTip hint = new ToolTip();
                    hint.IsBalloon = true;
                    hint.ToolTipTitle = "Your window might be wider than your monitor";
                    hint.ToolTipIcon = ToolTipIcon.Error;
                    hint.Show(string.Empty, textBox11, -10, -10, 0);
                    hint.Show("Please enter a number lower than " + (screenWidth+1) + ".", textBox11);
                }
                else
                    Properties.Settings.Default.windowWidth = int.Parse(textBox11.Text);
                Properties.Settings.Default.Save();
            }
            catch (Exception ea)
            {

                ToolTip hint = new ToolTip();
                hint.IsBalloon = true;
                hint.ToolTipTitle = "Your window might be wider than your monitor";
                hint.ToolTipIcon = ToolTipIcon.Error;
                hint.Show(string.Empty, textBox11, 0);
                hint.Show("Please enter a number lower than " + (screenWidth + 1) + ".", textBox11, 15, 17);

            }
            
            
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            //screen dimensions
            int screenHeight = int.Parse(getScreenHeight());
            try
            {

                //go to line manager
                if (int.Parse(textBox10.Text) > screenHeight)
                {
                    ToolTip hint = new ToolTip();
                    hint.IsBalloon = true;
                    hint.ToolTipTitle = "Your window might be taller than your monitor";
                    hint.ToolTipIcon = ToolTipIcon.Error;
                    hint.Show(string.Empty, textBox10, -10, -10, 0);
                    hint.Show("Please enter a number lower than " + (screenHeight + 1) + ".", textBox10);
                }
                else
                    Properties.Settings.Default.windowHeight = int.Parse(textBox10.Text);
                Properties.Settings.Default.Save();

            }
            catch (Exception ea)
            {

                ToolTip hint = new ToolTip();
                hint.IsBalloon = true;
                hint.ToolTipTitle = "Your window might be taller than your monitor";
                hint.ToolTipIcon = ToolTipIcon.Error;
                hint.Show(string.Empty, textBox10, 0);
                hint.Show("Please enter a number lower than " + (screenHeight + 1) + ".", textBox10, 15, 17);

            }

            
        }

        private void checkBox20_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox20.Checked)
            {
                Properties.Settings.Default.autoWindowState = false;
                label91.Enabled = true; comboBox20.Enabled = true;
            }
            else if (checkBox20.Checked)
            {
                Properties.Settings.Default.autoWindowState = true;
                label91.Enabled = false; comboBox20.Enabled = false;
            }
        }

        private void comboBox20_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox20.Text)
            {
                case "Normal":
                    Properties.Settings.Default.windowState = "Normal";
                    break;

                case "Maximized":
                    Properties.Settings.Default.windowState = "Maximized";
                    break;
            }
        }

        private void checkBox21_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox21.Checked)
                Properties.Settings.Default.textPropertiesVisible = true;
            else
                Properties.Settings.Default.textPropertiesVisible = false;
            Properties.Settings.Default.Save();
        }

        private void checkBox23_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox23.Checked)
                label98.Text = "You'll be able to interact with JFlex through a graphical user interface.";
            else
                label98.Text = "JFlex will start in a Command Prompt window without a graphical user interface.";
        }

        private void checkBox24_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox24.Checked)
                label99.Text = "You'll be able to interact with ByaccJ through a graphical user interface (recommended).";
            else
                label99.Text = "ByaccJ will start in a Command Prompt window without a graphical user interface.";
        }

        private void checkBox22_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox22.Checked)
            {
                Properties.Settings.Default.jFlexIntegration = false;
                label95.Enabled = true; label96.Enabled = false; label97.Enabled = false;
                textBox12.Enabled = false; button33.Enabled = false; button32.Enabled = false;
                checkBox23.Enabled = false; label98.Enabled = false;
            }
            else
            {
                Properties.Settings.Default.jFlexIntegration = true;
                label95.Enabled = true; label96.Enabled = true; label97.Enabled = true;
                textBox12.Enabled = true; button33.Enabled = true; button32.Enabled = true;
                checkBox23.Enabled = true; label98.Enabled = true;
            }
        }

        private void checkBox25_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox25.Checked)
            {
                Properties.Settings.Default.bYaccIntegration = false;
                label100.Enabled = false; label101.Enabled = false;
                textBox13.Enabled = false; button35.Enabled = false; button34.Enabled = false;
                checkBox24.Enabled = false; label99.Enabled = false;
            }
            else
            {
                Properties.Settings.Default.bYaccIntegration = true;
                label100.Enabled = true; label101.Enabled = true;
                textBox13.Enabled = true; button35.Enabled = true; button34.Enabled = true;
                checkBox24.Enabled = true; label99.Enabled = true;
            }
        }

     


        
        }
    
    class darkModeColors{
        public Color WindowColor;
        public Color PanelBG; public Color PanelFG; //panel colors
        public Color ButtonBG; public Color ButtonFG; //button colors
        public Color TextBoxBG; public Color TextBoxFG; //textbox colors
        public Color LabelFG;

    public darkModeColors()
    {
        //set all the color variables from RGB - otherwise it doesn't work with msstyles
        //Thanks Microsoft for not knowing how to use your own theming engine!
        WindowColor = Color.FromArgb(0, 0, 0);
        PanelBG = Color.FromArgb(20, 20, 20);
        PanelFG = Color.FromArgb(255, 255, 255);
        ButtonBG = Color.FromArgb(15, 15, 15);
        ButtonFG = Color.FromArgb(255, 255, 255);
        TextBoxBG = Color.FromArgb(0, 0, 0);
        TextBoxFG = Color.FromArgb(255,255,255);
        LabelFG = Color.FromArgb(255, 255, 255);
    }

    }

        }

