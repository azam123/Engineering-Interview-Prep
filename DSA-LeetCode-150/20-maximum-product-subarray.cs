public static class MaximumProductSubarray
{
    public static int MaxProductBruteForce(int[] nums)
    {
        int best = nums[0];
        for (int i = 0; i < nums.Length; i++)
        {
            int product = 1;
            for (int j = i; j < nums.Length; j++)
            {
                product *= nums[j];
                best = System.Math.Max(best, product);
            }
        }
        return best;
    }

    public static int MaxProduct(int[] nums)
    {
        int max = nums[0], min = nums[0], answer = nums[0];
        for (int i = 1; i < nums.Length; i++)
        {
            int value = nums[i];
            if (value < 0) (max, min) = (min, max);
            max = System.Math.Max(value, max * value);
            min = System.Math.Min(value, min * value);
            answer = System.Math.Max(answer, max);
        }
        return answer;
    }
}