using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TheStockedKitchen.Web;
using TheStockedKitchen.Web.Infrastructure;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
    options.ProviderOptions.AdditionalScopesToConsent.Add(builder.Configuration["TheStockedKitchenAPI:Scope"]);
});

builder.Services.AddTheStockedKitchenApiClient(builder.Configuration);

await builder.Build().RunAsync();
