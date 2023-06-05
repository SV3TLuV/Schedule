namespace Schedule.Core.Common.Extensions;

public static class StringExtensions
{
    public static string Capitalize(this string line)
    {
        if (string.IsNullOrWhiteSpace(line))
            return line;
        return char.ToUpper(line[0]) + line[1..].ToLower();
    }
    
    public static string GetFirstCapitalWord(this string line)
    {
        for (var i = 0; i < line.Length; i++)
            if (char.IsUpper(line[i]) && i != 0)
                return line[..i];
        return line;
    }
    
    public static string GetLastCapitalWord(this string line)
    {
        for (var i = 0; i < line.Length; i++)
            if (char.IsUpper(line[i]) && i != 0)
                return line[..i];
        return line;
    }
}