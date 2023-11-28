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
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Windows;
using System.Threading;

namespace PrologParsec
{
    public partial class LogixPE : Form
    {
        private bool Windows9x; private bool resize9x;

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

        public LogixPE()
        {
            InitializeComponent();
            label15.Text = Properties.Resources.PEName;
            label14.Text = Properties.Resources.PEEdition + " Edition";
            label9.Text = "Version " + Properties.Resources.PEVersion;
            label13.Text = Properties.Resources.PEDescription;
            label16.Text = Properties.Resources.PEDescription2;
            tabChange();

            string curDir = Directory.GetCurrentDirectory(); //current app directory
            if (!Properties.Settings.Default.auroraCustom) //if custom aurora is disabled
            {
                this.webBrowser1.Url = new Uri(String.Format("file:///{0}/aurora/" + Properties.Settings.Default.auroraFile, curDir)); //show the aurora preview in settings

            }
            else
            {
                webBrowser1.Navigate(Properties.Settings.Default.auroraCustomURL);

            }
        }

        public void Windows9xMode()
        {
            Version version = NtDll.RtlGetVersion();



            if (version.Major == 5 || version.Major == 4)
            {
                resize9x = true; Windows9x = true;
            }
        }

        private void tabChange()
        {
            int iformwidth; int iformheight;
            if (!generateFinished)
                antoniotti80Panel.Visible = false;
            //check();

            if (tabControl1.SelectedIndex == 0)
            {
                iformwidth = 419; //generate
                iformheight = 335;
            }
            else if (tabControl1.SelectedIndex == 2)
            {
                iformwidth = 475; //settings
                iformheight = 675;
            }
            else if (tabControl1.SelectedIndex == 3)
            {
                iformwidth = 475; //about
                iformheight = 310;
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                iformwidth = 465; //output
                iformheight = 282;
            }
            else
            {
                iformwidth = 419;
                iformheight = 335;
            }

            if (tabControl1.SelectedIndex == 1)
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.MaximizeBox = true;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                this.MaximizeBox = false;
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

        public static void OpenForm()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MDIParent1());
        }

        private void LogixPE_Load(object sender, EventArgs e)
        {
            check();

        }

        private void check()
        {
            textBox1.Text = Properties.Settings.Default.gramlexLexical;
            textBox5.Text = Properties.Settings.Default.gramlexParser;
            textBox6.Text = Properties.Settings.Default.gramlexOutput;
            textBox12.Text = Properties.Settings.Default.jFlexPath;
            textBox2.Text = Properties.Settings.Default.bYaccPath;
            checkBox9.Checked = Properties.Settings.Default.gramlexJavaRun;
            checkBox15.Checked = Properties.Settings.Default.gramlexSkeleton;
            checkBox1.Checked = Properties.Settings.Default.gramlexEncoding;
            checkBox2.Checked = Properties.Settings.Default.gramlexDefaultCode;
            checkBox3.Checked = Properties.Settings.Default.gramlexJLEX;
            checkBox4.Checked = Properties.Settings.Default.gramlexMinimization;
            checkBox5.Checked = Properties.Settings.Default.gramlexLegacyDot;
            checkBox6.Checked = Properties.Settings.Default.gramlexBackup;
            checkBox7.Checked = Properties.Settings.Default.gramlexTransition;
            checkBox8.Checked = Properties.Settings.Default.gramlexAutomata;
            textBox3.Text = Properties.Settings.Default.gramlexSkeletonFile;
            textBox4.Text = Properties.Settings.Default.gramlexEncodingCustom;
            richTextBox1.Font = Properties.Settings.Default.defaultFont;
            checkBox22.Checked = Properties.Settings.Default.gramlexExclusive;
            textBox7.Text = Properties.Settings.Default.byaccPathName;
        }

