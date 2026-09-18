public static class SetMatrixZeroes
{
    public static void Apply(int[][] a)
    {
        int rows=a.Length, cols=a[0].Length; bool firstRow=false,firstCol=false;
        for(int c=0;c<cols;c++) if(a[0][c]==0) firstRow=true;
        for(int r=0;r<rows;r++) if(a[r][0]==0) firstCol=true;
        for(int r=1;r<rows;r++) for(int c=1;c<cols;c++) if(a[r][c]==0){a[r][0]=0;a[0][c]=0;}
        for(int r=1;r<rows;r++) for(int c=1;c<cols;c++) if(a[r][0]==0||a[0][c]==0)a[r][c]=0;
        if(firstRow) for(int c=0;c<cols;c++) a[0][c]=0;
        if(firstCol) for(int r=0;r<rows;r++) a[r][0]=0;
    }
}