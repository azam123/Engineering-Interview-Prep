# Search Insert Position

- **Problem:** https://leetcode.com/problems/search-insert-position/
- **Difficulty:** Easy
- **Topic:** Array, Binary Search
- **Pattern / DS / Algorithm:** Binary search on a sorted array.

## Explanation
Given a sorted array of distinct integers, return the index of a target. If it is absent, return the index where it should be inserted.

Example: `nums = [1,3,5,6]`, `target = 2` → `1`; `target = 7` → `4`.

## Clarifying Questions
- Is the array sorted in ascending order?
- Are values distinct?
- Should the insertion index be returned when the target is missing?

## Brute Force
Scan from left to right and return the first index whose value is greater than or equal to the target; otherwise return the array length.

- **Time:** O(n)
- **Space:** O(1)
- **Drawback:** Does not use the sorted property.

## Optimized Approach
Use binary search. If `nums[mid] < target`, move right; otherwise record `mid` as a possible answer and move left.

- **Time:** O(log n)
- **Space:** O(1)

## Dry Run
For `[1,3,5,6]`, target `2`: midpoint value `5` moves left; value `3` moves left; value `1` moves right. The insertion index is `1`.

## Further Optimization
The binary-search solution is asymptotically optimal for comparison-based searching.

## C#
See the matching `.cs` file for brute-force and optimized implementations.
