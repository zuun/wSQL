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

      sut.AddDefinition(new TokenDefinition("identifier", false, new Regex("[A-Za-z_][A-Za-z0-9_]*")));
      sut.AddDefinition(new TokenDefinition("separator", true, new Regex("[ ,]")));
      sut.AddDefinition(new TokenDefinition("assignment", false, new Regex("[=]")));
      sut.AddDefinition(new TokenDefinition("string", false, new Regex("\"[^\"]*\"")));
    }

    [TestMethod]
    public void ReturnsSingleIdentifier()
    {
      var result = sut.Parse("abc").ToArray();

      CollectionAssert.AreEqual(new[]
      {
        new Token("identifier", "abc"),
      },
        result);
    }

    [TestMethod]
    public void ReturnsTwoIdentifiersSeparatedBySpace()
    {
      var result = sut.Parse("a b").ToArray();

      CollectionAssert.AreEqual(new[]
      {
        new Token("identifier", "a"),
        new Token("identifier", "b"),
      },
        result);
    }

    [TestMethod]
    public void ReturnsMultipleIdentifiersSeparatedBySpaceAndComma()
    {
      var result = sut.Parse("declare a,b,c").ToArray();

      CollectionAssert.AreEqual(new[]
      {
        new Token("identifier", "declare"),
        new Token("identifier", "a"),
        new Token("identifier", "b"),
        new Token("identifier", "c"),
      },
        result);
    }

    [TestMethod]
    public void IdentifiesAssignment()
    {
      var result = sut.Parse("set a=b").ToArray();

      CollectionAssert.AreEqual(new[]
      {
        new Token("identifier", "set"),
        new Token("identifier", "a"),
        new Token("assignment", "="),
        new Token("identifier", "b"),
      },
        result);
    }

    [TestMethod]
    public void IdentifiesStrings()
    {
      var result = sut.Parse("print \"abc def\"").ToArray();

      CollectionAssert.AreEqual(new[]
      {
        new Token("identifier", "print"),
        new Token("string", "\"abc def\""),
      },
        result);
    }
  }
}