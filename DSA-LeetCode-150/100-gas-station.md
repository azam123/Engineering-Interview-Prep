# 100. Gas Station

**Pattern:** Greedy

## C# Solution
```csharp
public class Solution
{
    public int CanCompleteCircuit(int[] gas, int[] cost)
    {
        int total = 0, current = 0, start = 0;

        for (int i = 0; i < gas.Length; i++)
        {
            int gain = gas[i] - cost[i];
            total += gain;
            current += gain;

            if (current < 0)
            {
                start = i + 1;
                current = 0;
            }
        }

        return total >= 0 ? start : -1;
    }
}
```

**Complexity:** O(n) time and O(1) space.

**Interview Note:** If total gas is less than total cost, completing the circuit is impossible.
