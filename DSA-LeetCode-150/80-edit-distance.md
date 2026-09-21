# 80. Edit Distance

**Pattern:** Dynamic Programming

## Approach
Let `dp[i][j]` represent the minimum edits needed to convert the first `i` characters of `word1` into the first `j` characters of `word2`. If characters match, carry the diagonal value; otherwise choose insert, delete, or replace.

## C# Solution
```csharp
public class Solution
{
    public int MinDistance(string word1, string word2)
    {
        int m = word1.Length, n = word2.Length;
        int[,] dp = new int[m + 1, n + 1];

        for (int i = 0; i <= m; i++) dp[i, 0] = i;
        for (int j = 0; j <= n; j++) dp[0, j] = j;

        for (int i = 1; i <= m; i++)
        {
            for (int j = 1; j <= n; j++)
            {
                if (word1[i - 1] == word2[j - 1])
                    dp[i, j] = dp[i - 1, j - 1];
                else
                    dp[i, j] = 1 + Math.Min(dp[i - 1, j - 1],
                        Math.Min(dp[i - 1, j], dp[i, j - 1]));
            }
        }

        return dp[m, n];
    }
}
```

**Complexity:** `O(mn)` time and `O(mn)` space.

**Interview Notes:** Explain insert, delete, and replace transitions clearly.
