namespace MessageService.Api;

public interface IUserRepository
{
    
    Task<User> GetAsync(string email, CancellationToken cancellationToken);
    Task AddAsync(User user, CancellationToken cancellationToken);
    Task UpdateAsync(User user, CancellationToken cancellationToken);
}