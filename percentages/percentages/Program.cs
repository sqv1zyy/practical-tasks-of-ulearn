using System;

namespace percentages
{
    class Program
    {
        public static void Main(string[] args)
        {
            string userInput = Console.ReadLine();
            Console.WriteLine(Calculate(userInput));
        }
        public static double Calculate(string userInput)
        {
            var parts = userInput.Split();
            double depositAmount = double.Parse(parts[0]);
            double percentMonth = (double.Parse(parts[1]) / 100 / 12);
            double month = double.Parse(parts[2]);
            return depositAmount * Math.Pow(1 + percentMonth, month);
        }
    }
}
