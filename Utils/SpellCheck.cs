using System.Text.RegularExpressions;
using Humanizer;

namespace Trackit.Utils;

public abstract class SpellCheck
{
    public static string CapitalizeName(string name)
    {
        return name
            .Trim()
            .Transform(To.TitleCase);
    }

    public static string CapitalizeText(string text)
    {
        return text
                   .Trim()
                   [0].ToString().ToUpper()
               + text[1..];
    }

    public static string CleanSpecialChar(string input)
    {
        return Regex.Replace(input, "[^0-9]", "");
    }
}