using System.Net;
using AnimeCatalog.Domain.Exceptions;

namespace AnimeCatalog.Application.Exceptions;

public class AnimeNotFoundException : BaseException
{
    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
    public override string ErrorCode => "ANIME_NOT_FOUND";
    public int AnimeId { get; }

    public AnimeNotFoundException(int animeId) 
        : base($"Anime com ID {animeId} não foi encontrado.")
    {
        AnimeId = animeId;
    }

    public override object Details => new { AnimeId };
}