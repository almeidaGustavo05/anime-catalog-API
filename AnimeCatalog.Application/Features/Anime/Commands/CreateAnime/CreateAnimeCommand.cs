using AnimeCatalog.Application.DTOs;
using MediatR;

namespace AnimeCatalog.Application.Features.Anime.Commands.CreateAnime;

public record CreateAnimeCommand(CreateAnimeDto AnimeDto) : IRequest<AnimeDto>;