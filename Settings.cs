using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using Microsoft.Win32;
using mshtml;

namespace PrologParsec
{
    public partial class Settings : Form
    {

        private WebBrowser webBrowser;
        private int auroraHeight;

        public Settings(WebBrowser webBrowser)
        {
            InitializeComponent();
            this.webBrowser = webBrowser;

            if (Properties.Settings.Default.auroraCustom)
                webBrowser1.Navigate(Properties.Settings.Default.auroraCustomURL);
     
            check();
        }

        public Settings()
        {
            InitializeComponent();
            reloadAurora(webBrowser);

            
            check();
        }

        public void check()
        {
            // debug MessageBox.Show(Properties.Settings.Default.auroraFile.ToString());
            if (!Properties.Settings.Default.auroraCustom)
            {
                switch (Properties.Settings.Default.auroraFile)
                {
                    case "aurora.svg":
                        comboBox1.Text = "Blue and purple";
                        break;
                    case "aurora_black.svg":
                        comboBox1.Text = "Black";
                        break;
                    case "aurora_blue.svg":
                        comboBox1.Text = "Light blue";
                        break;
                    case "aurora_brown.svg":
                        comboBox1.Text = "Yellow";
                        break;
                    case "aurora_green.svg":
                        comboBox1.Text = "Green";
                        break;
                    case "aurora_grey.svg":
                        comboBox1.Text = "Grey";
                        break;
                    case "aurora_lime.svg":
                        comboBox1.Text = "Lime";
                        break;
                    case "aurora_orange.svg":
                        comboBox1.Text = "Orange";
                        break;
                    case "aurora_pink.svg":
                        comboBox1.Text = "Pink";
                        break;
                    case "aurora_red.svg":
                        comboBox1.Text = "Red";
                        break;
                    case "aurora_solar.svg":
                        comboBox1.Text = "Solarized";
                        break;

                }
            }

            if (Properties.Settings.Default.auroraStartPage)
                checkBox1.Checked = true;
            else
                checkBox1.Checked = false;

            if (webBrowser.Visible)
                checkBox1.Checked = true;
            else
                checkBox1.Checked = false;

            if (Properties.Settings.Default.auroraCustom)
                checkBox2.Checked = true;
            else
                checkBox2.Checked = false;
            auroraCustomCheck();
            textBox1.Text = Properties.Settings.Default.auroraCustomURL;

            darkModeColors dark = new darkModeColors();
            if (Properties.Settings.Default.appTheme == "dark")
                radioButton20.Checked = true;
            else if (Properties.Settings.Default.appTheme == "system")
                radioButton22.Checked = true;
            // else if (Properties.Settings.Default.appTheme == "light")
            //radioButton21.Checked = true;
        }


        private void Settings_Load(object sender, EventArgs e)
        {
            auroraHeight = 285;

            label28.Text = Properties.Resources.AppName + " " + Properties.Resources.AppEdition + " " + Properties.Resources.AppVersion;
            label16.Text = Properties.Resources.AppName; label15.Text = Properties.Resources.AppEdition + " Edition"; label14.Text = Properties.Resources.AppDescription;
        }

