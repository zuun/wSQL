using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wSQL.Data.Models
{
   public static class Extenders
   {
      public static string ExtractStringValue(this string value)
      {
         if (value != string.Empty)
         {
            var result = value.TrimStart('"').TrimEnd('"');

            switch (result)
            {
               case "{tab}":
                  result = "\t";
                  break;
               case "{new line}":
               case "{nl}":
                  result = Environment.NewLine;
                  break;
               default:
                  break;
            }

            return result;
         }
         else
            return value;
      }
   }
}
