using System.Collections.Generic;
using wSQL.Language.Models;

namespace wSQL.Language.Contracts
{
  public interface Tokenizer
  {
    IEnumerable<Token> Parse(string s);
  }
}