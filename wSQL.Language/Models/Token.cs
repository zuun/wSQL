namespace wSQL.Language.Models
{
  public class Token
  {
    // ReSharper disable once InconsistentNaming
    public static Token EOF = new Token("(end)", null);

    public string Type { get; private set; }
    public string Value { get; private set; }

    public Token(string type, string value)
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
        return ((Type ?? "").GetHashCode() * 397) ^ (Value ?? "").GetHashCode();
      }
    }
  }
}