using System.Collections.Generic;
public static class SpiralMatrix
{
    public static IList<int> Traverse(int[][] m)
    {
        var result = new List<int>(); if (m.Length == 0) return result;
        int top=0,bottom=m.Length-1,left=0,right=m[0].Length-1;
        while(top<=bottom && left<=right)
        {
            for(int c=left;c<=right;c++) result.Add(m[top][c]); top++;
            for(int r=top;r<=bottom;r++) result.Add(m[r][right]); right--;
            if(top<=bottom) { for(int c=right;c>=left;c--) result.Add(m[bottom][c]); bottom--; }
            if(left<=right) { for(int r=bottom;r>=top;r--) result.Add(m[r][left]); left++; }
        }
        return result;
    }
}