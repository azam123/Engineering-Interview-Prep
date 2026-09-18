# 33. Longest Palindromic Substring

- **Problem:** https://leetcode.com/problems/longest-palindromic-substring/
- **Difficulty:** Medium
- **Topic:** String, Dynamic Programming
- **Pattern:** Expand Around Center
- **Data Structure:** String

## Explanation
A palindrome expands equally to the left and right. Every character and every gap can be a center.

**Flow:** `Choose center → Expand while equal → Track longest`

Example: `babad` → `bab` or `aba`.

## Clarifying Questions
- Is the input empty?
- If multiple answers exist, can I return any?
- Is case sensitivity relevant?

## Brute Force
Generate every substring and check whether it is a palindrome. This costs **O(n³)** time and **O(1)** extra space.

## Optimized Approach
Expand from each of `2n-1` centers. Each expansion costs O(n), giving **O(n²)** time and **O(1)** space.

## Dry Run
For `cbbd`, center between the two `b`s expands to `bb`, which becomes the answer.

## Further Optimization
Manacher’s algorithm achieves O(n) time but is more complex.

## C# Code
See [`33-longest-palindromic-substring.cs`](./33-longest-palindromic-substring.cs).

### Code Walkthrough
`Expand` moves outward while characters match. The returned length is used to calculate the substring’s start index. Odd and even centers are handled separately.
