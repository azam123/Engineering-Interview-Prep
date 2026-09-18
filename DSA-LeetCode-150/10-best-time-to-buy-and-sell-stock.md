# 10. Best Time to Buy and Sell Stock

- **Problem link:** https://leetcode.com/problems/best-time-to-buy-and-sell-stock/
- **Difficulty:** Easy
- **Topic:** Array, Greedy
- **Pattern:** Track minimum and maximize difference
- **Data structure / algorithm:** One-pass greedy scan

## Problem Explanation

Given daily stock prices, choose one day to buy and a later day to sell to maximize profit. If no profit is possible, return `0`.

Examples:

```text
prices = [7,1,5,3,6,4] => 5
prices = [7,6,4,3,1] => 0
```

## Clarifying Questions

1. Must buying happen before selling? Yes.
2. Is only one transaction allowed?
3. Can the prices array be empty?
4. Should negative profit be returned or should the result be zero?

## Brute Force Approach and Solution

Evaluate every valid buy-and-sell pair.

```csharp
public int MaxProfitBruteForce(int[] prices)
{
    int maxProfit = 0;

    for (int buy = 0; buy < prices.Length; buy++)
    {
        for (int sell = buy + 1; sell < prices.Length; sell++)
        {
            maxProfit = Math.Max(maxProfit, prices[sell] - prices[buy]);
        }
    }

    return maxProfit;
}
```

- **Pattern / DSA:** Nested loops, exhaustive search
- **Time:** `O(n²)`
- **Space:** `O(1)`
- **Drawback:** Recomputes many possible transactions and is inefficient for large input sizes.

## Optimized Approach and Solution

Maintain the lowest price seen so far. At each day, calculate the profit from selling today and update the maximum profit.

```csharp
public int MaxProfit(int[] prices)
{
    int minimumPrice = int.MaxValue;
    int maxProfit = 0;

    foreach (int price in prices)
    {
        minimumPrice = Math.Min(minimumPrice, price);
        maxProfit = Math.Max(maxProfit, price - minimumPrice);
    }

    return maxProfit;
}
```

- **Time:** `O(n)`
- **Space:** `O(1)`

## Dry Run

For `[7,1,5,3,6,4]`:

| Price | Minimum so far | Current profit | Max profit |
|---:|---:|---:|---:|
| 7 | 7 | 0 | 0 |
| 1 | 1 | 0 | 0 |
| 5 | 1 | 4 | 4 |
| 3 | 1 | 2 | 4 |
| 6 | 1 | 5 | 5 |
| 4 | 1 | 3 | 5 |

## Further Optimization?

The one-pass greedy solution is asymptotically optimal: it requires `O(n)` time because every price must be inspected, and `O(1)` space.
