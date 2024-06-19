using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PatientTickets.Api.JsonSerializer;
using PatientTickets.Application.Abstractions.Service;
using PatientTickets.Application.Behavior;
using PatientTickets.Domain.Enums;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

namespace PatientTickets.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreApiServices(this IServiceCollection services)
    {
        return services
            .AddHttpContextAccessor()
            .Configure<JsonOptions>(options =>
            {
                options.SerializerOptions.Converters.Add(new TrimmingConverter());
                options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            })
            .AddResponseCompression(options => { options.EnableForHttps = true; });
    }

    public static IServiceCollection AddAllCors(this IServiceCollection services)
    {
        return services
            .AddCors(options =>
            {
                options.AddPolicy(CorsPolicy.AllowAll, policy =>
                {

                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();

                    policy.WithExposedHeaders("*");
                    policy.SetIsOriginAllowed(origin => true);
                });
            });
    }
    public static IServiceCollection AddSwaggerWidthJwtAuth(
       this IServiceCollection services,
       Assembly apiAssembly,
       string appName,
       string version,
       string description)
    {
        return services.AddEndpointsApiExplorer()
            .AddSwaggerGen(options =>
            {
                options.SwaggerDoc(version, new OpenApiInfo
                {
                    Version = version,
                    Title = appName,
                    Description = description
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });

                // using System.Reflection;
                var xmlFilename = $"{apiAssembly.GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
    }


    public static IServiceCollection AddCoreAuthApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorization()
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!))
                };
            });

        var authBuilder = services.AddAuthorizationBuilder();
        authBuilder
            .AddPolicy(AuthorizationPoliciesEnum.AdminGreetings.ToString(), policy =>
                policy.RequireRole(ApplicationUserRolesEnum.Admin.ToString()));

        return services.AddTransient<ICurrentUserService, CurrentUserService>();
    }
    public static IServiceCollection AddCoreAuthServices(this IServiceCollection services)
    {
        return services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizePermissionsBehavior<,>));
    }
    public static IServiceCollection AddCoreApplicationServices(this IServiceCollection services)
    {
        return services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(DatabaseTransactionBehavior<,>));
    }


    //public static IServiceCollection AddSwagger(
    //    this IServiceCollection services,
    //    Assembly apiAssembly,
    //    string appName,
    //    string version,
    //    string description)
    //{
    //    return services.AddEndpointsApiExplorer()
    //        .AddSwaggerGen(options =>
    //        {
    //            options.SwaggerDoc(version, new OpenApiInfo
    //            {
    //                Version = version,
    //                Title = appName,
    //                Description = description
    //            });

    //            // using System.Reflection;
    //            var xmlFilename = $"{apiAssembly.GetName().Name}.xml";
    //            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    //        });
    //}
}