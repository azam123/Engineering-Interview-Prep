# 110. Pacific Atlantic Water Flow

## Problem
Return cells from which water can flow to both the Pacific and Atlantic oceans.

## Key Insight
Reverse the traversal. Start from each ocean and move to neighboring cells whose height is greater than or equal to the current cell.

## C# Solution
~~~csharp
public class Solution
{
    private static readonly int[][] Directions =
    {
        new[] { 1, 0 }, new[] { -1, 0 },
        new[] { 0, 1 }, new[] { 0, -1 }
    };

    public IList<IList<int>> PacificAtlantic(int[][] heights)
    {
        int rows = heights.Length, cols = heights[0].Length;
        var pacific = new bool[rows, cols];
        var atlantic = new bool[rows, cols];

        var pq = new Queue<(int r, int c)>();
        var aq = new Queue<(int r, int c)>();

        for (int r = 0; r < rows; r++)
        {
            Enqueue(r, 0, pacific, pq);
            Enqueue(r, cols - 1, atlantic, aq);
        }

        for (int c = 0; c < cols; c++)
        {
            Enqueue(0, c, pacific, pq);
            Enqueue(rows - 1, c, atlantic, aq);
        }

        Flood(heights, pacific, pq);
        Flood(heights, atlantic, aq);

        var result = new List<IList<int>>();
        for (int r = 0; r < rows; r++)
            for (int c = 0; c < cols; c++)
                if (pacific[r, c] && atlantic[r, c])
                    result.Add(new List<int> { r, c });

        return result;
    }

    private static void Enqueue(int r, int c, bool[,] seen, Queue<(int, int)> q)
    {
        if (!seen[r, c])
        {
            seen[r, c] = true;
            q.Enqueue((r, c));
        }
    }

    private static void Flood(int[][] h, bool[,] seen, Queue<(int r, int c)> q)
    {
        while (q.Count > 0)
        {
            var (r, c) = q.Dequeue();

            foreach (var d in Directions)
            {
                int nr = r + d[0], nc = c + d[1];

                if (nr < 0 || nr >= h.Length || nc < 0 || nc >= h[0].Length)
                    continue;

                if (!seen[nr, nc] && h[nr][nc] >= h[r][c])
                    Enqueue(nr, nc, seen, q);
            }
        }
    }
}
~~~

## Complexity
Time O(rows × cols), space O(rows × cols).

## Interview Follow-ups
- Why traverse from the oceans?
- Can DFS replace BFS?
- How would you parallelize the two independent traversals?
