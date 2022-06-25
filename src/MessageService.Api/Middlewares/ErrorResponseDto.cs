namespace MessageService.Api
{
    internal class ErrorResponseDto
    {
        public string Instance { get; set; }
        public List<MessageContractDto> Messages { get; set; }
        public string? ReturnPath { get; internal set; }
    }
}