        private void reloadAurora(WebBrowser wb)
        {
            string curDir = Directory.GetCurrentDirectory();
            if (!Properties.Settings.Default.auroraCustom)
            {
                this.webBrowser1.Url = new Uri(String.Format("file:///{0}/aurora/" + Properties.Settings.Default.auroraFile, curDir));
                wb.Url = new Uri(String.Format("file:///{0}/aurora/" + Properties.Settings.Default.auroraFile, curDir));
            }
            else
            {
                webBrowser1.Navigate(textBox1.Text);
                wb.Navigate(textBox1.Text);
            }
        }



        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
          
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                label8.Text = "Personalise the appearance of the Aurora effect in the start page.";
                webBrowser.Show();
                Properties.Settings.Default.auroraStartPage = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                label8.Text = "The Aurora effect is disabled. Any change will be applied next time you enable it.";
                webBrowser.Hide();
                Properties.Settings.Default.auroraStartPage = false;
                Properties.Settings.Default.Save();
            }
        }


        private void radioButton22_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.system;
            Properties.Settings.Default.appTheme = "system";
            Properties.Settings.Default.Save();
            systemTheme();
        }

        private void radioButton21_CheckedChanged(object sender, EventArgs e)
        {
                pictureBox1.Image = Properties.Resources.light;
            Properties.Settings.Default.appTheme = "light";
            Properties.Settings.Default.Save();
        }

        private void radioButton20_CheckedChanged(object sender, EventArgs e)
        {
                pictureBox1.Image = Properties.Resources.dark;
            Properties.Settings.Default.appTheme = "dark";
            Properties.Settings.Default.Save();
            darkMode();
        }

        

        public void darkMode()
        {
            darkModeColors dark = new darkModeColors();
            Settings.ActiveForm.BackColor = dark.WindowColor;
            label1.ForeColor = dark.LabelFG;
            label3.ForeColor = dark.LabelFG;
            label4.ForeColor = dark.LabelFG;
            label6.ForeColor = dark.LabelFG;
            label8.ForeColor = dark.LabelFG;

            comboBox1.BackColor = dark.TextBoxBG;
            comboBox1.ForeColor = dark.TextBoxFG;
            radioButton20.ForeColor = dark.LabelFG;
            radioButton21.ForeColor = dark.LabelFG;
            radioButton22.ForeColor = dark.LabelFG;

}

        

        public void systemTheme()
        {
            Settings.ActiveForm.BackColor = SystemColors.Window;
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
            darkMode();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "Blue and purple":
                    Properties.Settings.Default.auroraFile = "aurora.svg";
                    Properties.Settings.Default.Save();
                    reloadAurora(webBrowser);
                    break;
                case "Light blue":
                    Properties.Settings.Default.auroraFile = "aurora_blue.svg";
                    Properties.Settings.Default.Save();
                    reloadAurora(webBrowser);
                    break;
                case "Red":
                    Properties.Settings.Default.auroraFile = "aurora_red.svg";
                    Properties.Settings.Default.Save();
                    reloadAurora(webBrowser);
                    break;
                case "Yellow":
                    Properties.Settings.Default.auroraFile = "aurora_brown.svg";
                    Properties.Settings.Default.Save();
                    reloadAurora(webBrowser);
                    break;
                case "Green":
                    Properties.Settings.Default.auroraFile = "aurora_green.svg";
                    Properties.Settings.Default.Save();
                    reloadAurora(webBrowser);
                    break;
                case "Black":
                    Properties.Settings.Default.auroraFile = "aurora_black.svg";
                    Properties.Settings.Default.Save();
                    reloadAurora(webBrowser);
                    break;
                case "Grey":
                    Properties.Settings.Default.auroraFile = "aurora_grey.svg";
                    Properties.Settings.Default.Save();
                    reloadAurora(webBrowser);
                    break;
                case "Orange":
                    Properties.Settings.Default.auroraFile = "aurora_orange.svg";
                    Properties.Settings.Default.Save();
                    reloadAurora(webBrowser);
                    break;
                case "Pink":
                    Properties.Settings.Default.auroraFile = "aurora_pink.svg";
                    Properties.Settings.Default.Save();
                    reloadAurora(webBrowser);
                    break;
                case "Lime":
                    Properties.Settings.Default.auroraFile = "aurora_lime.svg";
                    Properties.Settings.Default.Save();
                    reloadAurora(webBrowser);
                    break;
                case "Solarized":
                    Properties.Settings.Default.auroraFile = "aurora_solar.svg";
                    Properties.Settings.Default.Save();
                    reloadAurora(webBrowser);
                    break;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            auroraOpen();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            auroraCustomCheck();
        }

        public void auroraCustomCheck()
        {
            if (checkBox2.Checked)
            {
                label11.Enabled = true;
                label12.Enabled = true;
                textBox1.Enabled = true;
                button4.Enabled = true;
                button1.Enabled = true;
                comboBox1.Enabled = false;
                label7.Enabled = false;
                label9.Enabled = false;
                Properties.Settings.Default.auroraCustom = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                label11.Enabled = false;
                label12.Enabled = false;
                textBox1.Enabled = false;
                button4.Enabled = false;
                button1.Enabled = false;
                comboBox1.Enabled = true;
                label7.Enabled = true;
                label9.Enabled = true;
                Properties.Settings.Default.auroraCustom = false;
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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "SVG images (*.svg)|*.svg|XML documents (*.html)|*.html|JPEG images (*.jpg)|*.jpg|PNG images (*.png)|*.png|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            string filePath; string fileContent; string currentFilePath;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //read file
                filePath = openFileDialog.FileName;
                fileContent = File.ReadAllText(filePath);
                currentFilePath = openFileDialog.FileName;
                currentFilePath = Path.GetFileName(currentFilePath);
                try
                {
                    textBox1.Text = filePath; //put the window title
                    webBrowser1.Navigate(filePath);
                    webBrowser.Navigate(filePath);
                }
                catch (NullReferenceException e)
                {
                    
                }
            }
            
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                webBrowser.Navigate(textBox1.Text);
                webBrowser1.Navigate(textBox1.Text);
                Properties.Settings.Default.auroraCustomURL = textBox1.Text;
                Properties.Settings.Default.Save();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            webBrowser.Navigate(textBox1.Text);
                webBrowser1.Navigate(textBox1.Text);
            }

        private void button1_Click_2(object sender, EventArgs e)
        {
            webBrowser.Navigate(textBox1.Text);
            webBrowser1.Navigate(textBox1.Text);
        
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.auroraCustomURL = textBox1.Text;
            Properties.Settings.Default.auroraCustom = checkBox2.Checked;
            Properties.Settings.Default.Save();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (tabControl1.SelectedIndex == 3)
            {
                Settings.ActiveForm.Height = 506;
            }
            else
            {
                Settings.ActiveForm.Height = 285;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(button6, -65, -95);
        }
        
        }
    
    class darkModeColors{
        public Color WindowColor;
        public Color PanelBG; public Color PanelFG;
        public Color ButtonBG; public Color ButtonFG;
        public Color TextBoxBG; public Color TextBoxFG;
        public Color LabelFG;

    public darkModeColors()
    {
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

