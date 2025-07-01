using AnimeCatalog.Domain.Entities;

namespace AnimeCatalog.Domain.Interfaces;

public interface IAnimeRepository
{
    Task<IEnumerable<Anime>> GetAllAsync();
    Task<Anime?> GetByIdAsync(int id);
    Task<IEnumerable<Anime>> SearchAsync(int? id = null, string? name = null, string? director = null);
    Task<Anime> CreateAsync(Anime anime);
    Task<Anime> UpdateAsync(Anime anime);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}