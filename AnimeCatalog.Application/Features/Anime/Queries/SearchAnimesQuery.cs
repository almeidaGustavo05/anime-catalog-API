using AnimeCatalog.Application.Features.Anime.DTOs;
using MediatR;

namespace AnimeCatalog.Application.Features.Anime.Queries;

public record SearchAnimesQuery(int? Id = null, string? Name = null, string? Director = null) : IRequest<IEnumerable<AnimeDto>>;