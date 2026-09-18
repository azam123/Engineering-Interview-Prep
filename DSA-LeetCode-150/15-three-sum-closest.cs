using System;

public static class ThreeSumClosest
{
    public static int Find(int[] nums, int target)
    {
        Array.Sort(nums);
        int closest = nums[0] + nums[1] + nums[2];

        for (int i = 0; i < nums.Length - 2; i++)
        {
            int left = i + 1;
            int right = nums.Length - 1;

            while (left < right)
            {
                int sum = nums[i] + nums[left] + nums[right];
                if (Math.Abs(target - sum) < Math.Abs(target - closest))
                    closest = sum;

                if (sum == target)
                    return sum;
                if (sum < target)
                    left++;
                else
                    right--;
            }
        }

        return closest;
    }
}