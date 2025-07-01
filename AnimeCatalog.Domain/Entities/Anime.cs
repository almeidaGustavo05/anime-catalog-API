namespace AnimeCatalog.Domain.Entities;

public class Anime : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Director { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
}