using System;

public static class JumpGame
{
    public static bool CanJumpBruteForce(int[] nums) => Explore(nums, 0);

    private static bool Explore(int[] nums, int index)
    {
        if (index >= nums.Length - 1) return true;

        int furthest = Math.Min(index + nums[index], nums.Length - 1);
        for (int next = index + 1; next <= furthest; next++)
            if (Explore(nums, next)) return true;

        return false;
    }

    public static bool CanJump(int[] nums)
    {
        int farthest = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if (i > farthest) return false;
            farthest = Math.Max(farthest, i + nums[i]);
            if (farthest >= nums.Length - 1) return true;
        }
        return true;
    }
}
