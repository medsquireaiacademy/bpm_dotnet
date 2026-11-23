using System.Reflection;
using beeproperties.admin.webapi.FluentValidationCollectionExtensions;
using beeproperties.admin.webapi.repository.Context;
using beeproperties.admin.webapi.ServiceCollectionExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

// CORS configuration for Admin_bee_web React portal
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configure Entity Framework Core with connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Server=13.204.39.91;Database=YodaLIMS;User Id=sa;Password=dev1@dms;TrustServerCertificate=True;MultipleActiveResultSets=true;";

builder.Services.AddDbContext<BeePropertiesDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 3,
            maxRetryDelay: TimeSpan.FromSeconds(5),
            errorNumbersToAdd: null);
    }));

// OpenAPI & Swagger documentation setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Bee Properties Admin API",
        Version = "v1",
        Description = "ASP.NET Core 9.0 Web API for Bee Properties Management with Entity Framework Core and CQRS Architecture."
    });

    // Include XML comments if available
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

// Register CQRS services, repositories, handlers, data stores, seeder
builder.Services.AddAppServices(builder.Configuration);

// Register FluentValidation validators
builder.Services.AddAppFluentValidation();

var app = builder.Build();

// Automatically verify and seed database if accessible via EF Core
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetService<IDatabaseSeeder>();
    if (seeder != null)
    {
        await seeder.InitializeAsync();
    }
}

app.UseCors("AllowAll");

// Serve static files (such as file uploads in wwwroot)
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
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

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bee Properties Admin API v1");
        c.RoutePrefix = "swagger";
        c.DocumentTitle = "Bee Properties Admin API - Swagger UI";
        c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.List);
    });
}

// Redirect root to Swagger UI for quick browser access
app.MapGet("/", () => Results.Redirect("/swagger"));

app.UseAuthorization();

app.MapControllers();

app.Run();
