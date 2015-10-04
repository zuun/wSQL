using System.Collections.Generic;
using wSQL.Language.Models;

namespace wSQL.Language.Contracts
{
  public interface Executor
  {
    dynamic Run(IList<Token> tokens, Context context);
  }
}