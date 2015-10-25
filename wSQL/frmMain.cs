using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using wSQL.Business.Repository;
using wSQL.Business.Services;
using wSQL.Data.Models;
using wSQL.Library;
using wSQL.Controls;
using wSQL.Models;
using wSQL.Language.Services;
using wSQL.Language.Models;
using System.Text.RegularExpressions;
using System.Collections;

namespace wSQL
{
   public partial class frmMain : Form
   {
      private RuntimeCoreRepository runtimeCore;


      private Dictionary<string, LanguageToolTip> documentToolTips;
      string[] keywords = { "declare", "set" };
      //string[] snippets = { "if(^)\n{\n;\n}", "if(^)\n{\n;\n}\nelse\n{\n;\n}", "for(^;;)\n{\n;\n}", "while(^)\n{\n;\n}", "do\n{\n^;\n}while();", "switch(^)\n{\ncase : break;\n}" };
      string[] snippets = { "load (\"^\")", "find(^, \"\")", "map (^, )", "flatten( ^ )", "print ^", "ToString(^)", "PrintList (^)", "ToArray(^)", "Trim(^)" };

      Color currentLineColor = Color.FromArgb(100, 210, 210, 255);
      Color changedLineColor = Color.FromArgb(255, 230, 230, 255);

      public frmMain()
      {
         InitializeComponent();

         runtimeCore = new RuntimeCore();
         tabContainer.TabPages.Clear();
         initDocumentToolTips();
         createNewFile();
      }

      private void initDocumentToolTips()
      {
         documentToolTips = new Dictionary<string, LanguageToolTip>();
         documentToolTips.Add("declare", new LanguageToolTip("declare [variable name], [variable name]", "declare valiable to be available in the script"));
         documentToolTips.Add("set", new LanguageToolTip("set [variable] = [value]", "Set a variable value"));
         documentToolTips.Add("load", new LanguageToolTip("load (\"[url]\")", "Loads the page content from the provided url"));
      }

      #region editor
      private void BuildAutocompleteMenu(AutocompleteMenu popupMenu, string[] localVariables = null)
      {
         List<AutocompleteItem> items = new List<AutocompleteItem>();

         foreach (var item in snippets)
            items.Add(new SnippetAutocompleteItem(item) { ImageIndex = 1 });
         //foreach (var item in declarationSnippets)
         //   items.Add(new DeclarationSnippet(item) { ImageIndex = 0 });
         //foreach (var item in methods)
         //   items.Add(new MethodAutocompleteItem(item) { ImageIndex = 2 });
         foreach (var item in keywords)
            items.Add(new AutocompleteItem(item));

         //items.Add(new InsertSpaceSnippet());
         //items.Add(new InsertSpaceSnippet(@"^(\w+)([=<>!:]+)(\w+)$"));
         //items.Add(new InsertEnterSnippet());


         //add code variables
         //localVariables = new string[9] { "page", "table", "tds", "content", "tdsText", "texts", "test1", "outFile", "printToFile" };
         if (localVariables != null && localVariables.Count() > 0)
            foreach(var item in localVariables)
               items.Add(new AutocompleteItem(item));

         //set as autocomplete source
         popupMenu.Items.SetAutocompleteItems(items);
         popupMenu.SearchPattern = @"[\w\.:=!<>]";
      }

      void popupMenu_Opening(object sender, CancelEventArgs e)
      {
         //---block autocomplete menu for comments
         //get index of green style (used for comments)
         var iGreenStyle = CurrentTB.GetStyleIndex(CurrentTB.SyntaxHighlighter.GreenStyle);
         if (iGreenStyle >= 0)
            if (CurrentTB.Selection.Start.iChar > 0)
            {
               //current char (before caret)
               var c = CurrentTB[CurrentTB.Selection.Start.iLine][CurrentTB.Selection.Start.iChar - 1];
               //green Style
               var greenStyleIndex = Range.ToStyleIndex(iGreenStyle);
               //if char contains green style then block popup menu
               if ((c.style & greenStyleIndex) != 0)
                  e.Cancel = true;
            }
      }

      FastColoredTextBox CurrentTB
      {
         get
         {
            return tabContainer.GetActiveEditor();
         }

         set
         {
            //tsFiles.SelectedItem = (value.Parent as FATabStripItem);
            tabContainer.SelectedTab = value.Parent as TabPage;
            value.BringToFront();
            value.Focus();
         }
      }


