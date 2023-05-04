using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using TheStockedKitchen.Api.Config;
using SCGPlanningTool.Db;
using SCGPlanningTool.Api.Services;

var builder = WebApplication.CreateBuilder(args);

var scgPlanningConfiguration = builder.Configuration.GetTheStockedKitchenConfiguration();

builder.Services.AddTheStockedKitchenConfiguration(builder.Configuration);

builder.Services.AddMicrosoftIdentityWebApiAuthentication(builder.Configuration);

builder.Services.AddDbContext<AppDBContext>(options =>
{
    options.UseSqlServer(scgPlanningConfiguration.ConnectionStrings.AppDb,
        sqlServerOptions =>
            sqlServerOptions.MigrationsHistoryTable(
                scgPlanningConfiguration.EntityFramework.MigrationsHistoryTable,
                scgPlanningConfiguration.EntityFramework.MigrationsHistorySchema));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: scgPlanningConfiguration.WasmCors.PolicyName,
        policyBuilder => {
            policyBuilder
                .WithOrigins(scgPlanningConfiguration.WasmCors.BaseUrl)
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

app.UseCors(scgPlanningConfiguration.WasmCors.PolicyName);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
