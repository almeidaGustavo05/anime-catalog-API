using AnimeCatalog.Application.Features.Anime.DTOs;
using MediatR;

namespace AnimeCatalog.Application.Features.Anime.Queries;

public record GetAnimeByIdQuery(int Id) : IRequest<AnimeDto>;