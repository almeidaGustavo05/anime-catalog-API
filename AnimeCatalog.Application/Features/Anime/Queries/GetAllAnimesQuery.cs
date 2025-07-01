using AnimeCatalog.Application.DTOs;
using MediatR;

namespace AnimeCatalog.Application.Features.Anime.Queries;

public record GetAllAnimesQuery : IRequest<IEnumerable<AnimeDto>>;