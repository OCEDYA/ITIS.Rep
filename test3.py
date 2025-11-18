using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace TableParser;

[TestFixture]
public class FieldParserTaskTests
{
  public static void Test(string input, string[] expectedResult)
  {
      var actualResult = FieldsParserTask.ParseLine(input);
      ClassicAssert.AreEqual(expectedResult.Length, actualResult.Count);
      for (int i = 0; i < expectedResult.Length; ++i)
      {
          ClassicAssert.AreEqual(expectedResult[i], actualResult[i].Value);
      }
  }

  [TestCase("a", new[] { "a" })]
  [TestCase("a b", new[] { "a", "b" })]
  [TestCase("", new string[0])]
  [TestCase("a  b", new[] { "a", "b" })]
  [TestCase("''", new[] { "" })]
  [TestCase("a ''", new[] { "a", "" })]
  [TestCase("''a", new[] { "", "a" })]
  [TestCase("\"", new[] { "" })]
  [TestCase("\"\\\"\"", new[] { "\"" })]
  [TestCase("\"\\\'\"", new[] { "\'" })]
  [TestCase("\'\\\"\'", new[] { "\"" })]
  [TestCase("\'\\\'\'", new[] { "\'" })]
  [TestCase("' '", new[] { " " })]
  [TestCase("\"\\\\\"", new[] { "\\" })]
  [TestCase(" a", new[] { "a" })]
  [TestCase("\" ", new[] { " " })]
  [TestCase("a \"bcd ef\" 'x y'", new[] { "a", "bcd ef", "x y" })]
  [TestCase("a\"b c d e\"f", new[] { "a", "b c d e", "f" })]
  public static void RunTests(string input, string[] expectedOutput)
  {
      Test(input, expectedOutput);
  }
}

public class FieldsParserTask
{
  public static List<Token> ParseLine(string line)
  {
      var tokens = new List<Token>();
      var index = 0;
      
      while (index < line.Length)
      {
          if (line[index] == ' ')
          {
              index++;
              continue;
          }
          
          var token = (line[index] == '\'' || line[index] == '"') 
              ? ReadQuotedField(line, index) 
              : ReadField(line, index);
              
          tokens.Add(token);
          index = token.GetIndexNextToToken();
      }
      
      return tokens;
  }
  
  private static Token ReadField(string line, int startIndex)
  {
      var length = 0;
      for (var i = startIndex; i < line.Length; i++)
      {
          if (line[i] == ' ' || line[i] == '\'' || line[i] == '"') break;
          length++;
      }
      return new Token(line.Substring(startIndex, length), startIndex, length);
  }

  public static Token ReadQuotedField(string line, int startIndex)
  {
      return QuotedFieldTask.ReadQuotedField(line, startIndex);
  }
}
