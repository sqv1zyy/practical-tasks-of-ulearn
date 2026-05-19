using System.Collections.Generic;
using Avalonia.Media;
using Geometry;

namespace GeometryPainting;

//Напишите здесь код, который заставит работать методы segment.GetColor и segment.SetColor

public static class SegmentExtensions
{
    private static Dictionary<Segment, Color> Colors = new Dictionary<Segment, Color>();

    public static Color GetColor(this Segment segment)
    {
        if (Colors.ContainsKey(segment))
        {
            return Colors[segment];
        }

        return Avalonia.Media.Colors.Black;
    }

    public static void SetColor(this Segment segment, Color color)
    {
        Colors[segment] = color;
    }
}