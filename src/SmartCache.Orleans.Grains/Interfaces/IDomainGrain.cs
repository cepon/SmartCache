namespace SmartCache.Orleans.Grains.Interfaces;
public interface IDomainGrain : IGrainWithStringKey
{
    Task<bool> IsEmailBreachedAsync(string email);
    Task<bool> AddBreachedEmailAsync(string email);
}
