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
    public Interpreter(Symbols symbols, Tokenizer tokenizer, Executor executor)
    {
      this.symbols = symbols;
      this.tokenizer = tokenizer;
      this.executor = executor;
    }

    public void Run(string script, WebCoreRepository core)
    {
      var context = new Context(symbols, core);

      var tokenizedLines = script
        .Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
        .Select(line => tokenizer.Parse(line));

      foreach (var tokens in tokenizedLines)
        executor.Run(tokens.ToArray(), context);
    }

    //

    private readonly Symbols symbols;
    private readonly Tokenizer tokenizer;
    private readonly Executor executor;

    private void InternalRun(IList<Token> tokens, WebCoreRepository core)
    {
      if (!tokens.Any())
        return;

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
}