namespace MessageService.Api
{
    public interface IMessageRepository
    {
        Task AddAsync(Message message, CancellationToken cancellationToken);
        Task<List<Message>> GetUsersAsync(string from, CancellationToken cancellationToken);
        Task<List<Message>> GetHistoriesAsync(string from, string to, CancellationToken cancellationToken);
    }
}