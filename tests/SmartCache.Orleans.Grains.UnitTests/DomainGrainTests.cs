using NSubstitute;
using Orleans.Runtime;
using Orleans.Timers;

namespace SmartCache.Orleans.Grains.UnitTests;
public class DomainGrainTests
{

    private readonly IPersistentState<DomainGrain.State> _mockPersistentState = Substitute.For<IPersistentState<DomainGrain.State>>();
    private readonly ITimerRegistry _mockTimerRegistry = Substitute.For<ITimerRegistry>();
    private readonly IGrainContext _mockGrainContext = Substitute.For<IGrainContext>();
    private readonly DomainGrain _subject;
    public DomainGrainTests()
    {

        _subject = new DomainGrain(_mockPersistentState, _mockTimerRegistry, _mockGrainContext);
    }

    [Fact]
    public async Task IsEmailBreachedAsync_ReturnsTrue_WhenEmailExists()
    {
        // Arrange
        var email = "test@test.com";
        var breachedEmails = new List<string>() { email };

        var mockState = new DomainGrain.State();
        mockState.Emails = breachedEmails;

        _mockPersistentState.State
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

        _mockPersistentState.State
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

        _mockPersistentState.State
            .Returns(mockState);

        // Act
        var result = await _subject.AddBreachedEmailAsync(email);

        // Assert
        Assert.True(result);
        Assert.Contains(email, _mockPersistentState.State.Emails);
    }
}
