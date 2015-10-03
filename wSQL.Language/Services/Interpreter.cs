using System;
using System.Collections.Generic;
using System.Linq;
using wSQL.Business.Repository;
using wSQL.Language.Contracts;
using wSQL.Language.Models;

namespace wSQL.Language.Services
{
  public class Interpreter
  {
    public Interpreter(Symbols symbols)
    {
      this.symbols = symbols;
    }

    public void Run(string script, WebCoreRepository core)
    {
      var lines = script.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
      foreach (var line in lines)
      {
        var tokens = Parse(line).ToList();
        if (!tokens.Any())
          continue;

        switch (tokens[0].Value.ToUpperInvariant())
        {
          case "DECLARE":
            if (tokens.Count < 2)
              throw new Exception("Missing variable name.");

            symbols.Declare(tokens[1].Value);
            break;
        }
      }
    }

    //

    private readonly Symbols symbols;

    private static IEnumerable<Token> Parse(string line)
    {
      return line
        .Split(' ')
        .Select(it => new Token(TokenType.Identifier, it));
    }
  }
}