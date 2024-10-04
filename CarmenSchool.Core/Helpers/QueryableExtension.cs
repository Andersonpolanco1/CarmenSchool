using CarmenSchool.Core.DTOs;
using System.Linq.Expressions;
using System.Reflection;

namespace CarmenSchool.Core.Helpers
{
  public static class QueryableExtension
  {
    /// <summary>
    /// Intenta ordenar el IQueryable dado por el nombre de campo especificado de manera dinámica.
    /// Soporta la ordenación por propiedades, incluyendo propiedades anidadas, usando notación de puntos.
    /// </summary>
    /// <typeparam name="T">El tipo de los elementos en el IQueryable fuente.</typeparam>
    /// <param name="source">El IQueryable que será ordenado.</param>
    /// <param name="fieldName">
    /// El nombre del campo o propiedad por el cual ordenar. Soporta propiedades anidadas separadas por puntos (por ejemplo, "Direccion.Ciudad").
    /// </param>
    /// <param name="sortedQuery">
    /// Cuando este método retorna, contiene el IQueryable ordenado si la ordenación fue exitosa, o la fuente original si no se encontró el campo.
    /// </param>
    /// <param name="sortOrder">
    /// Especifica el orden en el que se debe ordenar la consulta. Puede ser ascendente o descendente.
    /// </param>
    /// <returns>
    /// True si la ordenación fue aplicada con éxito; false si no se encontró el nombre del campo.
    /// </returns>
    public static bool TrySortQueryByField<T>(this IQueryable<T> source, string fieldName, out IQueryable<T> sortedQuery, SortOrder sortOrder)
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
          sortedQuery = source;
          return false;
        }
        currentEntityType = property.PropertyType;
        propertyAccessExpression = Expression.Property(propertyAccessExpression, propertyName);
      }

      var lambda = Expression.Lambda(propertyAccessExpression, parameter);

      string methodName = sortOrder == SortOrder.Ascending ? "OrderBy" : "OrderByDescending";
      var orderByMethod = typeof(Queryable).GetMethods()
          .First(m => m.Name == methodName && m.GetParameters().Length == 2)
          .MakeGenericMethod(typeof(T), propertyAccessExpression.Type);

      sortedQuery = (IQueryable<T>)orderByMethod.Invoke(null, [source, lambda]);

      return true;
    }
  }
}
