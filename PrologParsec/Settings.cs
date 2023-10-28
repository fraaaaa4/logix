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

        private WebBrowser webBrowser; //web browser reference
        private int auroraHeight; private FarsiLibrary.Win.FATabStrip startPage; private FarsiLibrary.Win.FATabStripItem startPageTab; //tabs reference

        public Settings(WebBrowser webBrowser, FarsiLibrary.Win.FATabStrip startPage, FarsiLibrary.Win.FATabStripItem startPageTab) //constructor
        {
            InitializeComponent();
            this.webBrowser = webBrowser; //construct reference for aurora web browser
            this.startPage = startPage; this.startPageTab = startPageTab; //construct reference for start page tab

            if (Properties.Settings.Default.auroraCustom) //custom aurora handler - if it's custom then load the page in the preview
                webBrowser1.Navigate(Properties.Settings.Default.auroraCustomURL);
     
            check(); //check method
        }

        public Settings()
        {
            InitializeComponent();
            reloadAurora(webBrowser); //reload the web browser navigation each time the form starts

            
            check(); //check again
        }

        public void check()
        {
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

            if (Properties.Settings.Default.auroraStartPage) //verify if the aurora is activated or not
                checkBox1.Checked = true;
            else
                checkBox1.Checked = false;

            //if (webBrowser.Visible)
            //    checkBox1.Checked = true;
            //else
             //   checkBox1.Checked = false;

            if (Properties.Settings.Default.auroraCustom) //verify if there is a custom aurora or not
                checkBox2.Checked = true;
            else
                checkBox2.Checked = false;

            auroraCustomCheck(); //enable and disable UI elements given the fact it is enabled a custom aurora or not

            textBox1.Text = Properties.Settings.Default.auroraCustomURL; //load the custom aurora url

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
            }
            else
            {
                checkBox4.Checked = false;
                checkBox6.Checked = false;
            }

                if (checkBox4.Checked) //check if the two checkboxes have the same value
                    checkBox6.Checked = true;
                else
                    checkBox6.Checked = false;


            if (Properties.Settings.Default.untitledFileStartup) //Verify if creating a new untitled file at startup is enabled or not.
                checkBox5.Checked = true;
            else
                checkBox5.Checked = false;

            

            
        }


        private void Settings_Load(object sender, EventArgs e)
        {
            auroraHeight = 285; //set the default height for settings form in Aurora tab

            //load start page preview text
            label28.Text = Properties.Resources.AppName + " " + Properties.Resources.AppEdition + " " + Properties.Resources.AppVersion;
            label16.Text = Properties.Resources.AppName; label15.Text = Properties.Resources.AppEdition + " Edition"; label14.Text = Properties.Resources.AppDescription;
            //label20.Text = Properties.Settings.Default.startPageStartup.ToString(); //DEBUG - load the property value at runtime

            loadHighlightPrologColors(); //load syntax colors
            loadFontStyleProlog(); //load font styles syntax
            
            
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
            }
            else
            {
                label8.Text = "The Aurora effect is disabled. Any change will be applied next time you enable it."; //aurora is disabled: tell the user about it
                webBrowser.Hide();
                Properties.Settings.Default.auroraStartPage = false;
                Properties.Settings.Default.Save();
            }
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
                label7.Enabled = false;
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
                label7.Enabled = true;
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
            Properties.Settings.Default.Save(); //fallback method - always save
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (tabControl1.SelectedIndex == 3)
            {
                Settings.ActiveForm.Height = 506; //change the height to be the one for the start page appearance
            }
            else
            {
                Settings.ActiveForm.Height = 285; //change the height to be the one for the aurora appearance
            }
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
                startPage.AddTab(startPageTab); //add it as a tab in mdiparent
                Properties.Settings.Default.startPageVisible = true; //set the property
                Properties.Settings.Default.Save();
            }
            else
            {
                startPage.RemoveTab(startPageTab); //else remove the page in mdiparent
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
            if (checkBox5.Checked) //save if you want an untitled file on startup or not
                Properties.Settings.Default.untitledFileStartup = true;
            else
                Properties.Settings.Default.untitledFileStartup = false;
            Properties.Settings.Default.Save();
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            //label20.Text = Properties.Settings.Default.startPageStartup.ToString(); //DEBUG - verify the value at runtime
            if (checkBox6.Checked) //if open at startup is true
            {
                Properties.Settings.Default.startPageStartup = true; //set the property
                Properties.Settings.Default.Save();
                checkBox4.Checked = true;
            }
            else
            {
                Properties.Settings.Default.startPageStartup = false; //set the property
                Properties.Settings.Default.Save();
                checkBox4.Checked = false;
            }
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 3 && tabControl2.SelectedIndex == 0)
            {
                Settings.ActiveForm.Height = 506; //change the height to be the one for the start page appearance
                Settings.ActiveForm.Width = 606;
            }
            else if (tabControl2.SelectedIndex == 1 && tabControl3.SelectedIndex == 1)
            {
                Settings.ActiveForm.Width = 535;
                Settings.ActiveForm.Height = 479;
            }
            else
            {
                Settings.ActiveForm.Height = 285; //change the height to be the one for the aurora appearance
                Settings.ActiveForm.Width = 606;
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
            button22.BackColor = Properties.Settings.Default.keywordBackColor;

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

