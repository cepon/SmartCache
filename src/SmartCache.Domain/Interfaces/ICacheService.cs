namespace SmartCache.Domain.Interfaces;
public interface ICacheService
{
    Task<bool> IsEmailBreached(string email);
    Task AddBreachedEmail(string email);
}
