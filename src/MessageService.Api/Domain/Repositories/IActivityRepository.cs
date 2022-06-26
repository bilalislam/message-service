namespace MessageService.Api
{
    public interface IActivityRepository
    {
        Task AddAsync(Activity activity, CancellationToken cancellationToken);
        Task<List<Activity>> FilterAsync(string email, CancellationToken cancellationToken);
    }
}