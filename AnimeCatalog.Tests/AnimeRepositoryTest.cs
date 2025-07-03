using AnimeCatalog.Domain.Entities;
using AnimeCatalog.Domain.Pagination;
using AnimeCatalog.Infrastructure.Data;
using AnimeCatalog.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;
using FluentAssertions;

namespace AnimeCatalog.Tests;

public class AnimeRepositoryTest : IDisposable
{
    private readonly AnimeCatalogDbContext _context;
    private readonly AnimeRepository _repository;

    public AnimeRepositoryTest()
    {
        var options = new DbContextOptionsBuilder<AnimeCatalogDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AnimeCatalogDbContext(options);
        _repository = new AnimeRepository(_context);
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Anime_When_Exists()
    {
        var anime = CreateTestAnime("Test Anime");
        _context.Animes.Add(anime);
        await _context.SaveChangesAsync();

        var result = await _repository.GetByIdAsync(anime.Id);

        result.Should().NotBeNull();
        result!.Name.Should().Be("Test Anime");
    }

    [Fact]
    public async Task GetByIdAsync_Should_Return_Null_When_Not_Exists()
    {
        var result = await _repository.GetByIdAsync(999);
        result.Should().BeNull();
    }

    [Fact]
    public async Task CreateAsync_Should_Add_Anime_With_Timestamps()
    {
        var anime = CreateTestAnime("New Anime");

        var result = await _repository.CreateAsync(anime);
        await _context.SaveChangesAsync();

        result.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public async Task UpdateAsync_Should_Update_Timestamp()
    {
        var anime = CreateTestAnime("Original");
        _context.Animes.Add(anime);
        await _context.SaveChangesAsync();
        
        anime.Name = "Updated";
        var result = await _repository.UpdateAsync(anime);

        result.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        result.Name.Should().Be("Updated");
    }

    [Fact]
    public async Task DeleteAsync_Should_Soft_Delete_When_Exists()
    {
        var anime = CreateTestAnime("To Delete");
        _context.Animes.Add(anime);
        await _context.SaveChangesAsync();

        var result = await _repository.DeleteAsync(anime.Id);
        await _context.SaveChangesAsync();

        result.Should().BeTrue();
        var deletedAnime = await _context.Animes.IgnoreQueryFilters().FirstOrDefaultAsync(a => a.Id == anime.Id);
        deletedAnime!.DeletedAt.Should().NotBeNull();
    }

    [Fact]
    public async Task DeleteAsync_Should_Return_False_When_Not_Exists()
    {
        var result = await _repository.DeleteAsync(999);
        result.Should().BeFalse();
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_Paginated_Results()
    {
        var animes = new[] { CreateTestAnime("Anime 1"), CreateTestAnime("Anime 2"), CreateTestAnime("Anime 3") };
        _context.Animes.AddRange(animes);
        await _context.SaveChangesAsync();
        
        var result = await _repository.GetAllAsync(new PageParams { PageNumber = 1, PageSize = 2 });

        result.Items.Should().HaveCount(2);
        result.TotalCount.Should().Be(3);
    }

    [Fact]
    public async Task ExistsAsync_Should_Return_Correct_Boolean()
    {
        var anime = CreateTestAnime("Exists Test");
        _context.Animes.Add(anime);
        await _context.SaveChangesAsync();

        var exists = await _repository.ExistsAsync(anime.Id);
        var notExists = await _repository.ExistsAsync(999);

        exists.Should().BeTrue();
        notExists.Should().BeFalse();
    }

    private static Anime CreateTestAnime(string name) => new()
    {
        Name = name,
        Director = "Test Director",
        Summary = "Test Summary",
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
    };

    public void Dispose() => _context.Dispose();
}