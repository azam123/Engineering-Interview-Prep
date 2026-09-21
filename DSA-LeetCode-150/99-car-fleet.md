# 99. Car Fleet

**Pattern:** Sorting, Monotonic Observation

## C# Solution
```csharp
public class Solution
{
    public int CarFleet(int target, int[] position, int[] speed)
    {
        var cars = new (int Position, double Time)[position.Length];
        for (int i = 0; i < position.Length; i++)
            cars[i] = (position[i], (double)(target - position[i]) / speed[i]);

        Array.Sort(cars, (a, b) => b.Position.CompareTo(a.Position));
        int fleets = 0;
        double slowestTime = 0;

        foreach (var car in cars)
        {
            if (car.Time > slowestTime)
            {
                fleets++;
                slowestTime = car.Time;
            }
        }
        return fleets;
    }
}
```

**Complexity:** O(n log n) time and O(n) space.
