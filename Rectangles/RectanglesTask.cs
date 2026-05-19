using Avalonia.Controls.Platform;
using DynamicData.Aggregation;
using System;
using System.Security.Cryptography;

namespace Rectangles;

public static class RectanglesTask
{
    // Пересекаются ли два прямоугольника (пересечение только по границе также считается пересечением)
    public static bool AreIntersected(Rectangle r1, Rectangle r2)
    {
        // так можно обратиться к координатам левого верхнего угла первого прямоугольника: r1.Left, r1.Top        
        if (((r1.Left <= r2.Right) && (r2.Left <= r1.Right)) && ((r1.Top <= r2.Bottom) && (r2.Top <= r1.Bottom)))
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    // Площадь пересечения прямоугольников
    public static int IntersectionSquare(Rectangle r1, Rectangle r2)
    {
        var leftOverlap = Math.Max(r1.Left, r2.Left);
        var rightOverlap = Math.Min(r1.Left + r1.Width, r2.Left + r2.Width);
        var topOverlap = Math.Max(r1.Top, r2.Top);
        var bottomOverlap = Math.Min(r1.Top + r1.Height, r2.Top + r2.Height);

        if ((rightOverlap - leftOverlap <= 0) || (bottomOverlap - topOverlap <= 0))
        {
            return 0;
        }

        var square = (rightOverlap - leftOverlap) * (bottomOverlap - topOverlap);
        return square;
    }

    // Если один из прямоугольников целиком находится внутри другого — вернуть номер (с нуля) внутреннего.
    // Иначе вернуть -1
    // Если прямоугольники совпадают, можно вернуть номер любого из них.
    public static int IndexOfInnerRectangle(Rectangle r1, Rectangle r2)
    {
        if ((r1.Left >= r2.Left) && (r1.Left + r1.Width <= r2.Left + r2.Width)
            && (r1.Top >= r2.Top) && (r1.Top + r1.Height <= r2.Top + r2.Height))
        {
            return 0;
        }

        else if ((r2.Left >= r1.Left) && (r2.Left + r2.Width <= r1.Left + r1.Width)
            && (r2.Top >= r1.Top) && (r2.Top + r2.Height <= r1.Top + r1.Height))
        {
            return 1;
        }

        else
        {
            return -1;
        }
    }
}