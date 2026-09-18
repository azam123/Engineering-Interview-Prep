# Maximum Subarray

- **Problem:** https://leetcode.com/problems/maximum-subarray/
- **Difficulty:** Medium
- **Topic:** Array, Dynamic Programming, Greedy
- **Pattern / DS / Algorithm:** Kadane's algorithm.

## Explanation
Find the contiguous subarray with the largest sum.

Example: `[-2,1,-3,4,-1,2,1,-5,4]` → `6`, from `[4,-1,2,1]`.

## Clarifying Questions
- Must the subarray be non-empty?
- Are negative numbers allowed?
- Do we need the sum only or the actual range too?

## Brute Force
Enumerate every start and end index while accumulating each subarray sum.

- **Time:** O(n²)
- **Space:** O(1)
- **Drawback:** Repeatedly evaluates overlapping subarrays.

## Optimized Approach
Kadane's algorithm maintains the best sum ending at the current position: `current = max(value, current + value)`. Track the global maximum.

- **Time:** O(n)
- **Space:** O(1)

## Dry Run
For `[−2,1,−3,4,−1,2,1]`, the best running sum becomes `6` after processing `4,-1,2,1`.

## Further Optimization
The O(n) time and O(1) auxiliary space solution is optimal for reading all elements.

## C#
See the matching `.cs` file.
