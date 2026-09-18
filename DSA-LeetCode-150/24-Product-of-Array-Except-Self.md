# 24. Product of Array Except Self

- **Problem:** https://leetcode.com/problems/product-of-array-except-self/
- **Difficulty:** Medium
- **Topic:** Array, Prefix/Suffix
- **Pattern:** Prefix product + suffix product
- **Data Structure:** Array
- **Algorithm:** Build prefix products and combine with a running suffix product.

## Problem Explanation

Return an array where `answer[i]` equals the product of every element except `nums[i]`. Division is not allowed, and the solution should run in O(n).

**Example:** `[1,2,3,4]` → `[24,12,8,6]`.

## Clarifying Questions

1. Can the array contain zero? Yes.
2. Are negative values possible? Yes.
3. Is division allowed? No.
4. Should the result use O(1) extra space excluding the output array? Yes.

## Brute Force

For every index, multiply all other elements.

- **Time:** O(n²)
- **Space:** O(1) excluding output
- **Drawback:** Repeats almost the same multiplication work for every position.

## Optimized Approach

Store the product of all elements to the left of each index in `answer`. Then traverse from right to left while maintaining the product of elements to the right and multiply it into `answer[i]`.

- **Time:** O(n)
- **Space:** O(1) extra excluding the output array.

## Dry Run

`[1,2,3,4]`

Prefix phase → `[1,1,2,6]`.

Right-to-left suffix products:
- i=3: `6 * 1 = 6`
- i=2: `2 * 4 = 8`
- i=1: `1 * 12 = 12`
- i=0: `1 * 24 = 24`

Result: `[24,12,8,6]`.

## Further Optimization

The two-pass approach is already optimal at O(n) time and O(1) auxiliary space beyond the returned array.

## C# Solution

See [`24-Product-of-Array-Except-Self.cs`](24-Product-of-Array-Except-Self.cs).