      private void extractVariables(string text)
      {
         Regex regex = new Regex(@"^(?<range>[\w\s]+\b(declare|class|struct|enum|interface)\s+[\w<>,\s]+)|^\s*(declare|public|private|internal|protected)[^\n]+(\n?\s*{|;)?", RegexOptions.Multiline);
         string[] localVariables;

         foreach (Match r in regex.Matches(text))
         {
            string s = r.Value;
            int i = s.IndexOfAny(new char[] { '=', '{', ';' });
            if (i >= 0)
               s = s.Substring(0, i);
            s = s.Trim();
            //System.Diagnostics.Debug.WriteLine("--> " + s);
            if (s.StartsWith("declare"))
               s = s.TrimStart("declare".ToCharArray());

            localVariables = s.Split(',').Select(str => str.Trim()).ToArray();

            var edior = tabContainer.GetActiveEditor();
            if (edior != null)
            {
               var menu = edior.Tag as AutocompleteMenu;
               bool doUpdate = false;


               if (menu.Tag != null)
               {
                  var existentVars = menu.Tag as string[];

                  if (existentVars != null && existentVars.Count() > 0)
                     doUpdate = !localVariables.SequenceEqual(existentVars);
                  else if (localVariables != null && localVariables.Count() > 0)
                     doUpdate = true;
               }
               else if (localVariables != null && localVariables.Count() > 0)
                  doUpdate = true;
               
               if (doUpdate)
               {
                  menu.Tag = localVariables;
                  if (localVariables != null && localVariables.Count() > 0)
                     BuildAutocompleteMenu(menu, localVariables);
                  else
                     BuildAutocompleteMenu(menu);
               }
            }
         }
      }
      /*
      private void ReBuildObjectExplorer(string text)
      {
         try
         {
            List<ExplorerItem> list = new List<ExplorerItem>();
            int lastClassIndex = -1;
            //find classes, methods and properties
            Regex regex = new Regex(@"^(?<range>[\w\s]+\b(class|struct|enum|interface)\s+[\w<>,\s]+)|^\s*(public|private|internal|protected)[^\n]+(\n?\s*{|;)?", RegexOptions.Multiline);
            foreach (Match r in regex.Matches(text))
               try
               {
                  string s = r.Value;
                  int i = s.IndexOfAny(new char[] { '=', '{', ';' });
                  if (i >= 0)
                     s = s.Substring(0, i);
                  s = s.Trim();

                  var item = new ExplorerItem() { title = s, position = r.Index };
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
                     var parts = item.title.Split('(');
                     item.title = parts[0].Substring(parts[0].LastIndexOf(' ')).Trim() + "(" + parts[1];
                     item.type = ExplorerItemType.Method;
                  }
                  else
                              if (item.title.EndsWith("]"))
                  {
                     var parts = item.title.Split('[');
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
               catch {; }

            list.Sort(lastClassIndex + 1, list.Count - (lastClassIndex + 1), new ExplorerItemComparer());

            BeginInvoke(
                new Action(() =>
                {
                   explorerList = list;
                   dgvObjectExplorer.RowCount = explorerList.Count;
                   dgvObjectExplorer.Invalidate();
                })
            );
         }
         catch {; }
      }
      */
      #endregion

      #region tab functions
      private void updateTab(TabPage page)
      {
         if (page == null) page = tabContainer.SelectedTab;
         var info = page.GetFileDetails();
         if (info != null)
            page.Text = info.Saved ? info.FileName : info.FileName + "*";
      }
      #endregion

