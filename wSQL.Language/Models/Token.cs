namespace wSQL.Language.Models
{
  public class Token
  {
    public TokenType Type { get; private set; }
    public string Value { get; private set; }

    public Token(TokenType type, string value)
    {
      Type = type;
      Value = value;
    }

    public override bool Equals(object obj)
    {
      var other = obj as Token;
      return other != null && Type == other.Type && Value == other.Value;
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return ((int) Type * 397) ^ (Value != null ? Value.GetHashCode() : 0);
      }
    }
  }
}