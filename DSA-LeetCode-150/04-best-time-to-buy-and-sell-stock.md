# 4. Best Time to Buy and Sell Stock

- **Problem link:** https://leetcode.com/problems/best-time-to-buy-and-sell-stock/
- **Difficulty:** Easy
- **Topic:** Array, Greedy, Dynamic Programming
- **Pattern:** Track minimum-so-far and best profit
- **Data structure / algorithm:** Array, one-pass greedy scan

## Problem Explanation

Given daily stock prices, choose one day to buy and a later day to sell. Return the maximum profit. If no profitable transaction exists, return `0`.

**Example:** `prices = [7,1,5,3,6,4]` → `5` by buying at `1` and selling at `6`.

## Clarifying Questions

1. Can prices be negative? Normally no; confirm input constraints.
2. Can I make only one transaction? Yes.
3. Must the buy day occur before the sell day? Yes.
4. What should be returned when no profit is possible? `0`.

## Brute Force Approach and Solution

Try every buy/sell pair and keep the largest positive difference.

```csharp
public int MaxProfitBruteForce(int[] prices)
{
    int best = 0;

    for (int buy = 0; buy < prices.Length; buy++)
    {
        for (int sell = buy + 1; sell < prices.Length; sell++)
            best = Math.Max(best, prices[sell] - prices[buy]);
    }

    return best;
}
```

- **Pattern / DSA:** Nested array traversal
- **Time:** `O(n²)`
- **Space:** `O(1)`
- **Drawback:** Recomputes many buy/sell combinations.

## Optimized Approach and Solution

At each day, the best transaction ending today uses the **lowest price seen before today**. Track that minimum and update the maximum profit in one pass.

```csharp
public int MaxProfit(int[] prices)
{
    int minPrice = int.MaxValue;
    int maxProfit = 0;

    foreach (int price in prices)
    {
        minPrice = Math.Min(minPrice, price);
        maxProfit = Math.Max(maxProfit, price - minPrice);
    }

    return maxProfit;
}
```

- **Time:** `O(n)`
- **Space:** `O(1)`

## Dry Run

For `[7,1,5,3,6,4]`:

| Price | Min so far | Profit today | Best |
|---:|---:|---:|---:|
| 7 | 7 | 0 | 0 |
| 1 | 1 | 0 | 0 |
| 5 | 1 | 4 | 4 |
| 3 | 1 | 2 | 4 |
| 6 | 1 | 5 | 5 |
| 4 | 1 | 3 | 5 |

## Is There a Further Optimized Approach?

For the standard problem, `O(n)` time and `O(1)` extra space is asymptotically optimal because every price may need to be inspected.
