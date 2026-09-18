public static class HIndex
{
    public static int Calculate(int[] citations)
    {
        int n = citations.Length;
        int[] buckets = new int[n + 1];

        foreach (int citation in citations)
            buckets[Math.Min(citation, n)]++;

        int papers = 0;
        for (int h = n; h >= 0; h--)
        {
            papers += buckets[h];
            if (papers >= h)
                return h;
        }

        return 0;
    }
}
