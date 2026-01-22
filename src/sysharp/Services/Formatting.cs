using System.Text.RegularExpressions;

public static class Formatting
{
    public static string? ExtractValue(string text, string pattern)
    {
        var match = Regex.Match(text, pattern);
        return match.Success ? match.Groups[1].Value.Trim() : null;
    }
}
