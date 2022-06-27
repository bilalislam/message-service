using MassTransit;
using MediatR;
using MongoDB.Bson;

namespace MessageService.Api;

public class SignInCommandHandler : IRequestHandler<SignInCommand, SignInCommandResult>
{
    private readonly IUserRepository _repository;
    private readonly ITokenProxy _tokenProxy;
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public SignInCommandHandler(IUserRepository repository, ITokenProxy tokenProxy,
        ISendEndpointProvider sendEndpointProvider)
    {
        _repository = repository;
        _tokenProxy = tokenProxy;
        _sendEndpointProvider = sendEndpointProvider;
    }


    public async Task<SignInCommandResult> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByEmailAsync(request.Email, cancellationToken);
        if (user != null)
        {
            if (user.Password == request.Password)
            {
                user.CreateToken(_tokenProxy);
                await _repository.UpdateAsync(user, cancellationToken);
                await CreateActivityLog(user.Email, "login is success");

                return new SignInCommandResult()
                {
                    Token = user.Token,
                    Success = true,
                    ReturnPath = "/sign-in",
                    ValidateState = ValidationState.Valid
                };
            }
            else
            {
                await CreateActivityLog(user.Email, "login is failure");
                return new SignInCommandResult()
                {
                    Success = false,
                    ReturnPath = "/sign-in",
                    ValidateState = ValidationState.UnProcessable
                };
            }
        }
        else
        {
            return new SignInCommandResult()
            {
                Success = false,
                ReturnPath = "/sign-in",
                ValidateState = ValidationState.DoesNotExist
            };
        }
    }

    private async Task CreateActivityLog(string email, string message)
    {
        var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:activity"));
        await endpoint.Send(Activity.Load(new ActivityCommand()
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Email = email,
            Event = message,
            EventOn = DateTime.Now
        }));
    }
}