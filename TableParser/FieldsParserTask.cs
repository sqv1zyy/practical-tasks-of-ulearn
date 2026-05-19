using NUnit.Framework;
using System.Collections.Generic;
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

    [TestCase("text", new[] { "text" })]
    [TestCase("hello world", new[] { "hello", "world" })]
    [TestCase(@"'' ""bcd ef"" 'x y'", new[] { "", "bcd ef", "x y" })]
    [TestCase("\\ b", new[] { "\\", "b" })]
    [TestCase(@"""def g h", new[] { "def g h" })]
    [TestCase(@"""\\""", new[] { @"\" })]
    [TestCase("a   b   c", new[] { "a", "b", "c" })]
    [TestCase("   hello world", new[] { "hello", "world" })]
    [TestCase("hello world   ", new[] { "hello", "world" })]
    [TestCase("\"a 'b' c\"", new[] { "a 'b' c" })]
    [TestCase("'' \"\"", new[] { "", "" })]
    [TestCase("\"a 'b' c\"", new[] { "a 'b' c" })]
    [TestCase("\"a \\\"b c", new[] { "a \"b c" })]
    [TestCase("\"\\'\"", new[] { "'" })]
    [TestCase("\" \"", new[] { " " })]
    [TestCase("\"\"\"\"", new[] { "", "" })]
    [TestCase("\\ b", new[] { "\\", "b" })]
    [TestCase("\"a \\\"b c\"", new[] { "a \"b c" })]
    [TestCase("a\"b", new[] { "a", "b" })]
    [TestCase(@"'a \' b'", new[] { "a ' b" })]
    [TestCase(@"""abc"" def", new[] { "abc", "def" })]
    [TestCase(@"'a ""b"" c'", new[] { @"a ""b"" c" })]
    [TestCase("   ", new string[0])]
    [TestCase("\"abc def ", new[] { "abc def " })]
    public static void RunTests(string input, string[] expectedOutput)
    {
        // Тело метода изменять не нужно
        Test(input, expectedOutput);
    }
}

public class FieldsParserTask
{
    // При решении этой задаче постарайтесь избежать создания методов, длиннее 10 строк.
    // Подумайте как можно использовать ReadQuotedField и Token в этой задаче.
    public static List<Token> ParseLine(string line)
    {
        if (string.IsNullOrEmpty(line))
            return new List<Token>();

        var tokens = new List<Token>();
        int i = 0;

        while (i < line.Length && char.IsWhiteSpace(line[i]))
            i++;

        while (i < line.Length)
        {
            char typeClose = line[i];
            Token token = null;

            if (typeClose == '"' || typeClose == '\'')
            {
                token = ReadQuotedField(line, i);
                if (token != null)
                {
                    tokens.Add(token);
                    i = token.GetIndexNextToToken();
                }
            }
            else if (!char.IsWhiteSpace(typeClose)) token = AddCharNotWhiteSpace(line, tokens, ref i);

            while (i < line.Length && char.IsWhiteSpace(line[i]))
                i++;
        }

        return tokens;
    }

    private static Token AddCharNotWhiteSpace(string line, List<Token> tokens, ref int i)
    {
        Token token;
        int start = i;

        while (i < line.Length && !char.IsWhiteSpace(line[i]) && line[i] != '"' && line[i] != '\'')
        {
            i++;
        }


        string value = line.Substring(start, i - start);
        token = new Token(value, start, i - start);
        tokens.Add(token);
        return token;
    }

    private static Token ReadField(string line, int startIndex)
    {
        return new Token(line, 0, line.Length);
    }

    public static Token ReadQuotedField(string line, int startIndex)
    {
        return QuotedFieldTask.ReadQuotedField(line, startIndex);
    }


}