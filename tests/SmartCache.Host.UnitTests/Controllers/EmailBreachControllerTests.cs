using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using SmartCache.Domain.Interfaces;
using SmartCache.Host.Controllers;

namespace SmartCache.Host.UnitTests.Controllers;
public class EmailBreachControllerTests
{
    private readonly IEmailBreachService _mockEmailBreachService;
    private readonly EmailBreachController _subject;

    public EmailBreachControllerTests()
    {
        _mockEmailBreachService = Substitute.For<IEmailBreachService>();
        _subject = new EmailBreachController(_mockEmailBreachService);
    }

    [Fact]
    public async Task IsEmailBreachedAsync_ReturnsO_WhenEmailBreachedk()
    {
        // Arrange
        _mockEmailBreachService.IsEmailBreachedAsync(Arg.Any<string>()).Returns(Task.FromResult(true));

        // Act
        var result = await _subject.IsEmailBreachedAsync("test@test.com");

        // Assert
        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task IsEmailBreachedAsync_ReturnsNotFound_WhenEmailNotBreached()
    {
        // Arrange
        _mockEmailBreachService.IsEmailBreachedAsync(Arg.Any<string>()).Returns(Task.FromResult(false));

        // Act
        var result = await _subject.IsEmailBreachedAsync("test@test.com");

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task AddBreachedEmailAsync_ReturnsCreatedResult_WhenTrue()
    {
        // Arrange
        _mockEmailBreachService.AddBreachedEmailAsync(Arg.Any<string>()).Returns(Task.FromResult(true));

        // Act
        var result = await _subject.AddBreachedEmailAsync("test@test.com");

        // Assert
        var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
        Assert.Equal(201, statusCodeResult.StatusCode);
    }

    [Fact]
    public async Task AddBreachedEmailAsync_ReturnsConflictResult_WhenFalse()
    {
        // Arrange
        _mockEmailBreachService.AddBreachedEmailAsync(Arg.Any<string>()).Returns(Task.FromResult(false));

        // Act
        var result = await _subject.AddBreachedEmailAsync("test@test.com");

        // Assert
        Assert.IsType<ConflictResult>(result);
    }
}
