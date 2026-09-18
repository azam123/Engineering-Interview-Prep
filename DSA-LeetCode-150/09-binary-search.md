# 9. Binary Search

- **Problem link:** https://leetcode.com/problems/binary-search/
- **Difficulty:** Easy
- **Topic:** Array, Binary Search
- **Pattern:** Divide and conquer on sorted data
- **Data structure / algorithm:** Iterative binary search

## Problem Explanation

Given a sorted array of distinct integers and a target, return the target index. Return `-1` if it does not exist.

Examples:

```text
nums = [-1,0,3,5,9,12], target = 9 => 4
nums = [-1,0,3,5,9,12], target = 2 => -1
```

## Clarifying Questions

1. Is the array sorted in ascending order?
2. Are values distinct?
3. Should the method support descending arrays?
4. What should be returned when the target is absent?

## Brute Force Approach and Solution

Scan the array from left to right.

```csharp
public int SearchBruteForce(int[] nums, int target)
{
    for (int i = 0; i < nums.Length; i++)
    {
        if (nums[i] == target) return i;
    }

    return -1;
}
```

- **Pattern / DSA:** Linear search
- **Time:** `O(n)`
- **Space:** `O(1)`
- **Drawback:** Does not exploit the sorted property of the input.

## Optimized Approach and Solution

Use two pointers, `left` and `right`. Compare the middle element with the target and discard half of the search range each iteration.

```csharp
public int Search(int[] nums, int target)
{
    int left = 0;
    int right = nums.Length - 1;

    while (left <= right)
    {
        int middle = left + (right - left) / 2;

        if (nums[middle] == target) return middle;
        if (nums[middle] < target)
            left = middle + 1;
        else
            right = middle - 1;
    }

    return -1;
}
```

- **Time:** `O(log n)`
- **Space:** `O(1)`

## Dry Run

For `nums = [-1,0,3,5,9,12]`, target `9`:

| Left | Right | Middle | Value | Action |
|---:|---:|---:|---:|---|
| 0 | 5 | 2 | 3 | Move left to 3 |
| 3 | 5 | 4 | 9 | Return 4 |

## Further Optimization?

The iterative solution is asymptotically optimal for this problem: it uses logarithmic time and constant auxiliary space. A recursive implementation is possible but uses `O(log n)` call-stack space.
