namespace wSQL.Language.Contracts
{
  public interface Symbols
  {
    void Declare(string name);
    void Set(string name, object value);
    object Get(string name);
    bool Exists(string name);
  }
}