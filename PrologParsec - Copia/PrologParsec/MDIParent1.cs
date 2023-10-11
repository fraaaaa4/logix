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


namespace PrologParsec
{
    public partial class MDIParent1 : Form
    {
        AutocompleteMenu popupMenu; 
        private int childFormNumber = 0;
        public MDIParent1()
           
        {
            InitializeComponent();
            check();

           CreateTab(null);
            faTabStrip1.RemoveTab(faTabStripItem1);


            if (Properties.Settings.Default.documentMap)
                documentMap1.Visible = true;
            else
                documentMap1.Visible = false;
            //create new autocomplete
            //popupMenu = new AutocompleteMenu();
            //popupMenu.SetAutocompleteItems(new DynamicCollection(popupMenu, Fastcolored1));
        }

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

        

        private void toolStripLabel7_Click_1(object sender, EventArgs e)
        {
            Fastcolored1.Cut();
        }

        private void toolStripLabel6_Click_1(object sender, EventArgs e)
        {
            Fastcolored1.Copy();
        }

        private void toolStripLabel5_Click_1(object sender, EventArgs e)
        {
            Fastcolored1.Paste();
        }

        private void toolStripLabel4_Click_1(object sender, EventArgs e)
        {
            Fastcolored1.Undo();
        }

        private void toolStripLabel9_Click_1(object sender, EventArgs e)
        {
            CurrentTB.Redo();
        }

        private void toolStripLabel3_Click_1(object sender, EventArgs e)
        {
            openFile();

        }

        private void toolStripLabel8_Click_1(object sender, EventArgs e)
        {
            SaveQuestion();
        }

        private void toolStripLabel1_Click_1(object sender, EventArgs e)
        {
            CurrentTB.NavigateBackward();
        }

        FastColoredTextBoxNS.FastColoredTextBox CurrentTB
        {
            get
            {
                if (faTabStrip1.SelectedItem != null && faTabStrip1.SelectedItem.Controls.Count > 0)
                {
                    return (faTabStrip1.SelectedItem.Controls[0] as FastColoredTextBoxNS.FastColoredTextBox);
                }

                return null; // Nessun controllo presente o nessuna scheda selezionata
            }

            set
            {
                if (value != null)
                {
                    FarsiLibrary.Win.FATabStripItem tabItem = value.Parent as FarsiLibrary.Win.FATabStripItem;
                    if (tabItem != null)
                    {
                        faTabStrip1.SelectedItem = tabItem;
                        value.Focus();
                    }
                }
            }
        }


        private void fontText()
        {
            FontDialog fontDialog = new FontDialog();

            //min and max dimensions
            fontDialog.MinSize = 8;
            fontDialog.MaxSize = 72;
            fontDialog.FontMustExist = true;

           
                
            

            
            fontDialog.Font = CurrentTB.Font;

            //font dialog
            //fastcolored doesn't support all fonts
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
               Font newFont = fontDialog.Font;

               // CurrentTB.Font = fontDialog.Font;
                CurrentTB.Invalidate(); //refresh
                



               
                    CurrentTB.Font = fontDialog.Font;
                    CurrentTB.Invalidate();
                    Properties.Settings.Default.defaultFont = newFont;
                    Properties.Settings.Default.Save();
                }



              
                
            }
            
        



        internal class CustomCommandItem : AutocompleteItem
        {
            private string commandExplanation;

            public string CommandExplanation
            {
                get { return commandExplanation; }
                set { commandExplanation = value; }
            }

            public CustomCommandItem(string commandText, string explanation)
            {
                Text = commandText;
                CommandExplanation = explanation;
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


       public void autoCompleteMenuPopulate()
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
            CurrentTB.Undo();
        }

        private void toolStripLabel5_Click(object sender, EventArgs e)
        {
            CurrentTB.Paste();
        }

        private void toolStripLabel6_Click(object sender, EventArgs e)
        {
            CurrentTB.Copy();
        }

        private void toolStripLabel7_Click(object sender, EventArgs e)
        {
            CurrentTB.Cut();
        }

        private string currentFilePath = string.Empty;

