using DrillBlockManager.Database;
using DrillBlockManager.Helpers;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Npgsql;



var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder().SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .Build();

builder.Services.AddControllers();

#region Swagger

builder.Services.AddSwaggerGen(options =>
{

    options.SwaggerDoc("v1.0", new OpenApiInfo
    {
        Title = $"Drill block manager",
        Description = $"Версия сборки: 1.0",
        Version = "v1.0"
    });

    options.UseAllOfToExtendReferenceSchemas();

    string pathToXmlDocs = Path.Combine(AppContext.BaseDirectory, AppDomain.CurrentDomain.FriendlyName + ".xml");
    options.IncludeXmlComments(pathToXmlDocs, true);
});

builder.Services.AddSwaggerGenNewtonsoftSupport();
#endregion

#region DataBase Contexts

NpgsqlConnection.GlobalTypeMapper.UseJsonNet(settings: new()
{
    Formatting = Formatting.None,
    ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver
    {
        NamingStrategy = new Newtonsoft.Json.Serialization.CamelCaseNamingStrategy()
    }
});

builder.Services.AddDbContext<PostgreDbContext>((serviceProvider, options) =>
{
    var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

    options.UseLoggerFactory(loggerFactory)
        .UseNpgsql(configuration.GetConnectionString("Postgre"),
            config => config.CommandTimeout(30));

    if (builder.Environment.IsDevelopment())
    {
        options.EnableSensitiveDataLogging();
    }
}, ServiceLifetime.Transient);

#endregion

builder.Services.RegistrateServices();

var app = builder.Build();

if (app.Environment.IsDevelopment() || configuration.GetValue<bool>("enable_swagger"))
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Drill block manager v1.0");
        options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
    });

    RewriteOptions redirections = new();
    redirections.AddRedirect("^$", "swagger");
    app.UseRewriter(redirections);
}

app.MapControllers();

app.Run();
