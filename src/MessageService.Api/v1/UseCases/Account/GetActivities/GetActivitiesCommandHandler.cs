using MediatR;

namespace MessageService.Api;

public class GetActivitiesCommandHandler : IRequestHandler<GetActivitiesCommand, GetActivitiesCommandResult>
{
    private readonly IActivityRepository _activityRepository;

    public GetActivitiesCommandHandler(IActivityRepository activityRepository)
    {
        _activityRepository = activityRepository;
    }

    public async Task<GetActivitiesCommandResult> Handle(GetActivitiesCommand request,
        CancellationToken cancellationToken)
    {
        var getActivitiesByUser = await _activityRepository.FilterAsync(request.Email, cancellationToken);
        if (!getActivitiesByUser.Any())
        {
            return new GetActivitiesCommandResult()
            {
                Success = false,
                ReturnPath = "/acitivities",
                ValidateState = ValidationState.DoesNotExist
            };
        }

        return new GetActivitiesCommandResult()
        {
            Success = true,
            Activities = getActivitiesByUser.Select(x => new ActivityDto()
            {
                Email = x.Email,
                Event = x.Event,
                EventOn = x.EventOn.ToLocalTime()
            }).ToList(),
            ValidateState = ValidationState.Valid
        };
    }
}