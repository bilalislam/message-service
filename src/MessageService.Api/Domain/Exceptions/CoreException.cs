namespace MessageService.Api
{
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
