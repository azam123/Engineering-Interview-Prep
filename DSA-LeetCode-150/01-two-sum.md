# 1. Two Sum

- **Problem link:** https://leetcode.com/problems/two-sum/
- **Difficulty:** Easy
- **Topic:** Array, Hash Map
- **Pattern:** Complement lookup
- **Data structure / algorithm:** Dictionary (hash map), one-pass scan

## Problem Explanation

Given an integer array `nums` and a target, return the indices of two distinct values whose sum equals the target. Exactly one solution is guaranteed.

Examples:

```text
nums = [2,7,11,15], target = 9
output = [0,1]

nums = [3,2,4], target = 6
output = [1,2]
```

## Clarifying Questions

1. Can the array contain negative numbers or duplicates?
2. Is exactly one solution guaranteed?
3. May I return the indices in any order?
4. Should the same element ever be used twice? (No.)

## Brute Force Approach

Check every pair `(i, j)` where `i < j`, and return when `nums[i] + nums[j] == target`.

```csharp
public int[] TwoSumBruteForce(int[] nums, int target)
{
    for (int i = 0; i < nums.Length; i++)
    {
        for (int j = i + 1; j < nums.Length; j++)
        {
            if (nums[i] + nums[j] == target)
                return [i, j];
        }
    }

    return [];
}
```

- **Pattern / DSA:** Nested loops, array traversal
- **Time:** `O(n²)`
- **Space:** `O(1)` auxiliary space
- **Drawback:** Repeats pair checks and becomes slow as input size grows.

## Optimized Approach

For each number, calculate its complement: `target - nums[i]`. If the complement already exists in a dictionary, the answer is found. Otherwise, store the current number and index.

```csharp
public int[] TwoSum(int[] nums, int target)
{
    var seen = new Dictionary<int, int>();

    for (int i = 0; i < nums.Length; i++)
    {
        int complement = target - nums[i];

        if (seen.TryGetValue(complement, out int index))
            return [index, i];

        seen[nums[i]] = i;
    }

    return [];
}
```

- **Time:** `O(n)` average
- **Space:** `O(n)`

## Dry Run

For `[2, 7, 11, 15]`, target `9`:

| i | Value | Complement | Dictionary | Result |
|---|---:|---:|---|---|
| 0 | 2 | 7 | `{2:0}` | Continue |
| 1 | 7 | 2 | `{2:0}` | `[0,1]` |

## Further Optimization?

A sorting plus two-pointer solution is possible, but preserving original indices requires storing index-value pairs and sorting them. It uses `O(n log n)` time and is not faster than the hash-map approach for this problem.
