namespace MessageService.Api
{
    public interface IBlockedUserRepository
    {
        Task<BlockUser> GetAsync(string from, string to, CancellationToken cancellationToken);
        Task AddAsync(BlockUser blockedUser, CancellationToken cancellationToken);
        Task RemoveAsync(string from, string to, CancellationToken cancellationToken);
    }
}