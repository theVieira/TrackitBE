using System.Globalization;
using System.Text.RegularExpressions;

namespace Trackit.Utils;

public static class SpellCheck
{
  public static string CapitalizeName(string name)
  {
    if (string.IsNullOrWhiteSpace(name))
    {
        return string.Empty;
    }

    string[] lowercaseWords = ["de", "da", "do", "dos", "das", "e"];
    string[] words = name.ToLower().Split(' ');

    for (int i = 0; i < words.Length; i++)
    {
        if (i == 0 || !lowercaseWords.Contains(words[i]))
        {
            words[i] = char.ToUpper(words[i][0]) + words[i][1..];
        }
    }

    return string.Join(' ', words);
  }

  public static string CapitalizeText(string text)
  {
    if(string.IsNullOrWhiteSpace(text))
      return string.Empty;

    text = text.Trim();
    return char.ToUpper(text[0]) + text[1..].ToLower();
  }

  public static string CleanSpecialChar(string input)
  {
    if (string.IsNullOrWhiteSpace(input))
      return string.Empty;

    return Regex.Replace(input, @"\D", "");
  }
}