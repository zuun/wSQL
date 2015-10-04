using System.Collections.Generic;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        new Token("identifier", "statement"),
        new Token("identifier", "arg1"),
        new Token("identifier", "arg2"),
        new Token("identifier", "arg3"),
      };
      A.CallTo(() => tokenizer.Parse("statement arg1, arg2, arg3")).Returns(tokens);

      sut.Run("statement arg1, arg2, arg3", null);

      A.CallTo(() => executor.Run(A<IList<Token>>.That.IsSameSequenceAs(tokens), A<Context>.Ignored)).MustHaveHappened();
    }
  }
}