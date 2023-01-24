using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Net;
using System.Security.Claims;
using System.Text;
using TransPorter.Domain;
using TransPorter.Infrastructure;
using TransPorter.Shared.Wrapper;
using TransPoster.Application;
using TransPoster.Application.Interface;

namespace TransPoster.MVC.Extensions;

public static class ServiceCollectionExtensions
{
    public static AppConfiguration GetApplicationSettings(
       this IServiceCollection services, IConfiguration configuration)
    {
        var applicationSettingsConfiguration = configuration.GetSection(nameof(AppConfiguration));
        services.Configure<AppConfiguration>(applicationSettingsConfiguration);
        return applicationSettingsConfiguration.Get<AppConfiguration>();
    }

    public static IServiceCollection GetDataBaseDbContextConfig(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<IDatabaseSeeder, DatabaseSeeder>();
        return services.AddDbContextPool<AppDbContext>(options =>
                options.ConfigureDbContextOptions(configuration));
    }

    private static void ConfigureDbContextOptions(
        this DbContextOptionsBuilder options,
        IConfiguration configuration)
        => options.UseSqlServer(/*Environment.GetEnvironmentVariable("CONNECTION_STRING") ??*/ 
            configuration.GetConnectionString("AppConnectionString"));

    public static IServiceCollection GetJwtAuthenticationConfig(
        this IServiceCollection services,
        AppConfiguration config)
    {
        var key = Encoding.ASCII.GetBytes(/*Environment.GetEnvironmentVariable("APP_CONFIG_SECRET") ??*/ config.Secret);
        services.AddAuthentication(auth =>
        {
            auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(bearer =>
        {
            bearer.RequireHttpsMetadata = false;
            bearer.SaveToken = true;
            bearer.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                RoleClaimType = ClaimTypes.Role,
                ClockSkew = TimeSpan.Zero
            };
            bearer.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = c =>
                {
                    if (c.Exception is SecurityTokenExpiredException)
                    {
                        c.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(Result.Fail("The Token is expired."));
                        return c.Response.WriteAsync(result);
                    }
                    else
                    {
#if DEBUG
                        c.NoResult();
                        c.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        c.Response.ContentType = "text/plain";
                        return c.Response.WriteAsync(c.Exception.ToString());
#else
                            c.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            c.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject(Result.Fail("An unhandled error has occurred."));
                            return c.Response.WriteAsync(result);
#endif
                    }
                },
                OnChallenge = context =>
                {
                    context.HandleResponse();
                    if (!context.Response.HasStarted)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(Result.Fail("You are not Authorized."));
                        return context.Response.WriteAsync(result);
                    }

                    return Task.CompletedTask;
                },
                OnForbidden = context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    context.Response.ContentType = "application/json";
                    var result = JsonConvert.SerializeObject(Result.Fail("You are not authorized to access this resource."));
                    return context.Response.WriteAsync(result);
                },
            };
        });
        //services.AddAuthorization(options =>
        //{
        //    foreach (var permission in Permissions.GetPermissions())
        //        options.AddPolicy(permission, policy => policy.RequireClaim(ApplicationClaimTypes.Permission, permission));
        //});
        //services.AddHttpContextAccessor();
        //services.AddScoped<ICurrentUserService, CurrentUserService>();
        //services.AddScoped<IAmazonSimpleStorageService, AmazonSimpleStorageService>();
        services.AddDataProtection();
        return services;
    }

    public static IServiceCollection GetUserIdentityConfig(this IServiceCollection services)
    {
        // To add authorization and permission policies here

        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
        {
            options.Password.RequiredLength = 10;
            options.Password.RequiredUniqueChars = 3;
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = true;
            options.SignIn.RequireConfirmedAccount = true;
            options.Lockout.MaxFailedAccessAttempts = 5;
        }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
        services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromHours(48));
        return services;
    }

    public static IServiceCollection GetInjectedRepositories(this IServiceCollection services)
    {
        services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        return services;
    }
}
