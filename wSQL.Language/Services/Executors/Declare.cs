using System.Collections.Generic;
using System.Linq;
using wSQL.Language.Models;

namespace wSQL.Language.Services.Executors
{
  public class Declare : BaseExecutor
  {
    public override void Run(IList<Token> tokens, Context context)
    {
      ExpectTokens(tokens, 1);

      foreach (var token in tokens.Skip(1))
        context.Symbols.Declare(token.Value);
    }
  }
}