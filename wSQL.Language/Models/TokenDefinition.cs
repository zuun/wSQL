using System.Text.RegularExpressions;

namespace wSQL.Language.Models
{
  public class TokenDefinition
  {
    public string Type { get; private set; }
    public bool IsIgnored { get; private set; }
    public Regex Regex { get; private set; }

    public TokenDefinition(string type, bool isIgnored, Regex regex)
    {
      Type = type;
      IsIgnored = isIgnored;
      Regex = regex;
    }
  }
}