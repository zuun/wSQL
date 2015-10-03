using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wSQL.Business.Repository;
using wSQL.Language.Tests.Properties;

namespace wSQL.Language.Tests
{
  [TestClass]
  public class AcceptanceTests
  {
    [TestMethod]
    public void CodeSample1()
    {
      var sut = new Interpreter();
      var core = A.Fake<WebCoreRepository>();

      sut.Run(Resources.Sample1, core);

      A.CallTo(() => core.Declare("page")).MustHaveHappened();
      A.CallTo(() => core.Declare("list")).MustHaveHappened();
      A.CallTo(() => core.Declare("descriptionNodes")).MustHaveHappened();
      A.CallTo(() => core.Declare("descriptions")).MustHaveHappened();

      A.CallTo(() => core.Load("https://www.google.com/search?q=something+to+search&cad=cbv&sei=drQPVuWtDoS7acCjptgG")).MustHaveHappened();
      A.CallTo(() => core.Set("page", A<string>.Ignored)).MustHaveHappened();

      A.CallTo(() => core.Get("page")).MustHaveHappened();
      A.CallTo(() => core.Print(A<string>.Ignored)).MustHaveHappened();

      A.CallTo(() => core.Find(A<string>.Ignored, "//div[class='srg']/div[class='g pb']")).MustHaveHappened();
      A.CallTo(() => core.Set("list", A<object>.Ignored));

      A.CallTo(() => core.Get("list")).MustHaveHappened();
      A.CallTo(() => core.Find(A<string>.Ignored, "div[class='st']"));
      A.CallTo(() => core.Set("descriptionNodes", A<object>.Ignored)).MustHaveHappened();

      A.CallTo(() => core.Get("descriptionNodes")).MustHaveHappened();
      A.CallTo(() => core.Set("descriptions", A<object>.Ignored)).MustHaveHappened();

      A.CallTo(() => core.Get("descriptions")).MustHaveHappened();
      A.CallTo(() => core.Print(A<object>.Ignored)).MustHaveHappened();
    }
  }
}