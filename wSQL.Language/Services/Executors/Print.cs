using System.Collections.Generic;
using System.Linq;
using wSQL.Language.Contracts;
using wSQL.Language.Models;

namespace wSQL.Language.Services.Executors
{
  public class Print : BaseExecutor
  {
    public Print(Executor recurse)
      : base(recurse)
    {
    }

    public override dynamic Run(IList<Token> tokens, Context context)
    {
      ExpectTokens(tokens, 1);

      var rhs = recurse.Run(tokens.Skip(1).ToArray(), context);
      context.Core.Print(rhs);

      return null;
    }
  }
}