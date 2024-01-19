namespace SmartCache.Domain.Interfaces;
public interface IEmailBreachService
{
    Task<bool> IsEmailBreachedAsync(string email);
    Task<bool> AddBreachedEmailAsync(string email);
}