        private void applyCheck()
        {
            Properties.Settings.Default.gramlexLexical = textBox1.Text;
            Properties.Settings.Default.gramlexParser = textBox5.Text;
            Properties.Settings.Default.gramlexOutput = textBox6.Text;
            Properties.Settings.Default.jFlexPath = textBox12.Text;
            Properties.Settings.Default.bYaccPath = textBox2.Text;
            Properties.Settings.Default.gramlexJavaRun = checkBox9.Checked;
            Properties.Settings.Default.gramlexSkeleton = checkBox9.Checked;
            Properties.Settings.Default.gramlexEncoding = checkBox1.Checked;
            Properties.Settings.Default.gramlexDefaultCode = checkBox2.Checked;
            Properties.Settings.Default.gramlexJLEX = checkBox3.Checked;
            Properties.Settings.Default.gramlexMinimization = checkBox4.Checked;
            Properties.Settings.Default.gramlexLegacyDot = checkBox5.Checked;
            Properties.Settings.Default.gramlexBackup = checkBox6.Checked;
            Properties.Settings.Default.gramlexTransition = checkBox7.Checked;
            Properties.Settings.Default.gramlexAutomata = checkBox8.Checked;
            Properties.Settings.Default.gramlexSkeletonFile = textBox3.Text;
            Properties.Settings.Default.gramlexEncodingCustom = textBox4.Text;
            Properties.Settings.Default.byaccPathName = textBox7.Text;
            Properties.Settings.Default.Save();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabChange();
        }

        private void LogixPE_VisibleChanged(object sender, EventArgs e)
        {
            applyCheck();
        }

