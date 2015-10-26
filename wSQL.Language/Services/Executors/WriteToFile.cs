using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using wSQL.Data.Models;
using wSQL.Language.Contracts;
using wSQL.Language.Models;

namespace wSQL.Language.Services.Executors
{
   public class WriteToFile: BaseExecutor
   {
      public WriteToFile(Executor recurse): base(recurse)
      {
      }

      public override dynamic Run(IList<Token> tokens, Context context)
      {
         /*
         1: content to write
         2: file object
         3: item separator
         4: line breack

         3: file mode -> w: write, a: append
         */

         var arguments = ExtractArguments(tokens.Skip(1).ToArray(), context);
         FileObject fileObject;
         string separator = ",", lineBreack = "\r\n";

         var rhs = recurse.Run(tokens.Skip(2).Take(1).ToArray(), context);
         fileObject = recurse.Run(tokens.Skip(4).Take(1).ToArray(), context);
        
         if (arguments.Count() >= 6)
            separator = arguments[5].Value.ExtractStringValue();
         if (arguments.Count() >= 7)
            lineBreack = arguments[6].Value.ExtractStringValue();

         context.Core.WriteToFile(rhs, fileObject, separator, lineBreack);

         return null;
      }
   }
}
