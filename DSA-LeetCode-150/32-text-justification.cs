public static class TextJustification
{
    public static IList<string> FullJustify(string[] words, int maxWidth)
    {
        var result = new List<string>();
        for (int i = 0; i < words.Length;)
        {
            int j = i, letters = 0;
            while (j < words.Length && letters + words[j].Length + (j - i) <= maxWidth) { letters += words[j].Length; j++; }
            int gaps = j - i - 1, spaces = maxWidth - letters;
            var line = new StringBuilder();
            for (int k = i; k < j; k++)
            {
                line.Append(words[k]);
                if (k == j - 1) line.Append(new string(' ', maxWidth - line.Length));
                else line.Append(new string(' ', j == words.Length || j - i == 1 ? 1 : spaces / gaps + (k - i < spaces % gaps ? 1 : 0)));
            }
            result.Add(line.ToString()); i = j;
        }
        return result;
    }
}