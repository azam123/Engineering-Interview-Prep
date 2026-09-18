public static class RotateImage
{
    public static void Rotate(int[][] a)
    {
        int n=a.Length;
        for(int r=0;r<n;r++) for(int c=r;c<n;c++) (a[r][c],a[c][r])=(a[c][r],a[r][c]);
        for(int r=0;r<n;r++) for(int l=0,h=n-1;l<h;l++,h--) (a[r][l],a[r][h])=(a[r][h],a[r][l]);
    }
}