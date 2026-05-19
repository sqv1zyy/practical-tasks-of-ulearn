namespace Pluralize;

public static class PluralizeTask
{
    public static string PluralizeRubles(int count)
    {
        var b = count % 10;
        var c = count % 100;
        List<int> lya = new List<int> { 2, 3, 4 };
        List<int> lei = new List<int> { 5, 6, 7, 8, 9, 0, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
        // Напишите функцию склонения слова "рублей" в зависимости от предшествующего числительного count.

        if (!(lei.Contains(c)) && (lya.Contains(b)))
        {
            return "рубля";
        }
        else if (lei.Contains(b) || lei.Contains(c))
        {
            return "рублей";
        }
        else
        {
            return "рубль";
        }

    }
}



