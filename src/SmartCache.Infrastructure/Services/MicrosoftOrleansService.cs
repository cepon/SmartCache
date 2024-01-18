using SmartCache.Domain.Helpers;
using SmartCache.Domain.Interfaces;
using SmartCache.Infrastructure.MicrosoftOrleans.Grains.Interfaces;

namespace SmartCache.Infrastructure.Services;
public class MicrosoftOrleansService : ICacheService
{
    private readonly IGrainFactory _grainFactory;
    public MicrosoftOrleansService(IGrainFactory grainFactory)
    {
        _grainFactory = grainFactory;
    }
    public async Task AddBreachedEmail(string email)
    {
        var emailDomain = EmailHelper.GetEmailDomain(email);
        await _grainFactory.GetGrain<IDomainGrain>(emailDomain).AddBreachedEmailAsync(email);
    }

    public async Task<bool> IsEmailBreached(string email)
    {
        var emailDomain = EmailHelper.GetEmailDomain(email);
        return await _grainFactory.GetGrain<IDomainGrain>(emailDomain).IsEmailBreachedAsync(email);
    }
}
