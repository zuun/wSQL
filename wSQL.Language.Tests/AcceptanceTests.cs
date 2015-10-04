using System.Text.RegularExpressions;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wSQL.Business.Repository;
using wSQL.Language.Models;
using wSQL.Language.Services;
using wSQL.Language.Tests.Properties;

namespace wSQL.Language.Tests
{
  [TestClass]
  public class AcceptanceTests
  {
    [TestMethod]
    public void CodeSample1()
    {
      var symbols = new SymbolsTable();
      var tokenizer = CreateLexer();
      var executor = new StatementRunner();
      var sut = new Interpreter(symbols, tokenizer, executor);
      var core = A.Fake<WebCoreRepository>();

      sut.Run(Resources.Sample1, core);

      A.CallTo(() => core.Print("Hello, World")).MustHaveHappened();
    }

    [TestMethod]
    public void CodeSample2()
    {
      var symbols = new SymbolsTable();
      var tokenizer = CreateLexer();
      var executor = new StatementRunner();
      var sut = new Interpreter(symbols, tokenizer, executor);
      var core = A.Fake<WebCoreRepository>();

      sut.Run(Resources.Sample1, core);

      A.CallTo(() => core.OpenPage("https://www.google.com/search?q=something+to+search&cad=cbv&sei=drQPVuWtDoS7acCjptgG")).MustHaveHappened();
      A.CallTo(() => core.Print(A<string>.Ignored)).MustHaveHappened();
      A.CallTo(() => core.Find(A<string>.Ignored, "//div[class='srg']/div[class='g pb']")).MustHaveHappened();
      A.CallTo(() => core.Find(A<string>.Ignored, "div[class='st']"));
      A.CallTo(() => core.Print(A<object>.Ignored)).MustHaveHappened();
    }

    //

    private static Lexer CreateLexer()
    {
      var tokenizer = new Lexer();
      tokenizer.AddDefinition(new TokenDefinition(TokenType.Identifier, false, new Regex("[A-Za-z_][A-Za-z0-9_]*")));
      tokenizer.AddDefinition(new TokenDefinition(TokenType.Separator, true, new Regex("[ ,]")));
      tokenizer.AddDefinition(new TokenDefinition(TokenType.Lambda, false, new Regex("=>")));
      tokenizer.AddDefinition(new TokenDefinition(TokenType.Assignment, false, new Regex("=")));
      tokenizer.AddDefinition(new TokenDefinition(TokenType.String, false, new Regex("\"[^\"]*\"")));
      tokenizer.AddDefinition(new TokenDefinition(TokenType.OpenPar, false, new Regex("[(]")));
      tokenizer.AddDefinition(new TokenDefinition(TokenType.ClosedPar, false, new Regex("[)]")));
      tokenizer.AddDefinition(new TokenDefinition(TokenType.Access, false, new Regex("[.]")));

      return tokenizer;
    }
  }
}