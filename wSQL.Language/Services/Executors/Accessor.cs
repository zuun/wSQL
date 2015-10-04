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
      ExpectTokens(tokens, 1);

      var it = context.Symbols.Get("$");
      var name = tokens[1].Value;

      var property = it.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
      if (property == null)
        throw new Exception("Unknown property " + name);

      return property.GetValue(it);
    }
  }
}