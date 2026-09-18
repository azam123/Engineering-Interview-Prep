public static class MaximumSubarray
{
    public static int MaxSubArrayBruteForce(int[] nums)
    {
        int best = int.MinValue;
        for (int i = 0; i < nums.Length; i++)
        {
            int sum = 0;
            for (int j = i; j < nums.Length; j++)
            {
                sum += nums[j];
                best = System.Math.Max(best, sum);
            }
        }
        return best;
    }

    public static int MaxSubArray(int[] nums)
    {
        int current = nums[0], best = nums[0];
        for (int i = 1; i < nums.Length; i++)
        {
            current = System.Math.Max(nums[i], current + nums[i]);
            best = System.Math.Max(best, current);
        }
        return best;
    }
}