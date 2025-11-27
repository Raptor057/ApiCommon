using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Common.Logging;

public static class ServiceCollectionEx
{
    public static IServiceCollection AddLoggingServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Bindea sección a CustomLoggingOptions
        var options = new CustomLoggingOptions();
        configuration.GetSection("CustomLogging").Bind(options);

        var serilogLogger = new LoggerConfiguration()
            .Enrich.WithProperty("Project", options.Project)
            .WriteTo.Seq(
                serverUrl: options.SeqUri,
                restrictedToMinimumLevel: options.LogEventLevel)
            .CreateLogger();

        services.AddLogging(logging =>
        {
            logging.AddSerilog(serilogLogger, dispose: true);
        });

        return services;
    }
}
