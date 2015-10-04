using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wSQL.Language.Models;
using wSQL.Language.Services;

namespace wSQL.Language.Tests.Services
{
  [TestClass]
  public class LexerTests
  {
    private Lexer sut;

    [TestInitialize]
    public void SetUp()
    {
      sut = new Lexer();
    }

    [TestMethod]
    public void ReturnsSingleIdentifier()
    {
      var result = sut.Parse("abc").ToList();

      CollectionAssert.AreEqual(new[]
      {
        new Token(TokenType.Identifier, "abc"),
      },
        result);
    }

    [TestMethod]
    public void ReturnsTwoIdentifiersSeparatedBySpace()
    {
      var result = sut.Parse("a b").ToList();

      CollectionAssert.AreEqual(new[]
      {
        new Token(TokenType.Identifier, "a"),
        new Token(TokenType.Identifier, "b"),
      },
        result);
    }

    [TestMethod]
    public void ReturnsMultipleIdentifiersSeparatedBySpaceAndComma()
    {
      var result = sut.Parse("declare a,b,c").ToList();

      CollectionAssert.AreEqual(new[]
      {
        new Token(TokenType.Identifier, "declare"),
        new Token(TokenType.Identifier, "a"),
        new Token(TokenType.Identifier, "b"),
        new Token(TokenType.Identifier, "c"),
      },
        result);
    }
  }
}