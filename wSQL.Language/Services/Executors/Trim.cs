using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using wSQL.Data.Models;
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

         string trimValue = "";
         if (arguments.Count >= 4)
            trimValue = arguments[arguments.Count - 1].Value.ExtractStringValue();

         object expression = null;

         if (arguments[0].Type == TokenType.Identifier && arguments[0].Value.ToLower() == "it")
         {
            var expr = arguments.Take(3).ToArray();
            var argName = arguments[0].Value; // do not evaluate this symbol
            //context.Symbols.Set(argName, item);
            expression = recurse.Run(expr, context);
         }
         else
            expression = recurse.Run(arguments, context);

         if (expression is string)
            return TrimString(expression, trimValue);
         else
            if (expression is IEnumerable<string>)
         {
            var list = ((IEnumerable<string>)expression).ToArray();
            for (int index = 0; index < list.Count(); index++)
               list[index] = TrimString(list[index], trimValue);
            return list;
         }

         throw new Exception("Trim expected string values but not foud");
      }

      private string TrimString(object value, string trimValue = "")
      {
         if (value is string)
         {
            if (trimValue.Trim() != "")
               return ((string)value).Trim(trimValue.ToCharArray());
            else
               return ((string)value).Trim();
         }
         else
            throw new Exception("String expected for Trim but now found");
      }
   }
}
