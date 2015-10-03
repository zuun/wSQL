using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wSQL.Business.Repository;

namespace wSQL.Business.Contracts
{
   public class RuntimeCore : RuntimeCoreRepository
   {
      public void RunScript(string script)
      {
         throw new NotImplementedException();
      }
   }
}
