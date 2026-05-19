using System;

namespace AngryBirds;

public static class AngryBirdsTask
{
    public static double FindSightAngle(double v, double distance)
    {
        const double g = 9.8;
        double angleAiming = 0.5 * (Math.Asin((distance * g) / (v * v)));
        
        return angleAiming;
    }
}