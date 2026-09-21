# 79. Partition Equal Subset Sum

**Pattern:** 0/1 Knapsack DP

## C# Solution
```csharp
public class Solution
{
    public bool CanPartition(int[] nums)
    {
        int total = nums.Sum();
        if (total % 2 != 0) return false;
        int target = total / 2;
        var dp = new bool[target + 1];
        dp[0] = true;

        foreach (int num in nums)
            for (int sum = target; sum >= num; sum--)
                dp[sum] |= dp[sum - num];

        return dp[target];
    }
}
```

## Complexity
- Time: O(n × target).
- Space: O(target).
