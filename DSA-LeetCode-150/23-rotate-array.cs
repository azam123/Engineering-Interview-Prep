public static class RotateArray
{
    public static void Rotate(int[] nums, int k)
    {
        if (nums.Length <= 1) return;
        k %= nums.Length;
        Reverse(nums, 0, nums.Length - 1);
        Reverse(nums, 0, k - 1);
        Reverse(nums, k, nums.Length - 1);
    }

    private static void Reverse(int[] nums, int left, int right)
    {
        while (left < right)
        {
            (nums[left], nums[right]) = (nums[right], nums[left]);
            left++; right--;
        }
    }
}