namespace PrologParsec
{
    partial class xmlDetails
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(xmlDetails));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.btZoom = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox1 = new FastColoredTextBoxNS.FastColoredTextBox();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.btZoom});
            this.statusStrip.Location = new System.Drawing.Point(0, 485);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(682, 22);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(161, 17);
            this.toolStripStatusLabel.Text = "Current XML Description file";
            // 
            // btZoom
            // 
            this.btZoom.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btZoom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btZoom.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem11,
            this.toolStripMenuItem10,
            this.toolStripMenuItem9,
            this.toolStripMenuItem8,
            this.toolStripMenuItem7,
            this.toolStripMenuItem6});
            this.btZoom.Image = ((System.Drawing.Image)(resources.GetObject("btZoom.Image")));
            this.btZoom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btZoom.Name = "btZoom";
            this.btZoom.Size = new System.Drawing.Size(56, 20);
            this.btZoom.Text = "Zoom";
            this.btZoom.ButtonClick += new System.EventHandler(this.btZoom_ButtonClick);
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(107, 22);
            this.toolStripMenuItem11.Tag = "300";
            this.toolStripMenuItem11.Text = "300%";
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(107, 22);
            this.toolStripMenuItem10.Tag = "200";
            this.toolStripMenuItem10.Text = "200%";
            this.toolStripMenuItem10.Click += new System.EventHandler(this.Zoom_click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(107, 22);
            this.toolStripMenuItem9.Tag = "150";
            this.toolStripMenuItem9.Text = "150%";
            this.toolStripMenuItem9.Click += new System.EventHandler(this.Zoom_click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(107, 22);
            this.toolStripMenuItem8.Tag = "100";
            this.toolStripMenuItem8.Text = "100%";
            this.toolStripMenuItem8.Click += new System.EventHandler(this.Zoom_click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(107, 22);
            this.toolStripMenuItem7.Tag = "50";
            this.toolStripMenuItem7.Text = "50%";
            this.toolStripMenuItem7.Click += new System.EventHandler(this.Zoom_click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(107, 22);
            this.toolStripMenuItem6.Tag = "25";
            this.toolStripMenuItem6.Text = "25%";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.Zoom_click);
            // 
            // textBox1
            // 
            this.textBox1.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.textBox1.AutoScrollMinSize = new System.Drawing.Size(0, 15);
            this.textBox1.BackBrush = null;
            this.textBox1.BackColor = System.Drawing.SystemColors.Window;
            this.textBox1.BookmarkColor = System.Drawing.SystemColors.Highlight;
            this.textBox1.CharHeight = 15;
            this.textBox1.CharWidth = 8;
            this.textBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox1.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.FoldingIndicatorColor = System.Drawing.SystemColors.Highlight;
            this.textBox1.Font = new System.Drawing.Font("ProggyCleanTT", 14.25F);
            this.textBox1.IndentBackColor = System.Drawing.SystemColors.WindowFrame;
            this.textBox1.IsReplaceMode = false;
            this.textBox1.Language = FastColoredTextBoxNS.Language.XML;
            this.textBox1.LineNumberColor = System.Drawing.SystemColors.Highlight;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.Paddings = new System.Windows.Forms.Padding(0);
            this.textBox1.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(150)))), ((int)(((byte)(108)))), ((int)(((byte)(61)))));
            this.textBox1.ServiceLinesColor = System.Drawing.SystemColors.WindowFrame;
            this.textBox1.Size = new System.Drawing.Size(682, 485);
            this.textBox1.TabIndex = 5;
            this.textBox1.Text = "fastColoredTextBox1";
            this.textBox1.TextAreaBorderColor = System.Drawing.SystemColors.Window;
            this.textBox1.WordWrap = true;
            this.textBox1.Zoom = 100;
            // 
            // xmlDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 507);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "xmlDetails";
            this.Text = "XML Configuration Editor";
            this.Load += new System.EventHandler(this.xmlDetails_Load);
            this.Leave += new System.EventHandler(this.xmlDetails_Leave);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.xmlDetails_FormClosing);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripSplitButton btZoom;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem11;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private FastColoredTextBoxNS.FastColoredTextBox textBox1;
    }
}