using System;
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
  }
}