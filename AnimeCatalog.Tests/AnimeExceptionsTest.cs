using AnimeCatalog.Application.Exceptions;
using Xunit;
using System.Net;

namespace AnimeCatalog.Tests;

public class AnimeExceptionsTest
{
    [Fact]
    public void AnimeNotFoundException_Should_Have_Correct_Properties()
    {
        var animeId = 1;
        var exception = new AnimeNotFoundException(animeId);

        Assert.Equal($"Anime com ID {animeId} não foi encontrado.", exception.Message);
        Assert.Equal(animeId, exception.AnimeId);
        Assert.Equal("ANIME_NOT_FOUND", exception.ErrorCode);
        Assert.Equal(HttpStatusCode.NotFound, exception.StatusCode);
    }

    [Fact]
    public void AnimeAlreadyExistsException_Should_Have_Correct_Properties()
    {
        var animeName = "Naruto";
        var exception = new AnimeAlreadyExistsException(animeName);

        Assert.Equal($"Já existe um anime com o nome '{animeName}'.", exception.Message);
        Assert.Equal(animeName, exception.AnimeName);
        Assert.Equal("ANIME_ALREADY_EXISTS", exception.ErrorCode);
        Assert.Equal(HttpStatusCode.Conflict, exception.StatusCode);
    }

    [Fact]
    public void ValidationException_Should_Handle_Field_Error()
    {
        var field = "Name";
        var error = "Nome é obrigatório";
        var exception = new ValidationException(field, error);

        Assert.Equal($"Erro de validação no campo '{field}': {error}", exception.Message);
        Assert.Equal("VALIDATION_ERROR", exception.ErrorCode);
        Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
        Assert.True(exception.Errors.ContainsKey(field));
    }

    [Fact]
    public void DatabaseException_Should_Have_Correct_Properties()
    {
        var operation = "INSERT";
        var message = "Falha na conexão";
        var exception = new DatabaseException(operation, message);

        Assert.Equal($"Erro na operação de banco de dados '{operation}': {message}", exception.Message);
        Assert.Equal("DATABASE_ERROR", exception.ErrorCode);
        Assert.Equal(HttpStatusCode.InternalServerError, exception.StatusCode);
    }

    [Fact]
    public void BusinessRuleException_Should_Have_Correct_Properties()
    {
        var rule = "DUPLICATE_NAME";
        var message = "Nome duplicado não é permitido";
        var exception = new BusinessRuleException(rule, message);

        Assert.Equal(message, exception.Message);
        Assert.Equal(rule, exception.RuleName);
        Assert.Equal("BUSINESS_RULE_VIOLATION", exception.ErrorCode);
        Assert.Equal(HttpStatusCode.UnprocessableEntity, exception.StatusCode);
    }
}