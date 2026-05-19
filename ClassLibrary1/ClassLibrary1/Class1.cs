using System.Drawing;
using System.Runtime.CompilerServices;

namespace Geometry;

public class Vector
{
    public double X;
    public double Y;

    public double GetLength()
    {
        return Geometry.GetLength(this);
    }

    public Vector Add(Vector other)
    {
        return Geometry.Add(this, other);
    }

    public bool Belongs(Segment other)
    {
        return Geometry.IsVectorInSegment(this, other);
    }
}

public class Segment
{
    public Vector Begin;
    public Vector End;

    public double GetLength()
    {
        return Geometry.GetLength(this);
    }

    public bool Contains(Vector vector)
    {
        return Geometry.IsVectorInSegment(vector, this);
    }
}

public class Geometry
{
    public static double GetLength(Vector vector)
    {
        return Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
    }

    public static double GetLength(Segment segment)
    {
        double x = segment.End.X - segment.Begin.X;
        double y = segment.End.Y - segment.Begin.Y;
        return Math.Sqrt(x * x + y * y);
    }

    public static Vector Add(Vector one, Vector two)
    {
        return new Vector
        {
            X = one.X + two.X,
            Y = one.Y + two.Y
        };
    }

    public static bool IsVectorInSegment(Vector vector, Segment segment)
    {
        double productVectors = (vector.Y - segment.Begin.Y) * (segment.End.X - segment.Begin.X) -
                             (vector.X - segment.Begin.X) * (segment.End.Y - segment.Begin.Y);

        if (Math.Abs(productVectors) > 1e-10)
        {
            return false;
        }

        return vector.X >= Math.Min(segment.Begin.X, segment.End.X) &&
               vector.X <= Math.Max(segment.Begin.X, segment.End.X) &&
               vector.Y >= Math.Min(segment.Begin.Y, segment.End.Y) &&
               vector.Y <= Math.Max(segment.Begin.Y, segment.End.Y);
    }
}