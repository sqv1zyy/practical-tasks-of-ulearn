using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Recognizer;

public static class ThresholdFilterTask
{
    public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
    {
        int countPixelWhite, lenX, lenY;
        double[] totalPixel;
        CalculateDimension(original, whitePixelsFraction, out countPixelWhite, out lenX, out lenY, out totalPixel);
        var result = new double[lenX, lenY];
        var whitePixel = new List<double>();
        AddPixelToOneDimenArray(original, lenX, lenY, totalPixel);
        AddListFractionWhitePixel(countPixelWhite, totalPixel, whitePixel);
        DrawToWhite(original, lenX, lenY, result, whitePixel);
        DrawToBlack(lenX, lenY, result);
        return result;
        
    }

    private static void DrawToBlack(int lenX, int lenY, double[,] result)
    {
        for (int i = 0; i < lenX; i++)
        {
            for (int j = 0; j < lenY; j++)
            {
                if (result[i, j] != 1)
                {
                    result[i, j] = 0.0;
                }
            }
        }
    }

    private static void DrawToWhite(double[,] original, int lenX, int lenY, double[,] result, List<double> whitePixel)
    {
        for (int i = 0; i < lenX; i++)
        {
            for (int j = 0; j < lenY; j++)
            {
                if (whitePixel.Contains(original[i, j]))
                {
                    result[i, j] = 1.0;
                }
            }
        }
    }

    private static void CalculateDimension(double[,] original, double whitePixelsFraction, out int countPixelWhite, out int lenX, out int lenY, out double[] totalPixel)
    {
        countPixelWhite = (int)(original.Length * whitePixelsFraction);
        lenX = original.GetLength(0);
        lenY = original.GetLength(1);
        totalPixel = new double[lenX * lenY];
    }

    private static void AddListFractionWhitePixel(int countPixelWhite, double[] totalPixel, List<double> whitePixel)
    {
        int ind = 0;
        while (ind < countPixelWhite)
        {
            whitePixel.Add(totalPixel[ind]);
            ind++;
        }
    }

    private static void AddPixelToOneDimenArray(double[,] original, int lenX, int lenY, double[] totalPixel)
    {
        int index = 0;
        for (int i = 0; i < lenX; i++)
        {
            for (int j = 0; j < lenY; j++)
            {
                totalPixel[index] = original[i, j];
                index++;
            }
        }
        Array.Sort(totalPixel);
        Array.Reverse(totalPixel);
    }
}
