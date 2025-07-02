using AnimeCatalog.Application.Features.Anime.DTOs;
using AnimeCatalog.Domain.Pagination;
using MediatR;

namespace AnimeCatalog.Application.Features.Anime.Queries;

public record GetAllAnimesQuery(PageParams PageParams) : IRequest<PageList<AnimeDto>>;