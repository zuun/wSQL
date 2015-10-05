using System;
using System.Collections.Generic;
using System.Linq;
using wSQL.Language.Contracts;
using wSQL.Language.Models;

namespace wSQL.Language.Services.Executors
{
  public class Map : BaseExecutor
  {
    public Map(Executor recurse)
      : base(recurse)
    {
    }

    public override dynamic Run(IList<Token> tokens, Context context)
    {
      // map ( list , name => ... )
      // for each item in list:
      //   set name (in symbol table) to value of item
      //   evaluate ...

      var arguments = ExtractArguments(tokens.Skip(1).ToArray(), context);
      var listName = arguments[0].Value;
      var lambda = arguments.Skip(2).ToArray();

      var list = context.Symbols.Get(listName) as IEnumerable<object>;
      if (list == null)
        throw new Exception("Map: first argument is not a list.");

      return null;
    }
  }
}