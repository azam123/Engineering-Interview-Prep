# 23. H-Index

- **Problem:** https://leetcode.com/problems/h-index/
- **Difficulty:** Medium
- **Topic:** Array, Sorting, Counting
- **Pattern:** Counting / Sorting
- **Data Structure:** Array
- **Algorithm:** Sort citations and find the largest `h` where at least `h` papers have at least `h` citations.

## Problem Explanation

Given an array where `citations[i]` is the number of citations for a paper, return the researcher's H-Index: the largest `h` such that at least `h` papers have at least `h` citations each.

**Example:** `citations = [3,0,6,1,5]` → `3` because three papers have at least three citations.

## Clarifying Questions

1. Can the array be empty? Yes.
2. Are citation counts non-negative? Yes.
3. Does the original array need to remain unchanged? If yes, avoid in-place sorting.

## Brute Force

For every possible `h` from `0` to `n`, count how many papers have at least `h` citations.

- **Time:** O(n²)
- **Space:** O(1)
- **Drawback:** Repeatedly scans the array.

## Optimized Approach

Sort in descending order. At index `i`, there are `i + 1` papers considered. If `citations[i] >= i + 1`, then an H-Index of `i + 1` is possible.

- **Time:** O(n log n)
- **Space:** O(log n) to O(n), depending on sorting implementation.

## Dry Run

`[3,0,6,1,5]` → sorted descending: `[6,5,3,1,0]`

- 6 ≥ 1 → h=1
- 5 ≥ 2 → h=2
- 3 ≥ 3 → h=3
- 1 < 4 → stop

Answer = **3**.

## Further Optimization

A counting-array solution achieves **O(n)** time and **O(n)** space by grouping citation counts greater than or equal to `n` into bucket `n`.

## C# Solution

See [`23-H-Index.cs`](23-H-Index.cs).
