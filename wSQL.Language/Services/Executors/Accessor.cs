using System;
using System.Collections.Generic;
using System.Reflection;
using wSQL.Language.Contracts;
using wSQL.Language.Models;

namespace wSQL.Language.Services.Executors
{
  public class Accessor : BaseExecutor
  {
    public Accessor(Executor recurse)
      : base(recurse)
    {
    }

    public override dynamic Run(IList<Token> tokens, Context context)
    {
      ExpectTokens(tokens, 2);

      var it = context.Symbols.Get(tokens[0].Value);
      var name = tokens[2].Value;

      var property = it.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
      if (property == null)
        throw new Exception("Unknown property " + name);

      return property.GetValue(it);
    }
  }
}