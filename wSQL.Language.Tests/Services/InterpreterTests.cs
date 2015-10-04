using System.Collections;
using System.Collections.Generic;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wSQL.Business.Repository;
using wSQL.Language.Contracts;
using wSQL.Language.Models;
using wSQL.Language.Services;

namespace wSQL.Language.Tests.Services
{
  [TestClass]
  public class InterpreterTests
  {
    private Symbols symbols;
    private Tokenizer tokenizer;
    private Executor executor;
    private Interpreter sut;

    [TestInitialize]
    public void SetUp()
    {
      symbols = A.Fake<Symbols>();
      tokenizer = A.Fake<Tokenizer>();
      executor = A.Fake<Executor>();
      sut = new Interpreter(symbols, tokenizer, executor);
    }

    [TestClass]
    public class _ : InterpreterTests
    {
      [TestMethod]
      public void CallsTheTokenizer()
      {
        sut.Run("some line", null);

        A.CallTo(() => tokenizer.Parse("some line")).MustHaveHappened();
      }

      [TestMethod]
      public void CallsTheExecutor()
      {
        var tokens = new[]
        {
          new Token(TokenType.Identifier, "statement"),
          new Token(TokenType.Identifier, "arg1"),
          new Token(TokenType.Identifier, "arg2"),
          new Token(TokenType.Identifier, "arg3"),
        };
        A.CallTo(() => tokenizer.Parse("statement arg1, arg2, arg3")).Returns(tokens);

        sut.Run("statement arg1, arg2, arg3", null);

        A.CallTo(() => executor.Run(A<IList<Token>>.That.IsSameSequenceAs(tokens), A<Context>.Ignored)).MustHaveHappened();
      }
    }

    [TestClass]
    public class Declare : InterpreterTests
    {
      [TestMethod]
      public void IntepretsDeclareWithSingleName()
      {
        A.CallTo(() => tokenizer
          .Parse("declare abc"))
          .Returns(new[]
          {
            new Token(TokenType.Identifier, "declare"),
            new Token(TokenType.Identifier, "abc"),
          });

        sut.Run("declare abc", null);

        A.CallTo(() => symbols.Declare("abc")).MustHaveHappened();
      }

      [TestMethod]
      public void InterpretsDeclareWithMultipleNames()
      {
        A.CallTo(() => tokenizer
          .Parse("declare a,b, c"))
          .Returns(new[]
          {
            new Token(TokenType.Identifier, "declare"),
            new Token(TokenType.Identifier, "a"),
            new Token(TokenType.Identifier, "b"),
            new Token(TokenType.Identifier, "c"),
          });

        sut.Run("declare a,b, c", null);

        A.CallTo(() => symbols.Declare("a")).MustHaveHappened();
        A.CallTo(() => symbols.Declare("b")).MustHaveHappened();
        A.CallTo(() => symbols.Declare("c")).MustHaveHappened();
      }
    }

    [TestClass]
    public class Print : InterpreterTests
    {
      [TestMethod]
      public void PrintsSingleVariable()
      {
        A.CallTo(() => tokenizer
          .Parse("print abc"))
          .Returns(new[]
          {
            new Token(TokenType.Identifier, "print"),
            new Token(TokenType.Identifier, "abc"),
          });

        A.CallTo(() => symbols.Get("abc")).Returns("def");
        var core = A.Fake<WebCoreRepository>();

        sut.Run("print abc", core);

        A.CallTo(() => core.Print("def")).MustHaveHappened();
      }
    }
  }
}