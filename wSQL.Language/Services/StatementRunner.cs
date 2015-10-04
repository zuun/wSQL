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

          context.Core.Print(GetValue(tokens[1], context));
          break;

        case "LOAD":
          if (tokens.Count < 4)
            throw new Exception("Missing argument(s).");
          if (tokens[1].Type != TokenType.OpenPar || tokens[3].Type != TokenType.ClosedPar)
            throw new Exception("Invalid syntax.");

          context.Core.Load(GetValue(tokens[2], context).ToString());
          break;
      }
    }

    //

    private static object GetValue(Token token, Context context)
    {
      var argument = token;
      var value = argument.Type == TokenType.Identifier ? context.Symbols.Get(argument.Value) : Unquote(argument.Value);

      return value;
    }

    private static string Unquote(string s)
    {
      return s.Substring(1, s.Length - 2);
    }
  }
}