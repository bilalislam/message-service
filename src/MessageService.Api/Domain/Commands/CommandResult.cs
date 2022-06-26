using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace MessageService.Api.Domain.Commands;

[ExcludeFromCodeCoverage]
public abstract class CommandResultBase
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public virtual ValidationState ValidateState { get; set; }

    public virtual IEnumerable<MessageContractDto> Messages { get; set; }

    public virtual bool Success { get; set; }

    public string ReturnPath { get; set; }
}