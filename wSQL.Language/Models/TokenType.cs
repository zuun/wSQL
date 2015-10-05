namespace wSQL.Language.Models
{
  public enum TokenType
  {
    Identifier,
    Whitespace,
    Lambda,
    Assignment,
    String,
    OpenPar,
    ClosedPar,
    Access,
    Comma,
  }
}