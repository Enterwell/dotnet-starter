using Acme.Application.Configuration;
using Acme.Infrastructure.EF.PostgreSql.Configuration;
using Acme.Interface.WebAPI.Configuration;
using Acme.Interface.WebAPI.Configuration.InstallerExtensions;
using Acme.Interface.WebAPI.Extensions;
using Enterwell.Exceptions.Web;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Environment information
var isDevelopment = builder.Environment.IsDevelopment();
var isProduction = builder.Environment.IsProduction();

// Configure Serilog
var serilogLogger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();
builder.Host.UseSerilog(serilogLogger);

// Add services to the DI container.
builder.Services
    .AddCustomConfiguration(builder.Configuration)
    .AddAutoMapper(config =>
        {
            config.ShouldUseConstructor = (_ => true);
        },
        typeof(ApiMapperProfile).Assembly,
        typeof(ApplicationMapperProfile).Assembly,
        typeof(EfPostgreSqlMapperProfile).Assembly
    )
    .AddCustomSwagger()
    .AddApiServices()
    .AddApplicationServices()
    .AddEfPostgreSqlInfrastructure(builder.Configuration)
    .AddCustomCors()
    .AddCustomAuth(builder.Configuration)
    .AddCustomControllers()
    .AddCustomHealthChecks(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (isDevelopment)
{
    app.UseDeveloperExceptionPage();
}

app
    .UseSerilogRequestLogging()
    .UseCustomRouting(isProduction)
    .UseCustomCors()
    .UseEnterwellHttpExceptionMiddleware()
    .UseCustomAuth()
    .UseCustomControllers()
    .UseCustomSwagger(isProduction);

app
    .MapHealthChecksWithJsonResponse("/healthz");

app.Run();
