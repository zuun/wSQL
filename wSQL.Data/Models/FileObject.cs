using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wSQL.Data.Models
{
   public class FileObject
   {
      public FileObject() { }
      public FileObject(string fileName, string fileMode)
      {
         FileName = fileName;
         FileMode = fileMode;
      }

      public string FileName { get; set; }
      public string FileMode { get; set; }
   }
}
