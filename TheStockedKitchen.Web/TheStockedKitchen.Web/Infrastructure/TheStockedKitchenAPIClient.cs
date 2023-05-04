using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using TheStockedKitchen.Client;

namespace TheStockedKitchen.Web.Infrastructure;

public class TheStockedKitchenApiConfiguration
{
    public string BaseUrl { get; set; }

    public string Scope { get; set; }

    public List<string> Scopes
    {
        get
        {
            return new List<string>() { Scope };
        }
    }
}

public sealed class TheStockedKitchenApiAuthorizationMessageHandler : AuthorizationMessageHandler
{
    public TheStockedKitchenApiAuthorizationMessageHandler(IAccessTokenProvider provider, NavigationManager navigation,
        TheStockedKitchenApiConfiguration apiConfiguration) : base(provider, navigation)
    {
        ConfigureHandler(new[] { apiConfiguration.BaseUrl }, apiConfiguration.Scopes);
    }
}

public static class ServiceCollectionExtensions
{
    public static TheStockedKitchenApiConfiguration GetTheStockedKitchenApiConfiguration(
        this IConfiguration configuration,
        string apiConfiguration = "TheStockedKitchenAPI")
    {
        var TheStockedKitchenApiConfiguration = new TheStockedKitchenApiConfiguration();

        var section = configuration.GetSection(apiConfiguration);

        section.Bind(TheStockedKitchenApiConfiguration);

        return TheStockedKitchenApiConfiguration;
    }

    public static IServiceCollection AddTheStockedKitchenApiConfiguration(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var apiConfiguration = configuration.GetTheStockedKitchenApiConfiguration();

        services.AddSingleton(apiConfiguration);

        return services;
    }

    public static IServiceCollection AddTheStockedKitchenApiAuthorizationMessageHandler(this IServiceCollection services)
    {
        services.AddTransient(serviceProvider =>
        {
            var tokenAccessorProvider = serviceProvider.GetRequiredService<IAccessTokenProvider>();

            var navigationManager = serviceProvider.GetRequiredService<NavigationManager>();

            var apiConfiguration = serviceProvider.GetRequiredService<TheStockedKitchenApiConfiguration>();

            var messageHandler =
                new TheStockedKitchenApiAuthorizationMessageHandler(
                    tokenAccessorProvider,
                    navigationManager,
                    apiConfiguration);

            return messageHandler;
        });

        return services;
    }

    public static IServiceCollection AddTheStockedKitchenApiClient(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var apiConfiguration = configuration.GetTheStockedKitchenApiConfiguration();

        services
            .AddTheStockedKitchenApiConfiguration(configuration)
            //.AddTheStockedKitchenApiAuthorizationMessageHandler()
            .AddHttpClient<ITheStockedKitchenClient, TheStockedKitchenClient>()
            //.AddHttpMessageHandler<TheStockedKitchenApiAuthorizationMessageHandler>()
            .ConfigureHttpClient(client => client.BaseAddress = new Uri(apiConfiguration.BaseUrl));

        return services;
    }
}