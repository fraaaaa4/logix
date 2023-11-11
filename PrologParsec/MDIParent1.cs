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
using System.Threading;
using Transitions;
using System.Diagnostics;


namespace PrologParsec
{
    public partial class MDIParent1 : Form
    {
        AutocompleteMenu popupMenu; //popup menu for when you type in
        private int childFormNumber = 0; //mdi window childs

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

        //toolbar buttons padding
        int leftNew; int rightNew; int leftTab; int rightTab; int leftOpen; int rightOpen; int leftCut; int rightCut; int leftCopy; int rightCopy; int leftUndo; int rightUndo; int leftFont; int rightFont; int leftNav; int rightNav; int leftSave; int rightSave; int leftPaste; int rightPaste; int leftNavNav; int rightNavNav;
        static bool Windows9x = false; private bool resize9x = false;//deactivate aurora and transitions if you're running it on 9x/2000
        static string productName; static string currentVersion; static string buildLab; //Windows version check

        //check Windows version
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

        //get Windows product name - check GetWindowsVersion()
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

        //get the Windows build - check GetWindowsVersion()
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

        //automatic icon changing method
        public void iconChange()
        {
            string version = GetWindowsVersion();
            if (!Properties.Settings.Default.customIcons) //automatic changing based on your Windows version
            {
                if (version == "5.0" || productName.Contains("2000")) //Windows 2000 - classic toolbar
                {
                    toolStrip1.Visible = false; //Fluent
                    toolStrip2.Visible = false; //Aero
                    toolStrip3.Visible = true;  //Classic
                    toolStrip4.Visible = false; //Gnome

                    label21.Text = "  "; //remove the fluent icon from Open
                    label21.Image = Properties.Resources.open200032; //set the Open icon
                    label21.Height = label21.Height + 10; //make it taller
                    label21.Top = label21.Top - 10; //move it down to counter-attack the height change

                    label26.Text = "  "; //remove the fluent icon from New
                    label26.Image = Properties.Resources.tab2000; //set the New icon
                    label26.Height = label26.Height + 4; //make it taller
                    label26.Top = label26.Top - 4; //move it down to counter-attack the height change

                    label36.Text = "  "; //see previous ones
                    label36.Image = Properties.Resources.tab2000;
                    label36.Height = label36.Height + 4;
                    label36.Top = label36.Top - 4;

                    label25.Text = "  "; //see previous ones
                    label25.Image = Properties.Resources.open200032;
                    label25.Height = label25.Height + 4;
                    label25.Top = label25.Top - 4;

                    label35.Text = "  "; //see previous ones
                    label35.Image = Properties.Resources.open200032;
                    label35.Height = label35.Height + 4;
                    label35.Top = label35.Top - 4;
                }
                else if (version == "5.1" || version == "5.2" || version == "6.0" || version == "6.1" || version == "6.2" || version == "6.3") //Windows XP/Vista/7/8 - Aero toolbar
                {
                    if (!productName.Contains("Windows 10")) //if you're running 10: Fluent toolbar
                    {
                        toolStrip1.Visible = false; //Fluent
                        toolStrip2.Visible = true;  //Aero
                        toolStrip3.Visible = false; //Classic
                        toolStrip4.Visible = false; //Gnome

                        label21.Text = "  "; //see Windows 2000 part
                        label21.Image = Properties.Resources.Open32; //Big Open icon variant
                        label21.Height = label21.Height + 25; //make it bigger so it fits
                        label21.Top = label21.Top - 20;
                        label21.Width = label21.Width + 25;
                        label21.Left = label21.Left - 10;


                        label26.Text = "  ";
                        label26.Image = Properties.Resources.Tab_Sheet_New;
                        label26.Height = label26.Height + 4;

                        label36.Text = "  ";
                        label36.Image = Properties.Resources.Tab_Sheet_New;
                        label36.Height = label36.Height + 4;

                        label25.Text = "  ";
                        label25.Image = Properties.Resources.Open;
                        label25.Height = label25.Height + 4;

                        label35.Text = "  ";
                        label35.Image = Properties.Resources.Open;
                        label35.Height = label35.Height + 4;

                    }
                }
                else if (version == "6.3" && productName.Contains("Windows 10")) //if you're running 10 or 11, turn on the Fluent toolbar
                {
                    toolStrip1.Visible = true; //Fluent
                    toolStrip2.Visible = false;//Aero
                    toolStrip3.Visible = false;//Classic
                    toolStrip4.Visible = false;//Gnome

                    label21.Text = ""; //Set Unicode Fluent Icons
                    label21.Height = 35;
                    label21.Top = 35;
                    label21.Width = 50;
                    label21.Left = 63;
                    label21.Image = null; //remove the image

                    label26.Text = "";
                    label26.Image = null;
                    label26.Location = new Point(49, 110); //set back the locations manually
                    label26.Size = new Size(20, 13); //set back the size manually

                    label25.Text = "";
                    label25.Image = null;
                    label25.Location = new Point(49, 133);
                    label25.Size = new Size(20, 13);

                    label36.Text = "";
                    label36.Image = null;
                    label36.Location = new Point(13, 40);
                    label36.Size = new Size(20, 13);

                    label35.Text = "";
                    label35.Image = null;
                    label35.Location = new Point(13, 61);
                    label35.Size = new Size(20, 13);
                }
                else if (!productName.Contains("Windows 10") && version != "5.0" && version != "5.1" && version != "5.2" && version != "6.0" && version != "6.1" && version != "6.2" && version != "6.3")
                {
                    //if you're running a version that's not one recognised, just put the Aero toolbar
                    toolStrip1.Visible = false; //Fluent
                    toolStrip2.Visible = true;  //Aero
                    toolStrip3.Visible = false; //Classic
                    toolStrip4.Visible = false; //Gnome

                    label21.Text = "  ";
                    label21.Image = Properties.Resources.Open32;
                    label21.Height = label21.Height + 25;
                    label21.Top = label21.Top - 20;
                    label21.Width = label21.Width + 25;
                    label21.Left = label21.Left - 10;


                    label26.Text = "  ";
                    label26.Image = Properties.Resources.Tab_Sheet_New;
                    label26.Height = label26.Height + 4;

                    label36.Text = "  ";
                    label36.Image = Properties.Resources.Tab_Sheet_New;
                    label36.Height = label36.Height + 4;

                    label25.Text = "  ";
                    label25.Image = Properties.Resources.Open;
                    label25.Height = label25.Height + 4;

                    label35.Text = "  ";
                    label35.Image = Properties.Resources.Open;
                    label35.Height = label35.Height + 4;
                }
            }

            //changing toolbars from Settings - force change them
            if (Properties.Settings.Default.customIcons)
            {
                if (Properties.Settings.Default.fluentStyle) //fluent toolbar
                {
                    toolStrip1.Visible = true;
                    toolStrip2.Visible = false;
                    toolStrip3.Visible = false;
                    toolStrip4.Visible = false;

                    label21.Text = "";
                    label21.Height = 35;
                    label21.Top = 35;
                    label21.Width = 50;
                    label21.Left = 63;
                    label21.Image = null;

                    label26.Text = "";
                    label26.Image = null;
                    label26.Location = new Point(49, 110);
                    label26.Size = new Size(20, 13);

                    label25.Text = "";
                    label25.Image = null;
                    label25.Location = new Point(49, 133);
                    label25.Size = new Size(20, 13);

                    label36.Text = "";
                    label36.Image = null;
                    label36.Location = new Point(13, 40);
                    label36.Size = new Size(20, 13);

                    label35.Text = "";
                    label35.Image = null;
                    label35.Location = new Point(13, 61);
                    label35.Size = new Size(20, 13);

                    cutToolStripMenuItem1.Image = Properties.Resources.cut;
                    copyToolStripMenuItem1.Image = Properties.Resources.copy;
                    pasteToolStripMenuItem1.Image = Properties.Resources.paste;
                    deleteToolStripMenuItem1.Image = Properties.Resources.delete;
                    selectToolStripMenuItem1.Image = Properties.Resources.select;
                    findreplaceToolStripMenuItem.Image = Properties.Resources.find;
                    searchOnTheInternetToolStripMenuItem.Image = Properties.Resources.link;
                    goToToolStripMenuItem1.Image = Properties.Resources.go_to;
                    bookmarksToolStripMenuItem.Image = Properties.Resources.bookmark;
                    addANewTabToolStripMenuItem.Image = Properties.Resources.add;
                    closeCurrentTabToolStripMenuItem.Image = Properties.Resources.remove;
                    renameCurrentTabToolStripMenuItem.Image = Properties.Resources.rename;

                }
                else if (Properties.Settings.Default.lunaStyle) //aero toolbar
                {
                    toolStrip1.Visible = false;
                    toolStrip2.Visible = true;
                    toolStrip3.Visible = false;
                    toolStrip4.Visible = false;

                    label21.Text = "  ";
                    label21.Image = Properties.Resources.Open32;
                    label21.Height = label21.Height + 25;
                    label21.Top = label21.Top - 20;
                    label21.Width = label21.Width + 25;
                    label21.Left = label21.Left - 10;


                    label26.Text = "  ";
                    label26.Image = Properties.Resources.Tab_Sheet_New;
                    label26.Height = label26.Height + 4;

                    label36.Text = "  ";
                    label36.Image = Properties.Resources.Tab_Sheet_New;
                    label36.Height = label36.Height + 4;

                    label25.Text = "  ";
                    label25.Image = Properties.Resources.Open;
                    label25.Height = label25.Height + 4;

                    label35.Text = "  ";
                    label35.Image = Properties.Resources.Open;
                    label35.Height = label35.Height + 4;

                    cutToolStripMenuItem1.Image = Properties.Resources.Cut1;
                    copyToolStripMenuItem1.Image = Properties.Resources.Copy1;
                    pasteToolStripMenuItem1.Image = Properties.Resources.Paste1;
                    deleteToolStripMenuItem1.Image = Properties.Resources.deleteB;
                    selectToolStripMenuItem1.Image = Properties.Resources.selectB;
                    findreplaceToolStripMenuItem.Image = Properties.Resources.findB;
                    searchOnTheInternetToolStripMenuItem.Image = Properties.Resources.linkB;
                    goToToolStripMenuItem1.Image = Properties.Resources.go_toB;
                    bookmarksToolStripMenuItem.Image = Properties.Resources.bookmarkB;
                    addANewTabToolStripMenuItem.Image = Properties.Resources.addB;
                    closeCurrentTabToolStripMenuItem.Image = Properties.Resources.removeB;
                    renameCurrentTabToolStripMenuItem.Image = Properties.Resources.renameB;
                }
                else if (Properties.Settings.Default.classicStyle) //classic toolbar
                {
                    toolStrip1.Visible = false;
                    toolStrip2.Visible = false;
                    toolStrip3.Visible = true;
                    toolStrip4.Visible = false;

                    label21.Text = "  ";
                    label21.Image = Properties.Resources.open200032;
                    label21.Height = label21.Height + 10;
                    label21.Top = label21.Top - 10;

                    label26.Text = "  ";
                    label26.Image = Properties.Resources.tab2000;
                    label26.Height = label26.Height + 4;
                    label26.Top = label26.Top - 4;

                    label36.Text = "  ";
                    label36.Image = Properties.Resources.tab2000;
                    label36.Height = label36.Height + 4;
                    label36.Top = label36.Top - 4;

                    label25.Text = "  ";
                    label25.Image = Properties.Resources.open2000;
                    label25.Height = label25.Height + 4;
                    label25.Top = label25.Top - 4;

                    label35.Text = "  ";
                    label35.Image = Properties.Resources.open2000;
                    label35.Height = label35.Height + 4;
                    label35.Top = label35.Top - 4;

                    cutToolStripMenuItem1.Image = Properties.Resources.cut2000;
                    copyToolStripMenuItem1.Image = Properties.Resources.copy2000;
                    pasteToolStripMenuItem1.Image = Properties.Resources.paste2000;
                    deleteToolStripMenuItem1.Image = Properties.Resources.delete2000;
                    selectToolStripMenuItem1.Image = Properties.Resources.select2000;
                    findreplaceToolStripMenuItem.Image = Properties.Resources.find2000;
                    searchOnTheInternetToolStripMenuItem.Image = Properties.Resources.link2000;
                    goToToolStripMenuItem1.Image = Properties.Resources.go_to2000;
                    bookmarksToolStripMenuItem.Image = Properties.Resources.bookmark2000;
                    addANewTabToolStripMenuItem.Image = Properties.Resources.add2000;
                    closeCurrentTabToolStripMenuItem.Image = Properties.Resources.remove2000;
                    renameCurrentTabToolStripMenuItem.Image = Properties.Resources.rename2000;
                }
                else if (Properties.Settings.Default.ClassicNineStyle) //gnome toolbar
                {
                    toolStrip1.Visible = false;
                    toolStrip2.Visible = false;
                    toolStrip3.Visible = false;
                    toolStrip4.Visible = true;

                    label21.Text = "  ";
                    label21.Image = Properties.Resources.macOpen;
                    label21.Height = label21.Height + 10;
                    label21.Top = label21.Top - 10;

                    label26.Text = "  ";
                    label26.Image = Properties.Resources.macNew;
                    label26.Height = label26.Height + 4;
                    label26.Top = label26.Top - 4;

                    label36.Text = "  ";
                    label36.Image = Properties.Resources.macNew;
                    label36.Height = label36.Height + 4;
                    label36.Top = label36.Top - 4;

                    label25.Text = "  ";
                    label25.Image = Properties.Resources.macOpen;
                    label25.Height = label25.Height + 4;
                    label25.Top = label25.Top - 4;

                    label35.Text = "  ";
                    label35.Image = Properties.Resources.macOpen;
                    label35.Height = label35.Height + 4;
                    label35.Top = label35.Top - 4;

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
            }
        }

        //check your Windows version and activate-deactivate features
        public void WindowsCheck()
        {
            string version = GetWindowsVersion();
            string productName = GetWindowsProductName();
            //check windows version
            if (version.Equals("4.0") || version.Equals("5.0") || version.Equals("5.1") || version.Equals("5.2") || version.Equals("6.0") || version.Equals("6.1"))
            {
                webBrowser2.Navigate("about:blank"); //disable Aurora SVGs on Start Page
                splitContainer1.Panel2Collapsed = false; //do not disable the web browser
            }
            else if (version.Equals("6.2") || version.Equals("6.3"))
            {
                splitContainer1.Panel2Collapsed = false; //if you're running it on 8 or 10, do not disable the Aurora
            }
            else
            {
                webBrowser2.Visible = false; //if you're running it on any other unrecognised version, disable it entirely
                splitContainer1.Panel2Collapsed = true;
            }

            //change toolbar style on Vista and 7
            /*
                   if (version == "6.0" || version == "6.1")
                   {
                       toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
                       toolStrip1.RenderMode = ToolStripRenderMode.System;
                   }
             * */

            //transitions problem
            if (version.Equals("5.0") && !version.Equals("5.1") && !version.Equals("5.2") && !version.Equals("6.0") && !version.Equals("6.1") && !version.Equals("6.2") && !version.Equals("6.3"))
            {
                resize9x = true; //if you're running it on 2000, disable transitions
            }
            else if (version.Equals("5.1") || version.Equals("5.2") || version.Equals("6.0") || version.Equals("6.1") || version.Equals("6.2") || version.Equals("6.3"))
            {
                resize9x = false; //if you're not running it on 2000, enable transitions
            }
            else
            {
                resize9x = true; //disable transitions because you don't know whether or not 
            }
            iconChange(); //manage icon packs throughout the OS    
        }


        public MDIParent1()
        {
            InitializeComponent();
            check(); //check for checked properties and all
            WindowsCheck(); //check for your Windows version
            setStartWindowPlace(); //set in which position it must start
            faTabStrip1.RemoveTab(faTabStripItem1); //remove the tab with fastcontrol1 - old code

            //create new autocomplete
            //popupMenu = new AutocompleteMenu();
            //popupMenu.SetAutocompleteItems(new DynamicCollection(popupMenu, Fastcolored1));

            centerPanel(roundedPanel1); //center the panels 
            centerPanel(panel6);
            tabChangeCheck(); //check on which tab you currently are

        }

        //system methods
        private void ShowNewForm(object sender, EventArgs e)
        {
            // Create a new instance of the child form.
            Form childForm = new Form();
            // Make it a child of this MDI form before showing it.
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
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

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Use System.Windows.Forms.Clipboard to insert the selected text or images into the clipboard
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Use System.Windows.Forms.Clipboard to insert the selected text or images into the clipboard
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Use System.Windows.Forms.Clipboard.GetText() or System.Windows.Forms.GetData to retrieve information from the clipboard.
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
        {
            CurrentTB.Redo(); //redo
        }

        private void toolStripLabel3_Click_1(object sender, EventArgs e)
        {
            openFile(); //open a new file

        }

        private void toolStripLabel8_Click_1(object sender, EventArgs e)
        {
            SaveQuestion(); //ask if you want to save the current unsaved file
        }

        private void toolStripLabel1_Click_1(object sender, EventArgs e)
        {
            CurrentTB.NavigateBackward(); //navigate backwards
        }

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
            fontDialog.MinSize = 8;
            fontDialog.MaxSize = 72;
            fontDialog.FontMustExist = true;

            if (CurrentTB != null)
                fontDialog.Font = CurrentTB.Font; //set the font dialog if there's a textbox

            //font dialog
            //fastcolored doesn't support all fonts
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                Font newFont = fontDialog.Font;

                // CurrentTB.Font = fontDialog.Font;
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
        {
            CurrentTB.Undo(); //undo text
        }

        private void toolStripLabel5_Click(object sender, EventArgs e)
        {
            CurrentTB.Paste(); //paste text
        }

        private void toolStripLabel6_Click(object sender, EventArgs e)
        {
            CurrentTB.Copy(); //copy text
        }

        private void toolStripLabel7_Click(object sender, EventArgs e)
        {
            CurrentTB.Cut(); //cut text
        }

        private string currentFilePath = string.Empty; //current file path

        private void toolStripLabel8_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFile(currentFilePath); //try to save the current file
            }
            catch (ArgumentException)
            {
            }
        }


        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            openFile();  //open a file
            // MessageBox.Show(toolStripLabel3.Font.Size.ToString());
            if (toolStripLabel3.Font.Size == 14.25)
                clickToolbar(this.toolStripLabel3, leftNew, rightNew, Properties.Settings.Default.leftPaddingToolbar, Properties.Settings.Default.rightPaddingToolbar); //try to reset the icon dimension - only for Fluent toolbar
            openClicked = false;
        }

