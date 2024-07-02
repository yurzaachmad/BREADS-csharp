using System.Reflection;
//using IEM.Core.Services.JdbcProxies;
//using IEM.Core.Services.RestClientServices;
//using IEM.Core.Services.TokenServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Debugging;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Settings.Configuration;

namespace IEM.Core;
public static class DependencyInjection
{
    //public static IServiceCollection AddRestClientService(this IServiceCollection services)
    //{
    //    services.AddSingleton<IRestClientFactory, RestClientFactory>();
    //    services.AddScoped<IRestClientService, RestClientService>();
    //    return services;
    //}
    //public static IServiceCollection AddJdbcProxyService(this IServiceCollection services, string bindConfigurationKey)
    //{
    //    services.AddOptions<JdbcProxyOptions>().BindConfiguration(bindConfigurationKey);
    //    services.AddSingleton<IJdbcProxyService, JdbcProxyService>();
    //    services.AddSingleton<IQueryHelperService, QueryHelperService>();
    //    return services;
    //}

    //public static IServiceCollection AddTokenService(this IServiceCollection services)
    //{
    //    services.AddSingleton<ITokenService, TokenService>();
    //    return services;
    //}

    public static IHostBuilder UseLoggingService(this IHostBuilder hostBuilder, string sectionKey)
    {
        hostBuilder.UseSerilog((
           hostBuilderContext,
           loggerConfiguration) => loggerConfiguration.ConfigureLogging(hostBuilderContext.Configuration, sectionKey));
        SelfLog.Enable(Console.WriteLine);

        return hostBuilder;
    }

    private static LoggerConfiguration ConfigureLogging(this LoggerConfiguration loggerConfiguration, IConfiguration configuration, string sectionKey)
    {
        var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();

        return loggerConfiguration
            .ReadFrom.Configuration(configuration, new ConfigurationReaderOptions { SectionName = sectionKey })
            .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder().WithDefaultDestructurers())
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Version", assembly.GetName().Version!)
            .Enrich.WithProperty("AssemblyName", assembly.GetName().Name!)
            .Enrich.WithProperty("EnvironmentName", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
            .Enrich.WithEnvironmentName()
            .Enrich.WithMachineName()
            .Enrich.WithProcessId()
            .Enrich.WithThreadId()
            .Enrich.WithClientIp()
            .Enrich.WithCorrelationId();

    }
}
