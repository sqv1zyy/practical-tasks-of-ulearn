using Avalonia;
using Avalonia.Input;
using Avalonia.Media;
using System;
using System.Globalization;

namespace Manipulation;

public static class VisualizerTask
{
    public static double X = 220;
    public static double Y = -100;
    public static double Alpha = 0.05;
    public static double Wrist = 2 * Math.PI / 3;
    public static double Elbow = 3 * Math.PI / 4;
    public static double Shoulder = Math.PI / 2;
    public static double Value = Math.PI / 180;
    public static float Radius = 5;

    public static Brush UnreachableAreaBrush = new SolidColorBrush(Color.FromArgb(255, 255, 230, 230));
    public static Brush ReachableAreaBrush = new SolidColorBrush(Color.FromArgb(255, 230, 255, 230));
    public static Pen ManipulatorPen = new Pen(Brushes.Black, 3);
    public static Brush JointBrush = new SolidColorBrush(Colors.Gray);

    public static void KeyDown(Visual visual, KeyEventArgs key)
    {
        // TODO: Добавьте реакцию на QAWS и пересчитывать Wrist
        switch (key.Key)
        {
            case Key.Q:
                Shoulder += Value;
                break;

            case Key.A:
                Shoulder -= Value;
                break;

            case Key.W:
                Elbow += Value;
                break;

            case Key.S:
                Elbow -= Value;
                break;
        }
        Wrist = -Alpha - Shoulder - Elbow;
        visual.InvalidateVisual(); // вызывает перерисовку канваса
    }

    public static void MouseMove(Visual visual, PointerEventArgs e)
    {
        var windowPoint = e.GetPosition(visual);
        var shoulderPos = GetShoulderPos(visual);
        var mathPoint = ConvertWindowToMath(windowPoint, shoulderPos);
        X = mathPoint.X;
        Y = mathPoint.Y;
        UpdateManipulator();
        visual.InvalidateVisual();
    }

    public static void MouseWheel(Visual visual, PointerWheelEventArgs e)
    {
        // TODO: Измените Alpha, используя e.Delta.Y — размер прокрутки колеса мыши
        Alpha += Value * e.Delta.Y;
        UpdateManipulator();
        visual.InvalidateVisual();
    }

    public static void UpdateManipulator()
    {
        // Вызовите ManipulatorTask.MoveManipulatorTo и обновите значения полей Shoulder, Elbow и Wrist,
        // если они не NaN. Это понадобится для последней задачи.
        double[] angles = ManipulatorTask.MoveManipulatorTo(X, Y, Alpha);
        foreach (double angle in angles)
        {
            if (Double.IsNaN(angle))
            {
                return;
            }
        }
        Shoulder = angles[0];
        Elbow = angles[1];
        Wrist = angles[2];
    }

    public static void DrawManipulator(DrawingContext context, Point shoulderPos)
    {
        var joints = AnglesToCoordinatesTask.GetJointPositions(Shoulder, Elbow, Wrist);
        DrawReachableZone(context, ReachableAreaBrush, UnreachableAreaBrush, shoulderPos, joints);
        FormattedText formattedText = FormText();
        context.DrawText(formattedText, new Point(10, 10));
        Point[] windowJoints = new Point[joints.Length];
        ConvertToMath(shoulderPos, joints, windowJoints);
        Point shoulderPoint, previousPoint;
        FindValue(shoulderPos, out shoulderPoint, out previousPoint);
        previousPoint = DrawToLine(context, windowJoints, previousPoint);
        context.DrawEllipse(JointBrush, null, shoulderPoint, Radius, Radius);
        DrawElips(context, windowJoints);
    }

    private static void DrawElips(DrawingContext context, Point[] windowJoints)
    {
        foreach (var joint in windowJoints)
        {
            context.DrawEllipse(JointBrush, null, joint, Radius, Radius);
        }
    }

    private static Point DrawToLine(DrawingContext context, Point[] windowJoints, Point previousPoint)
    {
        for (int i = 0; i < windowJoints.Length; i++)
        {
            context.DrawLine(ManipulatorPen, previousPoint, windowJoints[i]);
            previousPoint = windowJoints[i];
        }

        return previousPoint;
    }

    private static FormattedText FormText()
    {
        return new FormattedText(
                    $"X={X:0}, Y={Y:0}, Alpha={Alpha:0.00}",
                    CultureInfo.InvariantCulture,
                    FlowDirection.LeftToRight,
                    Typeface.Default,
                    18,
                    Brushes.DarkRed
                )
        {
            TextAlignment = TextAlignment.Center
        };
    }

    private static void FindValue(Point shoulderPos, out Point shoulderPoint, out Point previousPoint)
    {
        shoulderPoint = ConvertMathToWindow(new Point(0, 0), shoulderPos);
        previousPoint = shoulderPoint;
    }

    private static void ConvertToMath(Point shoulderPos, Point[] joints, Point[] windowJoints)
    {
        for (int i = 0; i < joints.Length; i++)
        {
            windowJoints[i] = ConvertMathToWindow(joints[i], shoulderPos);
        }
    }

    private static void DrawReachableZone(
        DrawingContext context,
        Brush reachableBrush,
        Brush unreachableBrush,
        Point shoulderPos,
        Point[] joints)
    {
        var radiusMin = Math.Abs(Manipulator.UpperArm - Manipulator.Forearm);
        var radiusMax = Manipulator.UpperArm + Manipulator.Forearm;
        var center = new Point(joints[2].X - joints[1].X, joints[2].Y - joints[1].Y);
        var windowCenter = ConvertMathToWindow(center, shoulderPos);
        context.DrawEllipse(reachableBrush,
            null,
            new Point(windowCenter.X, windowCenter.Y),
            radiusMax, radiusMax);
        context.DrawEllipse(unreachableBrush,
            null,
            new Point(windowCenter.X, windowCenter.Y),
            radiusMin, radiusMin);
    }

    public static Point GetShoulderPos(Visual visual)
    {
        return new Point(visual.Bounds.Width / 2, visual.Bounds.Height / 2);
    }

    public static Point ConvertMathToWindow(Point mathPoint, Point shoulderPos)
    {
        return new Point(mathPoint.X + shoulderPos.X, shoulderPos.Y - mathPoint.Y);
    }

    public static Point ConvertWindowToMath(Point windowPoint, Point shoulderPos)
    {
        return new Point(windowPoint.X - shoulderPos.X, shoulderPos.Y - windowPoint.Y);
    }
}