using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace PrologParsec
{
    public partial class xmlDetails : Form
    {

        string xmlFilePath = Properties.Settings.Default.descriptionFileDirectory;
        XmlDocument xmlDoc = new XmlDocument();

        public xmlDetails()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        
        private void xmlDetails_Load(object sender, EventArgs e)
        {
            string xmlFilePath = Properties.Settings.Default.descriptionFileDirectory;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);

            XmlNodeList styleNodes = xmlDoc.SelectNodes("/doc");

            foreach (XmlNode styleNode in styleNodes)
            {
                textBox1.AppendText(styleNode.InnerXml + Environment.NewLine);
            }
        }

      
    }
}