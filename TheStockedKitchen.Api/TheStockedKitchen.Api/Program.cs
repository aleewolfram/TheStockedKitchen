using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using System.Configuration;
using TheStockedKitchen.Api.Config;
using TheStockedKitchen.Api.Services;
using TheStockedKitchen.Db;

var builder = WebApplication.CreateBuilder(args);

var theStockedKitchenConfiguration = builder.Configuration.GetTheStockedKitchenConfiguration();

builder.Services.AddTheStockedKitchenConfiguration(builder.Configuration);

builder.Services.AddMicrosoftIdentityWebApiAuthentication(builder.Configuration);

builder.Services.AddDbContext<AppDBContext>(options =>
{
    options.UseSqlServer(theStockedKitchenConfiguration.ConnectionStrings.AppDb,
        sqlServerOptions =>
            sqlServerOptions.MigrationsHistoryTable(
                theStockedKitchenConfiguration.EntityFramework.MigrationsHistoryTable,
                theStockedKitchenConfiguration.EntityFramework.MigrationsHistorySchema));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: theStockedKitchenConfiguration.WasmCors.PolicyName,
        policyBuilder => {
            policyBuilder
                .WithOrigins(theStockedKitchenConfiguration.WasmCors.BaseUrl)
                .AllowAnyMethod()
                .AllowAnyHeader()
            .AllowCredentials();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerDocument();

builder.Services.AddScoped<IFoodStockService, FoodStockService>();
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IIngredientService, IngredientService>();
builder.Services.AddScoped<IUnitService, UnitService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDBContext>();
        db.Database.Migrate();
    }
    app.UseOpenApi();
    app.UseSwaggerUi3();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(theStockedKitchenConfiguration.WasmCors.PolicyName);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
