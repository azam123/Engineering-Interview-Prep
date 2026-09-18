# 24. Insert Delete GetRandom O(1)

- **Problem:** https://leetcode.com/problems/insert-delete-getrandom-o1/
- **Difficulty:** Medium
- **Topic:** Array, Hash Table, Design
- **Pattern:** Hash Map + Dynamic Array
- **Data Structure:** `Dictionary<int,int>` + `List<int>`
- **Algorithm:** Store each value's index in a hash map; remove by swapping the target with the last array element.

## Problem Explanation

Design a structure supporting `Insert`, `Remove`, and `GetRandom`, each in average O(1) time. Values are unique.

**Example:** Insert 1, insert 2, remove 1 → the remaining collection is `[2]`, so `GetRandom()` returns 2.

## Clarifying Questions

1. Are values unique? Yes.
2. Should `GetRandom` return each current value with equal probability? Yes.
3. What should operations return when insert/remove cannot be performed? Boolean success/failure.

## Brute Force

Use a list for storage. Insert is O(1), but removal requires searching for the value and shifting elements.

- **Time:** Remove O(n), GetRandom O(1)
- **Space:** O(n)
- **Drawback:** Removal violates the required average O(1).

## Optimized Approach

Maintain:

1. A list of values for O(1) random indexing.
2. A dictionary mapping value → its list index.

For removal, replace the target with the last value, update its index, then remove the last element.

- **Time:** Average O(1) for all operations
- **Space:** O(n)

## Dry Run

Insert `10`, `20`, `30` → list `[10,20,30]`.

Remove `20`: move `30` to index 1 → `[10,30,30]`, then remove last → `[10,30]`. Update `30 → 1`.

## Further Optimization

This is already the standard average O(1) design. A cryptographically uniform random generator is unnecessary unless stronger randomness requirements are specified.

## C# Solution

See [`24-Insert-Delete-GetRandom-O1.cs`](24-Insert-Delete-GetRandom-O1.cs).
