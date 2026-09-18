# 3. Merge Sorted Array

- **Problem link:** https://leetcode.com/problems/merge-sorted-array/
- **Difficulty:** Easy
- **Topic:** Array, Two Pointers
- **Pattern:** Merge from the end
- **Data structure / algorithm:** In-place two pointers

## Problem Explanation

`nums1` contains `m` valid sorted elements followed by enough space for `n` elements from sorted `nums2`. Merge `nums2` into `nums1` in non-decreasing order.

Example:

```text
nums1 = [1,2,3,0,0,0], m = 3
nums2 = [2,5,6], n = 3
result = [1,2,2,3,5,6]
```

## Clarifying Questions

1. Must the merge happen in-place? (Yes.)
2. Are both arrays sorted in ascending order?
3. Can either array be empty?
4. Are duplicate values allowed? (Yes.)

## Brute Force Approach

Copy the `n` values from `nums2` into the unused portion of `nums1`, then sort the complete array.

```csharp
public void MergeBruteForce(int[] nums1, int m, int[] nums2, int n)
{
    Array.Copy(nums2, 0, nums1, m, n);
    Array.Sort(nums1);
}
```

- **Pattern / DSA:** Array copy and sorting
- **Time:** `O((m+n) log(m+n))`
- **Space:** Depends on sorting implementation; typically `O(log(m+n))` auxiliary stack space
- **Drawback:** Does unnecessary sorting even though both inputs are already sorted.

## Optimized Approach

Use three pointers: `i = m-1`, `j = n-1`, and `k = m+n-1`. Compare the largest remaining elements and write the larger one at the end. Working backward prevents overwriting unprocessed values.

```csharp
public void Merge(int[] nums1, int m, int[] nums2, int n)
{
    int i = m - 1;
    int j = n - 1;
    int k = m + n - 1;

    while (j >= 0)
    {
        if (i >= 0 && nums1[i] > nums2[j])
            nums1[k--] = nums1[i--];
        else
            nums1[k--] = nums2[j--];
    }
}
```

- **Time:** `O(m+n)`
- **Space:** `O(1)` auxiliary space

## Dry Run

`nums1 = [1,2,3,0,0,0]`, `nums2 = [2,5,6]`

1. Compare `3` and `6`; write `6` at index `5`.
2. Compare `3` and `5`; write `5` at index `4`.
3. Compare `3` and `2`; write `3` at index `3`.
4. Compare `2` and `2`; write `2` at index `2`.
5. Copy remaining `2` and `1` into indices `1` and `0`.

Result: `[1,2,2,3,5,6]`.

## Further Optimization?

The solution is asymptotically optimal: all relevant elements may need to be examined, so `O(m+n)` time and `O(1)` auxiliary space cannot be improved in the general case.
