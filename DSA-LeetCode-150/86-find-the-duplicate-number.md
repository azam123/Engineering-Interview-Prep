# 86. Find the Duplicate Number

**Pattern:** Floyd's Cycle Detection

## C# Solution
```csharp
public class Solution
{
    public int FindDuplicate(int[] nums)
    {
        int slow = nums[0], fast = nums[0];
        do { slow = nums[slow]; fast = nums[nums[fast]]; }
        while (slow != fast);

        slow = nums[0];
        while (slow != fast) { slow = nums[slow]; fast = nums[fast]; }
        return slow;
    }
}
```

**Complexity:** `O(n)` time and `O(1)` space.
