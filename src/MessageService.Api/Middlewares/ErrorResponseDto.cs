using System.Diagnostics.CodeAnalysis;

namespace MessageService.Api
{
    [ExcludeFromCodeCoverage]
    internal class ErrorResponseDto
    {
        public string Instance { get; set; }
        public List<MessageContractDto> Messages { get; set; }
        public string? ReturnPath { get; internal set; }
    }
}