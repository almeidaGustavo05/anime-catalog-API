using System.Net;

namespace AnimeCatalog.Domain.Exceptions;

public class BusinessRuleException : BaseException
{
    public override HttpStatusCode StatusCode => HttpStatusCode.UnprocessableEntity;
    public override string ErrorCode => "BUSINESS_RULE_VIOLATION";
    public string RuleName { get; }

    public BusinessRuleException(string ruleName, string message) 
        : base(message)
    {
        RuleName = ruleName;
    }

    public override object Details => new { RuleName };
}