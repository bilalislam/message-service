using MediatR;

namespace MessageService.Api
{
    public class SignInCommand : IRequest<SignInCommandResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class ActivityCommand
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Event { get; set; }
        public DateTime EventOn { get; set; }
    }
}