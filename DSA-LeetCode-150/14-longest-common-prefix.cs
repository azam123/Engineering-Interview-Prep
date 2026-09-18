using System;

public static class LongestCommonPrefix
{
    public static string Find(string[] strs)
    {
        if (strs == null || strs.Length == 0)
            return string.Empty;

        string prefix = strs[0];
        for (int i = 1; i < strs.Length; i++)
        {
            while (!strs[i].StartsWith(prefix, StringComparison.Ordinal))
            {
                if (prefix.Length == 0)
                    return string.Empty;
                prefix = prefix[..^1];
            }
        }

        return prefix;
    }
}