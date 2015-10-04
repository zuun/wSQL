using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using wSQL.Language.Contracts;
using wSQL.Language.Models;

namespace wSQL.Language.Services
{
  public class Lexer : Tokenizer
  {
    public IEnumerable<Token> Parse(string s)
    {
      var identifiers = Regex.Matches(s, "[A-Za-z_][A-Za-z0-9_]*");
      return identifiers.Cast<Match>().Select(it => new Token(TokenType.Identifier, it.Value));
    }
  }
}