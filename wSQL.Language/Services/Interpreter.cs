using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using wSQL.Business.Repository;
using wSQL.Language.Contracts;
using wSQL.Language.Models;

namespace wSQL.Language.Services
{
  public class Interpreter
  {
    public Interpreter(Symbols symbols, Tokenizer tokenizer)
    {
      this.symbols = symbols;
      this.tokenizer = tokenizer;
    }

    public void Run(string script, WebCoreRepository core)
    {
      var lines = script.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
      foreach (var line in lines)
      {
        var tokens = tokenizer.Parse(line).ToList();
        if (!tokens.Any())
          continue;

        switch (tokens[0].Value.ToUpperInvariant())
        {
          case "DECLARE":
            if (tokens.Count < 2)
              throw new Exception("Missing variable name.");

            foreach (var token in tokens.Skip(1))
              symbols.Declare(token.Value);
            break;

          case "PRINT":
            if (tokens.Count < 2)
              throw new Exception("Missing variable name.");

            var value = symbols.Get(tokens[1].Value);
            core.Print(value);
            break;
        }
      }
    }

    //

    private readonly Symbols symbols;
    private readonly Tokenizer tokenizer;

    private static IEnumerable<Token> Parse(string line)
    {
      var identifiers = Regex.Matches(line, "[A-Za-z_][A-Za-z0-9_]*");
      return identifiers.Cast<Match>().Select(it => new Token(TokenType.Identifier, it.Value));
    }
  }
}