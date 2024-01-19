using SmartCache.Domain.Interfaces;

namespace SmartCache.Application.Services;
public class EmailBreachService : IEmailBreachService
{
    private readonly ICacheService _cacheService;

    public EmailBreachService(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task<bool> AddBreachedEmailAsync(string email)
    {
        return await _cacheService.AddBreachedEmailAsync(email);
    }

    public async Task<bool> IsEmailBreachedAsync(string email)
    {
        return await _cacheService.IsEmailBreachedAsync(email);
    }
}
