public static class LongestPalindromicSubstring
{
    public static string Find(string s)
    {
        if (string.IsNullOrEmpty(s)) return string.Empty;
        int start = 0, maxLength = 1;
        for (int i = 0; i < s.Length; i++)
        {
            Expand(s, i, i, ref start, ref maxLength);
            Expand(s, i, i + 1, ref start, ref maxLength);
        }
        return s.Substring(start, maxLength);
    }

    private static void Expand(string s, int left, int right, ref int start, ref int maxLength)
    {
        while (left >= 0 && right < s.Length && s[left] == s[right])
        {
            if (right - left + 1 > maxLength)
            {
                start = left;
                maxLength = right - left + 1;
            }
            left--; right++;
        }
    }
}