using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wSQL.Controls
{
   public class OutputConsole: Panel
   {
      private string _textConsoleOutput;
      private string _pageContent;
      private TabControl consoleTabs;

      public OutputConsole()
      {
         consoleTabs = new TabControl();
         this.Controls.Add(consoleTabs);
         consoleTabs.Dock = DockStyle.Fill;

         var textTab = new TabPage();
         textTab.Name = "TextTab";
         textTab.Text = "Text Content";
         var pageContentEditor = new FastColoredTextBox();
         pageContentEditor.Name = "PageContentTextEditor";
         pageContentEditor.Language = FastColoredTextBoxNS.Language.Custom;
         pageContentEditor.Dock = DockStyle.Fill;
         textTab.Controls.Add(pageContentEditor);

         consoleTabs.TabPages.Add(textTab);


         var pageTab = new TabPage();
         pageTab.Name = "PageTab";
         pageTab.Text = "Page Content";

         pageContentEditor = new FastColoredTextBox();
         pageContentEditor.Name = "PageContentEditor";
         pageContentEditor.Language = FastColoredTextBoxNS.Language.HTML;
         pageContentEditor.Dock = DockStyle.Fill;
         pageTab.Controls.Add(pageContentEditor);

         consoleTabs.TabPages.Add(pageTab);


         var pagePreview = new TabPage();
         pagePreview.Name = "PagePreviewTab";
         pagePreview.Text = "Page Preview";
         consoleTabs.TabPages.Add(pagePreview);

         var browser = new WebBrowser();
         browser.Dock = DockStyle.Fill;
         browser.Name = "PageContentPreview";
         pagePreview.Controls.Add(browser);

         
      }

      public string TextOutput
      {
         get { return _textConsoleOutput; }
         set
         {
            if (value != _textConsoleOutput)
            {
               _textConsoleOutput = value;
               updateControlInterface("TextOutput");
            }
         }
      }

      public string PageContent
      {
         get { return _pageContent; }
         set
         {
            if (value != _pageContent)
            {
               _pageContent = value;
               updateControlInterface("PageContent");
            }
         }
      }


      private bool isConsole()
      {
         if (this.Parent.Parent is SplitContainer)
         {
            return ((SplitContainer)this.Parent.Parent).Panel2Collapsed;
         }

         return false;
      }
      private void makeConsoleVisible()
      {
         if (this.Parent.Parent is SplitContainer)
            ((SplitContainer)this.Parent.Parent).Panel2Collapsed = false;
      }
      private void updateControlInterface(string sender)
      {
         makeConsoleVisible();

         var editor = (FastColoredTextBox)this.Controls.Find("PageContentEditor", true).First();
         editor.Text = PageContent;
         editor.SelectAll();
         editor.DoAutoIndent();

         var text = (FastColoredTextBox)this.Controls.Find("PageContentTextEditor", true).First();
         text.Text = TextOutput;

         var browser = (WebBrowser)this.Controls.Find("PageContentPreview", true).First();
         browser.DocumentText = PageContent;


      }
   }
}
