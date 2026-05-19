namespace Passwords;

public class CaseAlternatorTask
{
    //Тесты будут вызывать этот метод
    public static List<string> AlternateCharCases(string lowercaseWord)
    {
        var result = new List<string>();
        AlternateCharCases(lowercaseWord.ToCharArray(), 0, result);
        return result;
    }

    static void AlternateCharCases(char[] word, int startIndex, List<string> result)
    {
   
        if (startIndex == word.Length)
        {
            result.Add(new string(word));
        }
        else if (char.IsLetter(word[startIndex])) 
        {
            var lowerChar = char.ToLower(word[startIndex]);
            var upperChar = char.ToUpper(word[startIndex]);

            word[startIndex] = lowerChar;
            AlternateCharCases(word, startIndex + 1, result);

            if (lowerChar != upperChar)
            {
                word[startIndex] = upperChar;
                AlternateCharCases(word, startIndex + 1, result);
            }
        }
        else
        {
            AlternateCharCases(word, startIndex + 1, result);
        }
       
    }
}