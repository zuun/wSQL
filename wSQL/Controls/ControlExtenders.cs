using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using wSQL.Data.Models;

namespace wSQL.Controls
{
   public static class ControlExtenders
   {
      public static FastColoredTextBox GetEditor(this TabPage tabPage)
      {
         if (tabPage != null)
            foreach (var control in tabPage.Controls.Find("mainEditor", true))
               if (control is FastColoredTextBox)
                  return control as FastColoredTextBox;

         return null;
      }

      public static FastColoredTextBox GetActiveEditor(this TabControl tabControl)
      {
         if (tabControl != null && tabControl.SelectedTab != null)
            return tabControl.SelectedTab.GetEditor();

         return null;
      }

      public static OutputConsole GetActiveOutputConsole(this TabControl tabControl)
      {
         if (tabControl != null && tabControl.SelectedTab != null)
            return tabControl.SelectedTab.GetOutputConsole();
         return null;
      }
      public static OutputConsole GetOutputConsole(this TabPage tabPage)
      {
         if (tabPage != null)
            foreach (var control in tabPage.Controls.Find("outputConsole", true))
               if (control is OutputConsole)
                  return control as OutputConsole;

         return null;
      }

      public static FileDetails GetActiveFileDetails(this TabControl tabControl)
      {
         if (tabControl != null && tabControl.SelectedTab != null)
            return tabControl.SelectedTab.GetFileDetails();

         return null;
      }

      public static FileDetails GetFileDetails(this TabPage tabpage)
      {
         if (tabpage != null && tabpage.Tag is FileDetails)
            return (FileDetails)tabpage.Tag;

         return null;
      }
   }
}
