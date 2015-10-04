using System;
using System.Collections.Generic;
using System.Linq;
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

    protected IList<Token> ExtractArguments(IList<Token> tokens, Context context)
    {
      if (tokens.Count < 3)
        throw new Exception("Invalid subexpression.");

      if (tokens[0].Type != TokenType.OpenPar)
        throw new Exception("Syntax error.");

      // start depth at zero; for each token, increment the depth for "(" and decrement for ")"
      // when the depth reaches zero again, stop and return the explored sublist
      var depth = 0;
      var index = 0;
      while (index < tokens.Count)
      {
        if (tokens[index].Type == TokenType.OpenPar)
          depth++;
        else if (tokens[index].Type == TokenType.ClosedPar)
          depth--;

        if (depth == 0)
          break;

        index++;
      }

      // if the loop has ended and the depth is not zero, the parentheses are unbalanced
      if (depth > 0)
        throw new Exception("Too many open parentheses.");
      // this normally can't happen but just in case
      if (depth < 0)
        throw new Exception("Too many closed parentheses.");

      return tokens.Skip(1).Take(index - 1).ToArray();
    }
  }
}