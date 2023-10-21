using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;
using System.Xml;
using AutocompleteMenuNS;
using System.Text.RegularExpressions;
using System.Reflection;
using Microsoft.Win32;
using System.Drawing.Drawing2D;

namespace PrologParsec
{
    public partial class xmlDetails : Form
    {

        string xmlFilePath = Properties.Settings.Default.descriptionFileDirectory;
        XmlDocument xmlDoc = new XmlDocument();

        public xmlDetails()
        {
            InitializeComponent();
            //textBox1.DescriptionFile = Properties.Settings.Default.xmlFile;
            textBox1.Invalidate();
            textBox1.Refresh();
        }


        
        private void xmlDetails_Load(object sender, EventArgs e)
        {
            string xmlFilePath = Properties.Settings.Default.descriptionFileDirectory;
            if (File.Exists(xmlFilePath))
            {
                string xmlContent = File.ReadAllText(xmlFilePath);
                textBox1.Text = xmlContent;
            }

            toolStripStatusLabel.Text = "Current XML description file - " + xmlFilePath;
          
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void xmlDetails_Leave(object sender, EventArgs e)
        {
            textBox1.SaveToFile(xmlFilePath, Encoding.Unicode);
        }

        private void xmlDetails_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (File.Exists(Properties.Settings.Default.descriptionFileDirectory))
            {
                // Salva il contenuto del TextBox nel file XML
                File.WriteAllText(Properties.Settings.Default.descriptionFileDirectory, textBox1.Text);
            }
        }

        private void btZoom_ButtonClick(object sender, EventArgs e)
        {

        }

        private void Zoom_click(object sender, EventArgs e)
        {
            if (textBox1 != null)
                textBox1.Zoom = int.Parse((sender as ToolStripItem).Tag.ToString());
        }
      
    }
}