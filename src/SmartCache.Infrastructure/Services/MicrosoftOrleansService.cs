using SmartCache.Domain.Helpers;
using SmartCache.Domain.Interfaces;
using SmartCache.Orleans.Grains.Interfaces;

namespace SmartCache.Infrastructure.Services;
public class MicrosoftOrleansService : ICacheService
{
    private readonly IClusterClient _clusterClient;
    public MicrosoftOrleansService(IClusterClient clusterClient)
    {
        _clusterClient = clusterClient;
    }
    public async Task<bool> AddBreachedEmailAsync(string email)
    {
        var emailDomain = EmailHelper.GetEmailDomain(email);
        return await _clusterClient.GetGrain<IDomainGrain>(emailDomain).AddBreachedEmailAsync(email);
    }

    public async Task<bool> IsEmailBreachedAsync(string email)
    {
        var emailDomain = EmailHelper.GetEmailDomain(email);
        return await _clusterClient.GetGrain<IDomainGrain>(emailDomain).IsEmailBreachedAsync(email);
    }
}
