using System;
using System.Collections.Generic;
using wSQL.Language.Contracts;
using wSQL.Language.Models;

namespace wSQL.Language.Services.Executors
{
  public class Lambda : BaseExecutor
  {
    public Lambda(Executor recurse)
      : base(recurse)
    {
    }

    public override dynamic Run(IList<Token> tokens, Context context)
    {
      // list => ...
      // Func<IEnumerable<dynamic>, IEnumerable<dynamic>> f = list.Select(...)
      throw new NotImplementedException();
    }
  }
}