﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wSQL.Data.Models
{
   public class FileDetails
   {
      public string FileName { get; set; }
      public string FullPath { get; set; }
      public bool Saved { get; set; }
   }
}
