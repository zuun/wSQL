using System;
using System.Collections.Generic;
using System.Linq;
using wSQL.Language.Contracts;
using wSQL.Language.Models;

namespace wSQL.Language.Services.Executors
{
  public class Load : BaseExecutor
  {
    public Load(Executor recurse)
      : base(recurse)
    {
    }

    public override dynamic Run(IList<Token> tokens, Context context)
    {
      ExpectTokens(tokens, 3);

      if (tokens[1].Type != TokenType.OpenPar || tokens[3].Type != TokenType.ClosedPar)
        throw new Exception("Invalid syntax.");

      var rhs = recurse.Run(tokens.Skip(2).ToArray(), context);
      return context.Core.OpenPage(rhs.ToString());
    }
  }
}