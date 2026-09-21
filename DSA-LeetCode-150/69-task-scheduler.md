# 69. Task Scheduler

**Pattern:** Greedy + Frequency Counting

## Approach
The most frequent task determines the minimum frame structure. Fill idle slots with other tasks and compare against the total task count.

## C# Solution
```csharp
public class Solution
{
    public int LeastInterval(char[] tasks, int n)
    {
        int[] count = new int[26];
        foreach (char task in tasks) count[task - 'A']++;
        int max = count.Max();
        int maxCount = count.Count(x => x == max);
        int frame = (max - 1) * (n + 1) + maxCount;
        return Math.Max(tasks.Length, frame);
    }
}
```

**Complexity:** O(T + 26) time and O(1) auxiliary space.
