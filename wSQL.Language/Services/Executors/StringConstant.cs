using System.Collections.Generic;
using wSQL.Language.Contracts;
using wSQL.Language.Models;

namespace wSQL.Language.Services.Executors
{
  public class StringConstant : BaseExecutor
  {
    public StringConstant(Executor recurse)
      : base(recurse)
    {
    }

    public override dynamic Run(IList<Token> tokens, Context context)
    {
      return Unquote(tokens[0].Value);
    }
  }
}