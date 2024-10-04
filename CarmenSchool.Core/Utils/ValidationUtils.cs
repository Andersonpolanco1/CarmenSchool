using CarmenSchool.Core.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
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

    public static bool TrySortQueryByField<T>(string fieldName, out IQueryable<T> sortedQuery, IQueryable<T> entityQuery, SortOrder sortOrder)
    {
      var propertyNames = fieldName.Split('.');
      var parameter = Expression.Parameter(typeof(T), "e");
      Expression propertyAccessExpression = parameter;
      Type currentEntityType = typeof(T);

      foreach (var propertyName in propertyNames)
      {
        var property = currentEntityType.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        if (property == null)
        {
          // Propiedad para ordenar no encontrada, retornar query sin ordenacion
          sortedQuery = entityQuery;
          return false; 
        }
        currentEntityType = property.PropertyType;
        propertyAccessExpression = Expression.Property(propertyAccessExpression, propertyName);
      }

      // Crear la expresión de lambda e => e.Propiedad
      var lambda = Expression.Lambda(propertyAccessExpression, parameter);

      // Generar el método de ordenación (OrderBy o OrderByDescending)
      string methodName = sortOrder == SortOrder.Ascending ? "OrderBy" : "OrderByDescending";
      var orderByMethod = typeof(Queryable).GetMethods()
          .First(m => m.Name == methodName && m.GetParameters().Length == 2)
          .MakeGenericMethod(typeof(T), propertyAccessExpression.Type);

      // Ejecutar la ordenación
      sortedQuery = (IQueryable<T>)orderByMethod.Invoke(null, [entityQuery, lambda]);

      return true;
    }

  }
}
