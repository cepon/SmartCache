﻿namespace SmartCache.Domain.Interfaces;
public interface ICacheService
{
    Task<bool> IsEmailBreachedAsync(string email);
    Task<bool> AddBreachedEmailAsync(string email);
}
