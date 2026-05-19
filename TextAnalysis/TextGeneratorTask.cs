
namespace TextAnalysis;

using System.Text;

static class TextGeneratorTask
{
    public static string ContinuePhrase(
        Dictionary<string, string> nextWords,
        string phraseBeginning,
        int wordsCount)
    {
        var stringB = new StringBuilder(phraseBeginning);

        for (int i = 0; i < wordsCount; i++)
        {
            string[] phraseBeginningSplit = stringB.ToString().Split(' ');
            string lastWord = phraseBeginningSplit[phraseBeginningSplit.Length - 1];
            string preLastWord = phraseBeginningSplit.Length >= 2 ?
                phraseBeginningSplit[phraseBeginningSplit.Length - 2] : null;
            bool flowControl = WordAdd(nextWords, stringB, lastWord, preLastWord);
            if (!flowControl)
            {
                break;
            }
        }
        return stringB.ToString();
    }

    private static bool WordAdd(Dictionary<string, string> nextWords, StringBuilder stringB, string lastWord, string preLastWord)
    {
        bool wordAdd = false;

        if (preLastWord != "")
        {
            string twoWord = preLastWord + " " + lastWord;
            if (nextWords.ContainsKey(twoWord))
            {
                stringB.Append(" " + nextWords[twoWord]);
                wordAdd = true;
            }
        }

        if (!wordAdd && nextWords.ContainsKey(lastWord))
        {
            stringB.Append(" " + nextWords[lastWord]);
            wordAdd = true;
        }

        if (!wordAdd)
        {
            return false;
        }

        return true;
    }
}