using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Application.Common.Interfaces;
using TaskTracker.Infastructore.Auth;
using TaskTracker.Infastructore.Common.Persistence;
using TaskTracker.Infastructore.Identity;

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
    
}
