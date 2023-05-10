namespace TheStockedKitchen.Api.Config;

public class CorsConfiguration
{
    public string BaseUrl { get; set; }
    public string PolicyName { get; set; }
}

public class ConnectionStringsConfiguration
{
    public string AppDb { get; set; }
}

public class ApiKeyConfiguration
{
    public string USDAApiKey { get; set; }
}

public class AzureAdConfiguration
{
    public string Instance { get; set; }

    public string Domain { get; set; }

    public string TenantId { get; set; }

    public string ClientId { get; set; }

    public string ClientSecret { get; set; }

    public string CallbackPath { get; set; }
}

public class AzureApplicationInsightsConfiguration
{
    public string InstrumentationKey { get; set; }
}

public class EntityFrameworkConfiguration
{
    public string MigrationsHistoryTable { get; set; }

    public string MigrationsHistorySchema { get; set; }
}

public class TheStockedKitchenConfiguration
{
    public AzureAdConfiguration AzureAd { get; set; }

    public ApiKeyConfiguration ApiKey { get; set; }

    public AzureApplicationInsightsConfiguration ApplicationInsights { get; set; }

    public ConnectionStringsConfiguration ConnectionStrings { get; set; }

    public EntityFrameworkConfiguration EntityFramework { get; set; }

    public CorsConfiguration WasmCors { get; set; }

    public TheStockedKitchenConfiguration()
    {
        AzureAd = new AzureAdConfiguration();

        ApiKey = new ApiKeyConfiguration();

        WasmCors = new CorsConfiguration();

        ConnectionStrings = new ConnectionStringsConfiguration();

        EntityFramework = new EntityFrameworkConfiguration();

        ApplicationInsights = new AzureApplicationInsightsConfiguration();
    }
}

public static class ConfigurationExtensions
{
    public static TheStockedKitchenConfiguration GetTheStockedKitchenConfiguration(this IConfiguration configuration)
    {
        var TheStockedKitchenConfiguration = new TheStockedKitchenConfiguration();

        configuration.GetSection("AzureAd").Bind(TheStockedKitchenConfiguration.AzureAd);

        configuration.GetSection("ApiKeys").Bind(TheStockedKitchenConfiguration.ApiKey);

        configuration.GetSection("WasmCors").Bind(TheStockedKitchenConfiguration.WasmCors);

        configuration.GetSection("ConnectionStrings").Bind(TheStockedKitchenConfiguration.ConnectionStrings);

        configuration.GetSection("EntityFramework").Bind(TheStockedKitchenConfiguration.EntityFramework);

        return TheStockedKitchenConfiguration;
    }

    public static IServiceCollection AddTheStockedKitchenConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        var TheStockedKitchenConfiguration = configuration.GetTheStockedKitchenConfiguration();

        services.AddSingleton(TheStockedKitchenConfiguration);

        return services;
    }
}
