using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using wSQL.Language.Contracts;
using wSQL.Language.Models;

namespace wSQL.Language.Services.Executors
{
   public class Trim : BaseExecutor
   {
      public Trim(Executor recurse) : base(recurse)
      {
      }

      public override dynamic Run(IList<Token> tokens, Context context)
      {
         var arguments = ExtractArguments(tokens.Skip(1).ToArray(), context);
         
         if (arguments[0].Type == TokenType.Identifier && arguments[0].Value.ToLower() == "it")
         {
            string trimValue = "";
            if (arguments.Count >= 4)
            {
               switch (arguments[4].Value)
               {
                  case "{tab}":
                     trimValue = "\t";
                     break;
                  default:
                     trimValue = arguments[4].Value;
                     break;
               }
            }

            var expr = arguments.Take(3).ToArray();
            //context.Symbols.Set(argName, item);
            //result.Add(recurse.Run(expr, context));
            var argName = arguments[0].Value; // do not evaluate this symbol
            //context.Symbols.Set(argName, item);
            var re = recurse.Run(expr, context);
            string response = "";
            if (trimValue.Trim() != "")
               response = ((string)re).Trim(trimValue.ToCharArray());
            else
               response = ((string)re).Trim();

            return response;
            //context.Symbols.Undeclare(argName);
         }

         throw new NotImplementedException();
      }
   }
}
