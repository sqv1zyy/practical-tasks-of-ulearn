using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Manipulation;

public class TriangleTask
{
    /// <summary>
    /// Возвращает угол (в радианах) между сторонами a и b в треугольнике со сторонами a, b, c 
    /// </summary>
    public static double GetABAngle(double a, double b, double c)
    {
        if ((a > 0 && b > 0) || c >= 0)
        {
            return Math.Acos((a * a + b * b - c * c) / (2 * a * b));
        }

        return double.NaN;
    }
}

[TestFixture]
public class TriangleTask_Tests
{
    [TestCase(3, 4, 5, Math.PI / 2)]
    [TestCase(1, 1, 1, Math.PI / 3)]
    [TestCase(0, 0, 0, double.NaN)]
    // добавьте ещё тестовых случаев!
    public void TestGetABAngle(double a, double b, double c, double expectedAngle)
    {
        var result = TriangleTask.GetABAngle(a, b, c);
        if (double.IsNaN(expectedAngle))
        {
            ClassicAssert.IsNaN(result);
        }
        else
        {
            ClassicAssert.AreEqual(expectedAngle, result, 1e-10);
        }
    }
}