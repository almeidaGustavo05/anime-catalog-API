using AnimeCatalog.Application.Features.Anime.Commands.CreateAnime;
using AnimeCatalog.Application.Features.Anime.Commands.UpdateAnime;
using AnimeCatalog.Application.Features.Anime.DTOs;
using FluentValidation.TestHelper;
using Xunit;

namespace AnimeCatalog.Tests;

public class AnimeValidationTest
{
    private readonly CreateAnimeCommandValidator _createValidator = new();
    private readonly UpdateAnimeCommandValidator _updateValidator = new();

    [Fact]
    public void CreateAnimeCommand_Should_Validate_Required_Fields()
    {
        var command = new CreateAnimeCommand(new CreateAnimeDto { Name = "", Director = "", Summary = "" });
        var result = _createValidator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.AnimeDto.Name);
        result.ShouldHaveValidationErrorFor(x => x.AnimeDto.Director);
        result.ShouldHaveValidationErrorFor(x => x.AnimeDto.Summary);
    }

    [Fact]
    public void CreateAnimeCommand_Should_Validate_Max_Length()
    {
        var command = new CreateAnimeCommand(new CreateAnimeDto
        {
            Name = new string('a', 201),
            Director = new string('b', 101),
            Summary = new string('c', 1001)
        });
        var result = _createValidator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.AnimeDto.Name);
        result.ShouldHaveValidationErrorFor(x => x.AnimeDto.Director);
        result.ShouldHaveValidationErrorFor(x => x.AnimeDto.Summary);
    }

    [Fact]
    public void UpdateAnimeCommand_Should_Validate_Id_And_Required_Fields()
    {
        var command = new UpdateAnimeCommand(0, new UpdateAnimeDto { Name = "", Director = "", Summary = "" });
        var result = _updateValidator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Id);
        result.ShouldHaveValidationErrorFor(x => x.AnimeDto.Name);
    }

    [Fact]
    public void Valid_Commands_Should_Pass_Validation()
    {
        var createCommand = new CreateAnimeCommand(new CreateAnimeDto
        {
            Name = "Valid Name",
            Director = "Valid Director",
            Summary = "Valid Summary"
        });
        var updateCommand = new UpdateAnimeCommand(1, new UpdateAnimeDto
        {
            Name = "Valid Name",
            Director = "Valid Director",
            Summary = "Valid Summary"
        });

        _createValidator.TestValidate(createCommand).ShouldNotHaveAnyValidationErrors();
        _updateValidator.TestValidate(updateCommand).ShouldNotHaveAnyValidationErrors();
    }
}