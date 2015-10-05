using System;
using System.Collections;
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
      var arguments = ExtractArguments(tokens.Skip(1).ToArray(), context);
      var listName = arguments[0].Value;

      var list = context.Symbols.Get(listName) as IEnumerable;
      if (list == null)
        throw new Exception("Map: first argument is not a list.");

      var lambda = arguments.Skip(2).ToArray();
      
      var argName = lambda[0].Value; // do not evaluate this symbol
      context.Symbols.Declare(argName);

      var expr = lambda.Skip(2).ToArray();

      var result = new List<object>();
      foreach (var item in list)
      {
        context.Symbols.Set(argName, item);
        result.Add(recurse.Run(expr, context));
      }

      context.Symbols.Undeclare(argName);

      return result;
    }
  }
}