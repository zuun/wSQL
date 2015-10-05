using System.Text.RegularExpressions;
using wSQL.Language.Models;

namespace wSQL.Language.Services
{
  public static class DefaultLexer
  {
    public static Lexer Create()
    {
      var lexer = new Lexer();

      lexer.AddDefinition(new TokenDefinition(TokenType.Identifier, false, new Regex("[A-Za-z_][A-Za-z0-9_]*")));
      lexer.AddDefinition(new TokenDefinition(TokenType.Access, false, new Regex("[.]")));
      lexer.AddDefinition(new TokenDefinition(TokenType.OpenPar, false, new Regex("[(]")));
      lexer.AddDefinition(new TokenDefinition(TokenType.ClosedPar, false, new Regex("[)]")));
      lexer.AddDefinition(new TokenDefinition(TokenType.Lambda, false, new Regex("=>")));
      lexer.AddDefinition(new TokenDefinition(TokenType.Assignment, false, new Regex("=")));
      lexer.AddDefinition(new TokenDefinition(TokenType.String, false, new Regex("\"[^\"]*\"")));
      lexer.AddDefinition(new TokenDefinition(TokenType.Comma, false, new Regex(",")));
      lexer.AddDefinition(new TokenDefinition(TokenType.Whitespace, true, new Regex(@"\s+")));
      lexer.AddDefinition(new TokenDefinition(TokenType.Whitespace, true, new Regex("//.*")));

      return lexer;
    }
  }
}