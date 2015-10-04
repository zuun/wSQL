using System;
using System.Collections.Generic;
using wSQL.Language.Contracts;
using wSQL.Language.Models;

namespace wSQL.Language.Services.Executors
{
  public abstract class BaseExecutor : Executor
  {
    public abstract dynamic Run(IList<Token> tokens, Context context);

    //

    // ReSharper disable once InconsistentNaming
    protected Executor recurse;

    protected BaseExecutor(Executor recurse)
    {
      this.recurse = recurse;
    }

    protected static void ExpectTokens(ICollection<Token> tokens, int count)
    {
      if (tokens.Count < count + 1)
        throw new Exception(string.Format("Missing argument(s), expected at least {0} but found only {1}.", count, (tokens.Count - 1)));
    }

    protected static string Unquote(string s)
    {
      return s.Substring(1, s.Length - 2);
    }
  }
}