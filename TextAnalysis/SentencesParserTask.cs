namespace TextAnalysis;

using System.Text;
static class SentencesParserTask
{
    public static List<List<string>> ParseSentences(string text)
    {
        var sentencesList = new List<List<string>>();
        var sentences = text.Split(new char[] { '.', '!', '?', ';'
            , ':', ')', '(' }, StringSplitOptions.RemoveEmptyEntries);

        SentencesFromText(sentencesList, sentences);
        return sentencesList;
    }

    private static void SentencesFromText(List<List<string>> sentencesList, string[] sentences)
    {
        foreach (var sentence in sentences)
        {
            LowerWordFromSentences(sentencesList, sentence);
        }
    }

    private static void LowerWordFromSentences(List<List<string>> sentencesList, string sentence)
    {
        var sentenceWordLower = new List<string>();
        var word = new StringBuilder();
        CheckingForChar(sentencesList, sentence, sentenceWordLower, word);
    }

    private static void CheckingForChar(List<List<string>> sentencesList, string sentence, List<string> sentenceWordLower, StringBuilder word)
    {
        foreach (var ch in sentence)
        {
            if ((char.IsLetter(ch)) || (ch == '\''))
            {
                word.Append(char.ToLower(ch));
            }
            else
            {
                if (word.Length > 0)
                {
                    sentenceWordLower.Add(word.ToString().ToLower());
                    word.Clear();
                }
            }
        }
        if (word.Length > 0)
        {
            sentenceWordLower.Add(word.ToString().ToLower());
        }
        if (sentenceWordLower.Count > 0)
        {
            sentencesList.Add(sentenceWordLower);
        }
    }
}