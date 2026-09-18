public static class FirstOccurrence
{
    public static int Find(string haystack, string needle) => haystack.IndexOf(needle, StringComparison.Ordinal);
}