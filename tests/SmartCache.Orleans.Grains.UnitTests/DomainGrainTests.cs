using NSubstitute;
using Orleans.Runtime;

namespace SmartCache.Orleans.Grains.UnitTests;
public class DomainGrainTests
{
    private readonly IPersistentState<DomainGrain.State> _persistentStateMock = Substitute.For<IPersistentState<DomainGrain.State>>();
    private readonly DomainGrain _subject;
    public DomainGrainTests()
    {

        _subject = new DomainGrain(_persistentStateMock);
    }

    [Fact]
    public async Task IsEmailBreachedAsync_ReturnsTrue_WhenEmailExists()
    {
        // Arrange
        var email = "test@test.com";
        var breachedEmails = new List<string>() { email };

        var mockState = new DomainGrain.State();
        mockState.Emails = breachedEmails;

        _persistentStateMock.State
            .Returns(mockState);

        // Act
        var result = await _subject.IsEmailBreachedAsync(email);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task IsEmailBreachedAsync_ReturnsFalse_WhenEmailDoesNotExist()
    {
        // Arrange
        var email = "test@test.com";
        var breachedEmails = new List<string>();

        var mockState = new DomainGrain.State();
        mockState.Emails = breachedEmails;

        _persistentStateMock.State
            .Returns(mockState);

        // Act
        var result = await _subject.IsEmailBreachedAsync(email);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task AddBreachedEmailAsync_AddsEmailAndWritesState()
    {
        // Arrange
        var email = "test@test.com";
        var breachedEmails = new List<string>();

        var mockState = new DomainGrain.State();
        mockState.Emails = breachedEmails;

        _persistentStateMock.State
            .Returns(mockState);

        // Act
        await _subject.AddBreachedEmailAsync(email);

        // Assert
        Assert.Contains(email, _persistentStateMock.State.Emails);
        await _persistentStateMock.Received(1).WriteStateAsync();
    }
}
