using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wSQL.Business.Repository;

namespace wSQL.Business.Contracts
{
   public class RuntimeCore : RuntimeCoreRepository
   {
      private WebCoreRepository webCore;

      public RuntimeCore()
      {
         webCore = new WebCore();
      }

      public DynamicObject RunScript(string script)
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
