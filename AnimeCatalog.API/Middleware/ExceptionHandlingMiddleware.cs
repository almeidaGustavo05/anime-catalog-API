using AnimeCatalog.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace AnimeCatalog.API.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu uma exceção: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        
        var response = exception switch
        {
            BaseException baseEx => new ErrorResponse
            {
                Message = baseEx.Message,
                ErrorCode = baseEx.ErrorCode,
                StatusCode = (int)baseEx.StatusCode,
                Details = baseEx.Details
            },
            
            ArgumentException argEx => new ErrorResponse
            {
                Message = argEx.Message,
                ErrorCode = "INVALID_ARGUMENT",
                StatusCode = (int)HttpStatusCode.BadRequest
            },
            
            UnauthorizedAccessException => new ErrorResponse
            {
                Message = "Acesso não autorizado.",
                ErrorCode = "UNAUTHORIZED",
                StatusCode = (int)HttpStatusCode.Unauthorized
            },
            
            _ => new ErrorResponse
            {
                Message = "Ocorreu um erro interno no servidor.",
                ErrorCode = "INTERNAL_SERVER_ERROR",
                StatusCode = (int)HttpStatusCode.InternalServerError
            }
        };
        
        context.Response.StatusCode = response.StatusCode;
        
        var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });
        
        await context.Response.WriteAsync(jsonResponse);
    }
}

public class ErrorResponse
{
    public string Message { get; set; } = string.Empty;
    public string ErrorCode { get; set; } = string.Empty;
    public int StatusCode { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public object? Details { get; set; }
}