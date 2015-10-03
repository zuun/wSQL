using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wSQL.Library;

namespace wSQL.Business.Contracts
{
   public class WebCore : WebCoreRepository
   {
      public string OpenPage(string url)
      {
         using (var web = new CookieAwareWebClient())
         {
            return web.DownloadString(url);
         }
      }

      public string OpenPage(string url, string loginPage, string userName, string password)
      {
         using (var web = new CookieAwareWebClient())
         {
            var response = web.DownloadString(loginPage);
            web.Headers["Content-Type"] = "application/x-www-form-urlencoded";
            response = web.UploadString(loginPage, string.Format("UserName={0}&Password={1}&__RequestVerificationToken=", userName, password) + extractValidationToken(response));

            response = web.DownloadString(url);

            return response;
         }
      }

      private string extractValidationToken(string page)
      {
         var startIndex = page.IndexOf("__RequestVerificationToken");
         startIndex = page.IndexOf("value=", startIndex) + 7;
         var endIndex = page.IndexOf("\"", startIndex);
         return page.Substring(startIndex, endIndex - startIndex);
      }
   }
}
