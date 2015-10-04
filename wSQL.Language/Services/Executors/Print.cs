using System.Collections.Generic;
using wSQL.Language.Models;

namespace wSQL.Language.Services.Executors
{
  public class Print : BaseExecutor
  {
    public override void Run(IList<Token> tokens, Context context)
    {
      ExpectTokens(tokens, 1);

      var rhs = GetValue(tokens[1], context);
      context.Core.Print(rhs);
    }
  }
}