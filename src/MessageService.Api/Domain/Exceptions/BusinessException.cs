using System.Diagnostics.CodeAnalysis;

namespace MessageService.Api
{
    [ExcludeFromCodeCoverage]
    public class BusinessException : CoreException
    {
        public string Code { get; private set; }
        public BusinessException(string code, string message)
            : base(message, null)
        {
            Code = code;
        }
    }
}

