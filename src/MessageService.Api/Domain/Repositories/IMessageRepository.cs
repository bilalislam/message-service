namespace MessageService.Api
{
    public interface IMessageRepository
    {
        Task AddAsync(Message message);
    }
}