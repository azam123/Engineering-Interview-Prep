# 13. Length of Last Word

- **Problem:** https://leetcode.com/problems/length-of-last-word/
- **Difficulty:** Easy
- **Topic:** String
- **Pattern / Data Structure / Algorithm:** Reverse scan with constant space

## Explanation
Return the length of the final word in a string containing words separated by spaces.

Examples:
- `"Hello World"` → `5`
- `" fly me to the moon "` → `4`

## Clarifying Questions
- Can the string contain leading or trailing spaces?
- Are words separated only by spaces?
- Can the input be empty?

## Brute Force Approach
Trim the string, split it by spaces, and return the length of the last token.

- **Time:** O(n)
- **Space:** O(n)
- **Drawback:** Allocates additional strings and arrays unnecessarily.

## Optimized Approach
Scan backward, skip trailing spaces, then count characters until the next space or beginning of the string.

- **Time:** O(n)
- **Space:** O(1)

## Dry Run
For `"Hello World  "`, skip trailing spaces, count `World`, and return `5`.

## Further Optimization
The reverse scan is optimal in asymptotic terms and avoids allocations.

See `13-length-of-last-word.cs`.