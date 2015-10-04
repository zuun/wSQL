using FakeItEasy;
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
        A.CallTo(() => symbols.Exists("abc")).Returns(true);
        A.CallTo(() => symbols.Get("abc")).Returns("def");

        sut.Run(tokens, context);

        A.CallTo(() => core.Print("def")).MustHaveHappened();
      }

      [TestMethod]
      public void PrintsStringArgument()
      {
        var tokens = new[]
        {
          new Token(TokenType.Identifier, "print"),
          new Token(TokenType.String, "\"abc def\""),
        };

        sut.Run(tokens, context);

        A.CallTo(() => core.Print("abc def")).MustHaveHappened();
      }
    }

    [TestClass]
    public class Load : StatementRunnerTests
    {
      [TestMethod]
      public void LoadsPageFromConstantStringUrl()
      {
        var tokens = new[]
        {
          new Token(TokenType.Identifier, "load"),
          new Token(TokenType.OpenPar, "("),
          new Token(TokenType.String, "\"abc def\""),
          new Token(TokenType.ClosedPar, ")"),
        };

        sut.Run(tokens, context);

        A.CallTo(() => core.OpenPage("abc def")).MustHaveHappened();
      }

      [TestMethod]
      public void LoadsPageFromVariableUrl()
      {
        var tokens = new[]
        {
          new Token(TokenType.Identifier, "load"),
          new Token(TokenType.OpenPar, "("),
          new Token(TokenType.Identifier, "abc"),
          new Token(TokenType.ClosedPar, ")"),
        };
        A.CallTo(() => symbols.Exists("abc")).Returns(true);
        A.CallTo(() => symbols.Get("abc")).Returns("def");

        sut.Run(tokens, context);

        A.CallTo(() => core.OpenPage("def")).MustHaveHappened();
      }

      [TestMethod]
      public void ReturnsValueFromCore()
      {
        var tokens = new[]
        {
          new Token(TokenType.Identifier, "load"),
          new Token(TokenType.OpenPar, "("),
          new Token(TokenType.String, "\"abc def\""),
          new Token(TokenType.ClosedPar, ")"),
        };
        A.CallTo(() => core.OpenPage("abc def")).Returns("def");

        var result = sut.Run(tokens, context);

        Assert.AreEqual("def", result);
      }
    }

    [TestClass]
    public class Set : StatementRunnerTests
    {
      [TestMethod]
      public void SetsVariableToConstantValue()
      {
        var tokens = new[]
        {
          new Token(TokenType.Identifier, "set"),
          new Token(TokenType.Identifier, "a"),
          new Token(TokenType.Assignment, "="),
          new Token(TokenType.String, "\"abc\""),
        };

        sut.Run(tokens, context);

        A.CallTo(() => symbols.Set("a", "abc")).MustHaveHappened();
      }

      [TestMethod]
      public void SetsVariableToAnotherVariable()
      {
        var tokens = new[]
        {
          new Token(TokenType.Identifier, "set"),
          new Token(TokenType.Identifier, "a"),
          new Token(TokenType.Assignment, "="),
          new Token(TokenType.Identifier, "b"),
        };
        A.CallTo(() => symbols.Exists("b")).Returns(true);
        A.CallTo(() => symbols.Get("b")).Returns("def");

        sut.Run(tokens, context);

        A.CallTo(() => symbols.Set("a", "def")).MustHaveHappened();
      }

      [TestMethod]
      public void SetsVariableToResultOfLoadCall()
      {
        var tokens = new[]
        {
          new Token(TokenType.Identifier, "set"),
          new Token(TokenType.Identifier, "a"),
          new Token(TokenType.Assignment, "="),
          new Token(TokenType.Identifier, "load"),
          new Token(TokenType.OpenPar, "("),
          new Token(TokenType.String, "\"abc\""),
          new Token(TokenType.ClosedPar, ")"),
        };
        A.CallTo(() => core.OpenPage("abc")).Returns("def");

        sut.Run(tokens, context);

        A.CallTo(() => symbols.Set("a", "def")).MustHaveHappened();
      }
    }

    [TestClass]
    public class Find : StatementRunnerTests
    {
      [TestMethod]
      public void LoadsVariable()
      {
        var tokens = new[]
        {
          new Token(TokenType.Identifier, "find"),
          new Token(TokenType.OpenPar, "("),
          new Token(TokenType.Identifier, "page"),
          new Token(TokenType.String, "\"//div\""),
          new Token(TokenType.ClosedPar, ")"),
        };
        A.CallTo(() => symbols.Exists("page")).Returns(true);
        A.CallTo(() => symbols.Get("page")).Returns("abc");

        sut.Run(tokens, context);

        A.CallTo(() => core.Find("abc", "//div")).MustHaveHappened();
      }
    }
  }
}