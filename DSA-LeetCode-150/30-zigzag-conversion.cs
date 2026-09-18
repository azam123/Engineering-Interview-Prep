public static class ZigzagConversion
{
    public static string Convert(string s, int numRows)
    {
        if (numRows == 1 || numRows >= s.Length) return s;
        var rows = Enumerable.Range(0, numRows).Select(_ => new System.Text.StringBuilder()).ToArray();
        int row = 0, step = 1;
        foreach (char c in s)
        {
            rows[row].Append(c);
            if (row == 0) step = 1;
            else if (row == numRows - 1) step = -1;
            row += step;
        }
        return string.Concat(rows.Select(x => x.ToString()));
    }
}