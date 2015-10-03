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

namespace wSQL
{
   public partial class frmMain : Form
   {
      private RuntimeCoreRepository runtimeCore;

      //string[] snippets = { "if(^)\n{\n;\n}", "if(^)\n{\n;\n}\nelse\n{\n;\n}", "for(^;;)\n{\n;\n}", "while(^)\n{\n;\n}", "do\n{\n^;\n}while();", "switch(^)\n{\ncase : break;\n}" };
      string[] snippets = { "Load(\"^\")" };

      public frmMain()
      {
         InitializeComponent();

         runtimeCore = new RuntimeCore();

         createNewFile();
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
         //foreach (var item in keywords)
         //   items.Add(new AutocompleteItem(item));

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
            if (tabContainer.SelectedTab == null)
               return null;
            return (tabContainer.SelectedTab.Controls[0] as FastColoredTextBox);
         }

         set
         {
            //tsFiles.SelectedItem = (value.Parent as FATabStripItem);
            value.Focus();
         }
      }
      #endregion

      #region tab functions
      private void updateTab(TabPage page)
      {
         if (page == null) page = tabContainer.SelectedTab;
         var info = page.Tag as FileDetails;
         if (info != null)
         {
            page.Text = info.Saved ? info.FileName : info.FileName + "*";
         }
      }
      #endregion

      #region file handeling
      private void createNewContainet(string name)
      {
         //create new tab
         TabPage newPage = new TabPage(name);
         tabContainer.TabPages.Add(newPage);

         //create new editor
         var newEditor = new FastColoredTextBox();
         newEditor.DelayedTextChangedInterval = 1000;
         newEditor.DelayedEventsInterval = 500;
         newEditor.TextChangedDelayed += new EventHandler<TextChangedEventArgs>(editor_TextChangedDelayed);
         newEditor.Name = ProjectConstants.EditorControlName;
         newPage.Controls.Add(newEditor);
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
            Saved = false
         };

         newPage.Tag = details;

         updateTab(newPage);

         
         newEditor.BringToFront();
         newEditor.Focus();
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

      }

      private void closeActiveFile()
      {
         if (tabContainer.SelectedTab != null)
         {
            var info = tabContainer.SelectedTab.Tag as FileDetails;
            if (info != null)
            {
               if (!info.Saved) saveActiveFile();

               tabContainer.TabPages.Remove(tabContainer.SelectedTab);
            }
         }
      }

      private void saveActiveFile()
      {
         if (tabContainer.SelectedTab != null)
         {
            var info = tabContainer.SelectedTab.Tag as FileDetails;
            if (info.FullPath == null || info.FullPath.Trim() == "")
               saveActiveFileAs();
            else
            {
               var editor = tabContainer.SelectedTab.Controls.Find(ProjectConstants.EditorControlName, true).FirstOrDefault();
               if (editor != null)
               {
                  ((FastColoredTextBox)editor).SaveToFile(info.FullPath, Encoding.ASCII);
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
            var info = tabContainer.SelectedTab.Tag as FileDetails;
            saveFileDialog1.DefaultExt = ProjectConstants.SourceFileExtension;
            saveFileDialog1.Filter = ProjectConstants.ApplicationName + "|*." + ProjectConstants.SourceFileExtension + "|Text file|*.txt";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
               info.FullPath = saveFileDialog1.FileName;
               info.FileName = Path.GetFileName(info.FullPath);

               var editor = tabContainer.SelectedTab.Controls.Find(ProjectConstants.EditorControlName, true).FirstOrDefault();
               if (editor != null)
               {
                  ((FastColoredTextBox)editor).SaveToFile(info.FullPath, Encoding.ASCII);
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
         //TODO: open file
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
         var response = runtimeCore.RunScript(CurrentTB.Text);

         if (((dynamic)response).PageContent != null)
         {
            //enable vhiw for page content
         }
      }

      private void tabContainer_SelectedIndexChanged(object sender, EventArgs e)
      {
         tabContainer.SelectedTab.Controls[0].Focus();
      }
   }
}
