using System;
using System.Reflection.Emit;

namespace Names;

internal static class HeatmapTask
{
    public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
    {
        return new HeatmapData(
            "Пример карты интенсивностей",
            DataToHeatmap(names),
            LabelX(),
            LabelY());
    }

    public static string[] LabelX()
    {
        return NewMethod();
    }

    private static string[] NewMethod()
    {
        string[] labelX = new string[30];
        for (int i = 2; i < 32; i++)
        {
            labelX[i - 2] = i.ToString();
        }
        return labelX;
    }

    public static string[] LabelY()
    {
        string[] labelY = new string[12];
        for (int j = 1; j < 13; j++)
        {
            labelY[j - 1] = j.ToString();
        }
        return labelY;
    }

    public static double[,] DataToHeatmap(NameData[] names)
    {
        double[,] data = new double[30, 12];

        for (int k = 0; k < names.Length; k++)
        {
            int day = names[k].BirthDate.Day;
            int month = names[k].BirthDate.Month;
            if (names[k].BirthDate.Day != 1)
            {
                data[day - 2, month - 1]++;
            }
        }
        return data;
    }
}