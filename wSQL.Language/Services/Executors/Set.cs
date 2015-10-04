using System;
using System.Collections.Generic;
using wSQL.Language.Models;

namespace wSQL.Language.Services.Executors
{
  public class Set : BaseExecutor
  {
    public override void Run(IList<Token> tokens, Context context)
    {
      ExpectTokens(tokens, 3);

      if (tokens[2].Type != TokenType.Assignment)
        throw new Exception("Invalid syntax.");

      var lhs = tokens[1].Value;
      var rhs = GetValue(tokens[3], context);
      context.Symbols.Set(lhs, rhs);
    }
  }
}