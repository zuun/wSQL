using wSQL.Data.Models;

namespace wSQL.Business.Repository
{
   public interface WebCoreRepository
   {
      string OpenPage(string url);
      string OpenPage(string url, string loginPage, string userName, string password);
      string OpenFile(string fileName);

      void WriteToFile(dynamic value, dynamic fileObject, dynamic itemSeparator, dynamic lineEnd);

      void Print(dynamic value);
      void PrintList(dynamic value, dynamic itemSeparator, dynamic lineEnd);
      dynamic Find(string value, string xpath);
      dynamic ExtractText(string value, string xpath);
   }
}