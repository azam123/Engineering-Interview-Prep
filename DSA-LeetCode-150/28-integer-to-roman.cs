public static class IntegerToRoman
{
    public static string Convert(int num)
    {
        int[] values = {1000,900,500,400,100,90,50,40,10,9,5,4,1};
        string[] symbols = {"M","CM","D","CD","C","XC","L","XL","X","IX","V","IV","I"};
        var result = new System.Text.StringBuilder();
        for (int i = 0; i < values.Length; i++)
            while (num >= values[i]) { result.Append(symbols[i]); num -= values[i]; }
        return result.ToString();
    }
}