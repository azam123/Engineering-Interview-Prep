# 31. Find the Index of the First Occurrence in a String
- **Problem:** https://leetcode.com/problems/find-the-index-of-the-first-occurrence-in-a-string/ | **Difficulty:** Medium
- **Topic:** String | **Pattern:** Sliding window / substring search.

## Explanation
Return the first index where `needle` occurs in `haystack`, or `-1`. Example: `sadbutsad`, `sad` → `0`.

## Clarifying Questions
- What should happen when needle is empty?
- Is case sensitivity required?

## Brute Force
Try matching needle at every valid starting index: **O(nm)** time, **O(1)** space.

## Optimized Approach
Use built-in `IndexOf` (typically optimized internally) or KMP for guaranteed **O(n+m)** time. This implementation uses `IndexOf`.

## Dry Run
Check candidate windows from left to right and stop at the first match.

## Further Optimization
KMP avoids repeated comparisons for adversarial inputs.

## C#
See `31-find-index-first-occurrence.cs`.
