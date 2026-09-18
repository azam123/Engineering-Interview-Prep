using System;

public static class LengthOfLastWord
{
    public static int Length(string s)
    {
        int index = s.Length - 1;
        while (index >= 0 && s[index] == ' ')
            index--;

        int length = 0;
        while (index >= 0 && s[index] != ' ')
        {
            length++;
            index--;
        }

        return length;
    }
}