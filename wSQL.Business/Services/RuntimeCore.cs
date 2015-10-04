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

         var pageContent = webCore.OpenPage("http://www.google.com");

         dynamic response = new ExpandoObject();
         response.PageContent = pageContent;

         return response;
      }
   }
}