        private void toolStripLabel8_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFile(currentFilePath);
            }
            catch (ArgumentException)
            {
            }
        }
        

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            openFile(); 
        }

        private void openFile()
        {
            if (isTextModified)
            {
                SaveQuestion();
            }
            //open prolog files
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Prolog source files (*.pl)|*.pl|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //read file
                string filePath = openFileDialog.FileName;
                string fileContent = File.ReadAllText(filePath);
                currentFilePath = openFileDialog.FileName;
                currentFilePath = Path.GetFileName(currentFilePath);
                CreateTab(openFileDialog.FileName);
                faTabStrip1.RemoveTab(faTabStripItem1);
                try
                {
                    MDIParent1.ActiveForm.Text = "Logix Testfire - " + openFileDialog.FileName; //put the window title
                    faTabStripItem1.Title = openFileDialog.FileName;
                    isTextModified = false;
                }
                catch (NullReferenceException e)
                {
                    toolStripStatusLabel.Text = ("Null reference error");
                }
            }
            if (!string.IsNullOrEmpty(currentFilePath) || !string.IsNullOrEmpty(currentFilePath))
            {
                saveToolStripMenuItem.Enabled = false;
            }
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            newFile();
        }


        private void newFile()
        {
            SaveQuestion();
            CurrentTB.Clear();
            isTextModified = false;
            currentFilePath = string.Empty;
            MDIParent1.ActiveForm.Text = "Logix Testfire";
        }
        private void SaveQuestion()
        {
            if (isTextModified)
            {
                DialogResult result = MessageBox.Show("Do you want to save the current file?", "Unsaved changes", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    if (!string.IsNullOrEmpty(currentFilePath))
                    {
                        SaveFile(currentFilePath);
                    }
                    else
                    {
                        SaveFileAsNew();
                    }
                }
                else if (result == DialogResult.Cancel)
                {
                    return;
                }
                
            }
            faTabStripItem1.Title = "Untitled";
        }

        private void SaveFileAsNew()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Prolog source files (*.pl)|*.pl|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                string textToSave = CurrentTB.Text;
                File.WriteAllText(filePath, textToSave);
                toolStripStatusLabel.Text = "Saved";
                isTextModified = false;
                currentFilePath = filePath;
                faTabStripItem1.Title = filePath;
                
                MDIParent1.ActiveForm.Text = MDIParent1.ActiveForm.Text.Replace("*", "");
            }
        }

        private void SaveFile(string filePath)
        {
            if (string.IsNullOrEmpty(currentFilePath)  || string.IsNullOrEmpty(filePath)){
                SaveFileAsNew();
            }
            String textToSave = CurrentTB.Text;
            File.WriteAllText(filePath, textToSave);
            toolStripStatusLabel.Text = "Saved";
            isTextModified = false;
            MDIParent1.ActiveForm.Text = MDIParent1.ActiveForm.Text.Replace("*", "");
        }

       
        private bool isTextModified = false;

        private void Fastcolored1_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            isTextModified = true;
            toolStripStatusLabel.Text = "Ready";
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(Asterisk)); //add an asterisk if you modify the textbox
            } else {
                Asterisk();
            }
            if (!string.IsNullOrEmpty(currentFilePath) || !string.IsNullOrEmpty(currentFilePath))
            {
                saveToolStripMenuItem.Enabled = true;
            }

           // CurrentTB.CommentPrefix = "%";
            //autocompleteMenu1.SetAutocompleteItems(new DynamicCollection(CurrentTB));
        }

        private void CurrentTB_TextChanged(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            isTextModified = true;
            toolStripStatusLabel.Text = "Ready";
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(Asterisk)); //add an asterisk if you modify the textbox
            }
            else
            {
                Asterisk();
            }
            if (!string.IsNullOrEmpty(currentFilePath) || !string.IsNullOrEmpty(currentFilePath))
            {
                saveToolStripMenuItem.Enabled = true;
            }

            //CurrentTB.CommentPrefix = "%";
            //autocompleteMenu1.SetAutocompleteItems(new DynamicCollection(CurrentTB));
        }

        private void Asterisk()
        {
            if (this.Text[this.Text.Length -1] != '*')
            {
                this.Text = this.Text + "*";
            }
        }

        private void MDIParent1_Load(object sender, EventArgs e)
        {
            this.Text = this.Text.Substring(0, this.Text.Length - 1);
            faTabStripItem1.Title = "Untitled";
            faTabStrip1.RemoveTab(faTabStripItem1);
           if (CurrentTB != null)
                CurrentTB.DescriptionFile = Properties.Settings.Default.descriptionFileDirectory; //modify description file of the fastcolored textbox
            
           
            autoCompleteMenuPopulate();

            string curDir = Directory.GetCurrentDirectory();
            this.webBrowser1.Url = new Uri(String.Format("file:///{0}/" + Properties.Settings.Default.auroraFile, curDir));
            label13.Text = "Version " + Properties.Resources.AppVersion;
            label11.Text = "Built on " + Properties.Resources.AppDate;
            panel2.Left = faTabStrip1.Width - panel2.Width - 20;
            string appPath = Path.GetDirectoryName(Application.ExecutablePath);
            string filePath = Path.Combine(appPath, "changelog.txt");
            
            try
            {
                if (File.Exists(filePath))
                {
                    richTextBox1.LoadFile(filePath, RichTextBoxStreamType.PlainText);
                }
                else
                {
                    richTextBox1.Text = "Changelog not found";
                }
            }
            catch (ArgumentException eq)
            {
            }

            //start page
            label1.Text = Properties.Resources.AppName + " " + Properties.Resources.AppEdition + " " + Properties.Resources.AppVersion;
            label4.Text = Properties.Resources.AppName; label2.Text = Properties.Resources.AppEdition + " Edition" ; label3.Text = Properties.Resources.AppDescription;
            
        }

        private void Fastcolored1_Load(object sender, EventArgs e)
        {
            
            isTextModified = false;

            CurrentTB.DescriptionFile = Properties.Settings.Default.descriptionFileDirectory;


            CurrentTB.Font = Properties.Settings.Default.defaultFont;

            check();
            autoCompleteMenuPopulate();

            
            //autocompleteMenu1.SetAutocompleteItems(new DynamicCollection(Fastcolored1));
        }

        private void runWithPrologToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        
}

        private void MDIParent1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(isTextModified)
            SaveQuestion();
        }

        private void newFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newFile();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFile();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile(currentFilePath);
        }

        private void saveAsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SaveFileAsNew();
        }

        private void printText()
        {

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
            e.Graphics.DrawString(this.Fastcolored1.Text, this.Fastcolored1.Font, Brushes.Black, 10, 25);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printText();
        }

        private void openFileToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            openFile(); 
        }

        private void saveToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SaveFile(currentFilePath);
        }

        private void saveAsToolStripMenuItem_Click_2(object sender, EventArgs e)
        {
            SaveFileAsNew();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.Redo();
        }

        private void cutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            CurrentTB.Cut();
        }

        private void copyToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            CurrentTB.Copy();
        }

        private void pasteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            CurrentTB.Paste();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.SelectedText = "";
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.Text = "";
        }

        private void menuBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            check();
            if (menuStrip1.Visible)
            {
                menuStrip1.Visible = false;
            }
            else
            {
                menuStrip1.Visible = true;
            } 
        }

        private void check()
        {
            if (menuStrip1.Visible)
            {
                menuBarToolStripMenuItem.Checked = true;
            }
            else
            {
                menuBarToolStripMenuItem.Checked = false;
            }

            if (toolStrip1.Visible)
            {
                toolbarToolStripMenuItem.Checked = false;
            }
            else
            {
                toolbarToolStripMenuItem.Checked = true;
            }

            if (Fastcolored1.WordWrap)
                wordWrapToolStripMenuItem.Checked = false;
            else
                wordWrapToolStripMenuItem.Checked = true;

            if (Fastcolored1.WordWrapIndent > 0)
                wordWrapIndentToolStripMenuItem.Checked = false;
            else
                wordWrapIndentToolStripMenuItem.Checked = true;

            if (Fastcolored1.DescriptionFile == null)
                checkForSyntaxToolStripMenuItem.Checked = false;
            else
                checkForSyntaxToolStripMenuItem.Checked = true;

            if (documentMap1.Visible == true)
            {
                documentMapToolStripMenuItem.Checked = true;
                Properties.Settings.Default.documentMap = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                documentMapToolStripMenuItem.Checked = false;
                Properties.Settings.Default.documentMap = false;
                Properties.Settings.Default.Save();
            }
        }

        private void toolbarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            check();
            if (toolStrip1.Visible)
            {
                toolStrip1.Visible = false;
            }
            else
            {
                toolStrip1.Visible = true;
            } 
        }

        private void addHeaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.InsertText(Properties.Settings.Default.headerText);
        }

        private void addFooterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.AppendText("\n" + Properties.Settings.Default.footerText + currentFilePath);
        }

        private void addFooterToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            CurrentTB.AppendText("\n" + Properties.Settings.Default.footerText + currentFilePath);
        }

        private void addHeaderToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            CurrentTB.InsertText(Properties.Settings.Default.headerText);
            CurrentTB.Refresh();
            CurrentTB.Invalidate();

        }

        private void aboutLogixToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 form2 = new AboutBox1();
            form2.ShowDialog();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentTB.InsertText("\n%" + toolStripTextBox1.Text);
        }
        

        private void colorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xmlDetails form2 = new xmlDetails();
            form2.ShowDialog();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            check();
            if (CurrentTB.WordWrap)
                CurrentTB.WordWrap = false;
            else
                CurrentTB.WordWrap = true;
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
                item.Tag = bookmark;
                item.Click += new EventHandler(item_Click);
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
            if (documentMapToolStripMenuItem.Checked == true)
            {
                documentMap1.Visible = false;
            }
            else
            {
                documentMap1.Visible = true;
            }
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
                tb.Font = Fastcolored1.Font;
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
                AutocompleteMenu popupMenu = new AutocompleteMenu();
                popupMenu.SetAutocompleteMenu(CurrentTB, autocompleteMenu1);
                popupMenu = autocompleteMenu1;

            }
            catch (Exception ex)
            {
                if (MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.Retry)
                    CreateTab(fileName);
            }
        }

        private void faTabStrip1_TabStripItemSelectionChanged(FarsiLibrary.Win.TabStripItemChangedEventArgs e)
        {
            documentMap1.Target = CurrentTB;
            if (CurrentTB != null)
            {
                CurrentTB.DescriptionFile = Properties.Settings.Default.descriptionFileDirectory; //modify description file of the fastcolored textbox
               
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
                CurrentTB.Zoom = int.Parse((sender as ToolStripItem).Tag.ToString());
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

        private void MDIParent1_Resize(object sender, EventArgs e)
        {
            panel2.Left = faTabStrip1.Width - panel2.Width - 20;
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