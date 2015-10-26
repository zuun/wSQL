using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using wSQL.Data.Models;
using wSQL.Language.Contracts;
using wSQL.Language.Models;

namespace wSQL.Language.Services.Executors
{
   public class ToArray : BaseExecutor
   {
      public ToArray(Executor recurse): base(recurse)
      {
      }

      public override dynamic Run(IList<Token> tokens, Context context)
      {
         var arguments = ExtractArguments(tokens.Skip(1).ToArray(), context);

         object result = new string[0];
         object stringValue = null;

         string separator = ",";
         if (tokens.Count >= 6 && tokens[6].Type == TokenType.String)
         {
            separator = tokens[6].Value.ExtractStringValue();
            if (separator == "\\r\\n")
               separator = Environment.NewLine;
         }


         if (arguments[0].Type == TokenType.Identifier && arguments[0].Value.ToLower() == "it")
         {
            var expr = arguments.Take(3).ToArray();
            var argName = arguments[0].Value; // do not evaluate this symbol
            stringValue = recurse.Run(expr, context);
         }
         else
            stringValue = recurse.Run(arguments, context);
         
         if (stringValue != null)
         {
            if (stringValue is string)
               result = ((string)stringValue).Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            else
               throw new Exception("ToArray: Expected string value but " + stringValue.GetType() + " found");
         }
         else
            throw new Exception("ToArray: Expected string value but no value found");

         return result;
      }
   }
}
