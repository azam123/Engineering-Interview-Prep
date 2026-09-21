# 66. Koko Eating Bananas

**Pattern:** Binary Search on Answer

## Approach
Binary-search the minimum eating speed. For a speed `k`, calculate the required hours using ceiling division.

## C# Solution
```csharp
public class Solution
{
    public int MinEatingSpeed(int[] piles, int h)
    {
        int low = 1, high = piles.Max();
        while (low < high)
        {
            int speed = low + (high - low) / 2;
            long hours = 0;
            foreach (int pile in piles) hours += (pile + speed - 1) / speed;
            if (hours <= h) high = speed;
            else low = speed + 1;
        }
        return low;
    }
}
```

**Complexity:** O(n log M) time, where M is the largest pile; O(1) space.
