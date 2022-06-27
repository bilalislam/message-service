using MassTransit;

namespace MessageService.Api
{
    public class ActivityConsumer : IConsumer<Activity>
    {
        private readonly ILogger<ActivityConsumer> _logger;
        private readonly IActivityRepository _activityRepository;


        public ActivityConsumer(ILogger<ActivityConsumer> logger, IActivityRepository activityRepository)
        {
            _logger = logger;
            _activityRepository = activityRepository;
        }

        public async Task Consume(ConsumeContext<Activity> context)
        {
            await _activityRepository.AddAsync(context.Message, CancellationToken.None);
            _logger.LogInformation(
                $"Activity log received from : {context.Message.Email} to : {context.Message.Event}");
        }
    }
}