        //syntax menu in status bar
        private void checkSyntaxMenu()
        {
            switch (Properties.Settings.Default.syntaxChosen)
            {

                case "Prolog": //you've chosen Prolog
                    prologToolStripMenuItem.Checked = true;
                    lispToolStripMenuItem.Checked = false;
                    yaccJToolStripMenuItem.Checked = false;
                    jFlexToolStripMenuItem.Checked = false;
                    noneToolStripMenuItem.Checked = false;
                    break;

                case "Lisp": //you've chosen Lisp
                    prologToolStripMenuItem.Checked = false;
                    lispToolStripMenuItem.Checked = true;
                    yaccJToolStripMenuItem.Checked = false;
                    jFlexToolStripMenuItem.Checked = false;
                    noneToolStripMenuItem.Checked = false;
                    break;

                case "yacc": //you've chosen yacc
                    prologToolStripMenuItem.Checked = false;
                    lispToolStripMenuItem.Checked = false;
                    yaccJToolStripMenuItem.Checked = true;
                    jFlexToolStripMenuItem.Checked = false;
                    noneToolStripMenuItem.Checked = false;
                    break;

                case "jflex": //you've chosen jflex
                    prologToolStripMenuItem.Checked = false;
                    lispToolStripMenuItem.Checked = false;
                    yaccJToolStripMenuItem.Checked = false;
                    jFlexToolStripMenuItem.Checked = true;
                    noneToolStripMenuItem.Checked = false;
                    break;

                case "None": //plain text
                    prologToolStripMenuItem.Checked = false;
                    lispToolStripMenuItem.Checked = false;
                    yaccJToolStripMenuItem.Checked = false;
                    jFlexToolStripMenuItem.Checked = false;
                    noneToolStripMenuItem.Checked = true;
                    break;

            }
        }

        private void openFile()
        {
            //if (isTextModified)
            // {
            //    SaveQuestion();
            // }
            //open prolog files
            OpenFileDialog openFileDialog = new OpenFileDialog(); //create a new dialog
            openFileDialog.Filter = "Prolog source files (*.pl, *.pro)|*.pl;*.pro|Prolog consultable files (*.consult)|*.consult|LISP source files (*.lisp, *.l, *.cl, *.fasl)|*.lisp;*.l;*.cl;*.fasl|yacc files (*.y)|*.y|JFlex Source files (*.j, *.l)|*.j;*.l|XML files (*.xml)|*.xml|XAML files (*.xaml)|*.xaml|XML Document Type Definition files (*.dtd)|*.dtd|XML Schema Definition files (*.xsd)|*.xsd|XML Extensible Stylesheet Language files (*.xsl)|*.xsl|C# source files (*.cs)|*.cs|Visual Basic source files (*.vb)|*.vb|Visual Basic .NET files (*.vbnet)|*.vbnet|HTML files (*.html, *.htm)|*.html;*.htm|SQL files (*.sql)|*.sql|PHP files (*.php)|*.php|Javascript files (*.js)|*.js|Lua files (*.lua)|*.lua|Rich Text Document files (*.rtf)|*.rtf|Plain Text files (*.txt)|*.txt|All files (*.*)|*.*"; //set extensions for dialog
            if (Properties.Settings.Default.syntaxFileExtensionOpen)
            {
                switch (Properties.Settings.Default.syntaxChosen)
                {
                    case "Prolog":
                        openFileDialog.FilterIndex = 1; //if you've selected prolog syntax, select Prolog files
                        break;

                    case "Lisp":
                        openFileDialog.FilterIndex = 3; //if you've selected lisp syntax, select Lisp files
                        break;

                    case "yacc":
                        openFileDialog.FilterIndex = 4; //if you've selected yacc syntax, select yacc files
                        break;

                    case "jflex":
                        openFileDialog.FilterIndex = 5; //if you've selected jflex syntax, select jflex files
                        break;

                    case "None":
                        openFileDialog.FilterIndex = 20; //if you want to write a plain text, open txt files
                        break;
                }
            }
            else if (!Properties.Settings.Default.syntaxFileExtensionOpen) //if you don't have automatic extensions set up
            {
                openFileDialog.FilterIndex = Properties.Settings.Default.syntaxFileExtensionIndexOpen; //get from the properties which file extension you want 
            }
            openFileDialog.RestoreDirectory = true; //restore directory to the previous one you were in
            string filePath; string fileContent; //file path and file content
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //read file
                filePath = openFileDialog.FileName; //file name
                fileContent = File.ReadAllText(filePath); //read all the contents of a file
                currentFilePath = openFileDialog.FileName; //current file path
                currentFilePath = Path.GetFileName(currentFilePath); //Get the name of the file path
                FarsiLibrary.Win.FATabStripItem tabCreated = CreateTab1(openFileDialog.FileName); //create a new tab with the 
                openClicked = false;
                try
                {
                    MDIParent1.ActiveForm.Text = "Logix Testfire - " + filePath; //put the window title
                    tabCreated.Title = filePath; //set the tab title as the file path
                    isTextModified = false; //obsoleted - set the file as not edited yet
                    if (Properties.Settings.Default.syntaxOpenFileAuto)
                    {
                        if (openFileDialog.FileName.Contains(".pl") || openFileDialog.FileName.Contains(".pro"))
                            Properties.Settings.Default.syntaxChosen = "Prolog";
                        else if (openFileDialog.FileName.Contains(".lisp") || openFileDialog.FileName.Contains(".lsp") || openFileDialog.FileName.Contains(".cl") || openFileDialog.FileName.Contains(".fasl"))
                            Properties.Settings.Default.syntaxChosen = "Lisp";
                        else if (openFileDialog.FileName.Contains(".y"))
                        {
                            Properties.Settings.Default.syntaxChosen = "yacc";
                        }
                        else if (openFileDialog.FileName.Contains(".j") || openFileDialog.FileName.Contains(".l"))
                        {
                            Properties.Settings.Default.syntaxChosen = "jflex";
                            jFlexToolStripMenuItem.Checked = true;
                        }
                        else
                        {
                            Properties.Settings.Default.syntaxChosen = "None";
                        }
                    }
                    else
                    {
                        if (Properties.Settings.Default.syntaxOpenDefaultMode != "Previous one")
                            Properties.Settings.Default.syntaxChosen = Properties.Settings.Default.syntaxOpenDefaultMode;
                        //else do nothing
                    }
                }
                catch (Exception e)
                {
                    toolStripStatusLabel.Text = ("Null reference error"); //null error
                }
            }
            if (!string.IsNullOrEmpty(currentFilePath) || !string.IsNullOrEmpty(currentFilePath)) //if there isn't a file opened then disable save
            {
                saveToolStripMenuItem.Enabled = false;
            }

