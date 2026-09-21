# Rotting Oranges

**Problem:** Return the minimum minutes required for all reachable fresh oranges to become rotten.

## Approach: Multi-source BFS
Add all initially rotten oranges to a queue and process the grid level by level. Each level represents one minute.

```csharp
public class Solution
{
    public int OrangesRotting(int[][] grid)
    {
        int rows = grid.Length, cols = grid[0].Length;
        var queue = new Queue<(int r, int c)>();
        int fresh = 0, minutes = 0;
        int[][] directions = { new[] { 1, 0 }, new[] { -1, 0 }, new[] { 0, 1 }, new[] { 0, -1 } };

        for (int r = 0; r < rows; r++)
            for (int c = 0; c < cols; c++)
            {
                if (grid[r][c] == 2) queue.Enqueue((r, c));
                else if (grid[r][c] == 1) fresh++;
            }

        while (queue.Count > 0 && fresh > 0)
        {
            int levelSize = queue.Count;
            for (int i = 0; i < levelSize; i++)
            {
                var (r, c) = queue.Dequeue();
                foreach (var direction in directions)
                {
                    int nr = r + direction[0], nc = c + direction[1];
                    if (nr < 0 || nr >= rows || nc < 0 || nc >= cols || grid[nr][nc] != 1) continue;
                    grid[nr][nc] = 2;
                    fresh--;
                    queue.Enqueue((nr, nc));
                }
            }
            minutes++;
        }

        return fresh == 0 ? minutes : -1;
    }
}
```

**Time:** O(rows × cols)  
**Space:** O(rows × cols)

**Follow-up:** Explain how to handle diagonal spread or weighted travel time.
