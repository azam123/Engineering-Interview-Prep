# Remove Element

- **Problem:** https://leetcode.com/problems/remove-element/
- **Difficulty:** Easy
- **Topic:** Array
- **Pattern:** Two Pointers / In-place filtering
- **Data Structure & Algorithm:** Array, read/write pointers

## Explanation
Remove every occurrence of `val` in-place and return the number `k` of remaining elements. The order of remaining elements may be changed.

Example: `nums = [3,2,2,3], val = 3` → `k = 2`, prefix becomes `[2,2]`.

## Clarifying Questions
- Is changing the order allowed?
- Must the array be modified in-place?
- Are values after the first `k` positions relevant?

## Brute Force
When a match is found, shift all later elements left by one. Repeat until the scan completes.
- **Time:** O(n²) worst case due to repeated shifts
- **Space:** O(1)
- **Drawback:** Repeated shifting causes unnecessary work.

## Optimized Approach
Use a write pointer. Scan each value; if it is not equal to `val`, copy it to `nums[write]` and increment `write`.

- **Time:** O(n)
- **Space:** O(1)

## Dry Run
`[3,2,2,3]`, `val=3`: skip first 3; write 2 at index 0; write 2 at index 1; skip final 3. Return `k=2`.

## Further Optimization
If order does not matter, swap matching elements with the last unchecked element, potentially reducing writes. The stable write-pointer approach is simpler and remains O(n).

## C#
See [`17-remove-element.cs`](17-remove-element.cs).