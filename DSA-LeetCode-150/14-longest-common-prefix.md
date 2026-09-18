# 14. Longest Common Prefix

- **Problem:** https://leetcode.com/problems/longest-common-prefix/
- **Difficulty:** Easy
- **Topic:** String, Array
- **Pattern / Data Structure / Algorithm:** Horizontal prefix comparison

## Explanation
Find the longest string that appears at the beginning of every string in an array.

Examples:
- `["flower", "flow", "flight"]` → `"fl"`
- `["dog", "racecar", "car"]` → `""`

## Clarifying Questions
- Can the array be empty?
- Can strings be empty?
- Is case sensitivity required?

## Brute Force Approach
Compare every character position across all strings while maintaining a prefix.

- **Time:** O(S), where S is the total number of characters inspected
- **Space:** O(1) auxiliary space
- **Drawback:** A less direct implementation can repeatedly compare the same prefix.

## Optimized Approach
Use the first string as the candidate prefix. Compare it with each following string and shorten the prefix until that string starts with it. Stop when the prefix becomes empty.

- **Time:** O(S)
- **Space:** O(1) auxiliary space

## Dry Run
For `["flower", "flow", "flight"]`, start with `flower`, reduce to `flow`, then reduce to `fl` after comparing with `flight`.

## Further Optimization
Sorting can compare only the first and last strings, but sorting costs O(n log n); the horizontal scan is preferable when input order should remain untouched.

See `14-longest-common-prefix.cs`.