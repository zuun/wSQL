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
      var symbols = A.Fake<Symbols>();
      var sut = new Interpreter(symbols);
      var core = A.Fake<WebCoreRepository>();

      sut.Run(Resources.Sample1, core);

      A.CallTo(() => symbols.Declare("page")).MustHaveHappened();
      A.CallTo(() => symbols.Declare("list")).MustHaveHappened();
      A.CallTo(() => symbols.Declare("descriptionNodes")).MustHaveHappened();
      A.CallTo(() => symbols.Declare("descriptions")).MustHaveHappened();

      A.CallTo(() => core.Load("https://www.google.com/search?q=something+to+search&cad=cbv&sei=drQPVuWtDoS7acCjptgG")).MustHaveHappened();
      A.CallTo(() => symbols.Set("page", A<object>.Ignored)).MustHaveHappened();

      A.CallTo(() => symbols.Get("page")).MustHaveHappened();
      A.CallTo(() => core.Print(A<string>.Ignored)).MustHaveHappened();

      A.CallTo(() => core.Find(A<string>.Ignored, "//div[class='srg']/div[class='g pb']")).MustHaveHappened();
      A.CallTo(() => symbols.Set("list", A<object>.Ignored));

      A.CallTo(() => symbols.Get("list")).MustHaveHappened();
      A.CallTo(() => core.Find(A<string>.Ignored, "div[class='st']"));
      A.CallTo(() => symbols.Set("descriptionNodes", A<object>.Ignored)).MustHaveHappened();

      A.CallTo(() => symbols.Get("descriptionNodes")).MustHaveHappened();
      A.CallTo(() => symbols.Set("descriptions", A<object>.Ignored)).MustHaveHappened();

      A.CallTo(() => symbols.Get("descriptions")).MustHaveHappened();
      A.CallTo(() => core.Print(A<object>.Ignored)).MustHaveHappened();
    }
  }
}