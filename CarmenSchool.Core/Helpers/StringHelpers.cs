using System.Globalization;

namespace CarmenSchool.Core.Helpers
{
  public static class StringHelpers
  {
    public static string CapitalizeWords(this string input)
    {
      if (string.IsNullOrWhiteSpace(input))
        return input;

      TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
      return textInfo.ToTitleCase(input.ToLower());
    }
  }
}
