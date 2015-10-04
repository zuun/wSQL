using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wSQL.Models
{
   public class LanguageToolTip
   {
      public LanguageToolTip() { }
      public LanguageToolTip(string title, string text)
      {
         Title = title;
         Text = text;
      }

      public string Title { get; set; }
      public string Text { get; set; }
   }
}
