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

      symbols = new SymbolsTable();
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

        Assert.IsTrue(symbols.Exists("abc"));
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

        Assert.IsTrue(symbols.Exists("a"));
        Assert.IsTrue(symbols.Exists("b"));
        Assert.IsTrue(symbols.Exists("c"));
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
        symbols.Declare("abc");
        symbols.Set("abc", "def");

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
        symbols.Declare("abc");
        symbols.Set("abc", "def");

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
        symbols.Declare("a");

        sut.Run(tokens, context);

        Assert.AreEqual("abc", symbols.Get("a"));
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
        symbols.Declare("a");
        symbols.Declare("b");
        symbols.Set("b", "def");

        sut.Run(tokens, context);

        Assert.AreEqual("def", symbols.Get("a"));
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
        symbols.Declare("a");
        A.CallTo(() => core.OpenPage("abc")).Returns("def");

        sut.Run(tokens, context);

        Assert.AreEqual("def", symbols.Get("a"));
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
        symbols.Declare("page");
        symbols.Set("page", "abc");

        sut.Run(tokens, context);

        A.CallTo(() => core.Find("abc", "//div")).MustHaveHappened();
      }
    }

    [TestClass]
    public class Flatten : StatementRunnerTests
    {
      [TestMethod]
      public void FlattensListOfLists()
      {
        var tokens = new[]
        {
          new Token(TokenType.Identifier, "flatten"),
          new Token(TokenType.OpenPar, "("),
          new Token(TokenType.Identifier, "a"),
          new Token(TokenType.ClosedPar, ")"),
        };
        symbols.Declare("a");
        symbols.Set("a", new[] {new[] {1, 2}, new[] {3, 4}, new[] {5, 6},});

        var result = sut.Run(tokens, context);

        CollectionAssert.AreEqual(new[] {1, 2, 3, 4, 5, 6}, result);
      }
    }

    [TestClass]
    public class Accessor : StatementRunnerTests
    {
      [TestMethod]
      public void ReturnsValueOfProperty()
      {
        var tokens = new[]
        {
          new Token(TokenType.Identifier, "a"),
          new Token(TokenType.Access, "."),
          new Token(TokenType.Identifier, "b"),
        };
        symbols.Declare("a");
        symbols.Set("a", new {b = "cde"});

        var result = sut.Run(tokens, context);

        Assert.AreEqual("cde", result);
      }
    }

    [TestClass]
    public class Map : StatementRunnerTests
    {
      [TestMethod]
      public void ReturnsListOfConstants()
      {
        var tokens = new[]
        {
          new Token(TokenType.Identifier, "map"),
          new Token(TokenType.OpenPar, "("),
          new Token(TokenType.Identifier, "list"),
          new Token(TokenType.Comma, ","),
          new Token(TokenType.Identifier, "it"),
          new Token(TokenType.Lambda, "=>"),
          new Token(TokenType.String, "\"x\""),
          new Token(TokenType.ClosedPar, ")"),
        };
        symbols.Declare("list");
        symbols.Set("list", new[] {"1", "2", "3"});

        var result = sut.Run(tokens, context);

        CollectionAssert.AreEqual(new[] {"x", "x", "x"}, result);
      }

      [TestMethod]
      public void ReturnsListOfItems()
      {
        var tokens = new[]
        {
          new Token(TokenType.Identifier, "map"),
          new Token(TokenType.OpenPar, "("),
          new Token(TokenType.Identifier, "list"),
          new Token(TokenType.Comma, ","),
          new Token(TokenType.Identifier, "it"),
          new Token(TokenType.Lambda, "=>"),
          new Token(TokenType.Identifier, "it"),
          new Token(TokenType.ClosedPar, ")"),
        };
        symbols.Declare("list");
        symbols.Set("list", new[] {"1", "2", "3"});

        var result = sut.Run(tokens, context);

        CollectionAssert.AreEqual(new[] {"1", "2", "3"}, result);
      }

      [TestMethod]
      public void ReturnsListOfItemPropertiess()
      {
        var tokens = new[]
        {
          new Token(TokenType.Identifier, "map"),
          new Token(TokenType.OpenPar, "("),
          new Token(TokenType.Identifier, "list"),
          new Token(TokenType.Comma, ","),
          new Token(TokenType.Identifier, "it"),
          new Token(TokenType.Lambda, "=>"),
          new Token(TokenType.Identifier, "it"),
          new Token(TokenType.Access, "."),
          new Token(TokenType.Identifier, "X"),
          new Token(TokenType.ClosedPar, ")"),
        };
        symbols.Declare("list");
        symbols.Set("list", new[] {new {X = "1"}, new {X = "2"}, new {X = "3"},});

        var result = sut.Run(tokens, context);

        CollectionAssert.AreEqual(new[] {"1", "2", "3"}, result);
      }
    }
  }
}