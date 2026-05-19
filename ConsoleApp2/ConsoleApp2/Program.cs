using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;

// Подключение библиотеки

namespace MyFirstApp // Пространство имен (папка для классов)
{
    public static void Check(int num, int den)
    {
        var ratio = new Ratio(num, den);
        Console.WriteLine("{0}/{1} = {2}",
            ratio.Numerator, ratio.Denominator,
            ratio.Value.ToString(CultureInfo.InvariantCulture));
    }

    public class Ratio
    {
        public readonly int Numerator; 
        public readonly int Denominator; 
        public readonly double Value; 
        public Ratio(int num, int den)
        {
            if (den <= 0)
            {
                throw new ArgumentException();
            }

            Numerator = num;
            Denominator = den;
            Value = (double)Numerator / Denominator;
        } 
    }
}

    //public class Vector
    //{
    //    public double X { get; set; }
    //    public double Y { get; set; }
    //    public double Length
    //    {
    //        get { return Math.Sqrt(X * X + Y * Y); }
    //    }

    //    // добавьте конструктор!
    //    public Vector(double X, double Y)
    //    {
    //        this.X = X;
    //        this.Y = Y;
    //    }

    //    public override string ToString()
    //    {
    //        return string.Format("({0}, {1}) with length: {2}", X, Y, Length);
    //    }
}

    //public class Student
    //{
    //    private string name;
    //    public string Name
    //    {
    //        get { return name; }
    //        set
    //        {
    //            if (name is null)
    //            {
    //                throw new ArgumentException();
    //            }
    //            name = value;
    //        }
    //    }
    //}
    //class Program
    //{
    //    public static void Main()
    //    {
    //        var triangle = new Triangle
    //        {
    //            A = new Point { X = 0, Y = 0 },
    //            B = new Point { X = 1, Y = 2 },
    //            C = new Point { X = 3, Y = 2 }
    //        };
    //        Console.WriteLine(triangle.ToString());
    //    }

    //    public class Point
    //    {
    //        public double X;
    //        public double Y;

    //        public override string ToString()
    //        {
    //            return $"{X} {Y}";
    //        }
    //    }

    //    public class Triangle
    //    {
    //        public Point A;
    //        public Point B;
    //        public Point C;
    //        public override string ToString()
    //        {
    //            return $"({A}) ({B}) ({C})";
    //        }
    //    }
    //}
    //public static void Print(params object[] arg)
    //{
    //    for (var i = 0; i < arg.Length; i++)
    //    {
    //        if (i > 0)
    //            Console.Write(", ");
    //        Console.Write(arg[i]);
    //    }

    //}
    //private static int FindLeftBorder(long[] arr, long value)
    //{
    //    return BinSearchLeftBorder(arr, value, -1, arr.Length);
    //}

    //public static int BinSearchLeftBorder(long[] array, long value, int left, int right)
    //{
    //    if ((right - left <= 1)) return left;
    //    var m = (left + right) / 2;
    //    if (array[m] >= value)
    //        return BinSearchLeftBorder(array, value, left, m);
    //    return BinSearchLeftBorder(array, value, m, right);
    //}
    //static void Main(string[] args) // Точка входа
    //{
    //    var a = new long[] { 1, 2, 3 };
    //    var b = 2;

    //    Console.WriteLine(FindLeftBorder(a, b));
    //    //Console.WriteLine(Encoding.UTF8.GetBytes("БЩФzw!").Length);
    //    //foreach (var s in t)
    //    //{
    //    //    Console.WriteLine(s);
    //    //    Console.WriteLine(s);
    //    //}
    //    //var a = new List<string> {"са:саша1995@саша.ру", "са:саня@в.уй", "лалала:лалалалалала@ла.ла" };
    //    ////Console.WriteLine(OptimizeContacts(a)); // Вывод в консоль

    //    //foreach (var b in a)
    //    //{
    //    //    string[] listToString = b.Split(':');
    //    //    Console.WriteLine(listToString[1]);
    //    //    Console.WriteLine(listToString[0]);
    //    //}
    //}
    //private static string ApplyCommands(string[] commands)
    //{
    //    var result = new StringBuilder();
    //    var text = string.Join("", commands);
    //    var t = text.Split(" ");

    //    for (int i = 0; i < t.Length; i++)
    //    {
    //        if (t[i] == "push")
    //        {
    //            for (int j = 0; j < t.Length - i; j++)
    //            {
    //                if (t[i + j] != "push" && t[i + j] != "pop")
    //                {
    //                    result.Append(t[i + j]);
    //                }
    //            }

    //        }
    //        else if (t[i] == "pop")
    //        {
    //            string pop = t[i + 1];
    //            int maxCount = 0;
    //            foreach (var c in pop)
    //            {
    //                int num = c - 0;
    //                maxCount = Math.Max(maxCount, num);
    //            }
    //            result.Remove(maxCount, result.Length - maxCount);
    //        }
    //    }
    //    return result.ToString();
    //}

    //public static string ReplaceIncorrectSeparators(string text)
    //{
    //    var newString = text.Split(new char[] { ' ', ';', ':', '-', ',' }, StringSplitOptions.RemoveEmptyEntries);
    //    string result = string.Join("\t", newString);
    //    return result;
    //}
    //public static int[] GetBenfordStatistics(string text)
    //{
    //    var statistics = new int[10];
    //    var str = text.Split(",");
    //    for (int i = 0; i < str.Length; i++)
    //    {
    //        if(!string.IsNullOrWhiteSpace(str[i]) && int.TryParse(str[i], out int result))
    //        {
    //            int number = Math.Abs(result);
    //            string numberString = number.ToString();
    //            if (numberString.Length > 0)
    //            {
    //                int firstNumber = int.Parse(numberString[0].ToString());
    //                statistics[firstNumber]++;
    //            }
    //        }
    //    }
    //    return statistics;
    //}
    //private static Dictionary<string, List<string>> OptimizeContacts(List<string> contacts)
    //{
    //    var dictionary = new Dictionary<string, List<string>>();
    //    foreach (var contact in contacts)
    //    {
    //        string[] listToString = contact.Split(":");
    //        if (!dictionary.ContainsKey(listToString[0]))
    //        {
    //            dictionary[listToString[0]] = ;
    //        }
    //    }
    //    return dictionary;
    //}
    //private static string DecodeMessage(string[] lines)
    //{
    //    var words = new List<string>();
    //    foreach (string line in lines)
    //    {
    //        string[] lWords = line.Split(' ');
    //        foreach (string word in lWords)
    //        {
    //            if (word.Length > 0 && char.IsUpper(word[0]))
    //            {
    //                words.Add(word);
    //            }
    //        }
    //    }
    //    words.Reverse();
    //    string res = string.Join(" ", words);
    //    return res;
    //}
}