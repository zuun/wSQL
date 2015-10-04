using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wSQL.Business.Repository;
using wSQL.Language.Contracts;
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
      var tokenizer = A.Fake<Tokenizer>();
      var sut = new Interpreter(symbols, tokenizer);
      var core = A.Fake<WebCoreRepository>();

      sut.Run(Resources.Sample1, core);

      A.CallTo(() => core.OpenPage("https://www.google.com/search?q=something+to+search&cad=cbv&sei=drQPVuWtDoS7acCjptgG")).MustHaveHappened();
      A.CallTo(() => core.Print(A<string>.Ignored)).MustHaveHappened();
      A.CallTo(() => core.Find(A<string>.Ignored, "//div[class='srg']/div[class='g pb']")).MustHaveHappened();
      A.CallTo(() => core.Find(A<string>.Ignored, "div[class='st']"));
      A.CallTo(() => core.Print(A<object>.Ignored)).MustHaveHappened();
    }
  }
}