using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

    [TestMethod]
    public void IntepretsDeclareWithSingleName()
    {
      sut.Run("declare abc", null);

      A.CallTo(() => symbols.Declare("abc")).MustHaveHappened();
    }
  }
}