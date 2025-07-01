namespace AnimeCatalog.Application.Features.Anime.DTOs;

public class AnimeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Director { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}