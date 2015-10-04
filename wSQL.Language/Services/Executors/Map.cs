using System;
using System.Collections.Generic;
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
      // map ( list name => ... )
      // for each item in list:
      //   set name (in symbol table) to value of item
      //   evaluate ...
      throw new NotImplementedException();
    }
  }
}