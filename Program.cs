using System.Reflection;
using beeproperties.admin.webapi.FluentValidationCollectionExtensions;
using beeproperties.admin.webapi.ServiceCollectionExtensions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

// Configure CORS for web access
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// OpenAPI & Swagger documentation setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Bee Properties Admin API",
        Version = "v1",
        Description = "ASP.NET Core Web API for Bee Properties Management (Community, Agency), with CQRS & Handler Architecture."
    });

    // Include XML comments if available
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

// Register CQRS services, repositories, handlers
builder.Services.AddAppServices();

// Register FluentValidation validators
builder.Services.AddAppFluentValidation();

var app = builder.Build();

// Configure the HTTP request pipeline.
// Middleware to ensure OpenAPI version compatibility with Swagger UI (e.g. rewrite 3.0.4 -> 3.0.1)
app.Use(async (context, next) =>
{
    var path = context.Request.Path.Value;
    if (path != null && (path.EndsWith("swagger.json", StringComparison.OrdinalIgnoreCase) || path.EndsWith(".json", StringComparison.OrdinalIgnoreCase)))
    {
        var originalBodyStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        await next();

        if (context.Response.StatusCode == StatusCodes.Status200OK)
        {
            responseBody.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(responseBody).ReadToEndAsync();
            if (text.Contains("\"3.0.4\""))
            {
                text = text.Replace("\"openapi\": \"3.0.4\"", "\"openapi\": \"3.0.1\"")
                           .Replace("\"openapi\":\"3.0.4\"", "\"openapi\":\"3.0.1\"");
            }

            var bytes = System.Text.Encoding.UTF8.GetBytes(text);
            context.Response.Body = originalBodyStream;
            context.Response.ContentLength = bytes.Length;
            await context.Response.Body.WriteAsync(bytes);
            return;
        }

        responseBody.Seek(0, SeekOrigin.Begin);
        await responseBody.CopyToAsync(originalBodyStream);
        return;
    }

    await next();
});

// Enable Swagger UI across environments so it is accessible on MonsterASP.NET
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bee Properties Admin API v1");
    c.RoutePrefix = "swagger";
    c.DocumentTitle = "Bee Properties Admin API - Swagger UI";
    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
});

// Redirect root to Swagger UI for quick browser access
app.MapGet("/", () => Results.Redirect("/swagger"));

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
