# Maximum Product Subarray

- **Problem:** https://leetcode.com/problems/maximum-product-subarray/
- **Difficulty:** Medium
- **Topic:** Array, Dynamic Programming
- **Pattern / DS / Algorithm:** Track maximum and minimum products ending at each index.

## Explanation
Return the largest product of a non-empty contiguous subarray.

Example: `[2,3,-2,4]` → `6`; `[-2,0,-1]` → `0`.

## Clarifying Questions
- Must the subarray be non-empty?
- Can the array contain zero and negative values?
- Is integer overflow a concern?

## Brute Force
Enumerate all start indices and extend the end index while maintaining a running product.

- **Time:** O(n²)
- **Space:** O(1)
- **Drawback:** Recomputes candidate ranges and can overflow quickly.

## Optimized Approach
Maintain both the maximum and minimum product ending at the current index because multiplying by a negative number swaps their roles. Reset naturally when zero is encountered.

- **Time:** O(n)
- **Space:** O(1)

## Dry Run
For `[2,3,-2,4]`, the product grows to `6`; after `-2`, the minimum product preserves the possibility of a later positive maximum. The final answer is `6`.

## Further Optimization
The linear-time constant-space approach is optimal because every element must be examined.

## C#
See the matching `.cs` file.
