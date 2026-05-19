using System;

namespace DistanceTask;

public static class DistanceTask
{
    // Расстояние от точки (x, y) до отрезка AB с координатами A(ax, ay), B(bx, by)
    public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
    {
        double vectAbX = bx - ax;
        double vectAbY = by - ay;
        double vectAxP = x - ax;
        double vectAyP = y - ay;
        double vectBxP = x - bx;
        double vectByP = y - by;
        double scalarProduct = vectAbX * vectAxP + vectAbY * vectAyP;

        if (scalarProduct <= 0)
        {
            return Math.Sqrt(vectAxP * vectAxP + vectAyP * vectAyP);
        }

        else if (scalarProduct >= (vectAbX * vectAbX + vectAbY * vectAbY))
        {
            return Math.Sqrt(vectBxP * vectBxP + vectByP * vectByP);
        }
        else
        {
            return Math.Abs(vectAbX * vectAyP - vectAbY * vectAxP) / Math.Sqrt(vectAbX * vectAbX + vectAbY * vectAbY);
        }
    }
}