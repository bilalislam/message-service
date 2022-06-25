namespace MessageService.Api
{
    public class MessageContractDto
    {
        public string Code { get; internal set; }
        public string Title { get; internal set; }
        public string Content { get; internal set; }
    }
}