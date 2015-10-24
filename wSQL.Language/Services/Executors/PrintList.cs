using System.Collections.Generic;
using System.Linq;
using wSQL.Language.Contracts;
using wSQL.Language.Models;

namespace wSQL.Language.Services.Executors
{
   public class PrintList : BaseExecutor
   {
      public PrintList(Executor recurse)
      : base(recurse)
      {
      }

      public override dynamic Run(IList<Token> tokens, Context context)
      {
         ExpectTokens(tokens, 1);

         var rhs = recurse.Run(tokens.Skip(2).ToArray(), context);
         var separator = recurse.Run(tokens.Skip(4).ToArray(), context);
         context.Core.PrintList(rhs, separator, null);

         return null;
      }
   }
}
