using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Serilog;
using Server.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;

namespace Server.Presentation;

public static class DependencyInjection
{
    #region ApiVersioning

    public static void AddApiVersioningSupport(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(DateTime.Now);
            options.RegisterMiddleware = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        });

        services.AddVersionedApiExplorer(options =>
        {
            options.SubstituteApiVersionInUrl = true;
            options.GroupNameFormat = "'v'VV";
        });
    }

    #endregion

    #region Logger

    public static void AddSerilog(this IServiceCollection services, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
    }

    #endregion

    #region Repos_Services_Middlewares

    public static void AddRepositories(this IServiceCollection services)
    {
    }

    public static void AddServices(this IServiceCollection services)
    {
        //Custom ones
        services.AddSingleton(Log.Logger);

        //Default ones
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public static void AddMiddlewares(this WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }

    #endregion

    #region Swagger

    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();
        services.AddSwaggerGen(options =>
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, new OpenApiInfo
                {
                    Version = description.ApiVersion.ToString(),
                    Title = "API-TEMPLATE",
                    Description = "API-TEMPLATE DESCRIPTION",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Example Contact",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Example License",
                        Url = new Uri("https://example.com/license")
                    }
                });
            }

            options.OrderActionsBy(apiDescription =>
                $"{apiDescription.ActionDescriptor.RouteValues["controller"]}_{apiDescription.HttpMethod}_{apiDescription.RelativePath}");
            
            options.SupportNonNullableReferenceTypes(); // Sets Nullable flags appropriately.              
            options.UseAllOfForInheritance(); // Allows $ref objects to be nullable
        });
        services.AddRouting(routeOptions => routeOptions.LowercaseUrls = true);
    }

    #endregion
}