# Kth Largest Element in an Array

**Pattern:** Quickselect

```csharp
public class Solution
{
    public int FindKthLargest(int[] nums, int k)
    {
        Array.Sort(nums);
        return nums[nums.Length - k];
    }
}
```

**Complexity:** O(n log n) time and O(1) auxiliary space. Interview follow-up: implement Quickselect for average O(n) time.
