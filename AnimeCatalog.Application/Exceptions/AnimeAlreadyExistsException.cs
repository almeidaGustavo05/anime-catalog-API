using System.Net;
using AnimeCatalog.Domain.Exceptions;

namespace AnimeCatalog.Application.Exceptions;

public class AnimeAlreadyExistsException : BaseException
{
    public override HttpStatusCode StatusCode => HttpStatusCode.Conflict;
    public override string ErrorCode => "ANIME_ALREADY_EXISTS";
    public string AnimeName { get; }

    public AnimeAlreadyExistsException(string animeName) 
        : base($"Já existe um anime com o nome '{animeName}'.")
    {
        AnimeName = animeName;
    }

    public override object Details => new { AnimeName };
}