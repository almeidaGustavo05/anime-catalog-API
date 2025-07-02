using AnimeCatalog.Domain.Entities;
using AnimeCatalog.Domain.Interfaces;
using AnimeCatalog.Domain.Pagination;
using AnimeCatalog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AnimeCatalog.Infrastructure.Repository;

public class AnimeRepository : IAnimeRepository
{
    private readonly AnimeCatalogDbContext _context;

    public AnimeRepository(AnimeCatalogDbContext context)
    {
        _context = context;
    }

    public async Task<PageList<Anime>> GetAllAsync(PageParams pageParams)
    {
        var query = _context.Animes
            .AsNoTracking()
            .Where(a => a.DeletedAt == null)
            .OrderBy(a => a.Id);

        var totalCount = await query.CountAsync();
        
        var items = await query
            .Skip((pageParams.PageNumber - 1) * pageParams.PageSize)
            .Take(pageParams.PageSize)
            .ToListAsync();

        return new PageList<Anime>(items, totalCount, pageParams.PageNumber, pageParams.PageSize);
    }

    public async Task<PageList<Anime>> SearchAsync(PageParams pageParams, int? id = null, string? name = null, string? director = null)
    {
        var query = _context.Animes
            .AsNoTracking()
            .Where(a => a.DeletedAt == null);

        if (id.HasValue)
            query = query.Where(a => a.Id == id);

        if (!string.IsNullOrWhiteSpace(name))
            query = query.Where(a => a.Name.Contains(name));

        if (!string.IsNullOrWhiteSpace(director))
            query = query.Where(a => a.Director.Contains(director));

        query = query.OrderBy(a => a.Id);

        var totalCount = await query.CountAsync();
        
        var items = await query
            .Skip((pageParams.PageNumber - 1) * pageParams.PageSize)
            .Take(pageParams.PageSize)
            .ToListAsync();

        return new PageList<Anime>(items, totalCount, pageParams.PageNumber, pageParams.PageSize);
    }

    public async Task<Anime?> GetByIdAsync(int id)
    {
        return await _context.Animes
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id && a.DeletedAt == null);
    }

    public async Task<Anime?> GetByNameAsync(string name)
    {
        return await _context.Animes
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Name == name && a.DeletedAt == null);
    }

    public async Task<Anime> CreateAsync(Anime anime)
    {
        anime.CreatedAt = DateTime.UtcNow;
        anime.UpdatedAt = DateTime.UtcNow;
        await _context.Animes.AddAsync(anime);
        return anime;
    }

    public async Task<Anime> UpdateAsync(Anime anime)
    {
        anime.UpdatedAt = DateTime.UtcNow;
        _context.Entry(anime).State = EntityState.Modified;
        return await Task.FromResult(anime);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var anime = await _context.Animes.FindAsync(id);
        if (anime == null || anime.DeletedAt != null) return false;

        anime.DeletedAt = DateTime.UtcNow;
        anime.UpdatedAt = DateTime.UtcNow;
        _context.Entry(anime).State = EntityState.Modified;
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Animes
            .AsNoTracking()
            .AnyAsync(a => a.Id == id && a.DeletedAt == null);
    }
}