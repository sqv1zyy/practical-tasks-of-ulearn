using System;

namespace Names;

internal static class HistogramTask
{
    public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
    {
        double[] countsDay = new double[31];
        for (int i = 0; i < names.Length; i++)
        {
            if (names[i].Name == name && names[i].BirthDate.Day != 1)
            {
                countsDay[names[i].BirthDate.Day - 1]++;
            }
        }

        return new HistogramData(
            $"Рождаемость людей с именем '{name}'",
            new[] { 
                "1", "2", "3", "4", "5", "6", "7", "8", "9", "10",
                "11", "12", "13", "14", "15", "16", "17", "18", "19",
                "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31" 
            },
            countsDay);
    }
}
