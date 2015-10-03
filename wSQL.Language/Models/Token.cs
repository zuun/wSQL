﻿namespace wSQL.Language.Models
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
  }
}