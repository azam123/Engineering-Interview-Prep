# 27. Trapping Rain Water
- **Problem:** https://leetcode.com/problems/trapping-rain-water/
- **Difficulty:** Hard | **Topic:** Array, Two Pointers
- **Pattern / DS / Algorithm:** Prefix maxima with two pointers.

## Explanation
Water above index `i` is limited by the shorter maximum wall on both sides. Example `[0,1,0,2,1,0,1,3,2,1,2,1] → 6`.

## Clarifying Questions
- Can heights be empty or contain zero values?
- Is extra linear space acceptable?

## Brute Force
For every index, scan left and right to find maxima: **O(n²)** time, **O(1)** space.

## Optimized Approach
Use `leftMax`, `rightMax`, and two pointers. Process the side with the smaller height; accumulated water is bounded by its maximum. **O(n)** time, **O(1)** space.

## Dry Run
At each pointer, add `leftMax-leftHeight` or `rightMax-rightHeight` when the corresponding side is lower.

## Further Optimization
The two-pointer approach is already optimal in asymptotic time and auxiliary space.

## C#
See `27-trapping-rain-water.cs`.
