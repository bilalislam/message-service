namespace MessageService.Api
{
    public interface IBlockedUserRepository
    {
        Task AddAsync(BlockUser blockedUser);
        Task RemoveAsync(string from, string to);
    }
}