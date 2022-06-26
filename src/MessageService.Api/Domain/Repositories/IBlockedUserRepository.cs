namespace MessageService.Api
{
    public interface IBlockedUserRepository
    {
        Task<BlockUser> GetAsync(string from, string to);
        Task AddAsync(BlockUser blockedUser);
        Task RemoveAsync(string from, string to);
    }
}