using System.Reflection;

namespace CarmenSchool.Core.Utils
{
  public static class ValidationUtils
  {
    public static bool FieldHasChanged(object? requestValue, object? currentValue)
    {
      if (requestValue == null)
        return false;

      if (currentValue == null)
        return true;

      if (requestValue is string requestString && currentValue is string currentString)
        return !string.Equals(requestString.ToUpperInvariant(), currentString.ToUpperInvariant(), StringComparison.Ordinal);

      return !Equals(requestValue, currentValue);
    }

    public static bool TryGetProperty<T>(string propertyName, out string foundPropertyName)
    {
      var property = typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

      if (property != null)
      {
        foundPropertyName = property.Name;
        return true;
      }

      foundPropertyName = null;
      return false;
    }

  }
}
