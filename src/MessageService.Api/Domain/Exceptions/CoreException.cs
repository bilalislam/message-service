using System.Diagnostics.CodeAnalysis;

namespace MessageService.Api
{
    [ExcludeFromCodeCoverage]
    public class CoreException : Exception
    {
        public CoreException(string message)
            : this(message, null)
        {
        }

        public CoreException(string message, Exception innerEx)
            : base(message, innerEx)
        {
        }
    }
}
