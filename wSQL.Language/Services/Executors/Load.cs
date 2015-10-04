using System;
using System.Collections.Generic;
using wSQL.Language.Models;

namespace wSQL.Language.Services.Executors
{
  public class Load : BaseExecutor
  {
    public override dynamic Run(IList<Token> tokens, Context context)
    {
      ExpectTokens(tokens, 3);

      if (tokens[1].Type != TokenType.OpenPar || tokens[3].Type != TokenType.ClosedPar)
        throw new Exception("Invalid syntax.");

      var rhs = GetValue(tokens[2], context);
      return context.Core.OpenPage(rhs.ToString());
    }
  }
}