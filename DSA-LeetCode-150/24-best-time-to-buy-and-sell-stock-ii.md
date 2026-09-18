# 24. Best Time to Buy and Sell Stock II

- **Problem:** https://leetcode.com/problems/best-time-to-buy-and-sell-stock-ii/
- **Difficulty:** Medium
- **Topic:** Array
- **Pattern:** Greedy
- **Data Structure & Algorithm:** Array, accumulate positive differences

## Explanation
You may complete unlimited transactions but must sell before buying again. Add every positive day-to-day price increase. Example: `[7,1,5,3,6,4]` → profit `7`.

## Clarifying Questions
- Can multiple transactions overlap? No.
- Are same-day buy and sell allowed?
- Is holding more than one stock allowed? No.

## Brute Force
Try every possible buy/sell combination using recursion. Worst-case complexity is exponential, making it impractical.

## Optimized Approach
Whenever `prices[i] > prices[i-1]`, add the difference. This captures every profitable rise. Time **O(n)**, extra space **O(1)**.

## Dry Run
`[1,5,3,6]`: add `4`, skip decline, add `3` → total `7`.

## Further Optimization
The greedy one-pass solution is asymptotically optimal.

## C#
See `24-best-time-to-buy-and-sell-stock-ii.cs`.
