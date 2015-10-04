using System.Text.RegularExpressions;

namespace wSQL.Language.Models
{
  public class TokenDefinition
  {
    public TokenType Type { get; private set; }
    public bool IsIgnored { get; private set; }
    public Regex Regex { get; private set; }

    public TokenDefinition(TokenType type, bool isIgnored, Regex regex)
    {
      Type = type;
      IsIgnored = isIgnored;
      Regex = regex;
    }
  }
}