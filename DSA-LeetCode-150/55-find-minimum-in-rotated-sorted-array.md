# 55. Find Minimum in Rotated Sorted Array

**Pattern:** Binary Search

```csharp
public int FindMin(int[] nums)
{
    int left = 0, right = nums.Length - 1;
    while (left < right)
    {
        int mid = left + (right - left) / 2;
        if (nums[mid] > nums[right]) left = mid + 1;
        else right = mid;
    }
    return nums[left];
}
```

**Complexity:** O(log n) time and O(1) space.

**Follow-ups:** How does the solution change when duplicates are allowed? Why compare with the right boundary?
