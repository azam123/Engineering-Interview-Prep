# 8. Contains Duplicate

- **Problem link:** https://leetcode.com/problems/contains-duplicate/
- **Difficulty:** Easy
- **Topic:** Array, Hash Set
- **Pattern:** Seen-element tracking
- **Data structure / algorithm:** HashSet, one-pass scan

## Problem Explanation

Given an integer array, return `true` if any value appears at least twice; otherwise return `false`.

Examples:

```text
nums = [1,2,3,1] => true
nums = [1,2,3,4] => false
nums = [1,1,1,3,3,4,3,2,4,2] => true
```

## Clarifying Questions

1. Can the array be empty?
2. Can values be negative?
3. Is modifying the input array allowed?
4. What are the expected input-size constraints?

## Brute Force Approach and Solution

Compare every pair of elements.

```csharp
public bool ContainsDuplicateBruteForce(int[] nums)
{
    for (int i = 0; i < nums.Length; i++)
    {
        for (int j = i + 1; j < nums.Length; j++)
        {
            if (nums[i] == nums[j]) return true;
        }
    }

    return false;
}
```

- **Pattern / DSA:** Nested loops, array comparison
- **Time:** `O(n²)`
- **Space:** `O(1)`
- **Drawback:** Pairwise comparisons scale poorly for large arrays.

## Optimized Approach and Solution

Store each value in a `HashSet`. If a value already exists, a duplicate has been found.

```csharp
public bool ContainsDuplicate(int[] nums)
{
    var seen = new HashSet<int>();

    foreach (int number in nums)
    {
        if (!seen.Add(number)) return true;
    }

    return false;
}
```

- **Time:** `O(n)` average
- **Space:** `O(n)`

## Dry Run

For `nums = [1, 2, 3, 1]`:

| Value | Set before | Result |
|---:|---|---|
| 1 | `{}` | Add 1 |
| 2 | `{1}` | Add 2 |
| 3 | `{1,2}` | Add 3 |
| 1 | `{1,2,3}` | Duplicate found |

## Further Optimization?

Sorting allows duplicate detection using `O(1)` auxiliary space if in-place sorting is allowed, but it increases time complexity to `O(n log n)` and modifies the input.

```csharp
public bool ContainsDuplicateUsingSorting(int[] nums)
{
    Array.Sort(nums);

    for (int i = 1; i < nums.Length; i++)
    {
        if (nums[i] == nums[i - 1]) return true;
    }

    return false;
}
```
