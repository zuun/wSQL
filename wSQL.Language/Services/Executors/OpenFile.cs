using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using wSQL.Data.Models;
using wSQL.Language.Contracts;
using wSQL.Language.Models;

namespace wSQL.Language.Services.Executors
{
   public class OpenFile: BaseExecutor
   {
      public OpenFile(Executor recurse): base(recurse)
      {
      }

      public override dynamic Run(IList<Token> tokens, Context context)
      {
         /*
         1: file name
         2: file mode -> w - write; a - append
         */

         var arguments = ExtractArguments(tokens.Skip(1).ToArray(), context);
         string fileName = "", mode = "w";
         if (arguments[0].Type == TokenType.String)
            fileName = arguments[0].Value;
         else if (arguments[0].Type == TokenType.Identifier)
            fileName = recurse.Run(tokens.Skip(2).Take(1).ToArray(), context);
         else
            throw new Exception("OpenFile expected file location value");

         if (arguments.Count() >= 3)
            mode = arguments[2].Value.ExtractStringValue();

         FileObject response = new FileObject(fileName, mode);

         return response;
      }
   }
}
