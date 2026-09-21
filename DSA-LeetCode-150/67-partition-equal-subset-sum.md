# 67. Partition Equal Subset Sum

**Pattern:** 0/1 Knapsack DP

## Approach
If the total sum is odd, partitioning is impossible. Otherwise, determine whether a subset can reach `sum / 2` using a reverse-updated boolean DP array.

## C# Solution
```csharp
public class Solution
{
    public bool CanPartition(int[] nums)
    {
        int total = nums.Sum();
        if (total % 2 != 0) return false;
        int target = total / 2;
        bool[] dp = new bool[target + 1];
        dp[0] = true;

        foreach (int num in nums)
            for (int sum = target; sum >= num; sum--)
                dp[sum] |= dp[sum - num];

        return dp[target];
    }
}
```

**Complexity:** O(n × target) time and O(target) space.
