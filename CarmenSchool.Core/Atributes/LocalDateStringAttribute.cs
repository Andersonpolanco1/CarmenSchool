using CarmenSchool.Core.Utils;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CarmenSchool.Core.Atributes
{
  public class LocalDateStringAttribute : ValidationAttribute
  {
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
      PropertyInfo? property = validationContext.ObjectType.GetProperty(validationContext.MemberName!);

      if (property == null)
        return new ValidationResult("La propiedad no fue encontrada.");

      bool isNullable = IsNullable(property);

      if (isNullable)
      {
        if (value == null)
          return ValidationResult.Success;

        if (!IsValidDate(value?.ToString() ?? string.Empty))
          return new ValidationResult(DateTimeUtils.GetDateFormatErrorMessage(validationContext.DisplayName));
      }
      else
      {
        if (value == null)
          return new ValidationResult($"El campo {validationContext.DisplayName} no acepta nulos.");

        string date = value.ToString()!;
        if (!IsValidDate(date))
          return new ValidationResult(DateTimeUtils.GetDateFormatErrorMessage(validationContext.DisplayName));
      }

      return ValidationResult.Success;
    }

    private bool IsValidDate(string date)
    {
      string pattern = @"^(0[1-9]|[12][0-9]|3[01])[-/](0[1-9]|1[0-2])[-/](\d{4})$";
      return Regex.IsMatch(date, pattern);
    }

    private bool IsNullable(PropertyInfo property)
    {
      if (Nullable.GetUnderlyingType(property.PropertyType) != null)
        return true;

      return !property.PropertyType.IsValueType;
    }
  }
}
