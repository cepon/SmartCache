using NSubstitute;
using SmartCache.Application.Services;
using SmartCache.Domain.Interfaces;

namespace SmartCache.Application.UnitTests.Services;
public class EmailBreachServiceTests
{
    private readonly ICacheService _mockCacheService;
    private readonly EmailBreachService _subject;

    public EmailBreachServiceTests()
    {
        _mockCacheService = Substitute.For<ICacheService>();
        _subject = new EmailBreachService(_mockCacheService);
    }

    [Fact]
    public async Task AddBreachedEmailAsync_CallsCacheService()
    {
        // Act
        await _subject.AddBreachedEmailAsync("test@example.com");

        // Assert
        await _mockCacheService.Received(1).AddBreachedEmailAsync("test@example.com");
    }

    [Fact]
    public async Task IsEmailBreachedAsync_CallsCacheService()
    {
        // Act
        await _subject.IsEmailBreachedAsync("test@example.com");

        // Assert
        await _mockCacheService.Received(1).IsEmailBreachedAsync("test@example.com");
    }
}
