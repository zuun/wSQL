using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using wSQL.Language.Contracts;
using wSQL.Language.Models;

namespace wSQL.Language.Services.Executors
{
  public class Flatten : BaseExecutor
  {
    public Flatten(Executor recurse)
      : base(recurse)
    {
    }

    public override dynamic Run(IList<Token> tokens, Context context)
    {
      var arguments = ExtractArguments(tokens.Skip(1).ToArray(), context);
      var value = recurse.Run(arguments, context);

      var list = value as IEnumerable<object>;
      if (list == null)
        throw new Exception("Flatten: argument is not a list.");

      var enumerables = list
        .Select(item => item as IEnumerable)
        .Where(it => it != null);

      var result = new List<object>();
      foreach (var sublist in enumerables)
        result.AddRange(sublist.Cast<object>());

      return result;
    }
  }
}