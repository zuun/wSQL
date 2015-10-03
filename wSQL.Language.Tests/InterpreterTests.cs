using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wSQL.Business.Repository;
using wSQL.Language.Contracts;
using wSQL.Language.Services;

namespace wSQL.Language.Tests
{
  [TestClass]
  public class InterpreterTests
  {
    private Symbols symbols;
    private Interpreter sut;

    [TestInitialize]
    public void SetUp()
    {
      symbols = A.Fake<Symbols>();
      sut = new Interpreter(symbols);
    }

    [TestClass]
    public class Declare : InterpreterTests
    {
      [TestMethod]
      public void IntepretsDeclareWithSingleName()
      {
        sut.Run("declare abc", null);

        A.CallTo(() => symbols.Declare("abc")).MustHaveHappened();
      }

      [TestMethod]
      public void InterpretsDeclareWithMultipleNames()
      {
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
        A.CallTo(() => symbols.Get("abc")).Returns("def");
        var core = A.Fake<WebCoreRepository>();

        sut.Run("print abc", core);

        A.CallTo(() => core.Print("def")).MustHaveHappened();
      }
    }
  }
}