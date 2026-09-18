using System.Collections.Generic;
public static class ValidSudoku
{
    public static bool IsValid(char[][] board)
    {
        var rows = new HashSet<char>[9]; var cols = new HashSet<char>[9]; var boxes = new HashSet<char>[9];
        for (int i = 0; i < 9; i++) { rows[i] = new(); cols[i] = new(); boxes[i] = new(); }
        for (int r = 0; r < 9; r++) for (int c = 0; c < 9; c++)
        {
            char ch = board[r][c]; if (ch == '.') continue;
            int b = (r / 3) * 3 + c / 3;
            if (!rows[r].Add(ch) || !cols[c].Add(ch) || !boxes[b].Add(ch)) return false;
        }
        return true;
    }
}