# 72. Decode Ways

**Pattern:** Dynamic Programming

## Approach
`dp[i]` stores the number of ways to decode the first `i` characters. Add one-digit and valid two-digit transitions.

## C# Solution
```csharp
public class Solution
{
    public int NumDecodings(string s)
    {
        if (string.IsNullOrEmpty(s) || s[0] == '0') return 0;
        int prev2 = 1, prev1 = 1;

        for (int i = 1; i < s.Length; i++)
        {
            int current = 0;
            if (s[i] != '0') current += prev1;
            int two = (s[i - 1] - '0') * 10 + s[i] - '0';
            if (two >= 10 && two <= 26) current += prev2;
            prev2 = prev1;
            prev1 = current;
        }
        return prev1;
    }
}
```

## Complexity
- Time: O(n).
- Space: O(1).
