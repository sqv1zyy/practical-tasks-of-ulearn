using NUnit.Framework.Legacy;
using TableParser;
using System.Collections.Generic;
namespace table_parser_test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }



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

    }
}
