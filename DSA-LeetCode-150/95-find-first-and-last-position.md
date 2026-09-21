# 95. Find First and Last Position of Element in Sorted Array

**Pattern:** Binary Search

## C# Solution
```csharp
public class Solution
{
    public int[] SearchRange(int[] nums, int target)
    {
        int first = Bound(nums, target, true);
        if (first == nums.Length || nums[first] != target) return new[] { -1, -1 };
        return new[] { first, Bound(nums, target, false) - 1 };
    }

    private int Bound(int[] nums, int target, bool lower)
    {
        int left = 0, right = nums.Length;
        while (left < right)
        {
            int mid = left + (right - left) / 2;
            if (nums[mid] > target || (lower && nums[mid] == target)) right = mid;
            else left = mid + 1;
        }
        return left;
    }
}
```

**Complexity:** O(log n) time and O(1) space.
