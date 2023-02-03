using Microsoft.Extensions.Configuration;

namespace Demo.Classes;
internal sealed class ApplicationSettings
{
    private static readonly Lazy<ApplicationSettings> Lazy = new(() => new ApplicationSettings());
    public static ApplicationSettings Instance => Lazy.Value;

    public bool LogSqlCommands { get; set; }
    private ApplicationSettings()
    {
        IConfigurationRoot configuration = 
            Configurations.GetConfigurationRoot();
        LogSqlCommands = Convert.ToBoolean(configuration.GetSection("Debug")["LogSqlCommand"]) ;
    }
}