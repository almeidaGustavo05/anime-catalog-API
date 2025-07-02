using System.Net;

namespace AnimeCatalog.Domain.Exceptions;

public class DatabaseException : BaseException
{
    public override HttpStatusCode StatusCode => HttpStatusCode.InternalServerError;
    public override string ErrorCode => "DATABASE_ERROR";
    public string Operation { get; }

    public DatabaseException(string operation, string message) 
        : base($"Erro na operação de banco de dados '{operation}': {message}")
    {
        Operation = operation;
    }

    public DatabaseException(string operation, string message, Exception innerException) 
        : base($"Erro na operação de banco de dados '{operation}': {message}", innerException)
    {
        Operation = operation;
    }

    public override object Details => new { Operation };
}