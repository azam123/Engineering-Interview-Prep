# 68. Word Search

**Pattern:** Backtracking + DFS

## Approach
Start DFS from every cell matching the first character. Mark cells as visited during the current path and restore them during backtracking.

## C# Solution
```csharp
public class Solution
{
    public bool Exist(char[][] board, string word)
    {
        int rows = board.Length, cols = board[0].Length;
        bool Dfs(int r, int c, int index)
        {
            if (index == word.Length) return true;
            if (r < 0 || c < 0 || r >= rows || c >= cols || board[r][c] != word[index]) return false;
            char original = board[r][c];
            board[r][c] = '#';
            bool found = Dfs(r + 1, c, index + 1) || Dfs(r - 1, c, index + 1) ||
                         Dfs(r, c + 1, index + 1) || Dfs(r, c - 1, index + 1);
            board[r][c] = original;
            return found;
        }

        for (int r = 0; r < rows; r++)
            for (int c = 0; c < cols; c++)
                if (Dfs(r, c, 0)) return true;
        return false;
    }
}
```

**Complexity:** O(R × C × 4^L) time in the worst case and O(L) recursion space.
