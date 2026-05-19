using Splat;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Autocomplete;

// Внимание!
// Есть одна распространенная ловушка при сравнении строк: строки можно сравнивать по-разному:
// с учетом регистра, без учета, зависеть от кодировки и т.п.
// В файле словаря все слова отсортированы методом StringComparison.InvariantCultureIgnoreCase.
// Во всех функциях сравнения строк в C# можно передать способ сравнения.
public class LeftBorderTask
{
    /// <returns>
    /// Возвращает индекс левой границы.
    /// То есть индекс максимальной фразы, которая не начинается с prefix и меньшая prefix.
    /// Если такой нет, то возвращает -1 НАЙТИ ФРАЗУ НЕ НАЧИНАЮЩУЮСЯ НА ПРЕФИКС И МЕНЬШЕ ЕГО и ВЕРНУТЬ ЕЁ
    /// </returns>
    /// <remarks>
    /// Функция должна быть рекурсивной
    /// и работать за O(log(items.Length)*L), где L — ограничение сверху на длину фразы
    /// </remarks>
    
    public static int GetLeftBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
    {
        if (left == -1)
        {
            left = 0;
        }
        if (left > right || right < 0 || left >= phrases.Count)
        {
            return -1;
        }

        var middle = left + (right - left) / 2;
        int result = string.Compare(phrases[middle], prefix, StringComparison.InvariantCultureIgnoreCase);

        if (result < 0)
        {
            var rightResult = GetLeftBorderIndex(phrases, prefix, middle + 1, right);
            return rightResult != -1 ? rightResult : middle;
        }
        else
        {
            return GetLeftBorderIndex(phrases, prefix, left, middle - 1);
        }
    }
}