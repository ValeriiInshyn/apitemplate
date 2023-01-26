using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Serilog;
using Server.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Server.Presentation.Options;

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

    #region Authentication

    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var authOptions = configuration.GetSection("AuthOptions").Get<AuthOptions>() ??
                          throw new NullReferenceException();

        services.AddCors(options =>
        {
            options.AddPolicy("All", builder =>
            {
                builder.WithOrigins("*")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = authOptions.ValidateIssuer,
                ValidIssuer = authOptions.Issuer,
                ValidateAudience = authOptions.ValidateAudience,
                ValidAudience = authOptions.Audience,
                ValidateLifetime = authOptions.ValidateAccessTokenLifetime,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(authOptions.Key)),
                ValidateIssuerSigningKey = authOptions.ValidateKey
            };
            options.Events = new JwtBearerEvents
            {
                OnChallenge = async context =>
                {
                    context.HandleResponse();
                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "application/json";
                    if (context.AuthenticateFailure?.GetType() == typeof(SecurityTokenExpiredException))
                    {
                        await context.Response.WriteAsJsonAsync(new SecurityTokenExpiredException("Token expired"));
                        return;
                    }

                    await context.Response.WriteAsync("Not authorized");
                },
                OnAuthenticationFailed = context =>
                {
                    if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        context.Response.Headers.Add("Token-Expired", "true");

                    return Task.CompletedTask;
                }
            };
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
            
            //Authentication settings for swagger
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "Put ONLY your JWT Bearer token in text box below!",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    jwtSecurityScheme,
                    Array.Empty<string>()
                }
            });

            options.OrderActionsBy(apiDescription =>
                $"{apiDescription.ActionDescriptor.RouteValues["controller"]}_{apiDescription.HttpMethod}_{apiDescription.RelativePath}");

            options.SupportNonNullableReferenceTypes(); // Sets Nullable flags appropriately.              
            options.UseAllOfForInheritance(); // Allows $ref objects to be nullable
        });
        services.AddRouting(routeOptions => routeOptions.LowercaseUrls = true);
    }

    #endregion
}