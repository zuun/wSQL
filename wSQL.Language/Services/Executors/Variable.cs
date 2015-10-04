using System.Collections.Generic;
using wSQL.Language.Contracts;
using wSQL.Language.Models;

namespace wSQL.Language.Services.Executors
{
  public class Variable : BaseExecutor
  {
    public Variable(Executor recurse)
      : base(recurse)
    {
    }

    public override dynamic Run(IList<Token> tokens, Context context)
    {
      return context.Symbols.Get(tokens[0].Value);
    }
  }
}