﻿namespace wSQL.Business.Repository
{
  public interface WebCoreRepository
  {
    string OpenPage(string url);
    string OpenPage(string url, string loginPage, string userName, string password);

    string Load(string url);
    
    void Declare(string name);
    void Set(string name, dynamic value);
    dynamic Get(string name);
    
    void Print(dynamic value);
    dynamic Find(string value, string xpath);
  }
}