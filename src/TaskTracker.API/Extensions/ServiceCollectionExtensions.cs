using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Text;
using TaskTracker.Application.Common.Interfaces;
using TaskTracker.Infastructore.Auth;
using TaskTracker.Infastructore.Common.Persistence;
using TaskTracker.Infastructore.Identity;
using TaskTracker.Infastructore.Managers;
using TaskTracker.Infastructore.Teams;
using TaskTracker.Infastructore.Users;

namespace TaskTracker.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static WebApplicationBuilder AddData(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        builder.Services.AddDbContext<TaskTrackerDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Data")));

        builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<TaskTrackerDbContext>()
            .AddDefaultTokenProviders();

        return builder;
    }

    public static WebApplicationBuilder Auth(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IJwtTokenGeneration, JwtTokenGeneration>();

        builder.Services.Configure<JwtSettings>(
            builder.Configuration.GetSection(nameof(JwtSettings)));

        return builder;
    }

    public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
    {    

        builder.Services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "TaskTracker Api",
                Version = "v1",
            });

            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Введите JWT токен в формате: Bearer {токен}",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });

            option.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                    }
                },
                new string[] {}
            }
        });
        });

        return builder;
    }

    public static WebApplicationBuilder AddApplicationLayer(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(typeof(Application.AssemblyReference).Assembly);
        });

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserApplicationService, UserApplicationService>();
        builder.Services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<TaskTrackerDbContext>());
        builder.Services.AddScoped<ITeamRepositoty, TeamRepositoty>();
        builder.Services.AddScoped<IManagerRepository, ManagerRepository>();

        builder.Services.AddValidatorsFromAssemblyContaining(typeof(Application.AssemblyReference));

        return builder;
    }

    public static WebApplicationBuilder AddBearerAuthorizetion(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        var authSettings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(authSettings.TokenPrivateKey)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.Events.OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = 401; 
                return Task.CompletedTask;
            };
        });

        builder.Services.AddAuthorization();

        return builder;
    }

    public static WebApplicationBuilder AddSerialog(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
            .Enrich.FromLogContext()
            .MinimumLevel.Information()
            .CreateLogger();

        builder.Host.UseSerilog();

        return builder;
    }
}
