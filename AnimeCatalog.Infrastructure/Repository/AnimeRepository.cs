using AnimeCatalog.Domain.Entities;
using AnimeCatalog.Domain.Interfaces;
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

    public async Task<Anime> CreateAsync(Anime anime)
    {
        await _context.Animes.AddAsync(anime);
        return anime;
    }

    public async Task<Anime> UpdateAsync(Anime anime)
    {
        _context.Entry(anime).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return anime;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var anime = await _context.Animes.FindAsync(id);
        if (anime == null) return false;

        _context.Animes.Remove(anime);
        return true;
    }

    public async Task<Anime?> GetByIdAsync(int id)
    {
        return await _context.Animes.FindAsync(id);
    }

    public async Task<IEnumerable<Anime>> GetAllAsync()
    {
        return await _context.Animes.ToListAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Animes.AnyAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Anime>> SearchAsync(int? id = null, string? name = null, string? director = null)
    {
        var query = _context.Animes.AsQueryable();

        if (id.HasValue)
            query = query.Where(a => a.Id == id);

        if (!string.IsNullOrWhiteSpace(name))
            query = query.Where(a => a.Name.Contains(name));

        if (!string.IsNullOrWhiteSpace(director))
            query = query.Where(a => a.Director.Contains(director));

        return await query.ToListAsync();
    }
}