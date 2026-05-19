using System;

namespace Recognizer;

internal static class SobelFilterTask
{
    public static double[,] SobelFilter(double[,] g, double[,] sx)
    {
        var width = g.GetLength(0);
        var height = g.GetLength(1);
        var result = new double[width, height];

        var sy = TransposeMatrix(sx);
        var matrixSxSize = sx.GetLength(0);
        var offset = matrixSxSize / 2;
        BypassMatrixGetGradient(g, sx, width, height, result, sy, offset);

        return result;
    }

    private static void BypassMatrixGetGradient(double[,] g, double[,] sx, int width, int height, double[,] result, double[,] sy, int offset)
    {
        for (int x = offset; x < width - offset; x++)
        {
            for (int y = offset; y < height - offset; y++)
            {
                var gx = RollUpMatrix(g, sx, x, y, offset);
                var gy = RollUpMatrix(g, sy, x, y, offset);
                result[x, y] = Math.Sqrt(gx * gx + gy * gy);
            }
        }
    }

    private static double[,] TransposeMatrix(double[,] matrix)
    {
        var size = matrix.GetLength(0);
        var result = new double[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                result[j, i] = matrix[i, j];
            }
        }
        return result;
    }

    private static double RollUpMatrix(double[,] image, double[,] matrixSx, int centerX, int centerY, int offset)
    {
        double result = 0.0;
        var matrixSxSize = matrixSx.GetLength(0);

        result = ConvolutionMatrix(image, matrixSx, centerX, centerY, offset, result, matrixSxSize);

        return result;
    }

    private static double ConvolutionMatrix(double[,] image, double[,] matrixSx, int centerX, int centerY, int offset, double result, int matrixSxSize)
    {
        for (int i = 0; i < matrixSxSize; i++)
        {
            for (int j = 0; j < matrixSxSize; j++)
            {
                var imageX = centerX + i - offset;
                var imageY = centerY + j - offset;
                result += image[imageX, imageY] * matrixSx[i, j];
            }
        }

        return result;
    }
}