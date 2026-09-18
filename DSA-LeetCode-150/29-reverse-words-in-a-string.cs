public static class ReverseWords
{
    public static string Convert(string s) => string.Join(" ", s.Split((char[])null, StringSplitOptions.RemoveEmptyEntries).Reverse());
}