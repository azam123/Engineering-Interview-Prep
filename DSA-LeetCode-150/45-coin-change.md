# 45. Coin Change

**Problem:** Find the minimum number of coins needed to make a target amount.

## Approach
Use bottom-up dynamic programming. `dp[a]` stores the minimum coins required for amount `a`. Initialize values as `amount + 1`, then try every coin for each amount.

## C# Solution
```csharp
public class Solution
{
    public int CoinChange(int[] coins, int amount)
    {
        var dp = new int[amount + 1];
        Array.Fill(dp, amount + 1);
        dp[0] = 0;

        for (int current = 1; current <= amount; current++)
        {
            foreach (int coin in coins)
            {
                if (coin <= current)
                    dp[current] = Math.Min(dp[current], dp[current - coin] + 1);
            }
        }

        return dp[amount] > amount ? -1 : dp[amount];
    }
}
```

**Complexity:** `O(amount × numberOfCoins)` time and `O(amount)` space.

**Follow-ups:** Count the number of combinations, bounded coin supply, and reconstruct the selected coins.
