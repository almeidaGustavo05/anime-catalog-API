using AnimeCatalog.Application.Features.Anime.DTOs;
using MediatR;

namespace AnimeCatalog.Application.Features.Anime.Commands.UpdateAnime;

public record UpdateAnimeCommand(int Id, UpdateAnimeDto AnimeDto) : IRequest<AnimeDto>;