      #region file handeling
      private Style sameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(50, Color.Gray)));

      private TabPage createNewContainet(string name, string fullPath = null, bool saved = false)
      {
         //create new tab
         TabPage newPage = new TabPage(name);
         tabContainer.TabPages.Add(newPage);

         SplitContainer container = new SplitContainer();
         container.Orientation = Orientation.Horizontal;
         container.Dock = DockStyle.Fill;
         newPage.Controls.Add(container);
         

         //create output console
         OutputConsole console = new OutputConsole();
         console.Name = "outputConsole";
         console.Dock = DockStyle.Fill;
         container.Panel2.Controls.Add(console);
         container.Panel2Collapsed = true;

         //create new editor
         var newEditor = new FastColoredTextBox();
         newEditor.Name = "mainEditor";
         newEditor.DelayedTextChangedInterval = 1000;
         newEditor.DelayedEventsInterval = 500;
         newEditor.Language = FastColoredTextBoxNS.Language.wQL;
         newEditor.TextChangedDelayed += new EventHandler<TextChangedEventArgs>(editor_TextChangedDelayed);
         newEditor.TextChanged += new EventHandler<TextChangedEventArgs>(editor_TextChanged);
         newEditor.SelectionChangedDelayed += new EventHandler(editor_SelectionChangedDelayed);
         newEditor.ToolTipNeeded += NewEditor_ToolTipNeeded;
         newEditor.Name = ProjectConstants.EditorControlName;
         container.Panel1.Controls.Add(newEditor);
         //newPage.Controls.Add(newEditor);
         newEditor.CurrentLineColor = highlightCurrentLineToolStripMenuItem.Checked ? currentLineColor : Color.Transparent;
         newEditor.HighlightingRangeType = HighlightingRangeType.VisibleRange;
         newEditor.AddStyle(sameWordsStyle);//same words style
         newEditor.Dock = DockStyle.Fill;

         AutocompleteMenu popupMenu = new AutocompleteMenu(newEditor);
         //popupMenu.Items.ImageList = ilAutocomplete;
         popupMenu.Opening += new EventHandler<CancelEventArgs>(popupMenu_Opening);
         BuildAutocompleteMenu(popupMenu);
         newEditor.Tag= popupMenu;
         
         tabContainer.SelectedTab = newPage;
         
         //create the details for the file
         var details = new FileDetails()
         {
            FileName = name,
            FullPath = fullPath,
            Saved = saved
         };

         newPage.Tag = details;

         updateTab(newPage);

         
         newEditor.BringToFront();
         newEditor.Focus();

         return newPage;
      }

      private void editor_SelectionChangedDelayed(object sender, EventArgs e)
      {
         FastColoredTextBox tb = (sender as FastColoredTextBox);
         /*
         //remember last visit time
         if (tb.Selection.IsEmpty && tb.Selection.Start.iLine < tb.LinesCount)
         {
            if (lastNavigatedDateTime != tb[tb.Selection.Start.iLine].LastVisit)
            {
               tb[tb.Selection.Start.iLine].LastVisit = DateTime.Now;
               lastNavigatedDateTime = tb[tb.Selection.Start.iLine].LastVisit;
            }
         }
         */
         //highlight same words
         tb.VisibleRange.ClearStyle(sameWordsStyle);
         if (!tb.Selection.IsEmpty)
            return;//user selected diapason
                   //get fragment around caret
         var fragment = tb.Selection.GetFragment(@"\w");
         string text = fragment.Text;
         if (text.Length == 0)
            return;
         //highlight same words
         Range[] ranges = tb.VisibleRange.GetRanges("\\b" + text + "\\b").ToArray();

         if (ranges.Length > 1)
            foreach (var r in ranges)
               r.SetStyle(sameWordsStyle);
      }

      private void NewEditor_ToolTipNeeded(object sender, ToolTipNeededEventArgs e)
      {
         if (!string.IsNullOrEmpty(e.HoveredWord))
         {
            if (documentToolTips.ContainsKey(e.HoveredWord))
            {
               var info = documentToolTips[e.HoveredWord];
               e.ToolTipTitle = info.Title;
               e.ToolTipText = info.Text;
            }
         }

         /*
          * Also you can get any fragment of the text for tooltip.
          * Following example gets whole line for tooltip:

         var range = new Range(sender as FastColoredTextBox, e.Place, e.Place);
         string hoveredWord = range.GetFragment("[^\n]").Text;
         e.ToolTipTitle = hoveredWord;
         e.ToolTipText = "This is tooltip for '" + hoveredWord + "'";
          */
      }

      private void editor_TextChanged(object sender, TextChangedEventArgs e)
      {
         FastColoredTextBox tb = (sender as FastColoredTextBox);
         if (tb.Parent.Parent.Parent is TabPage)
         {
            var parentPage = (TabPage)tb.Parent.Parent.Parent;
            var info = (FileDetails)parentPage.Tag;
            info.Saved = false;
            updateTab(null);
         }
         //rebuild object explorer
         //string text = (sender as FastColoredTextBox).Text;
         //ThreadPool.QueueUserWorkItem( (o) => ReBuildObjectExplorer(text) );

         //show invisible chars
         //HighlightInvisibleChars(e.ChangedRange);
      }

      private void editor_TextChangedDelayed(object sender, TextChangedEventArgs e)
      {
         FastColoredTextBox tb = (sender as FastColoredTextBox);
         extractVariables(tb.Text);
      }

      private void createNewFile()
      {
         createNewContainet("Untitled");
      }
      
      private void openFile(string fileName)
      {
         if (tabContainer.TabCount == 1 && tabContainer.GetActiveEditor().Text == "")
            closeActiveFile();

         var tabPage = createNewContainet(Path.GetFileName(fileName), fileName, true);
         tabPage.GetEditor().OpenFile(fileName);
         tabPage.GetFileDetails().Saved = true;
         updateTab(tabPage);
      }

      private void closeActiveFile()
      {
         if (tabContainer.SelectedTab != null)
         {
            var info = tabContainer.SelectedTab.GetFileDetails();
            if (info != null)
            {
               if (!info.Saved && tabContainer.GetActiveEditor().Text != "" ) saveActiveFile();

               tabContainer.TabPages.Remove(tabContainer.SelectedTab);
            }
         }
      }

      private void saveActiveFile()
      {
         if (tabContainer.SelectedTab != null)
         {
            var info = tabContainer.SelectedTab.GetFileDetails();
            if (info.FullPath == null || info.FullPath.Trim() == "")
               saveActiveFileAs();
            else
            {
               var editor = tabContainer.SelectedTab.GetEditor();
               if (editor != null)
               {
                  editor.SaveToFile(info.FullPath, Encoding.ASCII);
                  info.Saved = true;
                  updateTab(null);
               }
            }
         }
      }

      private void saveActiveFileAs()
      {
         //TODO: save as
         if (tabContainer.SelectedTab != null)
         {
            var info = tabContainer.SelectedTab.GetFileDetails();
            saveFileDialog1.DefaultExt = ProjectConstants.SourceFileExtension;
            saveFileDialog1.Filter = ProjectConstants.ApplicationName + "|*." + ProjectConstants.SourceFileExtension + "|Text file|*.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
               info.FullPath = saveFileDialog1.FileName;
               info.FileName = Path.GetFileName(info.FullPath);

               var editor = tabContainer.SelectedTab.GetEditor();
               if (editor != null)
               {
                  editor.SaveToFile(info.FullPath, Encoding.ASCII);
                  info.Saved = true;
                  updateTab(null);
               }
            }
         }
      }
      #endregion

      private void exitToolStripMenuItem_Click(object sender, EventArgs e)
      {
         Application.Exit();
      }

      private void newToolStripMenuItem_Click(object sender, EventArgs e)
      {
         createNewFile();
      }

      private void openToolStripMenuItem_Click(object sender, EventArgs e)
      {
         openFileDialog1.FileName = "";
         openFileDialog1.Filter = "wQL files|*.3wql|All files|*.*";
         if (openFileDialog1.ShowDialog() == DialogResult.OK)
            openFile(openFileDialog1.FileName);
      }

      private void closeToolStripMenuItem_Click(object sender, EventArgs e)
      {
         closeActiveFile();
      }

      private void saveToolStripMenuItem_Click(object sender, EventArgs e)
      {
         saveActiveFile();
      }

      private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
      {
         saveActiveFileAs();
      }

      private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
      {

      }

      private void recentFilesToolStripMenuItem_Click(object sender, EventArgs e)
      {

      }
      
      private void runScriptToolStripMenuItem_Click(object sender, EventArgs e)
      {
         if (autosaveOnRunToolStripMenuItem.Checked)
            saveActiveFile();

         var console = tabContainer.GetActiveOutputConsole();
         console.TextOutput = "";
         Application.DoEvents();

         try
         {
            var interpretor = new Interpreter(new SymbolsTable(), DefaultLexer.Create(), new StatementRunner());
            var webCore = new WebCore();
            webCore.OnPrint += WebCore_OnPrint;
            var script = tabContainer.GetActiveEditor().Text;
            interpretor.Run(script, webCore);
            //Task.Factory.StartNew(() => interpretor.Run(script, webCore));
            //interpretor.Run(tabContainer.GetActiveEditor().Text, webCore);
         }
         catch (Exception ex)
         {
            console.TextOutput += ex.Message;
         }
      }


      private void printItem(object e, OutputConsole console)
      {
         var s = e as string;
         if (s != null)
            console.TextOutput += s + Environment.NewLine;
         else
         {
            var list = e as IEnumerable;
            if (list != null)
            {
               foreach (var item in list)
               {
                  printItem(item, console);
               }
            }
            else
               if (e != null)
                  printItem(e.ToString(), console);
         }
      }

      private void WebCore_OnPrint(object sender, object e)
      {
         // Do the dirty work of my method here.
         var console = tabContainer.GetActiveOutputConsole();
         printItem(e, console);
      }

      private void tabContainer_SelectedIndexChanged(object sender, EventArgs e)
      {
         if (tabContainer.SelectedTab != null)
            tabContainer.SelectedTab.Controls[0].Focus();
      }

      private void autosaveOnRunToolStripMenuItem_Click(object sender, EventArgs e)
      {
         autosaveOnRunToolStripMenuItem.Checked = !autosaveOnRunToolStripMenuItem.Checked;
      }

      private void highlightCurrentLineToolStripMenuItem_Click(object sender, EventArgs e)
      {
         highlightCurrentLineToolStripMenuItem.Checked = !highlightCurrentLineToolStripMenuItem.Checked;

         foreach (TabPage tab in tabContainer.TabPages)
         {
            var editor = tab.GetEditor();
            if (editor != null)
               editor.CurrentLineColor = highlightCurrentLineToolStripMenuItem.Checked ? currentLineColor : Color.Transparent;
         }

         var activeEditor = tabContainer.GetActiveEditor();
         if (activeEditor != null)
            activeEditor.Invalidate();
      }
   }
}
