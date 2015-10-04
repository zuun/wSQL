using System;
using System.Collections.Generic;
using System.Linq;
using wSQL.Language.Contracts;
using wSQL.Language.Models;

namespace wSQL.Language.Services
{
  public class Lexer : Tokenizer
  {
    public void AddDefinition(TokenDefinition tokenDefinition)
    {
      definitions.Add(tokenDefinition);
    }

    // based on http://blogs.msdn.com/b/drew/archive/2009/12/31/a-simple-lexer-in-c-that-uses-regular-expressions.aspx
    public IEnumerable<Token> Parse(string source)
    {
      var currentIndex = 0;

      while (currentIndex < source.Length)
      {
        var found = definitions
          .Select(rule => new {rule, match = rule.Regex.Match(source, currentIndex)})
          .Where(it => it.match.Success && (it.match.Index - currentIndex) == 0)
          .Select(it => Tuple.Create(it.rule, it.match.Length))
          .FirstOrDefault();
        if (found == null)
          throw new Exception(string.Format("Unrecognized symbol '{0}'.", source[currentIndex]));

        var matchedDefinition = found.Item1;
        var matchLength = found.Item2;

        var value = source.Substring(currentIndex, matchLength);

        if (!matchedDefinition.IsIgnored)
          yield return new Token(matchedDefinition.Type, value);

        currentIndex += matchLength;
      }

      yield return Token.EOF;
    }

    //

    private readonly List<TokenDefinition> definitions = new List<TokenDefinition>();
  }
}