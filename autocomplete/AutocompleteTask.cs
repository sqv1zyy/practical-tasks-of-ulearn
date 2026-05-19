using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;

namespace Autocomplete;

internal class AutocompleteTask
{
    /// <returns>
    /// Возвращает первую фразу словаря, начинающуюся с prefix.
    /// </returns>
    /// <remarks>
    /// Эта функция уже реализована, она заработает,
    /// как только вы выполните задачу в файле LeftBorderTask
    /// </remarks>
    public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
    {
        var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
        if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
            return phrases[index];

        return null;
    }

    /// <returns>
    /// Возвращает первые в лексикографическом порядке count (или меньше, если их меньше count)
    /// элементов словаря, начинающихся с prefix.
    /// </returns>
    /// <remarks>Эта функция должна работать за O(log(n) + count)</remarks>
    public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
    {
        var indexTopByPrefix = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
        var counter = 0;
        var result = new string[count];

        if (indexTopByPrefix != -1 && indexTopByPrefix < phrases.Count)
        {
            for (int i = indexTopByPrefix; i < phrases.Count; i++)
            {
                if (counter < count)
                {
                    if (phrases[i].StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
                    {
                        result[counter] = phrases[i];
                        counter++;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        return result;
    }

    /// <returns>
    /// Возвращает количество фраз, начинающихся с заданного префикса
    /// </returns>
    public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
    {
        // тут стоит использовать написанные ранее классы LeftBorderTask и RightBorderTask
        return -1;
    }
}

[TestFixture]
public class AutocompleteTests
{
    [Test]
    public void TopByPrefix_IsEmpty_WhenNoPhrases()
    {
        // ...
        //CollectionAssert.IsEmpty(actualTopWords);
    }

    // ...

    [Test]
    public void CountByPrefix_IsTotalCount_WhenEmptyPrefix()
    {
        // ...
        //Assert.AreEqual(expectedCount, actualCount);
    }

    // ...
}