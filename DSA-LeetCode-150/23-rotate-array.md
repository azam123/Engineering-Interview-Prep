# 23. Rotate Array

- **Problem:** https://leetcode.com/problems/rotate-array/
- **Difficulty:** Medium
- **Topic:** Array
- **Pattern:** Reverse array / in-place transformation
- **Data Structure & Algorithm:** Array, three reversals

## Explanation
Rotate the array right by `k` positions. Example: `[1,2,3,4,5,6,7]`, `k=3` → `[5,6,7,1,2,3,4]`.

## Clarifying Questions
- Can `k` be larger than the array length? (Normalize with `k % n`.)
- Must the operation be in-place?
- What should happen for an empty or one-element array?

## Brute Force
Move the last element to the front one step at a time. This takes **O(nk)** time and **O(1)** space; it is slow for large `k`.

## Optimized Approach
Normalize `k`, reverse the complete array, reverse the first `k` elements, then reverse the remaining elements. This gives **O(n)** time and **O(1)** extra space.

## Dry Run
`[1,2,3,4,5,6,7], k=3` → reverse all → `[7,6,5,4,3,2,1]` → reverse first 3 → `[5,6,7,4,3,2,1]` → reverse rest → `[5,6,7,1,2,3,4]`.

## Further Optimization
The three-reversal method is asymptotically optimal because every element may need to move.

## C#
See `23-rotate-array.cs`.
