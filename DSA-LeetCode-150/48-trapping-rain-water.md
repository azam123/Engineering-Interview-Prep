# Trapping Rain Water

**Problem:** Given an elevation map, calculate how much water can be trapped after raining.

## Approach: Two Pointers
Maintain `leftMax` and `rightMax`. Process the side with the smaller height because its trapped water is bounded by that side.

```csharp
public class Solution
{
    public int Trap(int[] height)
    {
        int left = 0, right = height.Length - 1;
        int leftMax = 0, rightMax = 0, water = 0;

        while (left < right)
        {
            if (height[left] <= height[right])
            {
                leftMax = Math.Max(leftMax, height[left]);
                water += leftMax - height[left];
                left++;
            }
            else
            {
                rightMax = Math.Max(rightMax, height[right]);
                water += rightMax - height[right];
                right--;
            }
        }

        return water;
    }
}
```

**Time:** O(n)  
**Space:** O(1)

**Follow-up:** Solve using prefix/suffix maximum arrays.
