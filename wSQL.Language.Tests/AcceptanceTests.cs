﻿using System.Collections;
using System.Text;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wSQL.Business.Repository;
using wSQL.Business.Services;
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
      var tokenizer = DefaultLexer.Create();
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
      var tokenizer = DefaultLexer.Create();
      var executor = new StatementRunner();
      var sut = new Interpreter(symbols, tokenizer, executor);
      var core = new WebCore();

      var sb = new StringBuilder();
      core.OnPrint += (_, value) =>
      {
        var list = value as IEnumerable;
        if (list == null)
          sb.Append(value.ToString());
        else
        {
          foreach (var item in list)
            sb.AppendLine(item.ToString());
        }
      };

      sut.Run(Resources.Sample2, core);

      var expected = Resources.Expected2.Replace("\\n", "\n");
      var result = sb.ToString();
      Assert.AreEqual(expected, result);

      //A.CallTo(() => core.OpenPage("https://www.google.com/search?q=something+to+search&cad=cbv&sei=drQPVuWtDoS7acCjptgG")).MustHaveHappened();
      //A.CallTo(() => core.Print(A<string>.Ignored)).MustHaveHappened();
      //A.CallTo(() => core.Find(A<string>.Ignored, "//div[class='srg']/div[class='g pb']")).MustHaveHappened();
      //A.CallTo(() => core.Find(A<string>.Ignored, "div[class='st']"));
      //A.CallTo(() => core.Print(A<object>.Ignored)).MustHaveHappened();
    }
  }
}