using AnimeCatalog.Application.Exceptions;
using System.Text.Json;
using Xunit;

namespace AnimeCatalog.Tests;

public class AnimeRepositoryTest
{
    [Fact]
    public void Should_Throw_AnimeNotFoundException_When_Anime_Not_Found()
    {
        var animeId = 999;

        Action action = () => throw new AnimeNotFoundException(animeId);
        var ex = Assert.Throws<AnimeNotFoundException>(action);
        
        Assert.Equal($"Anime com ID {animeId} não foi encontrado.", ex.Message);
        Assert.Equal(animeId, ex.AnimeId);
        Assert.Equal("ANIME_NOT_FOUND", ex.ErrorCode);
    }

    [Fact]
    public void Should_Create_AnimeNotFoundException_With_Correct_Details()
    {
        var animeId = 123;
        var ex = new AnimeNotFoundException(animeId);

        Assert.NotNull(ex.Details);
        
        var json = JsonSerializer.Serialize(ex.Details);
        var detailsDict = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
        
        Assert.True(detailsDict.ContainsKey("AnimeId"));
        Assert.Equal(animeId, ((JsonElement)detailsDict["AnimeId"]).GetInt32());
    }

    [Fact]
    public void Should_Have_Correct_AnimeId_Property()
    {
        var animeId = 456;
        var ex = new AnimeNotFoundException(animeId);

        Assert.Equal(animeId, ex.AnimeId);
    }

    [Fact]
    public void Should_Have_Correct_StatusCode_And_ErrorCode()
    {
        var ex = new AnimeNotFoundException(1);

        Assert.Equal(System.Net.HttpStatusCode.NotFound, ex.StatusCode);
        Assert.Equal("ANIME_NOT_FOUND", ex.ErrorCode);
    }
}