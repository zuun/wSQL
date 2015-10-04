﻿using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wSQL.Business.Repository;
using wSQL.Language.Contracts;
using wSQL.Language.Models;
using wSQL.Language.Services;

namespace wSQL.Language.Tests.Services
{
  [TestClass]
  public class StatementRunnerTests
  {
    private StatementRunner sut;

    private Symbols symbols;
    private WebCoreRepository core;
    private Context context;

    [TestInitialize]
    public void SetUp()
    {
      sut = new StatementRunner();

      symbols = A.Fake<Symbols>();
      core = A.Fake<WebCoreRepository>();
      context = new Context(symbols, core);
    }

    [TestClass]
    public class Declare : StatementRunnerTests
    {
      [TestMethod]
      public void IntepretsDeclareWithSingleName()
      {
        var tokens = new[]
        {
          new Token(TokenType.Identifier, "declare"),
          new Token(TokenType.Identifier, "abc"),
        };

        sut.Run(tokens, context);

        A.CallTo(() => symbols.Declare("abc")).MustHaveHappened();
      }

      [TestMethod]
      public void InterpretsDeclareWithMultipleNames()
      {
        var tokens = new[]
        {
          new Token(TokenType.Identifier, "declare"),
          new Token(TokenType.Identifier, "a"),
          new Token(TokenType.Identifier, "b"),
          new Token(TokenType.Identifier, "c"),
        };

        sut.Run(tokens, context);

        A.CallTo(() => symbols.Declare("a")).MustHaveHappened();
        A.CallTo(() => symbols.Declare("b")).MustHaveHappened();
        A.CallTo(() => symbols.Declare("c")).MustHaveHappened();
      }
    }

    [TestClass]
    public class Print : StatementRunnerTests
    {
      [TestMethod]
      public void PrintsSingleVariable()
      {
        var tokens = new[]
        {
          new Token(TokenType.Identifier, "print"),
          new Token(TokenType.Identifier, "abc"),
        };

        A.CallTo(() => symbols.Get("abc")).Returns("def");

        sut.Run(tokens, context);

        A.CallTo(() => core.Print("def")).MustHaveHappened();
      }
    }
  }
}