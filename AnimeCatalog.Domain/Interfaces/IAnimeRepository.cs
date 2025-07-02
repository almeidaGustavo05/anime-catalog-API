using AnimeCatalog.Domain.Entities;
using AnimeCatalog.Domain.Pagination;

namespace AnimeCatalog.Domain.Interfaces;

public interface IAnimeRepository
{
    Task<PageList<Anime>> GetAllAsync(PageParams pageParams);
    Task<Anime?> GetByIdAsync(int id);
    Task<PageList<Anime>> SearchAsync(PageParams pageParams, int? id = null, string? name = null, string? director = null);
    Task<Anime?> GetByNameAsync(string name);
    Task<Anime> CreateAsync(Anime anime);
    Task<Anime> UpdateAsync(Anime anime);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}