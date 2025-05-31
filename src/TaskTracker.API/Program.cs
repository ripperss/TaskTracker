using Microsoft.EntityFrameworkCore;
using System;
using TaskTracker.API.Extensions;
using TaskTracker.API.Middlewares;
using TaskTracker.API.Services.UserServices;
using TaskTracker.Infastructore.Common.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IJwtValidatorService, JwtValidatorService>();

builder.AddData(builder.Configuration)
    .AddSwagger()
    .Auth()
    .AddApplicationLayer()
    .AddBearerAuthorizetion(builder.Configuration)
    .AddSerialog();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TaskTrackerDbContext>();
    try
    {
        dbContext.Database.Migrate();
    }
    catch (Exception ex)
    {
        throw;
    }
}

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();
