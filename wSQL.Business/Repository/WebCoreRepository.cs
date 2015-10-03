using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wSQL.Business
{
   public interface WebCoreRepository
   {

      string OpenPage(string url);
      string OpenPage(string url, string loginPage, string userName, string password);
   }
}
