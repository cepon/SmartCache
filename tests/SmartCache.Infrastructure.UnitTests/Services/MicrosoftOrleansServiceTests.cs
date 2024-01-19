using NSubstitute;
using SmartCache.Infrastructure.Services;
using SmartCache.Orleans.Grains.Interfaces;

namespace SmartCache.Infrastructure.UnitTests.Services;
public class MicrosoftOrleansServiceTests
{
    private readonly IClusterClient _mockClusterClient;
    private readonly MicrosoftOrleansService _subject;

    public MicrosoftOrleansServiceTests()
    {
        _mockClusterClient = Substitute.For<IClusterClient>();
        _subject = new MicrosoftOrleansService(_mockClusterClient);
    }

    [Fact]
    public async Task AddBreachedEmailAsync_InvokesAddBreachedEmailOnGrain_WhenCalled()
    {
        // Arrange
        var email = "test@test.com";
        var domainGrain = Substitute.For<IDomainGrain>();
        _mockClusterClient.GetGrain<IDomainGrain>(Arg.Any<string>()).Returns(domainGrain);

        // Act
        await _subject.AddBreachedEmailAsync(email);

        // Assert
        await domainGrain.Received(1).AddBreachedEmailAsync(email);
    }

    [Fact]
    public async Task IsEmailBreachedAsync_InvokesIsEmailBreachedOnGrain_WhenCalled()
    {
        // Arrange
        var email = "test@test.com";
        var domainGrain = Substitute.For<IDomainGrain>();
        _mockClusterClient.GetGrain<IDomainGrain>(Arg.Any<string>()).Returns(domainGrain);

        // Act
        await _subject.IsEmailBreachedAsync(email);

        // Assert
        await domainGrain.Received(1).IsEmailBreachedAsync(email);
    }
}
