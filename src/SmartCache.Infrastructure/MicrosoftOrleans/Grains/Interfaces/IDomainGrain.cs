namespace SmartCache.Infrastructure.MicrosoftOrleans.Grains.Interfaces;
public interface IDomainGrain : IGrainWithStringKey
{
    Task<bool> IsEmailBreachedAsync(string email);
    Task AddBreachedEmailAsync(string email);
}
