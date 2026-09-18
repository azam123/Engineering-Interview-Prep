using System;
using System.Collections.Generic;

public static class RomanToInteger
{
    public static int RomanToInt(string s)
    {
        var values = new Dictionary<char, int>
        {
            ['I'] = 1, ['V'] = 5, ['X'] = 10,
            ['L'] = 50, ['C'] = 100, ['D'] = 500, ['M'] = 1000
        };

        int result = 0;
        for (int i = 0; i < s.Length; i++)
        {
            int current = values[s[i]];
            if (i + 1 < s.Length && current < values[s[i + 1]])
                result -= current;
            else
                result += current;
        }

        return result;
    }
}