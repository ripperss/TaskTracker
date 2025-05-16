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
builder.Services.AddScoped<IUserService, UserService>();

builder.AddData(builder.Configuration)
    .AddSwagger()
    .Auth()
    .AddApplicationLayer()
    .AddBearerAuthorizetion(builder.Configuration);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();
