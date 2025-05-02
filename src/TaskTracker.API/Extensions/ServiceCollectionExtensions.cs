using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
    
}
