# 34. Longest Substring Without Repeating Characters

- **Problem:** https://leetcode.com/problems/longest-substring-without-repeating-characters/
- **Difficulty:** Medium
- **Topic:** String, Sliding Window
- **Pattern:** Expand/shrink window with last-seen indexes

## Explanation
Maintain a window containing unique characters. When a repeated character appears, move the left pointer beyond its previous index. Update the maximum length after each step.

**Flow:** Add character → if repeated, move left → update maximum.

- **Brute force:** Enumerate all substrings and check uniqueness; O(n³) time.
- **Optimized:** Sliding window; O(n) time and O(min(n, charset)) space.
- **Dry run:** `abcabcbb` gives `abc` length 3.
- **Clarifying:** Is input ASCII or Unicode? Is empty input allowed?
- **Further optimization:** Use a fixed array for ASCII characters.

See the C# implementation for line-by-line structure.