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

      object rhs;
      switch (tokens[0].Value.ToUpperInvariant())
      {
        case "DECLARE":
          ExpectTokens(tokens, 1);

          foreach (var token in tokens.Skip(1))
          {
            context.Symbols.Declare(token.Value);
          }
          break;

        case "PRINT":
          ExpectTokens(tokens, 1);

          rhs = GetValue(tokens[1], context);
          context.Core.Print(rhs);
          break;

        case "LOAD":
          ExpectTokens(tokens, 3);

          if (tokens[1].Type != TokenType.OpenPar || tokens[3].Type != TokenType.ClosedPar)
            throw new Exception("Invalid syntax.");

          rhs = GetValue(tokens[2], context);
          context.Core.OpenPage(rhs.ToString());
          break;

        case "SET":
          ExpectTokens(tokens, 3);
          
          if (tokens[2].Type != TokenType.Assignment)
            throw new Exception("Invalid syntax.");

          var lhs = tokens[1].Value;
          rhs = GetValue(tokens[3], context);
          context.Symbols.Set(lhs, rhs);
          break;
      }
    }

    //

    private static void ExpectTokens(IList<Token> tokens, int count)
    {
      if (tokens.Count < count + 1)
        throw new Exception("Missing argument(s).");
    }

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