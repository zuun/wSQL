using System;
using System.Collections.Generic;
using System.Linq;
using wSQL.Language.Contracts;
using wSQL.Language.Models;

namespace wSQL.Language.Services.Executors
{
  public class Set : BaseExecutor
  {
    public Set(Executor recurse)
      : base(recurse)
    {
    }

    public override dynamic Run(IList<Token> tokens, Context context)
    {
      ExpectTokens(tokens, 3);

      if (tokens[2].Type != TokenType.Assignment)
        throw new Exception("Invalid syntax.");

      var lhs = tokens[1].Value;
      var rhs = recurse.Run(tokens.Skip(3).ToArray(), context);
      context.Symbols.Set(lhs, rhs);

      return null;
    }
  }
}