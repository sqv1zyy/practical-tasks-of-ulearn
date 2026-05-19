using Avalonia;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using static Manipulation.Manipulator;

namespace Manipulation;

public static class AnglesToCoordinatesTask
{
    /// <summary>
    /// По значению углов суставов возвращает массив координат суставов
    /// в порядке new []{elbow, wrist, palmEnd}
    /// </summary>
    public static Point[] GetJointPositions(double shoulder, double elbow, double wrist)
    {
        double upperArmAngle, forearmAngle, palmAngle;
        CalculateAbsolutAngles(shoulder, elbow, wrist, out upperArmAngle, out forearmAngle, out palmAngle);
        var upperArmVec = new Point(
            UpperArm * Math.Cos(upperArmAngle),
            UpperArm * Math.Sin(upperArmAngle)
            );

        var forearmVec = new Point(
            Forearm * Math.Cos(forearmAngle),
            Forearm * Math.Sin(forearmAngle)
        );

        var palmVec = new Point(
            Palm * Math.Cos(palmAngle),
            Palm * Math.Sin(palmAngle)
        );
        var elbowPos = upperArmVec;
        var wristPos = elbowPos + forearmVec;          
        var palmEndPos = wristPos + palmVec;
        return new[] { elbowPos, wristPos, palmEndPos };
    }

    private static void CalculateAbsolutAngles(double shoulder, double elbow, double wrist, out double upperArmAngle, out double forearmAngle, out double palmAngle)
    {
        upperArmAngle = shoulder;
        forearmAngle = shoulder + Math.PI + elbow;
        palmAngle = shoulder + Math.PI + elbow + Math.PI + wrist;
    }
}

[TestFixture]
public class AnglesToCoordinatesTask_Tests
{
    // Доработайте эти тесты!
    // С помощью строчки TestCase можно добавлять новые тестовые данные.
    // Аргументы TestCase превратятся в аргументы метода.
    public static double Distance(Point a, Point b)
    {
        double dx = a.X - b.X;
        double dy = a.Y - b.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }

    [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, 180.0, 150.0)]
    [TestCase(0, 0, 0, UpperArm - Forearm + Palm, 0)]
    [TestCase(Math.PI / 2, 0, 0, 0, UpperArm - Forearm + Palm)]
    [TestCase(-Math.PI / 2, 0, 0, 0, -(UpperArm - Forearm + Palm))]
    [TestCase(0, Math.PI, 0, UpperArm + Forearm + Palm, 0)]
    public void TestGetJointPositions(double shoulder, double elbow, double wrist, double palmEndX, double palmEndY)
    {
        var coordinats = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);  
        var shoulderPos = new Point(0, 0);
        ClassicAssert.AreEqual(UpperArm, Distance(shoulderPos, coordinats[0]), 1e-5, "UpperArm segment length");
        ClassicAssert.AreEqual(Forearm, Distance(coordinats[0], coordinats[1]), 1e-5, "Forearm segment length");
        ClassicAssert.AreEqual(Palm, Distance(coordinats[1], coordinats[2]), 1e-5, "Palm segment length");
    }
}