# 29. Reverse Words in a String
- **Problem:** https://leetcode.com/problems/reverse-words-in-a-string/ | **Difficulty:** Medium
- **Topic:** String | **Pattern:** Tokenization and reverse traversal.

## Explanation
Return words in reverse order, removing extra spaces. Example: `"the sky is blue" → "blue is sky the"`.

## Clarifying Questions
- Should repeated spaces be ignored?
- Is leading/trailing whitespace allowed?

## Brute Force
Split, reverse the word list, and join: **O(n)** time, **O(n)** space.

## Optimized Approach
Trim, split on whitespace, reverse tokens, and join. **O(n)** time and **O(n)** space; practical and readable in C#.

## Dry Run
`" hello world "` → tokens `[hello, world]` → `"world hello"`.

## Further Optimization
Manual in-place reversal can reduce allocations when working with a character array.

## C#
See `29-reverse-words-in-a-string.cs`.
