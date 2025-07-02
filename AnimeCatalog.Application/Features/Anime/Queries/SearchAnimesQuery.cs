using AnimeCatalog.Application.Features.Anime.DTOs;
using AnimeCatalog.Domain.Pagination;
using MediatR;

namespace AnimeCatalog.Application.Features.Anime.Queries;

public record SearchAnimesQuery(PageParams PageParams, int? Id = null, string? Name = null, string? Director = null) : IRequest<PageList<AnimeDto>>;