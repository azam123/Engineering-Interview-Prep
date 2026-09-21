# 65. Median of Two Sorted Arrays

**Pattern:** Binary Search on Partition

## Approach
Binary-search the smaller array. Choose partitions so every element on the left is less than or equal to every element on the right.

## C# Solution
```csharp
public class Solution
{
    public double FindMedianSortedArrays(int[] nums1, int[] nums2)
    {
        if (nums1.Length > nums2.Length) return FindMedianSortedArrays(nums2, nums1);
        int m = nums1.Length, n = nums2.Length, low = 0, high = m;

        while (low <= high)
        {
            int i = (low + high) / 2;
            int j = (m + n + 1) / 2 - i;
            int leftA = i == 0 ? int.MinValue : nums1[i - 1];
            int rightA = i == m ? int.MaxValue : nums1[i];
            int leftB = j == 0 ? int.MinValue : nums2[j - 1];
            int rightB = j == n ? int.MaxValue : nums2[j];

            if (leftA <= rightB && leftB <= rightA)
            {
                if ((m + n) % 2 == 1) return Math.Max(leftA, leftB);
                return (Math.Max(leftA, leftB) + Math.Min(rightA, rightB)) / 2.0;
            }
            if (leftA > rightB) high = i - 1;
            else low = i + 1;
        }
        return 0;
    }
}
```

**Complexity:** O(log min(m, n)) time and O(1) space.
