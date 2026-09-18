public static class ProductOfArrayExceptSelf
{
    public static int[] Calculate(int[] nums)
    {
        int n = nums.Length;
        int[] answer = new int[n];
        int prefix = 1;

        for (int i = 0; i < n; i++)
        {
            answer[i] = prefix;
            prefix *= nums[i];
        }

        int suffix = 1;
        for (int i = n - 1; i >= 0; i--)
        {
            answer[i] *= suffix;
            suffix *= nums[i];
        }

        return answer;
    }
}
