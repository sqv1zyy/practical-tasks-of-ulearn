using System;
using NUnit.Framework;

namespace Manipulation
{
    public static class ManipulatorTask
    {
        /// <summary>
        /// Возвращает массив углов (shoulder, elbow, wrist),
        /// необходимых для приведения эффектора манипулятора в точку x и y 
        /// с углом между последним суставом и горизонталью, равному angle (в радианах)
        /// См. чертеж manipulator.png!
        /// </summary>
        public static double[] MoveManipulatorTo(double x, double y, double angle)
        {
            double elbow, angleToWrist, shoulder, wrist;
            CalculateVariables(x, y, angle, out elbow, out angleToWrist, out shoulder, out wrist);

            if (Double.IsNaN(elbow))
            {
                return new[] { double.NaN, double.NaN, double.NaN };
            }

            if (Double.IsNaN(angleToWrist))
            {
                return new[] { double.NaN, double.NaN, double.NaN };
            }


            return new[] { shoulder, elbow, wrist };
        }

        private static void CalculateVariables(double x, 
            double y, double angle, out double elbow, out double angleToWrist, 
            out double shoulder, out double wrist)
        {
            double wristX = x + Manipulator.Palm * Math.Cos(Math.PI - angle);
            double wristY = y + Manipulator.Palm * Math.Sin(Math.PI - angle);
            double distanceFromShoulderToWrist = Math.Sqrt(wristX * wristX + wristY * wristY);
            elbow = TriangleTask.GetABAngle(Manipulator.UpperArm, Manipulator.Forearm, 
                distanceFromShoulderToWrist);
            angleToWrist = Math.Atan2(wristY, wristX);
            shoulder = TriangleTask.GetABAngle(Manipulator.UpperArm, 
                distanceFromShoulderToWrist, Manipulator.Forearm) + angleToWrist;
            wrist = -angle - shoulder - elbow;
        }
    }

    [TestFixture]
    public class ManipulatorTask_Tests
    {
        public const int NumbersTest = 1000;
        public const int SeedTest = 12345;
        public const double Size = Manipulator.UpperArm + Manipulator.Forearm +Manipulator.Palm;

        [Test]
        public void TestMoveManipulatorTo()
        {
            var randomMeaning = new Random(SeedTest);
            for (var i = 0; i < NumbersTest; ++i)
            {   
                var x = randomMeaning.NextDouble() * 2 * Size - Size;
                var y = randomMeaning.NextDouble() * 2 * Size - Size;
                var a = randomMeaning.NextDouble() * 2 * Math.PI;
                var angles = ManipulatorTask.MoveManipulatorTo(x, y, a);
                Assert.That(angles.Length, Is.EqualTo(3));
                if (!Double.IsNaN(angles[0]))
                { 
                    var joints = AnglesToCoordinatesTask.GetJointPositions(angles[0], angles[1], angles[2]);
                    Assert.That(joints.Length, Is.EqualTo(3));
                    Assert.That(joints[2].X, Is.EqualTo(x).Within(0.001));
                    Assert.That(joints[2].Y, Is.EqualTo(y).Within(0.001));
                }
            }
        }
    }
}