            //clickToolbarUndo(this.toolStripLabel3, leftNew, rightNew);
            //openClicked = false;
            checkSyntaxMenu(); tabChangeCheck();
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            newFile(); //new file
        }


        private void newFile()
        {
            SaveQuestion(); //ask to save the current file or not
            tabChangeCheck();
            CurrentTB.Clear(); //clear the current textbox content
            isTextModified = false; //set the document as not modified
            currentFilePath = string.Empty; //empty the current file path as it's nothing
            MDIParent1.ActiveForm.Text = "Logix Testfire "; //reset the window titlebar
        }

        private void SaveQuestion() //ask to save a file or not
        {
            newClicked = false; //you haven't clicked the new button therefore it shouldn't enlarge it

            if (isTextModified) //if you've modified a file
            {
                DialogResult result = MessageBox.Show("Do you want to save the current file?", "Unsaved changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning); //show a dialog box asking you if you want to save or not

                if (result == DialogResult.Yes) //if you press yes
                {
                    if (!string.IsNullOrEmpty(currentFilePath)) //if there's a file already opened
                    {
                        SaveFile(currentFilePath); //just save it
                    }
                    else
                    {
                        SaveFileAsNew(); //else ask to save it as a new file
                    }
                }
                else if (result == DialogResult.Cancel) //else if you press cancel, end it
                {
                    return;
                }

            }

            newClicked = false; //you haven't clicked the new button therefore it shouldn't enlarge it
        }

        private void SaveFileAsNew() //save as
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog(); //create a new save dialog
            saveFileDialog.Filter = "Prolog source files (*.pl, *.pro)|*.pl;*.pro|Prolog consultable files (*.consult)|*.consult|LISP source files (*.lisp, *.l, *.cl, *.fasl)|*.lisp;*.l;*.cl;*.fasl|yacc files (*.y)|*.y|JFlex Source files (*.j)|*.j|XML files (*.xml)|*.xml|XAML files (*.xaml)|*.xaml|XML Document Type Definition files (*.dtd)|*.dtd|XML Schema Definition files (*.xsd)|*.xsd|XML Extensible Stylesheet Language files (*.xsl)|*.xsl|C# source files (*.cs)|*.cs|Visual Basic source files (*.vb)|*.vb|Visual Basic .NET files (*.vbnet)|*.vbnet|HTML files (*.html, *.htm)|*.html;*.htm|SQL files (*.sql)|*.sql|PHP files (*.php)|*.php|Javascript files (*.js)|*.js|Lua files (*.lua)|*.lua|Rich Text Document files (*.rtf)|*.rtf|Plain Text files (*.txt)|*.txt|All files (*.*)|*.*"; //set extensions for dialog
            if (Properties.Settings.Default.syntaxFileExtension)
            {
                switch (Properties.Settings.Default.syntaxChosen)
                {
                    case "Prolog":
                        saveFileDialog.FilterIndex = 1;
                        break;

                    case "Lisp":
                        saveFileDialog.FilterIndex = 3;
                        break;

                    case "yacc":
                        saveFileDialog.FilterIndex = 4;
                        break;

                    case "jflex":
                        saveFileDialog.FilterIndex = 5;
                        break;

                    case "None":
                        saveFileDialog.FilterIndex = 20;
                        break;
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
                isTextModified = false; //deprecated - set the document as already saved
                currentFilePath = filePath; //set the current file path as the one in which you've saved
                faTabStripItem1.Title = filePath; //set again the tab title

                MDIParent1.ActiveForm.Text = MDIParent1.ActiveForm.Text.Replace("*", ""); //remove the asterisk from the titlebar to simbolize you have saved the document
            }
        }

        private void SaveFile(string filePath) //save file
        {
            if (string.IsNullOrEmpty(currentFilePath) || string.IsNullOrEmpty(filePath))
            { //if there isn't any file open
                SaveFileAsNew(); //save file as new
            }
            String textToSave = CurrentTB.Text; //save the current text to a string
            File.WriteAllText(filePath, textToSave); //write it to a file
            toolStripStatusLabel.Text = "Saved"; //tell the user it's saved
            isTextModified = false; //set the bool deprecated to false
            MDIParent1.ActiveForm.Text = MDIParent1.ActiveForm.Text.Replace("*", ""); //replace the asterisk that tells the user that a file is modified
        }


        private bool isTextModified = false; //deprecated

        private void Fastcolored1_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            isTextModified = true; //deprecated - the text is modified
            toolStripStatusLabel.Text = "Ready"; //modify the status label to be ready rather than save
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(Asterisk)); //add an asterisk if you modify the textbox
            }
            else
            {
                Asterisk();
            }
            if (!string.IsNullOrEmpty(currentFilePath) || !string.IsNullOrEmpty(currentFilePath)) //if there exists a file
            {
                saveToolStripMenuItem.Enabled = true; //make it so you can save it
            }

            // CurrentTB.CommentPrefix = "%";
            //autocompleteMenu1.SetAutocompleteItems(new DynamicCollection(CurrentTB));
        }

        //Check fastcolored1
        private void CurrentTB_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            isTextModified = true;

            toolStripStatusLabel.Text = "Ready";
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(Asterisk)); //add an asterisk if you modify the textbox
                SaveFile(currentFilePath);
            }
            else
            {
                Asterisk();
            }
            if (!string.IsNullOrEmpty(currentFilePath) || !string.IsNullOrEmpty(currentFilePath))
            {
                saveToolStripMenuItem.Enabled = true;
            }

            if (Properties.Settings.Default.antoniottiStandard && currentColumn > Properties.Settings.Default.columnLineLimit)
            {
                toolStripStatusLabel.Text = Properties.Settings.Default.antoniottiStandardText;
            }
            else if (Properties.Settings.Default.antoniottiCrazy && currentColumn > Properties.Settings.Default.columnLineLimit)
            {
                toolStripStatusLabel.Text = Properties.Settings.Default.antoniottiCrazyText;
            }

            //CurrentTB.CommentPrefix = "%";
            //autocompleteMenu1.SetAutocompleteItems(new DynamicCollection(CurrentTB));




            if (Properties.Settings.Default.syntaxChosen == "Prolog")
                applyPrologSyntax(e);
            else if (Properties.Settings.Default.syntaxChosen == "None")
                clearStyles(e);
            else if (Properties.Settings.Default.syntaxChosen == "Lisp")
                applyLispSyntax(e);
            else if (Properties.Settings.Default.syntaxChosen == "yacc")
                applyYaccSyntax(e);
            else if (Properties.Settings.Default.syntaxChosen == "jflex")
                applyJFlexSyntax(e);

            trackBar2.Maximum = CurrentTB.LinesCount;

            try
            {
                CurrentTB.DescriptionFile = "";
            }
            catch (NullReferenceException ea3)
            {
            }
        }

        private void clearStyles(FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            if (CurrentTB != null)
                CurrentTB.Range.tb.ClearStylesBuffer();
        }

        private void cleanStyles2(FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            if (CurrentTB != null)
                CurrentTB.Range.tb.ClearStylesBuffer();
        }
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
                //e.ChangedRange.SetStyle(anoni, "(?!<%)\\s*'([^']*)'");

                e.ChangedRange.SetStyle(comma, "(?!<=%)\\,");
                e.ChangedRange.SetStyle(point, "(?!<=%)\\.");

                e.ChangedRange.SetStyle(equals, "(?!<=%)=|@|<|>");
                e.ChangedRange.SetStyle(anon, "(?!<=%)_");
                CurrentTB.Refresh();

                /*foreach (FastColoredTextBoxNS.Range r in CurrentTB.Range.GetRanges(@"^\s*/
                //.*$", RegexOptions.Multiline))
                /*{
                    foreach (FastColoredTextBoxNS.Range rr in r.GetRanges(@"^\s*/
                //", RegexOptions.Multiline))
                /*  {
                      CurrentTB.Range.tb.ClearStylesBuffer();
                      rr.SetStyle(comment);
                  }
              }
                 * */
            }
            catch (Exception ex2)
            {
                toolStripStatusLabel.Text = "Exceeded Prolog!";
            }
            //e.ChangedRange.SetStyle(comment, "(?<!\\/)\\/\\*((?:(?!\\*\\/).|\\s)*)\\*\\/", RegexOptions.Multiline);

        }

        private void Asterisk()
        {
            if (this.Text[this.Text.Length - 1] != '*') //if there isn't an asterisk at the end
            {
                this.Text = this.Text + "*"; //add one if it's modified
            }
        }

        private void MDIParent1_Load(object sender, EventArgs e)
        {



            if (Properties.Settings.Default.untitledFileStartup)
                CreateTab(null); //create a new untitled tab - optional

            check(); //check for checked methods and all
            tabChangeCheck();
            checkPadding(); //check the padding in the toolbar
            lunaToolbar();

            //window state
            setWindowState(); setWindowSize(); setWindowPosition();

            this.Text = this.Text.Substring(0, this.Text.Length - 1); //remove the asterisk when it starts
            faTabStripItem1.Title = "Untitled"; //deprecated - rename the new tab as Untitled
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
            catch (ArgumentException eq)
            {
            }

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

            tabChangeCheck(); //check if oyu need to enable the toolbar or not

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
        }

        private void Fastcolored1_Load(object sender, EventArgs e)
        {

            //isTextModified = false;
            if (CurrentTB != null)
            {
                CurrentTB.DescriptionFile = Properties.Settings.Default.descriptionFileDirectory; //load the description file in CurrentTB
                CurrentTB.AddStyle(comment);
            }

            CurrentTB.Font = Properties.Settings.Default.defaultFont; //load the default font - there's a problem in fastcolored1

            check(); //check method            
            //autocompleteMenu1.SetAutocompleteItems(new DynamicCollection(Fastcolored1));
        }

        private void runWithPrologToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void MDIParent1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isTextModified) //if the text is modified, upon closing the app, ask if you want to save it
                SaveQuestion();

            if (Properties.Settings.Default.autoWindowState)
            {
                windowState();
            }

            if (Properties.Settings.Default.autoWindowPosition)
                getWindowPosition();

            if (Properties.Settings.Default.autoWindowSize)
                getWindowSize();
        }

        private void newFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newFile(); //new file
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFile(); //open file
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile(currentFilePath); //save current file
        }

        private void saveAsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SaveFileAsNew(); //save as
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
        {
            openFile();  //open the file
            tabChangeCheck();
        }

        private void saveToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SaveFile(currentFilePath); //save the current file
        }

        private void saveAsToolStripMenuItem_Click_2(object sender, EventArgs e)
        {
            SaveFileAsNew(); //save as a new file
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit(); //exit from the mdi
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.Undo(); //undo
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.Redo(); //redo
        }

        private void cutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            CurrentTB.Cut(); //cut
        }

        private void copyToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            CurrentTB.Copy(); //copy
        }

        private void pasteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            CurrentTB.Paste(); //paste
        }

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
            check(); //check method
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

        private void check()
        {
            if (menuStrip1.Visible) //menu bar check
            {
                menuBarToolStripMenuItem.Checked = true;
                menuBarToolStripMenuItem1.Checked = true;
            }
            else
            {
                menuBarToolStripMenuItem.Checked = false;
                menuBarToolStripMenuItem1.Checked = false;
            }

            if (toolStrip1.Visible) //toolbar check
            {
                toolbarToolStripMenuItem.Checked = true;
                toolbarToolStripMenuItem1.Checked = true;
            }
            else
            {
                toolbarToolStripMenuItem.Checked = false;
                toolbarToolStripMenuItem1.Checked = false;
            }

            if (Fastcolored1.WordWrap) //eodr wrap check
            {
                wordWrapToolStripMenuItem.Checked = true;
                wordWrapToolStripMenuItem1.Checked = true;
            }
            else
            {
                wordWrapToolStripMenuItem.Checked = false;
                wordWrapToolStripMenuItem1.Checked = false;
            }

            if (Fastcolored1.WordWrapIndent > 0) //wird wroìao indent check
                wordWrapIndentToolStripMenuItem.Checked = true;
            else
                wordWrapIndentToolStripMenuItem.Checked = false;

            if (Fastcolored1.DescriptionFile == null) //description file check
                checkForSyntaxToolStripMenuItem.Checked = false;
            else
                checkForSyntaxToolStripMenuItem.Checked = true;

            if (documentMap1.Visible == true) //document map - is shown on the right
            {
                documentMapToolStripMenuItem.Checked = true;
                documentMapToolStripMenuItem1.Checked = true;
                Properties.Settings.Default.documentMap = true;
                Properties.Settings.Default.Save();
                splitContainer2.Panel2Collapsed = false;

            }
            else
            {
                documentMapToolStripMenuItem.Checked = false;
                documentMapToolStripMenuItem1.Checked = false;
                Properties.Settings.Default.documentMap = false;
                Properties.Settings.Default.Save();
                splitContainer2.Panel2Collapsed = true;

            }

            if (roundedPanel1.Visible) //if there's the go to line panel then check it
            {
                goToToolStripMenuItem.Checked = true;
                goToToolStripMenuItem1.Checked = true;
            }
            else
            {
                goToToolStripMenuItem.Checked = false;
                goToToolStripMenuItem1.Checked = false;
            }

            if (panel6.Visible) //the same but for the find and replace panel
            {
                findreplaceToolStripMenuItem.Checked = true;
                fIndAndReplaceToolStripMenuItem.Checked = true;
            }
            else
            {
                findreplaceToolStripMenuItem.Checked = false;
                fIndAndReplaceToolStripMenuItem.Checked = false;
            }


            if (toolStripStatusLabel2.Visible && Properties.Settings.Default.LineColumnView)
                lineAndColumnToolStripMenuItem.Checked = true;
            else
                lineAndColumnToolStripMenuItem.Checked = false;

            if (currentLine.Equals(""))
            {
                currentLine = 1;
            }

            if (currentColumn.Equals(""))
            {
                currentColumn = 1;
            }

            if (Properties.Settings.Default.antoniottiStandard)
            {
                columnsToolStripMenuItem.Checked = true;
                crazy80ToolStripMenuItem.Checked = false;
                noLimitToolStripMenuItem.Checked = false;
            }
            else if (Properties.Settings.Default.antoniottiCrazy && antoniotti80Panel.Visible == true)
            {
                columnsToolStripMenuItem.Checked = false;
                crazy80ToolStripMenuItem.Checked = true;
                noLimitToolStripMenuItem.Checked = false;
            }
            else if (!Properties.Settings.Default.antoniottiCrazy)
            {
                crazy80ToolStripMenuItem.Checked = false;
                antoniotti80Panel.Visible = false;
            }
            else if (!Properties.Settings.Default.antoniottiCrazy && !Properties.Settings.Default.antoniottiStandard)
            {
                crazy80ToolStripMenuItem.Checked = false;
                antoniotti80Panel.Visible = false;
                noLimitToolStripMenuItem.Checked = true;
                columnsToolStripMenuItem.Checked = false;
            }

            if (!Properties.Settings.Default.antoniottiCrazy)
                antoniotti80Panel.Visible = false;

            //edit columns limit based on what column limit you've set up
            columnsToolStripMenuItem.Text = Properties.Settings.Default.columnLineLimit + " columns";
            crazy80ToolStripMenuItem.Text = "Crazy " + Properties.Settings.Default.columnLineLimit;

            //web search
            if (Properties.Settings.Default.searchWeb)
            {
                searchOnTheInternetToolStripMenuItem.Visible = true;
                searchOnTheInternetToolStripMenuItem1.Visible = true;
            }
            else
            {
                searchOnTheInternetToolStripMenuItem.Visible = false;
                searchOnTheInternetToolStripMenuItem1.Visible = false;
            }

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

            string version = GetWindowsVersion();
            Console.WriteLine("Windows version: " + version);
            if (version == "4.0" || version == "5.0" || version == "5.1" || version == "6.0") //if you're running this on NT 4, 2000, XP or Vista don't show XAML - might be deprecated
                webBrowser2.Visible = false;

            //stay on top check
            if (Properties.Settings.Default.windowOnTop)
            {
                stayOnTopToolStripMenuItem.Checked = true;
                this.TopMost = true;
            }
            else
            {
                stayOnTopToolStripMenuItem.Checked = false;
                this.TopMost = false;
            }

            //jflex - yacc
            if (Properties.Settings.Default.bYaccIntegration)
                runWithByaccJToolStripMenuItem.Visible = true;
            else if (Properties.Settings.Default.bYaccIntegration == false)
                runWithByaccJToolStripMenuItem.Visible = false;

            if (Properties.Settings.Default.jFlexIntegration)
                runWithJFlexToolStripMenuItem.Visible = true;
            else if (Properties.Settings.Default.jFlexIntegration == false)
                runWithJFlexToolStripMenuItem.Visible = false;
        }

        private void toolbarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            check();
            if (toolStrip1.Visible) //toolbar check - make it visible or not
            {
                toolStrip1.Visible = false;
                toolbarToolStripMenuItem.Checked = false;
            }
            else
            {
                toolStrip1.Visible = true;
                toolbarToolStripMenuItem.Checked = true;
            }


        }

        private void addHeaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.Text = Properties.Settings.Default.headerText + "\n" + CurrentTB.Text; //add the default header text on top
            //to modify later: make it so it changes based on the selected syntax
        }

        private void addFooterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.AppendText("\n" + Properties.Settings.Default.footerText + currentFilePath); //add the default footer text on top
            //to modify later: make it so it changes based on the selected syntax
        }

        private void addFooterToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            CurrentTB.AppendText("\n" + Properties.Settings.Default.footerText + currentFilePath); //the same as the other
        }

        private void addHeaderToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.syntaxChosen == "Prolog")
                CurrentTB.Text = Properties.Settings.Default.headerText + "\n" + CurrentTB.Text; //the same as the other
            else if (Properties.Settings.Default.syntaxChosen == "Lisp")
                CurrentTB.Text = Properties.Settings.Default.headerLispText + "\n" + CurrentTB.Text;
            else if (Properties.Settings.Default.syntaxChosen == "yacc")
                CurrentTB.Text = Properties.Settings.Default.headerYaccText + "\n" + CurrentTB.Text;
            else if (Properties.Settings.Default.syntaxChosen == "jflex")
                CurrentTB.Text = Properties.Settings.Default.headerJflexText + "\n" + CurrentTB.Text;

        }

        private void aboutLogixToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 form2 = new AboutBox1(); //Create a new aboutBox
            form2.ShowDialog(); //show it
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.InsertText("\n%" + toolStripTextBox1.Text); //insert a new comment
        }


        private void colorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //  xmlDetails form2 = new xmlDetails();  //DEPRECATED - show XML config - to be removed
            //  form2.ShowDialog();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //go to line manager
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, "[^0-9]"))
            {
                //roundedPanel1.Height = 56;
                ToolTip hint = new ToolTip();
                hint.IsBalloon = true;
                hint.ToolTipTitle = "Please enter only numbers";
                hint.ToolTipIcon = ToolTipIcon.Error;
                hint.Show(string.Empty, textBox1, 0);
                hint.Show("Letters and symbols are not allowed.", textBox1);
                //label15.Text = "Please enter only numbers";
                //label15.Left = (this.roundedPanel1.Width - label15.Width) / 2;
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
                //check if the number of lines inserted is right
            }
            else
                GoToLine(int.Parse(textBox1.Text));
        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            check();
            if (CurrentTB.WordWrap)
            {
                CurrentTB.WordWrap = false;
                wordWrapToolStripMenuItem.Checked = false;
                wordWrapToolStripMenuItem1.Checked = false;
            }
            else
            {
                CurrentTB.WordWrap = true;
                wordWrapToolStripMenuItem.Checked = true;
                wordWrapToolStripMenuItem1.Checked = true;
            }
        }

        private void toolStripLabel10_Click(object sender, EventArgs e)
        {
            CurrentTB.NavigateForward();
        }

        private void toolStripLabel11_Click(object sender, EventArgs e)
        {
            fontText();
        }

        private void fOntsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontText();
        }

        private void wordWrapIndentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            check();
            if (CurrentTB.WordWrapIndent > 0)
                CurrentTB.WordWrapIndent = 0;
            else
                CurrentTB.WordWrapIndent = 1;
        }

        private void checkForSyntaxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentTB.DescriptionFile == null)
            {
                // Imposta il DescriptionFile per abilitare la formattazione della sintassi
                CurrentTB.DescriptionFile = Properties.Settings.Default.descriptionFileDirectory;

                // Aggiorna il controllo
                CurrentTB.Invalidate();
                CurrentTB.Refresh();
            }
            else
            {
                // Disabilita la formattazione della sintassi impostando DescriptionFile su null
                CurrentTB.DescriptionFile = null;

                // Aggiorna il controllo
                CurrentTB.Invalidate();
                CurrentTB.Refresh();
            }
        }

        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.Bookmarks.Remove(CurrentTB.Selection.Start.iLine);
        }

        private void dataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Rimuovi solo gli elementi associati ai bookmark
            RemoveBookmarkItems();

            // Aggiungi gli elementi del menu per i bookmark
            foreach (FastColoredTextBoxNS.Bookmark bookmark in Fastcolored1.Bookmarks)
            {
                ToolStripItem item = dataToolStripMenuItem.DropDownItems.Add(bookmark.Name);
                ToolStripItem item2 = bookmarksToolStripMenuItem.DropDownItems.Add(bookmark.Name);
                item.Tag = bookmark;
                item.Click += new EventHandler(item_Click);
                item2.Tag = bookmark;
                item2.Click += new EventHandler(item_Click);
            }
        }

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

        private void toolStripTextBox2_Click(object sender, EventArgs e)
        {

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

        private void documentMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            check();
            if (documentMapToolStripMenuItem.Checked == true || documentMapToolStripMenuItem1.Checked == true)
            {

                Transition.run(documentMap1, "Left", this.Width, new TransitionType_EaseInEaseOut(1000));
                documentMap1.Visible = false;
                splitContainer2.Panel2Collapsed = true;
                documentMapToolStripMenuItem1.Checked = false;
                documentMapToolStripMenuItem.Checked = false;
                Properties.Settings.Default.documentMap = false;
            }
            else
            {
                Transition.run(documentMap1, "Left", documentMap1.Left, new TransitionType_EaseInEaseOut(1000));
                documentMap1.Visible = true;
                splitContainer2.Panel2Collapsed = false;
                documentMapToolStripMenuItem1.Checked = true;
                documentMapToolStripMenuItem.Checked = true;
                Properties.Settings.Default.documentMap = true;
            }
            Properties.Settings.Default.Save();
            check();
        }



        private void newTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateTab(null);
        }


        private void CreateTab(string fileName)
        {
            try
            {
                FastColoredTextBoxNS.FastColoredTextBox tb = new FastColoredTextBoxNS.FastColoredTextBox();
                //tb.Font = Properties.Settings.Default.defaultFont;

                tb.Dock = DockStyle.Fill;
                //tb.VirtualSpace = true;

                tb.Language = Fastcolored1.Language;
                FarsiLibrary.Win.FATabStripItem tab = new FarsiLibrary.Win.FATabStripItem();
                tab.Tag = fileName;
                if (fileName != null)
                    tb.OpenFile(fileName);
                faTabStrip1.AddTab(tab);
                faTabStrip1.SelectedItem = tab;
                tb.Focus();
                tb.DelayedTextChangedInterval = 1000;
                tb.DelayedEventsInterval = 500;
                if (fileName == null)
                    tab.Title = "Untitled";
                else
                    tab.Title = fileName;
                tab.Controls.Add(tb);


                //clona
                tb.BackColor = Fastcolored1.BackColor;
                tb.ForeColor = Fastcolored1.ForeColor;
                tb.SelectionColor = Fastcolored1.SelectionColor;
                tb.TextAreaBorderColor = Fastcolored1.TextAreaBorderColor;
                tb.CaretColor = Fastcolored1.CaretColor;
                tb.DisabledColor = Fastcolored1.DisabledColor;
                tb.BookmarkColor = Fastcolored1.BookmarkColor;
                tb.CurrentLineColor = Fastcolored1.CurrentLineColor;
                tb.IndentBackColor = Fastcolored1.IndentBackColor;
                tb.FoldingIndicatorColor = Fastcolored1.FoldingIndicatorColor;
                tb.PaddingBackColor = Fastcolored1.PaddingBackColor;
                tb.ServiceLinesColor = Fastcolored1.ServiceLinesColor;
                tb.LeftBracket = Fastcolored1.LeftBracket;
                tb.LeftBracket2 = Fastcolored1.LeftBracket2;
                tb.RightBracket = Fastcolored1.RightBracket;
                tb.RightBracket2 = Fastcolored1.RightBracket2;
                tb.AutoCompleteBrackets = Fastcolored1.AutoCompleteBrackets;
                tb.ContextMenuStrip = Fastcolored1.ContextMenuStrip;
                tb.LineNumberColor = Fastcolored1.LineNumberColor;
                AutocompleteMenu popupMenu = new AutocompleteMenu();
                popupMenu.SetAutocompleteMenu(CurrentTB, autocompleteMenu1);
                popupMenu = autocompleteMenu1;
                tb.Font = new Font(Fastcolored1.Font.Name, Fastcolored1.Font.Size, Fastcolored1.Font.Style);
                tb.ZoomChanged += new EventHandler(CurrentTB_ZoomChanged);
                tb.TextChanged += new EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.CurrentTB_TextChanged);

                CurrentTB.TextChanged += CurrentTB_TextChanged;
                tb.SelectionChanged += new EventHandler(CurrentTB_SelectionChanged);
                tb.Zoom = Properties.Settings.Default.zoomFile;

                noFileOpenVisible();
                CurrentTB.Focus(); tabChangeCheck();



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
                FastColoredTextBoxNS.FastColoredTextBox tb = new FastColoredTextBoxNS.FastColoredTextBox();
                //tb.Font = Fastcolored1.Font;
                tb.Dock = DockStyle.Fill;
                //tb.VirtualSpace = true;

                tb.Language = Fastcolored1.Language;
                FarsiLibrary.Win.FATabStripItem tab = new FarsiLibrary.Win.FATabStripItem();
                tab.Tag = fileName;
                if (fileName != null)
                    tb.OpenFile(fileName);
                faTabStrip1.AddTab(tab);
                faTabStrip1.SelectedItem = tab;
                tb.Focus();
                tb.DelayedTextChangedInterval = 1000;
                tb.DelayedEventsInterval = 500;
                if (fileName == null)
                    tab.Title = "Untitled";
                else
                    tab.Title = fileName;

                tab.Controls.Add(tb);

                //clona
                tb.BackColor = Fastcolored1.BackColor;
                tb.ForeColor = Fastcolored1.ForeColor;
                tb.SelectionColor = Fastcolored1.SelectionColor;
                tb.TextAreaBorderColor = Fastcolored1.TextAreaBorderColor;
                tb.CaretColor = Fastcolored1.CaretColor;
                tb.DisabledColor = Fastcolored1.DisabledColor;
                tb.BookmarkColor = Fastcolored1.BookmarkColor;
                tb.CurrentLineColor = Fastcolored1.CurrentLineColor;
                tb.IndentBackColor = Fastcolored1.IndentBackColor;
                tb.FoldingIndicatorColor = Fastcolored1.FoldingIndicatorColor;
                tb.PaddingBackColor = Fastcolored1.PaddingBackColor;
                tb.ServiceLinesColor = Fastcolored1.ServiceLinesColor;
                tb.LeftBracket = Fastcolored1.LeftBracket;
                tb.LeftBracket2 = Fastcolored1.LeftBracket2;
                tb.RightBracket = Fastcolored1.RightBracket;
                tb.RightBracket2 = Fastcolored1.RightBracket2;
                tb.AutoCompleteBrackets = Fastcolored1.AutoCompleteBrackets;
                tb.ContextMenuStrip = Fastcolored1.ContextMenuStrip;
                tb.LineNumberColor = Fastcolored1.LineNumberColor;
                AutocompleteMenu popupMenu = new AutocompleteMenu();
                popupMenu.SetAutocompleteMenu(CurrentTB, autocompleteMenu1);
                popupMenu = autocompleteMenu1;
                tb.Font = new Font(Fastcolored1.Font.Name, Fastcolored1.Font.Size, Fastcolored1.Font.Style);
                tb.ZoomChanged += new EventHandler(CurrentTB_ZoomChanged);
                tb.TextChanged += new EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.CurrentTB_TextChanged);

                CurrentTB.TextChanged += CurrentTB_TextChanged;
                tb.SelectionChanged += new EventHandler(CurrentTB_SelectionChanged);
                tb.Zoom = Properties.Settings.Default.zoomFile;

                noFileOpenVisible();
                CurrentTB.Focus(); tabChangeCheck();

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
            try
            {
                if (CurrentTB != null)
                {
                    documentMap1.Target = CurrentTB;

                    //toolStripStatusLabel1.Text = "Syntax: " + CurrentTB.Language.ToString();
                    string text = CurrentTB.Text;
                    CurrentTB.Zoom = Properties.Settings.Default.zoomFile;


                    if (faTabStrip1.SelectedItem == faTabStripItem2)
                    {
                        tabChangeCheck();
                    }
                    else
                    {
                        tabChangeCheck();
                    }

                    toolStripTextBox4.Text = faTabStrip1.SelectedItem.Title;

                }
            }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.ToString());
            }



        }

        private void toolStripLabel12_Click(object sender, EventArgs e)
        {
            CreateTab(null);
        }

        private void btZoom_ButtonClick(object sender, EventArgs e)
        {

        }

        private void Zoom_click(object sender, EventArgs e)
        {
            if (CurrentTB != null)
            {
                CurrentTB.Zoom = int.Parse((sender as ToolStripItem).Tag.ToString());
                Properties.Settings.Default.zoomFile = CurrentTB.Zoom;
                Properties.Settings.Default.Save();
            }

        }

        private void toolStripTextBox3_KeyDown(object sender, KeyEventArgs e)
        {

        }

        bool tbFindChanged = false;

        private void toolStripTextBox3_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {
            CreateTab(null);
        }

        private void label9_Click(object sender, EventArgs e)
        {
            openFile();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

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
            centerElement(noOpenedFileGroup);
            centerElement(antoniotti80Panel);

            if (!moveGoToPanel && goToDockTop || !moveGoToPanel && goToDockBottom)
            {
                centerPanel(roundedPanel1);
                centerPanel(panel6);

            }
        }



        private void toolStripTextBox4_Click(object sender, EventArgs e)
        {

        }

        private void faTabStripItem1_Changed(object sender, EventArgs e)
        {

        }

        private void faTabStripItem1_Changed_1(object sender, EventArgs e)
        {

        }

        private void addFunctionToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
        {
            CurrentTB.SelectAll();
        }

        private void selectCurrentLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectCurrentLine();
        }

        private void selectCurrentWordToolStripMenuItem_Click(object sender, EventArgs e)
        {

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
        {
            roundedPanel1.Visible = false;
            roundedPanel1.Height = 36;
            goToToolStripMenuItem.Checked = false;
            goToToolStripMenuItem1.Checked = false;
        }

        private void goToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            check();
            centerPanel(roundedPanel1);
            bool roundedPanelVisible;
            if (!roundedPanel1.Visible)
            {
                roundedPanelVisible = true;
                roundedPanel1.Visible = true;


                goToToolStripMenuItem.Checked = true;
                goToToolStripMenuItem1.Checked = true;
            }
            else
            {
                roundedPanel1.Visible = false;
                goToToolStripMenuItem.Checked = false;
                goToToolStripMenuItem1.Checked = false;
            }

        }

        private void centerElement(Control control1)
        {
            int centerX = (this.ClientSize.Width - control1.Width) / 2;
            int centerY = (this.ClientSize.Height - control1.Height) / 2;

            if (Properties.Settings.Default.animationResizeEnable && !resize9x)
            {
                Transition.run(control1, "Left", centerX, new TransitionType_EaseInEaseOut(Properties.Settings.Default.animationResizeTop));
                Transition.run(control1, "Top", centerY, new TransitionType_EaseInEaseOut(Properties.Settings.Default.animationResizeTop));
            }
            else
            {
                control1.Location = new Point(centerX, centerY);
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            GoToLine(int.Parse(textBox1.Text));
        }


        private void GoToLine(int lineNumber)
        {
            lineNumber = lineNumber - 1;
            if (lineNumber >= 0 && lineNumber < CurrentTB.LinesCount)
            {
                FastColoredTextBoxNS.Place startPlace = new FastColoredTextBoxNS.Place(0, lineNumber);
                FastColoredTextBoxNS.Place endPlace = new FastColoredTextBoxNS.Place(0, lineNumber);

                CurrentTB.Selection.Start = startPlace;
                CurrentTB.Selection.End = endPlace;

                CurrentTB.DoSelectionVisible();
                CurrentTB.Focus(); CurrentTB.Select();
                roundedPanel1.Height = 36;
            }
            else
            {
                ToolTip hint = new ToolTip();
                hint.IsBalloon = true;
                hint.ToolTipTitle = "Not a valid line number";
                hint.ToolTipIcon = ToolTipIcon.Error;
                hint.Show(string.Empty, textBox1, 0);
                if (CurrentTB.LinesCount > 1)
                    hint.Show("Enter a number between 1 and " + CurrentTB.LinesCount + ".", textBox1, 15, 17);
                else
                    hint.Show("You have only one line.", textBox1, 15, 17);
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            GoToLine(int.Parse(textBox1.Text));
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            roundedPanel1.Height = 36;
        }

        private void roundedPanel1_VisibleChanged(object sender, EventArgs e)
        {
            roundedPanel1.Height = 36;
        }

        private Point _Offset = Point.Empty;
        private bool goToDockTop = false;
        private bool goToDockBottom = false;

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


        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    //go to line manager
                    if (System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, "[^0-9]"))
                    {
                        ToolTip hint = new ToolTip();
                        hint.IsBalloon = true;
                        hint.ToolTipTitle = "Please enter only numbers";
                        hint.ToolTipIcon = ToolTipIcon.Error;
                        hint.Show(string.Empty, textBox1, -10, -10, 0);
                        hint.Show("Letters and symbols are not allowed.", textBox1);
                        textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
                    }
                    else
                        GoToLine(int.Parse(textBox1.Text));
                }
            }
            catch (Exception ea)
            {

                ToolTip hint = new ToolTip();
                hint.IsBalloon = true;
                hint.ToolTipTitle = "Please enter only numbers";
                hint.ToolTipIcon = ToolTipIcon.Error;
                hint.Show(string.Empty, textBox1, 0);
                if (CurrentTB.LinesCount > 1)
                    hint.Show("Letters and symbols are not allowed.", textBox1, 15, 17);
                else
                    hint.Show("Letters and symbols are not allowed.", textBox1, 15, 17);

            }


        }
        /*
        private void bracketsAlert(TextBox textBox1)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    //go to line manager
                    if (System.Text.RegularExpressions.Regex.IsMatch(textBox1.Text, "[^(][^)][^\\[][^\\]][^\\{][^\\}]]"))
                    {
                        ToolTip hint = new ToolTip();
                        hint.IsBalloon = true;
                        hint.ToolTipTitle = "Please enter only brackets";
                        hint.ToolTipIcon = ToolTipIcon.Error;
                        hint.Show(string.Empty, textBox1, -10, -10, 0);
                        hint.Show("Only brackets are allowed.", textBox1);
                        textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
                    }
                    else
                        GoToLine(int.Parse(textBox1.Text));
                }
            }
            catch (Exception ea)
            {

                ToolTip hint = new ToolTip();
                hint.IsBalloon = true;
                hint.ToolTipTitle = "Please enter only brackeyd";
                hint.ToolTipIcon = ToolTipIcon.Error;
                hint.Show(string.Empty, textBox1, 0);
                if (CurrentTB.LinesCount > 1)
                    hint.Show("Letters and symbols are not allowed.", textBox1, 15, 17);
                else
                    hint.Show("Letters and symbols are not allowed.", textBox1, 15, 17);

            }
        }*/

        private void panel4_MouseMove(object sender, MouseEventArgs e)
        {
            moveGoToPanel = false;
        }

        private void fIndAndReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            check();
            centerPanel(panel6);
            if (!panel6.Visible)
            {
                panel6.Visible = true;
                fIndAndReplaceToolStripMenuItem.Checked = true;
                findreplaceToolStripMenuItem.Checked = true;
            }
            else
            {
                panel6.Visible = false;
                fIndAndReplaceToolStripMenuItem.Checked = false;
                findreplaceToolStripMenuItem.Checked = false;
            }
        }

        private void tabControl1_MouseMove(object sender, MouseEventArgs e)
        {
            mouseMovePanelTopBottom(sender, e, panel6);
        }

        private void tabControl1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseUpPanelTopBottom(sender, e, panel6);
        }

        private void tabControl1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDownPanelTopBottom(sender, e, panel6);
        }

        private bool findTextEdit = false;
        private void comboBox1_Enter(object sender, EventArgs e)
        {
            if (!findTextEdit)
            {
                findTextEdit = true;
                comboBox1.Text = "";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FindNext(comboBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            replaceAll(comboBox1.Text, comboBox3.Text);

        }

        private void replaceAll(string searchText, string replaceText)
        {
            //string searchText = text1;
            //string replaceText = text2;

            RegexOptions options = RegexOptions.None;

            Regex regex = new Regex(searchText, options);

            foreach (Match result in regex.Matches(CurrentTB.Text))
            {
                CurrentTB.Text = CurrentTB.Text.Remove(result.Index, result.Length).Insert(result.Index, replaceText);
            }


        }

        private string ReplaceFirst(string searchText, string replaceText)
        {
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex(searchText, options);

            Match match = regex.Match(CurrentTB.Text);

            if (match.Success)
            {
                string updatedText = regex.Replace(CurrentTB.Text, replaceText, 1);

                CurrentTB.Text = updatedText;
            }

            return CurrentTB.Text;
        }


        private void comboBox1_Leave(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                comboBox1.Text = "Find...";
                findTextEdit = false;
            }
        }

        private void panel6_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDownPanelTopBottom(sender, e, panel6);
            
        }

        private void panel6_MouseMove(object sender, MouseEventArgs e)
        {
            mouseMovePanelTopBottom(sender, e, panel6);
        }

        private void panel6_MouseUp(object sender, MouseEventArgs e)
        {
            mouseUpPanelTopBottom(sender, e, panel6);
        }

        private bool replaceEditText = false;
        private void comboBox3_Enter(object sender, EventArgs e)
        {
            if (!replaceEditText)
            {
                replaceEditText = true;
                comboBox2.Text = "";
            }
        }

        private void comboBox3_Leave(object sender, EventArgs e)
        {
            if (comboBox2.Text == "")
            {
                comboBox2.Text = "Replace...";
                replaceEditText = false;
            }
        }


        private int nextSearchStartIndex;

        private void FindNext(string searchText)
        {
            string pattern = wholeWordsFind ? "\\b" + searchText + "\\b" : searchText;

            RegexOptions options = caseSensitive ? RegexOptions.None : RegexOptions.IgnoreCase;
            Regex regex = new Regex(pattern, options);

            Match match = regex.Match(CurrentTB.Text, this.nextSearchStartIndex);

            if (!match.Success)
            {
                this.nextSearchStartIndex = 0;
                MessageBox.Show("Not Found");
            }
            else
            {
                this.nextSearchStartIndex = match.Index + match.Length;
                CurrentTB.SelectionStart = match.Index;
                CurrentTB.SelectionLength = match.Length;
                int lineIndex = 0;
                for (int i = 0; i < match.Index; i++)
                {
                    if (CurrentTB.Text[i] == '\n')
                    {
                        lineIndex++;
                    }
                }

                // Sposta la selezione per renderla visibile
                CurrentTB.Navigate(lineIndex);
                CurrentTB.DoSelectionVisible();
                CurrentTB.Focus();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ReplaceFirst(comboBox1.Text, comboBox3.Text);
        }

        private void menuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(button5, 15, 15);
        }

        private bool caseSensitive;
        private bool wholeWordsFind;

        private void caseSensitiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (caseSensitive)
            {
                caseSensitiveToolStripMenuItem.Checked = false;
                caseSensitive = false;
            }
            else
            {
                caseSensitiveToolStripMenuItem.Checked = true;
                caseSensitive = true;
            }

        }

        private void findWholeWordsOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (wholeWordsFind)
            {
                findWholeWordsOnlyToolStripMenuItem.Checked = false;
                wholeWordsFind = false;
            }
            else
            {
                findWholeWordsOnlyToolStripMenuItem.Checked = true;
                wholeWordsFind = true;
            }
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CurrentTB.SelectedText = "";
        }

        private void webBrowser2_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void prologToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CurrentTB.Language = FastColoredTextBoxNS.Language.Custom;
            CurrentTB.DescriptionFile = Properties.Settings.Default.descriptionFileDirectory;
            //toolStripStatusLabel1.Text = "Syntax: Prolog";
            CurrentTB.Invalidate();
            CurrentTB.Refresh();
        }

        private void xMLToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CurrentTB.Language = FastColoredTextBoxNS.Language.XML;
            //toolStripStatusLabel1.Text = "Syntax: " + CurrentTB.Language.ToString();
            CurrentTB.Invalidate();
            CurrentTB.Refresh();
        }

        private void cToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.Language = FastColoredTextBoxNS.Language.CSharp;
            //toolStripStatusLabel1.Text = "Syntax: " + CurrentTB.Language.ToString();
            CurrentTB.Invalidate();
            CurrentTB.Refresh();
        }

        private void visualBasicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.Language = FastColoredTextBoxNS.Language.VB;
            //toolStripStatusLabel1.Text = "Syntax: " + CurrentTB.Language.ToString();
            CurrentTB.Invalidate();
            CurrentTB.Refresh();
        }

        private void hTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.Language = FastColoredTextBoxNS.Language.HTML;
            //toolStripStatusLabel1.Text = "Syntax: " + CurrentTB.Language.ToString();
            CurrentTB.Invalidate();
            CurrentTB.Refresh();
        }

        private void sQLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.Language = FastColoredTextBoxNS.Language.SQL;
            //toolStripStatusLabel1.Text = "Syntax: " + CurrentTB.Language.ToString();
            CurrentTB.Invalidate();
            CurrentTB.Refresh();
        }

        private void pHPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.Language = FastColoredTextBoxNS.Language.PHP;
            //toolStripStatusLabel1.Text = "Syntax: " + CurrentTB.Language.ToString();
            CurrentTB.Invalidate();
            CurrentTB.Refresh();
        }

        private void jsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.Language = FastColoredTextBoxNS.Language.JS;
            //toolStripStatusLabel1.Text = "Syntax: " + CurrentTB.Language.ToString();
            CurrentTB.Invalidate();
            CurrentTB.Refresh();
        }

        private void luaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.Language = FastColoredTextBoxNS.Language.Lua;
            //toolStripStatusLabel1.Text = "Syntax: " + CurrentTB.Language.ToString();
            CurrentTB.Invalidate();
            CurrentTB.Refresh();
        }

        private void Fastcolored1_ZoomChanged(object sender, EventArgs e)
        {

        }

        private void CurrentTB_ZoomChanged(object sender, EventArgs e)
        {
            int zoom = 0;
            zoom = int.Parse(CurrentTB.Zoom.ToString());
            btZoom.Text = "Zoom: " + zoom + "%";
            btZoom.Invalidate();
            Properties.Settings.Default.zoomFile = zoom;
            Properties.Settings.Default.Save();

        }

        private void label20_Click(object sender, EventArgs e)
        {
            panel6.Visible = false;
            fIndAndReplaceToolStripMenuItem.Checked = false;
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings form3 = new Settings(webBrowser2, faTabStrip1, faTabStripItem2, splitContainer1, startPageToolStripMenuItem, contextMenuStrip3, searchOnTheInternetToolStripMenuItem, contextMenuStrip2, searchOnTheInternetToolStripMenuItem1, menuStrip1);
            form3.ShowDialog();
        }

        private void faTabStrip1_TabIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (CurrentTB != null)
                {
                    documentMap1.Target = CurrentTB;

                    //toolStripStatusLabel1.Text = "Syntax: " + CurrentTB.Language.ToString();
                    string text = CurrentTB.Text;
                    CurrentTB.Zoom = Properties.Settings.Default.zoomFile;


                    if (faTabStrip1.SelectedItem == faTabStripItem2)
                    {
                        //toolStripStatusLabel1.Visible = false;
                        tabChangeCheck();
                    }
                    else
                    {
                        //toolStripStatusLabel1.Visible = true;
                        tabChangeCheck();
                    }

                    toolStripTextBox4.Text = faTabStrip1.SelectedItem.Title;

                }
            }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.ToString());
            }

            noFileOpenVisible();
        }

        private void faTabStrip1_Click(object sender, EventArgs e)
        {
            tabChangeCheck();
            noFileOpenVisible();
        }

        private void tabChangeCheck()
        {
            try
            {
                if (CurrentTB != null)
                {
                    documentMap1.Target = CurrentTB;

                    //toolStripStatusLabel1.Text = "Syntax: " + CurrentTB.Language.ToString();
                    string text = CurrentTB.Text;
                    CurrentTB.Zoom = Properties.Settings.Default.zoomFile;

                    CurrentTB.Focus();
                }

                if (faTabStrip1.SelectedItem == faTabStripItem2 || faTabStrip1.Items.Count == 0) //start page
                {
                    //Fluent
                    toolStripLabel2.Enabled = false;
                    toolStripLabel8.Enabled = false;
                    toolStripLabel7.Enabled = false;
                    toolStripLabel6.Enabled = false;
                    toolStripLabel5.Enabled = false;
                    toolStripLabel4.Enabled = false;
                    toolStripLabel9.Enabled = false;
                    toolStripLabel11.Enabled = false;
                    toolStripLabel11.Enabled = false;
                    toolStripLabel1.Enabled = false;
                    toolStripLabel10.Enabled = false;

                    //Aero
                    toolStripButton2.Enabled = false;
                    toolStripButton4.Enabled = false;
                    toolStripButton5.Enabled = false;
                    toolStripButton6.Enabled = false;
                    toolStripButton7.Enabled = false;
                    toolStripButton8.Enabled = false;
                    toolStripButton9.Enabled = false;
                    toolStripButton10.Enabled = false;
                    toolStripButton11.Enabled = false;
                    toolStripButton12.Enabled = false;

                    //Classic
                    toolStripButton14.Enabled = false;
                    toolStripButton16.Enabled = false;
                    toolStripButton17.Enabled = false;
                    toolStripButton18.Enabled = false;
                    toolStripButton19.Enabled = false;
                    toolStripButton20.Enabled = false;
                    toolStripButton21.Enabled = false;
                    toolStripButton22.Enabled = false;
                    toolStripButton23.Enabled = false;
                    toolStripButton24.Enabled = false;

                    //OS 9
                    toolStripButton26.Enabled = false;
                    toolStripButton28.Enabled = false;
                    toolStripButton29.Enabled = false;
                    toolStripButton30.Enabled = false;
                    toolStripButton31.Enabled = false;
                    toolStripButton32.Enabled = false;
                    toolStripButton33.Enabled = false;
                    toolStripButton34.Enabled = false;
                    toolStripButton35.Enabled = false;
                    toolStripButton36.Enabled = false;

                    //menu bar
                    saveToolStripMenuItem.Enabled = false;
                    saveAsToolStripMenuItem.Enabled = false;
                    sendToToolStripMenuItem.Enabled = false;
                    printToolStripMenuItem.Enabled = false;

                    editToolStripMenuItem.Enabled = false;
                    documentMapToolStripMenuItem.Enabled = false;
                    splitContainer2.Panel2Collapsed = true;
                    syntaxToolStripMenuItem.Enabled = false;
                    rowsToolStripMenuItem.Enabled = false;
                    dataToolStripMenuItem.Enabled = false;
                    formatToolStripMenuItem.Enabled = false;
                    toolStripStatusLabel2.Visible = false;
                    documentMap1.Visible = false;

                    //text properties
                    trackBar1.Enabled = false;
                    trackBar2.Enabled = false;
                }
                else //any other page that's not the start page
                {
                    //toolStripStatusLabel1.Visible = true;
                    CurrentTB.Focus();
                    //Fluent
                    toolStripLabel2.Enabled = true;
                    toolStripLabel8.Enabled = true;
                    toolStripLabel7.Enabled = true;
                    toolStripLabel6.Enabled = true;
                    toolStripLabel5.Enabled = true;
                    toolStripLabel4.Enabled = true;
                    toolStripLabel9.Enabled = true;
                    toolStripLabel11.Enabled = true;
                    toolStripLabel11.Enabled = true;
                    toolStripLabel1.Enabled = true;
                    toolStripLabel10.Enabled = true;

                    //Aero
                    toolStripButton2.Enabled = true;
                    toolStripButton4.Enabled = true;
                    toolStripButton5.Enabled = true;
                    toolStripButton6.Enabled = true;
                    toolStripButton7.Enabled = true;
                    toolStripButton8.Enabled = true;
                    toolStripButton9.Enabled = true;
                    toolStripButton10.Enabled = true;
                    toolStripButton11.Enabled = true;
                    toolStripButton12.Enabled = true;

                    //Classic
                    toolStripButton14.Enabled = true;
                    toolStripButton16.Enabled = true;
                    toolStripButton17.Enabled = true;
                    toolStripButton18.Enabled = true;
                    toolStripButton19.Enabled = true;
                    toolStripButton20.Enabled = true;
                    toolStripButton21.Enabled = true;
                    toolStripButton22.Enabled = true;
                    toolStripButton23.Enabled = true;
                    toolStripButton24.Enabled = true;

                    //OS 9
                    toolStripButton26.Enabled = true;
                    toolStripButton28.Enabled = true;
                    toolStripButton29.Enabled = true;
                    toolStripButton30.Enabled = true;
                    toolStripButton31.Enabled = true;
                    toolStripButton32.Enabled = true;
                    toolStripButton33.Enabled = true;
                    toolStripButton34.Enabled = true;
                    toolStripButton35.Enabled = true;
                    toolStripButton36.Enabled = true;

                    //text properties
                    trackBar1.Value = CurrentTB.LineInterval;
                    trackBar1.Enabled = true;
                    trackBar2.Enabled = true;

                    //menu bar
                    saveToolStripMenuItem.Enabled = true;
                    saveAsToolStripMenuItem.Enabled = true;
                    sendToToolStripMenuItem.Enabled = true;
                    printToolStripMenuItem.Enabled = true;

                    editToolStripMenuItem.Enabled = true;
                    documentMapToolStripMenuItem.Enabled = true;
                    syntaxToolStripMenuItem.Enabled = true;
                    rowsToolStripMenuItem.Enabled = true;
                    dataToolStripMenuItem.Enabled = true;
                    formatToolStripMenuItem.Enabled = true;
                    if (Properties.Settings.Default.LineColumnView)
                        toolStripStatusLabel2.Visible = true;
                    getCurrentPosition();

                    if (Properties.Settings.Default.documentMap)
                    {
                        documentMap1.Visible = true;
                        splitContainer2.Panel2Collapsed = false;
                    }

                    //text properties
                    if (CurrentTB.ShowLineNumbers == true)
                        rowsToolStripMenuItem.Checked = true;
                    else
                        rowsToolStripMenuItem.Checked = false;

                    trackBar2.Value = (int)CurrentTB.LineNumberStartValue;
                    trackBar2.Maximum = CurrentTB.LinesCount;

                    trackBar3.Value = (int)CurrentTB.TabLength;

                    if (CurrentTB.WordWrapMode == FastColoredTextBoxNS.WordWrapMode.CharWrapControlWidth)
                    {
                        comboBox4.SelectedIndex = 0;
                        label62.Text = "Your document will wrap words based on the single characters of them.";
                    }
                    else if (CurrentTB.WordWrapMode == FastColoredTextBoxNS.WordWrapMode.WordWrapControlWidth)
                    {
                        comboBox4.SelectedIndex = 1;
                        label62.Text = "Your document will wrap words entirely, making the entire document more readable.";
                    }

                    if (CurrentTB.WideCaret)
                    {
                        checkBox10.Checked = true;
                        label61.Text = "The caret used on your document will be a wider one, more akin to old terminals and editors.";
                    }
                    else
                    {
                        checkBox10.Checked = false;
                        label61.Text = "The caret used on your document will be the default Windows one.";
                    }
                    if (CurrentTB.ReadOnly)
                        checkBox9.Checked = true;
                    else
                        checkBox9.Checked = false;

                    if (CurrentTB.ShowScrollBars)
                        checkBox7.Checked = true;
                    else
                        checkBox7.Checked = false;

                    if (CurrentTB.AutoIndent){
                        checkBox4.Checked = true;
                        checkBox6.Enabled = true;
                    } else {
                        checkBox4.Checked = false;
                        checkBox6.Enabled = false;
                    }

                    if (CurrentTB.AutoIndentChars)
                        checkBox5.Checked = true;
                    else
                        checkBox5.Checked = false;

                    textBox8.Text = CurrentTB.AutoIndentCharsPatterns;

                    if (CurrentTB.AutoIndentExistingLines)
                        checkBox6.Checked = true;
                    else
                        checkBox6.Checked = false;

                    if (CurrentTB.AutoCompleteBrackets)
                        checkBox1.Checked = true;
                    else
                        checkBox1.Checked = false;

                    textBox2.Text = CurrentTB.LeftBracket.ToString(); textBox5.Text = CurrentTB.LeftBracket2.ToString(); textBox3.Text = CurrentTB.RightBracket.ToString(); textBox4.Text = CurrentTB.RightBracket2.ToString();
                }


            }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.ToString());
            }



        }

        public void ListBrackets(CheckedListBox lstview)
        {
            lstview.Items.Clear();
            for (int i = 0; i < CurrentTB.AutoCompleteBracketsList.Length; i++)
            {
                lstview.Items.Add(CurrentTB.AutoCompleteBracketsList[i].ToString());
            }
        }


        public void clickToolbar(ToolStripLabel label, int leftog, int rightog, int left, int right)
        {
            label.Padding = new Padding(leftog + left, label.Padding.Top, rightog + right, label.Padding.Bottom);
            label.Font = new Font(label.Font.FontFamily, label.Font.Size - 3, label.Font.Style);
        }

        public void clickButtonText(Label label)
        {
        }

        public void clickToolbarUndo(ToolStripLabel label, int leftog, int rightog)
        {
            label.Padding = new Padding(leftog, label.Padding.Top, rightog, label.Padding.Bottom);
            label.Font = new Font(label.Font.FontFamily, label.Font.Size + 3, label.Font.Style);
        }

        public void checkPadding()
        {
            leftNew = toolStripLabel2.Padding.Left;
            rightNew = toolStripLabel2.Padding.Right;

            leftTab = toolStripLabel12.Padding.Left;
            rightTab = toolStripLabel12.Padding.Right;

            leftOpen = toolStripLabel3.Padding.Left;
            rightOpen = toolStripLabel3.Padding.Right;

            leftSave = toolStripLabel8.Padding.Left;
            rightSave = toolStripLabel8.Padding.Right;

            leftCut = toolStripLabel7.Padding.Left;
            rightCut = toolStripLabel7.Padding.Right;

            leftCopy = toolStripLabel6.Padding.Left;
            rightCopy = toolStripLabel6.Padding.Right;

            leftPaste = toolStripLabel5.Padding.Left;
            rightPaste = toolStripLabel5.Padding.Right;

            leftUndo = toolStripLabel4.Padding.Left;
            rightUndo = toolStripLabel4.Padding.Right;

            leftFont = toolStripLabel11.Padding.Left;
            rightFont = toolStripLabel11.Padding.Right;

            leftNav = toolStripLabel1.Padding.Left;
            rightNav = toolStripLabel1.Padding.Right;

            leftNavNav = toolStripLabel10.Padding.Left;
            rightNavNav = toolStripLabel10.Padding.Right;

        }

        private void toolStripLabel2_MouseDown(object sender, MouseEventArgs e)
        {
            clickToolbar(toolStripLabel2, leftNew, rightNew, Properties.Settings.Default.leftPaddingToolbar, Properties.Settings.Default.rightPaddingToolbar);
            newClicked = true;
        }

        private void toolStripLabel2_MouseUp(object sender, MouseEventArgs e)
        {
            clickToolbarUndo(toolStripLabel2, leftNew, rightNew);
            newClicked = false;
        }

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

        private void menuStrip1_MouseUp(object sender, MouseEventArgs e)
        {

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
        private bool newClicked; private bool tabClicked; private bool openClicked; private bool saveClicked; private bool cutClicked; private bool copyClicked; private bool pasteClicked; private bool undoClicked; private bool redoClicked; private bool fontClicked; private bool backClicked; private bool nextClicked;
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
        }

        private void noFileOpenVisible()
        {
            if (faTabStrip1.Items.Count == 0)
                noOpenedFileGroup.Visible = true;
            else
                noOpenedFileGroup.Visible = false;
        }

        private void faTabStrip1_TabStripItemClosed(object sender, EventArgs e)
        {
            noFileOpenVisible(); tabChangeCheck();
        }

        private void toolStripLabel2_MouseUp(object sender, EventArgs e)
        {
            if (newClicked)
            {
                clickToolbarUndo(toolStripLabel2, leftNew, rightNew);
                newClicked = false;
            }
        }

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
        /*
        private void toolStrip1_Paint(object sender, PaintEventArgs e)
        {
            string version = GetWindowsVersion();
            if (version == "6.0" || version == "6.1")
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(220, 230, 244)), 0, 10, toolStrip1.Width, toolStrip1.Height);
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(238, 246, 253)), 0, -10, toolStrip1.Width, toolStrip1.Height);
            }
        }

        private void menuStrip1_Paint(object sender, PaintEventArgs e)
        {
            string version = GetWindowsVersion();
            if (version == "6.0" || version == "6.1")
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(238,246,253)), 0, 0, menuStrip1.Width, menuStrip1.Height);
            }
        }
        */
        private void lunaToolbar()
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            lunaToolbar();
        }

        private void toolStripButton13_MouseEnter(object sender, EventArgs e)
        {
            toolStripButton13.Image = Properties.Resources.new2000;
        }

        private void toolStripButton13_MouseLeave(object sender, EventArgs e)
        {
            toolStripButton13.Image = Properties.Resources.newB;
        }

        private void toolStripButton14_MouseEnter(object sender, EventArgs e)
        {
            toolStripButton14.Image = Properties.Resources.tab2000;
        }

        private void toolStripButton14_MouseLeave(object sender, EventArgs e)
        {
            toolStripButton14.Image = Properties.Resources.tabB;
        }

        private void toolStripButton15_MouseEnter(object sender, EventArgs e)
        {
            toolStripButton15.Image = Properties.Resources.open2000;
        }

        private void toolStripButton15_MouseLeave(object sender, EventArgs e)
        {
            toolStripButton15.Image = Properties.Resources.openB;
        }

        private void toolStripButton16_MouseEnter(object sender, EventArgs e)
        {
            toolStripButton16.Image = Properties.Resources.save2000;
        }

        private void toolStripButton16_MouseLeave(object sender, EventArgs e)
        {
            toolStripButton16.Image = Properties.Resources.saveB;
        }

        private void toolStripButton17_MouseEnter(object sender, EventArgs e)
        {
            toolStripButton17.Image = Properties.Resources.cut2000;
        }

        private void toolStripButton17_MouseLeave(object sender, EventArgs e)
        {
            toolStripButton17.Image = Properties.Resources.cutB;
        }

        private void toolStripButton18_MouseEnter(object sender, EventArgs e)
        {
            toolStripButton18.Image = Properties.Resources.copy2000;
        }

        private void toolStripButton19_MouseEnter(object sender, EventArgs e)
        {
            toolStripButton19.Image = Properties.Resources.paste2000;
        }

        private void toolStripButton18_MouseLeave(object sender, EventArgs e)
        {
            toolStripButton18.Image = Properties.Resources.copyB;
        }

        private void toolStripButton19_MouseLeave(object sender, EventArgs e)
        {
            toolStripButton19.Image = Properties.Resources.pasteB;
        }

        private void toolStripButton20_MouseEnter(object sender, EventArgs e)
        {
            toolStripButton20.Image = Properties.Resources.undo2000;
        }

        private void toolStripButton20_MouseLeave(object sender, EventArgs e)
        {
            toolStripButton20.Image = Properties.Resources.undoB;
        }

        private void toolStripButton21_MouseEnter(object sender, EventArgs e)
        {
            toolStripButton21.Image = Properties.Resources.redo2000;
        }

        private void toolStripButton21_MouseLeave(object sender, EventArgs e)
        {
            toolStripButton21.Image = Properties.Resources.redoB;
        }

        private void toolStripButton22_MouseEnter(object sender, EventArgs e)
        {
            toolStripButton22.Image = Properties.Resources.Font2000;
        }

        private void toolStripButton22_MouseLeave(object sender, EventArgs e)
        {
            toolStripButton22.Image = Properties.Resources.fontB;
        }

        private void toolStripButton23_MouseEnter(object sender, EventArgs e)
        {
            toolStripButton23.Image = Properties.Resources.back2000;
        }

        private void toolStripButton23_MouseLeave(object sender, EventArgs e)
        {
            toolStripButton23.Image = Properties.Resources.bacjB;
        }

        private void toolStripButton24_MouseEnter(object sender, EventArgs e)
        {
            toolStripButton24.Image = Properties.Resources.next2000;
        }

        private void toolStripButton24_MouseLeave(object sender, EventArgs e)
        {
            toolStripButton24.Image = Properties.Resources.nextB;
        }

        int currentLine; int currentColumn; int finalColumn;

        private void getCurrentPosition()
        {
            currentLine = CurrentTB.Selection.Start.iLine + 1; // Aggiungi 1 perché iLine è zero-based
            currentColumn = CurrentTB.Selection.Start.iChar + 1; // Aggiungi 1 perché iChar è zero-based
            if (currentColumn > Properties.Settings.Default.columnLineLimit && Properties.Settings.Default.antoniottiStandard && !Properties.Settings.Default.antoniottiCrazy)
            {
                toolStripStatusLabel.ForeColor = Color.Red;
                toolStripStatusLabel.Font = new Font(toolStripStatusLabel.Font, FontStyle.Bold);
                toolStripStatusLabel.Text = Properties.Settings.Default.antoniottiStandardText;
            }
            else if (currentColumn > Properties.Settings.Default.columnLineLimit && Properties.Settings.Default.antoniottiCrazy && !Properties.Settings.Default.antoniottiStandard)
            {
                toolStripStatusLabel.ForeColor = Color.Red;
                toolStripStatusLabel.Font = new Font(toolStripStatusLabel.Font, FontStyle.Bold);
                antoniotti80Panel.Visible = true;
                label27.Text = Properties.Settings.Default.antoniottiCrazyTitle;
                label28.Text = Properties.Settings.Default.antoniottiCrazyTextDuo;
                toolStripStatusLabel.Text = Properties.Settings.Default.antoniottiCrazyText;
            }
            else if (currentColumn <= Properties.Settings.Default.columnLineLimit || !Properties.Settings.Default.antoniottiStandard && !Properties.Settings.Default.antoniottiCrazy)
            {
                toolStripStatusLabel.ForeColor = SystemColors.ControlText;
                toolStripStatusLabel.Font = new Font(toolStripStatusLabel.Font, FontStyle.Regular);
                antoniotti80Panel.Visible = false;
                toolStripStatusLabel.Text = "Ready";
            }

        }
        private void CurrentTB_SelectionChanged(object sender, EventArgs e)
        {
            if (CurrentTB != null)
            {
                getCurrentPosition();
                toolStripStatusLabel2.Text = "Line " + currentLine + ", column " + currentColumn;
            }
        }

        private void lineAndColumnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lineAndColumnToolStripMenuItem.Checked)
            {
                toolStripStatusLabel2.Visible = false;
                lineAndColumnToolStripMenuItem.Checked = false;
                Properties.Settings.Default.LineColumnView = false;
            }
            else
            {
                if (faTabStrip1.SelectedItem != faTabStripItem2)
                {
                    toolStripStatusLabel2.Visible = true;
                    lineAndColumnToolStripMenuItem.Checked = true;
                    Properties.Settings.Default.LineColumnView = true;
                }

            }


            Properties.Settings.Default.Save();
        }

        private void addANewTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateTab(null);
        }

        private void closeCurrentTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveQuestion();
            faTabStrip1.RemoveTab(faTabStrip1.SelectedItem);
        }

        private void startPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            faTabStrip1.AddTab(faTabStripItem2);
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void prologToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (prologToolStripMenuItem.Checked)
            {
                prologToolStripMenuItem.Checked = false;
                noneToolStripMenuItem.Checked = true;
                lispToolStripMenuItem.Checked = false;
                yaccJToolStripMenuItem.Checked = false;
                jFlexToolStripMenuItem.Checked = false;
                Properties.Settings.Default.syntaxChosen = "None";
                CurrentTB.Language = FastColoredTextBoxNS.Language.Custom;
                CurrentTB.DescriptionFile = null;
                CurrentTB.Text = CurrentTB.Text + "";
            }
            else
            {
                prologToolStripMenuItem.Checked = true;
                noneToolStripMenuItem.Checked = false;
                lispToolStripMenuItem.Checked = false;
                yaccJToolStripMenuItem.Checked = false;
                jFlexToolStripMenuItem.Checked = false;
                if (Properties.Settings.Default.syntaxChosen == "Lisp" || Properties.Settings.Default.syntaxChosen == "jflex" || Properties.Settings.Default.syntaxChosen == "yacc")
                {
                    Properties.Settings.Default.syntaxChosen = "None";
                }
                Properties.Settings.Default.syntaxChosen = "Prolog";
                CurrentTB.Text = CurrentTB.Text + "";
            }

            Properties.Settings.Default.Save();
        }

        public void cleanStyles(FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            e.ChangedRange.ClearStyle(CurrentTB.Styles);
        }

        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!noneToolStripMenuItem.Checked)
            {
                Properties.Settings.Default.syntaxChosen = "None";
                CurrentTB.Text = CurrentTB.Text + "";
                prologToolStripMenuItem.Checked = false;
                noneToolStripMenuItem.Checked = true;
                lispToolStripMenuItem.Checked = false;
                yaccJToolStripMenuItem.Checked = false;
                jFlexToolStripMenuItem.Checked = false;
            }

            Properties.Settings.Default.Save();
        }

        private void applyLispSyntax(FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            try
            {
                CurrentTB.Range.tb.ClearStylesBuffer();


                e.ChangedRange.SetStyle(lispComment, @";.*");  // Stile per i commenti

                e.ChangedRange.SetStyle(lispKeyword, @"\b(if|defun|setq)\b");  // Stile per le parole chiave di LISP
                e.ChangedRange.SetStyle(lispOperator, @"[\+\-\*\/]");  // Stile per gli operatori
                e.ChangedRange.SetStyle(lispSpecialChar, @"[\$\%\!]");  // Stile per i caratteri speciali
                e.ChangedRange.SetStyle(lispString, "\"[^\"]*\"");
                e.ChangedRange.SetStyle(lispNumber, @"\b\d+(\.\d+)?\b");
                e.ChangedRange.SetStyle(lispVariable, @"\b[a-zA-Z][-a-zA-Z0-9]*\b");  // Stile per le variabili
                e.ChangedRange.SetStyle(lispBrackets, @"[\(\)]");  // Stile per le parentesi tonde
                CurrentTB.Refresh();
            }
            catch (Exception ex2)
            {
                toolStripStatusLabel.Text = "Exceeded LISP!";
            }

        }

        private void applyYaccSyntax(FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            try
            {
                CurrentTB.Range.tb.ClearStylesBuffer();



                e.ChangedRange.SetStyle(yaccSection, @"%%\n*");  // Stile per i commenti
                e.ChangedRange.SetStyle(yaccBrackets, @"\((?:(?![{}[\]])[^\n])*\)|\[(?:(?![{}[\]])[^\n])*\]"); //stile per le parentesi
                e.ChangedRange.SetStyle(yaccNumber, @"[0-9]+");
                e.ChangedRange.SetStyle(yaccToken, @"(?<!(?s:\x22[^\x22]*?))\b[A-Z]+\b");
                e.ChangedRange.SetStyle(yaccPerc, @"%");
                e.ChangedRange.SetStyle(yaccInclusive, @"\<[^\<\>]*\>"); //inclusive ed exclusive
                e.ChangedRange.SetStyle(yaccOperator, @"([-\[\]{}()*+_?.,\\\/^$|#])");  // Stile per gli operatori, (\\a)(\\b)(\\d)(\\D)(\\s)(\\S)(\\w)(\\W)(\\b)(\\B)
                e.ChangedRange.SetStyle(jflexJava, @"(?<!(?s:\x22[^\x22]*?))\b(public|private|void|int|StringBuffer|static|double|final|protected|class|input|yyerror|yylval|Parser|Val|new|System|yyparse|return|if|else|;|null|String|class|unicode|Symbol|return|yyline|yycolumn|Object|Identifier|state|toString|throw|Error|yytext|out|print|println|printf|abstract|add|alias|as|ascending|async|await|base|bool|break|by|byte|case|catch|char|checked|class|const|continue|decimal|default|delegate|descending|do|double|dynamic|else|enum|equals|event|explicit|extern|false|finally|fixed|float|for|foreach|from|get|global|goto|group|if|implicit|in|int|interface|internal|into|is|join|let|lock|long|nameof|namespace|new|null|object|on|operator|orderby|out|override|params|partial|private|protected|public|readonly|ref|remove|return|sbyte|sealed|select|set|short|sizeof|stackalloc|static|static|string|struct|switch|this|throw|true|try|typeof|uint|ulong|unchecked|unsafe|ushort|using|using|value|var|virtual|void|volatile|when|where|while|yield|import|java|IO|IOException|out|println|print|Parser|new|InputStreamReader|Yylex|String)\b|#region\b|#endregion\b");

                /*
                Regex yaccStringRegex = new Regex(
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
                e.ChangedRange.SetStyle(yaccString, yaccStringRegex);
                 * */

                CurrentTB.Refresh();
            }
            catch (Exception ex2)
            {
                toolStripStatusLabel.Text = "Exceeded yacc!";
            }

        }

        private void applyJFlexSyntax(FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            try
            {
                CurrentTB.Range.tb.ClearStylesBuffer();


                e.ChangedRange.SetStyle(jflexSection, @"%%\n*");  // Stile per i commenti


                e.ChangedRange.SetStyle(jflexComment, @"//.*$", RegexOptions.Multiline);
                e.ChangedRange.SetStyle(jflexComment, @"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline);
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
                CurrentTB.Refresh();
            }
            catch (Exception ex2)
            {
                toolStripStatusLabel.Text = "Exceeded jflex!";
            }

        }

        private void lispToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lispToolStripMenuItem.Checked)
            {
                prologToolStripMenuItem.Checked = false;
                noneToolStripMenuItem.Checked = true;
                lispToolStripMenuItem.Checked = false;
                yaccJToolStripMenuItem.Checked = false;
                jFlexToolStripMenuItem.Checked = false;
                Properties.Settings.Default.syntaxChosen = "None";
                CurrentTB.Language = FastColoredTextBoxNS.Language.Custom;
                CurrentTB.DescriptionFile = null;
                //CurrentTB.Text = CurrentTB.Text + "";

            }
            else
            {
                prologToolStripMenuItem.Checked = false;
                noneToolStripMenuItem.Checked = false;
                lispToolStripMenuItem.Checked = true;
                yaccJToolStripMenuItem.Checked = false;
                jFlexToolStripMenuItem.Checked = false;
                if (Properties.Settings.Default.syntaxChosen == "Prolog" || Properties.Settings.Default.syntaxChosen == "jflex" || Properties.Settings.Default.syntaxChosen == "yacc")
                {
                    Properties.Settings.Default.syntaxChosen = "None";
                }
                Properties.Settings.Default.syntaxChosen = "Lisp";
                //CurrentTB.Text = CurrentTB.Text + "";
            }

            Properties.Settings.Default.Save();
        }

        private void label36_Click(object sender, EventArgs e)
        {

        }

        private void columnsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!columnsToolStripMenuItem.Checked)
            {
                columnsToolStripMenuItem.Checked = true;
                Properties.Settings.Default.antoniottiStandard = true;
                Properties.Settings.Default.antoniottiCrazy = false;
                crazy80ToolStripMenuItem.Checked = false;
                noLimitToolStripMenuItem.Checked = false;
            }
            else if (columnsToolStripMenuItem.Checked)
            {
                columnsToolStripMenuItem.Checked = false;
                noLimitToolStripMenuItem.Checked = true;
                crazy80ToolStripMenuItem.Checked = false;
                Properties.Settings.Default.antoniottiCrazy = false;
                Properties.Settings.Default.antoniottiStandard = false;
            }
            antoniotti80Panel.Visible = false;
            check(); getCurrentPosition();
            Properties.Settings.Default.Save();
        }

        private void crazy80ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!crazy80ToolStripMenuItem.Checked)
            {
                columnsToolStripMenuItem.Checked = false;
                Properties.Settings.Default.antoniottiStandard = false;
                Properties.Settings.Default.antoniottiCrazy = true;
                crazy80ToolStripMenuItem.Checked = true;
                noLimitToolStripMenuItem.Checked = false;
            }
            else if (crazy80ToolStripMenuItem.Checked)
            {
                columnsToolStripMenuItem.Checked = false;
                noLimitToolStripMenuItem.Checked = true;

                crazy80ToolStripMenuItem.Checked = false;
                Properties.Settings.Default.antoniottiCrazy = false;
                Properties.Settings.Default.antoniottiStandard = false;
            }
            check(); getCurrentPosition();
            Properties.Settings.Default.Save();
        }

        private void noLimitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            columnsToolStripMenuItem.Checked = false;
            noLimitToolStripMenuItem.Checked = true;
            crazy80ToolStripMenuItem.Checked = false;
            Properties.Settings.Default.antoniottiCrazy = false;
            Properties.Settings.Default.antoniottiStandard = false;
            Properties.Settings.Default.Save();
            antoniotti80Panel.Visible = false;
            check(); getCurrentPosition();
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            faTabStrip1.SelectedItem.Title = toolStripTextBox4.Text;
        }

        private void toolStripTextBox4_Click_1(object sender, EventArgs e)
        {
            toolStripTextBox4.SelectAll();
        }

        private void yaccJToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (yaccJToolStripMenuItem.Checked)
            {
                prologToolStripMenuItem.Checked = false;
                noneToolStripMenuItem.Checked = true;
                lispToolStripMenuItem.Checked = false;
                yaccJToolStripMenuItem.Checked = false;
                jFlexToolStripMenuItem.Checked = false;
                Properties.Settings.Default.syntaxChosen = "None";
                CurrentTB.Language = FastColoredTextBoxNS.Language.Custom;
                CurrentTB.DescriptionFile = null;
                CurrentTB.Text = CurrentTB.Text + "";
            }
            else
            {
                prologToolStripMenuItem.Checked = false;
                noneToolStripMenuItem.Checked = false;
                lispToolStripMenuItem.Checked = false;
                yaccJToolStripMenuItem.Checked = true;
                jFlexToolStripMenuItem.Checked = false;
                if (Properties.Settings.Default.syntaxChosen == "Lisp" || Properties.Settings.Default.syntaxChosen == "Prolog" || Properties.Settings.Default.syntaxChosen == "jflex")
                {
                    Properties.Settings.Default.syntaxChosen = "None";
                }
                Properties.Settings.Default.syntaxChosen = "yacc";
                CurrentTB.Text = CurrentTB.Text + "";
            }

            Properties.Settings.Default.Save();
        }

        private void jFlexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (jFlexToolStripMenuItem.Checked)
            {
                prologToolStripMenuItem.Checked = false;
                noneToolStripMenuItem.Checked = true;
                lispToolStripMenuItem.Checked = false;
                yaccJToolStripMenuItem.Checked = false;
                jFlexToolStripMenuItem.Checked = false;
                Properties.Settings.Default.syntaxChosen = "None";
                CurrentTB.Language = FastColoredTextBoxNS.Language.Custom;
                CurrentTB.DescriptionFile = null;
                CurrentTB.Text = CurrentTB.Text + "";
            }
            else
            {
                prologToolStripMenuItem.Checked = false;
                noneToolStripMenuItem.Checked = false;
                lispToolStripMenuItem.Checked = false;
                yaccJToolStripMenuItem.Checked = false;
                jFlexToolStripMenuItem.Checked = true;
                if (Properties.Settings.Default.syntaxChosen == "Lisp" || Properties.Settings.Default.syntaxChosen == "Prolog" || Properties.Settings.Default.syntaxChosen == "yacc")
                {
                    Properties.Settings.Default.syntaxChosen = "None";
                }
                Properties.Settings.Default.syntaxChosen = "jflex";
                CurrentTB.Text = CurrentTB.Text + "";
            }

            Properties.Settings.Default.Save();
        }

        private void logixHelp()
        {
            string curDir = Directory.GetCurrentDirectory(); //get the current app directory
            string filePath = String.Format("file:///{0}/" + Properties.Settings.Default.chmFile, curDir); //modify description file of the fastcolored textbox
            try
            {
                // Start the default application for the file
                Process.Start(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        private void quickPrologGuideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logixHelp();
        }

        public void webSearch()
        {
            try
            {
                String searchRequest = CurrentTB.SelectedText;

                if (Properties.Settings.Default.searchWebAuto)
                {
                    switch (Properties.Settings.Default.syntaxChosen)
                    {
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
                    }

                    Properties.Settings.Default.Save();
                }
                System.Diagnostics.Process.Start(Properties.Settings.Default.searchWebPath + System.Uri.EscapeDataString(searchRequest));
            }
            catch (Exception e4)
            {
            }
        }
        private void searchOnTheInternetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webSearch();
        }

        private void stayOnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (stayOnTopToolStripMenuItem.Checked)
            {
                stayOnTopToolStripMenuItem.Checked = false;
                Properties.Settings.Default.windowOnTop = false;
                this.TopMost = false;
            }
            else
            {
                stayOnTopToolStripMenuItem.Checked = true;
                Properties.Settings.Default.windowOnTop = true;
                this.TopMost = true;
            }
            Properties.Settings.Default.Save();
        }

        private void newWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(OpenForm);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        public static void OpenForm()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MDIParent1());
        }

        public void windowState()
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.windowState = "Normal";
            }
            else if (this.WindowState == FormWindowState.Maximized)
            {
                Properties.Settings.Default.windowState = "Maximized";
            }
            else if (this.WindowState == FormWindowState.Minimized)
            {
                Properties.Settings.Default.windowState = "Normal";
            }
            Properties.Settings.Default.Save();
        }

        public void setWindowState()
        {
            switch (Properties.Settings.Default.windowState)
            {
                case "Normal":
                    this.WindowState = FormWindowState.Normal;
                    break;

                case "Maximized":
                    this.WindowState = FormWindowState.Maximized;
                    break;
            }
        }

        public void setWindowPosition()
        {
            this.Location = new Point(Properties.Settings.Default.windowLeft, Properties.Settings.Default.windowTop);
        }

        public void getWindowPosition()
        {
            Properties.Settings.Default.windowLeft = this.Left;
            Properties.Settings.Default.windowTop = this.Top;
            Properties.Settings.Default.Save();
        }

        public void setWindowSize()
        {
            this.Size = new Size(Properties.Settings.Default.windowWidth, Properties.Settings.Default.windowHeight);
        }

        public void getWindowSize()
        {
            Properties.Settings.Default.windowWidth = this.Width;
            Properties.Settings.Default.windowHeight = this.Height;
            Properties.Settings.Default.Save();
        }

        public void setStartWindowPlace()
        {
            switch (Properties.Settings.Default.startWindowPlace)
            {
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
                    break;
            }
        }

       

        private void rowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rowsToolStripMenuItem.Checked)
            {
                CurrentTB.ShowLineNumbers = false;
                rowsToolStripMenuItem.Checked = false;
            }
            else
            {
                CurrentTB.ShowLineNumbers = true;
                rowsToolStripMenuItem.Checked = true;
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            CurrentTB.LineInterval = trackBar1.Value;
            ToolTip.Show("Line interval: " + trackBar1.Value.ToString(), trackBar1, 1000);
        }

        private void trackBar1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            CurrentTB.LineNumberStartValue = (uint)trackBar2.Value;
            ToolTip.Show("Starting number: " + trackBar2.Value.ToString(), trackBar2);
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            CurrentTB.TabLength = trackBar3.Value;
            ToolTip.Show("Tab length: " + trackBar3.Value.ToString(), trackBar3);
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox4.Text)
            {
                case "By word":
                    CurrentTB.WordWrapMode = FastColoredTextBoxNS.WordWrapMode.WordWrapControlWidth;
                    label62.Text = "Your document will have words wrapped entirely, making the entire document more readable.";
                    break;

                case "By character":
                    CurrentTB.WordWrapMode = FastColoredTextBoxNS.WordWrapMode.CharWrapControlWidth;
                    label62.Text = "Your document will have words wrapped based on the single characters of them.";
                    break;
            }
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox10.Checked)
            {
                CurrentTB.WideCaret = false;
                label61.Text = "The caret used on your document will be the default Windows one.";
            }
            else
            {
                CurrentTB.WideCaret = true;
                label61.Text = "The caret used on your document will be a wider one, more akin to old terminals and editors.";
            }
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox9.Checked)
            {
                CurrentTB.ReadOnly = true;
            }
            else
            {
                CurrentTB.ReadOnly = false;
            }
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked)
                CurrentTB.ShowScrollBars = true;
            else
                CurrentTB.ShowScrollBars = false;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked){
                CurrentTB.AutoIndent = true;
                      checkBox6.Enabled = true;
            } else{
                CurrentTB.AutoIndent = false;
                checkBox6.Enabled = false;
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked){
                CurrentTB.AutoIndentChars = true;
          
            }else{
            }   CurrentTB.AutoIndentChars = false;
        }

        private void textPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textPropertiesToolStripMenuItem.Checked)
            {
                panel8.Visible = false;
                textPropertiesToolStripMenuItem.Checked = false;
            }
            else
            {
                panel8.Visible = true;
                textPropertiesToolStripMenuItem.Checked = true;
            } 
        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            CurrentTB.PreferredLineWidth = trackBar4.Value;
            ToolTip.Show("Preferred line width: " + trackBar4.Value, trackBar4);
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            CurrentTB.AutoIndentCharsPatterns = textBox8.Text;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
                CurrentTB.AutoIndentExistingLines = true;
            else
                CurrentTB.AutoIndentExistingLines = false;
        }

        private void label42_Click(object sender, EventArgs e)
        {
            panel8.Hide();
            textPropertiesToolStripMenuItem.Checked = false;
        }

        private void label43_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDownPanelTopBottom(sender, e, panel8);
            panel8.Dock = DockStyle.None;
        }

        private void label43_MouseMove(object sender, MouseEventArgs e)
        {
            mouseMovePanelTopBottom(sender, e, panel8);
        }

        private void label43_MouseUp(object sender, MouseEventArgs e)
        {
            mouseUpPanelTopBottom(sender, e, panel8);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                CurrentTB.AutoCompleteBrackets = true;
            else
                CurrentTB.AutoCompleteBrackets = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            CurrentTB.LeftBracket = textBox2.Text[0];
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            CurrentTB.RightBracket = textBox3.Text[0];
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            CurrentTB.LeftBracket2 = textBox5.Text[0];
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            CurrentTB.RightBracket2 = textBox4.Text[0];
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
        Type type = null;
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