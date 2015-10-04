using System;
using System.Collections.Generic;
using System.Linq;
using wSQL.Language.Contracts;
using wSQL.Language.Models;

namespace wSQL.Language.Services
{
  public class StatementRunner : Executor
  {
    public void Run(IList<Token> tokens, Context context)
    {
      if (!tokens.Any())
        return;

      switch (tokens[0].Value.ToUpperInvariant())
      {
        case "DECLARE":
          if (tokens.Count < 2)
            throw new Exception("Missing argument(s).");

          foreach (var token in tokens.Skip(1))
            context.Symbols.Declare(token.Value);
          break;

        case "PRINT":
          if (tokens.Count < 2)
            throw new Exception("Missing argument(s).");

          var argument = tokens[1];
          var value = argument.Type == "identifier" ? context.Symbols.Get(argument.Value) : Unquote(argument.Value);
          context.Core.Print(value);
          break;
      }
    }

    //

    private static string Unquote(string s)
    {
      return s.Substring(1, s.Length - 2);
    }
  }
}