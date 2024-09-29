
namespace CarmenSchool.Web.Utils
{
  public class PathUtils(IHostEnvironment hostEnvironment)
  {
    public string GetFullPath(string relativePath)
    {
      if (relativePath == null) return string.Empty;

      var basePath = GetRootPath();
      return Path.Combine(basePath, relativePath);
    }

    public string GetRootPath()
    {
      var webPath = hostEnvironment.ContentRootPath;
      return new DirectoryInfo(webPath).Parent.FullName;
    }
  }
}
