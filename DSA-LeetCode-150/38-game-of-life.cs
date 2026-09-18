public static class GameOfLife
{
 public static void Next(int[][] b){int m=b.Length,n=b[0].Length;int[] dr={-1,-1,-1,0,0,1,1,1},dc={-1,0,1,-1,1,-1,0,1};for(int r=0;r<m;r++)for(int c=0;c<n;c++){int live=0;for(int k=0;k<8;k++){int x=r+dr[k],y=c+dc[k];if(x>=0&&x<m&&y>=0&&y<n&&(b[x][y]==1||b[x][y]==3))live++;}if(b[r][c]==1&&(live<2||live>3))b[r][c]=3;if(b[r][c]==0&&live==3)b[r][c]=2;}for(int r=0;r<m;r++)for(int c=0;c<n;c++)b[r][c]%=2;}
}