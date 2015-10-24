using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

         if (arguments[0].Type == TokenType.Identifier && arguments[0].Value.ToLower() == "it")
         {
            string separator = ",";
            if (tokens.Count >= 6 && tokens[6].Type == TokenType.String)
            {
               separator = tokens[6].Value;
               separator = separator.Substring(1, separator.Length - 2);
               if (separator == "\\r\\n")
                  separator = Environment.NewLine;
            }
            
            var expr = arguments.Take(3).ToArray();
            //context.Symbols.Set(argName, item);
            //result.Add(recurse.Run(expr, context));
            var argName = arguments[0].Value; // do not evaluate this symbol
            //context.Symbols.Set(argName, item);
            var re  = recurse.Run(expr, context);
            var response = ((string)re).Split( new string[] { separator } , StringSplitOptions.RemoveEmptyEntries);

            //context.Symbols.Undeclare(argName);
            result = response;

         }
         
         return result;
      }
   }
}
