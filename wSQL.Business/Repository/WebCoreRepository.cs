namespace wSQL.Business.Repository
{
  public interface WebCoreRepository
  {
    string OpenPage(string url);
    string OpenPage(string url, string loginPage, string userName, string password);

    void Print(dynamic value);
    dynamic Find(string value, string xpath);
  }
}