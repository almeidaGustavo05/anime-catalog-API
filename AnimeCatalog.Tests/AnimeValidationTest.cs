using AnimeCatalog.Domain.Entities;
using AnimeCatalog.Application.Exceptions;
using Xunit;

namespace AnimeCatalog.Tests;

public class AnimeValidationTest
{
    [Fact]
    public void Should_Throw_ValidationException_When_Name_Is_Empty()
    {
        var anime = new Anime
        {
            Name = "",
            Director = "Test Director",
            Summary = "Test Summary"
        };

        var ex = Assert.Throws<ValidationException>(() => ValidateAnime(anime));
        Assert.Contains("Name", ex.Errors.Keys);
    }

    [Fact]
    public void Should_Throw_ValidationException_When_Director_Is_Empty()
    {
        var anime = new Anime
        {
            Name = "Test Anime",
            Director = "",
            Summary = "Test Summary"
        };

        var ex = Assert.Throws<ValidationException>(() => ValidateAnime(anime));
        Assert.Contains("Director", ex.Errors.Keys);
    }

    [Fact]
    public void Should_Throw_ValidationException_When_Summary_Is_Empty()
    {
        var anime = new Anime
        {
            Name = "Test Anime",
            Director = "Test Director",
            Summary = ""
        };

        var ex = Assert.Throws<ValidationException>(() => ValidateAnime(anime));
        Assert.Contains("Summary", ex.Errors.Keys);
    }

    [Fact]
    public void Should_Pass_Validation_With_Valid_Anime()
    {
        var anime = new Anime
        {
            Name = "Valid Anime",
            Director = "Valid Director",
            Summary = "Valid Summary"
        };

        var exception = Record.Exception(() => ValidateAnime(anime));
        Assert.Null(exception);
    }

    [Fact]
    public void Should_Throw_ValidationException_With_Multiple_Errors()
    {
        var anime = new Anime
        {
            Name = "",
            Director = "",
            Summary = "Valid Summary"
        };

        var ex = Assert.Throws<ValidationException>(() => ValidateAnime(anime));
        Assert.Equal(2, ex.Errors.Count);
        Assert.Contains("Name", ex.Errors.Keys);
        Assert.Contains("Director", ex.Errors.Keys);
    }

    private static void ValidateAnime(Anime anime)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(anime.Name))
            errors.Add("Name", new[] { "Nome não pode ser vazio." });

        if (string.IsNullOrWhiteSpace(anime.Director))
            errors.Add("Director", new[] { "Diretor não pode ser vazio." });

        if (string.IsNullOrWhiteSpace(anime.Summary))
            errors.Add("Summary", new[] { "Resumo não pode ser vazio." });

        if (errors.Any())
            throw new ValidationException(errors);
    }
}