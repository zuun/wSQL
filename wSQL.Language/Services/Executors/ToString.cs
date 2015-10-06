using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using wSQL.Language.Contracts;
using wSQL.Language.Models;

namespace wSQL.Language.Services.Executors
{
   public class ToString : BaseExecutor
   {
      public ToString(Executor recurse)
      : base(recurse)
      {
      }

      public override dynamic Run(IList<Token> tokens, Context context)
      {
         var arguments = ExtractArguments(tokens.Skip(1).ToArray(), context);
         var listName = arguments[0].Value;

         var list = context.Symbols.Get(listName) as IEnumerable;
         if (list == null)
            throw new Exception("Map: first argument is not a list.");

         
         string result = "";
         foreach (var item in list)
         {
            result += item.ToString();
         }
         
         return result;
      }
   }
}
