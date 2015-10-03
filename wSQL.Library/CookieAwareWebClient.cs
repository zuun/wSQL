using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace wSQL.Library
{
   public class CookieAwareWebClient : WebClient
   {
      public CookieContainer Cookies { get { return cc; } }

      public CookieAwareWebClient()
      {
         cc = new CookieContainer();
      }

      public CookieAwareWebClient(CookieCollection cookies)
      {
         cc = new CookieContainer();
         cc.Add(cookies);
      }

      //

      protected override WebRequest GetWebRequest(System.Uri address)
      {
         WebRequest R = base.GetWebRequest(address);
         if (R is HttpWebRequest)
         {
            HttpWebRequest WR = (HttpWebRequest)R;
            WR.CookieContainer = cc;

            if (lastPage != null)
               WR.Referer = lastPage;
         }

         lastPage = address.ToString();
         return R;
      }

      private readonly CookieContainer cc;

      private string lastPage;
   }
}
