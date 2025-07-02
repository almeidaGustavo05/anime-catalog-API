using System.Net;

namespace AnimeCatalog.Domain.Exceptions;

public class ValidationException : BaseException
{
    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    public override string ErrorCode => "VALIDATION_ERROR";
    public Dictionary<string, string[]> Errors { get; }

    public ValidationException(Dictionary<string, string[]> errors) 
        : base("Um ou mais erros de validação ocorreram.")
    {
        Errors = errors;
    }

    public ValidationException(string field, string error) 
        : base($"Erro de validação no campo '{field}': {error}")
    {
        Errors = new Dictionary<string, string[]>
        {
            { field, new[] { error } }
        };
    }

    public override object Details => Errors;
}