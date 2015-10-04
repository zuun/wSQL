using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wSQL.Business.Repository;
using wSQL.Language.Contracts;
using wSQL.Language.Models;
using wSQL.Language.Services;

namespace wSQL.Language.Tests
{
  [TestClass]
  public class InterpreterTests
  {
    private Symbols symbols;
    private Tokenizer tokenizer;
    private Interpreter sut;

    [TestInitialize]
    public void SetUp()
    {
      symbols = A.Fake<Symbols>();
      tokenizer = A.Fake<Tokenizer>();
      sut = new Interpreter(symbols, tokenizer);
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