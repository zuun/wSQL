using System;
using System.Collections.Generic;
using wSQL.Language.Contracts;

namespace wSQL.Language.Services
{
  public class SymbolsTable : Symbols
  {
    public void Declare(string name)
    {
      if (dict.ContainsKey(name))
        throw new Exception("Variable has already been declared: " + name);

      dict[name] = null;
    }

    public void Set(string name, object value)
    {
      dict[name] = value;
    }

    public object Get(string name)
    {
      if (!dict.ContainsKey(name))
        throw new Exception("Unknown variable name: " + name);

      return dict[name];
    }

    //

    private readonly Dictionary<string, object> dict = new Dictionary<string, object>();
  }
}