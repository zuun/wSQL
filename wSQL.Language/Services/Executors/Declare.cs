﻿using System.Collections.Generic;
using System.Linq;
using wSQL.Language.Contracts;
using wSQL.Language.Models;

namespace wSQL.Language.Services.Executors
{
  public class Declare : BaseExecutor
  {
    public Declare(Executor recurse)
      : base(recurse)
    {
    }

    public override dynamic Run(IList<Token> tokens, Context context)
    {
      ExpectTokens(tokens, 1);

      foreach (var token in tokens.Skip(1))
        context.Symbols.Declare(token.Value);

      return null;
    }
  }
}