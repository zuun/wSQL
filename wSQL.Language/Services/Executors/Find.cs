using System;
using System.Collections.Generic;
using System.Linq;
using wSQL.Language.Contracts;
using wSQL.Language.Models;

namespace wSQL.Language.Services.Executors
{
  public class Find : BaseExecutor
  {
    public Find(Executor recurse)
      : base(recurse)
    {
    }

    public override dynamic Run(IList<Token> tokens, Context context)
    {
      ExpectTokens(tokens, 4);

      if (tokens[1].Type != TokenType.OpenPar || tokens[4].Type != TokenType.ClosedPar)
        throw new Exception("Invalid syntax.");

      var value = recurse.Run(tokens.Skip(2).Take(1).ToArray(), context);
      var xpath = recurse.Run(tokens.Skip(3).Take(1).ToArray(), context);
      return context.Core.Find(value, xpath);
    }
  }
}