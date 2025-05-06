using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection.Metadata;
using TaskTracker.Application.Auth.Commands.Register;
using TaskTracker.Application.Common.Interfaces;
using TaskTracker.Infastructore.Auth;
using TaskTracker.Infastructore.Common.Persistence;
using TaskTracker.Infastructore.Identity;
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
                Description = "Please Enter a valid  token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });

            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
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

        return builder;
    }
}
