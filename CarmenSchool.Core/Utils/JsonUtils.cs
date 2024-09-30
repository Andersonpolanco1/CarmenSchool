using System.Text.Json;

namespace CarmenSchool.Core.Utils
{
  public class JsonUtils
  {
    public static async Task<List<T>> GetObjectFromJsonAsync<T>(string path)
    {
      var students = new List<T>();

      try
      {
        var json = await File.ReadAllTextAsync(path);
        students = JsonSerializer.Deserialize<List<T>>(json);
      }
      catch (Exception) { }

      return students ?? [];
    }
  }
}
