public static class RemoveDuplicatesFromSortedArray
{
    public static int RemoveDuplicates(int[] nums)
    {
        if (nums.Length == 0) return 0;

        int write = 1;
        for (int read = 1; read < nums.Length; read++)
        {
            if (nums[read] != nums[write - 1])
                nums[write++] = nums[read];
        }

        return write;
    }
}