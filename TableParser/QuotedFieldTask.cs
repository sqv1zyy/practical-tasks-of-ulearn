using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace TableParser;

[TestFixture]
public class QuotedFieldTaskTests
{
    [TestCase("''", 0, "", 2)]
    [TestCase("'a'", 0, "a", 3)]
    public void Test(string line, int startIndex, string expectedValue, int expectedLength)
    {
        var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
        ClassicAssert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actualToken);
    }

    // Добавьте свои тесты
}

internal class QuotedFieldTask
{
    public static Token ReadQuotedField(string line, int startIndex)
    {
        char typeClose = line[startIndex];
        string value = "";
        int len = startIndex + 1;
        while (len < line.Length)
        {
            if (line[len] == '\\' && len + 1 < line.Length)
            {
                WriteShieldedChar(line, typeClose, ref value, ref len);
            }
            else if (line[len] == typeClose)
            {
                len++;
                break;
            }
            value += line[len];
            len++;
        }

        return new Token(value, startIndex, len - startIndex);
    }

    private static void WriteShieldedChar(string line, char typeClose, ref string value, ref int len)
    {
        char ecr = line[len + 1];
        if ((ecr == typeClose) || (ecr == '\\'))
        {
            value += ecr;
            len += 2;
        }
    }
}