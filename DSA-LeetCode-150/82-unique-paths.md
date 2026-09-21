# 82. Unique Paths

**Pattern:** Dynamic Programming

## Approach
`dp[j]` stores the number of ways to reach the current cell. Each cell can be reached from above or from the left.

## C# Solution
```csharp
public class Solution
{
    public int UniquePaths(int m, int n)
    {
        int[] dp = new int[n];
        Array.Fill(dp, 1);
        for (int row = 1; row < m; row++)
            for (int col = 1; col < n; col++)
                dp[col] += dp[col - 1];
        return dp[n - 1];
    }
}
```

**Complexity:** `O(mn)` time and `O(n)` space.
