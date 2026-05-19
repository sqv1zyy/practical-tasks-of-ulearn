using System;

namespace Fractals;

internal static class DragonFractalTask
{
    public static double Cos45 = Math.Cos(45 * Math.PI / 180);
    public static double Sin45 = Math.Sin(45 * Math.PI / 180);
    public static double Sqrt2 = Math.Sqrt(2);
    public static double Cos135 = Math.Cos(135 * Math.PI / 180);
    public static double Sin135 = Math.Sin(135 * Math.PI / 180);
    public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
    {
        double x = 1;
        double y = 0;
        pixels.SetPixel(x, y);
        var random = new Random(seed);


        for (int i = 0; i < iterationsCount; i++)
        {
            if (random.Next(2) == 0)
            {
                (x, y) = MakeTransformationOne(x, y);
            }

            else
            {
                (x, y) = MakeTransformationTwo(x, y);
            }
            pixels.SetPixel(x, y);
        }
    }
    public static (double x, double y) MakeTransformationOne(double x, double y)
    {
        double NewX = MakeTransformationOneX(x, y);
        double NewY = MakeTransformationOneY(x, y);
        x = NewX;
        y = NewY;
        return (NewX, NewY);
    }

    public static (double x, double y) MakeTransformationTwo(double x, double y)
    {
        double NewX = MakeTransformationTwoX(x, y);
        double NewY = MakeTransformationTwoY(x, y);
        x = NewX;
        y = NewY;
        return (NewX, NewY);
    }
    public static double MakeTransformationOneX(double x, double y)
    {
        double NewX = (x * Cos45 - y * Sin45) / Sqrt2;
        return NewX;
    }

    public static double MakeTransformationOneY(double x, double y)
    {
        double NewY = (x * Sin45 + y * Cos45) / Sqrt2;
        return NewY;
    }

    public static double MakeTransformationTwoX(double x, double y)
    {
        double NewX = (x * Cos135 - y * Sin135) / Sqrt2 + 1;
        return NewX;
    }
    public static double MakeTransformationTwoY(double x, double y)
    {
        double NewY = (x * Sin135 + y * Cos135) / Sqrt2;
        return NewY;
    }
}