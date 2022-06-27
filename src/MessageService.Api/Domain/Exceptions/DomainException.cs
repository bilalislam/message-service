using System.Diagnostics.CodeAnalysis;

namespace MessageService.Api
{

    [ExcludeFromCodeCoverage]
    public class DomainException : CoreException
    {
        public string Code { get; private set; }
        public DomainException(string code, string message)
            : base(message, null)
        {
            Code = code;
        }
    }
}
