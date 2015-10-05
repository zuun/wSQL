using System.Dynamic;
using wSQL.Business.Repository;

namespace wSQL.Business.Services
{
   public class RuntimeCore : RuntimeCoreRepository
   {
      private WebCoreRepository webCore;

      public RuntimeCore()
      {
         webCore = new WebCore();
      }

      public dynamic RunScript(string script)
      {
         //TODO: validate script
         //parse escript
         //execute 
         //return

         var pageContent = webCore.OpenPage("https://en.wikipedia.org/wiki/Solar_System");

         //"//div[class='srg']/div[class='g pb']"
         var xPath = "//table[@class='infobox']//tr";
         xPath = "//div[@id='toc']//ul//span[@class='toctext']";
         xPath = "//div[@id='toc']//ul";
         pageContent = webCore.Find(pageContent, xPath);

         xPath = ".//li";
         pageContent = webCore.Find(pageContent, xPath);
         dynamic response = new ExpandoObject();
         var text = webCore.ExtractText(pageContent, ".//li");

         response.Text = text;
         response.PageContent = pageContent;// beautifyHTML(pageContent);

         return response;
      }
      
   }
}
