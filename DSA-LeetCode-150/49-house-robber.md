# House Robber

**Problem:** Maximize the amount of money robbed from non-adjacent houses.

## Approach: Dynamic Programming
For each house, choose between skipping it or robbing it and adding the best result from two positions earlier.

```csharp
public class Solution
{
    public int Rob(int[] nums)
    {
        int previousTwo = 0;
        int previousOne = 0;

        foreach (int money in nums)
        {
            int current = Math.Max(previousOne, previousTwo + money);
            previousTwo = previousOne;
            previousOne = current;
        }

        return previousOne;
    }
}
```

**Time:** O(n)  
**Space:** O(1)

**Follow-up:** Adapt the solution for houses arranged in a circle.
