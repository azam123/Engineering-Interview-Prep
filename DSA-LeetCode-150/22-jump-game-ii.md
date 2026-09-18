# 22. Jump Game II

- **Problem:** https://leetcode.com/problems/jump-game-ii/
- **Difficulty:** Medium
- **Topic:** Array, Greedy
- **Pattern:** Greedy range expansion
- **Data Structure / Algorithm:** Array traversal, BFS-like greedy layers

## Explanation
Find the minimum jumps needed to reach the final index. Track the farthest index reachable in the current jump range; when the current index reaches the range end, take another jump and extend the range.

Example: `[2,3,1,1,4]` → `2` jumps.

## Clarifying Questions
- Is the last index always reachable?
- Can the array contain one element?
- Are all jump lengths non-negative?

## Brute Force
Use recursion to try every legal jump. Worst-case time is exponential, with O(n) recursion depth.

## Optimized Greedy Approach
Maintain `currentEnd`, `farthest`, and `jumps`. Scan until the penultimate index. At each boundary, increment jumps and set the next boundary to `farthest`.

- **Time:** O(n)
- **Space:** O(1)

## Dry Run
`[2,3,1,1,4]`: first range reaches index 2; farthest becomes 4; second jump reaches the end.

## Further Optimization
The greedy range scan is asymptotically optimal.

See `22-jump-game-ii.cs`.
