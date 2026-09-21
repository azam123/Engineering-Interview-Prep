# 60. Decode Ways

**Pattern:** Dynamic Programming

## Approach
`dp[i]` represents the number of decodings for the first `i` characters. A valid one-digit code contributes `dp[i-1]`; a valid two-digit code contributes `dp[i-2]`.

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
            if (prev1 == 0) return 0;
        }
        return prev1;
    }
}
```

**Complexity:** O(n) time and O(1) extra space.
