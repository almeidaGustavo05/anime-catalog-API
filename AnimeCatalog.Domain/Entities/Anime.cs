namespace AnimeCatalog.Domain.Entities;

public class Anime
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Director { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}