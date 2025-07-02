using System.Text.Json;
using AnimeCatalog.Application.Exceptions;
using System.Net;

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
        var response = context.Response;
        response.ContentType = "application/json";

        var (statusCode, message, errorCode) = exception switch
        {
            AnimeNotFoundException ex => (StatusCodes.Status404NotFound, ex.Message, "ANIME_NOT_FOUND"),
            AnimeAlreadyExistsException ex => (StatusCodes.Status409Conflict, ex.Message, "ANIME_ALREADY_EXISTS"),
            ValidationException ex => (StatusCodes.Status400BadRequest, ex.Message, "VALIDATION_ERROR"),
            BusinessRuleException ex => (StatusCodes.Status422UnprocessableEntity, ex.Message, "BUSINESS_RULE_VIOLATION"),
            DatabaseException ex => (StatusCodes.Status500InternalServerError, ex.Message, "DATABASE_ERROR"),
            ArgumentException ex => (StatusCodes.Status400BadRequest, ex.Message, "INVALID_ARGUMENT"),
            UnauthorizedAccessException => (StatusCodes.Status401Unauthorized, "Acesso não autorizado.", "UNAUTHORIZED"),
            _ => (StatusCodes.Status500InternalServerError, "Ocorreu um erro interno no servidor.", "INTERNAL_SERVER_ERROR")
        };

        response.StatusCode = statusCode;

        var errorResponse = new
        {
            Message = message,
            ErrorCode = errorCode,
            StatusCode = statusCode,
            Timestamp = DateTime.UtcNow
        };

        var result = JsonSerializer.Serialize(errorResponse);

        await response.WriteAsync(result);
    }
}