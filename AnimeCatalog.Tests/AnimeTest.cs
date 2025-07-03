using AnimeCatalog.Domain.Entities;
using Xunit;

namespace AnimeCatalog.Tests;

public class AnimeTest
{
    [Fact]
    public void Should_Create_Anime_With_Valid_Properties()
    {
        var name = "Attack on Titan";
        var director = "Hajime Isayama";
        var summary = "A thrilling anime about humanity's fight against titans.";

        var anime = new Anime
        {
            Name = name,
            Director = director,
            Summary = summary
        };

        Assert.Equal(name, anime.Name);
        Assert.Equal(director, anime.Director);
        Assert.Equal(summary, anime.Summary);
    }

    [Fact]
    public void Should_Create_Anime_With_BaseEntity_Properties()
    {
        var anime = new Anime
        {
            Id = 1,
            Name = "Naruto",
            Director = "Masashi Kishimoto",
            Summary = "A young ninja's journey to become Hokage.",
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        Assert.Equal(1, anime.Id);
        Assert.True(anime.CreatedAt != default);
        Assert.True(anime.UpdatedAt != default);
        Assert.Null(anime.DeletedAt);
    }

    [Fact]
    public void Should_Initialize_With_Empty_Strings()
    {
        var anime = new Anime();

        Assert.Equal(string.Empty, anime.Name);
        Assert.Equal(string.Empty, anime.Director);
        Assert.Equal(string.Empty, anime.Summary);
    }

    [Fact]
    public void Should_Allow_Setting_DeletedAt_For_Soft_Delete()
    {
        var anime = new Anime
        {
            Name = "One Piece",
            Director = "Eiichiro Oda",
            Summary = "A pirate's adventure to find the One Piece treasure."
        };
        
        var deletedAt = DateTime.Now;
        anime.DeletedAt = deletedAt;

        Assert.Equal(deletedAt, anime.DeletedAt);
    }

    [Fact]
    public void Should_Update_UpdatedAt_Property()
    {
        var anime = new Anime
        {
            Name = "Dragon Ball Z",
            Director = "Akira Toriyama",
            Summary = "Goku's adventures and battles to protect Earth.",
            CreatedAt = DateTime.Now.AddDays(-1),
            UpdatedAt = DateTime.Now.AddDays(-1)
        };

        var newUpdateTime = DateTime.Now;
        anime.UpdatedAt = newUpdateTime;
        anime.Summary = "Updated summary about Goku's epic battles.";

        Assert.Equal("Updated summary about Goku's epic battles.", anime.Summary);
        Assert.True(anime.UpdatedAt >= newUpdateTime.AddSeconds(-1));
    }

    [Fact]
    public void Should_Handle_Long_Summary_Text()
    {
        var longSummary = new string('A', 1000); 
        
        var anime = new Anime
        {
            Name = "Test Anime",
            Director = "Test Director",
            Summary = longSummary
        };

        Assert.Equal(longSummary, anime.Summary);
        Assert.Equal(1000, anime.Summary.Length);
    }

    [Fact]
    public void Should_Handle_Special_Characters_In_Properties()
    {
        var name = "Anime: Special & Characters!";
        var director = "Director-Name (2024)";
        var summary = "Summary with special chars: @#$%^&*()";

        var anime = new Anime
        {
            Name = name,
            Director = director,
            Summary = summary
        };

        Assert.Equal(name, anime.Name);
        Assert.Equal(director, anime.Director);
        Assert.Equal(summary, anime.Summary);
    }

    [Fact]
    public void Should_Inherit_From_BaseEntity()
    {
        var anime = new Anime();
        
        Assert.IsAssignableFrom<BaseEntity>(anime);
    }
}