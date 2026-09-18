# 32. Text Justification
- **Problem:** https://leetcode.com/problems/text-justification/ | **Difficulty:** Hard
- **Topic:** String, Greedy | **Pattern:** Greedy line construction.

## Explanation
Pack as many words as possible per line, distribute spaces evenly, and left-justify the final line. Example input `['This','is','an','example']`, width `16`.

## Clarifying Questions
- Is maxWidth positive? Are words shorter than maxWidth?
- Should the last line be left-justified?

## Brute Force
Try every grouping and then adjust spaces; unnecessary backtracking can be expensive.

## Optimized Approach
Greedily collect words fitting the line, calculate total gaps, distribute spaces, and handle the final line separately. **O(n)** relative to total characters, **O(n)** output space.

## Dry Run
Select words until adding another exceeds width; distribute extra spaces from left to right.

## Further Optimization
Use a StringBuilder to minimize intermediate allocations.

## C#
See `32-text-justification.cs`.
