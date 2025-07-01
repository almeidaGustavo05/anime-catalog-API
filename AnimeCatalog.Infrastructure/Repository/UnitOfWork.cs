using AnimeCatalog.Domain.Interfaces;
using AnimeCatalog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AnimeCatalog.Infrastructure.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly AnimeCatalogDbContext _context;
    private IAnimeRepository _animeRepository;

    public UnitOfWork(AnimeCatalogDbContext context)
    {
        _context = context;
        _animeRepository = new AnimeRepository(context);
    }

    public IAnimeRepository AnimeRepository
    {
        get { return _animeRepository ??= new AnimeRepository(_context); }
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await _context.Database.CommitTransactionAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await _context.Database.RollbackTransactionAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}