using MediatR;

namespace MessageService.Api;

public class SendCommand : IRequest<SendCommandResult>
{
    public string Sender { get; set; }
    public string Receiver { get; set; }
    public string ReceiverEmail { get; set; }
    public string Message { get; set; }
}