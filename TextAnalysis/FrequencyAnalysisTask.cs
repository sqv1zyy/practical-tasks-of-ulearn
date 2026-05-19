namespace TextAnalysis;



static class FrequencyAnalysisTask
{
    public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
    {
        var result = new Dictionary<string, string>();
        var bigramFrequencyAll = BuildFrequencyDictionary(text, 2);
        var trigramFrequencyAll = BuildFrequencyDictionary(text, 3);
        var bigramResult = GetFrequentContinuation(bigramFrequencyAll);
        var trigramResult = GetFrequentContinuation(trigramFrequencyAll);
        JoinResults(result, bigramResult);
        JoinResults(result, trigramResult);

        return result;
    }

    private static Dictionary<string, Dictionary<string, int>> BuildFrequencyDictionary(
        List<List<string>> text,
        int n)
    {
        var frequencyDict = new Dictionary<string, Dictionary<string, int>>();

        foreach (var sentence in text)
        {
            CountNgram(n, frequencyDict, sentence);
        }

        return frequencyDict;
    }

    private static void CountNgram(int n, Dictionary<string,
        Dictionary<string, int>> frequencyDict, List<string> sentence)
    {
        for (int i = 0; i <= sentence.Count - n; i++)
            UpdateStatsDict(n, frequencyDict, sentence, i);
    }

    private static void UpdateStatsDict(int n, Dictionary<string,
        Dictionary<string, int>> frequencyDict, List<string> sentence, int i)
    {
        string startGram, nextWord;
        ExtractPair(n, sentence, i, out startGram, out nextWord);

        if (!frequencyDict.ContainsKey(startGram))
        {
            frequencyDict[startGram] = new Dictionary<string, int>();
        }

        if (!frequencyDict[startGram].ContainsKey(nextWord))
        {
            frequencyDict[startGram][nextWord] = 0;
        }

        frequencyDict[startGram][nextWord]++;
    }

    private static void ExtractPair(int n, List<string> sentence,
        int i, out string startGram, out string nextWord)
    {
        startGram = "";
        if (n == 2)
        {
            startGram = sentence[i];
        }
        else
        {
            startGram = sentence[i] + " " + sentence[i + 1];
        }

        nextWord = sentence[i + n - 1];
    }

    private static Dictionary<string, string> GetFrequentContinuation(
        Dictionary<string, Dictionary<string, int>> frequencyDict)
    {
        var result = new Dictionary<string, string>();

        foreach (var pair in frequencyDict)
        {
            string start = pair.Key;
            var nextWords = pair.Value;

            string mostFrequent = "";
            int maxFrequency = -1;

            FindNewPair(nextWords, ref mostFrequent, ref maxFrequency);

            result[start] = mostFrequent;
        }

        return result;
    }

    private static void FindNewPair(Dictionary<string, int> nextWords,
        ref string mostFrequent, ref int maxFrequency)
    {
        foreach (var nextWordPair in nextWords)
        {
            string nextWord = nextWordPair.Key;
            int frequency = nextWordPair.Value;

            if (frequency > maxFrequency)
            {
                maxFrequency = frequency;
                mostFrequent = nextWord;
            }
            else if (frequency == maxFrequency)
            {
                if (string.CompareOrdinal(nextWord, mostFrequent) < 0)
                {
                    mostFrequent = nextWord;
                }
            }
        }
    }

    private static void JoinResults(
        Dictionary<string, string> target,
        Dictionary<string, string> source)
    {
        foreach (var pair in source)
        {
            target[pair.Key] = pair.Value;
        }
    }
}