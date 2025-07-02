using System.Net;

namespace AnimeCatalog.Domain.Exceptions;

public abstract class BaseException : Exception
{
    public abstract HttpStatusCode StatusCode { get; }
    public abstract string ErrorCode { get; }
    public virtual object? Details { get; }

    protected BaseException(string message) : base(message) { }
    
    protected BaseException(string message, Exception innerException) : base(message, innerException) { }
}