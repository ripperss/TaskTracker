using Microsoft.EntityFrameworkCore;
using TaskTracker.API.Extensions;
using TaskTracker.API.Middlewares;
using TaskTracker.Infastructore.Common.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.AddData(builder.Configuration)
    .AddSwagger()
    .Auth()
    .AddApplicationLayer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();
