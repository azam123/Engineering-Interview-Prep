# 15. 3Sum Closest

- **Problem:** https://leetcode.com/problems/3sum-closest/
- **Difficulty:** Medium
- **Topic:** Array, Sorting, Two Pointers
- **Pattern / Data Structure / Algorithm:** Sort + two pointers

## Explanation
Find three numbers whose sum is closest to a given target.

Example: `[-1, 2, 1, -4]`, target `1` → `2`.

## Clarifying Questions
- Is the array guaranteed to contain at least three numbers?
- Can values be negative or repeated?
- If two sums are equally close, what tie behavior is expected?

## Brute Force Approach
Try every combination of three indices and retain the sum with the smallest absolute difference from the target.

- **Time:** O(n³)
- **Space:** O(1) auxiliary space
- **Drawback:** Too many combinations for large arrays.

## Optimized Approach
Sort the array. Fix one number, then use left and right pointers for the remaining two numbers. Move the left pointer when the sum is below the target; otherwise move the right pointer.

- **Time:** O(n²) after sorting
- **Space:** O(1) auxiliary space, excluding sorting implementation details

## Dry Run
Sort `[-1,2,1,-4]` to `[-4,-1,1,2]`. Fix `-4`, evaluate pointer sums, then continue with the next fixed element while tracking the closest sum.

## Further Optimization
The O(n²) approach is the standard practical bound for this problem; pruning can reduce work in some inputs but does not improve the worst-case complexity.

See `15-three-sum-closest.cs`.