
using TaskTracker.Domain.Tems.Exceptions;
using TaskTracker.Infastructore.Exceptions;

namespace TaskTracker.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next
        , ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(NotFoundUserBtEmailException ex)
        {
            _logger.LogWarning(ex.Message);
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new
            {
                error = ex.Message
            });
        }
        catch(IncorrectPasswordExcepton ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new
            {
                error = ex.Message
            });

            _logger.LogWarning(ex.Message);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new 
            { 
                error = ex.Message 
            });

            _logger.LogWarning(ex.Message, "Не обработаные до конца ошибки");
        }
    }
}
