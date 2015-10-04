using System.Collections.Generic;
using wSQL.Language.Models;

namespace wSQL.Language.Contracts
{
  public interface Tokenizer
  {
    void AddDefinition(TokenDefinition tokenDefinition);
    IEnumerable<Token> Parse(string source);
  }
}