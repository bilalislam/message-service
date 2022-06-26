using MediatR;
using Newtonsoft.Json;

namespace MessageService.Api;

public class SignUpCommand : IRequest<SignUpCommandResult>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}