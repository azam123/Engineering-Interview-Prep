public static class TrappingRainWater
{
    public static int Calculate(int[] height)
    {
        int left = 0, right = height.Length - 1, leftMax = 0, rightMax = 0, water = 0;
        while (left < right)
        {
            if (height[left] <= height[right])
            {
                leftMax = Math.Max(leftMax, height[left]);
                water += leftMax - height[left++];
            }
            else
            {
                rightMax = Math.Max(rightMax, height[right]);
                water += rightMax - height[right--];
            }
        }
        return water;
    }
}