using System;
using System.Collections.Generic;
using System.Linq;
using wSQL.Language.Contracts;
using wSQL.Language.Models;
using wSQL.Language.Services.Executors;

namespace wSQL.Language.Services
{
  public class StatementRunner : Executor
  {
    public StatementRunner()
    {
      executors = new Dictionary<string, Executor>
      {
        {"DECLARE", new Declare()},
        {"PRINT", new Print()},
        {"LOAD", new Load()},
        {"SET", new Set()},
      };
    }

    public void Run(IList<Token> tokens, Context context)
    {
      if (!tokens.Any())
        return;

      var name = tokens[0].Value.ToUpperInvariant();
      if (!executors.ContainsKey(name))
        throw new Exception("Unknown statement: " + name);

      var executor = executors[name];
      executor.Run(tokens, context);
    }

    //

    private readonly Dictionary<string, Executor> executors;
  }
}