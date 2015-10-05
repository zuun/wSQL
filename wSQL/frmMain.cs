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
      string[] snippets = { "load (\"^\")", "find( ^, \"\")", "map ( ^, )", "flatten( ^ )", "print ^"};

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
      private void BuildAutocompleteMenu(AutocompleteMenu popupMenu)
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
         //newEditor.TextChangedDelayed += new EventHandler<TextChangedEventArgs>(editor_TextChangedDelayed);
         newEditor.TextChanged += new EventHandler<TextChangedEventArgs>(editor_TextChangedDelayed);
         newEditor.ToolTipNeeded += NewEditor_ToolTipNeeded;
         newEditor.Name = ProjectConstants.EditorControlName;
         container.Panel1.Controls.Add(newEditor);
         //newPage.Controls.Add(newEditor);
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

      private void editor_TextChangedDelayed(object sender, TextChangedEventArgs e)
      {
         FastColoredTextBox tb = (sender as FastColoredTextBox);
         if (tb.Parent is TabPage)
         {
            var parentPage = (TabPage)tb.Parent;
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
         var console = tabContainer.GetActiveOutputConsole();
         console.TextOutput = "";
         try
         {
            var interpretor = new Interpreter(new SymbolsTable(), DefaultLexer.Create(), new StatementRunner());
            var webCore = new WebCore();
            webCore.OnPrint += WebCore_OnPrint;
            interpretor.Run(tabContainer.GetActiveEditor().Text, webCore);
         }
         catch (Exception ex)
         {
            console.TextOutput += ex.Message;
         }
         /*
         var response = runtimeCore.RunScript();

         if (((dynamic)response).PageContent != null)
         {
            //enable view for page content
            var console = tabContainer.GetActiveOutputConsole();
            console.PageContent = ((dynamic)response).PageContent;
         }

         if (((dynamic)response).Text != null)
         {
            //enable view for page content
            var console = tabContainer.GetActiveOutputConsole();
            console.TextOutput = ((dynamic)response).Text;
         }
         */
      }

      private void WebCore_OnPrint(object sender, object e)
      {
         var console = tabContainer.GetActiveOutputConsole();
         var list = e as IEnumerable;
         if (list == null)
            console.TextOutput += e.ToString() + Environment.NewLine;
         else
         {
            foreach (var item in list)
               console.TextOutput += item.ToString() + Environment.NewLine;
         }
      }

      private void tabContainer_SelectedIndexChanged(object sender, EventArgs e)
      {
         if (tabContainer.SelectedTab != null)
            tabContainer.SelectedTab.Controls[0].Focus();
      }
   }
}
