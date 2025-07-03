using AnimeCatalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnimeCatalog.Infrastructure.Data; 

public class AnimeCatalogDbContext : DbContext
{
    public AnimeCatalogDbContext(DbContextOptions<AnimeCatalogDbContext> options) : base(options) { }
    public DbSet<Anime> Animes { get; set; } 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Anime>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Director).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Summary).IsRequired().HasMaxLength(1000);
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt).IsRequired();
            entity.Property(e => e.DeletedAt);

            entity.HasQueryFilter(e => e.DeletedAt == null);
        });
    }
}