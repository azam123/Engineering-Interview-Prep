public static class RemoveElement
{
    public static int Remove(int[] nums, int val)
    {
        int write = 0;
        foreach (int number in nums)
        {
            if (number != val)
                nums[write++] = number;
        }

        return write;
    }
}