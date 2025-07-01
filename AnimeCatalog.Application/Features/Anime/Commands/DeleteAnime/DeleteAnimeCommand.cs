using MediatR;

namespace AnimeCatalog.Application.Features.Anime.Commands.DeleteAnime;

public record DeleteAnimeCommand(int Id) : IRequest<bool>;