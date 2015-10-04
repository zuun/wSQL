using wSQL.Business.Repository;
using wSQL.Language.Contracts;

namespace wSQL.Language.Models
{
  public class Context
  {
    public Symbols Symbols { get; private set; }
    public WebCoreRepository Core { get; private set; }

    public Context(Symbols symbols, WebCoreRepository core)
    {
      Symbols = symbols;
      Core = core;
    }
  }
}