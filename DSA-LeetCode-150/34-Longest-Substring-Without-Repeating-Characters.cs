using System.Collections.Generic;
public static class LongestUniqueSubstring
{
    public static int LengthOfLongestSubstring(string s)
    {
        var last = new Dictionary<char, int>(); int left = 0, best = 0;
        for (int right = 0; right < s.Length; right++)
        {
            if (last.TryGetValue(s[right], out int index) && index >= left) left = index + 1;
            last[s[right]] = right;
            best = System.Math.Max(best, right - left + 1);
        }
        return best;
    }
}