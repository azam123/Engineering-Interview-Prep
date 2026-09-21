# 78. Koko Eating Bananas

**Pattern:** Binary Search on Answer

## C# Solution
```csharp
public class Solution
{
    public int MinEatingSpeed(int[] piles, int h)
    {
        int low = 1, high = piles.Max();
        while (low < high)
        {
            int mid = low + (high - low) / 2;
            long hours = 0;
            foreach (int pile in piles) hours += (pile + (long)mid - 1) / mid;
            if (hours <= h) high = mid;
            else low = mid + 1;
        }
        return low;
    }
}
```

## Complexity
- Time: O(n log M), where M is the largest pile.
- Space: O(1).
