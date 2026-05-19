using Autocomplete;
using NUnit.Framework.Legacy;
namespace TestProjectRightBorder
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
        public class Tests1
        {
            [TestCase(new[] { "a", "a" }, "a", 2, 1)]
            [TestCase(new[] { "a", "b" }, "a", 1, 2)]
            [TestCase(new[] { "b", "b" }, "a", 0, 3)]
            [TestCase(new[] { "a", "b", "b", "b" }, "a", 1, 4)]
            [TestCase(new[] { "ab", "b", "b", "b" }, "a", 1, 5)]
            [TestCase(new[] { "ab", "b", "b", "b" }, "ab", 1, 6)]
            [TestCase(new string[0] { }, "a", 0, 7)]
            [TestCase(new string[] { "a", "ab", "abc" }, "aa", 1, 8)]
            [TestCase(new string[] { "ab", "ab", "ab", "ab" }, "a", 4, 9)]
            [TestCase(new string[] { "ab", "ab", "ab", "ab" }, "aa", 0, 10)]

            public void TestCases(string[] arr, string prefix, int expectedResult, int testNumber)
            {
                var phrases = arr.ToList();
                Console.WriteLine($"Test: {testNumber} Expected: {expectedResult}");
                ClassicAssert.AreEqual(expectedResult, RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count()));
            }
        }
    }
}
