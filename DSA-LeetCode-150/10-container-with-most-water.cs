using System;

public static class ContainerWithMostWater
{
    public static int MaxAreaBruteForce(int[] height)
    {
        int best = 0;
        for (int i = 0; i < height.Length; i++)
        {
            for (int j = i + 1; j < height.Length; j++)
            {
                int area = Math.Min(height[i], height[j]) * (j - i);
                best = Math.Max(best, area);
            }
        }
        return best;
    }

    public static int MaxArea(int[] height)
    {
        int left = 0, right = height.Length - 1, best = 0;

        while (left < right)
        {
            int area = Math.Min(height[left], height[right]) * (right - left);
            best = Math.Max(best, area);

            if (height[left] <= height[right]) left++;
            else right--;
        }

        return best;
    }
}
