﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wSQL.Business.Repository
{
   public interface RuntimeCoreRepository
   {

      DynamicObject RunScript(string script);
   }
}