        private void LogixPE_FormClosing(object sender, FormClosingEventArgs e)
        {
            applyCheck(); help.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            try
            {

                //go to line manager
                if (System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, "^(?:[a-zA-Z]:|[\\/]{2}[^\\/]+[\\/]+[^\\/]+|[\\/]+[^\\/]+)?(?:[\\/][^\\/]+)*[\\/]?$"))
                {
                    //roundedPanel1.Height = 56;
                    ToolTip hint = new ToolTip();
                    hint.IsBalloon = true;
                    hint.ToolTipTitle = "Please enter a valid file path";
                    hint.ToolTipIcon = ToolTipIcon.Error;
                    hint.Show(string.Empty, textBox1, -10, -10, 0);
                    hint.Show("You have entered an incorrect file path. File paths are usually written like \x22C:\\Windows\\jlexer.l\x22", textBox1);
                    //label15.Text = "Please enter only numbers";
                    //label15.Left = (this.roundedPanel1.Width - label15.Width) / 2;

                    //check if the number of lines inserted is right
                }
                else
                {
                    Properties.Settings.Default.gramlexLexical = textBox1.Text;
                    Properties.Settings.Default.Save();
                }



            }
            catch (Exception)
            {

                ToolTip hint = new ToolTip();
                hint.IsBalloon = true;
                hint.ToolTipTitle = "Please enter a valid file path";
                hint.ToolTipIcon = ToolTipIcon.Error;
                hint.Show(string.Empty, textBox1, 0);
                hint.Show("You have entered an incorrect file path. File paths are usually written like \x22C:\\Windows\\lexer.l\x22", textBox1);


            }
            
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {

                //go to line manager
                if (System.Text.RegularExpressions.Regex.IsMatch(textBox5.Text, "^(?:[a-zA-Z]:|[\\/]{2}[^\\/]+[\\/]+[^\\/]+|[\\/]+[^\\/]+)?(?:[\\/][^\\/]+)*[\\/]?$"))
                {
                    //roundedPanel1.Height = 56;
                    ToolTip hint = new ToolTip();
                    hint.IsBalloon = true;
                    hint.ToolTipTitle = "Please enter a valid file path";
                    hint.ToolTipIcon = ToolTipIcon.Error;
                    hint.Show(string.Empty, textBox5, -10, -10, 0);
                    hint.Show("You have entered an incorrect file path. File paths are usually written like \x22C:\\Windows\\jlexer.l\x22", textBox5);
                    //label15.Text = "Please enter only numbers";
                    //label15.Left = (this.roundedPanel1.Width - label15.Width) / 2;

                    //check if the number of lines inserted is right
                }
                else
                {
                    Properties.Settings.Default.gramlexParser = textBox5.Text;
                    Properties.Settings.Default.Save();
                }



            }
            catch (Exception)
            {

                ToolTip hint = new ToolTip();
                hint.IsBalloon = true;
                hint.ToolTipTitle = "Please enter a valid file path";
                hint.ToolTipIcon = ToolTipIcon.Error;
                hint.Show(string.Empty, textBox5, 0);
                hint.Show("You have entered an incorrect file path. File paths are usually written like \x22C:\\Windows\\lexer.l\x22", textBox5);


            }
            
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.gramlexOutput = textBox6.Text;
            Properties.Settings.Default.Save();
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            try
            {

                //go to line manager
                if (System.Text.RegularExpressions.Regex.IsMatch(textBox12.Text, "^(?:[a-zA-Z]:|[\\/]{2}[^\\/]+[\\/]+[^\\/]+|[\\/]+[^\\/]+)?(?:[\\/][^\\/]+)*[\\/]?$"))
                {
                    //roundedPanel1.Height = 56;
                    ToolTip hint = new ToolTip();
                    hint.IsBalloon = true;
                    hint.ToolTipTitle = "Please enter a valid file path";
                    hint.ToolTipIcon = ToolTipIcon.Error;
                    hint.Show(string.Empty, textBox12, -10, -10, 0);
                    hint.Show("You have entered an incorrect file path. File paths are usually written like \x22C:\\Windows\\jflex.bat\x22", textBox12);
                    //label15.Text = "Please enter only numbers";
                    //label15.Left = (this.roundedPanel1.Width - label15.Width) / 2;

                    //check if the number of lines inserted is right
                }
                else {
                    Properties.Settings.Default.jFlexPath = textBox12.Text;
                    Properties.Settings.Default.Save();
                }



            }
            catch (Exception)
            {

                ToolTip hint = new ToolTip();
                hint.IsBalloon = true;
                hint.ToolTipTitle = "Please enter a valid file path";
                hint.ToolTipIcon = ToolTipIcon.Error;
                hint.Show(string.Empty, textBox12, 0);
                hint.Show("You have entered an incorrect file path. File paths are usually written like \x22C:\\Windows\\jflex.bat\x22", textBox12);


            }
        
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {

                //go to line manager
                if (System.Text.RegularExpressions.Regex.IsMatch(textBox2.Text, "^(?:[a-zA-Z]:|[\\/]{2}[^\\/]+[\\/]+[^\\/]+|[\\/]+[^\\/]+)?(?:[\\/][^\\/]+)*[\\/]?$"))
                {
                    //roundedPanel1.Height = 56;
                    ToolTip hint = new ToolTip();
                    hint.IsBalloon = true;
                    hint.ToolTipTitle = "Please enter a valid file path";
                    hint.ToolTipIcon = ToolTipIcon.Error;
                    hint.Show(string.Empty, textBox2, -10, -10, 0);
                    hint.Show("You have entered an incorrect file path. File paths are usually written like \x22C:\\Windows\\jflex.bat\x22", textBox2);
                    //label15.Text = "Please enter only numbers";
                    //label15.Left = (this.roundedPanel1.Width - label15.Width) / 2;

                    //check if the number of lines inserted is right
                }
                else
                {
                    Properties.Settings.Default.bYaccPath = textBox2.Text;
                    Properties.Settings.Default.Save();
                }


            }
            catch (Exception)
            {

                ToolTip hint = new ToolTip();
                hint.IsBalloon = true;
                hint.ToolTipTitle = "Please enter a valid file path";
                hint.ToolTipIcon = ToolTipIcon.Error;
                hint.Show(string.Empty, textBox2, 0);
                hint.Show("You have entered an incorrect file path. File paths are usually written like \x22C:\\Windows\\jflex.bat\x22", textBox2);


            }
            
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.gramlexJavaRun = checkBox9.Checked;
            Properties.Settings.Default.Save();
        }

        private void checkBox15_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.gramlexSkeleton = checkBox15.Checked;
            Properties.Settings.Default.Save();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.gramlexSkeletonFile = textBox3.Text;
            Properties.Settings.Default.Save();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.gramlexEncoding = checkBox1.Checked;
            Properties.Settings.Default.Save();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.gramlexDefaultCode = checkBox2.Checked;
            Properties.Settings.Default.Save();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.gramlexJLEX = checkBox3.Checked;
            Properties.Settings.Default.Save();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.gramlexMinimization = checkBox4.Checked;
            Properties.Settings.Default.Save();
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.gramlexLegacyDot = checkBox5.Checked;
            Properties.Settings.Default.Save();
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.gramlexBackup = checkBox6.Checked;
            Properties.Settings.Default.Save();
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.gramlexTransition = checkBox7.Checked;
            Properties.Settings.Default.Save();
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.gramlexAutomata = checkBox8.Checked;
            Properties.Settings.Default.Save();
        }

        private void button33_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog(); //create a new open dialog
            openFileDialog.Filter = "Windows batch files (*.bat)|*.bat|Java Executable files (*.jar)|*.jar|Executable files (*.exe)|*.exe|All files (*.*)|*.*"; //set up file extensions
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
                    textBox12.Text = filePath; //put the window title
                    Properties.Settings.Default.jFlexPath = textBox12.Text;
                    Properties.Settings.Default.Save();
                }
                catch (NullReferenceException)
                {

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Select the folder where ByaccJ is located";
                dlg.SelectedPath = Properties.Settings.Default.gramlexOutput;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    textBox2.Text = dlg.SelectedPath;
                    Properties.Settings.Default.bYaccPath = textBox2.Text;
                    Properties.Settings.Default.Save();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog(); //create a new open dialog
            openFileDialog.Filter = "Executable files (*.exe)|*.exe|All files (*.*)|*.*"; //set up file extensions
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
                    textBox3.Text = filePath; //put the window title
                    Properties.Settings.Default.gramlexSkeletonFile = textBox3.Text;
                    Properties.Settings.Default.Save();
                }
                catch (NullReferenceException)
                {

                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog(); //create a new open dialog
            openFileDialog.Filter = "JFlex lexical specifications (*.l)|*.l|All files (*.*)|*.*"; //set up file extensions
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
                    Properties.Settings.Default.gramlexLexical = textBox1.Text;
                    Properties.Settings.Default.Save();
                }
                catch (NullReferenceException)
                {

                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog(); //create a new open dialog
            openFileDialog.Filter = "ByaccJ Parser files (*.y)|*.y|All files (*.*)|*.*"; //set up file extensions
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
                    textBox5.Text = filePath; //put the window title
                    Properties.Settings.Default.gramlexParser = textBox5.Text;
                    Properties.Settings.Default.Save();
                }
                catch (NullReferenceException)
                {

                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Select a folder";
                dlg.SelectedPath = Properties.Settings.Default.gramlexOutput;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    textBox6.Text = dlg.SelectedPath;
                    Properties.Settings.Default.gramlexOutput = textBox6.Text;
                    Properties.Settings.Default.Save();
                }
            }
        }

        private void centerPanel(Panel panel)
        {
            // Calcola la posizione orizzontale per centrare il pannello
            int centerX = (this.ClientSize.Width - panel.Width) / 2;

            // Imposta la posizione del pannello
            if (Properties.Settings.Default.animationDockBottomEnable && Properties.Settings.Default.animationDockBottom > 0)
            {
                Transition t = new Transition(new TransitionType_EaseInEaseOut((int)Properties.Settings.Default.animationDockBottom));
                t.add(panel, "Left", centerX);
                t.add(panel, "Top", panel.Location.Y);
                t.run();
            }
            else
            {
                panel.Location = new Point(centerX, panel.Location.Y);
            }
        }

        private bool generateFinished;
        private void button5_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(Properties.Settings.Default.jFlexPath + " " + jflexArguments + " " + Properties.Settings.Default.gramlexLexical);
            //Process.Start(Properties.Settings.Default.jFlexPath, String.Format(Properties.Settings.Default.gramlexJFlexArguments + " " + Properties.Settings.Default.gramlexLexical));
            //Process.Start(Properties.Settings.Default.bYaccPath, String.Format(Properties.Settings.Default.gramlexByaccArguments + " " + Properties.Settings.Default.gramlexParser));
            generate();
        }

        private bool runCmd = true;

        public void generate()
        {
            antoniotti80Panel.Visible = false;
            generateFinished = false;
            richTextBox1.Clear();
            StartCmdProcess("java -Xmx128m -jar \x22" + Properties.Settings.Default.jFlexPath + "\x22 " + Properties.Settings.Default.gramlexJFlexArguments + " \x22" + Properties.Settings.Default.gramlexLexical + "\x22", richTextBox1);
            StartCmdProcess("cd " + Properties.Settings.Default.bYaccPath + " && " + Properties.Settings.Default.byaccPathName + " -J" + Properties.Settings.Default.gramlexByaccArguments + " " + Properties.Settings.Default.gramlexParser, richTextBox1);
            StartCmdProcess("cd " + Properties.Settings.Default.gramlexOutput + " && " + " del Parser.java " + " && " + " del ParserVal.java", richTextBox1);
            StartCmdProcess(" cd " + Properties.Settings.Default.bYaccPath + " && " + "move " + Properties.Settings.Default.bYaccPath + "\\parser.java " + Properties.Settings.Default.gramlexOutput + " && " + "move " + Properties.Settings.Default.bYaccPath + "\\ParserVal.java " + Properties.Settings.Default.gramlexOutput, richTextBox1);
            if (Properties.Settings.Default.gramlexJavaRun)
                StartCmdProcess(" cd " + Properties.Settings.Default.gramlexOutput + " && " + "javac Parser.java", richTextBox1);
            centerPanel(antoniotti80Panel);
            antoniotti80Panel.Visible = true;
            generateFinished = true;
           Clipboard.SetText("java Parser");
           if (runCmd)
           {
               Process processcmd = startCmd(Properties.Settings.Default.gramlexOutput);
           }
            runCmd = false;
           // sendCmd(processcmd, "java Parser");
        }

        private Process startCmd(string directoryPath)
        {
            ProcessStartInfo psi = new ProcessStartInfo("cmd.exe");
            psi.WorkingDirectory = directoryPath;

            psi.UseShellExecute = false;
            Process processCmd = new Process();
            processCmd.StartInfo = psi;

            processCmd.Start();

            if (processCmd.StartInfo.RedirectStandardInput)
            {
                processCmd.StandardInput.AutoFlush = true;
            }
            return processCmd;
        }

        private void sendCmd(Process cmdprocess, string command)
        {
            if (cmdprocess != null && !cmdprocess.HasExited)
            {
                cmdprocess.StandardInput.WriteLine(command);
            }
        }

        static void StartCmdProcess(string command, RichTextBox label)
        {
            ProcessStartInfo cmdProcessInfo = new ProcessStartInfo();
            cmdProcessInfo.FileName = "CMD.exe";
            cmdProcessInfo.RedirectStandardInput = true;
            cmdProcessInfo.RedirectStandardOutput = true;
            cmdProcessInfo.RedirectStandardError = true;
            cmdProcessInfo.UseShellExecute = false;
            cmdProcessInfo.CreateNoWindow = true;


            using (Process cmdProcess = new Process())
            {
                cmdProcess.StartInfo = cmdProcessInfo;
                cmdProcess.Start();

                // Scrivi il comando nel flusso di input standard del processo CMD
                cmdProcess.StandardInput.WriteLine(command);
                cmdProcess.StandardInput.Close(); // Chiudi il flusso di input standard

                // Leggi l'output e gli errori
                string output = cmdProcess.StandardOutput.ReadToEnd();
                string errors = cmdProcess.StandardError.ReadToEnd();

                // Attendere il completamento del processo CMD
                cmdProcess.WaitForExit();

                // Ora p uoi gestire l'output, gli errori o altre operazioni post-processo
                label.Text = label.Text + ("==========Command:==========\n" + command + "\n");
                label.Text = label.Text + ("==========Output:==========\n" + output + "\n");
                //label.Text = label.Text + ("Errors:\n" + errors + "\n");
            }
        }

        private void checkBox22_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox22.Checked)
                Properties.Settings.Default.gramlexExclusive = true;
            else
                Properties.Settings.Default.gramlexExclusive = false;
            Properties.Settings.Default.Save();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.byaccPathName = textBox7.Text;
            Properties.Settings.Default.Save();
        }

        private void textBox12_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(OpenForm);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Process processcmd = startCmd(Properties.Settings.Default.gramlexOutput);
        }
        LogixPEHelp help = new LogixPEHelp(); 
        private void button7_Click(object sender, EventArgs e)
        {
            help.Show();
        }

        
    }
    public static class NtDll
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct OSVERSIONINFOEX
        {
            public uint dwOSVersionInfoSize;
            public uint dwMajorVersion;
            public uint dwMinorVersion;
            public uint dwBuildNumber;
            public uint dwPlatformId;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCSDVersion;
            public ushort wServicePackMajor;
            public ushort wServicePackMinor;
            public ushort wSuiteMask;
            public byte wProductType;
            public byte wReserved;
        }

        public static class NativeMethods
        {
            [DllImport("ntdll.dll", CharSet = CharSet.Unicode)]
            public static extern int RtlGetVersion([In, Out] ref OSVERSIONINFOEX version);
        }

        public static Version RtlGetVersion()
        {
            NtDll.OSVERSIONINFOEX v = default(OSVERSIONINFOEX);
            v.dwOSVersionInfoSize = (uint)Marshal.SizeOf(typeof(OSVERSIONINFOEX));
            if (NativeMethods.RtlGetVersion(ref v) == 0)
            {
                return new Version((int)v.dwMajorVersion, (int)v.dwMinorVersion, (int)v.dwBuildNumber, 0);
            }
            // didn't work ???
            return default(Version);//Environment.OSVersion.Version;
        }
    }
}
