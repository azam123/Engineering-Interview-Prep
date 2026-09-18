# Remove Duplicates from Sorted Array

- **Problem:** https://leetcode.com/problems/remove-duplicates-from-sorted-array/
- **Difficulty:** Easy
- **Topic:** Array
- **Pattern:** Two Pointers / In-place modification
- **Data Structure & Algorithm:** Sorted array, read/write pointers

## Explanation
Given a sorted array, remove duplicates in-place so each unique value appears once. Return the number `k` of unique values; the first `k` positions must contain the result.

Example: `nums = [1,1,2]` → `k = 2`, array starts with `[1,2]`.

## Clarifying Questions
- Is the input guaranteed to be sorted?
- Must the operation be in-place?
- Should values after index `k - 1` be ignored?

## Brute Force
Use a separate list or set to collect unique values, then copy them back.
- **Time:** O(n)
- **Space:** O(n)
- **Drawback:** Uses additional memory and does not strictly satisfy the in-place constraint.

## Optimized Approach
Keep `write = 1`. Scan from left to right with `read`. Whenever `nums[read] != nums[write - 1]`, write the new value at `nums[write]` and increment `write`.

- **Time:** O(n)
- **Space:** O(1)

## Dry Run
`[0,0,1,1,2]`: write=1 → read 1 skip; read 2 writes 1 at index 1; read 3 skip; read 4 writes 2 at index 2. Result prefix: `[0,1,2]`, `k=3`.

## Further Optimization
The two-pointer in-place solution is optimal in asymptotic time and auxiliary space.

## C#
See [`16-remove-duplicates-from-sorted-array.cs`](16-remove-duplicates-from-sorted-array.cs).