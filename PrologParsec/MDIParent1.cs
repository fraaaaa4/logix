using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;
using System.Globalization;
using System.Xml;
using AutocompleteMenuNS;
using System.Text.RegularExpressions;
using System.Reflection;
using Microsoft.Win32;
using System.Drawing.Drawing2D;
using System.Threading;
using Transitions;
using System.Diagnostics;


namespace PrologParsec
{
    public partial class MDIParent1 : Form
    {
        //prolog styles
        FastColoredTextBoxNS.Style keyword = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.keywordForeColor), new SolidBrush(Properties.Settings.Default.keywordBackColor), Properties.Settings.Default.keywordFontStyle);
        FastColoredTextBoxNS.Style comma = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.commaForeColor), new SolidBrush(Properties.Settings.Default.commaBackColor), Properties.Settings.Default.commaFontStyle);
        FastColoredTextBoxNS.Style point = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.pointForeColor), new SolidBrush(Properties.Settings.Default.pointBackColor), Properties.Settings.Default.pointFontStyle);
        FastColoredTextBoxNS.Style question = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.questionForeColor), new SolidBrush(Properties.Settings.Default.questionBackColor), Properties.Settings.Default.questionFontStyle);
        FastColoredTextBoxNS.Style systemPar = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.systemParForeColor), new SolidBrush(Properties.Settings.Default.systemParBackColor), Properties.Settings.Default.systemParFontStyle);
        FastColoredTextBoxNS.Style equals = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.equalsForeColor), new SolidBrush(Properties.Settings.Default.equalsBackColor), Properties.Settings.Default.equalsFontStyle);
        FastColoredTextBoxNS.Style anon = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.anonForeColor), new SolidBrush(Properties.Settings.Default.anonBackColor), Properties.Settings.Default.anonFontStyle);
        FastColoredTextBoxNS.Style comment = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.commentForeColor), new SolidBrush(Properties.Settings.Default.commentBackColor), Properties.Settings.Default.commentFontStyle);
        FastColoredTextBoxNS.Style parentesi = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.parentesiForeColor), new SolidBrush(Properties.Settings.Default.parentesiBackColor), Properties.Settings.Default.parentesiFontStyle);
        FastColoredTextBoxNS.Style anoni = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.anoniForeColor), new SolidBrush(Properties.Settings.Default.anoniBackColor), Properties.Settings.Default.anoniFontStyle);
        FastColoredTextBoxNS.Style variables = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.variablesForeColor), new SolidBrush(Properties.Settings.Default.variablesBackColor), Properties.Settings.Default.variablesFontStyle);
        FastColoredTextBoxNS.Style functionsa = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.functionsForeColor), new SolidBrush(Properties.Settings.Default.functionsBackColor), Properties.Settings.Default.functionsFontStyle);

        //lisp styles
        FastColoredTextBoxNS.Style lispComment = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.lispCommentForeColor), new SolidBrush(Properties.Settings.Default.lispCommentBackColor), Properties.Settings.Default.lispCommentFontStyle);
        FastColoredTextBoxNS.Style lispVariable = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.lispVariablesForeColor), new SolidBrush(Properties.Settings.Default.lispVariablesBackColor), Properties.Settings.Default.lispVariablesFontStyle);
        FastColoredTextBoxNS.Style lispBrackets = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.lispBracketsForeColor), new SolidBrush(Properties.Settings.Default.lispBracketsBackColor), Properties.Settings.Default.lispBracketsFontStyle);
        FastColoredTextBoxNS.Style lispKeyword = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.lispKeywordForeColor), new SolidBrush(Properties.Settings.Default.lispKeywordBackColor), Properties.Settings.Default.lispKeywordFontStyle);
        FastColoredTextBoxNS.Style lispOperator = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.lispOperatorForeColor), new SolidBrush(Properties.Settings.Default.lispOperatorBackColor), Properties.Settings.Default.lispOperatorFontStyle);
        FastColoredTextBoxNS.Style lispSpecialChar = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.lispSpecialCharForeColor), new SolidBrush(Properties.Settings.Default.lispSpecialCharBackColor), Properties.Settings.Default.lispSpecialCharFontStyle);
        FastColoredTextBoxNS.Style lispString = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.lispStringForeColor), new SolidBrush(Properties.Settings.Default.lispStringBackColor), Properties.Settings.Default.lispStringFontStyle);
        FastColoredTextBoxNS.Style lispNumber = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.lispNumberForeColor), new SolidBrush(Properties.Settings.Default.lispNumberBackColor), Properties.Settings.Default.lispNumberFontStyle);

        //yacc styles
        FastColoredTextBoxNS.Style yaccSection = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.yaccSectionForeColor), new SolidBrush(Properties.Settings.Default.yaccSectionBackColor), Properties.Settings.Default.yaccSectionFontStyle);
        FastColoredTextBoxNS.Style yaccBrackets = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.yaccBracketsForeColor), new SolidBrush(Properties.Settings.Default.yaccBracketsBackColor), Properties.Settings.Default.yaccBracketsFontStyle);
        FastColoredTextBoxNS.Style yaccInclusive = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.yaccInclusiveForeColor), new SolidBrush(Properties.Settings.Default.yaccInclusiveBackColor), Properties.Settings.Default.yaccInclusiveFontStyle);
        FastColoredTextBoxNS.Style yaccJava = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.yaccJavaForeColor), new SolidBrush(Properties.Settings.Default.yaccJavaBackColor), Properties.Settings.Default.yaccJavaFontStyle);
        FastColoredTextBoxNS.Style yaccOperator = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.yaccOperatorForeColor), new SolidBrush(Properties.Settings.Default.yaccOperatorBackColor), Properties.Settings.Default.yaccOperatorFontStyle);
        FastColoredTextBoxNS.Style yaccString = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.yaccStringForeColor), new SolidBrush(Properties.Settings.Default.yaccStringBackColor), Properties.Settings.Default.yaccStringFontStyle);
        FastColoredTextBoxNS.Style yaccNumber = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.yaccNumberForeColor), new SolidBrush(Properties.Settings.Default.yaccNumberBackColor), Properties.Settings.Default.yaccNumberFontStyle);
        FastColoredTextBoxNS.Style yaccToken = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.yaccTokenForeColor), new SolidBrush(Properties.Settings.Default.yaccTokenBackColor), Properties.Settings.Default.yaccTokenFontStyle);
        FastColoredTextBoxNS.Style yaccPerc = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.yaccParForeColor), new SolidBrush(Properties.Settings.Default.yaccParBackColor), Properties.Settings.Default.yaccParFontStyle);

        //jflex
        FastColoredTextBoxNS.Style jflexSection = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.jflexSectionForeColor), new SolidBrush(Properties.Settings.Default.jflexSectionBackColor), Properties.Settings.Default.jflexSectionFontStyle);
        FastColoredTextBoxNS.Style jflexBrackets = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.jflexBracketsForeColor), new SolidBrush(Properties.Settings.Default.jflexBracketsBackColor), Properties.Settings.Default.jflexBracketsFontStyle);
        FastColoredTextBoxNS.Style jflexOperator = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.jflexOperatorForeColor), new SolidBrush(Properties.Settings.Default.jflexOperatorBackColor), Properties.Settings.Default.jflexOperatorFontStyle);
        FastColoredTextBoxNS.Style jflexInclusive = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.jflexInclusiveForeColor), new SolidBrush(Properties.Settings.Default.jflexInclusiveBackColor), Properties.Settings.Default.jflexInclusiveFontStyle);
        FastColoredTextBoxNS.Style jflexComment = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.jflexCommentForeColor), new SolidBrush(Properties.Settings.Default.jflexCommentBackColor), Properties.Settings.Default.jflexCommentFontStyle);
        FastColoredTextBoxNS.Style jflexJava = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.jflexJavaForeColor), new SolidBrush(Properties.Settings.Default.jflexJavaBackColor), Properties.Settings.Default.jflexJavaFontStyle);
        FastColoredTextBoxNS.Style jflexString = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.jflexStringForeColor), new SolidBrush(Properties.Settings.Default.jflexStringBackColor), Properties.Settings.Default.jflexStringFontStyle);

        //C++ styles
        FastColoredTextBoxNS.Style cComment = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.cCommentForeColor), new SolidBrush(Properties.Settings.Default.cCommentBackColor), Properties.Settings.Default.cCommentFontStyle);
        FastColoredTextBoxNS.Style cString = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.cStringForeColor), new SolidBrush(Properties.Settings.Default.cStringBackColor), Properties.Settings.Default.cStringFontStyle);
        FastColoredTextBoxNS.Style cNumber = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.cNumberForeColor), new SolidBrush(Properties.Settings.Default.cNumberBackColor), Properties.Settings.Default.cNumberFontStyle);
        FastColoredTextBoxNS.Style cAttribute = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.cAttributeForeColor), new SolidBrush(Properties.Settings.Default.cAttributeBackColor), Properties.Settings.Default.cAttributeFontStyle);
        FastColoredTextBoxNS.Style cClassName = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.cClassForeColor), new SolidBrush(Properties.Settings.Default.cClassBackColor), Properties.Settings.Default.cClassFontStyle);
        FastColoredTextBoxNS.Style cKeyword = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.cKeywordForeColor), new SolidBrush(Properties.Settings.Default.cKeywordBackColor), Properties.Settings.Default.cKeywordFontStyle);
        FastColoredTextBoxNS.Style cCommentTag = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.cCommentTagForeColor), new SolidBrush(Properties.Settings.Default.cCommentTagBackColor), Properties.Settings.Default.cCommentTagFontStyle);
        FastColoredTextBoxNS.Style cBrackets = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.cBracketsForeColor), new SolidBrush(Properties.Settings.Default.cBracketsBackColor), Properties.Settings.Default.cBracketsFontStyle);
        FastColoredTextBoxNS.Style cOperator = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.cOperatorForeColor), new SolidBrush(Properties.Settings.Default.cOperatorBackColor), Properties.Settings.Default.cOperatorFontStyle);

        //hyperlink
        FastColoredTextBoxNS.Style blueStyle = new FastColoredTextBoxNS.TextStyle(new SolidBrush(Properties.Settings.Default.hyperlinkForeColor), new SolidBrush(Properties.Settings.Default.hyperlinkBackColor), Properties.Settings.Default.hyperlinkFontStyle);
        //toolbar buttons padding
        int leftNew; int rightNew; int leftTab; int rightTab; int leftOpen; int rightOpen; int leftCut; int rightCut; int leftCopy; int rightCopy; int leftUndo; int rightUndo; int leftFont; int rightFont; int leftNav; int rightNav; int leftSave; int rightSave; int leftPaste; int rightPaste; int leftNavNav; int rightNavNav;
        static bool Windows9x = false; private bool resize9x = false;//deactivate aurora and transitions if you're running it on 9x/2000

        private void classicToolbar()
        {
            toolStrip1.Visible = false; toolStrip2.Visible = false; toolStrip3.Visible = true; toolStrip4.Visible = false;
            Properties.Settings.Default.fluentStyle = false; Properties.Settings.Default.lunaStyle = false; Properties.Settings.Default.lunaXPStyle = false;
            Properties.Settings.Default.classicStyle = true; Properties.Settings.Default.ClassicNineStyle = false; Properties.Settings.Default.flat8Style = false;
            Properties.Settings.Default.PantherStyle = false; Properties.Settings.Default.emacsStyle = false;
            menuStrip2.Hide(); menuStrip1.Show(); statusStrip1.Hide(); statusStrip2.Hide(); statusStrip.Show();
            label21.Text = "  "; label21.Image = Properties.Resources.open200032;
            label21.Height = label21.Height + 10; label21.Top = label21.Top - 10;

            label26.Text = "  ";
            label26.Image = Properties.Resources.tab2000;


            label36.Text = "  ";
            label36.Image = Properties.Resources.tab2000;


            label25.Text = "  ";
            label25.Image = Properties.Resources.open2000;


            label35.Text = "  ";
            label35.Image = Properties.Resources.open2000;

            this.Text = Properties.Resources.AppName + " " + Properties.Resources.AppEdition; this.Icon = Properties.Resources.prolog;
            cutToolStripMenuItem1.Image = Properties.Resources.cut2000; copyToolStripMenuItem1.Image = Properties.Resources.copy2000;
            pasteToolStripMenuItem1.Image = Properties.Resources.paste2000; deleteToolStripMenuItem1.Image = Properties.Resources.delete2000;
            selectToolStripMenuItem1.Image = Properties.Resources.select2000; findreplaceToolStripMenuItem.Image = Properties.Resources.find2000;
            searchOnTheInternetToolStripMenuItem.Image = Properties.Resources.link2000; goToToolStripMenuItem1.Image = Properties.Resources.go_to2000;
            bookmarksToolStripMenuItem.Image = Properties.Resources.bookmark2000; addANewTabToolStripMenuItem.Image = Properties.Resources.add2000;
            closeCurrentTabToolStripMenuItem.Image = Properties.Resources.remove2000; renameCurrentTabToolStripMenuItem.Image = Properties.Resources.rename2000;
        }

        private void aeroToolbar()
        {
            toolStrip1.Visible = false; toolStrip2.Visible = true; toolStrip3.Visible = false; toolStrip4.Visible = false;
            Properties.Settings.Default.fluentStyle = false; Properties.Settings.Default.lunaStyle = true; Properties.Settings.Default.lunaXPStyle = false;
            Properties.Settings.Default.classicStyle = false; Properties.Settings.Default.ClassicNineStyle = false; Properties.Settings.Default.flat8Style = false;
            Properties.Settings.Default.PantherStyle = false; Properties.Settings.Default.emacsStyle = false;
            menuStrip2.Hide(); menuStrip1.Show(); statusStrip.Show();
            label21.Text = "  "; label21.Image = Properties.Resources.Open32;
            label21.Size = new Size(label21.Width + 25, label21.Height + 25);
            label21.Location = new Point(label21.Left - 10, label21.Top - 20);


            label26.Text = "  ";
            label26.Image = Properties.Resources.Tab_Sheet_New;


            label36.Text = "  ";
            label36.Image = Properties.Resources.Tab_Sheet_New;


            label25.Text = "  ";
            label25.Image = Properties.Resources.Open;


            label35.Text = "  ";
            label35.Image = Properties.Resources.Open;

            this.Text = Properties.Resources.AppName + " " + Properties.Resources.AppEdition;
            toolStripButton1.Image = Properties.Resources.New; statusStrip1.Hide(); statusStrip2.Hide();
            toolStripButton3.Image = Properties.Resources.Open; toolStripButton4.Image = Properties.Resources.Save;
            toolStripButton5.Image = Properties.Resources.Cut1; toolStripButton6.Image = Properties.Resources.Copy1;
            toolStripButton7.Image = Properties.Resources.Paste1; toolStripButton8.Image = Properties.Resources.Undo1;
            toolStripButton9.Image = Properties.Resources.Redo1; toolStripButton10.Image = Properties.Resources.Font;
            toolStripButton11.Image = Properties.Resources.Back; toolStripButton12.Image = Properties.Resources.next;
            toolStripButton38.Image = Properties.Resources.gramlexAero; this.Icon = Properties.Resources.prolog;

            cutToolStripMenuItem1.Image = Properties.Resources.Cut1; copyToolStripMenuItem1.Image = Properties.Resources.Copy1;
            pasteToolStripMenuItem1.Image = Properties.Resources.Paste1; deleteToolStripMenuItem1.Image = Properties.Resources.deleteB;
            selectToolStripMenuItem1.Image = Properties.Resources.selectB; findreplaceToolStripMenuItem.Image = Properties.Resources.findB;
            searchOnTheInternetToolStripMenuItem.Image = Properties.Resources.linkB; goToToolStripMenuItem1.Image = Properties.Resources.go_toB;
            bookmarksToolStripMenuItem.Image = Properties.Resources.bookmarkB; addANewTabToolStripMenuItem.Image = Properties.Resources.addB;
            closeCurrentTabToolStripMenuItem.Image = Properties.Resources.removeB; renameCurrentTabToolStripMenuItem.Image = Properties.Resources.renameB;
        }

        private void lunaToolbar()
        {
            toolStrip1.Visible = false; toolStrip2.Visible = true; toolStrip3.Visible = false; toolStrip4.Visible = false; //Aero toolbar
            Properties.Settings.Default.fluentStyle = false; Properties.Settings.Default.lunaStyle = false; Properties.Settings.Default.lunaXPStyle = true;
            Properties.Settings.Default.classicStyle = false; Properties.Settings.Default.ClassicNineStyle = false; Properties.Settings.Default.flat8Style = false;
            Properties.Settings.Default.PantherStyle = false; Properties.Settings.Default.emacsStyle = false;
            menuStrip2.Hide(); menuStrip1.Show(); statusStrip.Hide();
            label21.Text = "  "; label21.Image = Properties.Resources.lunaOpen;
            label21.Size = new Size(label21.Width + 25, label21.Height + 25);
            label21.Location = new Point(label21.Left - 10, label21.Top - 20);


            label26.Text = "  "; label26.Image = Properties.Resources.lunaNew;
            label26.Height = label26.Height + 4;

            label36.Text = "  "; label36.Image = Properties.Resources.lunaNew;
            label36.Height = label36.Height + 4;

            label25.Text = "  "; label25.Image = Properties.Resources.lunaOpen;
            label25.Height = label25.Height + 4;

            label35.Text = "  "; label35.Image = Properties.Resources.lunaOpen;
            label35.Height = label35.Height + 4;
            this.Text = Properties.Resources.AppName + " " + Properties.Resources.AppEdition;
            toolStripButton1.Image = Properties.Resources.lunaNew; statusStrip1.Hide(); statusStrip2.Hide();
            toolStripButton3.Image = Properties.Resources.lunaOpen; toolStripButton4.Image = Properties.Resources.lunaSave;
            toolStripButton5.Image = Properties.Resources.lunaCut; toolStripButton6.Image = Properties.Resources.lunaCopy;
            toolStripButton7.Image = Properties.Resources.lunaPaste; toolStripButton8.Image = Properties.Resources.lunaUndo;
            toolStripButton9.Image = Properties.Resources.lunaRedo; toolStripButton10.Image = Properties.Resources.lunaFont;
            toolStripButton11.Image = Properties.Resources.lunaBack; this.Icon = Properties.Resources.prolog;
            toolStripButton12.Image = Properties.Resources.lunaNext; toolStripButton38.Image = Properties.Resources.lunaGramlex;
            cutToolStripMenuItem1.Image = Properties.Resources.lunaCut; copyToolStripMenuItem1.Image = Properties.Resources.lunaCopy; pasteToolStripMenuItem1.Image = Properties.Resources.lunaPaste;
            deleteToolStripMenuItem1.Image = Properties.Resources.lunaDelete1; selectToolStripMenuItem1.Image = Properties.Resources.lunaSelect;
            findreplaceToolStripMenuItem.Image = Properties.Resources.lunaFind; searchOnTheInternetToolStripMenuItem.Image = Properties.Resources.lunaLink;
            goToToolStripMenuItem1.Image = Properties.Resources.lunaGoTo; bookmarksToolStripMenuItem.Image = Properties.Resources.lunaBookMark;
            addANewTabToolStripMenuItem.Image = Properties.Resources.lunaAdd; closeCurrentTabToolStripMenuItem.Image = Properties.Resources.lunaRemove;
        }

        private void metroToolbar()
        {
            toolStrip1.Visible = false; toolStrip2.Visible = true; toolStrip3.Visible = false; toolStrip4.Visible = false;
            Properties.Settings.Default.fluentStyle = false; Properties.Settings.Default.lunaStyle = false; Properties.Settings.Default.lunaXPStyle = false;
            Properties.Settings.Default.classicStyle = false; Properties.Settings.Default.ClassicNineStyle = false; Properties.Settings.Default.flat8Style = true;
            Properties.Settings.Default.PantherStyle = false; Properties.Settings.Default.emacsStyle = false;
            menuStrip2.Hide(); menuStrip1.Show(); statusStrip.Show();
            label21.Text = "  "; label21.Image = Properties.Resources.lc_open;
            label21.Size = new Size(label21.Width + 25, label21.Height + 25);
            label21.Location = new Point(label21.Left - 10, label21.Top - 20);

            label26.Text = "  "; label26.Image = Properties.Resources.sc_newdoc;

            label36.Text = "  "; label36.Image = Properties.Resources.sc_newdoc;

            label25.Text = "  "; label25.Image = Properties.Resources.sc_open;

            label35.Text = "  "; label35.Image = Properties.Resources.sc_open;
            this.Text = Properties.Resources.AppName + " " + Properties.Resources.AppEdition;
            toolStripButton1.Image = Properties.Resources.sc_newdoc; statusStrip1.Hide(); statusStrip2.Hide();
            toolStripButton3.Image = Properties.Resources.sc_open; toolStripButton4.Image = Properties.Resources.sc_save;
            toolStripButton5.Image = Properties.Resources.sc_cut; toolStripButton6.Image = Properties.Resources.sc_copy;
            toolStripButton7.Image = Properties.Resources.sc_paste; toolStripButton8.Image = Properties.Resources.sc_undo;
            toolStripButton9.Image = Properties.Resources.sc_redo; toolStripButton10.Image = Properties.Resources.sc_fontdialog;
            toolStripButton11.Image = Properties.Resources.sc_gotostartoftable; toolStripButton12.Image = Properties.Resources.sc_gotoendofdoc;
            toolStripButton38.Image = Properties.Resources.sc_gramlex; this.Icon = Properties.Resources.prolog;

            cutToolStripMenuItem1.Image = Properties.Resources.sc_cut; copyToolStripMenuItem1.Image = Properties.Resources.sc_copy;
            pasteToolStripMenuItem1.Image = Properties.Resources.sc_paste; deleteToolStripMenuItem1.Image = Properties.Resources.sc_delete;
            selectToolStripMenuItem1.Image = Properties.Resources.sc_selectall; findreplaceToolStripMenuItem.Image = Properties.Resources.lc_zoomtoolbox;
            searchOnTheInternetToolStripMenuItem.Image = Properties.Resources.sc_webhtml; goToToolStripMenuItem1.Image = Properties.Resources.sc_gotopage;
            bookmarksToolStripMenuItem.Image = Properties.Resources.sc_star; addANewTabToolStripMenuItem.Image = Properties.Resources.lc_list_add;
            closeCurrentTabToolStripMenuItem.Image = Properties.Resources.sc_closedoc; renameCurrentTabToolStripMenuItem.Image = Properties.Resources.renameB;
              
                   
        }

        private void fluentToolbar()
        {
            toolStrip1.Visible = true; toolStrip2.Visible = false; toolStrip3.Visible = false; toolStrip4.Visible = false;
            Properties.Settings.Default.fluentStyle = true; Properties.Settings.Default.lunaStyle = false; Properties.Settings.Default.lunaXPStyle = false;
            Properties.Settings.Default.classicStyle = false; Properties.Settings.Default.ClassicNineStyle = false; Properties.Settings.Default.flat8Style = false;
            Properties.Settings.Default.PantherStyle = false; Properties.Settings.Default.emacsStyle = false;
            menuStrip2.Hide(); menuStrip1.Show(); statusStrip1.Hide(); statusStrip2.Hide(); statusStrip.Show();
            label21.Text = "";
            label21.Size = new Size(50, 35); label21.Location = new Point(70, 32);
            label21.Image = null;

            label26.Text = "";
            label26.Image = null;
            label26.Location = new Point(49, 110);
            label26.Size = new Size(20, 13);

            label25.Text = "";
            label25.Image = null;
            label25.Location = new Point(49, 133);
            label25.Size = new Size(20, 13);

            label36.Text = "";
            label36.Image = null;
            label36.Location = new Point(13, 40);
            label36.Size = new Size(20, 13);

            label35.Text = "";
            label35.Image = null;
            label35.Location = new Point(13, 61);
            label35.Size = new Size(20, 13);
            this.Text = Properties.Resources.AppName + " " + Properties.Resources.AppEdition; this.Icon = Properties.Resources.prolog;
            cutToolStripMenuItem1.Image = Properties.Resources.cut; copyToolStripMenuItem1.Image = Properties.Resources.copy;
            pasteToolStripMenuItem1.Image = Properties.Resources.paste; deleteToolStripMenuItem1.Image = Properties.Resources.delete;
            selectToolStripMenuItem1.Image = Properties.Resources.select; findreplaceToolStripMenuItem.Image = Properties.Resources.find;
            searchOnTheInternetToolStripMenuItem.Image = Properties.Resources.link; goToToolStripMenuItem1.Image = Properties.Resources.go_to;
            bookmarksToolStripMenuItem.Image = Properties.Resources.bookmark; addANewTabToolStripMenuItem.Image = Properties.Resources.add;
            closeCurrentTabToolStripMenuItem.Image = Properties.Resources.remove; renameCurrentTabToolStripMenuItem.Image = Properties.Resources.rename;
        }

        private void gnomeToolbar()
        {
            toolStrip1.Visible = false; toolStrip2.Visible = false; toolStrip3.Visible = false; toolStrip4.Visible = true;
            Properties.Settings.Default.fluentStyle = false; Properties.Settings.Default.lunaStyle = false; Properties.Settings.Default.lunaXPStyle = false;
            Properties.Settings.Default.classicStyle = false; Properties.Settings.Default.ClassicNineStyle = true; Properties.Settings.Default.flat8Style = false;
            Properties.Settings.Default.PantherStyle = false; Properties.Settings.Default.emacsStyle = false; statusStrip.Show();
            menuStrip2.Hide(); menuStrip1.Show(); emacsEnableToolbar();
            label21.Text = "  ";
            label21.Image = Properties.Resources.macOpen;


            label26.Text = "  ";
            label26.Image = Properties.Resources.macNew;


            label36.Text = "  ";
            label36.Image = Properties.Resources.macNew;


            label25.Text = "  ";
            label25.Image = Properties.Resources.macOpen;


            label35.Text = "  ";
            label35.Image = Properties.Resources.macOpen;

            this.Text = Properties.Resources.AppName + " " + Properties.Resources.AppEdition;
            toolStripButton25.Image = Properties.Resources.macNew;
            toolStripButton27.Image = Properties.Resources.macOpen; toolStripButton28.Image = Properties.Resources.macSave;
            toolStripButton29.Image = Properties.Resources.macCut; toolStripButton30.Image = Properties.Resources.macCopy;
            toolStripButton31.Image = Properties.Resources.macPaste;
            toolStripButton32.Image = Properties.Resources.macUndo;
            toolStripButton33.Image = Properties.Resources.macRedo;
            toolStripButton34.Image = Properties.Resources.macFont;
            toolStripButton39.Image = Properties.Resources.macGramlex;

            paddingRemoveToolbar(toolStripButton25); paddingRemoveToolbar(toolStripButton27);
            paddingRemoveToolbar(toolStripButton28); paddingRemoveToolbar(toolStripButton29); paddingRemoveToolbar(toolStripButton30);
            paddingRemoveToolbar(toolStripButton31); paddingRemoveToolbar(toolStripButton32); paddingRemoveToolbar(toolStripButton33);
            paddingRemoveToolbar(toolStripButton34); paddingRemoveToolbar(toolStripButton35); paddingRemoveToolbar(toolStripButton36);
            toolStrip4.BackgroundImage = null; this.Icon = Properties.Resources.prolog; statusStrip1.Hide(); statusStrip2.Hide();

            cutToolStripMenuItem1.Image = Properties.Resources.macCut;
            copyToolStripMenuItem1.Image = Properties.Resources.macCopy;
            pasteToolStripMenuItem1.Image = Properties.Resources.macPaste;
            deleteToolStripMenuItem1.Image = Properties.Resources.macDelete;
            selectToolStripMenuItem1.Image = Properties.Resources.macSelect;
            findreplaceToolStripMenuItem.Image = Properties.Resources.macFind;
            searchOnTheInternetToolStripMenuItem.Image = Properties.Resources.macLink;
            goToToolStripMenuItem1.Image = Properties.Resources.macGo_to;
            bookmarksToolStripMenuItem.Image = Properties.Resources.macBookmark;
            addANewTabToolStripMenuItem.Image = Properties.Resources.macAdd;
            closeCurrentTabToolStripMenuItem.Image = Properties.Resources.macRemove;
            renameCurrentTabToolStripMenuItem.Image = Properties.Resources.macRename;
        }

        private void emacsStyle()
        {
            toolStrip1.Visible = false; toolStrip2.Visible = false; toolStrip3.Visible = false; toolStrip4.Visible = true;
            Properties.Settings.Default.fluentStyle = false; Properties.Settings.Default.lunaStyle = false; Properties.Settings.Default.lunaXPStyle = false;
            Properties.Settings.Default.classicStyle = false; Properties.Settings.Default.ClassicNineStyle = false; Properties.Settings.Default.flat8Style = false;
            Properties.Settings.Default.PantherStyle = false; Properties.Settings.Default.emacsStyle = true; emacsDisableToolbar();
            menuStrip2.Show(); menuStrip1.Hide(); statusStrip.Hide(); statusStrip1.BackColor = Color.FromArgb(228, 222, 217);
            label21.Text = "  ";
            label21.Image = Properties.Resources.openEW;


            label26.Text = "  ";
            label26.Image = Properties.Resources.newEW;


            label36.Text = "  ";
            label36.Image = Properties.Resources.newEW;


            label25.Text = "  ";
            label25.Image = Properties.Resources.openEW;


            label35.Text = "  ";
            label35.Image = Properties.Resources.openEW;

            this.Icon = Properties.Resources.emacs; statusStrip1.Show(); statusStrip2.Show(); statusStrip1.ForeColor = Color.Black;
            toolStripButton25.Image = Properties.Resources.newEW;
            toolStripButton27.Image = Properties.Resources.openEW; toolStripButton28.Image = Properties.Resources.saveEW;
            toolStripButton29.Image = Properties.Resources.cutEW; toolStripButton30.Image = Properties.Resources.copyEW;
            toolStripButton31.Image = Properties.Resources.pasteEW;
            toolStripButton32.Image = Properties.Resources.undoEW;
            toolStripButton33.Image = null;
            toolStripButton34.Image = null;
            toolStripButton39.Image = Properties.Resources.findEW;
            this.Text = faTabStrip1.SelectedItem.Title + " - GNU Emacs at " + System.Windows.Forms.SystemInformation.ComputerName;
            paddingAddToolbar(toolStripButton25); paddingAddToolbar(toolStripButton27);
            paddingAddToolbar(toolStripButton28); paddingAddToolbar(toolStripButton29); paddingAddToolbar(toolStripButton30);
            paddingAddToolbar(toolStripButton31); paddingAddToolbar(toolStripButton32); paddingAddToolbar(toolStripButton33);
            paddingAddToolbar(toolStripButton34); paddingAddToolbar(toolStripButton35); paddingAddToolbar(toolStripButton36);
            toolStrip4.BackgroundImage = null;

            cutToolStripMenuItem1.Image = Properties.Resources.macCut;
            copyToolStripMenuItem1.Image = Properties.Resources.macCopy;
            pasteToolStripMenuItem1.Image = Properties.Resources.macPaste;
            deleteToolStripMenuItem1.Image = Properties.Resources.macDelete;
            selectToolStripMenuItem1.Image = Properties.Resources.macSelect;
            findreplaceToolStripMenuItem.Image = Properties.Resources.macFind;
            searchOnTheInternetToolStripMenuItem.Image = Properties.Resources.macLink;
            goToToolStripMenuItem1.Image = Properties.Resources.macGo_to;
            bookmarksToolStripMenuItem.Image = Properties.Resources.macBookmark;
            addANewTabToolStripMenuItem.Image = Properties.Resources.macAdd;
            closeCurrentTabToolStripMenuItem.Image = Properties.Resources.macRemove;
            renameCurrentTabToolStripMenuItem.Image = Properties.Resources.macRename;
        }

        private void emacsDisableToolbar() {
            toolStripSeparator33.Visible = false; toolStripSeparator34.Visible = false; toolStripButton33.Visible = false;
            toolStripButton34.Visible = false; toolStripButton35.Visible = false; toolStripButton36.Visible = false; }

        private void emacsEnableToolbar() {
            toolStripSeparator33.Visible = true; toolStripSeparator34.Visible = true; toolStripButton33.Visible = true;
            toolStripButton34.Visible = true; toolStripButton35.Visible = true; toolStripButton36.Visible = true; }

        private void pantherToolbar()
        {
            toolStrip1.Visible = false; toolStrip2.Visible = false; toolStrip3.Visible = false; toolStrip4.Visible = true;
            Properties.Settings.Default.fluentStyle = false; Properties.Settings.Default.lunaStyle = false; Properties.Settings.Default.lunaXPStyle = false;
            Properties.Settings.Default.classicStyle = false; Properties.Settings.Default.ClassicNineStyle = false; Properties.Settings.Default.flat8Style = false;
            Properties.Settings.Default.PantherStyle = true; Properties.Settings.Default.emacsStyle = false;
            menuStrip2.Hide(); menuStrip1.Show(); statusStrip1.Hide(); statusStrip2.Hide(); statusStrip.Show(); emacsEnableToolbar();
            label21.Text = "  ";
            label21.Image = Properties.Resources.pantherOpen;
            label21.Height = label21.Height + 10;
            label21.Top = label21.Top - 10;

            label26.Text = "  ";
            label26.Image = Properties.Resources.pantherNew16;

            label36.Text = "  ";
            label36.Image = Properties.Resources.pantherNew16;

            label25.Text = "  ";
            label25.Image = Properties.Resources.pantherOpen16;

            label35.Text = "  ";
            label35.Image = Properties.Resources.pantherOpen16;
            this.Icon = Properties.Resources.prolog; this.Text = Properties.Resources.AppName + " " + Properties.Resources.AppEdition;
            cutToolStripMenuItem1.Image = Properties.Resources.pantherCut; copyToolStripMenuItem1.Image = Properties.Resources.pantherCopy;
            pasteToolStripMenuItem1.Image = Properties.Resources.pantherPaste; deleteToolStripMenuItem1.Image = Properties.Resources.macDelete;
            selectToolStripMenuItem1.Image = Properties.Resources.pantherSelect; findreplaceToolStripMenuItem.Image = Properties.Resources.pantherFind;
            searchOnTheInternetToolStripMenuItem.Image = Properties.Resources.pantherLink; goToToolStripMenuItem1.Image = Properties.Resources.pantherGoTo;
            bookmarksToolStripMenuItem.Image = Properties.Resources.pantherBookmark; addANewTabToolStripMenuItem.Image = Properties.Resources.macAdd;
            closeCurrentTabToolStripMenuItem.Image = Properties.Resources.macRemove; renameCurrentTabToolStripMenuItem.Image = Properties.Resources.macRename;
            toolStripButton25.Image = Properties.Resources.pantherNew; statusStrip1.Hide(); statusStrip2.Hide();

            toolStripButton27.Image = Properties.Resources.pantherOpen; toolStripButton28.Image = Properties.Resources.pantherSave; toolStripButton29.Image = Properties.Resources.pantherCut;
            toolStripButton30.Image = Properties.Resources.pantherCopy; toolStripButton31.Image = Properties.Resources.pantherPaste; toolStripButton32.Image = Properties.Resources.pantherUndo;
            toolStripButton33.Image = Properties.Resources.pantherRedo; toolStripButton34.Image = Properties.Resources.pantherFont;


            paddingAddToolbar(toolStripButton25); paddingAddToolbar(toolStripButton27);
            paddingAddToolbar(toolStripButton28); paddingAddToolbar(toolStripButton29); paddingAddToolbar(toolStripButton30);
            paddingAddToolbar(toolStripButton31); paddingAddToolbar(toolStripButton32); paddingAddToolbar(toolStripButton33);
            paddingAddToolbar(toolStripButton34); paddingAddToolbar(toolStripButton35); paddingAddToolbar(toolStripButton36);
            pantherBackground();
        }
        //automatic icon changing method
        public void iconChange()
        {
            Version version = NtDll.RtlGetVersion();

            //changing toolbars from Settings - force change them
            if (Properties.Settings.Default.customIcons)
            {
                if (Properties.Settings.Default.fluentStyle) //fluent toolbar
                { fluentToolbar(); }
                else if (Properties.Settings.Default.lunaStyle) //aero toolbar
                { aeroToolbar(); }
                else if (Properties.Settings.Default.classicStyle) //classic toolbar
                { classicToolbar(); }
                else if (Properties.Settings.Default.ClassicNineStyle) //gnome toolbar
                { gnomeToolbar(); }
                else if (Properties.Settings.Default.PantherStyle) //panther toolbar
                { pantherToolbar(); }
                else if (Properties.Settings.Default.flat8Style) //Office 2013 toolbar
                { metroToolbar(); }
                else if (Properties.Settings.Default.lunaXPStyle) //Luna XP
                { lunaToolbar(); }
                else if (Properties.Settings.Default.emacsStyle) //emacs, ewwww
                { emacsStyle(); }
            } else {
                if (version.Major == 5 && version.Minor == 0) //Windows 2000 - classic toolbar
                { classicToolbar(); }
                else if ((version.Major == 6 && (version.Minor == 0 || version.Minor == 1))) // Aero toolbar
                { aeroToolbar(); }
                else if (version.Major == 5 && (version.Minor == 1 || version.Minor == 2 || version.Minor == 3)) //if you're running XP, load Luna icons
                { lunaToolbar(); }
                else if (version.Major == 6 && (version.Minor == 2 || version.Minor == 3)) //if you're running 8, turn on the Flat metro toolbar
                { metroToolbar(); }
                else if (version.Major == 10) //if you're running 10 or 11, turn on the Fluent toolbar
                { fluentToolbar(); }
                else { aeroToolbar(); }

                Properties.Settings.Default.ClassicNineStyle = false; Properties.Settings.Default.PantherStyle = false;
            }

            toolstripCheck(); tabChangeCheck();
        }

        private void paddingAddToolbar(ToolStripButton button)
        { button.Padding = new Padding(5, 0, 5, 0); }

        private void paddingRemoveToolbar(ToolStripButton button)
        { button.Padding = new Padding(0, 0, 0, 0); }

        public void pantherBackground() //enables/disables the stripes background in the toolba
        {
            if (Properties.Settings.Default.PantherToolbarBackground && Properties.Settings.Default.PantherStyle) //if you have the stripes enabled and have the panther theme enabled
            {
                toolStrip4.BackgroundImageLayout = ImageLayout.Tile; //put background type as tile so it doesn't stretch and it repetes
                toolStrip4.BackgroundImage = Properties.Resources.pantherMenu; //put the stripes background
            }
            else
            {
                toolStrip4.BackgroundImage = null; //just put the system one
            }
        }

        //check your Windows version and activate-deactivate features
        public void WindowsCheck()
        {
            Version version = NtDll.RtlGetVersion(); //MAUI version check

            //check toolbar
            if (version.Build < 19045)
            { micaSupport(); }

            if (Properties.Settings.Default.micaVariantForce)
            {
                Properties.Settings.Default.micaVariant = true; Properties.Settings.Default.Save();
            }

            //check windows version
            if (version.Major == 4 || version.Major == 5 && (version.Minor == 0 || version.Minor == 1 || version.Minor == 2) || (version.Major == 6 && (version.Minor == 0 || version.Minor == 1)))
            {
                webBrowser2.Navigate("about:blank"); //disable Aurora SVGs on Start Page
                splitContainer1.Panel2Collapsed = false; //do not disable the web browser
            }
            else if (version.Major == 6 && (version.Minor == 2 || version.Minor == 3) || version.Major == 10)
            {
                splitContainer1.Panel2Collapsed = false; //if you're running it on 8 or 10, do not disable the Aurora
            }
            else
            {
                webBrowser2.Visible = false; //if you're running it on any other unrecognised version, disable it entirely
                splitContainer1.Panel2Collapsed = true;
            }

            //manage transitions
            if ((version.Major == 5 && (version.Minor == 1 || version.Minor == 2 || version.Minor == 3) || version.Major == 6 || version.Major == 10))
            {
                resize9x = false; //if you're not running it on 2000, enable transitions
            }
            else if (version.Build < 2600)
            {
                resize9x = true;
            } else
            {
                resize9x = true; //disable transitions because you don't know whether or not 
            }
            iconChange(); //manage icon packs throughout the OS    

        }


        public MDIParent1()
        {
            InitializeComponent();
           

        }

        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer(); //timer to manage panels

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop(); panel9.Hide(); panel10.Hide(); panel11.Hide(); //panel9: save - panel10: gramlex
        }

        //system methods
        private void ShowNewForm(object sender, EventArgs e)
        {
            // Create a new instance of the child form.
            Form childForm = new Form();
            // Make it a child of this MDI form before showing it.
            childForm.MdiParent = this;
            childForm.Text = "Window ";
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
                toolStripStatusLabel4.Text = FileName;
                // TODO: Add code here to open the file.
            }
           
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
                // TODO: Add code here to save the current contents of the form to a file.
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void toolStripLabel9_Click_1(object sender, EventArgs e)
        { CurrentTB.Redo(); }

        private void toolStripLabel1_Click_1(object sender, EventArgs e)
        { NavigateBackward(); }

        FastColoredTextBoxNS.FastColoredTextBox CurrentTB //get the current textbox
        {
            get
            {
                if (faTabStrip1.SelectedItem != null && faTabStrip1.SelectedItem.Controls.Count > 0) //if the selected item isn't null and there exists something in the tab
                {
                    return (faTabStrip1.SelectedItem.Controls[0] as FastColoredTextBoxNS.FastColoredTextBox); //return the first item that is a fastcoloredtextbox
                }

                return null; // No control is present or no tab is selected
            }

            set
            {
                if (value != null) //if there's something 
                {
                    FarsiLibrary.Win.FATabStripItem tabItem = value.Parent as FarsiLibrary.Win.FATabStripItem; //set the tab item
                    if (tabItem != null) //if the tab item isn't null
                    {
                        faTabStrip1.SelectedItem = tabItem; //the selected item is the current tab
                        value.Focus(); //focus on the tab
                    }
                }
            }
        }

        private void fontText() //show the font idalog
        {
            FontDialog fontDialog = new FontDialog();

            //min and max dimensions
            fontDialog.MinSize = 8; fontDialog.MaxSize = 72;
            fontDialog.FontMustExist = true; fontDialog.FixedPitchOnly = true;

            if (CurrentTB != null) fontDialog.Font = CurrentTB.Font; //set the font dialog if there's a textbox

            //font dialog
            //fastcolored doesn't support all fonts
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                Font newFont = fontDialog.Font;
                CurrentTB.Invalidate(); //refresh
                CurrentTB.Font = fontDialog.Font; //set the font
                CurrentTB.Invalidate(); //refresh
                Properties.Settings.Default.defaultFont = newFont; //set the property
                Properties.Settings.Default.Save(); //save the properties
            }

        }

        internal class CustomCommandItem : AutocompleteItem //custom items in autocompletemenu
        {
            private string commandExplanation; //command explanation

            public string CommandExplanation //get and set methods
            {
                get { return commandExplanation; }
                set { commandExplanation = value; }
            }

            public CustomCommandItem(string commandText, string explanation) //constructor
            {
                Text = commandText; //command title
                CommandExplanation = explanation; //command explanation
            }

            public override string ToolTipTitle
            {
                get { return Text; }
            }

            public override string ToolTipText
            {
                get { return CommandExplanation; }
            }


        }

        public void autoCompleteMenuPopulate() //add prolog system functions
        {
            autocompleteMenu1.AddItem(new CustomCommandItem("@(", "2 - 2 - Call using calling context"));
            autocompleteMenu1.AddItem(new CustomCommandItem("!(", "0 - 0 - Cut (discard choicepoints)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("$(", "0 - 0 - Discard choicepoints and demand deterministic success"));
            autocompleteMenu1.AddItem(new CustomCommandItem("$(", "2 - 1 - Verify goal succeeds deterministically"));
            autocompleteMenu1.AddItem(new CustomCommandItem(",(", "2 - Conjunction of goals"));
            autocompleteMenu1.AddItem(new CustomCommandItem("->(", "2 - If-then-else"));
            autocompleteMenu1.AddItem(new CustomCommandItem("*->(", "2 - Soft-cut"));
            autocompleteMenu1.AddItem(new CustomCommandItem(".(", "2 - Consult. Also functional notation"));
            autocompleteMenu1.AddItem(new CustomCommandItem(":<(", "2 - Select keys from a dict"));
            autocompleteMenu1.AddItem(new CustomCommandItem(":=(", "2 - WASM: Call JavaScript"));
            autocompleteMenu1.AddItem(new CustomCommandItem(";(", "2 - Disjunction of two goals"));
            autocompleteMenu1.AddItem(new CustomCommandItem("<(", "2 - Arithmetic smaller"));
            autocompleteMenu1.AddItem(new CustomCommandItem("=(", "2 - True when arguments are unified"));
            autocompleteMenu1.AddItem(new CustomCommandItem("=..(", "2 - “Univ.'' Term to list conversion"));
            autocompleteMenu1.AddItem(new CustomCommandItem("=:=(", "2 - Arithmetic equality"));
            autocompleteMenu1.AddItem(new CustomCommandItem("=<(", "2 - Arithmetic smaller or equal"));
            autocompleteMenu1.AddItem(new CustomCommandItem("==(", "2 - Test for strict equality"));
            autocompleteMenu1.AddItem(new CustomCommandItem("=@=(", "2 - Test for structural equality (variant)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("=\\=(", "2 - Arithmetic not equal"));
            autocompleteMenu1.AddItem(new CustomCommandItem(">(", "2 - Arithmetic larger"));
            autocompleteMenu1.AddItem(new CustomCommandItem(">=(", "2 - Arithmetic larger or equal"));
            autocompleteMenu1.AddItem(new CustomCommandItem(">:<(", "2 - Partial dict unification"));
            autocompleteMenu1.AddItem(new CustomCommandItem("?=(", "2 - Test of terms can be compared now"));
            autocompleteMenu1.AddItem(new CustomCommandItem("@<(", "2 - Standard order smaller"));
            autocompleteMenu1.AddItem(new CustomCommandItem("@=<(", "2 - Standard order smaller or equal"));
            autocompleteMenu1.AddItem(new CustomCommandItem("@>(", "2 - Standard order larger"));
            autocompleteMenu1.AddItem(new CustomCommandItem("@>=(", "2 - Standard order larger or equal"));
            autocompleteMenu1.AddItem(new CustomCommandItem("\\+(", "2 - Negation by failure. Same as not/1"));
            autocompleteMenu1.AddItem(new CustomCommandItem("\\=(", "2 - True if arguments cannot be unified"));
            autocompleteMenu1.AddItem(new CustomCommandItem("\\==(", "2 - True if arguments are not strictly equal"));
            autocompleteMenu1.AddItem(new CustomCommandItem("\\=@=(", "2 - Not structural identical"));
            autocompleteMenu1.AddItem(new CustomCommandItem("^(", "2 - Existential quantification (bagof/3, setof/3)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("|(", "2 - Disjunction in DCGs. Same as ;/2"));
            autocompleteMenu1.AddItem(new CustomCommandItem("(", "2 - DCG escape; constraints"));
            autocompleteMenu1.AddItem(new CustomCommandItem("abolish(", "2 - Remove predicate definition from the database"));
            autocompleteMenu1.AddItem(new CustomCommandItem("abolish(", "2 - Remove predicate definition from the database"));
            autocompleteMenu1.AddItem(new CustomCommandItem("abolish_all_tables(", "0 - Abolish computed tables"));
            autocompleteMenu1.AddItem(new CustomCommandItem("abolish_module_tables(", "2 - Abolish all tables in a module"));
            autocompleteMenu1.AddItem(new CustomCommandItem("abolish_monotonic_tables(", "0 - Abolish all monotonic tables"));
            autocompleteMenu1.AddItem(new CustomCommandItem("abolish_nonincremental_tables(", "0 - Abolish non-auttomatic tables"));
            autocompleteMenu1.AddItem(new CustomCommandItem("abolish_nonincremental_tables(", "2 - Abolish non-auttomatic tables"));
            autocompleteMenu1.AddItem(new CustomCommandItem("abolish_private_tables(", "0 - Abolish tables of this thread"));
            autocompleteMenu1.AddItem(new CustomCommandItem("abolish_shared_tables(", "0 - Abolish tables shared between threads"));
            autocompleteMenu1.AddItem(new CustomCommandItem("abolish_table_subgoals(", "2 - Abolish tables for a goal"));
            autocompleteMenu1.AddItem(new CustomCommandItem("abort(", "0 - Abort execution, return to top level"));
            autocompleteMenu1.AddItem(new CustomCommandItem("absolute_file_name(", "2 - Get absolute path name"));
            autocompleteMenu1.AddItem(new CustomCommandItem("absolute_file_name(", "3 - Get absolute path name with options"));
            autocompleteMenu1.AddItem(new CustomCommandItem("answer_count_restraint(", "0 - Undefined answer due to max_answers"));
            autocompleteMenu1.AddItem(new CustomCommandItem("access_file(", "2 - Check access permissions of a file"));
            autocompleteMenu1.AddItem(new CustomCommandItem("acyclic_term(", "2 - Test term for cycles"));
            autocompleteMenu1.AddItem(new CustomCommandItem("add_import_module(", "3 - Add module to the auto-import list"));
            autocompleteMenu1.AddItem(new CustomCommandItem("add_nb_set(", "2 - Add term to a non-backtrackable set"));
            autocompleteMenu1.AddItem(new CustomCommandItem("add_nb_set(", "3 - Add term to a non-backtrackable set"));
            autocompleteMenu1.AddItem(new CustomCommandItem("append(", "2 - Append to a file"));
            autocompleteMenu1.AddItem(new CustomCommandItem("apple_current_locale_identifier(", "2 - Get Apple locale info"));
            autocompleteMenu1.AddItem(new CustomCommandItem("apply(", "2 - Call goal with additional arguments"));
            autocompleteMenu1.AddItem(new CustomCommandItem("apropos(", "2 - library(online_help) Search manual"));
            autocompleteMenu1.AddItem(new CustomCommandItem("arg(", "3 - Access argument of a term"));
            autocompleteMenu1.AddItem(new CustomCommandItem("assoc_to_list(", "2 - Convert association tree to list"));
            autocompleteMenu1.AddItem(new CustomCommandItem("assert(", "2 - Add a clause to the database"));
            autocompleteMenu1.AddItem(new CustomCommandItem("assert(", "2 - Add a clause to the database, give reference"));
            autocompleteMenu1.AddItem(new CustomCommandItem("asserta(", "2 - Add a clause to the database (first)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("asserta(", "2 - Add a clause to the database (first)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("assertion(", "2 - Make assertions about your program"));
            autocompleteMenu1.AddItem(new CustomCommandItem("assertz(", "2 - Add a clause to the database (last)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("assertz(", "2 - Add a clause to the database (last)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("attach_console(", "0 - Attach I/O console to thread"));
            autocompleteMenu1.AddItem(new CustomCommandItem("attach_packs(", "0 - Attach add-ons"));
            autocompleteMenu1.AddItem(new CustomCommandItem("attach_packs(", "2 - Attach add-ons from directory"));
            autocompleteMenu1.AddItem(new CustomCommandItem("attach_packs(", "2 - Attach add-ons from directory"));
            autocompleteMenu1.AddItem(new CustomCommandItem("attribute_goals(", "3 - Project attributes to goals"));
            autocompleteMenu1.AddItem(new CustomCommandItem("attr_unify_hook(", "2 - Attributed variable unification hook"));
            autocompleteMenu1.AddItem(new CustomCommandItem("attr_portray_hook(", "2 - Attributed variable print hook"));
            autocompleteMenu1.AddItem(new CustomCommandItem("attvar(", "2 - Type test for attributed variable"));
            autocompleteMenu1.AddItem(new CustomCommandItem("at_end_of_stream(", "0 - Test for end of file on input"));
            autocompleteMenu1.AddItem(new CustomCommandItem("at_end_of_stream(", "2 - Test for end of file on stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("at_halt(", "2 - Register goal to run at halt/1"));
            autocompleteMenu1.AddItem(new CustomCommandItem("atom(", "2 - Type check for an atom"));
            autocompleteMenu1.AddItem(new CustomCommandItem("atom_chars(", "2 - Convert between atom and list of characters"));
            autocompleteMenu1.AddItem(new CustomCommandItem("atom_codes(", "2 - Convert between atom and list of characters codes"));
            autocompleteMenu1.AddItem(new CustomCommandItem("atom_concat(", "3 - Contatenate two atoms"));
            autocompleteMenu1.AddItem(new CustomCommandItem("atom_length(", "2 - Determine length of an atom"));
            autocompleteMenu1.AddItem(new CustomCommandItem("atom_number(", "2 - Convert between atom and number"));
            autocompleteMenu1.AddItem(new CustomCommandItem("atom_prefix(", "2 - Test for start of atom"));
            autocompleteMenu1.AddItem(new CustomCommandItem("atom_string(", "2 - Conversion between atom and string"));
            autocompleteMenu1.AddItem(new CustomCommandItem("atom_to_term(", "3 - Convert between atom and term"));
            autocompleteMenu1.AddItem(new CustomCommandItem("atomic(", "2 - Type check for primitive"));
            autocompleteMenu1.AddItem(new CustomCommandItem("atomic_concat(", "3 - Concatenate two atomic values to an atom"));
            autocompleteMenu1.AddItem(new CustomCommandItem("atomic_list_concat(", "2 - Append a list of atomics"));
            autocompleteMenu1.AddItem(new CustomCommandItem("atomic_list_concat(", "3 - Append a list of atomics with separator"));
            autocompleteMenu1.AddItem(new CustomCommandItem("atomics_to_string(", "2 - Concatenate list of inputs to a string"));
            autocompleteMenu1.AddItem(new CustomCommandItem("atomics_to_string(", "3 - Concatenate list of inputs to a string"));
            autocompleteMenu1.AddItem(new CustomCommandItem("autoload(", "2 - Declare a file for autoloading"));
            autocompleteMenu1.AddItem(new CustomCommandItem("autoload(", "2 - Declare a file for autoloading specific predicates"));
            autocompleteMenu1.AddItem(new CustomCommandItem("autoload_all(", "0 - Autoload all predicates now"));
            autocompleteMenu1.AddItem(new CustomCommandItem("autoload_path(", "2 - Add directories for autoloading"));
            autocompleteMenu1.AddItem(new CustomCommandItem("await(", "2 - WASM: Wait for a Promise"));
            autocompleteMenu1.AddItem(new CustomCommandItem("b_getval(", "2 - Fetch backtrackable global variable"));
            autocompleteMenu1.AddItem(new CustomCommandItem("b_set_dict(", "3 - Destructive assignment on a dict"));
            autocompleteMenu1.AddItem(new CustomCommandItem("b_setval(", "2 - Assign backtrackable global variable"));
            autocompleteMenu1.AddItem(new CustomCommandItem("bagof(", "3 - Find all solutions to a goal"));
            autocompleteMenu1.AddItem(new CustomCommandItem("between(", "3 - Integer range checking/generating"));
            autocompleteMenu1.AddItem(new CustomCommandItem("blob(", "2 - Type check for a blob"));
            autocompleteMenu1.AddItem(new CustomCommandItem("bounded_number(", "3 - Number between bounds"));
            autocompleteMenu1.AddItem(new CustomCommandItem("break(", "0 - Start interactive top level"));
            autocompleteMenu1.AddItem(new CustomCommandItem("break_hook(", "6 - (hook) Debugger hook"));
            autocompleteMenu1.AddItem(new CustomCommandItem("byte_count(", "2 - Byte-position in a stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("call(", "2 - Call a goal"));
            autocompleteMenu1.AddItem(new CustomCommandItem("call/[2..]", "Call with additional arguments"));
            autocompleteMenu1.AddItem(new CustomCommandItem("call_cleanup(", "2 - Guard a goal with a cleaup-handler"));
            autocompleteMenu1.AddItem(new CustomCommandItem("call_dcg(", "3 - As phrase/3 without type checking"));
            autocompleteMenu1.AddItem(new CustomCommandItem("call_delays(", "2 - Get the condition associated with an answer"));
            autocompleteMenu1.AddItem(new CustomCommandItem("call_residue_vars(", "2 - Find residual attributed variables"));
            autocompleteMenu1.AddItem(new CustomCommandItem("call_residual_program(", "2 - Get residual program associated with an answer"));
            autocompleteMenu1.AddItem(new CustomCommandItem("call_shared_object_function(", "2 - UNIX: Call C-function in shared (.so) file"));
            autocompleteMenu1.AddItem(new CustomCommandItem("call_with_depth_limit(", "3 - Prove goal with bounded depth"));
            autocompleteMenu1.AddItem(new CustomCommandItem("call_with_inference_limit(", "3 - Prove goal in limited inferences"));
            autocompleteMenu1.AddItem(new CustomCommandItem("callable(", "2 - Test for atom or compound term"));
            autocompleteMenu1.AddItem(new CustomCommandItem("cancel_halt(", "2 - Cancel halt/0 from an at_halt/1 hook"));
            autocompleteMenu1.AddItem(new CustomCommandItem("catch(", "3 - Call goal, watching for exceptions"));
            autocompleteMenu1.AddItem(new CustomCommandItem("char_code(", "2 - Convert between character and character code"));
            autocompleteMenu1.AddItem(new CustomCommandItem("char_conversion(", "2 - Provide mapping of input characters"));
            autocompleteMenu1.AddItem(new CustomCommandItem("char_type(", "2 - Classify characters"));
            autocompleteMenu1.AddItem(new CustomCommandItem("character_count(", "2 - Get character index on a stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("chdir(", "2 - Compatibility: change working directory"));
            autocompleteMenu1.AddItem(new CustomCommandItem("chr_constraint(", "2 - CHR Constraint declaration"));
            autocompleteMenu1.AddItem(new CustomCommandItem("chr_show_store(", "2 - List suspended CHR constraints"));
            autocompleteMenu1.AddItem(new CustomCommandItem("chr_trace(", "0 - Start CHR tracer"));
            autocompleteMenu1.AddItem(new CustomCommandItem("chr_type(", "2 - CHR Type declaration"));
            autocompleteMenu1.AddItem(new CustomCommandItem("chr_notrace(", "0 - Stop CHR tracer"));
            autocompleteMenu1.AddItem(new CustomCommandItem("chr_leash(", "2 - Define CHR leashed ports"));
            autocompleteMenu1.AddItem(new CustomCommandItem("chr_option(", "2 - Specify CHR compilation options"));
            autocompleteMenu1.AddItem(new CustomCommandItem("clause(", "2 - Get clauses of a predicate"));
            autocompleteMenu1.AddItem(new CustomCommandItem("clause(", "3 - Get clauses of a predicate"));
            autocompleteMenu1.AddItem(new CustomCommandItem("clause_property(", "2 - Get properties of a clause"));
            autocompleteMenu1.AddItem(new CustomCommandItem("close(", "2 - Close stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("close(", "2 - Close stream (forced)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("close_dde_conversation(", "2 - Win32: Close DDE channel"));
            autocompleteMenu1.AddItem(new CustomCommandItem("close_shared_object(", "2 - UNIX: Close shared library (.so file)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("collation_key(", "2 - Sort key for locale dependent ordering"));
            autocompleteMenu1.AddItem(new CustomCommandItem("comment_hook(", "3 - (hook) handle comments in sources"));
            autocompleteMenu1.AddItem(new CustomCommandItem("compare(", "3 - Compare, using a predicate to determine the order"));
            autocompleteMenu1.AddItem(new CustomCommandItem("compile_aux_clauses(", "2 - Compile predicates for goal_expansion/2"));
            autocompleteMenu1.AddItem(new CustomCommandItem("compile_predicates(", "2 - Compile dynamic code to static"));
            autocompleteMenu1.AddItem(new CustomCommandItem("compiling(", "0 - Is this a compilation run?"));
            autocompleteMenu1.AddItem(new CustomCommandItem("compound(", "2 - Test for compound term"));
            autocompleteMenu1.AddItem(new CustomCommandItem("compound_name_arity(", "3 - Name and arity of a compound term"));
            autocompleteMenu1.AddItem(new CustomCommandItem("compound_name_arguments(", "3 - Name and arguments of a compound term"));
            autocompleteMenu1.AddItem(new CustomCommandItem("code_type(", "2 - Classify a character-code"));
            autocompleteMenu1.AddItem(new CustomCommandItem("consult(", "2 - Read (compile) a Prolog source file"));
            autocompleteMenu1.AddItem(new CustomCommandItem("context_module(", "2 - Get context module of current goal"));
            autocompleteMenu1.AddItem(new CustomCommandItem("convert_time/8", "Break time stamp into fields"));
            autocompleteMenu1.AddItem(new CustomCommandItem("convert_time(", "2 - Convert time stamp to string"));
            autocompleteMenu1.AddItem(new CustomCommandItem("copy_stream_data(", "2 - Copy all data from stream to stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("copy_stream_data(", "3 - Copy n bytes from stream to stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("copy_predicate_clauses(", "2 - Copy clauses between predicates"));
            autocompleteMenu1.AddItem(new CustomCommandItem("copy_term(", "2 - Make a copy of a term"));
            autocompleteMenu1.AddItem(new CustomCommandItem("copy_term(", "3 - Copy a term and obtain attribute-goals"));
            autocompleteMenu1.AddItem(new CustomCommandItem("copy_term(", "4 - Copy part of the variables in a term"));
            autocompleteMenu1.AddItem(new CustomCommandItem("copy_term_nat(", "2 - Make a copy of a term without attributes"));
            autocompleteMenu1.AddItem(new CustomCommandItem("copy_term_nat(", "4 - Copy part of the variables in a term"));
            autocompleteMenu1.AddItem(new CustomCommandItem("create_prolog_flag(", "3 - Create a new Prolog flag"));
            autocompleteMenu1.AddItem(new CustomCommandItem("current_arithmetic_function(", "2 - Examine evaluable functions"));
            autocompleteMenu1.AddItem(new CustomCommandItem("current_atom(", "2 - Examine existing atoms"));
            autocompleteMenu1.AddItem(new CustomCommandItem("current_blob(", "2 - Examine typed blobs"));
            autocompleteMenu1.AddItem(new CustomCommandItem("current_char_conversion(", "2 - Query input character mapping"));
            autocompleteMenu1.AddItem(new CustomCommandItem("current_engine(", "2 - Enumerate known engines"));
            autocompleteMenu1.AddItem(new CustomCommandItem("current_flag(", "2 - Examine existing flags"));
            autocompleteMenu1.AddItem(new CustomCommandItem("current_foreign_library(", "2 - library(shlib) Examine loaded shared libraries (.so files)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("current_format_predicate(", "2 - Enumerate user-defined format codes"));
            autocompleteMenu1.AddItem(new CustomCommandItem("current_functor(", "2 - Examine existing name/arity pairs"));
            autocompleteMenu1.AddItem(new CustomCommandItem("current_input(", "2 - Get current input stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("current_key(", "2 - Examine existing database keys"));
            autocompleteMenu1.AddItem(new CustomCommandItem("current_locale(", "2 - Get the current locale"));
            autocompleteMenu1.AddItem(new CustomCommandItem("current_module(", "2 - Examine existing modules"));
            autocompleteMenu1.AddItem(new CustomCommandItem("current_op(", "3 - Examine current operator declarations"));
            autocompleteMenu1.AddItem(new CustomCommandItem("current_output(", "2 - Get the current output stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("current_predicate(", "2 - Examine existing predicates (ISO)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("current_predicate(", "2 - Examine existing predicates"));
            autocompleteMenu1.AddItem(new CustomCommandItem("current_signal(", "3 - Current software signal mapping"));
            autocompleteMenu1.AddItem(new CustomCommandItem("current_stream(", "3 - Examine open streams"));
            autocompleteMenu1.AddItem(new CustomCommandItem("current_table(", "2 - Find answer table for a variant"));
            autocompleteMenu1.AddItem(new CustomCommandItem("current_transaction(", "2 - Detect encapsulating transactions"));
            autocompleteMenu1.AddItem(new CustomCommandItem("current_trie(", "2 - Enumerate known tries"));
            autocompleteMenu1.AddItem(new CustomCommandItem("cyclic_term(", "2 - Test term for cycles"));
            autocompleteMenu1.AddItem(new CustomCommandItem("day_of_the_week(", "2 - Determine ordinal-day from date"));
            autocompleteMenu1.AddItem(new CustomCommandItem("date_time_stamp(", "2 - Convert date structure to time-stamp"));
            autocompleteMenu1.AddItem(new CustomCommandItem("date_time_value(", "3 - Extract info from a date structure"));
            autocompleteMenu1.AddItem(new CustomCommandItem("dcg_translate_rule(", "2 - Source translation of DCG rules"));
            autocompleteMenu1.AddItem(new CustomCommandItem("dcg_translate_rule(", "4 - Source translation of DCG rules"));
            autocompleteMenu1.AddItem(new CustomCommandItem("dde_current_connection(", "2 - Win32: Examine open DDE connections"));
            autocompleteMenu1.AddItem(new CustomCommandItem("dde_current_service(", "2 - Win32: Examine DDE services provided"));
            autocompleteMenu1.AddItem(new CustomCommandItem("dde_execute(", "2 - Win32: Execute command on DDE server"));
            autocompleteMenu1.AddItem(new CustomCommandItem("dde_register_service(", "2 - Win32: Become a DDE server"));
            autocompleteMenu1.AddItem(new CustomCommandItem("dde_request(", "3 - Win32: Make a DDE request"));
            autocompleteMenu1.AddItem(new CustomCommandItem("dde_poke(", "3 - Win32: POKE operation on DDE server"));
            autocompleteMenu1.AddItem(new CustomCommandItem("dde_unregister_service(", "2 - Win32: Terminate a DDE service"));
            autocompleteMenu1.AddItem(new CustomCommandItem("debug(", "0 - Test for debugging mode"));
            autocompleteMenu1.AddItem(new CustomCommandItem("debug(", "2 - Select topic for debugging"));
            autocompleteMenu1.AddItem(new CustomCommandItem("debug(", "3 - Print debugging message on topic"));
            autocompleteMenu1.AddItem(new CustomCommandItem("debug_control_hook(", "2 - (hook) Extend spy/1, etc."));
            autocompleteMenu1.AddItem(new CustomCommandItem("debugging(", "0 - Show debugger status"));
            autocompleteMenu1.AddItem(new CustomCommandItem("debugging(", "2 - Test where we are debugging topic"));
            autocompleteMenu1.AddItem(new CustomCommandItem("default_module(", "2 - Query module inheritance"));
            autocompleteMenu1.AddItem(new CustomCommandItem("del_attr(", "2 - Delete attribute from variable"));
            autocompleteMenu1.AddItem(new CustomCommandItem("del_attrs(", "2 - Delete all attributes from variable"));
            autocompleteMenu1.AddItem(new CustomCommandItem("del_dict(", "4 - Delete Key-Value pair from a dict"));
            autocompleteMenu1.AddItem(new CustomCommandItem("delays_residual_program(", "2 - Get the residual program for an answer"));
            autocompleteMenu1.AddItem(new CustomCommandItem("delete_directory(", "2 - Remove a folder from the file system"));
            autocompleteMenu1.AddItem(new CustomCommandItem("delete_file(", "2 - Remove a file from the file system"));
            autocompleteMenu1.AddItem(new CustomCommandItem("delete_import_module(", "2 - Remove module from import list"));
            autocompleteMenu1.AddItem(new CustomCommandItem("det(", "2 - Declare predicates as deterministic"));
            autocompleteMenu1.AddItem(new CustomCommandItem("deterministic(", "2 - Test deterministicy of current clause"));
            autocompleteMenu1.AddItem(new CustomCommandItem("dif(", "2 - Constrain two terms to be different"));
            autocompleteMenu1.AddItem(new CustomCommandItem("directory_files(", "2 - Get entries of a directory/folder"));
            autocompleteMenu1.AddItem(new CustomCommandItem("discontiguous(", "2 - Indicate distributed definition of a predicate"));
            autocompleteMenu1.AddItem(new CustomCommandItem("divmod(", "4 - Compute quotient and remainder of two integers"));
            autocompleteMenu1.AddItem(new CustomCommandItem("downcase_atom(", "2 - Convert atom to lower-case"));
            autocompleteMenu1.AddItem(new CustomCommandItem("duplicate_term(", "2 - Create a copy of a term"));
            autocompleteMenu1.AddItem(new CustomCommandItem("dwim_match(", "2 - Atoms match in “Do What I Mean'' sense"));
            autocompleteMenu1.AddItem(new CustomCommandItem("dwim_match(", "3 - Atoms match in “Do What I Mean'' sense"));
            autocompleteMenu1.AddItem(new CustomCommandItem("dwim_predicate(", "2 - Find predicate in “Do What I Mean'' sense"));
            autocompleteMenu1.AddItem(new CustomCommandItem("dynamic(", "2 - Indicate predicate definition may change"));
            autocompleteMenu1.AddItem(new CustomCommandItem("dynamic(", "2 - Indicate predicate definition may change"));
            autocompleteMenu1.AddItem(new CustomCommandItem("edit(", "0 - Edit current script- or associated file"));
            autocompleteMenu1.AddItem(new CustomCommandItem("edit(", "2 - Edit a file, predicate, module (extensible)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("elif(", "2 - Part of conditional compilation (directive)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("else(", "0 - Part of conditional compilation (directive)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("empty_assoc(", "2 - Create/test empty association tree"));
            autocompleteMenu1.AddItem(new CustomCommandItem("empty_nb_set(", "2 - Test/create an empty non-backtrackable set"));
            autocompleteMenu1.AddItem(new CustomCommandItem("encoding(", "2 - Define encoding inside a source file"));
            autocompleteMenu1.AddItem(new CustomCommandItem("endif(", "0 - End of conditional compilation (directive)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("engine_create(", "3 - Create an interactor"));
            autocompleteMenu1.AddItem(new CustomCommandItem("engine_create(", "4 - Create an interactor"));
            autocompleteMenu1.AddItem(new CustomCommandItem("engine_destroy(", "2 - Destroy an interactor"));
            autocompleteMenu1.AddItem(new CustomCommandItem("engine_fetch(", "2 - Get term from caller"));
            autocompleteMenu1.AddItem(new CustomCommandItem("engine_next(", "2 - Ask interactor for next term"));
            autocompleteMenu1.AddItem(new CustomCommandItem("engine_next_reified(", "2 - Ask interactor for next term"));
            autocompleteMenu1.AddItem(new CustomCommandItem("engine_post(", "2 - Send term to an interactor"));
            autocompleteMenu1.AddItem(new CustomCommandItem("engine_post(", "3 - Send term to an interactor and wait for reply"));
            autocompleteMenu1.AddItem(new CustomCommandItem("engine_self(", "2 - Get handle to running interactor"));
            autocompleteMenu1.AddItem(new CustomCommandItem("engine_yield(", "2 - Make term available to caller"));
            autocompleteMenu1.AddItem(new CustomCommandItem("ensure_loaded(", "2 - Consult a file if that has not yet been done"));
            autocompleteMenu1.AddItem(new CustomCommandItem("erase(", "2 - Erase a database record or clause"));
            autocompleteMenu1.AddItem(new CustomCommandItem("exception(", "3 - (hook) Handle runtime exceptions"));
            autocompleteMenu1.AddItem(new CustomCommandItem("exists_directory(", "2 - Check existence of directory"));
            autocompleteMenu1.AddItem(new CustomCommandItem("exists_file(", "2 - Check existence of file"));
            autocompleteMenu1.AddItem(new CustomCommandItem("exists_source(", "2 - Check existence of a Prolog source"));
            autocompleteMenu1.AddItem(new CustomCommandItem("exists_source(", "2 - Check existence of a Prolog source"));
            autocompleteMenu1.AddItem(new CustomCommandItem("expand_answer(", "2 - Expand answer of query"));
            autocompleteMenu1.AddItem(new CustomCommandItem("expand_file_name(", "2 - Wildcard expansion of file names"));
            autocompleteMenu1.AddItem(new CustomCommandItem("expand_file_search_path(", "2 - Wildcard expansion of file paths"));
            autocompleteMenu1.AddItem(new CustomCommandItem("expand_goal(", "2 - Compiler: expand goal in clause-body"));
            autocompleteMenu1.AddItem(new CustomCommandItem("expand_goal(", "4 - Compiler: expand goal in clause-body"));
            autocompleteMenu1.AddItem(new CustomCommandItem("expand_query(", "4 - Expanded entered query"));
            autocompleteMenu1.AddItem(new CustomCommandItem("expand_term(", "2 - Compiler: expand read term into clause(s)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("expand_term(", "4 - Compiler: expand read term into clause(s)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("expects_dialect(", "2 - For which Prolog dialect is this code written?"));
            autocompleteMenu1.AddItem(new CustomCommandItem("explain(", "2 - library(explain) Explain argument"));
            autocompleteMenu1.AddItem(new CustomCommandItem("explain(", "2 - library(explain) 2nd argument is explanation of first"));
            autocompleteMenu1.AddItem(new CustomCommandItem("export(", "2 - Export a predicate from a module"));
            autocompleteMenu1.AddItem(new CustomCommandItem("fail(", "0 - Always false"));
            autocompleteMenu1.AddItem(new CustomCommandItem("false(", "0 - Always false"));
            autocompleteMenu1.AddItem(new CustomCommandItem("fast_term_serialized(", "2 - Fast term (de-)serialization"));
            autocompleteMenu1.AddItem(new CustomCommandItem("fast_read(", "2 - Read binary term serialization"));
            autocompleteMenu1.AddItem(new CustomCommandItem("fast_write(", "2 - Write binary term serialization"));
            autocompleteMenu1.AddItem(new CustomCommandItem("current_prolog_flag(", "2 - Get system configuration parameters"));
            autocompleteMenu1.AddItem(new CustomCommandItem("file_base_name(", "2 - Get file part of path"));
            autocompleteMenu1.AddItem(new CustomCommandItem("file_directory_name(", "2 - Get directory part of path"));
            autocompleteMenu1.AddItem(new CustomCommandItem("file_name_extension(", "3 - Add, remove or test file extensions"));
            autocompleteMenu1.AddItem(new CustomCommandItem("file_search_path(", "2 - Define path-aliases for locating files"));
            autocompleteMenu1.AddItem(new CustomCommandItem("find_chr_constraint(", "2 - Returns a constraint from the store"));
            autocompleteMenu1.AddItem(new CustomCommandItem("findall(", "3 - Find all solutions to a goal"));
            autocompleteMenu1.AddItem(new CustomCommandItem("findall(", "4 - Difference list version of findall/3"));
            autocompleteMenu1.AddItem(new CustomCommandItem("findnsols(", "4 - Find first N solutions"));
            autocompleteMenu1.AddItem(new CustomCommandItem("findnsols(", "5 - Difference list version of findnsols/4"));
            autocompleteMenu1.AddItem(new CustomCommandItem("fill_buffer(", "2 - Fill the input buffer of a stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("flag(", "3 - Simple global variable system"));
            autocompleteMenu1.AddItem(new CustomCommandItem("float(", "2 - Type check for a floating point number"));
            autocompleteMenu1.AddItem(new CustomCommandItem("float_class(", "2 - Classify (special) floats"));
            autocompleteMenu1.AddItem(new CustomCommandItem("float_parts(", "4 - Get mantissa and exponent of a float"));
            autocompleteMenu1.AddItem(new CustomCommandItem("flush_output(", "0 - Output pending characters on current stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("flush_output(", "2 - Output pending characters on specified stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("forall(", "2 - Prove goal for all solutions of another goal"));
            autocompleteMenu1.AddItem(new CustomCommandItem("format(", "2 - Formatted output"));
            autocompleteMenu1.AddItem(new CustomCommandItem("format(", "2 - Formatted output with arguments"));
            autocompleteMenu1.AddItem(new CustomCommandItem("format(", "3 - Formatted output on a stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("format_time(", "3 - C strftime() like date/time formatter"));
            autocompleteMenu1.AddItem(new CustomCommandItem("format_time(", "4 - date/time formatter with explicit locale"));
            autocompleteMenu1.AddItem(new CustomCommandItem("format_predicate(", "2 - Program format/[1,2]"));
            autocompleteMenu1.AddItem(new CustomCommandItem("term_attvars(", "2 - Find attributed variables in a term"));
            autocompleteMenu1.AddItem(new CustomCommandItem("term_variables(", "2 - Find unbound variables in a term"));
            autocompleteMenu1.AddItem(new CustomCommandItem("term_variables(", "3 - Find unbound variables in a term"));
            autocompleteMenu1.AddItem(new CustomCommandItem("text_to_string(", "2 - Convert arbitrary text to a string"));
            autocompleteMenu1.AddItem(new CustomCommandItem("freeze(", "2 - Delay execution until variable is bound"));
            autocompleteMenu1.AddItem(new CustomCommandItem("frozen(", "2 - Query delayed goals on var"));
            autocompleteMenu1.AddItem(new CustomCommandItem("functor(", "3 - Get name and arity of a term or construct a term"));
            autocompleteMenu1.AddItem(new CustomCommandItem("functor(", "4 - Get name and arity of a term or construct a term"));
            autocompleteMenu1.AddItem(new CustomCommandItem("garbage_collect(", "0 - Invoke the garbage collector"));
            autocompleteMenu1.AddItem(new CustomCommandItem("garbage_collect_atoms(", "0 - Invoke the atom garbage collector"));
            autocompleteMenu1.AddItem(new CustomCommandItem("garbage_collect_clauses(", "0 - Invoke clause garbage collector"));
            autocompleteMenu1.AddItem(new CustomCommandItem("gen_assoc(", "3 - Enumerate members of association tree"));
            autocompleteMenu1.AddItem(new CustomCommandItem("gen_nb_set(", "2 - Generate members of non-backtrackable set"));
            autocompleteMenu1.AddItem(new CustomCommandItem("gensym(", "2 - Generate unique atoms from a base"));
            autocompleteMenu1.AddItem(new CustomCommandItem("get(", "2 - Read first non-blank character"));
            autocompleteMenu1.AddItem(new CustomCommandItem("get(", "2 - Read first non-blank character from a stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("get_assoc(", "3 - Fetch key from association tree"));
            autocompleteMenu1.AddItem(new CustomCommandItem("get_assoc(", "5 - Fetch key from association tree"));
            autocompleteMenu1.AddItem(new CustomCommandItem("get_attr(", "3 - Fetch named attribute from a variable"));
            autocompleteMenu1.AddItem(new CustomCommandItem("get_attrs(", "2 - Fetch all attributes of a variable"));
            autocompleteMenu1.AddItem(new CustomCommandItem("get_byte(", "2 - Read next byte (ISO)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("get_byte(", "2 - Read next byte from a stream (ISO)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("get_char(", "2 - Read next character as an atom (ISO)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("get_char(", "2 - Read next character from a stream (ISO)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("get_code(", "2 - Read next character (ISO)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("get_code(", "2 - Read next character from a stream (ISO)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("get_dict(", "3 - Get the value associated to a key from a dict"));
            autocompleteMenu1.AddItem(new CustomCommandItem("get_dict(", "5 - Replace existing value in a dict"));
            autocompleteMenu1.AddItem(new CustomCommandItem("get_flag(", "2 - Get value of a flag"));
            autocompleteMenu1.AddItem(new CustomCommandItem("get_single_char(", "2 - Read next character from the terminal"));
            autocompleteMenu1.AddItem(new CustomCommandItem("get_string_code(", "3 - Get character code at index in string"));
            autocompleteMenu1.AddItem(new CustomCommandItem("get_time(", "2 - Get current time"));
            autocompleteMenu1.AddItem(new CustomCommandItem("get0(", "2 - Read next character"));
            autocompleteMenu1.AddItem(new CustomCommandItem("get0(", "2 - Read next character from a stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("getenv(", "2 - Get shell environment variable"));
            autocompleteMenu1.AddItem(new CustomCommandItem("goal_expansion(", "2 - Hook for macro-expanding goals"));
            autocompleteMenu1.AddItem(new CustomCommandItem("goal_expansion(", "4 - Hook for macro-expanding goals"));
            autocompleteMenu1.AddItem(new CustomCommandItem("ground(", "2 - Verify term holds no unbound variables"));
            autocompleteMenu1.AddItem(new CustomCommandItem("gdebug(", "0 - Debug using graphical tracer"));
            autocompleteMenu1.AddItem(new CustomCommandItem("gspy(", "2 - Spy using graphical tracer"));
            autocompleteMenu1.AddItem(new CustomCommandItem("gtrace(", "0 - Trace using graphical tracer"));
            autocompleteMenu1.AddItem(new CustomCommandItem("guitracer(", "0 - Install hooks for the graphical debugger"));
            autocompleteMenu1.AddItem(new CustomCommandItem("gxref(", "0 - Cross-reference loaded program"));
            autocompleteMenu1.AddItem(new CustomCommandItem("halt(", "0 - Exit from Prolog"));
            autocompleteMenu1.AddItem(new CustomCommandItem("halt(", "2 - Exit from Prolog with status"));
            autocompleteMenu1.AddItem(new CustomCommandItem("term_hash(", "2 - Hash-value of ground term"));
            autocompleteMenu1.AddItem(new CustomCommandItem("term_hash(", "4 - Hash-value of term with depth limit"));
            autocompleteMenu1.AddItem(new CustomCommandItem("help(", "0 - Give help on help"));
            autocompleteMenu1.AddItem(new CustomCommandItem("help(", "2 - Give help on predicates and show parts of manual"));
            autocompleteMenu1.AddItem(new CustomCommandItem("help_hook(", "2 - (hook) User-hook in the help-system"));
            autocompleteMenu1.AddItem(new CustomCommandItem("if(", "2 - Start conditional compilation (directive)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("ignore(", "2 - Call the argument, but always succeed"));
            autocompleteMenu1.AddItem(new CustomCommandItem("import(", "2 - Import a predicate from a module"));
            autocompleteMenu1.AddItem(new CustomCommandItem("import_module(", "2 - Query import modules"));
            autocompleteMenu1.AddItem(new CustomCommandItem("in_pce_thread(", "2 - Run goal in XPCE thread"));
            autocompleteMenu1.AddItem(new CustomCommandItem("in_pce_thread_sync(", "2 - Run goal in XPCE thread"));
            autocompleteMenu1.AddItem(new CustomCommandItem("include(", "2 - Include a file with declarations"));
            autocompleteMenu1.AddItem(new CustomCommandItem("initialization(", "2 - Initialization directive"));
            autocompleteMenu1.AddItem(new CustomCommandItem("initialization(", "2 - Initialization directive"));
            autocompleteMenu1.AddItem(new CustomCommandItem("initialize(", "0 - Run program initialization"));
            autocompleteMenu1.AddItem(new CustomCommandItem("instance(", "2 - Fetch clause or record from reference"));
            autocompleteMenu1.AddItem(new CustomCommandItem("integer(", "2 - Type check for integer"));
            autocompleteMenu1.AddItem(new CustomCommandItem("interactor(", "0 - Start new thread with console and top level"));
            autocompleteMenu1.AddItem(new CustomCommandItem("is(", "2 - Evaluate arithmetic expression"));
            autocompleteMenu1.AddItem(new CustomCommandItem("is_absolute_file_name(", "2 - True if arg defines an absolute path"));
            autocompleteMenu1.AddItem(new CustomCommandItem("is_assoc(", "2 - Verify association list"));
            autocompleteMenu1.AddItem(new CustomCommandItem("is_async(", "0 - WASM: Test Prolog can call await/2"));
            autocompleteMenu1.AddItem(new CustomCommandItem("is_dict(", "2 - Type check for a dict"));
            autocompleteMenu1.AddItem(new CustomCommandItem("is_dict(", "2 - Type check for a dict in a class"));
            autocompleteMenu1.AddItem(new CustomCommandItem("is_engine(", "2 - Type check for an engine handle"));
            autocompleteMenu1.AddItem(new CustomCommandItem("is_list(", "2 - Type check for a list"));
            autocompleteMenu1.AddItem(new CustomCommandItem("is_most_general_term(", "2 - Type check for general term"));
            autocompleteMenu1.AddItem(new CustomCommandItem("is_object(", "2 - WASM: Test JavaScript object"));
            autocompleteMenu1.AddItem(new CustomCommandItem("is_object(", "2 - WASM: Test JavaScript object and class"));
            autocompleteMenu1.AddItem(new CustomCommandItem("is_stream(", "2 - Type check for a stream handle"));
            autocompleteMenu1.AddItem(new CustomCommandItem("is_trie(", "2 - Type check for a trie handle"));
            autocompleteMenu1.AddItem(new CustomCommandItem("is_thread(", "2 - Type check for an thread handle"));
            autocompleteMenu1.AddItem(new CustomCommandItem("join_threads(", "0 - Join all terminated threads interactively"));
            autocompleteMenu1.AddItem(new CustomCommandItem("keysort(", "2 - Sort, using a key"));
            autocompleteMenu1.AddItem(new CustomCommandItem("known_licenses(", "0 - Print known licenses"));
            autocompleteMenu1.AddItem(new CustomCommandItem("last(", "2 - Last element of a list"));
            autocompleteMenu1.AddItem(new CustomCommandItem("leash(", "2 - Change ports visited by the tracer"));
            autocompleteMenu1.AddItem(new CustomCommandItem("length(", "2 - Length of a list"));
            autocompleteMenu1.AddItem(new CustomCommandItem("library_directory(", "2 - (hook) Directories holding Prolog libraries"));
            autocompleteMenu1.AddItem(new CustomCommandItem("license(", "0 - Evaluate licenses of loaded modules"));
            autocompleteMenu1.AddItem(new CustomCommandItem("license(", "2 - Define license for current file"));
            autocompleteMenu1.AddItem(new CustomCommandItem("license(", "2 - Define license for named module"));
            autocompleteMenu1.AddItem(new CustomCommandItem("line_count(", "2 - Line number on stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("line_position(", "2 - Character position in line on stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("list_debug_topics(", "0 - List registered topics for debugging"));
            autocompleteMenu1.AddItem(new CustomCommandItem("list_to_assoc(", "2 - Create association tree from list"));
            autocompleteMenu1.AddItem(new CustomCommandItem("list_to_set(", "2 - Remove duplicates from a list"));
            autocompleteMenu1.AddItem(new CustomCommandItem("list_strings(", "0 - Help porting to version 7"));
            autocompleteMenu1.AddItem(new CustomCommandItem("load_files(", "2 - Load source files"));
            autocompleteMenu1.AddItem(new CustomCommandItem("load_files(", "2 - Load source files with options"));
            autocompleteMenu1.AddItem(new CustomCommandItem("load_foreign_library(", "2 - library(shlib) Load shared library (.so file)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("load_foreign_library(", "2 - library(shlib) Load shared library (.so file)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("locale_create(", "3 - Create a new locale object"));
            autocompleteMenu1.AddItem(new CustomCommandItem("locale_destroy(", "2 - Destroy a locale object"));
            autocompleteMenu1.AddItem(new CustomCommandItem("locale_property(", "2 - Query properties of locale objects"));
            autocompleteMenu1.AddItem(new CustomCommandItem("locale_sort(", "2 - Language dependent sort of atoms"));
            autocompleteMenu1.AddItem(new CustomCommandItem("make(", "0 - Reconsult all changed source files"));
            autocompleteMenu1.AddItem(new CustomCommandItem("make_directory(", "2 - Create a folder on the file system"));
            autocompleteMenu1.AddItem(new CustomCommandItem("make_library_index(", "2 - Create autoload file INDEX.pl"));
            autocompleteMenu1.AddItem(new CustomCommandItem("malloc_property(", "2 - Property of the allocator"));
            autocompleteMenu1.AddItem(new CustomCommandItem("make_library_index(", "2 - Create selective autoload file INDEX.pl"));
            autocompleteMenu1.AddItem(new CustomCommandItem("map_assoc(", "2 - Map association tree"));
            autocompleteMenu1.AddItem(new CustomCommandItem("map_assoc(", "3 - Map association tree"));
            autocompleteMenu1.AddItem(new CustomCommandItem("dict_create(", "3 - Create a dict from data"));
            autocompleteMenu1.AddItem(new CustomCommandItem("dict_pairs(", "3 - Convert between dict and list of pairs"));
            autocompleteMenu1.AddItem(new CustomCommandItem("max_assoc(", "3 - Highest key in association tree"));
            autocompleteMenu1.AddItem(new CustomCommandItem("memberchk(", "2 - Deterministic member/2"));
            autocompleteMenu1.AddItem(new CustomCommandItem("message_hook(", "3 - Intercept print_message/2"));
            autocompleteMenu1.AddItem(new CustomCommandItem("message_line_element(", "2 - (hook) Intercept print_message_lines/3"));
            autocompleteMenu1.AddItem(new CustomCommandItem("message_property(", "2 - (hook) Define display of a message"));
            autocompleteMenu1.AddItem(new CustomCommandItem("message_queue_create(", "2 - Create queue for thread communication"));
            autocompleteMenu1.AddItem(new CustomCommandItem("message_queue_create(", "2 - Create queue for thread communication"));
            autocompleteMenu1.AddItem(new CustomCommandItem("message_queue_destroy(", "2 - Destroy queue for thread communication"));
            autocompleteMenu1.AddItem(new CustomCommandItem("message_queue_property(", "2 - Query message queue properties"));
            autocompleteMenu1.AddItem(new CustomCommandItem("message_queue_set(", "2 - Set a message queue property"));
            autocompleteMenu1.AddItem(new CustomCommandItem("message_to_string(", "2 - Translate message-term to string"));
            autocompleteMenu1.AddItem(new CustomCommandItem("meta_predicate(", "2 - Declare access to other predicates"));
            autocompleteMenu1.AddItem(new CustomCommandItem("min_assoc(", "3 - Lowest key in association tree"));
            autocompleteMenu1.AddItem(new CustomCommandItem("module(", "2 - Query/set current type-in module"));
            autocompleteMenu1.AddItem(new CustomCommandItem("module(", "2 - Declare a module"));
            autocompleteMenu1.AddItem(new CustomCommandItem("module(", "3 - Declare a module with language options"));
            autocompleteMenu1.AddItem(new CustomCommandItem("module_property(", "2 - Find properties of a module"));
            autocompleteMenu1.AddItem(new CustomCommandItem("module_transparent(", "2 - Indicate module based meta-predicate"));
            autocompleteMenu1.AddItem(new CustomCommandItem("msort(", "2 - Sort, do not remove duplicates"));
            autocompleteMenu1.AddItem(new CustomCommandItem("multifile(", "2 - Indicate distributed definition of predicate"));
            autocompleteMenu1.AddItem(new CustomCommandItem("mutex_create(", "2 - Create a thread-synchronisation device"));
            autocompleteMenu1.AddItem(new CustomCommandItem("mutex_create(", "2 - Create a thread-synchronisation device"));
            autocompleteMenu1.AddItem(new CustomCommandItem("mutex_destroy(", "2 - Destroy a mutex"));
            autocompleteMenu1.AddItem(new CustomCommandItem("mutex_lock(", "2 - Become owner of a mutex"));
            autocompleteMenu1.AddItem(new CustomCommandItem("mutex_property(", "2 - Query mutex properties"));
            autocompleteMenu1.AddItem(new CustomCommandItem("mutex_statistics(", "0 - Print statistics on mutex usage"));
            autocompleteMenu1.AddItem(new CustomCommandItem("mutex_trylock(", "2 - Become owner of a mutex (non-blocking)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("mutex_unlock(", "2 - Release ownership of mutex"));
            autocompleteMenu1.AddItem(new CustomCommandItem("mutex_unlock_all(", "0 - Release ownership of all mutexes"));
            autocompleteMenu1.AddItem(new CustomCommandItem("name(", "2 - Convert between atom and list of character codes"));
            autocompleteMenu1.AddItem(new CustomCommandItem("nb_current(", "2 - Enumerate non-backtrackable global variables"));
            autocompleteMenu1.AddItem(new CustomCommandItem("nb_delete(", "2 - Delete a non-backtrackable global variable"));
            autocompleteMenu1.AddItem(new CustomCommandItem("nb_getval(", "2 - Fetch non-backtrackable global variable"));
            autocompleteMenu1.AddItem(new CustomCommandItem("nb_link_dict(", "3 - Non-backtrackable assignment to dict"));
            autocompleteMenu1.AddItem(new CustomCommandItem("nb_linkarg(", "3 - Non-backtrackable assignment to term"));
            autocompleteMenu1.AddItem(new CustomCommandItem("nb_linkval(", "2 - Assign non-backtrackable global variable"));
            autocompleteMenu1.AddItem(new CustomCommandItem("nb_set_to_list(", "2 - Convert non-backtrackable set to list"));
            autocompleteMenu1.AddItem(new CustomCommandItem("nb_set_dict(", "3 - Non-backtrackable assignment to dict"));
            autocompleteMenu1.AddItem(new CustomCommandItem("nb_setarg(", "3 - Non-backtrackable assignment to term"));
            autocompleteMenu1.AddItem(new CustomCommandItem("nb_setval(", "2 - Assign non-backtrackable global variable"));
            autocompleteMenu1.AddItem(new CustomCommandItem("nl(", "0 - Generate a newline"));
            autocompleteMenu1.AddItem(new CustomCommandItem("nl(", "2 - Generate a newline on a stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("nodebug(", "0 - Disable debugging"));
            autocompleteMenu1.AddItem(new CustomCommandItem("nodebug(", "2 - Disable debug-topic"));
            autocompleteMenu1.AddItem(new CustomCommandItem("noguitracer(", "0 - Disable the graphical debugger"));
            autocompleteMenu1.AddItem(new CustomCommandItem("nonground(", "2 - Term is not ground due to witness"));
            autocompleteMenu1.AddItem(new CustomCommandItem("nonvar(", "2 - Type check for bound term"));
            autocompleteMenu1.AddItem(new CustomCommandItem("nonterminal(", "2 - Set predicate property"));
            autocompleteMenu1.AddItem(new CustomCommandItem("noprofile(", "2 - Hide (meta-) predicate for the profiler"));
            autocompleteMenu1.AddItem(new CustomCommandItem("noprotocol(", "0 - Disable logging of user interaction"));
            autocompleteMenu1.AddItem(new CustomCommandItem("normalize_space(", "2 - Normalize white space"));
            autocompleteMenu1.AddItem(new CustomCommandItem("nospy(", "2 - Remove spy point"));
            autocompleteMenu1.AddItem(new CustomCommandItem("nospyall(", "0 - Remove all spy points"));
            autocompleteMenu1.AddItem(new CustomCommandItem("not(", "2 - Negation by failure (argument not provable). Same as \\+/1"));
            autocompleteMenu1.AddItem(new CustomCommandItem("not_exists(", "2 - Tabled negation for non-ground or non-tabled goals"));
            autocompleteMenu1.AddItem(new CustomCommandItem("notrace(", "0 - Stop tracing"));
            autocompleteMenu1.AddItem(new CustomCommandItem("notrace(", "2 - Do not debug argument goal"));
            autocompleteMenu1.AddItem(new CustomCommandItem("nth_clause(", "3 - N-th clause of a predicate"));
            autocompleteMenu1.AddItem(new CustomCommandItem("nth_integer_root_and_remainder(", "4 - Integer root and remainder"));
            autocompleteMenu1.AddItem(new CustomCommandItem("number(", "2 - Type check for integer or float"));
            autocompleteMenu1.AddItem(new CustomCommandItem("number_chars(", "2 - Convert between number and one-char atoms"));
            autocompleteMenu1.AddItem(new CustomCommandItem("number_codes(", "2 - Convert between number and character codes"));
            autocompleteMenu1.AddItem(new CustomCommandItem("number_string(", "2 - Convert between number and string"));
            autocompleteMenu1.AddItem(new CustomCommandItem("numbervars(", "3 - Number unbound variables of a term"));
            autocompleteMenu1.AddItem(new CustomCommandItem("numbervars(", "4 - Number unbound variables of a term"));
            autocompleteMenu1.AddItem(new CustomCommandItem("on_signal(", "3 - Handle a software signal"));
            autocompleteMenu1.AddItem(new CustomCommandItem("once(", "2 - Call a goal deterministically"));
            autocompleteMenu1.AddItem(new CustomCommandItem("op(", "3 - Declare an operator"));
            autocompleteMenu1.AddItem(new CustomCommandItem("open(", "3 - Open a file (creating a stream)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("open(", "4 - Open a file (creating a stream)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("open_dde_conversation(", "3 - Win32: Open DDE channel"));
            autocompleteMenu1.AddItem(new CustomCommandItem("open_null_stream(", "2 - Open a stream to discard output"));
            autocompleteMenu1.AddItem(new CustomCommandItem("open_resource(", "3 - Open a program resource as a stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("open_shared_object(", "2 - UNIX: Open shared library (.so file)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("open_shared_object(", "3 - UNIX: Open shared library (.so file)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("open_source_hook(", "3 - (hook) Open a source file"));
            autocompleteMenu1.AddItem(new CustomCommandItem("open_string(", "2 - Open a string as a stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("ord_list_to_assoc(", "2 - Convert ordered list to assoc"));
            autocompleteMenu1.AddItem(new CustomCommandItem("parse_time(", "2 - Parse text to a time-stamp"));
            autocompleteMenu1.AddItem(new CustomCommandItem("parse_time(", "3 - Parse text to a time-stamp"));
            autocompleteMenu1.AddItem(new CustomCommandItem("pce_dispatch(", "2 - Run XPCE GUI in separate thread"));
            autocompleteMenu1.AddItem(new CustomCommandItem("pce_call(", "2 - Run goal in XPCE GUI thread"));
            autocompleteMenu1.AddItem(new CustomCommandItem("peek_byte(", "2 - Read byte without removing"));
            autocompleteMenu1.AddItem(new CustomCommandItem("peek_byte(", "2 - Read byte without removing"));
            autocompleteMenu1.AddItem(new CustomCommandItem("peek_char(", "2 - Read character without removing"));
            autocompleteMenu1.AddItem(new CustomCommandItem("peek_char(", "2 - Read character without removing"));
            autocompleteMenu1.AddItem(new CustomCommandItem("peek_code(", "2 - Read character-code without removing"));
            autocompleteMenu1.AddItem(new CustomCommandItem("peek_code(", "2 - Read character-code without removing"));
            autocompleteMenu1.AddItem(new CustomCommandItem("peek_string(", "3 - Read a string without removing"));
            autocompleteMenu1.AddItem(new CustomCommandItem("phrase(", "2 - Activate grammar-rule set"));
            autocompleteMenu1.AddItem(new CustomCommandItem("phrase(", "3 - Activate grammar-rule set (returning rest)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("phrase_from_quasi_quotation(", "2 - Parse quasi quotation with DCG"));
            autocompleteMenu1.AddItem(new CustomCommandItem("please(", "3 - Query/change environment parameters"));
            autocompleteMenu1.AddItem(new CustomCommandItem("plus(", "3 - Logical integer addition"));
            autocompleteMenu1.AddItem(new CustomCommandItem("portray(", "2 - (hook) Modify behaviour of print/1"));
            autocompleteMenu1.AddItem(new CustomCommandItem("predicate_property(", "2 - Query predicate attributes"));
            autocompleteMenu1.AddItem(new CustomCommandItem("predsort(", "3 - Sort, using a predicate to determine the order"));
            autocompleteMenu1.AddItem(new CustomCommandItem("print(", "2 - Print a term"));
            autocompleteMenu1.AddItem(new CustomCommandItem("print(", "2 - Print a term on a stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("print_message(", "2 - Print message from (exception) term"));
            autocompleteMenu1.AddItem(new CustomCommandItem("print_message_lines(", "3 - Print message to stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("profile(", "2 - Obtain execution statistics"));
            autocompleteMenu1.AddItem(new CustomCommandItem("profile(", "2 - Obtain execution statistics"));
            autocompleteMenu1.AddItem(new CustomCommandItem("profile_count(", "3 - Obtain profile results on a predicate"));
            autocompleteMenu1.AddItem(new CustomCommandItem("profiler(", "2 - Obtain/change status of the profiler"));
            autocompleteMenu1.AddItem(new CustomCommandItem("prolog(", "0 - Run interactive top level"));
            autocompleteMenu1.AddItem(new CustomCommandItem("prolog_alert_signal(", "2 - Query/set unblock signal"));
            autocompleteMenu1.AddItem(new CustomCommandItem("prolog_choice_attribute(", "3 - Examine the choice point stack"));
            autocompleteMenu1.AddItem(new CustomCommandItem("prolog_current_choice(", "2 - Reference to most recent choice point"));
            autocompleteMenu1.AddItem(new CustomCommandItem("prolog_current_frame(", "2 - Reference to goal's environment stack"));
            autocompleteMenu1.AddItem(new CustomCommandItem("prolog_cut_to(", "2 - Realise global cuts"));
            autocompleteMenu1.AddItem(new CustomCommandItem("prolog_edit:locate(", "2 - Locate targets for edit/1"));
            autocompleteMenu1.AddItem(new CustomCommandItem("prolog_edit:locate(", "3 - Locate targets for edit/1"));
            autocompleteMenu1.AddItem(new CustomCommandItem("prolog_edit:edit_source(", "2 - Call editor for edit/1"));
            autocompleteMenu1.AddItem(new CustomCommandItem("prolog_edit:edit_command(", "2 - Specify editor activation"));
            autocompleteMenu1.AddItem(new CustomCommandItem("prolog_edit:load(", "0 - Load edit/1 extensions"));
            autocompleteMenu1.AddItem(new CustomCommandItem("prolog_exception_hook(", "4 - Rewrite exceptions"));
            autocompleteMenu1.AddItem(new CustomCommandItem("prolog_file_type(", "2 - Define meaning of file extension"));
            autocompleteMenu1.AddItem(new CustomCommandItem("prolog_frame_attribute(", "3 - Obtain information on a goal environment"));
            autocompleteMenu1.AddItem(new CustomCommandItem("prolog_ide(", "2 - Program access to the development environment"));
            autocompleteMenu1.AddItem(new CustomCommandItem("prolog_interrupt(", "0 - Allow debugging a thread"));
            autocompleteMenu1.AddItem(new CustomCommandItem("prolog_list_goal(", "2 - (hook) Intercept tracer’L' command"));
            autocompleteMenu1.AddItem(new CustomCommandItem("prolog_listen(", "2 - Listen to Prolog events"));
            autocompleteMenu1.AddItem(new CustomCommandItem("prolog_listen(", "3 - Listen to Prolog events"));
            autocompleteMenu1.AddItem(new CustomCommandItem("prolog_load_context(", "2 - Context information for directives"));
            autocompleteMenu1.AddItem(new CustomCommandItem("prolog_load_file(", "2 - (hook) Program load_files/2"));
            autocompleteMenu1.AddItem(new CustomCommandItem("prolog_skip_level(", "2 - Indicate deepest recursion to trace"));
            autocompleteMenu1.AddItem(new CustomCommandItem("prolog_skip_frame(", "2 - Perform‘skip' on a frame"));
            autocompleteMenu1.AddItem(new CustomCommandItem("prolog_stack_property(", "2 - Query properties of the stacks"));
            autocompleteMenu1.AddItem(new CustomCommandItem("prolog_to_os_filename(", "2 - Convert between Prolog and OS filenames"));
            autocompleteMenu1.AddItem(new CustomCommandItem("prolog_trace_interception(", "4 - library(user) Intercept the Prolog tracer"));
            autocompleteMenu1.AddItem(new CustomCommandItem("prolog_unlisten(", "2 - Stop listening to Prolog events"));
            autocompleteMenu1.AddItem(new CustomCommandItem("project_attributes(", "2 - Project constraints to query variables"));
            autocompleteMenu1.AddItem(new CustomCommandItem("prompt1(", "2 - Change prompt for 1 line"));
            autocompleteMenu1.AddItem(new CustomCommandItem("prompt(", "2 - Change the prompt used by read/1"));
            autocompleteMenu1.AddItem(new CustomCommandItem("protocol(", "2 - Make a log of the user interaction"));
            autocompleteMenu1.AddItem(new CustomCommandItem("protocola(", "2 - Append log of the user interaction to file"));
            autocompleteMenu1.AddItem(new CustomCommandItem("protocolling(", "2 - On what file is user interaction logged"));
            autocompleteMenu1.AddItem(new CustomCommandItem("public(", "2 - Declaration that a predicate may be called"));
            autocompleteMenu1.AddItem(new CustomCommandItem("put(", "2 - Write a character"));
            autocompleteMenu1.AddItem(new CustomCommandItem("put(", "2 - Write a character on a stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("put_assoc(", "4 - Add Key-Value to association tree"));
            autocompleteMenu1.AddItem(new CustomCommandItem("put_attr(", "3 - Put attribute on a variable"));
            autocompleteMenu1.AddItem(new CustomCommandItem("put_attrs(", "2 - Set/replace all attributes on a variable"));
            autocompleteMenu1.AddItem(new CustomCommandItem("put_byte(", "2 - Write a byte"));
            autocompleteMenu1.AddItem(new CustomCommandItem("put_byte(", "2 - Write a byte on a stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("put_char(", "2 - Write a character"));
            autocompleteMenu1.AddItem(new CustomCommandItem("put_char(", "2 - Write a character on a stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("put_code(", "2 - Write a character-code"));
            autocompleteMenu1.AddItem(new CustomCommandItem("put_code(", "2 - Write a character-code on a stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("put_dict(", "3 - Add/replace multiple keys in a dict"));
            autocompleteMenu1.AddItem(new CustomCommandItem("put_dict(", "4 - Add/replace a single key in a dict"));
            autocompleteMenu1.AddItem(new CustomCommandItem("qcompile(", "2 - Compile source to Quick Load File"));
            autocompleteMenu1.AddItem(new CustomCommandItem("qcompile(", "2 - Compile source to Quick Load File"));
            autocompleteMenu1.AddItem(new CustomCommandItem("qsave_program(", "2 - Create runtime application"));
            autocompleteMenu1.AddItem(new CustomCommandItem("qsave_program(", "2 - Create runtime application"));
            autocompleteMenu1.AddItem(new CustomCommandItem("quasi_quotation_syntax(", "2 - Declare quasi quotation syntax"));
            autocompleteMenu1.AddItem(new CustomCommandItem("quasi_quotation_syntax_error(", "2 - Raise syntax error"));
            autocompleteMenu1.AddItem(new CustomCommandItem("radial_restraint(", "0 - Tabbling radial restraint was violated"));
            autocompleteMenu1.AddItem(new CustomCommandItem("random_property(", "2 - Query properties of random generation"));
            autocompleteMenu1.AddItem(new CustomCommandItem("rational(", "2 - Type check for a rational number"));
            autocompleteMenu1.AddItem(new CustomCommandItem("rational(", "3 - Decompose a rational"));
            autocompleteMenu1.AddItem(new CustomCommandItem("read(", "2 - Read Prolog term"));
            autocompleteMenu1.AddItem(new CustomCommandItem("read(", "2 - Read Prolog term from stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("read_clause(", "3 - Read clause from stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("read_link(", "3 - Read a symbolic link"));
            autocompleteMenu1.AddItem(new CustomCommandItem("read_pending_codes(", "3 - Fetch buffered input from a stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("read_pending_chars(", "3 - Fetch buffered input from a stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("read_string(", "3 - Read a number of characters into a string"));
            autocompleteMenu1.AddItem(new CustomCommandItem("read_string(", "5 - Read string upto a delimiter"));
            autocompleteMenu1.AddItem(new CustomCommandItem("read_term(", "2 - Read term with options"));
            autocompleteMenu1.AddItem(new CustomCommandItem("read_term(", "3 - Read term with options from stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("read_term_from_atom(", "3 - Read term with options from atom"));
            autocompleteMenu1.AddItem(new CustomCommandItem("read_term_with_history(", "2 - Read term with command line history"));
            autocompleteMenu1.AddItem(new CustomCommandItem("recorda(", "2 - Record term in the database (first)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("recorda(", "3 - Record term in the database (first)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("recorded(", "2 - Obtain term from the database"));
            autocompleteMenu1.AddItem(new CustomCommandItem("recorded(", "3 - Obtain term from the database"));
            autocompleteMenu1.AddItem(new CustomCommandItem("recordz(", "2 - Record term in the database (last)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("recordz(", "3 - Record term in the database (last)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("redefine_system_predicate(", "2 - Abolish system definition"));
            autocompleteMenu1.AddItem(new CustomCommandItem("reexport(", "2 - Load files and re-export the imported predicates"));
            autocompleteMenu1.AddItem(new CustomCommandItem("reexport(", "2 - Load predicates from a file and re-export it"));
            autocompleteMenu1.AddItem(new CustomCommandItem("reload_foreign_libraries(", "0 - Reload DLLs/shared objects"));
            autocompleteMenu1.AddItem(new CustomCommandItem("reload_library_index(", "0 - Force reloading the autoload index"));
            autocompleteMenu1.AddItem(new CustomCommandItem("rename_file(", "2 - Change name of file"));
            autocompleteMenu1.AddItem(new CustomCommandItem("repeat(", "0 - Succeed, leaving infinite backtrack points"));
            autocompleteMenu1.AddItem(new CustomCommandItem("require(", "2 - This file requires these predicates"));
            autocompleteMenu1.AddItem(new CustomCommandItem("reset(", "3 - Wrapper for delimited continuations"));
            autocompleteMenu1.AddItem(new CustomCommandItem("reset_gensym(", "2 - Reset a gensym key"));
            autocompleteMenu1.AddItem(new CustomCommandItem("reset_gensym(", "0 - Reset all gensym keys"));
            autocompleteMenu1.AddItem(new CustomCommandItem("reset_profiler(", "0 - Clear statistics obtained by the profiler"));
            autocompleteMenu1.AddItem(new CustomCommandItem("resource(", "2 - Declare a program resource"));
            autocompleteMenu1.AddItem(new CustomCommandItem("resource(", "3 - Declare a program resource"));
            autocompleteMenu1.AddItem(new CustomCommandItem("retract(", "2 - Remove clause from the database"));
            autocompleteMenu1.AddItem(new CustomCommandItem("retractall(", "2 - Remove unifying clauses from the database"));
            autocompleteMenu1.AddItem(new CustomCommandItem("same_file(", "2 - Succeeds if arguments refer to same file"));
            autocompleteMenu1.AddItem(new CustomCommandItem("same_term(", "2 - Test terms to be at the same address"));
            autocompleteMenu1.AddItem(new CustomCommandItem("see(", "2 - Change the current input stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("seeing(", "2 - Query the current input stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("seek(", "4 - Modify the current position in a stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("seen(", "0 - Close the current input stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("select_dict(", "2 - Select matching attributes from a dict"));
            autocompleteMenu1.AddItem(new CustomCommandItem("select_dict(", "3 - Select matching attributes from a dict"));
            autocompleteMenu1.AddItem(new CustomCommandItem("set_end_of_stream(", "2 - Set physical end of an open file"));
            autocompleteMenu1.AddItem(new CustomCommandItem("set_flag(", "2 - Set value of a flag"));
            autocompleteMenu1.AddItem(new CustomCommandItem("set_input(", "2 - Set current input stream from a stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("set_locale(", "2 - Set the default local"));
            autocompleteMenu1.AddItem(new CustomCommandItem("set_malloc(", "2 - Set memory allocator property"));
            autocompleteMenu1.AddItem(new CustomCommandItem("set_module(", "2 - Set properties of a module"));
            autocompleteMenu1.AddItem(new CustomCommandItem("set_output(", "2 - Set current output stream from a stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("set_prolog_IO(", "3 - Prepare streams for interactive session"));
            autocompleteMenu1.AddItem(new CustomCommandItem("set_prolog_flag(", "2 - Define a system feature"));
            autocompleteMenu1.AddItem(new CustomCommandItem("set_prolog_gc_thread(", "2 - Control the gc thread"));
            autocompleteMenu1.AddItem(new CustomCommandItem("set_prolog_stack(", "2 - Modify stack characteristics"));
            autocompleteMenu1.AddItem(new CustomCommandItem("set_random(", "2 - Control random number generation"));
            autocompleteMenu1.AddItem(new CustomCommandItem("set_stream(", "2 - Set stream attribute"));
            autocompleteMenu1.AddItem(new CustomCommandItem("set_stream_position(", "2 - Seek stream to position"));
            autocompleteMenu1.AddItem(new CustomCommandItem("set_system_IO(", "3 - Rebind stdin/stderr/stdout"));
            autocompleteMenu1.AddItem(new CustomCommandItem("setup_call_cleanup(", "3 - Undo side-effects safely"));
            autocompleteMenu1.AddItem(new CustomCommandItem("setup_call_catcher_cleanup(", "4 - Undo side-effects safely"));
            autocompleteMenu1.AddItem(new CustomCommandItem("setarg(", "3 - Destructive assignment on term"));
            autocompleteMenu1.AddItem(new CustomCommandItem("setenv(", "2 - Set shell environment variable"));
            autocompleteMenu1.AddItem(new CustomCommandItem("setlocale(", "3 - Set/query C-library regional information"));
            autocompleteMenu1.AddItem(new CustomCommandItem("setof(", "3 - Find all unique solutions to a goal"));
            autocompleteMenu1.AddItem(new CustomCommandItem("shell(", "2 - Execute OS command"));
            autocompleteMenu1.AddItem(new CustomCommandItem("shell(", "2 - Execute OS command"));
            autocompleteMenu1.AddItem(new CustomCommandItem("shift(", "2 - Shift control to the closest reset/3"));
            autocompleteMenu1.AddItem(new CustomCommandItem("shift_for_copy(", "2 - Shift control to the closest reset/3"));
            autocompleteMenu1.AddItem(new CustomCommandItem("show_profile(", "2 - Show results of the profiler"));
            autocompleteMenu1.AddItem(new CustomCommandItem("sig_atomic(", "2 - Run goal without handling signals"));
            autocompleteMenu1.AddItem(new CustomCommandItem("sig_block(", "2 - Block matching thread signals"));
            autocompleteMenu1.AddItem(new CustomCommandItem("sig_pending(", "2 - Query pending signals"));
            autocompleteMenu1.AddItem(new CustomCommandItem("sig_remove(", "2 - Remove pending signals"));
            autocompleteMenu1.AddItem(new CustomCommandItem("sig_unblock(", "2 - Unblock matching thread signals"));
            autocompleteMenu1.AddItem(new CustomCommandItem("size_abstract_term(", "3 - Abstract a term (tabling support)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("size_file(", "2 - Get size of a file in characters"));
            autocompleteMenu1.AddItem(new CustomCommandItem("size_nb_set(", "2 - Determine size of non-backtrackable set"));
            autocompleteMenu1.AddItem(new CustomCommandItem("skip(", "2 - Skip to character in current input"));
            autocompleteMenu1.AddItem(new CustomCommandItem("skip(", "2 - Skip to character on stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("sleep(", "2 - Suspend execution for specified time"));
            autocompleteMenu1.AddItem(new CustomCommandItem("snapshot(", "2 - Run goal in isolation"));
            autocompleteMenu1.AddItem(new CustomCommandItem("sort(", "2 - Sort elements in a list"));
            autocompleteMenu1.AddItem(new CustomCommandItem("sort(", "4 - Sort elements in a list"));
            autocompleteMenu1.AddItem(new CustomCommandItem("source_exports(", "2 - Check whether source exports a predicate"));
            autocompleteMenu1.AddItem(new CustomCommandItem("source_file(", "2 - Examine currently loaded source files"));
            autocompleteMenu1.AddItem(new CustomCommandItem("source_file(", "2 - Obtain source file of predicate"));
            autocompleteMenu1.AddItem(new CustomCommandItem("source_file_property(", "2 - Information about loaded files"));
            autocompleteMenu1.AddItem(new CustomCommandItem("source_location(", "2 - Location of last read term"));
            autocompleteMenu1.AddItem(new CustomCommandItem("split_string(", "4 - Break a string into substrings"));
            autocompleteMenu1.AddItem(new CustomCommandItem("spy(", "2 - Force tracer on specified predicate"));
            autocompleteMenu1.AddItem(new CustomCommandItem("stamp_date_time(", "3 - Convert time-stamp to date structure"));
            autocompleteMenu1.AddItem(new CustomCommandItem("statistics(", "2 - Obtain collected statistics"));
            autocompleteMenu1.AddItem(new CustomCommandItem("stream_pair(", "3 - Create/examine a bi-directional stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("stream_position_data(", "3 - Access fields from stream position"));
            autocompleteMenu1.AddItem(new CustomCommandItem("stream_property(", "2 - Get stream properties"));
            autocompleteMenu1.AddItem(new CustomCommandItem("string(", "2 - Type check for string"));
            autocompleteMenu1.AddItem(new CustomCommandItem("string_bytes(", "3 - Translates between text and bytes in encoding"));
            autocompleteMenu1.AddItem(new CustomCommandItem("string_concat(", "3 - atom_concat/3 for strings"));
            autocompleteMenu1.AddItem(new CustomCommandItem("string_length(", "2 - Determine length of a string"));
            autocompleteMenu1.AddItem(new CustomCommandItem("string_chars(", "2 - Conversion between string and list of characters"));
            autocompleteMenu1.AddItem(new CustomCommandItem("string_codes(", "2 - Conversion between string and list of character codes"));
            autocompleteMenu1.AddItem(new CustomCommandItem("string_code(", "3 - Get or find a character code in a string"));
            autocompleteMenu1.AddItem(new CustomCommandItem("string_lower(", "2 - Case conversion to lower case"));
            autocompleteMenu1.AddItem(new CustomCommandItem("string_upper(", "2 - Case conversion to upper case"));
            autocompleteMenu1.AddItem(new CustomCommandItem("string_predicate(", "2 - (hook) Predicate contains strings"));
            autocompleteMenu1.AddItem(new CustomCommandItem("strip_module(", "3 - Extract context module and term"));
            autocompleteMenu1.AddItem(new CustomCommandItem("style_check(", "2 - Change level of warnings"));
            autocompleteMenu1.AddItem(new CustomCommandItem("sub_atom(", "5 - Take a substring from an atom"));
            autocompleteMenu1.AddItem(new CustomCommandItem("sub_atom_icasechk(", "3 - Case insensitive substring match"));
            autocompleteMenu1.AddItem(new CustomCommandItem("sub_string(", "5 - Take a substring from a string"));
            autocompleteMenu1.AddItem(new CustomCommandItem("subsumes_term(", "2 - One-sided unification test"));
            autocompleteMenu1.AddItem(new CustomCommandItem("succ(", "2 - Logical integer successor relation"));
            autocompleteMenu1.AddItem(new CustomCommandItem("swritef(", "2 - Formatted write on a string"));
            autocompleteMenu1.AddItem(new CustomCommandItem("swritef(", "3 - Formatted write on a string"));
            autocompleteMenu1.AddItem(new CustomCommandItem("tab(", "2 - Output number of spaces"));
            autocompleteMenu1.AddItem(new CustomCommandItem("tab(", "2 - Output number of spaces on a stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("table(", "2 - Declare predicate to be tabled"));
            autocompleteMenu1.AddItem(new CustomCommandItem("tabled_call(", "2 - Helper for not_exists/1"));
            autocompleteMenu1.AddItem(new CustomCommandItem("tdebug(", "0 - Switch all threads into debug mode"));
            autocompleteMenu1.AddItem(new CustomCommandItem("tdebug(", "2 - Switch a thread into debug mode"));
            autocompleteMenu1.AddItem(new CustomCommandItem("tell(", "2 - Change current output stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("telling(", "2 - Query current output stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("term_expansion(", "2 - (hook) Convert term before compilation"));
            autocompleteMenu1.AddItem(new CustomCommandItem("term_expansion(", "4 - (hook) Convert term before compilation"));
            autocompleteMenu1.AddItem(new CustomCommandItem("term_singletons(", "2 - Find singleton variables in a term"));
            autocompleteMenu1.AddItem(new CustomCommandItem("term_string(", "2 - Read/write a term from/to a string"));
            autocompleteMenu1.AddItem(new CustomCommandItem("term_string(", "3 - Read/write a term from/to a string"));
            autocompleteMenu1.AddItem(new CustomCommandItem("term_subsumer(", "3 - Most specific generalization of two terms"));
            autocompleteMenu1.AddItem(new CustomCommandItem("term_to_atom(", "2 - Convert between term and atom"));
            autocompleteMenu1.AddItem(new CustomCommandItem("thread_affinity(", "3 - Query and control the affinity mask"));
            autocompleteMenu1.AddItem(new CustomCommandItem("thread_alias(", "2 - Set the alias name of a thread"));
            autocompleteMenu1.AddItem(new CustomCommandItem("thread_at_exit(", "2 - Register goal to be called at exit"));
            autocompleteMenu1.AddItem(new CustomCommandItem("thread_create(", "2 - Create a new Prolog task"));
            autocompleteMenu1.AddItem(new CustomCommandItem("thread_create(", "3 - Create a new Prolog task"));
            autocompleteMenu1.AddItem(new CustomCommandItem("thread_detach(", "2 - Make thread cleanup after completion"));
            autocompleteMenu1.AddItem(new CustomCommandItem("thread_exit(", "2 - Terminate Prolog task with value"));
            autocompleteMenu1.AddItem(new CustomCommandItem("thread_get_message(", "2 - Wait for message"));
            autocompleteMenu1.AddItem(new CustomCommandItem("thread_get_message(", "2 - Wait for message in a queue"));
            autocompleteMenu1.AddItem(new CustomCommandItem("thread_get_message(", "3 - Wait for message in a queue"));
            autocompleteMenu1.AddItem(new CustomCommandItem("thread_idle(", "2 - Reduce footprint while waiting"));
            autocompleteMenu1.AddItem(new CustomCommandItem("thread_initialization(", "2 - Run action at start of thread"));
            autocompleteMenu1.AddItem(new CustomCommandItem("thread_join(", "2 - Wait for Prolog task-completion"));
            autocompleteMenu1.AddItem(new CustomCommandItem("thread_join(", "2 - Wait for Prolog task-completion"));
            autocompleteMenu1.AddItem(new CustomCommandItem("thread_local(", "2 - Declare thread-specific clauses for a predicate"));
            autocompleteMenu1.AddItem(new CustomCommandItem("thread_message_hook(", "3 - Thread local message_hook/3"));
            autocompleteMenu1.AddItem(new CustomCommandItem("thread_peek_message(", "2 - Test for message"));
            autocompleteMenu1.AddItem(new CustomCommandItem("thread_peek_message(", "2 - Test for message in a queue"));
            autocompleteMenu1.AddItem(new CustomCommandItem("thread_property(", "2 - Examine Prolog threads"));
            autocompleteMenu1.AddItem(new CustomCommandItem("thread_self(", "2 - Get identifier of current thread"));
            autocompleteMenu1.AddItem(new CustomCommandItem("thread_send_message(", "2 - Send message to another thread"));
            autocompleteMenu1.AddItem(new CustomCommandItem("thread_send_message(", "3 - Send message to another thread"));
            autocompleteMenu1.AddItem(new CustomCommandItem("thread_setconcurrency(", "2 - Number of active threads"));
            autocompleteMenu1.AddItem(new CustomCommandItem("thread_signal(", "2 - Execute goal in another thread"));
            autocompleteMenu1.AddItem(new CustomCommandItem("thread_statistics(", "3 - Get statistics of another thread"));
            autocompleteMenu1.AddItem(new CustomCommandItem("thread_update(", "2 - Update a module and signal waiters"));
            autocompleteMenu1.AddItem(new CustomCommandItem("thread_wait(", "2 - Wait for a goal to become true"));
            autocompleteMenu1.AddItem(new CustomCommandItem("threads(", "0 - List running threads"));
            autocompleteMenu1.AddItem(new CustomCommandItem("throw(", "2 - Raise an exception (see catch/3)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("time(", "2 - Determine time needed to execute goal"));
            autocompleteMenu1.AddItem(new CustomCommandItem("time_file(", "2 - Get last modification time of file"));
            autocompleteMenu1.AddItem(new CustomCommandItem("tmp_file(", "2 - Create a temporary filename"));
            autocompleteMenu1.AddItem(new CustomCommandItem("tmp_file_stream(", "3 - Create a temporary file and open it"));
            autocompleteMenu1.AddItem(new CustomCommandItem("tnodebug(", "0 - Switch off debug mode in all threads"));
            autocompleteMenu1.AddItem(new CustomCommandItem("tnodebug(", "2 - Switch off debug mode in a thread"));
            autocompleteMenu1.AddItem(new CustomCommandItem("tnot(", "2 - Tabled negation"));
            autocompleteMenu1.AddItem(new CustomCommandItem("told(", "0 - Close current output"));
            autocompleteMenu1.AddItem(new CustomCommandItem("tprofile(", "2 - Profile a thread for some period"));
            autocompleteMenu1.AddItem(new CustomCommandItem("trace(", "0 - Start the tracer"));
            autocompleteMenu1.AddItem(new CustomCommandItem("tracing(", "0 - Query status of the tracer"));
            autocompleteMenu1.AddItem(new CustomCommandItem("transaction(", "2 - Run goal in a transaction"));
            autocompleteMenu1.AddItem(new CustomCommandItem("transaction(", "2 - Run goal in a transaction"));
            autocompleteMenu1.AddItem(new CustomCommandItem("transaction(", "3 - Run goal in a transaction"));
            autocompleteMenu1.AddItem(new CustomCommandItem("transaction_updates(", "2 - Updates to be committed in a transaction"));
            autocompleteMenu1.AddItem(new CustomCommandItem("trie_delete(", "3 - Remove term from trie"));
            autocompleteMenu1.AddItem(new CustomCommandItem("trie_destroy(", "2 - Destroy a trie"));
            autocompleteMenu1.AddItem(new CustomCommandItem("trie_gen(", "3 - Get all terms from a trie"));
            autocompleteMenu1.AddItem(new CustomCommandItem("trie_gen_compiled(", "2 - Get all terms from a trie"));
            autocompleteMenu1.AddItem(new CustomCommandItem("trie_gen_compiled(", "3 - Get all terms from a trie"));
            autocompleteMenu1.AddItem(new CustomCommandItem("trie_insert(", "2 - Insert term into a trie"));
            autocompleteMenu1.AddItem(new CustomCommandItem("trie_insert(", "3 - Insert term into a trie"));
            autocompleteMenu1.AddItem(new CustomCommandItem("trie_insert(", "4 - Insert term into a trie"));
            autocompleteMenu1.AddItem(new CustomCommandItem("trie_lookup(", "3 - Lookup a term in a trie"));
            autocompleteMenu1.AddItem(new CustomCommandItem("trie_new(", "2 - Create a trie"));
            autocompleteMenu1.AddItem(new CustomCommandItem("trie_property(", "2 - Examine a trie's properties"));
            autocompleteMenu1.AddItem(new CustomCommandItem("trie_update(", "3 - Update associated value in trie"));
            autocompleteMenu1.AddItem(new CustomCommandItem("trie_term(", "2 - Get term from a trie by handle"));
            autocompleteMenu1.AddItem(new CustomCommandItem("trim_heap(", "0 - Release unused malloc() resources"));
            autocompleteMenu1.AddItem(new CustomCommandItem("trim_stacks(", "0 - Release unused stack resources"));
            autocompleteMenu1.AddItem(new CustomCommandItem("tripwire(", "2 - (hook) Handle a tabling tripwire event"));
            autocompleteMenu1.AddItem(new CustomCommandItem("true(", "0 - Succeed"));
            autocompleteMenu1.AddItem(new CustomCommandItem("tspy(", "2 - Set spy point and enable debugging in all threads"));
            autocompleteMenu1.AddItem(new CustomCommandItem("tspy(", "2 - Set spy point and enable debugging in a thread"));
            autocompleteMenu1.AddItem(new CustomCommandItem("tty_get_capability(", "3 - Get terminal parameter"));
            autocompleteMenu1.AddItem(new CustomCommandItem("tty_goto(", "2 - Goto position on screen"));
            autocompleteMenu1.AddItem(new CustomCommandItem("tty_put(", "2 - Write control string to terminal"));
            autocompleteMenu1.AddItem(new CustomCommandItem("tty_size(", "2 - Get row/column size of the terminal"));
            autocompleteMenu1.AddItem(new CustomCommandItem("ttyflush(", "0 - Flush output on terminal"));
            autocompleteMenu1.AddItem(new CustomCommandItem("undefined(", "0 - Well Founded Semantics: true nor false"));
            autocompleteMenu1.AddItem(new CustomCommandItem("undo(", "2 - Schedule goal for backtracking"));
            autocompleteMenu1.AddItem(new CustomCommandItem("unify_with_occurs_check(", "2 - Logically sound unification"));
            autocompleteMenu1.AddItem(new CustomCommandItem("unifiable(", "3 - Determining binding required for unification"));
            autocompleteMenu1.AddItem(new CustomCommandItem("unknown(", "2 - Trap undefined predicates"));
            autocompleteMenu1.AddItem(new CustomCommandItem("unload_file(", "2 - Unload a source file"));
            autocompleteMenu1.AddItem(new CustomCommandItem("unload_foreign_library(", "2 - library(shlib) Detach shared library (.so file)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("unload_foreign_library(", "2 - library(shlib) Detach shared library (.so file)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("unsetenv(", "2 - Delete shell environment variable"));
            autocompleteMenu1.AddItem(new CustomCommandItem("untable(", "2 - Remove tabling instrumentation"));
            autocompleteMenu1.AddItem(new CustomCommandItem("upcase_atom(", "2 - Convert atom to upper-case"));
            autocompleteMenu1.AddItem(new CustomCommandItem("use_foreign_library(", "2 - Load DLL/shared object (directive)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("use_foreign_library(", "2 - Load DLL/shared object (directive)"));
            autocompleteMenu1.AddItem(new CustomCommandItem("use_module(", "2 - Import a module"));
            autocompleteMenu1.AddItem(new CustomCommandItem("use_module(", "2 - Import predicates from a module"));
            autocompleteMenu1.AddItem(new CustomCommandItem("valid_string_goal(", "2 - (hook) Goal handles strings"));
            autocompleteMenu1.AddItem(new CustomCommandItem("var(", "2 - Type check for unbound variable"));
            autocompleteMenu1.AddItem(new CustomCommandItem("var_number(", "2 - Check that var is numbered by numbervars"));
            autocompleteMenu1.AddItem(new CustomCommandItem("var_property(", "2 - Variable properties during macro expansion"));
            autocompleteMenu1.AddItem(new CustomCommandItem("variant_sha1(", "2 - Term-hash for term-variants"));
            autocompleteMenu1.AddItem(new CustomCommandItem("variant_hash(", "2 - Term-hash for term-variants"));
            autocompleteMenu1.AddItem(new CustomCommandItem("version(", "0 - Print system banner message"));
            autocompleteMenu1.AddItem(new CustomCommandItem("version(", "2 - Add messages to the system banner"));
            autocompleteMenu1.AddItem(new CustomCommandItem("visible(", "2 - Ports that are visible in the tracer"));
            autocompleteMenu1.AddItem(new CustomCommandItem("volatile(", "2 - Predicates that are not saved"));
            autocompleteMenu1.AddItem(new CustomCommandItem("wait_for_input(", "3 - Wait for input with optional timeout"));
            autocompleteMenu1.AddItem(new CustomCommandItem("when(", "2 - Execute goal when condition becomes true"));
            autocompleteMenu1.AddItem(new CustomCommandItem("wildcard_match(", "2 - POSIX style glob pattern matching"));
            autocompleteMenu1.AddItem(new CustomCommandItem("wildcard_match(", "3 - POSIX style glob pattern matching"));
            autocompleteMenu1.AddItem(new CustomCommandItem("win_add_dll_directory(", "2 - Add directory to DLL search path"));
            autocompleteMenu1.AddItem(new CustomCommandItem("win_add_dll_directory(", "2 - Add directory to DLL search path"));
            autocompleteMenu1.AddItem(new CustomCommandItem("win_remove_dll_directory(", "2 - Remove directory from DLL search path"));
            autocompleteMenu1.AddItem(new CustomCommandItem("win_exec(", "2 - Win32: spawn Windows task"));
            autocompleteMenu1.AddItem(new CustomCommandItem("win_has_menu(", "0 - Win32: true if console menu is available"));
            autocompleteMenu1.AddItem(new CustomCommandItem("win_folder(", "2 - Win32: get special folder by CSIDL"));
            autocompleteMenu1.AddItem(new CustomCommandItem("win_insert_menu(", "2 - swipl-win.exe: add menu"));
            autocompleteMenu1.AddItem(new CustomCommandItem("win_insert_menu_item(", "4 - swipl-win.exe: add item to menu"));
            autocompleteMenu1.AddItem(new CustomCommandItem("win_process_modules(", "2 - Win32 get .exe and .dll files of the process"));
            autocompleteMenu1.AddItem(new CustomCommandItem("win_shell(", "2 - Win32: open document through Shell"));
            autocompleteMenu1.AddItem(new CustomCommandItem("win_shell(", "3 - Win32: open document through Shell"));
            autocompleteMenu1.AddItem(new CustomCommandItem("win_registry_get_value(", "3 - Win32: get registry value"));
            autocompleteMenu1.AddItem(new CustomCommandItem("win_get_user_preferred_ui_languages(", "2 - Win32: get language preferences"));
            autocompleteMenu1.AddItem(new CustomCommandItem("win_window_color(", "2 - Win32: change colors of console window"));
            autocompleteMenu1.AddItem(new CustomCommandItem("win_window_pos(", "2 - Win32: change size and position of window"));
            autocompleteMenu1.AddItem(new CustomCommandItem("window_title(", "2 - Win32: change title of window"));
            autocompleteMenu1.AddItem(new CustomCommandItem("with_mutex(", "2 - Run goal while holding mutex"));
            autocompleteMenu1.AddItem(new CustomCommandItem("with_output_to(", "2 - Write to strings and more"));
            autocompleteMenu1.AddItem(new CustomCommandItem("with_quasi_quotation_input(", "3 - Parse quasi quotation from stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("with_tty_raw(", "2 - Run goal with terminal in raw mode"));
            autocompleteMenu1.AddItem(new CustomCommandItem("working_directory(", "2 - Query/change CWD"));
            autocompleteMenu1.AddItem(new CustomCommandItem("write(", "2 - Write term"));
            autocompleteMenu1.AddItem(new CustomCommandItem("write(", "2 - Write term to stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("writeln(", "2 - Write term, followed by a newline"));
            autocompleteMenu1.AddItem(new CustomCommandItem("writeln(", "2 - Write term, followed by a newline to a stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("write_canonical(", "2 - Write a term with quotes, ignore operators"));
            autocompleteMenu1.AddItem(new CustomCommandItem("write_canonical(", "2 - Write a term with quotes, ignore operators on a stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("write_length(", "3 - Dermine #characters to output a term"));
            autocompleteMenu1.AddItem(new CustomCommandItem("write_term(", "2 - Write term with options"));
            autocompleteMenu1.AddItem(new CustomCommandItem("write_term(", "3 - Write term with options to stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("writef(", "2 - Formatted write"));
            autocompleteMenu1.AddItem(new CustomCommandItem("writef(", "2 - Formatted write on stream"));
            autocompleteMenu1.AddItem(new CustomCommandItem("writeq(", "2 - Write term, insert quotes"));
            autocompleteMenu1.AddItem(new CustomCommandItem("writeq(", "2 - Write term, insert quotes on stream"));


        }

        private void toolStripLabel4_Click(object sender, EventArgs e)
        { CurrentTB.Undo(); }

        private void toolStripLabel5_Click(object sender, EventArgs e)
        { CurrentTB.Paste(); }

        private void toolStripLabel6_Click(object sender, EventArgs e)
        { CurrentTB.Copy(); }

        private void toolStripLabel7_Click(object sender, EventArgs e)
        { CurrentTB.Cut(); }

        private string currentFilePath = string.Empty; //current file path

        private void toolStripLabel8_Click(object sender, EventArgs e)
        {
            try
            { SaveFile(currentFilePath); }
            catch (ArgumentException)
            { }
        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            openFile();  //open a file
            if (toolStripLabel3.Font.Size == 14.25) clickToolbar(this.toolStripLabel3, leftNew, rightNew, Properties.Settings.Default.leftPaddingToolbar, Properties.Settings.Default.rightPaddingToolbar); //try to reset the icon dimension - only for Fluent toolbar
            openClicked = false;
        }

        private void toolstripOpen_Click(object sender, EventArgs e)
        { openFile(); }

        //syntax menu in status bar
        private void checkSyntaxMenu()
        {
            switch (Properties.Settings.Default.syntaxChosen)
            {

                case "Prolog": //you've chosen Prolog
                    prologToolStripMenuItem.Checked = true; lispToolStripMenuItem.Checked = false;
                    yaccJToolStripMenuItem.Checked = false; jFlexToolStripMenuItem.Checked = false;
                    noneToolStripMenuItem.Checked = false; break;

                case "Lisp": //you've chosen Lisp
                    prologToolStripMenuItem.Checked = false; lispToolStripMenuItem.Checked = true; 
                    yaccJToolStripMenuItem.Checked = false; jFlexToolStripMenuItem.Checked = false;
                    noneToolStripMenuItem.Checked = false; break;

                case "yacc": //you've chosen yacc
                    prologToolStripMenuItem.Checked = false; lispToolStripMenuItem.Checked = false;
                    yaccJToolStripMenuItem.Checked = true; jFlexToolStripMenuItem.Checked = false;
                    noneToolStripMenuItem.Checked = false; break;

                case "jflex": //you've chosen jflex
                    prologToolStripMenuItem.Checked = false; lispToolStripMenuItem.Checked = false;
                    yaccJToolStripMenuItem.Checked = false; jFlexToolStripMenuItem.Checked = true;
                    noneToolStripMenuItem.Checked = false; break;

                case "None": //plain text
                    prologToolStripMenuItem.Checked = false; lispToolStripMenuItem.Checked = false;
                    yaccJToolStripMenuItem.Checked = false; jFlexToolStripMenuItem.Checked = false;
                    noneToolStripMenuItem.Checked = true; break;
            }
        }

        string filePath; string fileContent; string fileName;
        private void openFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog(); //create a new dialog
            openFileDialog.Filter = "Prolog source files (*.pl, *.pro)|*.pl;*.pro|Prolog consultable files (*.consult)|*.consult|LISP source files (*.lisp, *.l, *.cl, *.fasl)|*.lisp;*.l;*.cl;*.fasl|yacc files (*.y)|*.y|JFlex Source files (*.j, *.l)|*.j;*.l|XML files (*.xml)|*.xml|XAML files (*.xaml)|*.xaml|XML Document Type Definition files (*.dtd)|*.dtd|XML Schema Definition files (*.xsd)|*.xsd|C Files(*.c, *.cpp, *.cxx, *.cc, *.tli, *.tlh, *.h, *.hpp)|*.c;*.cpp;*.cxx;*.cc;*.tli;*.tlh;*.h;*.hpp|XML Extensible Stylesheet Language files (*.xsl)|*.xsl|C# source files (*.cs)|*.cs|Visual Basic source files (*.vb)|*.vb|Visual Basic .NET files (*.vbnet)|*.vbnet|HTML files (*.html, *.htm)|*.html;*.htm|SQL files (*.sql)|*.sql|PHP files (*.php)|*.php|Javascript files (*.js)|*.js|Lua files (*.lua)|*.lua|Rich Text Document files (*.rtf)|*.rtf|Plain Text files (*.txt)|*.txt|All files (*.*)|*.*"; //set extensions for dialog
            if (Properties.Settings.Default.syntaxFileExtensionOpen)
            {
                switch (Properties.Settings.Default.syntaxChosen)
                {
                    case "Prolog":
                        openFileDialog.FilterIndex = 1; break;

                    case "Lisp":
                        openFileDialog.FilterIndex = 3; break;

                    case "yacc":
                        openFileDialog.FilterIndex = 4; break;

                    case "jflex":
                        openFileDialog.FilterIndex = 5; break;
                    case "C":
                        openFileDialog.FilterIndex = 10; break;

                    case "None":
                        openFileDialog.FilterIndex = 20; break;
                }
            }
            else if (!Properties.Settings.Default.syntaxFileExtensionOpen) //if you don't have automatic extensions set up
            { openFileDialog.FilterIndex = Properties.Settings.Default.syntaxFileExtensionIndexOpen; //get from the properties which file extension you want 
            }
            openFileDialog.RestoreDirectory = true; //restore directory to the previous one you were in
            
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName; //file name
                fileContent = File.ReadAllText(filePath); //read all the contents of a file
                currentFilePath = openFileDialog.FileName; //current file path
                currentFilePath = Path.GetFileName(currentFilePath); //Get the name of the file path
                FarsiLibrary.Win.FATabStripItem tabCreated = CreateTab1(currentFilePath); //create a new tab with the 
                openClicked = false;
                try
                {
                    if (Properties.Settings.Default.emacsStyle) this.Text = currentFilePath + " - GNU Emacs at " + System.Windows.Forms.SystemInformation.ComputerName;
                    else { this.Text = Properties.Resources.AppName + " " + Properties.Resources.AppEdition + " - " + currentFilePath; }
                    toolStripStatusLabel4.Text = currentFilePath;
                    if (Properties.Settings.Default.syntaxOpenFileAuto)
                    {
                        if (openFileDialog.FileName.Contains(".pl") || openFileDialog.FileName.Contains(".pro")) Properties.Settings.Default.syntaxChosen = "Prolog";
                        else if (openFileDialog.FileName.Contains(".lisp") || openFileDialog.FileName.Contains(".lsp") || openFileDialog.FileName.Contains(".cl") || openFileDialog.FileName.Contains(".fasl")) Properties.Settings.Default.syntaxChosen = "Lisp";
                        else if (openFileDialog.FileName.Contains(".y")) { Properties.Settings.Default.syntaxChosen = "yacc"; }
                        else if (openFileDialog.FileName.Contains(".j") || openFileDialog.FileName.Contains(".l")) { Properties.Settings.Default.syntaxChosen = "jflex"; jFlexToolStripMenuItem.Checked = true; }
                        else { Properties.Settings.Default.syntaxChosen = "None"; }
                    }
                    else
                    {
                        if (Properties.Settings.Default.syntaxOpenDefaultMode != "Previous one") Properties.Settings.Default.syntaxChosen = Properties.Settings.Default.syntaxOpenDefaultMode;
                    }
                }
                catch (Exception)
                { toolStripStatusLabel.Text = ("Error"); }
            }
            if (!string.IsNullOrEmpty(currentFilePath) || !string.IsNullOrEmpty(currentFilePath)) //if there isn't a file opened then disable save
            { saveToolStripMenuItem.Enabled = false; }

            checkSyntaxMenu(); tabChangeCheck(); gramlexSendCheck();
        }

        private void SaveFileAsNew() //save as
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog(); //create a new save dialog
            saveFileDialog.Filter = "Prolog source files (*.pl, *.pro)|*.pl;*.pro|Prolog consultable files (*.consult)|*.consult|LISP source files (*.lisp, *.l, *.cl, *.fasl)|*.lisp;*.l;*.cl;*.fasl|yacc files (*.y)|*.y|JFlex Source files (*.j, *.l)|*.j;*.l|XML files (*.xml)|*.xml|XAML files (*.xaml)|*.xaml|XML Document Type Definition files (*.dtd)|*.dtd|XML Schema Definition files (*.xsd)|*.xsd|C Files(*.c, *.cpp, *.cxx, *.cc, *.tli, *.tlh, *.h, *.hpp)|*.c;*.cpp;*.cxx;*.cc;*.tli;*.tlh;*.h;*.hpp|XML Extensible Stylesheet Language files (*.xsl)|*.xsl|C# source files (*.cs)|*.cs|Visual Basic source files (*.vb)|*.vb|Visual Basic .NET files (*.vbnet)|*.vbnet|HTML files (*.html, *.htm)|*.html;*.htm|SQL files (*.sql)|*.sql|PHP files (*.php)|*.php|Javascript files (*.js)|*.js|Lua files (*.lua)|*.lua|Rich Text Document files (*.rtf)|*.rtf|Plain Text files (*.txt)|*.txt|All files (*.*)|*.*"; //set extensions for dialog
            if (Properties.Settings.Default.syntaxFileExtension)
            {
                switch (Properties.Settings.Default.syntaxChosen)
                {
                    case "Prolog": saveFileDialog.FilterIndex = 1; break;

                    case "Lisp": saveFileDialog.FilterIndex = 3; break;

                    case "yacc": saveFileDialog.FilterIndex = 4; break;

                    case "jflex": saveFileDialog.FilterIndex = 5; break;

                    case "C": saveFileDialog.FilterIndex = 10; break;

                    case "None": saveFileDialog.FilterIndex = 20;  break;
                }
            }
            else if (!Properties.Settings.Default.syntaxFileExtension)
            {
                saveFileDialog.FilterIndex = Properties.Settings.Default.syntaxFileExtensionIndex;
            }
            saveFileDialog.RestoreDirectory = true; //restory the directory to the precedent one you were in
            if (saveFileDialog.ShowDialog() == DialogResult.OK) //if you press ok
            {
                string filePath = saveFileDialog.FileName; //save the file path
                string textToSave = CurrentTB.Text; //the current textbox text is the one to write to the file
                File.WriteAllText(filePath, textToSave); //write the text to the file
                toolStripStatusLabel.Text = "Saved"; //specify to the user that you've saved it
                currentFilePath = filePath; //set the current file path as the one in which you've saved
                faTabStripItem1.Title = filePath; //set again the tab title
                CurrentTB.IsChanged = false;
                MDIParent1.ActiveForm.Text = MDIParent1.ActiveForm.Text.Replace("*", ""); //remove the asterisk from the titlebar to simbolize you have saved the document
            }    
        }

        private void SaveFile(string filePath) //save file
        {
            if (string.IsNullOrEmpty(currentFilePath) || string.IsNullOrEmpty(filePath))
            { SaveFileAsNew(); }
            String textToSave = CurrentTB.Text; //save the current text to a string
            File.WriteAllText(filePath, textToSave); //write it to a file
            toolStripStatusLabel.Text = "Saved"; //tell the user it's saved
            faTabStrip1.SelectedItem.Title = filePath; CurrentTB.IsChanged = false; timer.Start(); panel9.Visible = true;
            MDIParent1.ActiveForm.Text = MDIParent1.ActiveForm.Text.Replace("*", ""); //replace the asterisk that tells the user that a file is modified
        }

        private void Fastcolored1_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            CurrentTB.IsChanged = true;
            toolStripStatusLabel.Text = "Ready"; //modify the status label to be ready rather than save
            if (this.InvokeRequired)
            { this.BeginInvoke(new MethodInvoker(Asterisk)); }
            else { Asterisk(); }
            if (!string.IsNullOrEmpty(currentFilePath) || !string.IsNullOrEmpty(currentFilePath)) saveToolStripMenuItem.Enabled = true; //make it so you can save it
        }

        private void CurrentTB_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            toolStripStatusLabel.Text = "Ready";
            updateInterface();

            if (this.InvokeRequired)
            { this.BeginInvoke(new MethodInvoker(Asterisk)); }
            else { Asterisk(); }
            if (!string.IsNullOrEmpty(currentFilePath) || !string.IsNullOrEmpty(currentFilePath)) saveToolStripMenuItem.Enabled = true;

            if (Properties.Settings.Default.antoniottiStandard && currentColumn > Properties.Settings.Default.columnLineLimit)
                toolStripStatusLabel.Text = Properties.Settings.Default.antoniottiStandardText;
            else if (Properties.Settings.Default.antoniottiCrazy && currentColumn > Properties.Settings.Default.columnLineLimit)
                toolStripStatusLabel.Text = Properties.Settings.Default.antoniottiCrazyText;

            if (Properties.Settings.Default.syntaxChosen == "Prolog") applyPrologSyntax(e);
            else if (Properties.Settings.Default.syntaxChosen == "None") clearStyles(e);
            else if (Properties.Settings.Default.syntaxChosen == "Lisp") applyLispSyntax(e);
            else if (Properties.Settings.Default.syntaxChosen == "yacc") applyYaccSyntax(e);
            else if (Properties.Settings.Default.syntaxChosen == "jflex") applyJFlexSyntax(e);
            else if (Properties.Settings.Default.syntaxChosen == "C") applyCSyntax(e);
            e.ChangedRange.ClearStyle(blueStyle); e.ChangedRange.SetStyle(blueStyle, @"(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?");
            trackBar2.Maximum = CurrentTB.LinesCount; //change maximum lines dynamically every time
            try { CurrentTB.DescriptionFile = ""; }
            catch (NullReferenceException) { }
        }

        private void fctb_ToolTipNeeded(object sender, FastColoredTextBoxNS.ToolTipNeededEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.HoveredWord))
            {
                if (Properties.Settings.Default.syntaxChosen == "Prolog")
                {
                    switch (e.HoveredWord)
                    {
        case "!":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Cut (discard choicepoints";
        break;
    case "$":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Discard choicepoints and demand deterministic success";
        break;
    case ",":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Conjunction of goals";
        break;
    case "->":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - If-then-else";
        break;
    case "*->":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Soft-cut";
        break;
    case ".":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Consult. Also functional notation";
        break;
    case ":<":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Select keys from a dict";
        break;
    case ":=":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - WASM: Call JavaScript";
        break;
    case ";":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Disjunction of two goals";
        break;
    case "<":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Arithmetic smaller";
        break;
    case "=":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - True when arguments are unified";
        break;
    case "=..":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - 'Univ.' Term to list conversion";
        break;
    case "=:=": 
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Arithmetic equality";
        break;
    case "=<":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Arithmetic smaller or equal";
        break;
    case "==":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Test for strict equality";
        break;
    case "=@=": 
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Test for structural equality (variant)";
        break;
    case "=\\=": 
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Arithmetic not equal";
        break;
    case ">":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Arithmetic larger";
        break;
    case ">=":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Arithmetic larger or equal";
        break;
    case ">:<":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Partial dict unification";
        break;
    case "?=":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Test of terms can be compared now";
        break;
    case "@<":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Standard order smaller";
        break;
    case "@=<":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Standard order smaller or equal";
        break;
    case "@>":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Standard order larger";
        break;
    case "@>=":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Standard order larger or equal";
        break;
    case "\\+": 
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Negation by failure. Same as not/1";
        break;
    case "\\=":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - True if arguments cannot be unified";
        break;
    case "\\==":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - True if arguments are not strictly equal";
        break;
    case "\\=@=": 
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Not structural identical";
        break;
    case "^":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Existential quantification (bagof/3, setof/3)";
        break;
    case "|":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Disjunction in DCGs. Same as ;/2";
        break;
    case "": 
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - DCG escape; constraints";
        break;
    case "abolish": 
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Remove predicate definition from the database";
        break;
    case "abolish_all_tables": 
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Abolish computed tables";
        break;
    case "abolish_module_tables": 
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Abolish all tables in a module";
        break;
    case "abolish_monotonic_tables": 
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Abolish all monotonic tables";
        break;
    case "abolish_nonincremental_tables": 
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Abolish non-auttomatic tables";
        break;
   
    case "abolish_private_tables": 
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Abolish tables of this thread";
        break;
    case "abolish_shared_tables": 
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Abolish tables shared between threads";
        break;
    case "abolish_table_subgoals": 
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Abolish tables for a goal";
        break;
    case "abort": 
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Abort execution, return to top level";
        break;
    case "absolute_file_name": 
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Get absolute path name";
        break;
   
    case "answer_count_restraint": 
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Undefined answer due to max_answers";
        break;
    case "access_file": 
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Check access permissions of a file";
        break;
    case "acyclic_term": 
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Test term for cycles";
        break;
    case "add_import_module": 
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "3 - Add module to the auto-import list";
        break;
    case "add_nb_set": 
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Add term to a non-backtrackable set";
        break;
   
    case "append": 
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Append to a file";
        break;
    case "apple_current_locale_identifier": 
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Get Apple locale info";
        break;
    case "apply": 
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Call goal with additional arguments";
        break;
    case "apropos": 
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - library(online_help) Search manual";
        break;
    case "arg": 
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "3 - Access argument of a term";
        break;
    case "assoc_to_list": 
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Convert association tree to list";
        break;
    case "assert": 
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Add a clause to the database";
        break;
    
    case "asserta": 
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Add a clause to the database (first)";
        break;
    
    case "assertz": 
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Add a clause to the database (last)";
        break;
   
    case "assertion": 
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Make assertions about your program";
        break;
    
    case "attach_console": 
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Attach I/O console to thread";
        break;
    case "attach_packs": 
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Attach add-ons";
        break;
   
    case "attribute_goals":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "3 - Project attributes to goals";
        break;
    case "attr_unify_hook":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Attributed variable unification hook";
        break;
    case "attr_portray_hook":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Attributed variable print hook";
        break;
    case "attvar":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Type test for attributed variable";
        break;
    case "at_end_of_stream":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Test for end of file on input";
        break;
   
    case "at_halt":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Register goal to run at halt/1";
        break;
    case "atom":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Type check for an atom";
        break;
    case "atom_chars":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Convert between atom and list of characters";
        break;
    case "atom_codes":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Convert between atom and list of characters codes";
        break;
    case "atom_concat":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "3 - Contatenate two atoms";
        break;
    case "atom_length":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Determine length of an atom";
        break;
    case "atom_number":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Convert between atom and number";
        break;
    case "atom_prefix":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Test for start of atom";
        break;
    case "atom_string":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Conversion between atom and string";
        break;
    case "atom_to_term":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "3 - Convert between atom and term";
        break;
    case "atomic":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Type check for primitive";
        break;
    case "atomic_concat":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "3 - Concatenate two atomic values to an atom";
        break;
    case "atomic_list_concat":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Append a list of atomics";
        break;
    
    case "atomics_to_string":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Concatenate list of inputs to a string";
        break;
   
    case "autoload":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Declare a file for autoloading";
        break;
    
    case "autoload_all":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Autoload all predicates now";
        break;
    case "autoload_path":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Add directories for autoloading";
        break;
    case "await":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - WASM: Wait for a Promise";
        break;
    case "b_getval":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Fetch backtrackable global variable";
        break;
    case "b_set_dict":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "3 - Destructive assignment on a dict";
        break;
    case "b_setval":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Assign backtrackable global variable";
        break;
    case "bagof":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "3 - Find all solutions to a goal";
        break;
    case "between":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "3 - Integer range checking/generating";
        break;
    case "blob":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Type check for a blob";
        break;
    case "bounded_number":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "3 - Number between bounds";
        break;
    case "break":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Start interactive top level";
        break;
    case "break_hook":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "6 - (hook) Debugger hook";
        break;
    case "byte_count":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Byte-position in a stream";
        break;
    case "call":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Call a goal";
        break;
    case "call/[2..]":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "Call with additional arguments";
        break;
    case "call_cleanup":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Guard a goal with a cleanup-handler";
        break;
    case "call_dcg":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "3 - As phrase/3 without type checking";
        break;
    case "call_delays":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Get the condition associated with an answer";
        break;
    case "call_residue_vars":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Find residual attributed variables";
        break;
    case "call_residual_program":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Get residual program associated with an answer";
        break;
    case "call_shared_object_function":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - UNIX: Call C-function in shared (.so) file";
        break;
    case "call_with_depth_limit":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "3 - Prove goal with bounded depth";
        break;
    case "call_with_inference_limit":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "3 - Prove goal in limited inferences";
        break;
    case "callable":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Test for atom or compound term";
        break;
    case "cancel_halt":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Cancel halt/0 from an at_halt/1 hook";
        break;
    case "catch":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "3 - Call goal, watching for exceptions";
        break;
    case "char_code":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Convert between character and character code";
        break;
    case "char_conversion":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Provide mapping of input characters";
        break;
    case "char_type":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Classify characters";
        break;
    case "character_count":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Get character index on a stream";
        break;
    case "chdir":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Compatibility: change working directory";
        break;
    case "chr_constraint":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - CHR Constraint declaration";
        break;
    case "chr_show_store":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - List suspended CHR constraints";
        break;
    case "chr_trace":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Start CHR tracer";
        break;
    case "chr_type":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - CHR Type declaration";
        break;
    case "chr_notrace":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Stop CHR tracer";
        break;
    case "chr_leash":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Define CHR leashed ports";
        break;
    case "chr_option":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Specify CHR compilation options";
        break;
    case "clause":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Get clauses of a predicate";
        break;
   
    case "clause_property":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Get properties of a clause";
        break;
    case "close":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Close stream";
        break;
    
    case "close_dde_conversation":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Win32: Close DDE channel";
        break;
    case "close_shared_object":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - UNIX: Close shared library (.so file)";
        break;
    case "collation_key":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Sort key for locale dependent ordering";
        break;
    case "comment_hook":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "3 - (hook) handle comments in sources";
        break;
    case "compare":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "3 - Compare, using a predicate to determine the order";
        break;
    case "compile_aux_clauses":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Compile predicates for goal_expansion/2";
        break;
    case "compile_predicates":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Compile dynamic code to static";
        break;
    case "compiling":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Is this a compilation run?";
        break;
    case "compound":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Test for a compound term";
        break;
    case "compound_name_arity":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "3 - Name and arity of a compound term";
        break;
    case "compound_name_arguments":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "3 - Name and arguments of a compound term";
        break;
    case "code_type":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Classify a character-code";
        break;
    case "consult":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Read (compile) a Prolog source file";
        break;
    case "context_module":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Get context module of current goal";
        break;
    case "convert_time/8":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "Break time stamp into fields";
        break;
    case "convert_time":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Convert time stamp to string";
        break;
    case "copy_stream_data":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Copy all data from stream to stream";
        break;
    
    case "copy_predicate_clauses":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Copy clauses between predicates";
        break;
    case "copy_term":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Make a copy of a term";
        break;
    
    case "copy_term_nat":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Make a copy of a term without attributes";
        break;
    
    case "create_prolog_flag":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "3 - Create a new Prolog flag";
        break;
    case "current_arithmetic_function":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Examine evaluable functions";
        break;
    case "current_atom":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Examine existing atoms";
        break;
    case "current_blob":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Examine typed blobs";
        break;
    case "current_char_conversion":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Query input character mapping";
        break;
    case "current_engine":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Enumerate known engines";
        break;
    case "current_flag":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Examine existing flags";
        break;
    case "current_foreign_library":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - library(shlib) Examine loaded shared libraries (.so files)";
        break;
    case "current_format_predicate":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Enumerate user-defined format codes";
        break;
    case "current_functor":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Examine existing name/arity pairs";
        break;
    case "current_input":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Get current input stream";
        break;
    case "current_key":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Examine existing database keys";
        break;
    case "current_locale":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Get the current locale";
        break;
    case "current_module":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Examine existing modules";
        break;
    case "current_op":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "3 - Examine current operator declarations";
        break;
    case "current_output":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Get the current output stream";
        break;
    case "current_predicate":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Examine existing predicates (ISO)";
        break;
   
    case "current_signal":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "3 - Current software signal mapping";
        break;
    case "current_stream":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "3 - Examine open streams";
        break;
    case "current_table":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Find answer table for a variant";
        break;
    case "current_transaction":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Detect encapsulating transactions";
        break;
    case "current_trie":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Enumerate known tries";
        break;
    case "cyclic_term":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Test term for cycles";
        break;
    case "day_of_the_week":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Determine ordinal-day from date";
        break;
    case "date_time_stamp":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Convert date structure to time-stamp";
        break;
    case "date_time_value":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "3 - Extract info from a date structure";
        break;
    case "dcg_translate_rule":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Source translation of DCG rules";
        break;
    
    case "dde_current_connection":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Win32: Examine open DDE connections";
        break;
    case "dde_current_service":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Win32: Examine DDE services provided";
        break;
    case "dde_execute":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Win32: Execute command on DDE server";
        break;
    case "dde_register_service":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Win32: Become a DDE server";
        break;
    case "dde_request":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "3 - Win32: Make a DDE request";
        break;
    case "dde_poke":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "3 - Win32: POKE operation on DDE server";
        break;
    case "dde_unregister_service":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Win32: Terminate a DDE service";
        break;
    case "debug":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Test for debugging mode";
        break;
    
    case "debug_control_hook":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - (hook) Extend spy/1, etc.";
        break;
    case "debugging":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Show debugger status";
        break;
    
    case "default_module":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Query module inheritance";
        break;
    case "del_attr":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Delete attribute from variable";
        break;
    case "del_attrs":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Delete all attributes from variable";
        break;
    case "del_dict":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "4 - Delete Key-Value pair from a dict";
        break;
    case "delays_residual_program":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Get the residual program for an answer";
        break;
    case "delete_directory":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Remove a folder from the file system";
        break;
    case "delete_file":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Remove a file from the file system";
        break;
    case "delete_import_module":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Remove module from import list";
        break;
    case "det":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Declare predicates as deterministic";
        break;
    case "deterministic":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Test deterministicy of current clause";
        break;
    case "dif":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Constrain two terms to be different";
        break;
    case "directory_files":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Get entries of a directory/folder";
        break;
    case "discontiguous":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Indicate distributed definition of a predicate";
        break;
    case "divmod":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "4 - Compute quotient and remainder of two integers";
        break;
    case "downcase_atom":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Convert atom to lower-case";
        break;
    case "duplicate_term":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Create a copy of a term";
        break;
    case "dwim_match":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Atoms match in 'Do What I Mean' sense";
        break;
   
    case "dwim_predicate":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Find predicate in 'Do What I Mean' sense";
        break;
    case "dynamic":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Indicate predicate definition may change";
        break;
    case "edit":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Edit current script- or associated file";
        break;
   
    case "elif":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Part of conditional compilation (directive)";
        break;
    case "else":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Part of conditional compilation (directive)";
        break;
    case "empty_assoc":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Create/test an empty association tree";
        break;
    case "empty_nb_set":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Test/create an empty non-backtrackable set";
        break;
    case "encoding":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Define encoding inside a source file";
        break;
    case "endif":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - End of conditional compilation (directive)";
        break;
    case "engine_create":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "3 - Create an interactor";
        break;
   
    case "engine_destroy":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Destroy an interactor";
        break;
    case "engine_fetch":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Get term from caller";
        break;
    case "engine_next":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Ask interactor for the next term";
        break;
    case "engine_next_reified":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Ask interactor for the next term";
        break;
    case "engine_post":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Send term to an interactor";
        break;
    
    case "engine_self":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Get handle to running interactor";
        break;
    case "engine_yield":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Make term available to caller";
        break;
    case "ensure_loaded":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Consult a file if that has not yet been done";
        break;
    case "erase":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Erase a database record or clause";
        break;
    case "exception":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "3 - (hook) Handle runtime exceptions";
        break;
    case "exists_directory":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Check existence of directory";
        break;
    case "exists_file":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Check existence of file";
        break;
    case "exists_source":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Check existence of a Prolog source";
        break;
   
    case "expand_answer":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Expand answer of query";
        break;
    case "expand_file_name":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Wildcard expansion of file names";
        break;
    case "expand_file_search_path":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Wildcard expansion of file paths";
        break;
    case "expand_goal":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Compiler: expand goal in clause-body";
        break;
   
    case "expand_query":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "4 - Expanded entered query";
        break;
    case "expand_term":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Compiler: expand read term into clause(s)";
        break;
   
    case "expects_dialect":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - For which Prolog dialect is this code written?";
        break;
    case "explain":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - library(explain) Explain argument";
        break;
    
    case "export":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Export a predicate from a module";
        break;
    case "fail":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Always false";
        break;
    case "false":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Always false";
        break;
    case "fast_term_serialized":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Fast term (de-)serialization";
        break;
    case "fast_read":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Read binary term serialization";
        break;
    case "fast_write":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Write binary term serialization";
        break;
    case "current_prolog_flag":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Get system configuration parameters";
        break;
    case "file_base_name":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Get file part of path";
        break;
    case "file_directory_name":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Get directory part of path";
        break;
    case "file_name_extension":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "3 - Add, remove, or test file extensions";
        break;
    case "file_search_path":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Define path-aliases for locating files";
        break;
    case "find_chr_constraint":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Returns a constraint from the store";
        break;
    case "findall":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "3 - Find all solutions to a goal";
        break;
   
    case "findnsols":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "4 - Find first N solutions";
        break;
   
    case "fill_buffer":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Fill the input buffer of a stream";
        break;
    case "flag":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "3 - Simple global variable system";
        break;
    case "float":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Type check for a floating-point number";
        break;
    case "float_class":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Classify (special) floats";
        break;
    case "float_parts":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "4 - Get mantissa and exponent of a float";
        break;
    case "flush_output":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Output pending characters on the current stream";
        break;
    
    case "forall":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Prove goal for all solutions of another goal";
        break;
    case "format":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Formatted output";
        break;
  
    case "format_time":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "3 - C strftime() like date/time formatter";
        break;
   
    case "format_predicate":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Program format/[1,2]";
        break;
    case "term_attvars":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Find attributed variables in a term";
        break;
    case "term_variables":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Find unbound variables in a term";
        break;
    
    case "text_to_string":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Convert arbitrary text to a string";
        break;
    case "freeze":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Delay execution until the variable is bound";
        break;
    case "frozen":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Query delayed goals on var";
        break;
    case "functor":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "3 - Get name and arity of a term or construct a term";
        break;
   
    case "garbage_collect":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Invoke the garbage collector";
        break;
    case "garbage_collect_atoms":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Invoke the atom garbage collector";
        break;
    case "garbage_collect_clauses":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Invoke the clause garbage collector";
        break;
    case "gen_assoc":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "3 - Enumerate members of the association tree";
        break;
    case "gen_nb_set":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Generate members of the non-backtrackable set";
        break;
    case "gensym":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Generate unique atoms from a base";
        break;
    case "get":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Read the first non-blank character";
        break;
   
    case "get_assoc":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "3 - Fetch key from the association tree";
        break;
   
    case "get_attr":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "3 - Fetch named attribute from a variable";
        break;
    case "get_attrs":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Fetch all attributes of a variable";
        break;
    case "get_byte":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Read the next byte (ISO)";
        break;
    
    case "get_char":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Read the next character as an atom (ISO)";
        break;
   
    case "get_code":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Read the next character (ISO)";
        break;
   
    case "get_dict":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "3 - Get the value associated with a key from a dict";
        break;
   
    case "get_flag":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Get the value of a flag";
        break;
    case "get_single_char":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Read the next character from the terminal";
        break;
    case "get_string_code":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "3 - Get character code at index in string";
        break;
    case "get_time":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Get the current time";
        break;
    case "get0":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Read the next character";
        break;
   
    case "getenv":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Get the shell environment variable";
        break;
    case "goal_expansion":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Hook for macro-expanding goals";
        break;
   
    case "ground":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Verify term holds no unbound variables";
        break;
    case "gdebug":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Debug using graphical tracer";
        break;
    case "gspy":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Spy using graphical tracer";
        break;
    case "gtrace":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Trace using graphical tracer";
        break;
    case "guitracer":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Install hooks for the graphical debugger";
        break;
    case "gxref":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Cross-reference loaded program";
        break;
    case "halt":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Exit from Prolog";
        break;
   
    case "term_hash":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Hash-value of a ground term";
        break;
   
    case "help":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Give help on help";
        break;
    case "help_hook":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - User-hook in the help-system";
        break;

    case "if":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Start conditional compilation (directive)";
        break;

    case "ignore":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Call the argument, but always succeed";
        break;

    case "import":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Import a predicate from a module";
        break;

    case "import_module":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Query import modules";
        break;

    case "in_pce_thread":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Run goal in XPCE thread";
        break;

    case "in_pce_thread_sync":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Run goal in XPCE thread";
        break;

    case "include":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Include a file with declarations";
        break;

    case "initialization":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Initialization directive";
        break;

    case "initialize":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Run program initialization";
        break;

    case "instance":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Fetch clause or record from reference";
        break;

    case "integer":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Type check for integer";
        break;

    case "interactor":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Start new thread with console and top level";
        break;

    case "is":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Evaluate arithmetic expression";
        break;

    case "is_absolute_file_name":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - True if arg defines an absolute path";
        break;

    case "is_assoc":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Verify association list";
        break;

    case "is_async":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - WASM: Test Prolog can call await/2";
        break;

    case "is_dict":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Type check for a dict";
        break;

   

    case "is_engine":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Type check for an engine handle";
        break;

    case "is_list":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Type check for a list";
        break;

    case "is_most_general_term":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Type check for general term";
        break;

    case "is_object":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - WASM: Test JavaScript object";
        break;

   

    case "is_stream":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Type check for a stream handle";
        break;

    case "is_trie":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Type check for a trie handle";
        break;

    case "is_thread":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Type check for a thread handle";
        break;

    case "join_threads":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Join all terminated threads interactively";
        break;

    case "keysort":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Sort, using a key";
        break;

    case "known_licenses":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Print known licenses";
        break;

    case "last":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Last element of a list";
        break;

    case "leash":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Change ports visited by the tracer";
        break;

    case "length":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Length of a list";
        break;

    case "library_directory":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - (hook) Directories holding Prolog libraries";
        break;

    case "license":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Evaluate licenses of loaded modules";
        break;

  

    case "line_count":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Line number on stream";
        break;

    case "line_position":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Character position in line on stream";
        break;

    case "list_debug_topics":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - List registered topics for debugging";
        break;

    case "list_to_assoc":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Create association tree from list";
        break;

    case "list_to_set":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Remove duplicates from a list";
        break;

    case "list_strings":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "0 - Help porting to version 7";
        break;

    case "load_files":
        e.ToolTipTitle = e.HoveredWord;
        e.ToolTipText = "2 - Load source files";
        break;
                           

case "load_foreign_library":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "2 - library(shlib) Load shared library (.so file)";
    break;



case "locale_create":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "3 - Create a new locale object";
    break;

case "locale_destroy":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "2 - Destroy a locale object";
    break;

case "locale_property":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "2 - Query properties of locale objects";
    break;

case "locale_sort":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "2 - Language dependent sort of atoms";
    break;

case "make":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "0 - Reconsult all changed source files";
    break;

case "make_directory":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "2 - Create a folder on the file system";
    break;

case "make_library_index":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "2 - Create autoload file INDEX.pl";
    break;

case "malloc_property":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "2 - Property of the allocator";
    break;


case "map_assoc":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "2 - Map association tree";
    break;



case "dict_create":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "3 - Create a dict from data";
    break;

case "dict_pairs":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "3 - Convert between dict and list of pairs";
    break;

case "max_assoc":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "3 - Highest key in association tree";
    break;

case "memberchk":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "2 - Deterministic member/2";
    break;

case "message_hook":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "3 - Intercept print_message/2";
    break;

case "message_line_element":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "(hook) Intercept print_message_lines/3";
    break;

case "message_property":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "(hook) Define display of a message";
    break;

case "message_queue_create":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "2 - Create queue for thread communication";
    break;



case "message_queue_destroy":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "2 - Destroy queue for thread communication";
    break;

case "message_queue_property":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "2 - Query message queue properties";
    break;

case "message_queue_set":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "2 - Set a message queue property";
    break;

case "message_to_string":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "2 - Translate message-term to string";
    break;

case "meta_predicate":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "2 - Declare access to other predicates";
    break;

case "min_assoc":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "3 - Lowest key in association tree";
    break;

case "module":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "2 - Query/set current type-in module";
    break;



case "module_property":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "2 - Find properties of a module";
    break;

case "module_transparent":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "2 - Indicate module based meta-predicate";
    break;

case "msort":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "2 - Sort, do not remove duplicates";
    break;

case "multifile":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "2 - Indicate distributed definition of predicate";
    break;

case "mutex_create":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "2 - Create a thread-synchronization device";
    break;


case "mutex_destroy":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "2 - Destroy a mutex";
    break;

case "mutex_lock":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "2 - Become owner of a mutex";
    break;

case "mutex_property":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "2 - Query mutex properties";
    break;

case "mutex_statistics":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "0 - Print statistics on mutex usage";
    break;

case "mutex_trylock":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "2 - Become owner of a mutex (non-blocking)";
    break;

case "mutex_unlock":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "2 - Release ownership of mutex";
    break;

case "mutex_unlock_all":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "0 - Release ownership of all mutexes";
    break;

case "name":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "2 - Convert between atom and list of character codes";
    break;

case "nb_current":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "2 - Enumerate non-backtrackable global variables";
    break;

case "nb_delete":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "2 - Delete a non-backtrackable global variable";
    break;

case "nb_getval":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "2 - Fetch non-backtrackable global variable";
    break;

case "nb_link_dict":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "3 - Non-backtrackable assignment to dict";
    break;

case "nb_linkarg":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "3 - Non-backtrackable assignment to term";
    break;

case "nb_linkval":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "2 - Assign non-backtrackable global variable";
    break;

case "nb_set_to_list":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "2 - Convert non-backtrackable set to list";
    break;

case "nb_set_dict":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "3 - Non-backtrackable assignment to dict";
    break;

case "nb_setarg":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "3 - Non-backtrackable assignment to term";
    break;

case "nb_setval":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "2 - Assign non-backtrackable global variable";
    break;

case "nl":
    e.ToolTipTitle = e.HoveredWord;
    e.ToolTipText = "0 - Generate a newline";
    break;


case "nodebug":
    e.ToolTipTitle = "nodebug";
    e.ToolTipText = "Disable debugging";
    break;



case "noguitracer":
    e.ToolTipTitle = "noguitracer";
    e.ToolTipText = "Disable the graphical debugger";
    break;

case "nonground":
    e.ToolTipTitle = "nonground";
    e.ToolTipText = "Term is not ground due to witness";
    break;

case "nonvar":
    e.ToolTipTitle = "nonvar";
    e.ToolTipText = "Type check for bound term";
    break;

case "nonterminal":
    e.ToolTipTitle = "nonterminal";
    e.ToolTipText = "Set predicate property";
    break;

case "noprofile":
    e.ToolTipTitle = "noprofile";
    e.ToolTipText = "Hide (meta-) predicate for the profiler";
    break;

case "noprotocol":
    e.ToolTipTitle = "noprotocol";
    e.ToolTipText = "Disable logging of user interaction";
    break;

case "normalize_space":
    e.ToolTipTitle = "normalize_space";
    e.ToolTipText = "Normalize white space";
    break;

case "nospy":
    e.ToolTipTitle = "nospy";
    e.ToolTipText = "Remove spy point";
    break;

case "nospyall":
    e.ToolTipTitle = "nospyall";
    e.ToolTipText = "Remove all spy points";
    break;

case "not":
    e.ToolTipTitle = "not";
    e.ToolTipText = "Negation by failure (argument not provable). Same as \\+/1";
    break;

case "not_exists":
    e.ToolTipTitle = "not_exists";
    e.ToolTipText = "Tabled negation for non-ground or non-tabled goals";
    break;

case "notrace":
    e.ToolTipTitle = "notrace";
    e.ToolTipText = "Stop tracing";
    break;



case "nth_clause":
    e.ToolTipTitle = "nth_clause";
    e.ToolTipText = "N-th clause of a predicate";
    break;

case "nth_integer_root_and_remainder":
    e.ToolTipTitle = "nth_integer_root_and_remainder";
    e.ToolTipText = "Integer root and remainder";
    break;

case "number":
    e.ToolTipTitle = "number";
    e.ToolTipText = "Type check for integer or float";
    break;

case "number_chars":
    e.ToolTipTitle = "number_chars";
    e.ToolTipText = "Convert between number and one-char atoms";
    break;

case "number_codes":
    e.ToolTipTitle = "number_codes";
    e.ToolTipText = "Convert between number and character codes";
    break;

case "number_string":
    e.ToolTipTitle = "number_string";
    e.ToolTipText = "Convert between number and string";
    break;

case "numbervars":
    e.ToolTipTitle = "numbervars";
    e.ToolTipText = "Number unbound variables of a term";
    break;



case "on_signal":
    e.ToolTipTitle = "on_signal";
    e.ToolTipText = "Handle a software signal";
    break;

case "once":
    e.ToolTipTitle = "once";
    e.ToolTipText = "Call a goal deterministically";
    break;

case "op":
    e.ToolTipTitle = "op";
    e.ToolTipText = "Declare an operator";
    break;

case "open":
    e.ToolTipTitle = "open";
    e.ToolTipText = "Open a file (creating a stream)";
    break;



case "open_dde_conversation":
    e.ToolTipTitle = "open_dde_conversation";
    e.ToolTipText = "Win32: Open DDE channel";
    break;

case "open_null_stream":
    e.ToolTipTitle = "open_null_stream";
    e.ToolTipText = "Open a stream to discard output";
    break;

case "open_resource":
    e.ToolTipTitle = "open_resource";
    e.ToolTipText = "Open a program resource as a stream";
    break;

case "open_shared_object":
    e.ToolTipTitle = "open_shared_object";
    e.ToolTipText = "UNIX: Open shared library (.so file)";
    break;



case "open_source_hook":
    e.ToolTipTitle = "open_source_hook";
    e.ToolTipText = "(hook) Open a source file";
    break;

case "open_string":
    e.ToolTipTitle = "open_string";
    e.ToolTipText = "Open a string as a stream";
    break;
                            case "ord_list_to_assoc":
    e.ToolTipTitle = "ord_list_to_assoc";
    e.ToolTipText = "Convert ordered list to assoc";
    break;

case "parse_time":
    e.ToolTipTitle = "parse_time";
    e.ToolTipText = "Parse text to a time-stamp";
    break;



case "pce_dispatch":
    e.ToolTipTitle = "pce_dispatch";
    e.ToolTipText = "Run XPCE GUI in separate thread";
    break;

case "pce_call":
    e.ToolTipTitle = "pce_call";
    e.ToolTipText = "Run goal in XPCE GUI thread";
    break;

case "peek_byte":
    e.ToolTipTitle = "peek_byte";
    e.ToolTipText = "Read byte without removing";
    break;



case "peek_char":
    e.ToolTipTitle = "peek_char";
    e.ToolTipText = "Read character without removing";
    break;



case "peek_code":
    e.ToolTipTitle = "peek_code";
    e.ToolTipText = "Read character-code without removing";
    break;



case "peek_string":
    e.ToolTipTitle = "peek_string";
    e.ToolTipText = "Read a string without removing";
    break;

case "phrase":
    e.ToolTipTitle = "phrase";
    e.ToolTipText = "Activate grammar-rule set";
    break;



case "phrase_from_quasi_quotation":
    e.ToolTipTitle = "phrase_from_quasi_quotation";
    e.ToolTipText = "Parse quasi quotation with DCG";
    break;

case "please":
    e.ToolTipTitle = "please";
    e.ToolTipText = "Query/change environment parameters";
    break;

case "plus":
    e.ToolTipTitle = "plus";
    e.ToolTipText = "Logical integer addition";
    break;

case "portray":
    e.ToolTipTitle = "portray";
    e.ToolTipText = "(hook) Modify behavior of print/1";
    break;

case "predicate_property":
    e.ToolTipTitle = "predicate_property";
    e.ToolTipText = "Query predicate attributes";
    break;

case "predsort":
    e.ToolTipTitle = "predsort";
    e.ToolTipText = "Sort, using a predicate to determine the order";
    break;

case "print":
    e.ToolTipTitle = "print";
    e.ToolTipText = "Print a term";
    break;



case "print_message":
    e.ToolTipTitle = "print_message";
    e.ToolTipText = "Print message from (exception) term";
    break;

case "print_message_lines":
    e.ToolTipTitle = "print_message_lines";
    e.ToolTipText = "Print message to stream";
    break;

case "profile":
    e.ToolTipTitle = "profile";
    e.ToolTipText = "Obtain execution statistics";
    break;



case "profile_count":
    e.ToolTipTitle = "profile_count";
    e.ToolTipText = "Obtain profile results on a predicate";
    break;

case "profiler":
    e.ToolTipTitle = "profiler";
    e.ToolTipText = "Obtain/change status of the profiler";
    break;

case "prolog":
    e.ToolTipTitle = "prolog";
    e.ToolTipText = "Run interactive top level";
    break;

case "prolog_alert_signal":
    e.ToolTipTitle = "prolog_alert_signal";
    e.ToolTipText = "Query/set unblock signal";
    break;

case "prolog_choice_attribute":
    e.ToolTipTitle = "prolog_choice_attribute";
    e.ToolTipText = "Examine the choice point stack";
    break;

case "prolog_current_choice":
    e.ToolTipTitle = "prolog_current_choice";
    e.ToolTipText = "Reference to most recent choice point";
    break;

case "prolog_current_frame":
    e.ToolTipTitle = "prolog_current_frame";
    e.ToolTipText = "Reference to goal's environment stack";
    break;

case "prolog_cut_to":
    e.ToolTipTitle = "prolog_cut_to";
    e.ToolTipText = "Realize global cuts";
    break;

case "prolog_edit:locate":
    e.ToolTipTitle = "prolog_edit:locate";
    e.ToolTipText = "Locate targets for edit/1";
    break;



case "prolog_edit:edit_source":
    e.ToolTipTitle = "prolog_edit:edit_source";
    e.ToolTipText = "Call editor for edit/1";
    break;

case "prolog_edit:edit_command":
    e.ToolTipTitle = "prolog_edit:edit_command";
    e.ToolTipText = "Specify editor activation";
    break;

case "prolog_edit:load":
    e.ToolTipTitle = "prolog_edit:load";
    e.ToolTipText = "Load edit/1 extensions";
    break;
case "prolog_exception_hook":
    e.ToolTipTitle = "prolog_exception_hook";
    e.ToolTipText = "Rewrite exceptions";
    break;

case "prolog_file_type":
    e.ToolTipTitle = "prolog_file_type";
    e.ToolTipText = "Define meaning of file extension";
    break;

case "prolog_frame_attribute":
    e.ToolTipTitle = "prolog_frame_attribute";
    e.ToolTipText = "Obtain information on a goal environment";
    break;

case "prolog_ide":
    e.ToolTipTitle = "prolog_ide";
    e.ToolTipText = "Program access to the development environment";
    break;

case "prolog_interrupt":
    e.ToolTipTitle = "prolog_interrupt";
    e.ToolTipText = "Allow debugging a thread";
    break;

case "prolog_list_goal":
    e.ToolTipTitle = "prolog_list_goal";
    e.ToolTipText = "(hook) Intercept tracer’L' command";
    break;

case "prolog_listen":
    e.ToolTipTitle = "prolog_listen";
    e.ToolTipText = "Listen to Prolog events";
    break;



case "prolog_load_context":
    e.ToolTipTitle = "prolog_load_context";
    e.ToolTipText = "Context information for directives";
    break;

case "prolog_load_file":
    e.ToolTipTitle = "prolog_load_file";
    e.ToolTipText = "(hook) Program load_files/2";
    break;

case "prolog_skip_level":
    e.ToolTipTitle = "prolog_skip_level";
    e.ToolTipText = "Indicate deepest recursion to trace";
    break;

case "prolog_skip_frame":
    e.ToolTipTitle = "prolog_skip_frame";
    e.ToolTipText = "Perform ‘skip' on a frame";
    break;

case "prolog_stack_property":
    e.ToolTipTitle = "prolog_stack_property";
    e.ToolTipText = "Query properties of the stacks";
    break;

case "prolog_to_os_filename":
    e.ToolTipTitle = "prolog_to_os_filename";
    e.ToolTipText = "Convert between Prolog and OS filenames";
    break;

case "prolog_trace_interception":
    e.ToolTipTitle = "prolog_trace_interception";
    e.ToolTipText = "library(user) Intercept the Prolog tracer";
    break;

case "prolog_unlisten":
    e.ToolTipTitle = "prolog_unlisten";
    e.ToolTipText = "Stop listening to Prolog events";
    break;

case "project_attributes":
    e.ToolTipTitle = "project_attributes";
    e.ToolTipText = "Project constraints to query variables";
    break;

case "prompt1":
    e.ToolTipTitle = "prompt1";
    e.ToolTipText = "Change prompt for 1 line";
    break;

case "prompt":
    e.ToolTipTitle = "prompt";
    e.ToolTipText = "Change the prompt used by read/1";
    break;

case "protocol":
    e.ToolTipTitle = "protocol";
    e.ToolTipText = "Make a log of the user interaction";
    break;

case "protocola":
    e.ToolTipTitle = "protocola";
    e.ToolTipText = "Append log of the user interaction to file";
    break;

case "protocolling":
    e.ToolTipTitle = "protocolling";
    e.ToolTipText = "On what file is user interaction logged";
    break;

case "public":
    e.ToolTipTitle = "public";
    e.ToolTipText = "Declaration that a predicate may be called";
    break;

case "put":
    e.ToolTipTitle = "put";
    e.ToolTipText = "Write a character";
    break;



case "put_assoc":
    e.ToolTipTitle = "put_assoc";
    e.ToolTipText = "Add Key-Value to association tree";
    break;

case "put_attr":
    e.ToolTipTitle = "put_attr";
    e.ToolTipText = "Put attribute on a variable";
    break;
                            case "put_attrs":
    e.ToolTipTitle = "put_attrs";
    e.ToolTipText = "Set/replace all attributes on a variable";
    break;

case "put_byte":
    e.ToolTipTitle = "put_byte";
    e.ToolTipText = "Write a byte";
    break;



case "put_char":
    e.ToolTipTitle = "put_char";
    e.ToolTipText = "Write a character";
    break;



case "put_code":
    e.ToolTipTitle = "put_code";
    e.ToolTipText = "Write a character-code";
    break;



case "put_dict":
    e.ToolTipTitle = "put_dict";
    e.ToolTipText = "Add/replace multiple keys in a dict";
    break;



case "qcompile":
    e.ToolTipTitle = "qcompile";
    e.ToolTipText = "Compile source to Quick Load File";
    break;



case "qsave_program":
    e.ToolTipTitle = "qsave_program";
    e.ToolTipText = "Create runtime application";
    break;



case "quasi_quotation_syntax":
    e.ToolTipTitle = "quasi_quotation_syntax";
    e.ToolTipText = "Declare quasi quotation syntax";
    break;

case "quasi_quotation_syntax_error":
    e.ToolTipTitle = "quasi_quotation_syntax_error";
    e.ToolTipText = "Raise syntax error";
    break;

case "radial_restraint":
    e.ToolTipTitle = "radial_restraint";
    e.ToolTipText = "Tabbling radial restraint was violated";
    break;

case "random_property":
    e.ToolTipTitle = "random_property";
    e.ToolTipText = "Query properties of random generation";
    break;

case "rational":
    e.ToolTipTitle = "rational";
    e.ToolTipText = "Type check for a rational number";
    break;



case "read":
    e.ToolTipTitle = "read";
    e.ToolTipText = "Read Prolog term";
    break;



case "read_clause":
    e.ToolTipTitle = "read_clause";
    e.ToolTipText = "Read clause from stream";
    break;

case "read_link":
    e.ToolTipTitle = "read_link";
    e.ToolTipText = "Read a symbolic link";
    break;
                            case "read_pending_codes":
    e.ToolTipTitle = "read_pending_codes";
    e.ToolTipText = "Fetch buffered input from a stream";
    break;

case "read_pending_chars":
    e.ToolTipTitle = "read_pending_chars";
    e.ToolTipText = "Fetch buffered input from a stream";
    break;

case "read_string":
    e.ToolTipTitle = "read_string";
    e.ToolTipText = "Read a number of characters into a string";
    break;

case "read_string(  5":
    e.ToolTipTitle = "read_string(  5";
    e.ToolTipText = "Read string upto a delimiter";
    break;

case "read_term":
    e.ToolTipTitle = "read_term";
    e.ToolTipText = "Read term with options";
    break;



case "read_term_from_atom":
    e.ToolTipTitle = "read_term_from_atom";
    e.ToolTipText = "Read term with options from atom";
    break;

case "read_term_with_history":
    e.ToolTipTitle = "read_term_with_history";
    e.ToolTipText = "Read term with command line history";
    break;

case "recorda":
    e.ToolTipTitle = "recorda";
    e.ToolTipText = "Record term in the database (first)";
    break;



case "recorded":
    e.ToolTipTitle = "recorded";
    e.ToolTipText = "Obtain term from the database";
    break;



case "recordz":
    e.ToolTipTitle = "recordz";
    e.ToolTipText = "Record term in the database (last)";
    break;



case "redefine_system_predicate":
    e.ToolTipTitle = "redefine_system_predicate";
    e.ToolTipText = "Abolish system definition";
    break;

case "reexport":
    e.ToolTipTitle = "reexport";
    e.ToolTipText = "Load files and re-export the imported predicates";
    break;



case "reload_foreign_libraries":
    e.ToolTipTitle = "reload_foreign_libraries";
    e.ToolTipText = "Reload DLLs/shared objects";
    break;

case "reload_library_index":
    e.ToolTipTitle = "reload_library_index";
    e.ToolTipText = "Force reloading the autoload index";
    break;

case "rename_file":
    e.ToolTipTitle = "rename_file";
    e.ToolTipText = "Change name of file";
    break;

case "repeat":
    e.ToolTipTitle = "repeat";
    e.ToolTipText = "Succeed, leaving infinite backtrack points";
    break;

case "require":
    e.ToolTipTitle = "require";
    e.ToolTipText = "This file requires these predicates";
    break;

case "reset":
    e.ToolTipTitle = "reset";
    e.ToolTipText = "Wrapper for delimited continuations";
    break;

case "reset_gensym":
    e.ToolTipTitle = "reset_gensym";
    e.ToolTipText = "Reset a gensym key";
    break;



case "reset_profiler":
    e.ToolTipTitle = "reset_profiler";
    e.ToolTipText = "Clear statistics obtained by the profiler";
    break;

case "resource":
    e.ToolTipTitle = "resource";
    e.ToolTipText = "Declare a program resource";
    break;



case "retract":
    e.ToolTipTitle = "retract";
    e.ToolTipText = "Remove clause from the database";
    break;

case "retractall":
    e.ToolTipTitle = "retractall";
    e.ToolTipText = "Remove unifying clauses from the database";
    break;

                            case "same_file":
    e.ToolTipTitle = "same_file";
    e.ToolTipText = "Succeeds if arguments refer to the same file";
    break;

case "same_term":
    e.ToolTipTitle = "same_term";
    e.ToolTipText = "Test terms to be at the same address";
    break;

case "see":
    e.ToolTipTitle = "see";
    e.ToolTipText = "Change the current input stream";
    break;

case "seeing":
    e.ToolTipTitle = "seeing";
    e.ToolTipText = "Query the current input stream";
    break;

case "seek":
    e.ToolTipTitle = "seek";
    e.ToolTipText = "Modify the current position in a stream";
    break;

case "seen":
    e.ToolTipTitle = "seen";
    e.ToolTipText = "Close the current input stream";
    break;

case "select_dict":
    e.ToolTipTitle = "select_dict";
    e.ToolTipText = "Select matching attributes from a dict";
    break;



case "set_end_of_stream":
    e.ToolTipTitle = "set_end_of_stream";
    e.ToolTipText = "Set physical end of an open file";
    break;

case "set_flag":
    e.ToolTipTitle = "set_flag";
    e.ToolTipText = "Set value of a flag";
    break;

case "set_input":
    e.ToolTipTitle = "set_input";
    e.ToolTipText = "Set current input stream from a stream";
    break;

case "set_locale":
    e.ToolTipTitle = "set_locale";
    e.ToolTipText = "Set the default local";
    break;

case "set_malloc":
    e.ToolTipTitle = "set_malloc";
    e.ToolTipText = "Set memory allocator property";
    break;

case "set_module":
    e.ToolTipTitle = "set_module";
    e.ToolTipText = "Set properties of a module";
    break;

case "set_output":
    e.ToolTipTitle = "set_output";
    e.ToolTipText = "Set current output stream from a stream";
    break;

case "set_prolog_IO":
    e.ToolTipTitle = "set_prolog_IO";
    e.ToolTipText = "Prepare streams for an interactive session";
    break;

case "set_prolog_flag":
    e.ToolTipTitle = "set_prolog_flag";
    e.ToolTipText = "Define a system feature";
    break;

case "set_prolog_gc_thread":
    e.ToolTipTitle = "set_prolog_gc_thread";
    e.ToolTipText = "Control the gc thread";
    break;

case "set_prolog_stack":
    e.ToolTipTitle = "set_prolog_stack";
    e.ToolTipText = "Modify stack characteristics";
    break;

case "set_random":
    e.ToolTipTitle = "set_random";
    e.ToolTipText = "Control random number generation";
    break;

case "set_stream":
    e.ToolTipTitle = "set_stream";
    e.ToolTipText = "Set stream attribute";
    break;

case "set_stream_position":
    e.ToolTipTitle = "set_stream_position";
    e.ToolTipText = "Seek stream to position";
    break;

case "set_system_IO":
    e.ToolTipTitle = "set_system_IO";
    e.ToolTipText = "Rebind stdin/stderr/stdout";
    break;

case "setup_call_cleanup":
    e.ToolTipTitle = "setup_call_cleanup";
    e.ToolTipText = "Undo side-effects safely";
    break;

case "setup_call_catcher_cleanup":
    e.ToolTipTitle = "setup_call_catcher_cleanup";
    e.ToolTipText = "Undo side-effects safely";
    break;

case "setarg":
    e.ToolTipTitle = "setarg";
    e.ToolTipText = "Destructive assignment on term";
    break;

case "setenv":
    e.ToolTipTitle = "setenv";
    e.ToolTipText = "Set shell environment variable";
    break;

case "setlocale":
    e.ToolTipTitle = "setlocale";
    e.ToolTipText = "Set/query C-library regional information";
    break;

case "setof":
    e.ToolTipTitle = "setof";
    e.ToolTipText = "Find all unique solutions to a goal";
    break;
                            case "shell":
    e.ToolTipTitle = "shell";
    e.ToolTipText = "Execute OS command";
    break;

case "shift":
    e.ToolTipTitle = "shift";
    e.ToolTipText = "Shift control to the closest reset/3";
    break;

case "shift_for_copy":
    e.ToolTipTitle = "shift_for_copy";
    e.ToolTipText = "Shift control to the closest reset/3";
    break;

case "show_profile":
    e.ToolTipTitle = "show_profile";
    e.ToolTipText = "Show results of the profiler";
    break;

case "sig_atomic":
    e.ToolTipTitle = "sig_atomic";
    e.ToolTipText = "Run goal without handling signals";
    break;

case "sig_block":
    e.ToolTipTitle = "sig_block";
    e.ToolTipText = "Block matching thread signals";
    break;

case "sig_pending":
    e.ToolTipTitle = "sig_pending";
    e.ToolTipText = "Query pending signals";
    break;

case "sig_remove":
    e.ToolTipTitle = "sig_remove";
    e.ToolTipText = "Remove pending signals";
    break;

case "sig_unblock":
    e.ToolTipTitle = "sig_unblock";
    e.ToolTipText = "Unblock matching thread signals";
    break;

case "size_abstract_term":
    e.ToolTipTitle = "size_abstract_term";
    e.ToolTipText = "Abstract a term (tabling support)";
    break;

case "size_file":
    e.ToolTipTitle = "size_file";
    e.ToolTipText = "Get size of a file in characters";
    break;

case "size_nb_set":
    e.ToolTipTitle = "size_nb_set";
    e.ToolTipText = "Determine size of non-backtrackable set";
    break;

case "skip":
    e.ToolTipTitle = "skip";
    e.ToolTipText = "Skip to character in current input";
    break;



case "sleep":
    e.ToolTipTitle = "sleep";
    e.ToolTipText = "Suspend execution for specified time";
    break;

case "snapshot":
    e.ToolTipTitle = "snapshot";
    e.ToolTipText = "Run goal in isolation";
    break;

case "sort":
    e.ToolTipTitle = "sort";
    e.ToolTipText = "Sort elements in a list";
    break;



case "source_exports":
    e.ToolTipTitle = "source_exports";
    e.ToolTipText = "Check whether source exports a predicate";
    break;

case "source_file":
    e.ToolTipTitle = "source_file";
    e.ToolTipText = "Examine currently loaded source files";
    break;



case "source_file_property":
    e.ToolTipTitle = "source_file_property";
    e.ToolTipText = "Information about loaded files";
    break;

case "source_location":
    e.ToolTipTitle = "source_location";
    e.ToolTipText = "Location of last read term";
    break;

case "split_string":
    e.ToolTipTitle = "split_string";
    e.ToolTipText = "Break a string into substrings";
    break;

case "spy":
    e.ToolTipTitle = "spy";
    e.ToolTipText = "Force tracer on specified predicate";
    break;

case "stamp_date_time":
    e.ToolTipTitle = "stamp_date_time";
    e.ToolTipText = "Convert time-stamp to date structure";
    break;

case "statistics":
    e.ToolTipTitle = "statistics";
    e.ToolTipText = "Obtain collected statistics";
    break;

case "stream_pair":
    e.ToolTipTitle = "stream_pair";
    e.ToolTipText = "Create/examine a bi-directional stream";
    break;

case "stream_position_data":
    e.ToolTipTitle = "stream_position_data";
    e.ToolTipText = "Access fields from stream position";
    break;

case "stream_property":
    e.ToolTipTitle = "stream_property";
    e.ToolTipText = "Get stream properties";
    break;

case "string":
    e.ToolTipTitle = "string";
    e.ToolTipText = "Type check for string";
    break;

case "string_bytes":
    e.ToolTipTitle = "string_bytes";
    e.ToolTipText = "Translates between text and bytes in encoding";
    break;

case "string_concat":
    e.ToolTipTitle = "string_concat";
    e.ToolTipText = "atom_concat/3 for strings";
    break;

case "string_length":
    e.ToolTipTitle = "string_length";
    e.ToolTipText = "Determine length of a string";
    break;

case "string_chars":
    e.ToolTipTitle = "string_chars";
    e.ToolTipText = "Conversion between string and list of characters";
    break;

case "string_codes":
    e.ToolTipTitle = "string_codes";
    e.ToolTipText = "Conversion between string and list of character codes";
    break;

case "string_code":
    e.ToolTipTitle = "string_code";
    e.ToolTipText = "Get or find a character code in a string";
    break;

case "string_lower":
    e.ToolTipTitle = "string_lower";
    e.ToolTipText = "Case conversion to lower case";
    break;

case "string_upper":
    e.ToolTipTitle = "string_upper";
    e.ToolTipText = "Case conversion to upper case";
    break;

case "string_predicate":
    e.ToolTipTitle = "string_predicate";
    e.ToolTipText = "(hook) Predicate contains strings";
    break;
                            case "strip_module":
    e.ToolTipTitle = "strip_module";
    e.ToolTipText = "Extract context module and term";
    break;

case "style_check":
    e.ToolTipTitle = "style_check";
    e.ToolTipText = "Change level of warnings";
    break;

case "sub_atom(  5":
    e.ToolTipTitle = "sub_atom(  5";
    e.ToolTipText = "Take a substring from an atom";
    break;

case "sub_atom_icasechk":
    e.ToolTipTitle = "sub_atom_icasechk";
    e.ToolTipText = "Case insensitive substring match";
    break;

case "sub_string(  5":
    e.ToolTipTitle = "sub_string(  5";
    e.ToolTipText = "Take a substring from a string";
    break;

case "subsumes_term":
    e.ToolTipTitle = "subsumes_term";
    e.ToolTipText = "One-sided unification test";
    break;

case "succ":
    e.ToolTipTitle = "succ";
    e.ToolTipText = "Logical integer successor relation";
    break;

case "swritef":
    e.ToolTipTitle = "swritef";
    e.ToolTipText = "Formatted write on a string";
    break;



case "tab":
    e.ToolTipTitle = "tab";
    e.ToolTipText = "Output number of spaces";
    break;



case "table":
    e.ToolTipTitle = "table";
    e.ToolTipText = "Declare predicate to be tabled";
    break;

case "tabled_call":
    e.ToolTipTitle = "tabled_call";
    e.ToolTipText = "Helper for not_exists/1";
    break;

case "tdebug":
    e.ToolTipTitle = "tdebug";
    e.ToolTipText = "Switch all threads into debug mode";
    break;



case "tell":
    e.ToolTipTitle = "tell";
    e.ToolTipText = "Change current output stream";
    break;

case "telling":
    e.ToolTipTitle = "telling";
    e.ToolTipText = "Query current output stream";
    break;

case "term_expansion":
    e.ToolTipTitle = "term_expansion";
    e.ToolTipText = "(hook) Convert term before compilation";
    break;



case "term_singletons":
    e.ToolTipTitle = "term_singletons";
    e.ToolTipText = "Find singleton variables in a term";
    break;

case "term_string":
    e.ToolTipTitle = "term_string";
    e.ToolTipText = "Read/write a term from/to a string";
    break;


case "term_subsumer":
    e.ToolTipTitle = "term_subsumer";
    e.ToolTipText = "Most specific generalization of two terms";
    break;

case "term_to_atom":
    e.ToolTipTitle = "term_to_atom";
    e.ToolTipText = "Convert between term and atom";
    break;

case "thread_affinity":
    e.ToolTipTitle = "thread_affinity";
    e.ToolTipText = "Query and control the affinity mask";
    break;

case "thread_alias":
    e.ToolTipTitle = "thread_alias";
    e.ToolTipText = "Set the alias name of a thread";
    break;

case "thread_at_exit":
    e.ToolTipTitle = "thread_at_exit";
    e.ToolTipText = "Register goal to be called at exit";
    break;

case "thread_create":
    e.ToolTipTitle = "thread_create";
    e.ToolTipText = "Create a new Prolog task";
    break;



case "thread_detach":
    e.ToolTipTitle = "thread_detach";
    e.ToolTipText = "Make thread cleanup after completion";
    break;

case "thread_exit":
    e.ToolTipTitle = "thread_exit";
    e.ToolTipText = "Terminate Prolog task with value";
    break;

case "thread_get_message":
    e.ToolTipTitle = "thread_get_message";
    e.ToolTipText = "Wait for message";
    break;



case "thread_idle":
    e.ToolTipTitle = "thread_idle";
    e.ToolTipText = "Reduce footprint while waiting";
    break;

case "thread_initialization":
    e.ToolTipTitle = "thread_initialization";
    e.ToolTipText = "Run action at start of thread";
    break;

case "thread_join":
    e.ToolTipTitle = "thread_join";
    e.ToolTipText = "Wait for Prolog task-completion";
    break;


case "thread_local":
    e.ToolTipTitle = "thread_local";
    e.ToolTipText = "Declare thread-specific clauses for a predicate";
    break;

case "thread_message_hook":
    e.ToolTipTitle = "thread_message_hook";
    e.ToolTipText = "Thread local message_hook/3";
    break;

case "thread_peek_message":
    e.ToolTipTitle = "thread_peek_message";
    e.ToolTipText = "Test for message";
    break;



case "thread_property":
    e.ToolTipTitle = "thread_property";
    e.ToolTipText = "Examine Prolog threads";
    break;

case "thread_self":
    e.ToolTipTitle = "thread_self";
    e.ToolTipText = "Get identifier of current thread";
    break;

case "thread_send_message":
    e.ToolTipTitle = "thread_send_message";
    e.ToolTipText = "Send message to another thread";
    break;



case "thread_setconcurrency":
    e.ToolTipTitle = "thread_setconcurrency";
    e.ToolTipText = "Number of active threads";
    break;

case "thread_signal":
    e.ToolTipTitle = "thread_signal";
    e.ToolTipText = "Execute goal in another thread";
    break;

case "thread_statistics":
    e.ToolTipTitle = "thread_statistics";
    e.ToolTipText = "Get statistics of another thread";
    break;

case "thread_update":
    e.ToolTipTitle = "thread_update";
    e.ToolTipText = "Update a module and signal waiters";
    break;

case "thread_wait":
    e.ToolTipTitle = "thread_wait";
    e.ToolTipText = "Wait for a goal to become true";
    break;

case "threads":
    e.ToolTipTitle = "threads";
    e.ToolTipText = "List running threads";
    break;

case "throw":
    e.ToolTipTitle = "throw";
    e.ToolTipText = "Raise an exception (see catch/3)";
    break;

case "time":
    e.ToolTipTitle = "time";
    e.ToolTipText = "Determine time needed to execute goal";
    break;

case "time_file":
    e.ToolTipTitle = "time_file";
    e.ToolTipText = "Get last modification time of file";
    break;

case "tmp_file":
    e.ToolTipTitle = "tmp_file";
    e.ToolTipText = "Create a temporary filename";
    break;

case "tmp_file_stream":
    e.ToolTipTitle = "tmp_file_stream";
    e.ToolTipText = "Create a temporary file and open it";
    break;

case "tnodebug":
    e.ToolTipTitle = "tnodebug";
    e.ToolTipText = "Switch off debug mode in all threads";
    break;



case "tnot":
    e.ToolTipTitle = "tnot";
    e.ToolTipText = "Tabled negation";
    break;

case "told":
    e.ToolTipTitle = "told";
    e.ToolTipText = "Close current output";
    break;

case "tprofile":
    e.ToolTipTitle = "tprofile";
    e.ToolTipText = "Profile a thread for some period";
    break;

case "trace":
    e.ToolTipTitle = "trace";
    e.ToolTipText = "Start the tracer";
    break;

case "tracing":
    e.ToolTipTitle = "tracing";
    e.ToolTipText = "Query status of the tracer";
    break;

case "transaction":
    e.ToolTipTitle = "transaction";
    e.ToolTipText = "Run goal in a transaction";
    break;
                     



case "transaction_updates":
    e.ToolTipTitle = "transaction_updates";
    e.ToolTipText = "Updates to be committed in a transaction";
    break;

case "trie_delete":
    e.ToolTipTitle = "trie_delete";
    e.ToolTipText = "Remove term from trie";
    break;

case "trie_destroy":
    e.ToolTipTitle = "trie_destroy";
    e.ToolTipText = "Destroy a trie";
    break;

case "trie_gen":
    e.ToolTipTitle = "trie_gen";
    e.ToolTipText = "Get all terms from a trie";
    break;

case "trie_gen_compiled":
    e.ToolTipTitle = "trie_gen_compiled";
    e.ToolTipText = "Get all terms from a trie";
    break;



case "trie_insert":
    e.ToolTipTitle = "trie_insert";
    e.ToolTipText = "Insert term into a trie";
    break;




case "trie_lookup":
    e.ToolTipTitle = "trie_lookup";
    e.ToolTipText = "Lookup a term in a trie";
    break;

case "trie_new":
    e.ToolTipTitle = "trie_new";
    e.ToolTipText = "Create a trie";
    break;

case "trie_property":
    e.ToolTipTitle = "trie_property";
    e.ToolTipText = "Examine a trie's properties";
    break;

case "trie_update":
    e.ToolTipTitle = "trie_update";
    e.ToolTipText = "Update associated value in trie";
    break;

case "trie_term":
    e.ToolTipTitle = "trie_term";
    e.ToolTipText = "Get term from a trie by handle";
    break;

case "trim_heap":
    e.ToolTipTitle = "trim_heap";
    e.ToolTipText = "Release unused malloc() resources";
    break;

case "trim_stacks":
    e.ToolTipTitle = "trim_stacks";
    e.ToolTipText = "Release unused stack resources";
    break;

case "tripwire":
    e.ToolTipTitle = "tripwire";
    e.ToolTipText = "(hook) Handle a tabling tripwire event";
    break;

case "true":
    e.ToolTipTitle = "true";
    e.ToolTipText = "Succeed";
    break;

case "tspy":
    e.ToolTipTitle = "tspy";
    e.ToolTipText = "Set spy point and enable debugging in all threads";
    break;



case "tty_get_capability":
    e.ToolTipTitle = "tty_get_capability";
    e.ToolTipText = "Get terminal parameter";
    break;

case "tty_goto":
    e.ToolTipTitle = "tty_goto";
    e.ToolTipText = "Goto position on screen";
    break;

case "tty_put":
    e.ToolTipTitle = "tty_put";
    e.ToolTipText = "Write control string to terminal";
    break;

case "tty_size":
    e.ToolTipTitle = "tty_size";
    e.ToolTipText = "Get row/column size of the terminal";
    break;

case "ttyflush":
    e.ToolTipTitle = "ttyflush";
    e.ToolTipText = "Flush output on terminal";
    break;

case "undefined":
    e.ToolTipTitle = "undefined";
    e.ToolTipText = "Well Founded Semantics: true nor false";
    break;

case "undo":
    e.ToolTipTitle = "undo";
    e.ToolTipText = "Schedule goal for backtracking";
    break;

case "unify_with_occurs_check":
    e.ToolTipTitle = "unify_with_occurs_check";
    e.ToolTipText = "Logically sound unification";
    break;

case "unifiable":
    e.ToolTipTitle = "unifiable";
    e.ToolTipText = "Determining binding required for unification";
    break;

case "unknown":
    e.ToolTipTitle = "unknown";
    e.ToolTipText = "Trap undefined predicates";
    break;

case "unload_file":
    e.ToolTipTitle = "unload_file";
    e.ToolTipText = "Unload a source file";
    break;

case "unload_foreign_library":
    e.ToolTipTitle = "unload_foreign_library";
    e.ToolTipText = "library(shlib) Detach shared library (.so file)";
    break;



case "unsetenv":
    e.ToolTipTitle = "unsetenv";
    e.ToolTipText = "Delete shell environment variable";
    break;

case "untable":
    e.ToolTipTitle = "untable";
    e.ToolTipText = "Remove tabling instrumentation";
    break;

case "upcase_atom":
    e.ToolTipTitle = "upcase_atom";
    e.ToolTipText = "Convert atom to upper-case";
    break;

case "use_foreign_library":
    e.ToolTipTitle = "use_foreign_library";
    e.ToolTipText = "Load DLL/shared object (directive)";
    break;



case "use_module":
    e.ToolTipTitle = "use_module";
    e.ToolTipText = "Import a module";
    break;



case "valid_string_goal":
    e.ToolTipTitle = "valid_string_goal";
    e.ToolTipText = "(hook) Goal handles strings";
    break;

case "var":
    e.ToolTipTitle = "var";
    e.ToolTipText = "Type check for unbound variable";
    break;

case "var_number":
    e.ToolTipTitle = "var_number";
    e.ToolTipText = "Check that var is numbered by numbervars";
    break;

case "var_property":
    e.ToolTipTitle = "var_property";
    e.ToolTipText = "Variable properties during macro expansion";
    break;

case "variant_sha1":
    e.ToolTipTitle = "variant_sha1";
    e.ToolTipText = "Term-hash for term-variants";
    break;

case "variant_hash":
    e.ToolTipTitle = "variant_hash";
    e.ToolTipText = "Term-hash for term-variants";
    break;

case "version":
    e.ToolTipTitle = "version";
    e.ToolTipText = "Print system banner message";
    break;



case "visible":
    e.ToolTipTitle = "visible";
    e.ToolTipText = "Ports that are visible in the tracer";
    break;

case "volatile":
    e.ToolTipTitle = "volatile";
    e.ToolTipText = "Predicates that are not saved";
    break;

case "wait_for_input":
    e.ToolTipTitle = "wait_for_input";
    e.ToolTipText = "Wait for input with optional timeout";
    break;

case "when":
    e.ToolTipTitle = "when";
    e.ToolTipText = "Execute goal when condition becomes true";
    break;

case "wildcard_match":
    e.ToolTipTitle = "wildcard_match";
    e.ToolTipText = "POSIX style glob pattern matching";
    break;



case "win_add_dll_directory":
    e.ToolTipTitle = "win_add_dll_directory";
    e.ToolTipText = "Add directory to DLL search path";
    break;



case "win_remove_dll_directory":
    e.ToolTipTitle = "win_remove_dll_directory";
    e.ToolTipText = "Remove directory from DLL search path";
    break;

case "win_exec":
    e.ToolTipTitle = "win_exec";
    e.ToolTipText = "Win32: spawn Windows task";
    break;

case "win_has_menu":
    e.ToolTipTitle = "win_has_menu";
    e.ToolTipText = "Win32: true if console menu is available";
    break;

case "win_folder":
    e.ToolTipTitle = "win_folder";
    e.ToolTipText = "Win32: get special folder by CSIDL";
    break;

case "win_insert_menu":
    e.ToolTipTitle = "win_insert_menu";
    e.ToolTipText = "swipl-win.exe: add menu";
    break;

case "win_insert_menu_item":
    e.ToolTipTitle = "win_insert_menu_item";
    e.ToolTipText = "swipl-win.exe: add item to menu";
    break;

case "win_process_modules":
    e.ToolTipTitle = "win_process_modules";
    e.ToolTipText = "Win32 get .exe and .dll files of the process";
    break;

case "win_shell":
    e.ToolTipTitle = "win_shell";
    e.ToolTipText = "Win32: open document through Shell";
    break;



case "win_registry_get_value":
    e.ToolTipTitle = "win_registry_get_value";
    e.ToolTipText = "Win32: get registry value";
    break;

case "win_get_user_preferred_ui_languages":
    e.ToolTipTitle = "win_get_user_preferred_ui_languages";
    e.ToolTipText = "Win32: get language preferences";
    break;

case "win_window_color":
    e.ToolTipTitle = "win_window_color";
    e.ToolTipText = "Win32: change colors of console window";
    break;

case "win_window_pos":
    e.ToolTipTitle = "win_window_pos";
    e.ToolTipText = "Win32: change size and position of window";
    break;

case "window_title":
    e.ToolTipTitle = "window_title";
    e.ToolTipText = "Win32: change title of window";
    break;

case "with_mutex":
    e.ToolTipTitle = "with_mutex";
    e.ToolTipText = "Run goal while holding mutex";
    break;

case "with_output_to":
    e.ToolTipTitle = "with_output_to";
    e.ToolTipText = "Write to strings and more";
    break;

case "with_quasi_quotation_input":
    e.ToolTipTitle = "with_quasi_quotation_input";
    e.ToolTipText = "Parse quasi-quotation from stream";
    break;

case "with_tty_raw":
    e.ToolTipTitle = "with_tty_raw";
    e.ToolTipText = "Run goal with terminal in raw mode";
    break;

case "working_directory":
    e.ToolTipTitle = "working_directory";
    e.ToolTipText = "Query/change CWD";
    break;

case "write":
    e.ToolTipTitle = "write";
    e.ToolTipText = "Write term";
    break;


case "writeln":
    e.ToolTipTitle = "writeln";
    e.ToolTipText = "Write term, followed by a newline";
    break;



case "write_canonical":
    e.ToolTipTitle = "write_canonical";
    e.ToolTipText = "Write a term with quotes, ignore operators";
    break;



case "write_length":
    e.ToolTipTitle = "write_length";
    e.ToolTipText = "Determine #characters to output a term";
    break;

case "write_term":
    e.ToolTipTitle = "write_term";
    e.ToolTipText = "Write term with options";
    break;



case "writef":
    e.ToolTipTitle = "writef";
    e.ToolTipText = "Formatted write";
    break;



case "writeq":
    e.ToolTipTitle = "writeq";
    e.ToolTipText = "Write term, insert quotes";
    break;





                        default:
                            e.ToolTipTitle = e.HoveredWord;
                            e.ToolTipText = "This is tooltip for '" + e.HoveredWord + "'";
                            break;
                    }
                }
            }

            /*
             * Also you can get any fragment of the text for tooltip.
             * Following example gets whole line for tooltip:
            
            var range = new Range(sender as FastColoredTextBox, e.Place, e.Place);
            string hoveredWord = range.GetFragment"[^\n]").Text;
            e.ToolTipTitle = hoveredWord;
            e.ToolTipText = "This is tooltip for '" + hoveredWord + "'";

             */
        }
        private void clearStyles(FastColoredTextBoxNS.TextChangedEventArgs e)
        { if (CurrentTB != null) CurrentTB.Range.tb.ClearStylesBuffer(); }

        private void applyPrologSyntax(FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            try
            {
                CurrentTB.Range.tb.ClearStylesBuffer();

                e.ChangedRange.SetStyle(comment, "((\\%[^\n\r]*))");
                e.ChangedRange.SetStyle(comment, "(?<!\\/)\\/\\*((?:(?!\\*\\/).|\\s)*)\\*\\/", RegexOptions.Multiline);

                e.ChangedRange.SetStyle(question, "(?!<=%)\\?");

                e.ChangedRange.SetStyle(parentesi, "(?!<=%)[\\[\\]()]|\\{\\}");
                e.ChangedRange.SetStyle(keyword, "^(?!<=%)/[a-zA-Z_][a-zA-Z_0-9]*\\s*\\(*\\)\\s*:-");
                e.ChangedRange.SetStyle(functionsa, "(?!<%)\\b\\w*(?=\\()|\\b(abolish|abolish_all_tables|abolish_module_tables|abolish_montonic_tables|abolish_nonincremental_tables|abolish_private_tables|abolish_private_tables|abolish_shared_tables|abolish_table_subgoals|abort|absolute_file_name|answer_count_restraint|access_file|acyclic_term|add_import_module|add_nb_set|append|apple_current_locale_identifier|apply|apropos|arg|assoc_to_list|assert|asserta|assertion|assertz|attach_console|attach_packs|attribute_goals|attr_unify_hook|attr_portray_hook|attvar|at_end_of_stream|at_halt|atom|atom_chars|atom_codes|atom_concat|atom_length|atom_number|atom_prefix|atom_string|atom_to_term|atomic|atomic_concat|atomic_list_concat|atomics_to_string|autoload|autoload_all|autoload_path|await|b_getval|b_set_dict|b_setval|bagof|between|blob|bounded_number|break|break_hook|byte_count|call|call_cleanup|call_dcg|call_delays|call_residue_vars|call_residual_program|call_shared_object_function|call_with_depth_limit|call_with_inference_limit|callable|cancel_halt|catch|char_code|char_conversion|char_type|character_count|chdir|chr_constraint|chr_show_store|chr_trace|chr_type|chr_notrace|chr_leash|chr_option|clause|clause_property|close|close_dde_conversation|close_shared_object|collation_key|comment_hook|compare|compile_aux_clauses|compile_predicates|compiling|compound|compound_name_arity|compound_name_arguments|code_type|consult|context_module|convert_time|copy_stream_data|copy_predicate_clauses|copy_term|copy_term_nat|create_prolog_flag|current_arithmetic_function|current_atom|current_blob|current_char_conversion|current_char_conversion|current_engine|current_flag|current_foreign_library|current_format_predicate|current_functor|current_input|current_key|current_locale|current_module|current_op|current_output|current_predicate|current_signal|current_stream|current_table|current_transaction|current_trie|cyclic_term|day_of_the_week|date_time_stamp|dcg_translate_rule|dde_current_connection|dde_current_service|dde_execute|dde_register_service|dde_request|dde_poke|dde_unregister_service|debug|debug_control_hook|debugging|default_module|del_attr|del_attrs|del_dict|delays_residual_program|delete_directory|delete_file|delete_import_module|det|deterministic|dif|directory_files|discontiguous|divmod|downcase_atom|duplicate_term|dwim_match|dwim_predicate|dynamic|edit|elif|else|empty_assoc|empty_nb_set|encoding|endif|engine_create|engine_destroy|engine_fetch|engine_next|engine_next_reified|engine_post|engine_self|engine_yield|ensure_loaded|erase|exception|exists_directory|exists_file|exists_source|expand_answer|expand_file_name|expand_file_search_path|expand_goal|expand_query|expand_term|expects_dialect|explain|export|fail|false|fast_term_serialized|fast_read|fast_write|current_prolog_flag|file_base_name|file_name_extension|file_search_path|find_chr_constraint|findall|findnsols|fill_buffer|flag|float|float_class|float_parts|flush_output|forall|format|format_time|format_predicate|term_attvars|term_variables|text_to_string|freeze|frozen|functor|garbage_collect|garbage_collect_atoms|garbage_collect_clauses|gen_assoc|gen_nb_set|gensym|get|get_assoc|get_attr|get_attrs|get_byte|get_char|get_code|get_dict|get_flag|get_single_char|get_string_code|get_time|get0|getenv|goal_expansion|ground|gdebug|gspy|gtrace|guitracer|gxref|halt|term_hash|help|help_hook|if|ignore|import|import_module|in_pce_thread|in_pce_thread_sync|include|initialization|initialize|instance|integer|interactor|is|is_absolute_file_name|is_assoc|is_async|is_dict|is_engine|is_list|is_most_general_term|is_object|is_stream|is_trie|is_thread|join_threads|keysort|known_licenses|last|leash|length|library_directory|license|line_count|line_position|list_debug_topics|list_to_assoc|list_to_set|list_strings|load_files|load_foreign_library|locale_create|locale_destroy|locale_property|locale_sort|make|make_directory|make_library_index|map_assoc|dict_create|dict_pairs|max_assoc|memberchk|message_hook|message_line_element|message_property|message_queue_create|message_queue_destroy|message_queue_property|message_queue_set|message_to_string|meta_predicate|min_assoc|module|module_property|module_transparent|msort|multifile|mutex_create|mutex_destroy|mutex_lock|mutex_property|mutex_statistics|mutex_trylock|mutex_unlock|mutex_unlock_all|name|nb_current|nb_delete|nb_getval|nb_link_dict|nb_linkarg|nb_linkval|nb_set_to_list|nb_set_dict|nb_setarg|nb_setval|nl|nodebug|noguitracer|nonground|nonvar|nonterminal|noprofile|noprotocol|normalize_space|nospy|nospyall|not|not_exists|notrace|nth_clause|nth_integer_root_and_remainder|number|number_chars|number_codes|number_string|numbervars|on_signal|once|op|open|open_dde_conversation|open_null_stream|open_resource|open_shared_object|open_source_hook|open_string|ord_list_to_assoc|parse_time|pce_dispatch|pce_call|peek_byte|peek_char|peek_code|peek_string|phrase|phrase_from_quasi_quotation|please|plus|portray|predicate_property|predsort|print|print_message|print_message_lines|profile|profile_count|profiler|prolog|prolog_alert_signal|prolog_choice_attribute|prolog_current_choice|prolog_current_frame|prolog_cut_to|prolog_edit:locate|prolog_edit:edit_source|prolog_edit:edit_command|prolog_edit:load|prolog_exception_hook|prolog_file_type|prolog_frame_attribute|prolog_ide|prolog_interrupt|prolog_list_goal|prolog_listen|prolog_load_context|prolog_load_file|prolog_skip_level|prolog_skip_frame|prolog_stack_property|prolog_to_os_filename|prolog_trace_interception|prolog_unlisten|project_attributes|prompt|prompt1|protocol|protocola|protocolling|public|put|put_assoc|put_attr|put_attrs|put_byte|put_char|put_code|put_code|put_dict|qcompile|qsave_program|quasi_quotation_syntax|quasi_quotation_syntax_error|radial_restraint|random_property|rational|read|read_clause|read_link|read_pending_codes|read_pending_chars|read_string|read_term|read_term_from_atom|read_term_with_history|recorda|recorded|recordz|redefine_system_predicate|reexport|reload_foreign_libraries|reload_library_index|rename_file|repeat|require|reset|reset_gensym|reset_profiler|resource|retract|retractall|same_file|same_term|see|seeing|seek|seen|select_dict|set_end_of_stream|set_flag|set_input|set_locale|set_malloc|set_module|set_output|set_prolog_IO|set_prolog_flag|set_prolog_gc_thread|set_prolog_stack|set_random|set_stream|set_stream_position|set_system_IO|setup_call_cleanup|setup_call_catcher_cleanup|setarg|setenv|setlocale|setof|shell|shift|shift_for_copy|show_profile|sig_atomic|sig_block|sig_pending|sig_remov|sig_unblock|size_abstract_term|size_file|size_nb_set|skip|sleep|snapshot|sort|source_exports|source_file|source_file_property|source_location|split_string|spy|stamp_date_time|statistics|stream_pair|stream_position_data|stream_property|string|string_bytes|string_concat|string_length|string_chars|string_codes|string_code|string_lower|string_upper|string_predicate|strip_module|style_check|sub_atom|sub_atom_icasechk|sub_string|subsumes_term|succ|swritef|tab|table|tabled_call|tdebug|tell|telling|term_expansion|term_singletons|term_string|term_subsumer|term_to_atom|thread_affinity|thread_alias|thread_at_exit|thread_create|thread_detach|thread_exit|thread_get_message|thread_idle|thread_initialization|thread_join|thread_local|thread_message_hook|thread_peek_message|thread_property|thread_self|thread_send_message|thread_property|thread_self|thread_send_message|thread_setconcurrency|thread_signal|thread_statistics|thread_update|thread_wait|threads|throw|time|time_file|tmp_file|tmp_file_stream|tnodebug|tnot|told|tprofile|trace|tracing|transaction|transaction_updates|trie_delete|trie_destroy|trie_gen|trie_gen_compiled|trie_insert|trie_lookup|trie_new|trie_property|trie_update|trie_term|trim_heap|trim_stacks|tripwire|true|tspy|tty_get_capability|tty_goto|tty_put|tty_size|ttyflush|undefined|undo|unify_with_occurs_check|unifiable|unknown|unload_file|unload_foreign_library|unsetenv|untable|upcase_atom|use_foreign_library|use_module|valid_string_goal|var|var_number|var_property|variant_sha1|variant_hash|version|visible|volatile|wait_for_input|when|wildcard_match|win_add_dll_directory|win_remove_dll_directory|win_exec|win_has_menu|win_folder|win_insert_menu|win_insert_menu_item|win_process_modules|win_shell|win_registry_get_value|win_get_user_preferred_ui_languages|win_window_color|win_window_pos|window_title|with_mutex|with_ouut_to|with_quasi_quotation_input|with_tty_raw|working_directory|write|writeln|write_canonical|write_length|write_term|writef|writeq)\\(\\s*[^)]*\\s*\\)");
                e.ChangedRange.SetStyle(variables, "(?!<%)\\b*[A-Z][A-Za-z0-9_]*\\b||\\b_[A-Z][A-Za-z0-9_]*\\b");

                e.ChangedRange.SetStyle(comma, "(?!<=%)\\,");
                e.ChangedRange.SetStyle(point, "(?!<=%)\\.");

                e.ChangedRange.SetStyle(equals, "(?!<=%)=|@|<|>");
                e.ChangedRange.SetStyle(anon, "(?!<=%)_");
                CurrentTB.Refresh();
            }
            catch (Exception)
            { toolStripStatusLabel.Text = "Exceeded Prolog!"; }
        }

        private void Asterisk()
        {
            if (this.Text[this.Text.Length - 1] != '*') //if there isn't an asterisk at the end
            { this.Text = this.Text + "*"; } }

        private void MDIParent1_Load(object sender, EventArgs e)
        { startupCheck(); }

        public void startupCheck()
        {
            check(); //check for checked properties and all
            WindowsCheck(); //check for your Windows version

            faTabStrip1.RemoveTab(faTabStripItem1); //remove the tab with fastcontrol1 - old code
            centerPanel(roundedPanel1);
            tabChangeCheck(); //check on which tab you currently are
            timer.Interval = Properties.Settings.Default.saveTimer * 1000;
            timer.Tick += Timer_Tick; //timer events to manage panels disappearing
            gramlexSendCheck();

            //Mica support
            micaSupport();

            if (Properties.Settings.Default.untitledFileStartup)
                CreateTab(null); //create a new untitled tab - optional
            checkPadding(); //check the padding in the toolbar

            //window state
            setWindowState(); setWindowSize(); setWindowPosition(); setStartWindowPlace(); //set in which position it must start
            faTabStrip1.RemoveTab(faTabStripItem1); //remove the tab where there's fastcolored1 so all tabs have CurrentTB
            string curDir = Directory.GetCurrentDirectory(); //get the current app directory
            if (CurrentTB != null)
            {
                CurrentTB.AddStyle(comment);
                CurrentTB.DescriptionFile = String.Format("file:///{0}/" + Properties.Settings.Default.descriptionFileDirectory, curDir); //modify description file of the fastcolored textbox
            }
            autoCompleteMenuPopulate(); //add all the system functions

            if (Properties.Settings.Default.auroraCustom) //if there's a custom aurora enabled
                this.webBrowser2.Navigate(Properties.Settings.Default.auroraCustomURL); //navigate to it
            else
                this.webBrowser2.Url = new Uri(String.Format("file:///{0}/aurora/" + Properties.Settings.Default.auroraFile, curDir)); //else navigate to the aurora you selected
            label3.Text = "Version " + Properties.Resources.AppVersion; //set start page text
            label4.Text = "Built on " + Properties.Resources.AppDate; //set start page text
            //panel2.Left = faTabStrip1.Width - panel2.Width - 20; //set changelog panel location
            string appPath = Path.GetDirectoryName(Application.ExecutablePath); //get the current app directory
            string filePath = Path.Combine(appPath, "changelog.txt"); //load the changelog.txt file

            try
            {
                if (File.Exists(filePath)) //if the changelog.txt file exists
                {
                    richTextBox3.LoadFile(filePath, RichTextBoxStreamType.PlainText); //load the changelog
                }
                else
                {
                    richTextBox3.Text = "Changelog not found"; //show that the file hasn't been found
                }
            }
            catch (ArgumentException)
            { }

            //start page
            if (Properties.Settings.Default.startPageVisible) //start page
                if (!faTabStrip1.Items.Contains(faTabStripItem2)) faTabStrip1.AddTab(faTabStripItem2);
                else
                    faTabStrip1.RemoveTab(faTabStripItem2);

            if (Properties.Settings.Default.startPageQuick) //quick actions in start page
                panel1.Visible = true;
            else
                panel1.Visible = false;

            if (Properties.Settings.Default.startPageChangelog) //changelog in start page
                panel2.Visible = true;
            else
                panel2.Visible = false;

            if (Properties.Settings.Default.startPageStartup) //verify if the start page should start on startup
            {
                if (!faTabStrip1.Items.Contains(faTabStripItem2)) faTabStrip1.AddTab(faTabStripItem2);
                Properties.Settings.Default.startPageVisible = true;
                ruler.Visible = false;
            }
            else
            {
                faTabStrip1.RemoveTab(faTabStripItem2);
                Properties.Settings.Default.startPageVisible = false;
            }

            label40.Text = Properties.Resources.AppName; //app versions
            label39.Text = Properties.Resources.AppEdition + " Edition"; label38.Text = Properties.Resources.AppDescription; //app versions
            faTabStrip1.SelectedItem = faTabStripItem2; //navigate to the start page

            if (Properties.Settings.Default.auroraStartPage) //load the aurora on start page
                webBrowser2.Visible = true;
            else
                webBrowser2.Visible = false;

            

            rulerToolStripMenuItem.Checked = false;
            //line and column property
            if (Properties.Settings.Default.LineColumnView)
                toolStripStatusLabel2.Visible = true;
            else
                toolStripStatusLabel2.Visible = false;

            if (Properties.Settings.Default.documentMap)
            {
                documentMap1.Visible = true;
                splitContainer2.Panel2Collapsed = false;
            }
            else
            {
                documentMap1.Visible = false;
                splitContainer2.Panel2Collapsed = true;
            }

            trackBar3.Maximum = Properties.Settings.Default.tabMaxLength; //tab length

            //text properties visible or not
            if (Properties.Settings.Default.textPropertiesVisible)
                panel8.Visible = true;
            else
                panel8.Visible = false;

            //no file opened check
            if (Properties.Settings.Default.startPageStartup == false && Properties.Settings.Default.untitledFileStartup == false)
                noFileOpenVisible();
            if (Properties.Settings.Default.animationResizeEnable && !resize9x)
                Transition.run(panel2, "Left", faTabStrip1.Width - panel2.Width - 20, new TransitionType_EaseInEaseOut(Properties.Settings.Default.animationResizeTop));
            else
                panel2.Left = faTabStrip1.Width - panel2.Width - 20;

            if (toolStrip1.Visible)
            {
                toolStrip2.Visible = false; toolStrip3.Visible = false; toolStrip4.Visible = false;
            }
            else if (toolStrip2.Visible)
            {
                toolStrip1.Visible = false; toolStrip3.Visible = false; toolStrip4.Visible = false;

            }
            else if (toolStrip3.Visible)
            {
                toolStrip1.Visible = false; toolStrip2.Visible = false; toolStrip4.Visible = false;
            }
            else if (toolStrip4.Visible)
            {
                toolStrip1.Visible = false; toolStrip2.Visible = false; toolStrip3.Visible = false;
            }

            if (Properties.Settings.Default.rulerVisible && !Properties.Settings.Default.startPageStartup)
            {
                ruler.Visible = true;
                rulerToolStripMenuItem.Checked = true;
            }
            else
            {
                ruler.Visible = false;

            }

            if (Properties.Settings.Default.fluentColorIcons) colorFluentIcons(); else uncolorFluentIcons();
        }

        public void micaSupport()
        {
            Version version = NtDll.RtlGetVersion();
            if ((version.Build > 19045 && Properties.Settings.Default.micaVariant) || Properties.Settings.Default.micaVariantForce)
            {
                menuStrip1.BackColor = SystemColors.MenuBar; toolStrip1.BackColor = SystemColors.MenuBar; toolStrip2.BackColor = SystemColors.MenuBar;
                toolStrip3.BackColor = SystemColors.MenuBar; toolStrip4.BackColor = SystemColors.MenuBar; ruler.BackColor = SystemColors.MenuBar;
                ruler.BackColor2 = SystemColors.MenuBar;
            }
            else if (version.Build < 19045 || Properties.Settings.Default.micaVariant == false || Properties.Settings.Default.micaVariantForce == false)
            {
                menuStrip1.BackColor = SystemColors.Control; toolStrip1.BackColor = SystemColors.Control; toolStrip2.BackColor = SystemColors.Control;
                toolStrip3.BackColor = SystemColors.Control; toolStrip4.BackColor = SystemColors.Control; ruler.BackColor2 = SystemColors.Control;
                ruler.BackColor = SystemColors.Control;
            }
        }

        DateTime lastNavigatedDateTime = DateTime.Now;

        private bool NavigateBackward()
        {
            try
            {
                DateTime max = new DateTime();
                int iLine = -1;
                FastColoredTextBoxNS.FastColoredTextBox tb = null;
                for (int iTab = 0; iTab < faTabStrip1.Items.Count; iTab++)
                {
                    if (faTabStrip1.Items[iTab] != faTabStripItem2)
                    {
                        FastColoredTextBoxNS.FastColoredTextBox t = (faTabStrip1.Items[iTab].Controls[0] as FastColoredTextBoxNS.FastColoredTextBox);
                        for (int i = 0; i < t.LinesCount; i++)
                            if (t[i].LastVisit < lastNavigatedDateTime && t[i].LastVisit > max)
                            {
                                max = t[i].LastVisit;
                                iLine = i;
                                tb = t;
                            }
                    }
                }
            
            if (iLine >= 0)
            {
                faTabStrip1.SelectedItem = (tb.Parent as FarsiLibrary.Win.FATabStripItem);
                tb.Navigate(iLine);
                 lastNavigatedDateTime = tb[iLine].LastVisit;
                tb.Focus();
                tb.Invalidate();
                return true;
            }
            else
                return false;
        }
        catch (NullReferenceException)
        {
        }
        return false;
        }

        private bool NavigateForward()
        {
            try
            {
                DateTime min = DateTime.Now;
                int iLine = -1;
                FastColoredTextBoxNS.FastColoredTextBox tb = null;
                for (int iTab = 0; iTab < faTabStrip1.Items.Count; iTab++)
                {
                    if (faTabStrip1.Items[iTab] != faTabStripItem2)
                    {
                        FastColoredTextBoxNS.FastColoredTextBox t = (faTabStrip1.Items[iTab].Controls[0] as FastColoredTextBoxNS.FastColoredTextBox);
                        for (int i = 0; i < t.LinesCount; i++)
                            if (t[i].LastVisit > lastNavigatedDateTime && t[i].LastVisit < min)
                            {
                                min = t[i].LastVisit;
                                iLine = i;
                                tb = t;
                            }
                    }
                }
                if (iLine >= 0)
                {
                    faTabStrip1.SelectedItem = (tb.Parent as FarsiLibrary.Win.FATabStripItem);
                    tb.Navigate(iLine);
                    lastNavigatedDateTime = tb[iLine].LastVisit;
                    tb.Focus();
                    tb.Invalidate();
                    return true;
                }
                else
                    return false;
            }
            catch (NullReferenceException)
            {
            }
            return false;


        }

        private void Fastcolored1_Load(object sender, EventArgs e)
        {;
            if (CurrentTB != null)
            {
                CurrentTB.DescriptionFile = Properties.Settings.Default.descriptionFileDirectory; //load the description file in CurrentTB
                CurrentTB.AddStyle(comment);
            }

            CurrentTB.Font = Properties.Settings.Default.defaultFont; //load the default font - there's a problem in fastcolored1           
        }


        private void MDIParent1_FormClosing(object sender, FormClosingEventArgs e)
        {
            List<FarsiLibrary.Win.FATabStripItem> list = new List<FarsiLibrary.Win.FATabStripItem>();
            foreach (FarsiLibrary.Win.FATabStripItem tab in faTabStrip1.Items)
                list.Add(tab);
            foreach (FarsiLibrary.Win.FATabStripItem tab in list)
            {
                FarsiLibrary.Win.TabStripItemClosingEventArgs args = new FarsiLibrary.Win.TabStripItemClosingEventArgs(tab);
                faTabStrip1_TabStripItemClosing(args);
                if (args.Cancel)
                {
                    e.Cancel = true;
                    return;
                }
                faTabStrip1.RemoveTab(tab);
            }


            if (Properties.Settings.Default.autoWindowState)
            { windowState(); }

            if (Properties.Settings.Default.autoWindowPosition) getWindowPosition();

            if (Properties.Settings.Default.autoWindowSize) getWindowSize();
        }

        private void printText()
        {
            //printing - disabled for now
            System.Windows.Forms.PrintDialog dialogue = new System.Windows.Forms.PrintDialog();
            DialogResult dr = dialogue.ShowDialog();
            if (dr == DialogResult.OK)
            {
                printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                printDocument1.PrintPage += PrintDocumentOnPrintPage;
                printDocument1.Print();
            }

        }

        private void PrintDocumentOnPrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(this.Fastcolored1.Text, this.Fastcolored1.Font, Brushes.Black, 10, 25); //Drawing the document - disabled for now
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printText();  //printing
        }

        private void openFileToolStripMenuItem_Click_1(object sender, EventArgs e)
        { openFile(); tabChangeCheck(); }

        private void saveToolStripMenuItem_Click_1(object sender, EventArgs e)
        { SaveFile(currentFilePath); }

        private void saveAsToolStripMenuItem_Click_2(object sender, EventArgs e)
        { SaveFileAsNew(); }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        { Application.Exit(); }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.SelectedText = ""; //delete the current selected tetx
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.Text = ""; //clear the current document
        }

        private void menuBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (menuStrip1.Visible) //if the menu bar is visible
            {
                menuStrip1.Visible = false; //it shouldn't be visible
                menuBarToolStripMenuItem.Checked = false; //it shouldn't be checked
                menuBarToolStripMenuItem1.Checked = false;

            }
            else
            {
                menuStrip1.Visible = true; //it should be visible
                menuBarToolStripMenuItem1.Checked = true; //it should be checked
                menuBarToolStripMenuItem.Checked = true;
            }
        }

        private void toolstripCheck()
        {
            if (toolStrip1.Visible || toolStrip2.Visible || toolStrip3.Visible || toolStrip4.Visible) //toolbar check
            { toolbarToolStripMenuItem.Checked = true; toolbarToolStripMenuItem1.Checked = true; }
            else if ((toolStrip1.Visible == false && Properties.Settings.Default.fluentStyle) || (toolStrip2.Visible == false && (Properties.Settings.Default.lunaStyle || Properties.Settings.Default.lunaXPStyle || Properties.Settings.Default.flat8Style)) || (toolStrip3.Visible == false && Properties.Settings.Default.classicStyle) || (toolStrip4.Visible == false && (Properties.Settings.Default.ClassicNineStyle || Properties.Settings.Default.PantherStyle)))
            {
                toolbarToolStripMenuItem.Checked = false; toolbarToolStripMenuItem1.Checked = false;
            }
        }

        private void updateInterface()
        {
            try
            {
                if (CurrentTB != null && faTabStrip1.Items.Count > 0)
                {
                    FastColoredTextBoxNS.FastColoredTextBox tb = CurrentTB;
                    toolStripLabel4.Enabled = toolStripButton20.Enabled = toolStripButton8.Enabled = toolStripButton32.Enabled = undoToolStripMenuItem.Enabled = tb.UndoEnabled;
                    toolStripLabel9.Enabled = toolStripButton21.Enabled = toolStripButton9.Enabled = toolStripButton33.Enabled = redoToolStripMenuItem.Enabled = tb.RedoEnabled;
                    toolStripLabel8.Enabled = toolStripButton16.Enabled = toolStripButton4.Enabled = toolStripButton28.Enabled = saveToolStripMenuItem.Enabled = saveToolStripMenuItem1.Enabled = tb.IsChanged;
                    toolStripLabel9.Enabled = toolStripButton21.Enabled = toolStripButton7.Enabled = toolStripButton31.Enabled = pasteToolStripMenuItem.Enabled = pasteToolStripMenuItem1.Enabled = pasteToolStripMenuItem2.Enabled = (Clipboard.ContainsAudio() || Clipboard.ContainsText() || Clipboard.ContainsImage());
                    toolStripLabel7.Enabled = toolStripButton17.Enabled = toolStripButton5.Enabled = toolStripButton29.Enabled = cutToolStripMenuItem.Enabled = cutToolStripMenuItem1.Enabled = cutToolStripMenuItem2.Enabled =
                    toolStripLabel6.Enabled = toolStripButton18.Enabled = toolStripButton6.Enabled = toolStripButton30.Enabled = copyToolStripMenuItem.Enabled = copyToolStripMenuItem1.Enabled = copyToolStripMenuItem2.Enabled = !tb.Selection.IsEmpty;

                }
                else
                {
                    tabChangeCheck();
                    dvgObjectExplorer.RowCount = 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void check()
        {

            //case sensitive - words whole
            if (Properties.Settings.Default.caseSensitive) caseSensitiveToolStripMenuItem.Checked = true; else caseSensitiveToolStripMenuItem.Checked = false;
            if (Properties.Settings.Default.wholeWordsFind) findWholeWordsOnlyToolStripMenuItem.Checked = true; else caseSensitiveToolStripMenuItem.Checked = false;
            if (Properties.Settings.Default.objectBrowserSort) comboBox6.SelectedIndex = 0; else comboBox6.SelectedIndex = 1;
            if (faTabStrip1.SelectedItem == faTabStripItem2 || faTabStrip1.Items.Count == 0) ruler.Visible = false;

            if (menuStrip1.Visible) //menu bar check
            { menuBarToolStripMenuItem.Checked = true; menuBarToolStripMenuItem1.Checked = true; }
            else
            {  menuBarToolStripMenuItem.Checked = false; menuBarToolStripMenuItem1.Checked = false; }

            if (panel12.Visible) objectBrowserToolStripMenuItem.Checked = true; else objectBrowserToolStripMenuItem.Checked = false;
            if (!ruler.Visible)
            { rulerToolStripMenuItem.Checked = false; }
            else { rulerToolStripMenuItem.Checked = true; }

            toolstripCheck();

            if (Fastcolored1.WordWrap) //word wrap check
            { wordWrapToolStripMenuItem.Checked = true; wordWrapToolStripMenuItem1.Checked = true; }
            else
            { wordWrapToolStripMenuItem.Checked = false; wordWrapToolStripMenuItem1.Checked = false; }

            if (Fastcolored1.WordWrapIndent > 0) wordWrapIndentToolStripMenuItem.Checked = true;
            else wordWrapIndentToolStripMenuItem.Checked = false;

            if (Fastcolored1.DescriptionFile == null) //description file check
                checkForSyntaxToolStripMenuItem.Checked = false;
            else
                checkForSyntaxToolStripMenuItem.Checked = true;

            documentMapCheck();

            if (roundedPanel1.Visible) //if there's the go to line panel then check it
            { goToToolStripMenuItem.Checked = true; goToToolStripMenuItem1.Checked = true; }
            else { goToToolStripMenuItem.Checked = false; goToToolStripMenuItem1.Checked = false; }

            if (panel6.Visible) //the same but for the find and replace panel
            { findreplaceToolStripMenuItem.Checked = true; fIndAndReplaceToolStripMenuItem.Checked = true; }
            else { findreplaceToolStripMenuItem.Checked = false; fIndAndReplaceToolStripMenuItem.Checked = false; }


            if (toolStripStatusLabel2.Visible && Properties.Settings.Default.LineColumnView) lineAndColumnToolStripMenuItem.Checked = true;
            else lineAndColumnToolStripMenuItem.Checked = false;

            //starting line and column
            if (currentLine.Equals(""))
            { currentLine = 1; }

            if (currentColumn.Equals(""))
            { currentColumn = 1; }

            //column limits
            if (Properties.Settings.Default.antoniottiStandard)
            {  columnsToolStripMenuItem.Checked = true; crazy80ToolStripMenuItem.Checked = false; noLimitToolStripMenuItem.Checked = false; }
            else if (Properties.Settings.Default.antoniottiCrazy && antoniotti80Panel.Visible == true)
            { columnsToolStripMenuItem.Checked = false; crazy80ToolStripMenuItem.Checked = true; noLimitToolStripMenuItem.Checked = false; }
            else if (!Properties.Settings.Default.antoniottiCrazy)
            { crazy80ToolStripMenuItem.Checked = false; antoniotti80Panel.Visible = false; }
            else if (!Properties.Settings.Default.antoniottiCrazy && !Properties.Settings.Default.antoniottiStandard)
            { crazy80ToolStripMenuItem.Checked = false; antoniotti80Panel.Visible = false; noLimitToolStripMenuItem.Checked = true; columnsToolStripMenuItem.Checked = false; }

            if (!Properties.Settings.Default.antoniottiCrazy) antoniotti80Panel.Visible = false;

            //edit columns limit based on what column limit you've set up
            columnsToolStripMenuItem.Text = Properties.Settings.Default.columnLineLimit + " columns"; crazy80ToolStripMenuItem.Text = "Crazy " + Properties.Settings.Default.columnLineLimit;

            //web search
            if (Properties.Settings.Default.searchWeb)
            { searchOnTheInternetToolStripMenuItem.Visible = true; searchOnTheInternetToolStripMenuItem1.Visible = true; }
            else
            { searchOnTheInternetToolStripMenuItem.Visible = false; searchOnTheInternetToolStripMenuItem1.Visible = false; }

            if (Properties.Settings.Default.documentMap)
            { documentMap1.Visible = true; splitContainer2.Panel2Collapsed = false; }
            else
            { documentMap1.Visible = false; splitContainer2.Panel2Collapsed = true;  }

            Version version = NtDll.RtlGetVersion();
            if (version.Major == 4 || (version.Major == 5) || (version.Major == 6 && (version.Minor == 0 || version.Minor == 1 || version.Minor == 2 || version.Minor == 3))) //if you're running this on NT 4, 2000, XP or Vista don't show XAML - might be deprecated
                webBrowser2.Visible = false;

            //stay on top check
            if (Properties.Settings.Default.windowOnTop)
            { stayOnTopToolStripMenuItem.Checked = true; this.TopMost = true; }
            else
            { stayOnTopToolStripMenuItem.Checked = false; this.TopMost = false; }

            //gramlex
            if (Properties.Settings.Default.gramlexIntegration)
                runWithJFlexToolStripMenuItem.Visible = true;
            else if (Properties.Settings.Default.gramlexIntegration == false)
                runWithJFlexToolStripMenuItem.Visible = false;
        }

        private void toolbarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (toolStrip1.Visible && Properties.Settings.Default.fluentStyle)
            { toolStrip1.Visible = false; toolbarToolStripMenuItem.Checked = false; toolbarToolStripMenuItem1.Checked = false; }
            else if (!toolStrip1.Visible && Properties.Settings.Default.fluentStyle)
            { toolStrip1.Visible = true; toolbarToolStripMenuItem.Checked = true; toolbarToolStripMenuItem1.Checked = true; }

            if (toolStrip2.Visible && (Properties.Settings.Default.lunaXPStyle || Properties.Settings.Default.lunaStyle || Properties.Settings.Default.flat8Style))
            { toolStrip2.Visible = false; toolbarToolStripMenuItem.Checked = false; toolbarToolStripMenuItem1.Checked = false; }
            else if (!toolStrip1.Visible && (Properties.Settings.Default.lunaXPStyle || Properties.Settings.Default.lunaStyle || Properties.Settings.Default.flat8Style))
            { toolStrip2.Visible = true; toolbarToolStripMenuItem.Checked = true; toolbarToolStripMenuItem1.Checked = true; }

            if (toolStrip3.Visible && Properties.Settings.Default.classicStyle)
            { toolStrip3.Visible = false; toolbarToolStripMenuItem.Checked = false; toolbarToolStripMenuItem1.Checked = false; }
            else if (!toolStrip3.Visible && Properties.Settings.Default.classicStyle)
            { toolStrip1.Visible = true; toolbarToolStripMenuItem.Checked = true; toolbarToolStripMenuItem1.Checked = true; }

            if (toolStrip4.Visible && (Properties.Settings.Default.ClassicNineStyle || Properties.Settings.Default.PantherStyle))
            { toolStrip4.Visible = false; toolbarToolStripMenuItem.Checked = false; toolbarToolStripMenuItem1.Checked = false; }
            else if (!toolStrip4.Visible && (Properties.Settings.Default.ClassicNineStyle || Properties.Settings.Default.PantherStyle))
            { toolStrip4.Visible = true; toolbarToolStripMenuItem.Checked = true; toolbarToolStripMenuItem1.Checked = true; }

        }

        private void addFooterToolStripMenuItem_Click_1(object sender, EventArgs e)
        { addFooter(); }

        private void addHeaderToolStripMenuItem_Click_1(object sender, EventArgs e)
        { addHeader(); }

        private void addFooter()
        {
            if (Properties.Settings.Default.customHeaderFooter)
            {
                if (Properties.Settings.Default.footerPath)
                    CurrentTB.AppendText("\n" + Properties.Settings.Default.footerText + faTabStrip1.SelectedItem.Title); //footer with title
                else
                    CurrentTB.AppendText("\n" + Properties.Settings.Default.footerText);
            }
            else
            {
                switch (Properties.Settings.Default.syntaxChosen){
                    case "Prolog":
                    if (Properties.Settings.Default.footerPath)
                        CurrentTB.AppendText("\n" + Properties.Settings.Default.footerProlog + faTabStrip1.SelectedItem.Title);
                    else
                        CurrentTB.AppendText("\n" + Properties.Settings.Default.footerProlog);
                break;
                    case "Lisp":
                    if (Properties.Settings.Default.footerPath)
                        CurrentTB.AppendText("\n" + Properties.Settings.Default.footerLisp + faTabStrip1.SelectedItem.Title);
                    else
                        CurrentTB.AppendText("\n" + Properties.Settings.Default.footerLisp);
                break;
                    case "yacc":
                    if (Properties.Settings.Default.footerPath)
                        CurrentTB.AppendText("\n" + Properties.Settings.Default.footeryacc + faTabStrip1.SelectedItem.Title);
                    else
                        CurrentTB.AppendText("\n" + Properties.Settings.Default.footeryacc);
                break;
                    case "jflex":
                        if (Properties.Settings.Default.footerPath)
                            CurrentTB.AppendText("\n" + Properties.Settings.Default.footerjflex + faTabStrip1.SelectedItem.Title);
                        else
                            CurrentTB.AppendText("\n" + Properties.Settings.Default.footerjflex);
                        break;

                    case "None":
                        if (Properties.Settings.Default.footerPath)
                            CurrentTB.AppendText("\n" + Properties.Settings.Default.footernone + faTabStrip1.SelectedItem.Title);
                        else
                            CurrentTB.AppendText("\n" + Properties.Settings.Default.footernone);
                        break;
                }
            }
        }

        private void addHeader()
        {
            if (Properties.Settings.Default.customHeaderFooter)
            {
                CurrentTB.Text = Properties.Settings.Default.headerText + "\n" + CurrentTB.Text; //custom header
            }
            else
            {
                switch (Properties.Settings.Default.syntaxChosen)
                {
                    case "Prolog":
                        CurrentTB.Text = Properties.Settings.Default.headerProlog + "\n" + CurrentTB.Text; //the same as the other
                        break;
                    case "Lisp":
                        CurrentTB.Text = Properties.Settings.Default.headerLispText + "\n" + CurrentTB.Text;
                        break;
                    case "yacc":
                        CurrentTB.Text = Properties.Settings.Default.headerYaccText + "\n" + CurrentTB.Text;
                        break;
                    case "jflex":
                        CurrentTB.Text = Properties.Settings.Default.headerJflexText + "\n" + CurrentTB.Text;
                        break;
                    case "None":
                        CurrentTB.Text = Properties.Settings.Default.headernone + "\n" + CurrentTB.Text;
                        break;
                }
            }
        }

        private void aboutLogixToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 form2 = new AboutBox1(); form2.ShowDialog(); 
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.InsertText("\n%" + toolStripTextBox1.Text); //insert a new comment
            //TODO: redo it completely with settings
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //go to line manager
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, "[^0-9]")) {
                ToolTip hint = new ToolTip();
                hint.IsBalloon = true; hint.ToolTipTitle = "Please enter only numbers"; hint.ToolTipIcon = ToolTipIcon.Error; hint.Show(string.Empty, textBox1, 0); hint.Show("Letters and symbols are not allowed.", textBox1);
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
            } else GoToLine(int.Parse(textBox1.Text)); }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentTB.WordWrap) { CurrentTB.WordWrap = false; wordWrapToolStripMenuItem.Checked = false; wordWrapToolStripMenuItem1.Checked = false; }
            else { CurrentTB.WordWrap = true; wordWrapToolStripMenuItem.Checked = true; wordWrapToolStripMenuItem1.Checked = true; }
        }

        private void toolStripLabel10_Click(object sender, EventArgs e)
        { NavigateForward(); }

        private void toolStripLabel11_Click(object sender, EventArgs e)
        { fontText(); }

        private void wordWrapIndentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Fastcolored1.WordWrapIndent > 0) wordWrapIndentToolStripMenuItem.Checked = true;
            else wordWrapIndentToolStripMenuItem.Checked = false;

            if (CurrentTB.WordWrapIndent > 0)
                CurrentTB.WordWrapIndent = 0;
            else
                CurrentTB.WordWrapIndent = 1;
        }


        // \/\/\/ REDO THESE BOOKMARKS
        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.Bookmarks.Remove(CurrentTB.Selection.Start.iLine);
        }

        private void dataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO: redo it completely
            // Rimuovi solo gli elementi associati ai bookmark
            RemoveBookmarkItems();

            // Aggiungi gli elementi del menu per i bookmark
            foreach (FastColoredTextBoxNS.Bookmark bookmark in Fastcolored1.Bookmarks)
            {
                ToolStripItem item = dataToolStripMenuItem.DropDownItems.Add(bookmark.Name); ToolStripItem item2 = bookmarksToolStripMenuItem.DropDownItems.Add(bookmark.Name); item.Tag = bookmark;
                item.Click += new EventHandler(item_Click); item2.Tag = bookmark; item2.Click += new EventHandler(item_Click); } }

        private void item_Click(object sender, EventArgs e)
        {
            ToolStripItem clickedItem = sender as ToolStripItem;
            if (clickedItem != null)
            {
                FastColoredTextBoxNS.Bookmark bookmark = clickedItem.Tag as FastColoredTextBoxNS.Bookmark;
                if (bookmark != null)
                {
                    bookmark.DoVisible();
                }
            }
        }

        private void RemoveBookmarkItems()
        {
            // Rimuovi gli elementi associati ai bookmark
            List<ToolStripItem> itemsToRemove = new List<ToolStripItem>();
            foreach (ToolStripItem item in dataToolStripMenuItem.DropDownItems)
            {
                if (item.Tag is FastColoredTextBoxNS.Bookmark)
                {
                    itemsToRemove.Add(item);
                }
            }

            foreach (ToolStripItem item in bookmarksToolStripMenuItem.DropDownItems)
            {
                if (item.Tag is FastColoredTextBoxNS.Bookmark)
                {
                    itemsToRemove.Add(item);
                }
            }

            foreach (ToolStripItem item in itemsToRemove)
            {
                dataToolStripMenuItem.DropDownItems.Remove(item);
            }
        }


        private void addToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            CurrentTB.Bookmarks.Add(CurrentTB.Selection.Start.iLine, toolStripTextBox2.Text);
            toolStripTextBox2.Text = "";
        }

        private void addToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            CurrentTB.Bookmarks.Add(CurrentTB.Selection.Start.iLine, toolStripTextBox3.Text);
            toolStripTextBox3.Text = "";
        }

        private bool bookedit = false;

        private void toolStripTextBox2_Enter(object sender, EventArgs e)
        {
            if (toolStripTextBox2.Text == "Title" && bookedit == false)
            {
                toolStripTextBox2.Text = "";
                bookedit = true;
            }
        }

        private void toolStripTextBox2_Leave(object sender, EventArgs e)
        {
            if (bookedit == true)
            {
                toolStripTextBox2.Text = "Title";
                bookedit = false;
            }
        }

        private void documentMapCheck()
        {
            if (documentMap1.Visible == true) //document map - is shown on the right
            {
                documentMapToolStripMenuItem.Checked = true; documentMapToolStripMenuItem1.Checked = true;
                Properties.Settings.Default.documentMap = true; Properties.Settings.Default.Save();
                splitContainer2.Panel2Collapsed = false;
            }
            else
            {
                documentMapToolStripMenuItem.Checked = false; documentMapToolStripMenuItem1.Checked = false;
                Properties.Settings.Default.documentMap = false; Properties.Settings.Default.Save();
                splitContainer2.Panel2Collapsed = true;
            }
        }

        private void documentMapApply()
        {
            if (documentMapToolStripMenuItem.Checked == true || documentMapToolStripMenuItem1.Checked == true)
            {

                Transition.run(documentMap1, "Left", this.Width, new TransitionType_EaseInEaseOut(1000)); documentMap1.Visible = false; splitContainer2.Panel2Collapsed = true;
                documentMapToolStripMenuItem1.Checked = false; documentMapToolStripMenuItem.Checked = false; Properties.Settings.Default.documentMap = false;
            }
            else
            {
                Transition.run(documentMap1, "Left", documentMap1.Left, new TransitionType_EaseInEaseOut(1000)); documentMap1.Visible = true;
                splitContainer2.Panel2Collapsed = false; documentMapToolStripMenuItem1.Checked = true; documentMapToolStripMenuItem.Checked = true; Properties.Settings.Default.documentMap = true;
            }
            Properties.Settings.Default.Save();
        }
        // ^^^^ REDO THESE BOOKMARKS
        private void documentMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            documentMapCheck(); documentMapApply(); }

        public bool createdATab = false;

        private void CreateTab(string fileName)
        {
            try
            {
                FastColoredTextBoxNS.FastColoredTextBox tb = new FastColoredTextBoxNS.FastColoredTextBox(); tb.Dock = DockStyle.Fill;

                tb.Language = Fastcolored1.Language; FarsiLibrary.Win.FATabStripItem tab = new FarsiLibrary.Win.FATabStripItem();
                tab.Tag = fileName;
                if (fileName != null) tb.OpenFile(fileName);
                faTabStrip1.AddTab(tab); faTabStrip1.SelectedItem = tab;
                tb.Focus(); tb.DelayedTextChangedInterval = 1000; tb.DelayedEventsInterval = 500;
                if (fileName == null) tab.Title = "Untitled"; else tab.Title = fileName;
                tab.Controls.Add(tb); createdATab = true;
               
                //clonazione
                tb.BackColor = SystemColors.Window;
                tb.ForeColor = SystemColors.ControlText;
                tb.SelectionColor = SystemColors.HotTrack;
                tb.TextAreaBorderColor = System.Drawing.Color.Transparent;
                tb.CaretColor = SystemColors.AppWorkspace;
                tb.DisabledColor = SystemColors.InactiveBorder;
                tb.BookmarkColor = SystemColors.HotTrack;
                tb.CurrentLineColor = SystemColors.Highlight;
                tb.IndentBackColor = System.Drawing.Color.Transparent;
                tb.FoldingIndicatorColor = SystemColors.MenuHighlight;
                tb.PaddingBackColor = System.Drawing.Color.Transparent;
                tb.ServiceLinesColor = SystemColors.ActiveBorder;

                tb.LeftBracket = '(';
                tb.LeftBracket2 = '[';
                tb.RightBracket = ')';
                tb.RightBracket2 = ']';
                tb.AutoCompleteBrackets = true;
                tb.ContextMenuStrip = contextMenuStrip2;

                tb.LineNumberColor = SystemColors.Highlight;
               
                AutocompleteMenu popupMenu = new AutocompleteMenu();
                popupMenu.SetAutocompleteMenu(CurrentTB, autocompleteMenu1); popupMenu = autocompleteMenu1;
               
                tb.Font = new Font(Properties.Settings.Default.defaultFont.FontFamily, Properties.Settings.Default.defaultFont.Size, Properties.Settings.Default.defaultFont.Style);
                tb.TextChangedDelayed += new EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(tb_TextChangedDelayed);
                tb.ZoomChanged += new EventHandler(CurrentTB_ZoomChanged);
                tb.TextChanged += new EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.CurrentTB_TextChanged);
                tb.MouseDown += new MouseEventHandler(this.fctb_MouseDown);
                tb.MouseMove += new MouseEventHandler(this.fctb_MouseMove);
                tb.ToolTipNeeded += new EventHandler<FastColoredTextBoxNS.ToolTipNeededEventArgs>(fctb_ToolTipNeeded);
                if (Properties.Settings.Default.wordWrapTab)
                { tb.WordWrap = true;  wordWrapToolStripMenuItem.Checked = true; wordWrapToolStripMenuItem1.Checked = true; }
                else
                { tb.WordWrap = false; wordWrapToolStripMenuItem.Checked = false; wordWrapToolStripMenuItem1.Checked = false; }
               
                CurrentTB.TextChanged += CurrentTB_TextChanged;
                tb.SelectionChanged += new EventHandler(CurrentTB_SelectionChanged);
                tb.Zoom = Properties.Settings.Default.zoomFile;

                noFileOpenVisible();
                CurrentTB.Focus(); tabChangeCheck();
                
                if (ruler.Visible) rulerToolStripMenuItem.Checked = true;
                else rulerToolStripMenuItem.Checked = false;

                if (Properties.Settings.Default.documentMapNewTab)
                {
                    documentMap1.Visible = true;
                    splitContainer2.Panel2Collapsed = false;
                    documentMapToolStripMenuItem.Checked = true;
                    documentMapToolStripMenuItem1.Checked = true;
                }
                else if (Properties.Settings.Default.documentMapNewTab == false && Properties.Settings.Default.documentMap == false)
                {
                    documentMap1.Visible = false;
                    splitContainer2.Panel2Collapsed = true;
                    documentMapToolStripMenuItem.Checked = false;
                    documentMapToolStripMenuItem1.Checked = false;
                }

                if (Properties.Settings.Default.syntaxTabNewChange)
                {
                    switch (Properties.Settings.Default.syntaxTabNew)
                    {
                        case "Prolog":
                            Properties.Settings.Default.syntaxChosen = "Prolog";
                            prologToolStripMenuItem.Checked = true;
                            noneToolStripMenuItem.Checked = false;
                            lispToolStripMenuItem.Checked = false;
                            yaccJToolStripMenuItem.Checked = false;
                            jFlexToolStripMenuItem.Checked = false;
                            cToolStripMenuItem.Checked = false;
                            break;

                        case "Lisp":
                            Properties.Settings.Default.syntaxChosen = "Lisp";
                            prologToolStripMenuItem.Checked = false;
                            noneToolStripMenuItem.Checked = false;
                            lispToolStripMenuItem.Checked = true;
                            yaccJToolStripMenuItem.Checked = false;
                            jFlexToolStripMenuItem.Checked = false;
                            cToolStripMenuItem.Checked = false;
                            break;

                        case "jflex":
                            Properties.Settings.Default.syntaxChosen = "jflex";
                            prologToolStripMenuItem.Checked = false;
                            noneToolStripMenuItem.Checked = false;
                            lispToolStripMenuItem.Checked = false;
                            yaccJToolStripMenuItem.Checked = false;
                            jFlexToolStripMenuItem.Checked = true;
                            cToolStripMenuItem.Checked = false;
                            break;

                        case "yacc":
                            Properties.Settings.Default.syntaxChosen = "yacc";
                            prologToolStripMenuItem.Checked = false;
                            noneToolStripMenuItem.Checked = false;
                            lispToolStripMenuItem.Checked = false;
                            yaccJToolStripMenuItem.Checked = true;
                            jFlexToolStripMenuItem.Checked = false;
                            cToolStripMenuItem.Checked = false;
                            break;

                        case "C":
                            Properties.Settings.Default.syntaxChosen = "C";
                            prologToolStripMenuItem.Checked = false;
                            noneToolStripMenuItem.Checked = false;
                            lispToolStripMenuItem.Checked = false;
                            yaccJToolStripMenuItem.Checked = false;
                            jFlexToolStripMenuItem.Checked = false;
                            cToolStripMenuItem.Checked = true;
                            break;

                        case "None":
                            Properties.Settings.Default.syntaxChosen = "None";
                            prologToolStripMenuItem.Checked = false;
                            noneToolStripMenuItem.Checked = true;
                            lispToolStripMenuItem.Checked = false;
                            yaccJToolStripMenuItem.Checked = false;
                            jFlexToolStripMenuItem.Checked = false;
                            cToolStripMenuItem.Checked = false;
                            break;
                    }
                    Properties.Settings.Default.Save(); updateInterface();
                }
                

            }
            catch (Exception ex)
            {

                MessageBox.Show("Exception: " + ex.ToString());
            }
        }

        private FarsiLibrary.Win.FATabStripItem CreateTab1(string fileName)
        {
            try
            {
                FastColoredTextBoxNS.FastColoredTextBox tb = new FastColoredTextBoxNS.FastColoredTextBox(); tb.Dock = DockStyle.Fill;

                tb.Language = Fastcolored1.Language; FarsiLibrary.Win.FATabStripItem tab = new FarsiLibrary.Win.FATabStripItem();
                tab.Tag = fileName;
                if (fileName != null) tb.OpenFile(fileName);
                faTabStrip1.AddTab(tab); faTabStrip1.SelectedItem = tab;
                tb.Focus(); tb.DelayedTextChangedInterval = 1000; tb.DelayedEventsInterval = 500;
                if (fileName == null) tab.Title = "Untitled"; else tab.Title = fileName;
                tab.Controls.Add(tb); createdATab = true;

                //clonazione
                tb.BackColor = SystemColors.Window;
                tb.ForeColor = SystemColors.ControlText;
                tb.SelectionColor = SystemColors.HotTrack;
                tb.TextAreaBorderColor = System.Drawing.Color.Transparent;
                tb.CaretColor = SystemColors.AppWorkspace;
                tb.DisabledColor = SystemColors.InactiveBorder;
                tb.BookmarkColor = SystemColors.HotTrack;
                tb.CurrentLineColor = SystemColors.Highlight;
                tb.IndentBackColor = System.Drawing.Color.Transparent;
                tb.FoldingIndicatorColor = SystemColors.MenuHighlight;
                tb.PaddingBackColor = System.Drawing.Color.Transparent;
                tb.ServiceLinesColor = SystemColors.ActiveBorder;

                tb.LeftBracket = '(';
                tb.LeftBracket2 = '[';
                tb.RightBracket = ')';
                tb.RightBracket2 = ']';
                tb.AutoCompleteBrackets = true;
                tb.ContextMenuStrip = contextMenuStrip2;

                tb.LineNumberColor = SystemColors.Highlight;

                AutocompleteMenu popupMenu = new AutocompleteMenu();
                popupMenu.SetAutocompleteMenu(CurrentTB, autocompleteMenu1); popupMenu = autocompleteMenu1;

                tb.Font = new Font(Properties.Settings.Default.defaultFont.FontFamily, Properties.Settings.Default.defaultFont.Size, Properties.Settings.Default.defaultFont.Style);

                tb.ZoomChanged += new EventHandler(CurrentTB_ZoomChanged);
                tb.TextChangedDelayed += new EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(tb_TextChangedDelayed);
                tb.TextChanged += new EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.CurrentTB_TextChanged);
                tb.MouseDown += new MouseEventHandler(this.fctb_MouseDown);
                tb.MouseMove += new MouseEventHandler(this.fctb_MouseMove);
                tb.ToolTipNeeded += new EventHandler<FastColoredTextBoxNS.ToolTipNeededEventArgs>(fctb_ToolTipNeeded);
                if (Properties.Settings.Default.wordWrapTab)
                { tb.WordWrap = true; wordWrapToolStripMenuItem.Checked = true; wordWrapToolStripMenuItem1.Checked = true; }
                else
                { tb.WordWrap = false; wordWrapToolStripMenuItem.Checked = false; wordWrapToolStripMenuItem1.Checked = false; }

                CurrentTB.TextChanged += CurrentTB_TextChanged;
                tb.SelectionChanged += new EventHandler(CurrentTB_SelectionChanged);
                tb.Zoom = Properties.Settings.Default.zoomFile;

                noFileOpenVisible();
                CurrentTB.Focus(); tabChangeCheck();

                if (ruler.Visible) rulerToolStripMenuItem.Checked = true;
                else rulerToolStripMenuItem.Checked = false;

                if (Properties.Settings.Default.documentMapNewTab)
                {
                    documentMap1.Visible = true;
                    splitContainer2.Panel2Collapsed = false;
                    documentMapToolStripMenuItem.Checked = true;
                    documentMapToolStripMenuItem1.Checked = true;
                }
                else if (Properties.Settings.Default.documentMapNewTab == false && Properties.Settings.Default.documentMap == false)
                {
                    documentMap1.Visible = false;
                    splitContainer2.Panel2Collapsed = true;
                    documentMapToolStripMenuItem.Checked = false;
                    documentMapToolStripMenuItem1.Checked = false;
                }

                if (Properties.Settings.Default.syntaxTabNewChange)
                {
                    switch (Properties.Settings.Default.syntaxTabNew)
                    {
                        case "Prolog":
                            Properties.Settings.Default.syntaxChosen = "Prolog";
                            prologToolStripMenuItem.Checked = true;
                            noneToolStripMenuItem.Checked = false;
                            lispToolStripMenuItem.Checked = false;
                            yaccJToolStripMenuItem.Checked = false;
                            jFlexToolStripMenuItem.Checked = false;
                            cToolStripMenuItem.Checked = false;
                            break;

                        case "Lisp":
                            Properties.Settings.Default.syntaxChosen = "Lisp";
                            prologToolStripMenuItem.Checked = false;
                            noneToolStripMenuItem.Checked = false;
                            lispToolStripMenuItem.Checked = true;
                            yaccJToolStripMenuItem.Checked = false;
                            jFlexToolStripMenuItem.Checked = false;
                            cToolStripMenuItem.Checked = false;
                            break;

                        case "jflex":
                            Properties.Settings.Default.syntaxChosen = "jflex";
                            prologToolStripMenuItem.Checked = false;
                            noneToolStripMenuItem.Checked = false;
                            lispToolStripMenuItem.Checked = false;
                            yaccJToolStripMenuItem.Checked = false;
                            jFlexToolStripMenuItem.Checked = true;
                            cToolStripMenuItem.Checked = false;
                            break;

                        case "yacc":
                            Properties.Settings.Default.syntaxChosen = "yacc";
                            prologToolStripMenuItem.Checked = false;
                            noneToolStripMenuItem.Checked = false;
                            lispToolStripMenuItem.Checked = false;
                            yaccJToolStripMenuItem.Checked = true;
                            jFlexToolStripMenuItem.Checked = false;
                            cToolStripMenuItem.Checked = false;
                            break;

                        case "C":
                            Properties.Settings.Default.syntaxChosen = "C";
                            prologToolStripMenuItem.Checked = false;
                            noneToolStripMenuItem.Checked = false;
                            lispToolStripMenuItem.Checked = false;
                            yaccJToolStripMenuItem.Checked = false;
                            jFlexToolStripMenuItem.Checked = false;
                            cToolStripMenuItem.Checked = true;
                            break;

                        case "None":
                            Properties.Settings.Default.syntaxChosen = "None";
                            prologToolStripMenuItem.Checked = false;
                            noneToolStripMenuItem.Checked = true;
                            lispToolStripMenuItem.Checked = false;
                            yaccJToolStripMenuItem.Checked = false;
                            jFlexToolStripMenuItem.Checked = false;
                            cToolStripMenuItem.Checked = false;
                            break;
                    }
                    Properties.Settings.Default.Save(); updateInterface();
                }

                return tab;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Exception: " + ex.ToString());
            }
            return null;
        }

        private void faTabStrip1_TabStripItemSelectionChanged(FarsiLibrary.Win.TabStripItemChangedEventArgs e)
        {
            try { tabChangeCheck(); }
            catch (Exception ex2) { MessageBox.Show(ex2.ToString()); } }

        private void Zoom_click(object sender, EventArgs e)
        {
            if (CurrentTB != null)
            { CurrentTB.Zoom = int.Parse((sender as ToolStripItem).Tag.ToString()); Properties.Settings.Default.zoomFile = CurrentTB.Zoom; Properties.Settings.Default.Save(); }
 }

        bool tbFindChanged = false;

        public bool moveGoToPanel = false;

        private void MDIParent1_Resize(object sender, EventArgs e)
        {

            if (Properties.Settings.Default.animationResizeEnable && !resize9x)
            {
                Transition.run(panel2, "Left", faTabStrip1.Width - panel2.Width - 20, new TransitionType_EaseInEaseOut(Properties.Settings.Default.animationResizeTop));
            }
            else
            {
                panel2.Left = faTabStrip1.Width - panel2.Width - 20;
            }
            centerElement(noOpenedFileGroup); centerElement(antoniotti80Panel); centerPanel(panel9); centerPanel(panel10); centerPanel(panel11);

            if (!moveGoToPanel && goToDockTop || !moveGoToPanel && goToDockBottom)
            { centerPanel(roundedPanel1); centerPanel(panel6); }
        }

        private void SelectCurrentLine()
        {
            int currentLineIndex = CurrentTB.Selection.Start.iLine;

            if (currentLineIndex >= 0 && currentLineIndex < CurrentTB.LinesCount)
            {
                int lineStartIndex = 0;
                for (int i = 0; i < currentLineIndex; i++)
                {
                    lineStartIndex += CurrentTB[i].Count + Environment.NewLine.Length;
                }
                int lineEndIndex = lineStartIndex + CurrentTB[currentLineIndex].Count;

                CurrentTB.Selection.Start = new FastColoredTextBoxNS.Place(lineStartIndex, currentLineIndex);
                CurrentTB.Selection.End = new FastColoredTextBoxNS.Place(lineEndIndex, currentLineIndex);
            }
        }

        // Select the current function
        private void SelectCurrentFunction()
        {
            int position = CurrentTB.SelectionStart;
            int openParenthesisIndex = CurrentTB.Text.LastIndexOf('(', position);

            if (openParenthesisIndex != -1)
            {
                int functionNameStart = CurrentTB.Text.LastIndexOf(' ', openParenthesisIndex);
                if (functionNameStart == -1)
                {
                    functionNameStart = 0;
                }


            }
        }

        // Select all text inside CurrentTB
        private void SelectAllText()
        { CurrentTB.SelectAll(); }

        private void selectCurrentLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectCurrentLine();
        }

        private void selectCurrentFunctionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectCurrentFunction();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectAllText();
        }

        private void selectCurrentLineToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SelectCurrentLine();
        }

        private void button2_Click(object sender, EventArgs e)
        { roundedPanel1.Visible = false; goToToolStripMenuItem.Checked = false; goToToolStripMenuItem1.Checked = false; }

        private void goToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            centerPanel(roundedPanel1);
            if (!roundedPanel1.Visible)
            {
                roundedPanel1.Visible = true; goToToolStripMenuItem.Checked = true; goToToolStripMenuItem1.Checked = true; }
            else
            {
                roundedPanel1.Visible = false; goToToolStripMenuItem.Checked = false; goToToolStripMenuItem1.Checked = false; }
        }

        private void centerElement(Control control1)
        {
            int centerX = (this.ClientSize.Width - control1.Width) / 2; int centerY = (this.ClientSize.Height - control1.Height) / 2;

            if (Properties.Settings.Default.animationResizeEnable && !resize9x) {
                Transition.run(control1, "Left", centerX, new TransitionType_EaseInEaseOut(Properties.Settings.Default.animationResizeTop));
                Transition.run(control1, "Top", centerY, new TransitionType_EaseInEaseOut(Properties.Settings.Default.animationResizeTop));
            } else { control1.Location = new Point(centerX, centerY); }
        }
        private void centerPanel(Panel panel)
        {
            int centerX = (this.ClientSize.Width - panel.Width) / 2;
            if (Properties.Settings.Default.animationDockBottomEnable && Properties.Settings.Default.animationDockBottom > 0) {
                Transition t = new Transition(new TransitionType_EaseInEaseOut((int)Properties.Settings.Default.animationDockBottom));
                t.add(panel, "Left", centerX); t.add(panel, "Top", panel.Location.Y); t.run(); }
            else { panel.Location = new Point(centerX, panel.Location.Y); } }

        private void GoToLine(int lineNumber)
        {
            lineNumber = lineNumber - 1;
            if (lineNumber >= 0 && lineNumber < CurrentTB.LinesCount)
            {
                FastColoredTextBoxNS.Place startPlace = new FastColoredTextBoxNS.Place(0, lineNumber); FastColoredTextBoxNS.Place endPlace = new FastColoredTextBoxNS.Place(0, lineNumber);

                CurrentTB.Selection.Start = startPlace; CurrentTB.Selection.End = endPlace;

                CurrentTB.DoSelectionVisible(); CurrentTB.Focus(); CurrentTB.Select(); }
            else {
                ToolTip hint = new ToolTip(); hint.IsBalloon = true; hint.ToolTipTitle = "Not a valid line number"; hint.ToolTipIcon = ToolTipIcon.Error;  hint.Show(string.Empty, textBox1, 0);
                if (CurrentTB.LinesCount > 1) hint.Show("Enter a number between 1 and " + CurrentTB.LinesCount + ".", textBox1, 15, 17);
                else hint.Show("You have only one line.", textBox1, 15, 17); } }

        private Point _Offset = Point.Empty; private bool goToDockTop = false; private bool goToDockBottom = false;

        private void mouseMovePanelTopBottom(object sender, MouseEventArgs e, Panel roundedPanel1)
        {
            if (_Offset != Point.Empty)
            {
                Point newlocation = roundedPanel1.Location;
                newlocation.X += e.X - _Offset.X;
                newlocation.Y += e.Y - _Offset.Y;
                if (newlocation.Y > toolStrip1.Height + (panel4.Height + 2))
                {
                    roundedPanel1.Location = newlocation;
                    goToDockTop = false;
                }

                if (newlocation.Y < panel5.Height - statusStrip.Height)
                {
                    roundedPanel1.Location = newlocation;
                    goToDockBottom = false;
                }

                if (roundedPanel1.Top < panel4.Top)
                {
                    roundedPanel1.Top = panel4.Top;
                    if (e.Y > toolStrip1.Height + (panel4.Height - 10))
                    {
                        moveGoToPanel = true;
                        panel4.Visible = false;
                        // Allow the panel to move again when the mouse is in the lower area.
                        roundedPanel1.Invalidate();
                        goToDockTop = false;
                        panel5.Visible = false;
                    }
                    else
                    {
                        moveGoToPanel = false;
                        panel4.Visible = true;
                        if (goToDockTop)
                            centerPanel(roundedPanel1);
                        // Block the top position, center the panel, and show the dock panel.
                        roundedPanel1.Invalidate();
                        goToDockTop = true;
                        panel5.Visible = false;
                    }
                }
                else
                {
                    roundedPanel1.Enabled = true;
                }

                if (roundedPanel1.Top + roundedPanel1.Height > panel5.Top)
                {
                    roundedPanel1.Top = panel5.Top - roundedPanel1.Height + 10;
                    if (e.Y < statusStrip.Height - (panel5.Height + 10) - statusStrip.Height)
                    {
                        moveGoToPanel = true;
                        panel5.Visible = false;
                        roundedPanel1.Invalidate();
                        goToDockBottom = false;
                        panel4.Visible = false;
                    }
                    else
                    {
                        moveGoToPanel = false;
                        panel5.Visible = true;
                        if (goToDockBottom)
                            centerPanel(roundedPanel1);
                        roundedPanel1.Invalidate();
                        goToDockBottom = true;
                        panel4.Visible = false;
                    }
                }
            }
        }
        private void roundedPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            mouseMovePanelTopBottom(sender, e, roundedPanel1);
        }

        private void mouseDownPanelTopBottom(object sender, MouseEventArgs e, Panel roundedPanel1)
        {
            if (e.Button == MouseButtons.Left)
            {
                _Offset = new Point(e.X, e.Y);
                if (e.Y > toolStrip1.Height + (panel4.Height + 2) || e.Y < this.ClientSize.Height - (panel5.Height + 2) - statusStrip.Height)
                    moveGoToPanel = true;
                Cursor.Current = Cursors.SizeAll;
            }
            
        }
        private void roundedPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDownPanelTopBottom(sender, e, roundedPanel1);
        }

        private void mouseUpPanelTopBottom(object sender, MouseEventArgs e, Panel roundedPanel1)
        {
            _Offset = Point.Empty;
            panel4.Visible = false;
            roundedPanel1.Enabled = true;
            if (!moveGoToPanel && goToDockTop || !moveGoToPanel && goToDockBottom)
            {
                centerPanel(roundedPanel1);
                panel5.Visible = false;
            }
            Cursor.Current = Cursors.Default;
        }

        private void roundedPanel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseUpPanelTopBottom(sender, e, roundedPanel1);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        { try {
                if (e.KeyChar == (char)Keys.Enter) {
                    if (System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, "[^0-9]")) {
                        ToolTip hint = new ToolTip(); hint.IsBalloon = true; hint.ToolTipTitle = "Please enter only numbers"; hint.ToolTipIcon = ToolTipIcon.Error;
                        hint.Show(string.Empty, textBox1, -10, -10, 0); hint.Show("Letters and symbols are not allowed.", textBox1); textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1); }
                    else GoToLine(int.Parse(textBox1.Text)); } }
            catch (Exception)
            { ToolTip hint = new ToolTip(); hint.IsBalloon = true; hint.ToolTipTitle = "Please enter only numbers"; hint.ToolTipIcon = ToolTipIcon.Error;
                hint.Show(string.Empty, textBox1, 0);
                if (CurrentTB.LinesCount > 1) hint.Show("Letters and symbols are not allowed.", textBox1, 15, 17);
                else hint.Show("Letters and symbols are not allowed.", textBox1, 15, 17); } }
        
        private void panel4_MouseMove(object sender, MouseEventArgs e)
        { moveGoToPanel = false; }

        private void fIndAndReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            centerPanel(panel6); if (!panel6.Visible) {
                panel6.Visible = true; fIndAndReplaceToolStripMenuItem.Checked = true; findreplaceToolStripMenuItem.Checked = true; }
            else { panel6.Visible = false; fIndAndReplaceToolStripMenuItem.Checked = false; findreplaceToolStripMenuItem.Checked = false; }
        }

        private void tabControl1_MouseMove(object sender, MouseEventArgs e)
        { mouseMovePanelTopBottom(sender, e, panel6); }

        private void tabControl1_MouseUp(object sender, MouseEventArgs e)
        { mouseUpPanelTopBottom(sender, e, panel6); }

        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        { mouseDownPanelTopBottom(sender, e, panel6); }

        private bool findTextEdit = false;

        private void comboBox1_Enter(object sender, EventArgs e)
        { if (!findTextEdit) { findTextEdit = true; comboBox1.Text = ""; } }

        private void button4_Click(object sender, EventArgs e)
        { FindNext(comboBox1.Text); }

        private void button3_Click(object sender, EventArgs e)
        { replaceAll(comboBox1.Text, comboBox3.Text); }

        private void replaceAll(string searchText, string replaceText)
        {
            RegexOptions options = RegexOptions.None; Regex regex = new Regex(searchText, options);
            foreach (Match result in regex.Matches(CurrentTB.Text))
            { CurrentTB.Text = CurrentTB.Text.Remove(result.Index, result.Length).Insert(result.Index, replaceText); } }

        private string ReplaceFirst(string searchText, string replaceText)
        {
            RegexOptions options = RegexOptions.None; Regex regex = new Regex(searchText, options);
            Match match = regex.Match(CurrentTB.Text);
            if (match.Success)
            { string updatedText = regex.Replace(CurrentTB.Text, replaceText, 1); CurrentTB.Text = updatedText; }
            return CurrentTB.Text; }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            { comboBox1.Text = "Find..."; findTextEdit = false; }
        }

        private void panel6_MouseDown(object sender, MouseEventArgs e)
        { mouseDownPanelTopBottom(sender, e, panel6); }

        private void panel6_MouseMove(object sender, MouseEventArgs e)
        { mouseMovePanelTopBottom(sender, e, panel6); }

        private void panel6_MouseUp(object sender, MouseEventArgs e)
        { mouseUpPanelTopBottom(sender, e, panel6); }

        private bool replaceEditText = false;
        private void comboBox3_Enter(object sender, EventArgs e) {
            if (!replaceEditText)
            { replaceEditText = true; comboBox2.Text = ""; } }

        private void comboBox3_Leave(object sender, EventArgs e)
        { if (comboBox2.Text == "") { comboBox2.Text = "Replace..."; replaceEditText = false; } }

        private int nextSearchStartIndex;

        private void FindNext(string searchText)
        {
            string pattern = Properties.Settings.Default.wholeWordsFind ? "\\b" + searchText + "\\b" : searchText;

            RegexOptions options = Properties.Settings.Default.caseSensitive ? RegexOptions.None : RegexOptions.IgnoreCase;
            Regex regex = new Regex(pattern, options); Match match = regex.Match(CurrentTB.Text, this.nextSearchStartIndex);
            if (!match.Success)
            { this.nextSearchStartIndex = 0; panel11.Visible = true; timer.Start(); }
            else { this.nextSearchStartIndex = match.Index + match.Length; CurrentTB.SelectionStart = match.Index; CurrentTB.SelectionLength = match.Length;
                int lineIndex = 0;
                for (int i = 0; i < match.Index; i++) { if (CurrentTB.Text[i] == '\n') { lineIndex++; } }
                CurrentTB.Navigate(lineIndex); CurrentTB.DoSelectionVisible(); CurrentTB.Focus(); } }

        private void button6_Click(object sender, EventArgs e)
        { ReplaceFirst(comboBox1.Text, comboBox3.Text); }

        private void button5_Click(object sender, EventArgs e)
        { contextMenuStrip1.Show(button5, 15, 15); }

        private bool caseSensitive; private bool wholeWordsFind; //SUBSTITUTE THEMWITH 

        private void caseSensitiveToolStripMenuItem_Click(object sender, EventArgs e) {
            if (Properties.Settings.Default.caseSensitive)
            { caseSensitiveToolStripMenuItem.Checked = false; Properties.Settings.Default.caseSensitive = false; }
            else { caseSensitiveToolStripMenuItem.Checked = true; Properties.Settings.Default.caseSensitive = true; }
            Properties.Settings.Default.Save(); }

        private void findWholeWordsOnlyToolStripMenuItem_Click(object sender, EventArgs e) {
            if (Properties.Settings.Default.wholeWordsFind) { findWholeWordsOnlyToolStripMenuItem.Checked = false; Properties.Settings.Default.wholeWordsFind = false; }
            else { findWholeWordsOnlyToolStripMenuItem.Checked = true; Properties.Settings.Default.wholeWordsFind = true; }
            Properties.Settings.Default.Save(); }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        { CurrentTB.SelectedText = ""; }

        private void CurrentTB_ZoomChanged(object sender, EventArgs e) {
            int zoom = 0; zoom = int.Parse(CurrentTB.Zoom.ToString()); btZoom.Text = "Zoom: " + zoom + "%";
            btZoom.Invalidate(); Properties.Settings.Default.zoomFile = zoom; Properties.Settings.Default.Save(); }

        private void label20_Click(object sender, EventArgs e)
        { panel6.Visible = false; fIndAndReplaceToolStripMenuItem.Checked = false; }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e) {
            Settings form3 = new Settings(webBrowser2, faTabStrip1, faTabStripItem2, splitContainer1, startPageToolStripMenuItem, contextMenuStrip3, searchOnTheInternetToolStripMenuItem, contextMenuStrip2, searchOnTheInternetToolStripMenuItem1, menuStrip1, this);
            form3.ShowDialog(); }

        private void faTabStrip1_TabIndexChanged(object sender, EventArgs e){ try {
                if (CurrentTB != null)
                { documentMap1.Target = CurrentTB; string text = CurrentTB.Text; CurrentTB.Zoom = Properties.Settings.Default.zoomFile;
                toolStripTextBox4.Text = faTabStrip1.SelectedItem.Title; } }
            catch (Exception ex2) { MessageBox.Show(ex2.ToString()); }
                noFileOpenVisible(); tabChangeCheck(); updateInterface(); 
            }

        private void faTabStrip1_Click(object sender, EventArgs e) { tabChangeCheck(); noFileOpenVisible(); updateInterface(); }

        private void gramlexSendCheck()
        {
            //if the file you have opened is either a lexer or a parser, add it - otherwise, don't enable it
            if (CurrentTB != null){
                if (faTabStrip1.SelectedItem.Title.Contains(".l") || faTabStrip1.SelectedItem.Title.Contains(".y")){
                    toolStripLabel14.Enabled = true; toolStripButton37.Enabled = true; toolStripButton38.Enabled = true; toolStripButton39.Enabled = true; }
                else{
                    toolStripLabel14.Enabled = false; toolStripButton37.Enabled = false; toolStripButton38.Enabled = false; toolStripButton39.Enabled = false;} }
        }

        private void toolbarDisableButtons() {
            if (Properties.Settings.Default.fluentStyle){
            //Fluent
            toolStripLabel8.Enabled = false; toolStripLabel7.Enabled = false; toolStripLabel6.Enabled = false; toolStripLabel5.Enabled = false;
            toolStripLabel4.Enabled = false; toolStripLabel9.Enabled = false; toolStripLabel11.Enabled = false; toolStripLabel1.Enabled = false;
            toolStripLabel10.Enabled = false; toolStripLabel14.Enabled = false;}

            if (Properties.Settings.Default.lunaStyle || Properties.Settings.Default.lunaXPStyle || Properties.Settings.Default.flat8Style){
            //Aero
            toolStripButton4.Enabled = false; toolStripButton5.Enabled = false; toolStripButton6.Enabled = false; toolStripButton7.Enabled = false;
            toolStripButton8.Enabled = false; toolStripButton9.Enabled = false; toolStripButton10.Enabled = false; toolStripButton11.Enabled = false;
            toolStripButton12.Enabled = false; toolStripButton38.Enabled = false; }

            if (Properties.Settings.Default.classicStyle){
            //Classic
            toolStripButton16.Enabled = false; toolStripButton17.Enabled = false; toolStripButton18.Enabled = false; toolStripButton19.Enabled = false;
            toolStripButton20.Enabled = false; toolStripButton21.Enabled = false; toolStripButton22.Enabled = false; toolStripButton23.Enabled = false;
            toolStripButton24.Enabled = false; toolStripButton37.Enabled = false;}

            if (Properties.Settings.Default.ClassicNineStyle || Properties.Settings.Default.PantherStyle || Properties.Settings.Default.emacsStyle){
            //OS 9
            toolStripButton28.Enabled = false; toolStripButton29.Enabled = false; toolStripButton30.Enabled = false; toolStripButton31.Enabled = false;
            toolStripButton32.Enabled = false; toolStripButton33.Enabled = false; toolStripButton34.Enabled = false; toolStripButton35.Enabled = false;
            toolStripButton36.Enabled = false; toolStripButton39.Enabled = false;}

            //menu bar
            saveToolStripMenuItem.Enabled = false; saveAsToolStripMenuItem.Enabled = false; sendToToolStripMenuItem.Enabled = false; printToolStripMenuItem.Enabled = false;
            editToolStripMenuItem.Enabled = false; documentMapToolStripMenuItem.Enabled = false; splitContainer2.Panel2Collapsed = true;
            rowsToolStripMenuItem.Enabled = false; dataToolStripMenuItem.Enabled = false; formatToolStripMenuItem.Enabled = false;
            toolStripStatusLabel2.Visible = false; rulerToolStripMenuItem.Enabled = false;  documentMap1.Visible = false;

            //text properties
            trackBar1.Enabled = false; trackBar2.Enabled = false; ruler.Visible = false;
        }

        private void toolbarEnableButtons()
        {
            if (CurrentTB != null)
                    {
                        CurrentTB.Focus();
                        if (Properties.Settings.Default.rulerVisible)
                        { ruler.Visible = true; rulerToolStripMenuItem.Checked = true; }
                        else { ruler.Visible = false; rulerToolStripMenuItem.Checked = false; }

                if (Properties.Settings.Default.fluentStyle){
                        //Fluent
                        toolStripLabel8.Enabled = true; toolStripLabel7.Enabled = true; toolStripLabel6.Enabled = true; toolStripLabel5.Enabled = true; toolStripLabel4.Enabled = true;
                        toolStripLabel9.Enabled = true; toolStripLabel11.Enabled = true; toolStripLabel11.Enabled = true; toolStripLabel1.Enabled = true;
                        toolStripLabel10.Enabled = true;}

                if (Properties.Settings.Default.lunaXPStyle || Properties.Settings.Default.lunaStyle || Properties.Settings.Default.flat8Style){
                        //Aero
                        toolStripButton4.Enabled = true; toolStripButton5.Enabled = true; toolStripButton6.Enabled = true; toolStripButton7.Enabled = true;
                        toolStripButton8.Enabled = true; toolStripButton9.Enabled = true; toolStripButton10.Enabled = true; toolStripButton11.Enabled = true;
                        toolStripButton12.Enabled = true; }

                if (Properties.Settings.Default.classicStyle){
                        //Classic
                        toolStripButton16.Enabled = true; toolStripButton17.Enabled = true; toolStripButton18.Enabled = true; toolStripButton19.Enabled = true;
                        toolStripButton20.Enabled = true; toolStripButton21.Enabled = true; toolStripButton22.Enabled = true; toolStripButton23.Enabled = true;
                        toolStripButton24.Enabled = true;}

                if (Properties.Settings.Default.ClassicNineStyle || Properties.Settings.Default.PantherStyle || Properties.Settings.Default.emacsStyle){
                        //OS 9
                        toolStripButton28.Enabled = true; toolStripButton29.Enabled = true; toolStripButton30.Enabled = true; toolStripButton31.Enabled = true;
                        toolStripButton32.Enabled = true; toolStripButton33.Enabled = true; toolStripButton34.Enabled = true; toolStripButton35.Enabled = true;
                        toolStripButton36.Enabled = true;}

                        //text properties
                        if (CurrentTB != null) trackBar1.Value = CurrentTB.LineInterval; trackBar1.Enabled = true; trackBar2.Enabled = true;

                        //menu bar
                        saveToolStripMenuItem.Enabled = true; saveAsToolStripMenuItem.Enabled = true; sendToToolStripMenuItem.Enabled = true;
                        printToolStripMenuItem.Enabled = true;  rulerToolStripMenuItem.Enabled = true; editToolStripMenuItem.Enabled = true;
                        documentMapToolStripMenuItem.Enabled = true; rowsToolStripMenuItem.Enabled = true; dataToolStripMenuItem.Enabled = true;
                        formatToolStripMenuItem.Enabled = true;
                        if (Properties.Settings.Default.LineColumnView) toolStripStatusLabel2.Visible = true;
                        getCurrentPosition();
                        updateInterface();
        }
        }

        private void textPropertiesCheck()
        {
            trackBar2.Value = (int)CurrentTB.LineNumberStartValue; trackBar2.Maximum = CurrentTB.LinesCount; trackBar3.Value = (int)CurrentTB.TabLength;

            if (CurrentTB.WordWrapMode == FastColoredTextBoxNS.WordWrapMode.CharWrapControlWidth){
                comboBox4.SelectedIndex = 0; label62.Text = "Your document will wrap words based on the single characters of them."; }
            else if (CurrentTB.WordWrapMode == FastColoredTextBoxNS.WordWrapMode.WordWrapControlWidth) {
                comboBox4.SelectedIndex = 1; label62.Text = "Your document will wrap words entirely, making the entire document more readable."; }

            if (CurrentTB.WideCaret){
                checkBox10.Checked = true; label61.Text = "The caret used on your document will be a wider one, more akin to old terminals and editors."; }
            else{
                checkBox10.Checked = false; label61.Text = "The caret used on your document will be the default Windows one."; }
            if (CurrentTB.ReadOnly) checkBox9.Checked = true; else checkBox9.Checked = false;
            if (CurrentTB.ShowScrollBars) checkBox7.Checked = true; else checkBox7.Checked = false;
            if (CurrentTB.AutoIndent) { checkBox4.Checked = true; checkBox6.Enabled = true; }
            else { checkBox4.Checked = false; checkBox6.Enabled = false; }
            if (CurrentTB.AutoIndentChars) checkBox5.Checked = true; else checkBox5.Checked = false;
            textBox8.Text = CurrentTB.AutoIndentCharsPatterns;
            if (CurrentTB.AutoIndentExistingLines) checkBox6.Checked = true; else checkBox6.Checked = false;
            if (CurrentTB.AutoCompleteBrackets) checkBox1.Checked = true; else checkBox1.Checked = false;
            textBox2.Text = CurrentTB.LeftBracket.ToString(); textBox5.Text = CurrentTB.LeftBracket2.ToString(); textBox3.Text = CurrentTB.RightBracket.ToString(); textBox4.Text = CurrentTB.RightBracket2.ToString();
                    
        }
        
        private void tabChangeCheck()
        {
            try
            {
                if (CurrentTB != null)
                {
                    if (Properties.Settings.Default.emacsStyle) { this.Text = faTabStrip1.SelectedItem.Title + " - GNU Emacs at " + System.Windows.Forms.SystemInformation.ComputerName; toolStripStatusLabel4.Text = faTabStrip1.SelectedItem.Title;  }
                    else { this.Text = Properties.Resources.AppName + " " + Properties.Resources.AppEdition + " - " + faTabStrip1.SelectedItem.Title; }
                    ThreadPool.QueueUserWorkItem(new WaitCallback(ReBuildObjectExplorer), CurrentTB.Text);

                    documentMap1.Target = CurrentTB;
                    gramlexSendCheck(); toolStripTextBox4.Text = faTabStrip1.SelectedItem.Title;

                    string text = CurrentTB.Text; CurrentTB.Zoom = Properties.Settings.Default.zoomFile; ruler.Target = CurrentTB;
                    if (ruler.Visible) rulerToolStripMenuItem.Checked = true; else rulerToolStripMenuItem.Checked = false;

                    if (CurrentTB.WordWrap) { wordWrapToolStripMenuItem1.Checked = true; wordWrapToolStripMenuItem.Checked = true; }
                    else { wordWrapToolStripMenuItem.Checked = false; wordWrapToolStripMenuItem1.Checked = false; }
                    CurrentTB.Focus();

                    if (Properties.Settings.Default.syntaxTabChange)
                    {
                        if (faTabStrip1.SelectedItem.Name.Contains(".pl")) Properties.Settings.Default.syntaxChosen = "Prolog";
                        else if (faTabStrip1.SelectedItem.Name.Contains(".lisp")) Properties.Settings.Default.syntaxChosen = "Lisp";
                        else if (faTabStrip1.SelectedItem.Name.Contains(".l")) Properties.Settings.Default.syntaxChosen = "jflex";
                        else if (faTabStrip1.SelectedItem.Name.Contains(".y")) Properties.Settings.Default.syntaxChosen = "yacc";
                        else if (faTabStrip1.SelectedItem.Name.Contains(".txt")) Properties.Settings.Default.syntaxChosen = "None";
                        else if (faTabStrip1.SelectedItem.Name.Contains(".c") || faTabStrip1.SelectedItem.Name.Contains(".h") || faTabStrip1.SelectedItem.Name.Contains(".cs")) Properties.Settings.Default.syntaxChosen = "C";
                        else Properties.Settings.Default.syntaxChosen = "None";
                    }

                    //text properties
                    if (CurrentTB.ShowLineNumbers == true) rowsToolStripMenuItem.Checked = true; else rowsToolStripMenuItem.Checked = false;
                    textPropertiesCheck();
                }
            }
            catch (Exception ex2) { } 
                                if (Properties.Settings.Default.textPropertiesVisible) panel8.Visible = true;
                            if (Properties.Settings.Default.documentMap) { documentMap1.Visible = true; splitContainer2.Panel2Collapsed = false; }
                            if (Properties.Settings.Default.objectBrowser) panel12.Show();                
            if (faTabStrip1.SelectedItem == faTabStripItem2 || faTabStrip1.Items.Count == 0 || faTabStrip1.Name == "Start Page") //start page
                            {
                                toolbarDisableButtons(); panel8.Visible = false; panel12.Hide();
                            if (Properties.Settings.Default.emacsStyle) { this.Text = "GNU Emacs at " + System.Windows.Forms.SystemInformation.ComputerName; toolStripStatusLabel4.Text = "*GNU Emacs*"; }
                            else { this.Text = Properties.Resources.AppName + " " + Properties.Resources.AppEdition + " - Start Page"; btZoom.Enabled = false; toolStripDropDownButton1.Enabled = false; }
                        }
                            else if (faTabStrip1.SelectedItem != faTabStripItem2 || faTabStrip1.Items.Count > 0 || faTabStrip1.Name != "Start Page")  //any other page that's not the start page
                        { toolbarEnableButtons(); btZoom.Enabled = true; toolStripDropDownButton1.Enabled = true; }
        
                        }


        public void clickToolbar(ToolStripLabel label, int leftog, int rightog, int left, int right){
            label.Padding = new Padding(leftog + left, label.Padding.Top, rightog + right, label.Padding.Bottom);
            label.Font = new Font(label.Font.FontFamily, label.Font.Size - 3, label.Font.Style); }

        public void gramlexSendFiles()
        {
            if (faTabStrip1.SelectedItem.Title.Contains(".l")) {
                Properties.Settings.Default.gramlexLexical = faTabStrip1.SelectedItem.Title; panel10.Visible = true; timer.Start(); }
            else if (faTabStrip1.SelectedItem.Title.Contains(".y")) {
                Properties.Settings.Default.gramlexParser = faTabStrip1.SelectedItem.Title; panel10.Visible = true; timer.Start(); } }
                
        public void clickToolbarUndo(ToolStripLabel label, int leftog, int rightog) {
            label.Padding = new Padding(leftog, label.Padding.Top, rightog, label.Padding.Bottom);
            float fontSize = label.Font.Size;
            if (fontSize + 3 > 14) fontSize = 14; else fontSize = fontSize + 3;
            label.Font = new Font(label.Font.FontFamily, fontSize, label.Font.Style); }

        public void checkPadding() {
            leftTab = toolStripLabel12.Padding.Left; rightTab = toolStripLabel12.Padding.Right;

            leftOpen = toolStripLabel3.Padding.Left; rightOpen = toolStripLabel3.Padding.Right;

            leftSave = toolStripLabel8.Padding.Left; rightSave = toolStripLabel8.Padding.Right;

            leftCut = toolStripLabel7.Padding.Left; rightCut = toolStripLabel7.Padding.Right;

            leftCopy = toolStripLabel6.Padding.Left; rightCopy = toolStripLabel6.Padding.Right;

            leftPaste = toolStripLabel5.Padding.Left; rightPaste = toolStripLabel5.Padding.Right;

            leftUndo = toolStripLabel4.Padding.Left; rightUndo = toolStripLabel4.Padding.Right;

            leftFont = toolStripLabel11.Padding.Left;  rightFont = toolStripLabel11.Padding.Right;

            leftNav = toolStripLabel1.Padding.Left; rightNav = toolStripLabel1.Padding.Right;

            leftNavNav = toolStripLabel10.Padding.Left; rightNavNav = toolStripLabel10.Padding.Right; }

        private void toolStripLabel12_MouseDown(object sender, MouseEventArgs e)
        {
            clickToolbar(this.toolStripLabel12, leftNew, rightNew, Properties.Settings.Default.leftPaddingToolbar, Properties.Settings.Default.rightPaddingToolbar);
            tabClicked = true;
        }

        private void toolStripLabel12_MouseUp(object sender, MouseEventArgs e)
        {
            clickToolbarUndo(this.toolStripLabel12, leftNew, rightNew);
            tabClicked = false;
        }

        private void toolStripLabel3_MouseDown(object sender, MouseEventArgs e)
        {
            clickToolbar(this.toolStripLabel3, leftNew, rightNew, Properties.Settings.Default.leftPaddingToolbar, Properties.Settings.Default.rightPaddingToolbar);
            openClicked = true;
        }

        private void toolStripLabel3_MouseUp(object sender, MouseEventArgs e)
        {
            clickToolbarUndo(this.toolStripLabel3, leftNew, rightNew);
            openClicked = false;
        }

        private void toolStripLabel8_MouseDown(object sender, MouseEventArgs e)
        {
            clickToolbar(this.toolStripLabel8, leftNew, rightNew, Properties.Settings.Default.leftPaddingToolbar, Properties.Settings.Default.rightPaddingToolbar);
            saveClicked = true;
        }

        private void toolStripLabel8_MouseUp(object sender, MouseEventArgs e)
        {
            clickToolbarUndo(this.toolStripLabel8, leftNew, rightNew);
            saveClicked = false;
        }

        private void toolStripLabel7_MouseUp(object sender, MouseEventArgs e)
        {
            clickToolbarUndo(this.toolStripLabel7, leftNew, rightNew);
            cutClicked = false;
        }

        private void toolStripLabel7_MouseDown(object sender, MouseEventArgs e)
        {
            clickToolbar(this.toolStripLabel7, leftNew, rightNew, Properties.Settings.Default.leftPaddingToolbar, Properties.Settings.Default.rightPaddingToolbar);
            cutClicked = true;
        }

        private void toolStripLabel6_MouseDown(object sender, MouseEventArgs e)
        {
            clickToolbar(this.toolStripLabel6, leftNew, rightNew, Properties.Settings.Default.leftPaddingToolbar, Properties.Settings.Default.rightPaddingToolbar);
            copyClicked = true;
        }

        private void toolStripLabel6_MouseUp(object sender, MouseEventArgs e)
        {
            clickToolbarUndo(this.toolStripLabel6, leftNew, rightNew);
            copyClicked = false;
        }

        private void toolStripLabel5_MouseUp(object sender, MouseEventArgs e)
        {
            clickToolbarUndo(this.toolStripLabel5, leftNew, rightNew);
            pasteClicked = false;
        }

        private void toolStripLabel5_MouseDown(object sender, MouseEventArgs e)
        {
            clickToolbar(this.toolStripLabel5, leftNew, rightNew, Properties.Settings.Default.leftPaddingToolbar, Properties.Settings.Default.rightPaddingToolbar);
            pasteClicked = true;
        }

        private void toolStripLabel4_MouseDown(object sender, MouseEventArgs e)
        {
            clickToolbar(this.toolStripLabel4, leftNew, rightNew, Properties.Settings.Default.leftPaddingToolbar, Properties.Settings.Default.rightPaddingToolbar);
            undoClicked = true;
        }

        private void toolStripLabel4_MouseUp(object sender, MouseEventArgs e)
        {
            clickToolbarUndo(this.toolStripLabel4, leftNew, rightNew);
            undoClicked = false;
        }

        private void toolStripLabel9_MouseUp(object sender, MouseEventArgs e)
        {
            clickToolbarUndo(this.toolStripLabel9, leftNew, rightNew);
            redoClicked = false;
        }

        private void toolStripLabel9_MouseDown(object sender, MouseEventArgs e)
        {
            clickToolbar(this.toolStripLabel9, leftNew, rightNew, Properties.Settings.Default.leftPaddingToolbar, Properties.Settings.Default.rightPaddingToolbar);
            redoClicked = true;
        }

        private void toolStripLabel11_MouseDown(object sender, MouseEventArgs e)
        {
            clickToolbar(this.toolStripLabel11, leftNew, rightNew, Properties.Settings.Default.leftPaddingToolbar, Properties.Settings.Default.rightPaddingToolbar);
            fontClicked = true;
        }

        private void toolStripLabel11_MouseUp(object sender, MouseEventArgs e)
        {
            clickToolbarUndo(this.toolStripLabel11, leftNew, rightNew);
            fontClicked = false;
        }

        private void toolStripLabel1_MouseUp(object sender, MouseEventArgs e)
        {
            clickToolbarUndo(this.toolStripLabel1, leftNew, rightNew);
            backClicked = false;
        }

        private void toolStripLabel1_MouseDown(object sender, MouseEventArgs e)
        {
            clickToolbar(this.toolStripLabel1, leftNew, rightNew, Properties.Settings.Default.leftPaddingToolbar, Properties.Settings.Default.rightPaddingToolbar);
            backClicked = true;
        }
        private bool newClicked; private bool tabClicked; private bool openClicked; private bool saveClicked; private bool cutClicked; private bool copyClicked; private bool pasteClicked; private bool undoClicked; private bool redoClicked; private bool fontClicked; private bool backClicked; private bool gramlexClicked; private bool nextClicked;
        private void toolStripLabel10_MouseDown(object sender, MouseEventArgs e)
        {
            clickToolbar(this.toolStripLabel10, leftNew, rightNew, Properties.Settings.Default.leftPaddingToolbar, Properties.Settings.Default.rightPaddingToolbar);
            nextClicked = true;
        }

        private void toolStripLabel10_MouseUp(object sender, MouseEventArgs e)
        {
            clickToolbarUndo(this.toolStripLabel10, leftNew, rightNew);
            nextClicked = false;
        }

        private void faTabStrip1_TabStripItemClosing(FarsiLibrary.Win.TabStripItemClosingEventArgs e)
        {
            noFileOpenVisible(); tabChangeCheck();
            try {
                if ((e.Item.Controls[0] as FastColoredTextBoxNS.FastColoredTextBox).IsChanged) {
                    switch (MessageBox.Show("Do you want save " + e.Item.Title + " ?", "Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information))
                    {
                        case System.Windows.Forms.DialogResult.Yes:
                            if (!Save(e.Item)) e.Cancel = true;
                            break;
                        case DialogResult.Cancel:
                            e.Cancel = true;
                            break; } }

                if (faTabStrip1.SelectedItem == faTabStripItem2) { splitContainer2.Panel2Collapsed = true; } }
            catch (NullReferenceException) { }  }

        private bool Save(FarsiLibrary.Win.FATabStripItem tab)
        {
            FastColoredTextBoxNS.FastColoredTextBox tb = (tab.Controls[0] as FastColoredTextBoxNS.FastColoredTextBox);
            if (tab.Tag == null) {
                 sfdMain.Filter = "Prolog source files (*.pl, *.pro)|*.pl;*.pro|Prolog consultable files (*.consult)|*.consult|LISP source files (*.lisp, *.l, *.cl, *.fasl)|*.lisp;*.l;*.cl;*.fasl|yacc files (*.y)|*.y|JFlex Source files (*.l)|*.l|XML files (*.xml)|*.xml|XAML files (*.xaml)|*.xaml|XML Document Type Definition files (*.dtd)|*.dtd|XML Schema Definition files (*.xsd)|*.xsd|XML Extensible Stylesheet Language files (*.xsl)|*.xsl|C# source files (*.cs)|*.cs|Visual Basic source files (*.vb)|*.vb|Visual Basic .NET files (*.vbnet)|*.vbnet|HTML files (*.html, *.htm)|*.html;*.htm|SQL files (*.sql)|*.sql|PHP files (*.php)|*.php|Javascript files (*.js)|*.js|Lua files (*.lua)|*.lua|Rich Text Document files (*.rtf)|*.rtf|Plain Text files (*.txt)|*.txt|All files (*.*)|*.*"; //set extensions for dialog
                if (Properties.Settings.Default.syntaxFileExtension) {
                    switch (Properties.Settings.Default.syntaxChosen) {
                        case "Prolog":
                            sfdMain.FilterIndex = 1;
                            break;

                        case "Lisp":
                            sfdMain.FilterIndex = 3;
                            break;

                        case "yacc":
                            sfdMain.FilterIndex = 4;
                            break;

                        case "jflex":
                            sfdMain.FilterIndex = 5;
                            break;

                        case "None":
                            sfdMain.FilterIndex = 20;
                            break;

                        case "C":
                            sfdMain.FilterIndex = 10;
                            break;

                    } } else if (!Properties.Settings.Default.syntaxFileExtension) { sfdMain.FilterIndex = Properties.Settings.Default.syntaxFileExtensionIndex; }
                sfdMain.RestoreDirectory = true; //restore the directory to the precedent one you were in
                MDIParent1.ActiveForm.Text = MDIParent1.ActiveForm.Text.Replace("*", ""); //remove the asterisk from the titlebar to simbolize you have saved the document
                if (sfdMain.ShowDialog() != System.Windows.Forms.DialogResult.OK) return false;
                tab.Title = Path.GetFileName(sfdMain.FileName);       tab.Tag = sfdMain.FileName; }
            try { File.WriteAllText(tab.Tag as string, tb.Text); tb.IsChanged = false; }
            catch (Exception ex) {
                if (MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry) return Save(tab);
                else return false; }
            tb.Invalidate(); return true; }

        private void noFileOpenVisible() {
            if (faTabStrip1.Items.Count == 0) noOpenedFileGroup.Visible = true; else noOpenedFileGroup.Visible = false;

            //Option for when you have "Close Logix when I close all tabs"
            if ((Properties.Settings.Default.logixTabsClose && Properties.Settings.Default.startPageStartup == true && faTabStrip1.Items.Count == 0) || (Properties.Settings.Default.logixTabsClose && createdATab && faTabStrip1.Items.Count == 0))
                Application.Exit(); }

        private void toolStripLabel12_MouseLeave(object sender, EventArgs e)
        {
            if (tabClicked)
            {
                clickToolbarUndo(this.toolStripLabel12, leftNew, rightNew);
                tabClicked = false;
            }
        }

        private void toolStripLabel3_MouseLeave(object sender, EventArgs e)
        {
            if (openClicked)
            {
                clickToolbarUndo(this.toolStripLabel3, leftNew, rightNew);
                openClicked = false;
            }
        }

        private void toolStripLabel8_MouseLeave(object sender, EventArgs e)
        {
            if (saveClicked)
            {
                clickToolbarUndo(this.toolStripLabel8, leftNew, rightNew);
                saveClicked = false;
            }
        }

        private void toolStripLabel7_MouseLeave(object sender, EventArgs e)
        {
            if (cutClicked)
            {
                clickToolbarUndo(this.toolStripLabel7, leftNew, rightNew);
                cutClicked = false;
            }
        }

        private void toolStripLabel6_MouseLeave(object sender, EventArgs e)
        {
            if (copyClicked)
            {
                clickToolbarUndo(this.toolStripLabel6, leftNew, rightNew);
                copyClicked = false;
            }
        }

        private void toolStripLabel5_MouseLeave(object sender, EventArgs e)
        {
            if (pasteClicked)
            {
                clickToolbarUndo(this.toolStripLabel5, leftNew, rightNew);
                pasteClicked = false;
            }
        }

        private void toolStripLabel4_MouseLeave(object sender, EventArgs e)
        {
            if (undoClicked)
            {
                clickToolbarUndo(this.toolStripLabel4, leftNew, rightNew);
                undoClicked = false;
            }
        }

        private void toolStripLabel9_MouseLeave(object sender, EventArgs e)
        {
            if (redoClicked)
            {
                clickToolbarUndo(this.toolStripLabel9, leftNew, rightNew);
                redoClicked = false;
            }
        }

        private void toolStripLabel11_MouseLeave(object sender, EventArgs e)
        {
            if (fontClicked)
            {
                clickToolbarUndo(this.toolStripLabel11, leftNew, rightNew);
                fontClicked = false;
            }
        }

        private void toolStripLabel1_MouseLeave(object sender, EventArgs e)
        {
            if (backClicked)
            {
                clickToolbarUndo(this.toolStripLabel1, leftNew, rightNew);
                backClicked = false;
            }
        }

        private void toolStripLabel14_MouseLeave(object sender, EventArgs e)
        {
            if (gramlexClicked)
            {
                clickToolbarUndo(this.toolStripLabel14, leftNew, rightNew);
                gramlexClicked = false;
            }
        }

        private void toolStripLabel10_MouseLeave(object sender, EventArgs e)
        {
            if (nextClicked)
            {
                clickToolbarUndo(this.toolStripLabel10, leftNew, rightNew);
                nextClicked = false;
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                FindNext(comboBox1.Text);
        }

        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                ReplaceFirst(comboBox1.Text, comboBox3.Text);
        }

        //fluent style coloring
        public void colorFluentIcons()
        {
            toolStripLabel12.ForeColor = Properties.Settings.Default.newFluentColor;
            toolStripLabel3.ForeColor = Properties.Settings.Default.openFluentColor;
            toolStripLabel8.ForeColor = Properties.Settings.Default.saveFluentColor;
            toolStripLabel7.ForeColor = Properties.Settings.Default.cutFluentColor;
            toolStripLabel6.ForeColor = Properties.Settings.Default.copyFluentColor;
            toolStripLabel5.ForeColor = Properties.Settings.Default.pasteFluentColor;
            toolStripLabel4.ForeColor = Properties.Settings.Default.undoFluentColor;
            toolStripLabel9.ForeColor = Properties.Settings.Default.redoFluentColor;
            toolStripLabel11.ForeColor = Properties.Settings.Default.fontFluentColor;
            toolStripLabel1.ForeColor = Properties.Settings.Default.backFluentColor;
            toolStripLabel10.ForeColor = Properties.Settings.Default.nextFluentColor;
            toolStripLabel14.ForeColor = Properties.Settings.Default.gramlexFluentColor;
        }

        public void uncolorFluentIcons()
        {
            toolStripLabel12.ForeColor = SystemColors.ControlText;
                toolStripLabel3.ForeColor =SystemColors.ControlText;
                toolStripLabel8.ForeColor =SystemColors.ControlText;
                toolStripLabel7.ForeColor =SystemColors.ControlText;
                toolStripLabel6.ForeColor =SystemColors.ControlText;
                toolStripLabel5.ForeColor =SystemColors.ControlText;
                toolStripLabel4.ForeColor =SystemColors.ControlText;
                toolStripLabel9.ForeColor =SystemColors.ControlText;
                toolStripLabel11.ForeColor =SystemColors.ControlText;
                toolStripLabel1.ForeColor =SystemColors.ControlText;
                toolStripLabel10.ForeColor =SystemColors.ControlText;
                toolStripLabel14.ForeColor = SystemColors.ControlText;
        }
        //2000: coloured classic icons; B: monochromatic classic icons
        private void toolStripButton13_MouseEnter(object sender, EventArgs e) { toolStripButton13.Image = Properties.Resources.new2000; }

        private void toolStripButton13_MouseLeave(object sender, EventArgs e) { toolStripButton13.Image = Properties.Resources.newB; }
 
        private void toolStripButton15_MouseEnter(object sender, EventArgs e) { toolStripButton15.Image = Properties.Resources.open2000; }

        private void toolStripButton15_MouseLeave(object sender, EventArgs e) { toolStripButton15.Image = Properties.Resources.openB; }

        private void toolStripButton16_MouseEnter(object sender, EventArgs e) { toolStripButton16.Image = Properties.Resources.save2000; }

        private void toolStripButton16_MouseLeave(object sender, EventArgs e) { toolStripButton16.Image = Properties.Resources.saveB; }

        private void toolStripButton17_MouseEnter(object sender, EventArgs e) { toolStripButton17.Image = Properties.Resources.cut2000; }

        private void toolStripButton17_MouseLeave(object sender, EventArgs e) { toolStripButton17.Image = Properties.Resources.cutB; }

        private void toolStripButton18_MouseEnter(object sender, EventArgs e) { toolStripButton18.Image = Properties.Resources.copy2000; }

        private void toolStripButton19_MouseEnter(object sender, EventArgs e) { toolStripButton19.Image = Properties.Resources.paste2000; }

        private void toolStripButton18_MouseLeave(object sender, EventArgs e) { toolStripButton18.Image = Properties.Resources.copyB; }

        private void toolStripButton19_MouseLeave(object sender, EventArgs e) { toolStripButton19.Image = Properties.Resources.pasteB; }

        private void toolStripButton20_MouseEnter(object sender, EventArgs e) { toolStripButton20.Image = Properties.Resources.undo2000; }

        private void toolStripButton20_MouseLeave(object sender, EventArgs e) { toolStripButton20.Image = Properties.Resources.undoB; }

        private void toolStripButton21_MouseEnter(object sender, EventArgs e) { toolStripButton21.Image = Properties.Resources.redo2000; }

        private void toolStripButton21_MouseLeave(object sender, EventArgs e) { toolStripButton21.Image = Properties.Resources.redoB; }

        private void toolStripButton22_MouseEnter(object sender, EventArgs e) { toolStripButton22.Image = Properties.Resources.Font2000; }

        private void toolStripButton22_MouseLeave(object sender, EventArgs e) { toolStripButton22.Image = Properties.Resources.fontB; }

        private void toolStripButton23_MouseEnter(object sender, EventArgs e)  { toolStripButton23.Image = Properties.Resources.back2000; }

        private void toolStripButton23_MouseLeave(object sender, EventArgs e) { toolStripButton23.Image = Properties.Resources.bacjB; }

        private void toolStripButton24_MouseEnter(object sender, EventArgs e) { toolStripButton24.Image = Properties.Resources.next2000; }

        private void toolStripButton24_MouseLeave(object sender, EventArgs e) { toolStripButton24.Image = Properties.Resources.nextB; }

        private void toolStripButton37_MouseEnter(object sender, EventArgs e) { toolStripButton37.Image = Properties.Resources.gramlex2000; }

        private void toolStripButton37_MouseLeave(object sender, EventArgs e) { toolStripButton37.Image = Properties.Resources.gramlexB; }

        int currentLine; int currentColumn;

        private void getCurrentPosition() {
            if (CurrentTB != null) {
                currentLine = CurrentTB.Selection.Start.iLine + 1; currentColumn = CurrentTB.Selection.Start.iChar + 1;
                if (currentColumn > Properties.Settings.Default.columnLineLimit && Properties.Settings.Default.antoniottiStandard && !Properties.Settings.Default.antoniottiCrazy) {
                    toolStripStatusLabel.ForeColor = Color.Red; toolStripStatusLabel.Font = new Font(toolStripStatusLabel.Font, FontStyle.Bold);
                    toolStripStatusLabel.Text = Properties.Settings.Default.antoniottiStandardText; }
                else if (currentColumn > Properties.Settings.Default.columnLineLimit && Properties.Settings.Default.antoniottiCrazy && !Properties.Settings.Default.antoniottiStandard) {
                    toolStripStatusLabel.ForeColor = Color.Red; toolStripStatusLabel.Font = new Font(toolStripStatusLabel.Font, FontStyle.Bold);
                    antoniotti80Panel.Visible = true; label27.Text = Properties.Settings.Default.antoniottiCrazyTitle; label28.Text = Properties.Settings.Default.antoniottiCrazyTextDuo;
                    toolStripStatusLabel.Text = Properties.Settings.Default.antoniottiCrazyText; }
                else if (currentColumn <= Properties.Settings.Default.columnLineLimit || !Properties.Settings.Default.antoniottiStandard && !Properties.Settings.Default.antoniottiCrazy) {
                    toolStripStatusLabel.ForeColor = SystemColors.ControlText; toolStripStatusLabel.Font = new Font(toolStripStatusLabel.Font, FontStyle.Regular);
                    antoniotti80Panel.Visible = false; toolStripStatusLabel.Text = "Ready"; } } }

        private void CurrentTB_SelectionChanged(object sender, EventArgs e)
        { if (CurrentTB != null) { getCurrentPosition(); updateInterface(); toolStripStatusLabel2.Text = "Line " + currentLine + ", column " + currentColumn; } }

        private void lineAndColumnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lineAndColumnToolStripMenuItem.Checked) { toolStripStatusLabel2.Visible = false; lineAndColumnToolStripMenuItem.Checked = false;
                Properties.Settings.Default.LineColumnView = false; } else {
                if (faTabStrip1.SelectedItem != faTabStripItem2) {
                    toolStripStatusLabel2.Visible = true; lineAndColumnToolStripMenuItem.Checked = true; Properties.Settings.Default.LineColumnView = true;
                } } Properties.Settings.Default.Save(); }

        private void addANewTabToolStripMenuItem_Click(object sender, EventArgs e) {
            CreateTab(null);
            switch (Properties.Settings.Default.syntaxTabNew) {
                case "Prolog":
                    Properties.Settings.Default.syntaxChosen = "Prolog";
                    break;
                case "Lisp":
                    Properties.Settings.Default.syntaxChosen = "Lisp";
                    break;
                case "jflex":
                    Properties.Settings.Default.syntaxChosen = "jflex";
                    break;
                case "yacc":
                    Properties.Settings.Default.syntaxChosen = "yacc";
                    break;
                case "None":
                    Properties.Settings.Default.syntaxChosen = "None";
                    break;
                case "C":
                    Properties.Settings.Default.syntaxChosen = "C";
                    break;
            }
        }

        private void closeCurrentTabToolStripMenuItem_Click(object sender, EventArgs e) {
            try{
                FarsiLibrary.Win.TabStripItemClosingEventArgs args = new FarsiLibrary.Win.TabStripItemClosingEventArgs(faTabStrip1.SelectedItem);
                if (CurrentTB.IsChanged){
                    switch (MessageBox.Show("Do you want save " + faTabStrip1.SelectedItem.Title + " ?", "Save", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information)) {
                        case System.Windows.Forms.DialogResult.Yes:
                            if (!Save(faTabStrip1.SelectedItem)) toolStripStatusLabel.Text = "Saved";
                            break;} } faTabStrip1.RemoveTab(faTabStrip1.SelectedItem); } catch (NullReferenceException) { }
        }

        private void startPageToolStripMenuItem_Click(object sender, EventArgs e)
        { faTabStrip1.AddTab(faTabStripItem2); }

        private void prologToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (prologToolStripMenuItem.Checked) {
                prologToolStripMenuItem.Checked = false; noneToolStripMenuItem.Checked = true; lispToolStripMenuItem.Checked = false; cCToolStripMenuItem.Checked = false;
                yaccJToolStripMenuItem.Checked = false; jFlexToolStripMenuItem.Checked = false; Properties.Settings.Default.syntaxChosen = "None";
                CurrentTB.Language = FastColoredTextBoxNS.Language.Custom; CurrentTB.DescriptionFile = null; CurrentTB.Text = CurrentTB.Text + ""; }
            else {
                prologToolStripMenuItem.Checked = true; noneToolStripMenuItem.Checked = false; lispToolStripMenuItem.Checked = false; yaccJToolStripMenuItem.Checked = false; cCToolStripMenuItem.Checked = false; jFlexToolStripMenuItem.Checked = false;
                if (Properties.Settings.Default.syntaxChosen == "Lisp" || Properties.Settings.Default.syntaxChosen == "jflex" || Properties.Settings.Default.syntaxChosen == "yacc")
                { Properties.Settings.Default.syntaxChosen = "None"; } Properties.Settings.Default.syntaxChosen = "Prolog"; CurrentTB.Text = CurrentTB.Text + ""; }
            Properties.Settings.Default.Save(); }

        public void cleanStyles(FastColoredTextBoxNS.TextChangedEventArgs e) { e.ChangedRange.ClearStyle(CurrentTB.Styles); }

        private void noneToolStripMenuItem_Click(object sender, EventArgs e) {
            if (!noneToolStripMenuItem.Checked) {
                Properties.Settings.Default.syntaxChosen = "None"; CurrentTB.Text = CurrentTB.Text + ""; prologToolStripMenuItem.Checked = false;
                noneToolStripMenuItem.Checked = true; lispToolStripMenuItem.Checked = false; yaccJToolStripMenuItem.Checked = false;
                jFlexToolStripMenuItem.Checked = false; cToolStripMenuItem.Checked = false;
            } Properties.Settings.Default.Save();
        }

        private void applyLispSyntax(FastColoredTextBoxNS.TextChangedEventArgs e) { try {
                CurrentTB.Range.tb.ClearStylesBuffer();
                e.ChangedRange.SetStyle(lispComment, @";.*");  // Stile per i commenti
                e.ChangedRange.SetStyle(lispKeyword, @"\b(if|defun|setq)\b");  // Stile per le parole chiave di LISP
                e.ChangedRange.SetStyle(lispOperator, @"[\+\-\*\/]");  // Stile per gli operatori
                e.ChangedRange.SetStyle(lispSpecialChar, @"[\$\%\!]");  // Stile per i caratteri speciali
                e.ChangedRange.SetStyle(lispString, "\"[^\"]*\""); e.ChangedRange.SetStyle(lispNumber, @"\b\d+(\.\d+)?\b"); e.ChangedRange.SetStyle(lispVariable, @"\b[a-zA-Z][-a-zA-Z0-9]*\b");  // Stile per le variabili
                e.ChangedRange.SetStyle(lispBrackets, @"[\(\)]");  // Stile per le parentesi tonde
                CurrentTB.Refresh(); } catch (Exception) {
                toolStripStatusLabel.Text = "Exceeded LISP!"; } }

        private void applyYaccSyntax(FastColoredTextBoxNS.TextChangedEventArgs e) { try {
                CurrentTB.Range.tb.ClearStylesBuffer();
                e.ChangedRange.SetStyle(yaccSection, @"%%\n*");  // Stile per i commenti
                e.ChangedRange.SetStyle(yaccBrackets, @"\((?:(?![{}[\]])[^\n])*\)|\[(?:(?![{}[\]])[^\n])*\]"); //stile per le parentesi
                e.ChangedRange.SetStyle(yaccNumber, @"[0-9]+"); e.ChangedRange.SetStyle(yaccToken, @"(?<!(?s:\x22[^\x22]*?))\b[A-Z]+\b"); e.ChangedRange.SetStyle(yaccPerc, @"%");
                e.ChangedRange.SetStyle(yaccInclusive, @"\<[^\<\>]*\>"); //inclusive ed exclusive
                e.ChangedRange.SetStyle(yaccOperator, @"([-\[\]{}()*+_?.,\\\/^$|#])");  // Stile per gli operatori, (\\a)(\\b)(\\d)(\\D)(\\s)(\\S)(\\w)(\\W)(\\b)(\\B)
                e.ChangedRange.SetStyle(jflexJava, @"(?<!(?s:\x22[^\x22]*?))\b(public|private|void|int|StringBuffer|static|double|final|protected|class|input|yyerror|yylval|Parser|Val|new|System|yyparse|return|if|else|;|null|String|class|unicode|Symbol|return|yyline|yycolumn|Object|Identifier|state|toString|throw|Error|yytext|out|print|println|printf|abstract|add|alias|as|ascending|async|await|base|bool|break|by|byte|case|catch|char|checked|class|const|continue|decimal|default|delegate|descending|do|double|dynamic|else|enum|equals|event|explicit|extern|false|finally|fixed|float|for|foreach|from|get|global|goto|group|if|implicit|in|int|interface|internal|into|is|join|let|lock|long|nameof|namespace|new|null|object|on|operator|orderby|out|override|params|partial|private|protected|public|readonly|ref|remove|return|sbyte|sealed|select|set|short|sizeof|stackalloc|static|static|string|struct|switch|this|throw|true|try|typeof|uint|ulong|unchecked|unsafe|ushort|using|using|value|var|virtual|void|volatile|when|where|while|yield|import|java|IO|IOException|out|println|print|Parser|new|InputStreamReader|Yylex|String)\b|#region\b|#endregion\b");
                CurrentTB.Refresh(); } catch (Exception) { toolStripStatusLabel.Text = "Exceeded yacc!"; } }

        private void applyJFlexSyntax(FastColoredTextBoxNS.TextChangedEventArgs e) { try {
                CurrentTB.Range.tb.ClearStylesBuffer();  e.ChangedRange.SetStyle(jflexSection, @"%%\n*");  // Stile per i commenti
                e.ChangedRange.SetStyle(jflexComment, @"//.*$", RegexOptions.Multiline); e.ChangedRange.SetStyle(jflexComment, @"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline);
                e.ChangedRange.SetStyle(jflexComment, @"(/\*.*?\*/)|(.*\*/)",
                                            RegexOptions.Singleline | RegexOptions.RightToLeft);
                e.ChangedRange.SetStyle(jflexBrackets, @"\((?:(?![{}[\]])[^\n])*\)|\[(?:(?![{}[\]])[^\n])*\]"); //stile per le parentesi

                e.ChangedRange.SetStyle(jflexInclusive, @"\<[^\<\>]*\>"); //inclusive ed exclusive
                e.ChangedRange.SetStyle(jflexJava, @"(?<!(?s:\x22[^\x22]*?))\b(public|private|void|int|StringBuffer|static|double|final|protected|class|input|yyerror|yylval|Parser|Val|new|System|yyparse|return|if|else|;|null|String|class|unicode|Symbol|return|yyline|yycolumn|Object|Identifier|state|toString|throw|Error|yytext|out|print|println|printf|abstract|add|alias|as|ascending|async|await|base|bool|break|by|byte|case|catch|char|checked|class|const|continue|decimal|default|delegate|descending|do|double|dynamic|else|enum|equals|event|explicit|extern|false|finally|fixed|float|for|foreach|from|get|global|goto|group|if|implicit|in|int|interface|internal|into|is|join|let|lock|long|nameof|namespace|new|null|object|on|operator|orderby|out|override|params|partial|private|protected|public|readonly|ref|remove|return|sbyte|sealed|select|set|short|sizeof|stackalloc|static|static|string|struct|switch|this|throw|true|try|typeof|uint|ulong|unchecked|unsafe|ushort|using|using|value|var|virtual|void|volatile|when|where|while|yield)\b|#region\b|#endregion\b");
                e.ChangedRange.SetStyle(jflexOperator, @"([-\[\]{}()*+?.,\\\/^$|#])");  // Stile per gli operatori, (\\a)(\\b)(\\d)(\\D)(\\s)(\\S)(\\w)(\\W)(\\b)(\\B)
                Regex jflexStringRegex = new Regex(
                    @"
                            # Character definitions:
                            '
                            (?> # disable backtracking
                              (?:
                                \\[^\r\n]|    # escaped meta char
                                [^'\r\n]      # any character except '
                              )*
                            )
                            '?
                            |
                            # Normal string & verbatim strings definitions:
                            (?<verbatimIdentifier>@)?         # this group matches if it is an verbatim string
                            ""
                            (?> # disable backtracking
                              (?:
                                # match and consume an escaped character including escaped double quote ("") char
                                (?(verbatimIdentifier)        # if it is a verbatim string ...
                                  """"|                         #   then: only match an escaped double quote ("") char
                                  \\.                         #   else: match an escaped sequence
                                )
                                | # OR
            
                                # match any char except double quote char ("")
                                [^""]
                              )*
                            )
                            ""
                        ",
                    RegexOptions.ExplicitCapture | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace
                    ); //thanks to rittergig for this regex
                e.ChangedRange.SetStyle(jflexString, jflexStringRegex);
                CurrentTB.Refresh(); } catch (Exception) { toolStripStatusLabel.Text = "Exceeded jflex!"; } }
        

        private void applyCSyntax(FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            CurrentTB.Range.tb.ClearStylesBuffer();
            Regex CSharpStringRegex =
                new Regex(
                    @"
                            # Character definitions:
                            '
                            (?> # disable backtracking
                              (?:
                                \\[^\r\n]|    # escaped meta char
                                [^'\r\n]      # any character except '
                              )*
                            )
                            '?
                            |
                            # Normal string & verbatim strings definitions:
                            (?<verbatimIdentifier>@)?         # this group matches if it is an verbatim string
                            ""
                            (?> # disable backtracking
                              (?:
                                # match and consume an escaped character including escaped double quote ("") char
                                (?(verbatimIdentifier)        # if it is a verbatim string ...
                                  """"|                         #   then: only match an escaped double quote ("") char
                                  \\.                         #   else: match an escaped sequence
                                )
                                | # OR
            
                                # match any char except double quote char ("")
                                [^""]
                              )*
                            )
                            ""
                        ",
                    RegexOptions.ExplicitCapture | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace
                    ); //thanks to rittergig for this regex

           Regex  CSharpCommentRegex1 = new Regex(@"//.*$", RegexOptions.Multiline);
            Regex CSharpCommentRegex2 = new Regex(@"(/\*.*?\*/)|(/\*.*)", RegexOptions.Multiline);
            Regex CSharpCommentRegex3 = new Regex(@"(/\*.*?\*/)|(.*\*/)",
                                            RegexOptions.Singleline | RegexOptions.RightToLeft);
            Regex CSharpNumberRegex = new Regex(@"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
            Regex CSharpAttributeRegex = new Regex(@"^\s*(?<range>\[.+?\])\s*$", RegexOptions.Multiline);
            Regex CSharpClassNameRegex = new Regex(@"\b(\#|class|struct|enum|interface)\s+(?<range>\w+?)\b");
            Regex CSharpBracketsRegex = new Regex(@"\#include\s+\<[^\<\>]*\>|\[(?:(?![{}[\]])[^\n])*\]|\#include\s+\x22[^\<\>]*\x22");
            Regex CSharpOperatorRegex = new Regex(@"(?<!\#include)\<|(?<!\#include)\>(?!\w)|\=|\+|\-");

           Regex CSharpKeywordRegex =
                new Regex(
                    @"\b(\#include|abstract|add|alias|as|ascending|async|await|base|bool|break|by|byte|case|catch|char|checked|class|const|continue|decimal|default|delegate|descending|do|double|dynamic|else|enum|equals|event|explicit|extern|false|finally|fixed|float|for|foreach|from|get|global|goto|group|if|implicit|in|int|interface|internal|into|is|join|let|lock|long|nameof|namespace|new|null|object|on|operator|orderby|out|override|params|partial|private|protected|public|readonly|ref|remove|return|sbyte|sealed|select|set|short|sizeof|stackalloc|static|static|string|struct|switch|this|throw|true|try|typeof|uint|ulong|unchecked|unsafe|ushort|using|using|value|var|virtual|void|volatile|when|where|while|yield)\b|#region\b|#endregion\b"
                    );

           CurrentTB.Range.tb.AutoIndentCharsPatterns
              = @"
^\s*[\w\.]+(\s\w+)?\s*(?<range>=)\s*(?<range>[^;=]+);
^\s*(case|default)\s*[^:]*(?<range>:)\s*(?<range>[^;]+);
";

   
           //comment highlighting
           e.ChangedRange.SetStyle(cComment, CSharpCommentRegex1);
           e.ChangedRange.SetStyle(cComment, CSharpCommentRegex2);
           e.ChangedRange.SetStyle(cComment, CSharpCommentRegex3);
        
         
            //brackets
           e.ChangedRange.SetStyle(cBrackets, CSharpBracketsRegex);
           //operators
           e.ChangedRange.SetStyle(cString, CSharpStringRegex);
           e.ChangedRange.SetStyle(cOperator, CSharpOperatorRegex);
           //number highlighting
           e.ChangedRange.SetStyle(cNumber, CSharpNumberRegex);
           //attribute highlighting
           e.ChangedRange.SetStyle(cAttribute, CSharpAttributeRegex);
           //class name highlighting
           e.ChangedRange.SetStyle(cClassName, CSharpClassNameRegex);
           //keyword highlighting
           e.ChangedRange.SetStyle(cKeyword, CSharpKeywordRegex);
        

           //find document comments
           foreach (FastColoredTextBoxNS.Range r in CurrentTB.Range.tb.GetRanges(@"^\s*///.*$", RegexOptions.Multiline))
           {
               //remove C# highlighting from this fragment
               r.ClearStyle(FastColoredTextBoxNS.StyleIndex.All);
              
               r.SetStyle(cComment);
              
               //prefix '///'
               foreach (FastColoredTextBoxNS.Range rr in r.GetRanges(@"^\s*///", RegexOptions.Multiline))
               {
                   rr.ClearStyle(FastColoredTextBoxNS.StyleIndex.All);
                   rr.SetStyle(cComment);
               }
           }

           //clear folding markers
           e.ChangedRange.ClearFoldingMarkers();
           //set folding markers
           e.ChangedRange.SetFoldingMarkers("{", "}"); //allow to collapse brackets block
           e.ChangedRange.SetFoldingMarkers(@"#region\b", @"#endregion\b"); //allow to collapse #region blocks
           e.ChangedRange.SetFoldingMarkers(@"/\*", @"\*/"); //allow to collapse comment block

        }

        private void lispToolStripMenuItem_Click(object sender, EventArgs e) {
            if (lispToolStripMenuItem.Checked) {
                prologToolStripMenuItem.Checked = false; noneToolStripMenuItem.Checked = true; lispToolStripMenuItem.Checked = false;
                yaccJToolStripMenuItem.Checked = false; jFlexToolStripMenuItem.Checked = false; cToolStripMenuItem.Checked = false; Properties.Settings.Default.syntaxChosen = "None";
                CurrentTB.Language = FastColoredTextBoxNS.Language.Custom; CurrentTB.DescriptionFile = null; }
            else {
                prologToolStripMenuItem.Checked = false; noneToolStripMenuItem.Checked = false; lispToolStripMenuItem.Checked = true;
                yaccJToolStripMenuItem.Checked = false; jFlexToolStripMenuItem.Checked = false; cToolStripMenuItem.Checked = false;
                if (Properties.Settings.Default.syntaxChosen == "Prolog" || Properties.Settings.Default.syntaxChosen == "jflex" || Properties.Settings.Default.syntaxChosen == "yacc") {
                    Properties.Settings.Default.syntaxChosen = "None"; } Properties.Settings.Default.syntaxChosen = "Lisp"; }
         Properties.Settings.Default.Save(); }

        private void columnsToolStripMenuItem_Click(object sender, EventArgs e)
            //80 columns limit but only in the status bar - obviously disables the other two
        { if (!columnsToolStripMenuItem.Checked) {
                columnsToolStripMenuItem.Checked = true; Properties.Settings.Default.antoniottiStandard = true; Properties.Settings.Default.antoniottiCrazy = false;
                crazy80ToolStripMenuItem.Checked = false; noLimitToolStripMenuItem.Checked = false; } 
        else if (columnsToolStripMenuItem.Checked) {
                columnsToolStripMenuItem.Checked = false; noLimitToolStripMenuItem.Checked = true; crazy80ToolStripMenuItem.Checked = false;
                Properties.Settings.Default.antoniottiCrazy = false; Properties.Settings.Default.antoniottiStandard = false; }
            antoniotti80Panel.Visible = false; check(); getCurrentPosition(); Properties.Settings.Default.Save(); }

        private void crazy80ToolStripMenuItem_Click(object sender, EventArgs e)
            //antoniotti mode - it shows in the middle of the window a popup
        { if (!crazy80ToolStripMenuItem.Checked) {
            columnsToolStripMenuItem.Checked = false; Properties.Settings.Default.antoniottiStandard = false; Properties.Settings.Default.antoniottiCrazy = true;
                crazy80ToolStripMenuItem.Checked = true; noLimitToolStripMenuItem.Checked = false; }
            else if (crazy80ToolStripMenuItem.Checked) {
                columnsToolStripMenuItem.Checked = false; noLimitToolStripMenuItem.Checked = true; crazy80ToolStripMenuItem.Checked = false;
                Properties.Settings.Default.antoniottiCrazy = false; Properties.Settings.Default.antoniottiStandard = false; }
            check(); getCurrentPosition(); Properties.Settings.Default.Save(); }

        private void noLimitToolStripMenuItem_Click(object sender, EventArgs e)
        //disables all limitations regarding columns
        { columnsToolStripMenuItem.Checked = false; noLimitToolStripMenuItem.Checked = true; crazy80ToolStripMenuItem.Checked = false;
            Properties.Settings.Default.antoniottiCrazy = false; Properties.Settings.Default.antoniottiStandard = false; Properties.Settings.Default.Save();
            antoniotti80Panel.Visible = false; check(); getCurrentPosition(); }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
            //renames the current tab to something else - might deprecate idk
        { faTabStrip1.SelectedItem.Title = toolStripTextBox4.Text; }

        private void toolStripTextBox4_Click_1(object sender, EventArgs e)
        { toolStripTextBox4.SelectAll(); }

        private void yaccJToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (yaccJToolStripMenuItem.Checked) {
                prologToolStripMenuItem.Checked = false; noneToolStripMenuItem.Checked = true;lispToolStripMenuItem.Checked = false;
                yaccJToolStripMenuItem.Checked = false; jFlexToolStripMenuItem.Checked = false; cCToolStripMenuItem.Checked = false;
                Properties.Settings.Default.syntaxChosen = "None"; CurrentTB.Language = FastColoredTextBoxNS.Language.Custom;
                CurrentTB.DescriptionFile = null; CurrentTB.Text = CurrentTB.Text + ""; }
            else {
                prologToolStripMenuItem.Checked = false; noneToolStripMenuItem.Checked = false; lispToolStripMenuItem.Checked = false;
                yaccJToolStripMenuItem.Checked = true; jFlexToolStripMenuItem.Checked = false; cCToolStripMenuItem.Checked = false;
                if (Properties.Settings.Default.syntaxChosen == "Lisp" || Properties.Settings.Default.syntaxChosen == "Prolog" || Properties.Settings.Default.syntaxChosen == "jflex" || Properties.Settings.Default.syntaxChosen == "C") {
                    Properties.Settings.Default.syntaxChosen = "None"; }
                Properties.Settings.Default.syntaxChosen = "yacc"; CurrentTB.Text = CurrentTB.Text + ""; } Properties.Settings.Default.Save(); }

        private void jFlexToolStripMenuItem_Click(object sender, EventArgs e) {
            if (jFlexToolStripMenuItem.Checked) {
                prologToolStripMenuItem.Checked = false; noneToolStripMenuItem.Checked = true; lispToolStripMenuItem.Checked = false;
                yaccJToolStripMenuItem.Checked = false; jFlexToolStripMenuItem.Checked = false; cCToolStripMenuItem.Checked = false;
                Properties.Settings.Default.syntaxChosen = "None"; CurrentTB.Language = FastColoredTextBoxNS.Language.Custom;
                CurrentTB.DescriptionFile = null; CurrentTB.Text = CurrentTB.Text + ""; }
            else {
                prologToolStripMenuItem.Checked = false; noneToolStripMenuItem.Checked = false; lispToolStripMenuItem.Checked = false;
                yaccJToolStripMenuItem.Checked = false; jFlexToolStripMenuItem.Checked = true; cCToolStripMenuItem.Checked = false;
                if (Properties.Settings.Default.syntaxChosen == "Lisp" || Properties.Settings.Default.syntaxChosen == "Prolog" || Properties.Settings.Default.syntaxChosen == "yacc" || Properties.Settings.Default.syntaxChosen == "C" ) {
                    Properties.Settings.Default.syntaxChosen = "None"; }
                Properties.Settings.Default.syntaxChosen = "jflex"; CurrentTB.Text = CurrentTB.Text + ""; } Properties.Settings.Default.Save(); }

        private void logixHelp() {
            string curDir = Directory.GetCurrentDirectory(); //get the current app directory
            string filePath = String.Format("file:///{0}/" + Properties.Settings.Default.chmFile, curDir); //modify description file of the fastcolored textbox
            try { // Start the default application for the file
                Process.Start(filePath); }
            catch (Exception ex) { toolStripStatusLabel.Text = ("An error occurred: " + ex.Message); } }

        //logix help
        private void quickPrologGuideToolStripMenuItem_Click(object sender, EventArgs e) { logixHelp(); }

        public void webSearch() { try { String searchRequest = CurrentTB.SelectedText;
                if (Properties.Settings.Default.searchWebAuto) {
                    switch (Properties.Settings.Default.syntaxChosen) {
                        case "Prolog":
                            Properties.Settings.Default.searchWebPath = "https://www.swi-prolog.org/search?for=";
                            break;

                        case "LISP":
                            Properties.Settings.Default.searchWebPath = "https://www.cliki.net/site/search?query=";
                            break;

                        case "byacc":
                            Properties.Settings.Default.searchWebPath = "https://stackoverflow.com/search?q=byacc+";
                            break;

                        case "jflex":
                            Properties.Settings.Default.searchWebPath = "https://stackoverflow.com/search?q=jflex+";
                            break;

                        case "None":
                            Properties.Settings.Default.searchWebPath = "https://www.google.com/search?q=";
                            break;
                        case "C":
                            Properties.Settings.Default.searchWebPath = "https://learn.microsoft.com/en-us/search/?scope=C%252B%252B&view=msvc-170&terms=";
                            break;
                    } Properties.Settings.Default.Save(); }
                System.Diagnostics.Process.Start(Properties.Settings.Default.searchWebPath + System.Uri.EscapeDataString(searchRequest)); }
            catch (Exception) { } }
        private void searchOnTheInternetToolStripMenuItem_Click(object sender, EventArgs e) { webSearch(); }

        private void stayOnTopToolStripMenuItem_Click(object sender, EventArgs e) {
            if (stayOnTopToolStripMenuItem.Checked) {
                stayOnTopToolStripMenuItem.Checked = false; Properties.Settings.Default.windowOnTop = false; this.TopMost = false; }
            else {
                stayOnTopToolStripMenuItem.Checked = true; Properties.Settings.Default.windowOnTop = true; this.TopMost = true; } Properties.Settings.Default.Save();
        }

        private void newWindowToolStripMenuItem_Click(object sender, EventArgs e) {
            Thread thread = new Thread(OpenForm); thread.SetApartmentState(ApartmentState.STA); thread.Start(); }

        public static void OpenForm() {
            Application.EnableVisualStyles(); Application.SetCompatibleTextRenderingDefault(false); Application.Run(new MDIParent1()); }

        //saves window state when you're closing it basically
        public void windowState() {
            switch (this.WindowState) { 
                case FormWindowState.Normal:
                    Properties.Settings.Default.windowState = "Normal";
                    break;

                case FormWindowState.Maximized:
                    Properties.Settings.Default.windowState = "Maximized";
                    break;

                case FormWindowState.Minimized:
                    Properties.Settings.Default.windowState = "Normal";
                    break;
            } Properties.Settings.Default.Save(); }

        public void setWindowState()
        {
            switch (Properties.Settings.Default.windowState) {
                case "Normal":
                    this.WindowState = FormWindowState.Normal;
                    break;

                case "Maximized":
                    this.WindowState = FormWindowState.Maximized;
                    break; } }

        public void setWindowPosition()
        { this.Location = new Point(Properties.Settings.Default.windowLeft, Properties.Settings.Default.windowTop); }

        //saves your current window position in the properties
        public void getWindowPosition() {
            Properties.Settings.Default.windowLeft = this.Left; Properties.Settings.Default.windowTop = this.Top; Properties.Settings.Default.Save(); }

        public void setWindowSize() {
            this.Size = new Size(Properties.Settings.Default.windowWidth, Properties.Settings.Default.windowHeight); }

        //saves your current window size in the properties
        public void getWindowSize() {
            Properties.Settings.Default.windowWidth = this.Width; Properties.Settings.Default.windowHeight = this.Height; Properties.Settings.Default.Save(); }

        public void setStartWindowPlace() {
            switch (Properties.Settings.Default.startWindowPlace) {
                case "Manual":
                    this.StartPosition = FormStartPosition.Manual;
                    break;

                case "WindowsDefaultLocation":
                    this.StartPosition = FormStartPosition.WindowsDefaultLocation;
                    break;

                case "WindowsDefaultBounds":
                    this.StartPosition = FormStartPosition.WindowsDefaultBounds;
                    break;

                case "CenterParent":
                    this.StartPosition = FormStartPosition.CenterParent;
                    break;

                case "CenterScreen":
                    this.StartPosition = FormStartPosition.CenterScreen;
                    break; } }

        private void rowsToolStripMenuItem_Click(object sender, EventArgs e) {
            if (rowsToolStripMenuItem.Checked)
            { CurrentTB.ShowLineNumbers = false; rowsToolStripMenuItem.Checked = false; rowsToolStripMenuItem1.Checked = false; }
            else { CurrentTB.ShowLineNumbers = true; rowsToolStripMenuItem.Checked = true; rowsToolStripMenuItem1.Checked = true; } }

        private void trackBar1_ValueChanged(object sender, EventArgs e) {
            CurrentTB.LineInterval = trackBar1.Value; ToolTip.Show("Line interval: " + trackBar1.Value.ToString(), trackBar1, 1000); }

        private void trackBar2_ValueChanged(object sender, EventArgs e) {
            CurrentTB.LineNumberStartValue = (uint)trackBar2.Value; ToolTip.Show("Starting number: " + trackBar2.Value.ToString(), trackBar2); }

        private void trackBar3_Scroll(object sender, EventArgs e) { CurrentTB.TabLength = trackBar3.Value; ToolTip.Show("Tab length: " + trackBar3.Value.ToString(), trackBar3); }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e) {
            switch (comboBox4.Text) {
                case "By word":
                    CurrentTB.WordWrapMode = FastColoredTextBoxNS.WordWrapMode.WordWrapControlWidth; label62.Text = "Your document will have words wrapped entirely, making the entire document more readable.";
                    break;

                case "By character":
                    CurrentTB.WordWrapMode = FastColoredTextBoxNS.WordWrapMode.CharWrapControlWidth; label62.Text = "Your document will have words wrapped based on the single characters of them.";
                    break; } }

        private void checkBox10_CheckedChanged(object sender, EventArgs e) {
            if (!checkBox10.Checked) {
                CurrentTB.WideCaret = false; label61.Text = "The caret used on your document will be the default Windows one."; }
            else {
                CurrentTB.WideCaret = true; label61.Text = "The caret used on your document will be a wider one, more akin to old terminals and editors.";
            } }

        private void checkBox9_CheckedChanged(object sender, EventArgs e) { if (checkBox9.Checked) { CurrentTB.ReadOnly = true; } else { CurrentTB.ReadOnly = false; } }

        private void checkBox7_CheckedChanged(object sender, EventArgs e) { if (checkBox7.Checked) CurrentTB.ShowScrollBars = true; else CurrentTB.ShowScrollBars = false; }

        private void checkBox4_CheckedChanged(object sender, EventArgs e) {
            if (checkBox4.Checked){ CurrentTB.AutoIndent = true; checkBox6.Enabled = true;
            } else{ CurrentTB.AutoIndent = false; checkBox6.Enabled = false; } }

        private void checkBox5_CheckedChanged(object sender, EventArgs e) { if (checkBox5.Checked) { CurrentTB.AutoIndentChars = true; } else { CurrentTB.AutoIndentChars = false; } }

        private void textPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textPropertiesToolStripMenuItem.Checked)
            { panel8.Visible = false; textPropertiesToolStripMenuItem.Checked = false; Properties.Settings.Default.textPropertiesVisible = false; }
            else
            { panel8.Visible = true; textPropertiesToolStripMenuItem.Checked = true; Properties.Settings.Default.textPropertiesVisible = true; }
            textPropertiesCheck(); Properties.Settings.Default.Save();
        }

        private void trackBar4_Scroll(object sender, EventArgs e) { CurrentTB.PreferredLineWidth = trackBar4.Value; ToolTip.Show("Preferred line width: " + trackBar4.Value, trackBar4); }

        private void textBox8_TextChanged(object sender, EventArgs e) { CurrentTB.AutoIndentCharsPatterns = textBox8.Text; }

        private void checkBox6_CheckedChanged(object sender, EventArgs e) {
            if (checkBox6.Checked) CurrentTB.AutoIndentExistingLines = true; else CurrentTB.AutoIndentExistingLines = false; }

        //text properties methods - NEED TO REDO mouse-related ones
        private void label42_Click(object sender, EventArgs e) { panel8.Hide(); textPropertiesToolStripMenuItem.Checked = false; }

        private void label43_MouseDown(object sender, MouseEventArgs e) {
            mouseDownPanelTopBottom(sender, e, panel8); panel8.Dock = DockStyle.None; }

        private void label43_MouseMove(object sender, MouseEventArgs e) { mouseMovePanelTopBottom(sender, e, panel8); }

        private void label43_MouseUp(object sender, MouseEventArgs e) { mouseUpPanelTopBottom(sender, e, panel8); }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) { if (checkBox1.Checked) CurrentTB.AutoCompleteBrackets = true; else CurrentTB.AutoCompleteBrackets = false; }

        private void textBox2_TextChanged(object sender, EventArgs e) { CurrentTB.LeftBracket = textBox2.Text[0]; }

        private void textBox3_TextChanged(object sender, EventArgs e) { CurrentTB.RightBracket = textBox3.Text[0]; }

        private void textBox5_TextChanged(object sender, EventArgs e) { CurrentTB.LeftBracket2 = textBox5.Text[0]; }

        private void textBox4_TextChanged(object sender, EventArgs e) { CurrentTB.RightBracket2 = textBox4.Text[0]; }

        private void fctb_MouseMove(object sender, MouseEventArgs e) {
            FastColoredTextBoxNS.Place p = CurrentTB.PointToPlace(e.Location);
            if (CharIsHyperlink(p)) CurrentTB.Cursor = Cursors.Hand; else CurrentTB.Cursor = Cursors.IBeam; }

        private void fctb_MouseDown(object sender, MouseEventArgs e) {
            FastColoredTextBoxNS.Place p = CurrentTB.PointToPlace(e.Location);
            if (CharIsHyperlink(p)) { String url = CurrentTB.GetRange(p, p).GetFragment(@"[\S]").Text; Process.Start(url); } }

        private void runWithJFlexToolStripMenuItem_Click(object sender, EventArgs e) { LogixPE logixPE = new LogixPE(); logixPE.Show(); }

        private void rulerToolStripMenuItem_Click(object sender, EventArgs e) {
            if (!rulerToolStripMenuItem.Checked) { ruler.Visible = true; Properties.Settings.Default.rulerVisible = true; rulerToolStripMenuItem.Checked = true; }
            else {
                ruler.Visible = false; Properties.Settings.Default.rulerVisible = false; rulerToolStripMenuItem.Checked = false; }
             Properties.Settings.Default.Save(); }

        bool CharIsHyperlink(FastColoredTextBoxNS.Place place) {
            FastColoredTextBoxNS.StyleIndex mask = CurrentTB.GetStyleIndexMask(new FastColoredTextBoxNS.Style[] { blueStyle });
            if (place.iChar < CurrentTB.GetLineLength(place.iLine))
                if ((CurrentTB[place].style & mask) != 0)
                    return true;
            return false;
        }

        private void printToolStripMenuItem_Click_1(object sender, EventArgs e) {
            if (CurrentTB != null) {
                PrintDocument p = new PrintDocument(); p.PrintPage += delegate(object sender1, PrintPageEventArgs e1)
                { e1.Graphics.DrawString(CurrentTB.Text, Properties.Settings.Default.defaultFont, Brushes.Black, 20, 20); };
                printDialog1.AllowPrintToFile = true; printDialog1.Document = p; printDialog1.AllowCurrentPage = true;
                printDialog1.UseEXDialog = true;
                if (printDialog1.ShowDialog() == DialogResult.OK) { p.Print(); } } }
        
        private void toolStripLabel14_MouseUp(object sender, MouseEventArgs e) {
            clickToolbarUndo(this.toolStripLabel14, leftNew, rightNew); gramlexClicked = false; }

        private void toolStripLabel14_MouseDown(object sender, MouseEventArgs e) {
            clickToolbar(this.toolStripLabel14, leftNew, rightNew, Properties.Settings.Default.leftPaddingToolbar, Properties.Settings.Default.rightPaddingToolbar);
            gramlexClicked = true; }

        private void toolStripButton37_Click(object sender, EventArgs e) { gramlexSendFiles(); }

        private void cCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cCToolStripMenuItem.Checked) {
                prologToolStripMenuItem.Checked = false; noneToolStripMenuItem.Checked = true; lispToolStripMenuItem.Checked = false;
                yaccJToolStripMenuItem.Checked = false; jFlexToolStripMenuItem.Checked = false; cCToolStripMenuItem.Checked = false;
                Properties.Settings.Default.syntaxChosen = "None"; CurrentTB.Language = FastColoredTextBoxNS.Language.Custom;
                CurrentTB.DescriptionFile = null; CurrentTB.Text = CurrentTB.Text + ""; }
            else {
                prologToolStripMenuItem.Checked = false; noneToolStripMenuItem.Checked = false; lispToolStripMenuItem.Checked = false;
                yaccJToolStripMenuItem.Checked = false; jFlexToolStripMenuItem.Checked = false; cCToolStripMenuItem.Checked = true;
                if (Properties.Settings.Default.syntaxChosen == "Lisp" || Properties.Settings.Default.syntaxChosen == "Prolog" || Properties.Settings.Default.syntaxChosen == "yacc" || Properties.Settings.Default.syntaxChosen == "jflex" ) {
                    Properties.Settings.Default.syntaxChosen = "None"; }
                Properties.Settings.Default.syntaxChosen = "C"; CurrentTB.Text = CurrentTB.Text + ""; } Properties.Settings.Default.Save(); 
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (CurrentTB != null)
                {
                    ExplorerItem item = explorerList[e.RowIndex];
                    CurrentTB.GoEnd();
                    CurrentTB.SelectionStart = item.position;
                    CurrentTB.DoSelectionVisible();
                    CurrentTB.Focus();
                }
            }
            catch (Exception) { }
        }

        List<ExplorerItem> explorerList = new List<ExplorerItem>();

        private void ReBuildObjectExplorer(object state)
        {
            string text = state as string;

            try
            {
                List<ExplorerItem> list = new List<ExplorerItem>();
                int lastClassIndex = -1;

                 Regex regex = new Regex(@"^(?<range>[\w\s]+\b(class|struct|enum|interface)\s+[\w<>,\s]+)|^\s*(public|private|internal|protected|int)[^\n]+(\n?\s*{|;)?", RegexOptions.Multiline);
                foreach (Match r in regex.Matches(text))
                    try
                    {
                        string s = r.Value;
                        int i = s.IndexOfAny(new char[] { '=', '{', ';' });
                        if (i >= 0)
                            s = s.Substring(0, i);
                        s = s.Trim();

                        ExplorerItem item = new ExplorerItem(); item.title = s; item.position = r.Index;
                        if (Regex.IsMatch(item.title, @"\b(class|struct|enum|interface)\b"))
                        {
                            item.title = item.title.Substring(item.title.LastIndexOf(' ')).Trim();
                            item.type = ExplorerItemType.Class;
                            list.Sort(lastClassIndex + 1, list.Count - (lastClassIndex + 1), new ExplorerItemComparer());
                            lastClassIndex = list.Count;
                        }
                        else
                            if (item.title.Contains(" event "))
                            {
                                int ii = item.title.LastIndexOf(' ');
                                item.title = item.title.Substring(ii).Trim();
                                item.type = ExplorerItemType.Event;
                            }
                            else
                                if (item.title.Contains("("))
                                {
                                    string[] parts = item.title.Split('(');
                                    item.title = parts[0].Substring(parts[0].LastIndexOf(' ')).Trim() + "(" + parts[1];
                                    item.type = ExplorerItemType.Method;
                                }
                                else
                                    if (item.title.EndsWith("]"))
                                    {
                                        string[] parts = item.title.Split('[');
                                        if (parts.Length < 2) continue;
                                        item.title = parts[0].Substring(parts[0].LastIndexOf(' ')).Trim() + "[" + parts[1];
                                        item.type = ExplorerItemType.Method;
                                    }
                                    else
                                    {
                                        int ii = item.title.LastIndexOf(' ');
                                        item.title = item.title.Substring(ii).Trim();
                                        item.type = ExplorerItemType.Property;
                                    }
                        list.Add(item);
                    }
                    catch { ;}

                if (Properties.Settings.Default.objectBrowserSort)
                list.Sort(lastClassIndex + 1, list.Count - (lastClassIndex + 1), new ExplorerItemComparer());
                BeginInvoke(new MethodInvoker(delegate
                {
                    explorerList = list;
                            dvgObjectExplorer.RowCount = explorerList.Count;
                            dvgObjectExplorer.Invalidate();
                            dvgObjectExplorer.Rows.Clear();
                    foreach(ExplorerItem ex in list){
                        switch (ex.type)
                        {
                            case ExplorerItemType.Class:
                                dvgObjectExplorer.Rows.Add(Properties.Resources.Class, ex.title);
                                break;

                            case ExplorerItemType.Method:
                                dvgObjectExplorer.Rows.Add(Properties.Resources.Method, ex.title);
                                break;

                            case ExplorerItemType.Event:
                                dvgObjectExplorer.Rows.Add(Properties.Resources.Event, ex.title);
                                break;

                            case ExplorerItemType.Property:
                                dvgObjectExplorer.Rows.Add(Properties.Resources.Property, ex.title);
                                break;
                        }
                    }
                }));
            }
            catch { ;}
        }

        void tb_TextChangedDelayed(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            FastColoredTextBoxNS.FastColoredTextBox tb = (sender as FastColoredTextBoxNS.FastColoredTextBox);
            //rebuild object explorer
            string text = (sender as FastColoredTextBoxNS.FastColoredTextBox).Text;
            ThreadPool.QueueUserWorkItem(new WaitCallback(ReBuildObjectExplorer), text);
        }

        enum ExplorerItemType
        {
            Class, Method, Property, Event
        }

        class ExplorerItem
        {
            public ExplorerItemType type;
            public string title;
            public int position;
        }

        class ExplorerItemComparer : IComparer<ExplorerItem>
        {
            public int Compare(ExplorerItem x, ExplorerItem y)
            {
                return x.title.CompareTo(y.title);
            }
        }

        private void dataGridView1_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            /*
           // try
           // {
                ExplorerItem item = explorerList[e.RowIndex]; MessageBox.Show(item.title.ToString() + " " + item.type.ToString());
                if (e.ColumnIndex == 1)
                    e.Value = item.title;
                else
                    switch (item.type)
                    {
                        case ExplorerItemType.Class:
                            e.Value = Properties.Resources.Class;
                            return;
                        case ExplorerItemType.Method:
                            e.Value = Properties.Resources.Method;
                            return;
                        case ExplorerItemType.Event:
                            e.Value = Properties.Resources.Event;
                            return;
                        case ExplorerItemType.Property:
                            e.Value = Properties.Resources.Property;
                            return;
                    }
            }
            catch { ;}
             * */
        }

        private void label70_Click(object sender, EventArgs e)
        {
            panel12.Hide(); Properties.Settings.Default.objectBrowser = false; objectBrowserToolStripMenuItem.Checked = false;
        }

        private void objectBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (objectBrowserToolStripMenuItem.Checked) { objectBrowserToolStripMenuItem.Checked = false; Properties.Settings.Default.objectBrowser = false; panel12.Visible = false; }
            else { objectBrowserToolStripMenuItem.Checked = true; Properties.Settings.Default.objectBrowser = true; panel12.Visible = true; }
            Properties.Settings.Default.Save();
        }

        private void label71_Click(object sender, EventArgs e)
        {
            if (splitContainer4.Panel1Collapsed) splitContainer4.Panel1Collapsed = false;
            else splitContainer4.Panel1Collapsed = true;
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox6.SelectedIndex)
            {
                case 0:
                    Properties.Settings.Default.objectBrowserSort = true;
                    break;

                case 1:
                    Properties.Settings.Default.objectBrowserSort = false;
                    break;
            }
            Properties.Settings.Default.Save();
        }

 
    }

    public class TextSourceWithLineFiltering : FastColoredTextBoxNS.TextSource
    {
        List<int> toSourceIndex = new List<int>();
        private string _lineFilterRegex;

        public string LineFilterRegex
        {
            get { return _lineFilterRegex; }
            set { _lineFilterRegex = value; UpdateFilter(); }
        }

        private void UpdateFilter()
        {
            toSourceIndex.Clear();

            int count = base.lines.Count;
            Regex regex = new Regex(LineFilterRegex);
            for (int i = 0; i < count; i++)
            {
                if (regex.IsMatch(lines[i].Text))
                    toSourceIndex.Add(i);
            }

            CurrentTB.NeedRecalc(true);
            CurrentTB.Invalidate();
        }

        public TextSourceWithLineFiltering(FastColoredTextBoxNS.FastColoredTextBox tb)
            : base(tb)
        {
        }

        public override int Count
        {
            get { return toSourceIndex.Count; }
        }

        public override FastColoredTextBoxNS.Line this[int i]
        {
            get { return base[toSourceIndex[i]]; }
            set { base[toSourceIndex[i]] = value; }
        }

        public override void InsertLine(int index, FastColoredTextBoxNS.Line line)
        {
            if (index >= toSourceIndex.Count)
            {
                int c = lines.Count;
                while (index >= toSourceIndex.Count)
                    toSourceIndex.Add(c++);
            }
            else
            {
                int srcIndex = toSourceIndex[index];
                toSourceIndex.Insert(index, srcIndex);
                for (int i = index + 1; i < toSourceIndex.Count; i++)
                    toSourceIndex[i]++;
            }

            index = toSourceIndex[index];
            base.InsertLine(index, line);
        }

        public override void RemoveLine(int index, int count)
        {
            for (int ii = index + count - 1; ii >= index; ii--)
            {
                int srcIndex = toSourceIndex[ii];
                base.RemoveLine(srcIndex, 1);

                toSourceIndex.RemoveAt(ii);

                for (int i = index; i < toSourceIndex.Count; i++)
                    toSourceIndex[i]--;
            }
        }

        public override int GetLineLength(int i)
        {
            return base.GetLineLength(toSourceIndex[i]);
        }

        public override bool LineHasFoldingStartMarker(int iLine)
        {
            return base.LineHasFoldingStartMarker(toSourceIndex[iLine]);
        }

        public override bool LineHasFoldingEndMarker(int iLine)
        {
            return base.LineHasFoldingEndMarker(toSourceIndex[iLine]);
        }
    }
    }





/// <summary>
/// Builds list of methods and properties for current class name was typed in the textbox
/// </summary>
internal class DynamicCollection : IEnumerable<AutocompleteItem>
{
    private AutocompleteMenu menu;
    private FastColoredTextBoxNS.FastColoredTextBox tb;

    public DynamicCollection(AutocompleteMenu menu, FastColoredTextBoxNS.FastColoredTextBox tb)
    {
        this.menu = menu;
        this.tb = tb;
    }

    public IEnumerator<AutocompleteItem> GetEnumerator()
        {
            //get current fragment of the text
            string text = menu.Fragment.Text;

            //extract class name (part before dot)
            string[] parts = text.Split('.');
            if (parts.Length < 2)
                yield break;
            string className = parts[parts.Length - 2];

            //find type for given className
            Type type = FindTypeByName(className);

            if (type == null)
                yield break;

            //return static methods of the class
            List<MethodAutocompleteItem> items = new List<MethodAutocompleteItem>();

            // Ottieni i nomi dei metodi distinti
            List<string> methodNames = new List<string>();
            MethodInfo[] methods = type.GetMethods();
            foreach (MethodInfo mi in methods)
            {
                if (!methodNames.Contains(mi.Name))
                {
                    methodNames.Add(mi.Name);
                    MethodAutocompleteItem item = new MethodAutocompleteItem(mi.Name + "()");
                    item.ToolTipTitle = mi.Name;
                    item.ToolTipText = "Description of method " + mi.Name + " goes here.";
                    items.Add(item);
                }
            }

            // Restituisci le proprietà statiche della classe
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo pi in properties)
            {
                MethodAutocompleteItem item = new MethodAutocompleteItem(pi.Name);
                item.ToolTipTitle = pi.Name;
                item.ToolTipText = "Description of property " + pi.Name + " goes here.";
                items.Add(item);
            }

            List<MethodAutocompleteItem> resultItems = new List<MethodAutocompleteItem>();
            resultItems.AddRange(items);
            foreach (MethodAutocompleteItem item in resultItems)
            {
                yield return item;
            }

        }

    Type FindTypeByName(string name)
    {
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (Assembly a in assemblies)
        {
            foreach (Type t in a.GetTypes())
                if (t.Name == name)
                {
                    return t;
                }
        }

        return null;
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class RoundedPanel : Panel {
    private float _thickness = 5;
    public float Thickness {
        get {
            return _thickness;
        }
        set {
            _thickness = value;
            _pen = new Pen(_borderColor,Thickness);
            Invalidate();
        }
    }

    private Color _borderColor = Color.White;
    public Color BorderColor {
        get {
            return _borderColor;
        }
        set {
            _borderColor = value;
            _pen = new Pen(_borderColor,Thickness);
            Invalidate();
        }
    }

    private int _radius = 20;
    public int Radius {
        get {
            return _radius;
        }
        set {
            _radius = value;
            Invalidate();
        }
    }

    private Pen _pen;

    public RoundedPanel() : base() {
        _pen = new Pen(BorderColor,Thickness);
        DoubleBuffered = true;            
    }

    private Rectangle GetLeftUpper(int e) {
        return new Rectangle(0,0,e,e);
    }

    private Rectangle GetRightUpper(int e) {
        return new Rectangle(Width - e,0,e,e);
    }
    private Rectangle GetRightLower(int e) {
        return new Rectangle(Width - e,Height - e,e,e);
    }
    private Rectangle GetLeftLower(int e) {
        return new Rectangle(0,Height - e,e,e);
    }

    private void ExtendedDraw(PaintEventArgs e) {
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        GraphicsPath path = new GraphicsPath();
        path.StartFigure();
        path.AddArc(GetLeftUpper(Radius),180,90);
        path.AddLine(Radius,0,Width - Radius,0);
        path.AddArc(GetRightUpper(Radius),270,90);
        path.AddLine(Width,Radius,Width,Height - Radius);
        path.AddArc(GetRightLower(Radius),0,90);
        path.AddLine(Width - Radius,Height,Radius,Height);
        path.AddArc(GetLeftLower(Radius),90,90);
        path.AddLine(0,Height - Radius,0,Radius);
        path.CloseFigure();
        Region = new Region(path);
    }
    private void DrawSingleBorder(Graphics graphics) {
        graphics.DrawArc(_pen,new Rectangle(0,0,Radius,Radius),180,90);
        graphics.DrawArc(_pen,new Rectangle(Width - Radius - 1,-1,Radius,Radius),270,90);
        graphics.DrawArc(_pen,new Rectangle(Width - Radius - 1,Height - Radius - 1,Radius,Radius),0,90);
        graphics.DrawArc(_pen,new Rectangle(0,Height - Radius - 1,Radius,Radius),90,90);
        graphics.DrawRectangle(_pen,0.0f,0.0f,(float)Width - 1.0f,(float)Height - 1.0f);
    }
    private void Draw3DBorder(Graphics graphics) {
        DrawSingleBorder(graphics);
    }
    private void DrawBorder(Graphics graphics) {
        DrawSingleBorder(graphics);
    }
    protected override void OnPaint(PaintEventArgs e) {
        base.OnPaint(e);
        ExtendedDraw(e);
        DrawBorder(e.Graphics);
    }
}