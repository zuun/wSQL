using System.Collections.Generic;
using wSQL.Language.Models;

namespace wSQL.Language.Contracts
{
  public interface Executor
  {
    void Run(IList<Token> tokens, Context context);
  }
}