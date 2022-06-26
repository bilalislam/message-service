namespace MessageService.Api;

public interface IUserRepository
{
    Task<User> GetByEmailAndNameAsync(string email, string name, CancellationToken cancellationToken);
    Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task AddAsync(User user, CancellationToken cancellationToken);
    Task UpdateAsync(User user, CancellationToken cancellationToken);
    Task<List<User>> ListAsync(CancellationToken cancellationToken);
}