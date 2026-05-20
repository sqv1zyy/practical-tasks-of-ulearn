using System;
using System.Collections.Generic;
using System.Text;

namespace PocketGoogle;

public class Indexer : IIndexer
{
    private readonly Dictionary<string, Dictionary<int, List<int>>> index = new();
    private readonly HashSet<char> separators = new() { ' ', '.', ',', '!', '?', ':', '-', '\r', '\n' };

    public void Add(int id, string documentText)
    {
        var words = ExtractWordsWithPositions(documentText);

        foreach (var (word, position) in words)
        {
            if (!index.ContainsKey(word))
            {
                index[word] = new Dictionary<int, List<int>>();
            }

            if (!index[word].ContainsKey(id))
            {
                index[word][id] = new List<int>();
            }

            index[word][id].Add(position);
        }
    }

    public List<int> GetIds(string word)
    {
        return index.TryGetValue(word, out var documents)
            ? new List<int>(documents.Keys)
            : new List<int>();
    }

    public List<int> GetPositions(int id, string word)
    {
        if (index.TryGetValue(word, out var documents) && documents.TryGetValue(id, out var positions))
        {
            return new List<int>(positions);
        }

        return new List<int>();
    }

    public void Remove(int id)
    {
        var wordsRemove = new List<string>();

        foreach (var word in index.Keys)
        {
            if (index[word].ContainsKey(id))
            {
                index[word].Remove(id);

                if (index[word].Count == 0)
                {
                    wordsRemove.Add(word);
                }
            }
        }

        foreach (var word in wordsRemove)
        {
            index.Remove(word);
        }
    }

    private List<(string word, int position)> ExtractWordsWithPositions(string text)
    {
        var result = new List<(string, int)>();
        var curWord = new StringBuilder();
        var curPosition = 0;

        curPosition = SplitWord(text, result, curWord, curPosition);

        AddWordAndPosition(result, curWord, curPosition);

        return result;
    }

    private static void AddWordAndPosition(List<(string, int)> result, StringBuilder curWord, int curPosition)
    {
        if (curWord.Length > 0)
        {
            result.Add((curWord.ToString(), curPosition));
        }
    }

    private int SplitWord(string text, List<(string, int)> result, StringBuilder curWord, int curPosition)
    {
        for (int i = 0; i < text.Length; i++)
        {
            char c = text[i];

            if (separators.Contains(c))
            {
                if (curWord.Length > 0)
                {
                    result.Add((curWord.ToString(), curPosition));
                    curWord.Clear();
                }
                curPosition = i + 1;
            }
            else
            {
                curWord.Append(c);
            }
        }

        return curPosition;
    }
}