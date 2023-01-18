using Microsoft.AspNetCore.Mvc;
using Serilog;
using Server.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Mvc.Versioning;

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

    

    #endregion

   
  
    
    
}