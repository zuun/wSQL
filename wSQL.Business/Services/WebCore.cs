using System;
using HtmlAgilityPack;
using System.IO;
using wSQL.Business.Repository;
using wSQL.Library;

namespace wSQL.Business.Services
{
   public class WebCore : WebCoreRepository
   {
      public event EventHandler<dynamic> OnPrint;

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
            response = web.UploadString(loginPage,
              string.Format("UserName={0}&Password={1}&__RequestVerificationToken=", userName, password) + extractValidationToken(response));

            response = web.DownloadString(url);

            return response;
         }
      }

      public void Print(dynamic value)
      {
          if (OnPrint != null)
              OnPrint(this, value);
      }

      public dynamic Find(string value, string xpath)
      {
         HtmlDocument document = new HtmlDocument();
         document.Load(new StringReader(value));
         return document.DocumentNode.SelectNodes(xpath);
      }

      public dynamic ExtractText(string value, string xpath)
      {
         string html = "";

         HtmlDocument document = new HtmlDocument();
         document.Load(new StringReader(value));
         var nodes = document.DocumentNode.SelectNodes(xpath);
        
         if (nodes != null)
            foreach (var node in nodes)
               html += node.InnerText + System.Environment.NewLine;

         return html;
      }

      //

      private string extractValidationToken(string page)
      {
         var startIndex = page.IndexOf("__RequestVerificationToken");
         startIndex = page.IndexOf("value=", startIndex) + 7;
         var endIndex = page.IndexOf("\"", startIndex);
         return page.Substring(startIndex, endIndex - startIndex);
      }
   }
}