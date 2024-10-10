namespace CarmenSchool.Core.Configurations
{/// <summary>
 /// Consifuraciones del sistema que seran patrametrizadas a través del archivo de configuración
 /// </summary>
    public class ConfigurationsOptions
    {
        public int MaxPageSize { get; set; } = 500;
        public MockJsonFilePathsOptions MockJsonFilePaths { get; set; }
    }
}
