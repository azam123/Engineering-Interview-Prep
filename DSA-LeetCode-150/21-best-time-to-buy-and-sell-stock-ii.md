# 21. Best Time to Buy and Sell Stock II

- **Problem:** https://leetcode.com/problems/best-time-to-buy-and-sell-stock-ii/
- **Difficulty:** Easy
- **Topic:** Array, Greedy
- **Pattern:** Capture every positive price difference
- **Data Structure / Algorithm:** Array traversal, greedy

## Explanation
You may complete multiple transactions but must sell before buying again. Add every increase between consecutive days.

Example: `[7,1,5,3,6,4]` → `(5-1)+(6-3)=7`.

## Clarifying Questions
- Can we hold only one stock at a time?
- Are same-day buy and sell operations allowed?
- What should be returned for fewer than two prices?

## Brute Force
Try all possible buy/sell decisions recursively. This explores exponential combinations and is unnecessary because every profitable adjacent rise can be collected.

- **Time:** O(2^n) worst case
- **Space:** O(n) recursion
- **Drawback:** Repeats overlapping decisions.

## Optimized Greedy Approach
For each adjacent pair, if `prices[i] > prices[i-1]`, add the difference.

- **Time:** O(n)
- **Space:** O(1)

## Dry Run
`[1,2,3,2,5]`: profit = `1 + 1 + 3 = 5`.

## Further Optimization
The one-pass greedy solution is asymptotically optimal.

See `21-best-time-to-buy-and-sell-stock-ii.cs`.
