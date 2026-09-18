public static class BestTimeToBuyAndSellStockII
{
    public static int MaxProfit(int[] prices)
    {
        int profit = 0;
        for (int i = 1; i < prices.Length; i++)
        {
            if (prices[i] > prices[i - 1])
                profit += prices[i] - prices[i - 1];
        }
        return profit;
    }
}
