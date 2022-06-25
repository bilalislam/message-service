using System;

namespace MessageService.Api
{
    public interface IActivityRepository
    {
        Task AddAsync(Activity activity);
    }
}