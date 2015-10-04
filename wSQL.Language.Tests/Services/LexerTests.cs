using System.Linq;
using System.Text.RegularExpressions;
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

      sut.AddDefinition(new TokenDefinition(TokenType.Identifier, false, new Regex("[A-Za-z_][A-Za-z0-9_]*")));
      sut.AddDefinition(new TokenDefinition(TokenType.Separator, true, new Regex("[ ,]")));
      sut.AddDefinition(new TokenDefinition(TokenType.Assignment, false, new Regex("[=]")));
      sut.AddDefinition(new TokenDefinition(TokenType.String, false, new Regex("\"[^\"]*\"")));
      sut.AddDefinition(new TokenDefinition(TokenType.OpenPar, false, new Regex("[(]")));
      sut.AddDefinition(new TokenDefinition(TokenType.ClosedPar, false, new Regex("[)]")));
    }

    [TestMethod]
    public void ReturnsSingleIdentifier()
    {
      var result = sut.Parse("abc").ToArray();

      CollectionAssert.AreEqual(new[]
      {
        new Token(TokenType.Identifier, "abc"),
      },
        result);
    }

    [TestMethod]
    public void ReturnsTwoIdentifiersSeparatedBySpace()
    {
      var result = sut.Parse("a b").ToArray();

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
      var result = sut.Parse("declare a,b,c").ToArray();

      CollectionAssert.AreEqual(new[]
      {
        new Token(TokenType.Identifier, "declare"),
        new Token(TokenType.Identifier, "a"),
        new Token(TokenType.Identifier, "b"),
        new Token(TokenType.Identifier, "c"),
      },
        result);
    }

    [TestMethod]
    public void IdentifiesAssignment()
    {
      var result = sut.Parse("set a=b").ToArray();

      CollectionAssert.AreEqual(new[]
      {
        new Token(TokenType.Identifier, "set"),
        new Token(TokenType.Identifier, "a"),
        new Token(TokenType.Assignment, "="),
        new Token(TokenType.Identifier, "b"),
      },
        result);
    }

    [TestMethod]
    public void IdentifiesStrings()
    {
      var result = sut.Parse("print \"abc def\"").ToArray();

      CollectionAssert.AreEqual(new[]
      {
        new Token(TokenType.Identifier, "print"),
        new Token(TokenType.String, "\"abc def\""),
      },
        result);
    }

    [TestMethod]
    public void IdentifiesParentheses()
    {
      var result = sut.Parse("load(\"abc def\")").ToArray();

      CollectionAssert.AreEqual(new[]
      {
        new Token(TokenType.Identifier, "load"),
        new Token(TokenType.OpenPar, "("),
        new Token(TokenType.String, "\"abc def\""),
        new Token(TokenType.ClosedPar, ")"),
      },
        result);
    }

    [TestMethod]
    public void IdentifiesAssignmentWithFunctionCall()
    {
      var result = sut.Parse("set page = load(\"abc def\")").ToArray();

      CollectionAssert.AreEqual(new[]
      {
        new Token(TokenType.Identifier, "set"),
        new Token(TokenType.Identifier, "page"),
        new Token(TokenType.Assignment, "="),
        new Token(TokenType.Identifier, "load"),
        new Token(TokenType.OpenPar, "("),
        new Token(TokenType.String, "\"abc def\""),
        new Token(TokenType.ClosedPar, ")"),
      },
        result);
    }
  }
}