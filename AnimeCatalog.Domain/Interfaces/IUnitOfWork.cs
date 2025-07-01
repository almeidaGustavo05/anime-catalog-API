namespace AnimeCatalog.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IAnimeRepository AnimeRepository { get; }
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}