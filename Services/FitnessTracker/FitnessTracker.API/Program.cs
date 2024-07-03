using FitnessTracker.API;
using FitnessTracker.Application;
using FitnessTracker.Infrastructure;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    ApplicationName = typeof(Program).Assembly.FullName,
    ContentRootPath = Directory.GetCurrentDirectory(),
    EnvironmentName = Environments.Development,
    WebRootPath = ""
});

#region Application Settings

string env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
builder.Environment.EnvironmentName = env;

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile($"appsettings.{env}.json", false, true)
    .AddJsonFile($"serilogsettings.{env}.json", false, true)
    .AddEnvironmentVariables();
#endregion

#region Serilog Configuration

builder.Host.UseSerilog();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();
#endregion

#region Swagger Configuration

builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());
builder.Services.AddSwaggerCommonResponseExamples();
builder.Services.AddFluentValidationRulesToSwagger();

builder.Services.AddSwaggerGen(opt =>
{
    opt.EnableAnnotations();

    opt.UseInlineDefinitionsForEnums();
    opt.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

    using (ServiceProvider serviceProvider = builder.Services.BuildServiceProvider())
    {
        IApiVersionDescriptionProvider provider = serviceProvider.GetRequiredService<IApiVersionDescriptionProvider>();

        foreach (var description in provider.ApiVersionDescriptions)
        {
            opt.SwaggerDoc(description.GroupName, new OpenApiInfo
            {
                Title = "Fitness Tracker API",
                Version = description.ApiVersion.ToString(),
                Description = "Fitness Tracker API",
            });
        }
    }

    //Api assembly xml documents
    List<string> apiXmlDocs = Directory.GetFiles(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"{Assembly.GetExecutingAssembly().GetName().Name.Split('.').FirstOrDefault()}*.xml").ToList();

    foreach (string filePath in apiXmlDocs)
    {
        opt.IncludeXmlComments(filePath);
    }

    opt.ExampleFilters();
});
#endregion

#region Controller Configuration

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers()
    .AddFluentValidation(config => config.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()))
    .AddJsonOptions(options => {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
    });

builder.Services.AddEndpointsApiExplorer();
#endregion

#region Custom FitnessTrackerAPI Configuration

builder.Services.AddApiVersioningConfigured();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
#endregion

var app = builder.Build();

app.AddApplicationBuilderServices();

#region "Swagger Configuration"

app.UseSwagger();

IApiVersionDescriptionProvider provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

app.UseSwaggerUI(options =>
{
    foreach (ApiVersionDescription description in provider.ApiVersionDescriptions)
    {
        options.SwaggerEndpoint($"{description.GroupName}/swagger.json", $"Fitness Tracker API {description.GroupName.ToLowerInvariant()}");
    }
});
#endregion


app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.MapControllers().AllowAnonymous();
}
else
{
    app.MapControllers();
}

app.Run();
