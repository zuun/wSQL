using System;
using System.Collections.Generic;
using wSQL.Language.Contracts;
using wSQL.Language.Models;

namespace wSQL.Language.Services.Executors
{
  public abstract class BaseExecutor : Executor
  {
    public abstract void Run(IList<Token> tokens, Context context);

    //

    protected static void ExpectTokens(ICollection<Token> tokens, int count)
    {
      if (tokens.Count < count + 1)
        throw new Exception(string.Format("Missing argument(s), expected at least {0} but found only {1}.", count, (tokens.Count - 1)));
    }

    protected static object GetValue(Token token, Context context)
    {
      var argument = token;
      var value = argument.Type == TokenType.Identifier ? context.Symbols.Get(argument.Value) : Unquote(argument.Value);

      return value;
    }

    protected static string Unquote(string s)
    {
      return s.Substring(1, s.Length - 2);
    }
  }
}