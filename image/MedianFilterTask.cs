using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace Recognizer;

internal static class MedianFilterTask
{
    /* 
	 * Для борьбы с пиксельным шумом, подобным тому, что на изображении,
	 * обычно применяют медианный фильтр, в котором цвет каждого пикселя, 
	 * заменяется на медиану всех цветов в некоторой окрестности пикселя.
	 * https://en.wikipedia.org/wiki/Median_filter
	 * 
	 * Используйте окно размером 3х3 для не граничных пикселей,
	 * Окно размером 2х2 для угловых и 3х2 или 2х3 для граничных.
	 */
    public static double[,] MedianFilter(double[,] original)
    {
        int lenX = original.GetLength(0);
        int lenY = original.GetLength(1);
        double[,] result = new double[lenX, lenY];
        for (int i = 0; i < lenX; i++)
        {
            for (int j = 0; j < lenY; j++)
            {
                result[i, j] = GetMedian(original, i, j, lenX, lenY);
            }
        }

        return result;
    }

    public static double GetMedian(double[,] original, int x, int y, int lenX, int lenY)
    {
        List<double> neighbors = new List<double>();
        int startX, endX, startY, endY;
        CheckValue(x, y, lenX, lenY, out startX, out endX, out startY, out endY);
        for (int i = startX; i <= endX; i++)
        {
            for (int j = startY; j <= endY; j++)
            {
                neighbors.Add(original[i, j]);
            }
        }
        return FindMedian(neighbors);
    }

    private static void CheckValue(int x, int y, int lenX, 
        int lenY, out int startX, out int endX, out int startY, out int endY)
    {
        startX = x - 1;
        endX = x + 1;
        startY = y - 1;
        endY = y + 1;
        if (startX < 0)
        {
            startX = 0;
        }
        if (endX >= lenX)
        {
            endX = lenX - 1;
        }
        if (startY < 0)
        {
            startY = 0;
        }
        if (endY >= lenY)
        {
            endY = lenY - 1;
        }
    }

    private static double FindMedian(List<double> neighbors)
    {
        neighbors.Sort();
        int count = neighbors.Count;

        if (count % 2 == 1)
        {
            return neighbors[count / 2];
        }
        else
        {
            return (neighbors[count / 2 - 1] + neighbors[count / 2]) / 2;
        }
    }
}
