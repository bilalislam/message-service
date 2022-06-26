namespace MessageService.Api;

public class SendCommandContract
{
    public string Receiver { get; set; }
    public string ReceiverEmail { get; set; }
    public string Message { get; set; }
}