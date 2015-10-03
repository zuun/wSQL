using wSQL.Business.Repository;
using wSQL.Language.Contracts;

namespace wSQL.Language.Services
{
  public class Interpreter
  {
    public Interpreter(Symbols symbols)
    {
      this.symbols = symbols;
    }

    public void Run(string script, WebCoreRepository core)
    {
      //
    }

    //

    private Symbols symbols;
  }
}