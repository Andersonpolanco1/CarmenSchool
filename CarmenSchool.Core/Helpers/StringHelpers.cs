using System.Globalization;

namespace CarmenSchool.Core.Helpers
{
  public static class StringHelpers
  {
    /// <summary>
    /// Coloca la primera letra de cada palabra en mayúscula y el resto en minúusculas
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string CapitalizeWords(this string input)
    {
      if (string.IsNullOrWhiteSpace(input))
        return input;

      TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
      return textInfo.ToTitleCase(input.ToLower());
    }
  }
}
