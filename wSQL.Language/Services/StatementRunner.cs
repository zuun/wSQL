﻿using System;
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
        {"declare", new Declare(this)},
        {"print", new Print(this)},
        {"load", new Load(this)},
        {"set", new Set(this)},
        {"find", new Find(this)},
        {"flatten", new Flatten(this)},
      };
      variable = new Variable(this);
      stringConstant = new StringConstant(this);
    }

    public dynamic Run(IList<Token> tokens, Context context)
    {
      if (!tokens.Any())
        return null;

      var name = tokens[0].Value;

      // precedence: string constant, statement/function, variable

      if (tokens[0].Type == TokenType.String)
        return stringConstant.Run(tokens, context);

      if (executors.ContainsKey(name))
      {
        var executor = executors[name];
        return executor.Run(tokens, context);
      }

      if (context.Symbols.Exists(name))
        return variable.Run(tokens, context);

      // if all failed, assume it's a mispelled statement name
      throw new Exception("Unknown statement: " + name);
    }

    //

    private readonly Dictionary<string, Executor> executors;
    private readonly Executor variable;
    private readonly Executor stringConstant;
  }
}