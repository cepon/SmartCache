using SmartCache.Domain.Exceptions;
using SmartCache.Domain.Helpers;
using System.Net;

namespace SmartCache.Domain.UnitTests.Helpers;
public class EmailHelperTests
{
    [Fact]
    public void GetEmailDomain_ReturnsDomain_WhenValidEmail()
    {
        // Arrange
        var email = "test@test.com";

        // Act
        var domain = EmailHelper.GetEmailDomain(email);

        // Assert
        Assert.Equal("test.com", domain);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void GetEmailDomain_ThrowsCustomException_WhenEmailNotProvided(string email)
    {
        // Act
        var exception = Assert.Throws<CustomException>(() => EmailHelper.GetEmailDomain(email));

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
        Assert.Equal("Email is not provided.", exception.Message);
    }

    [Theory]
    [InlineData("invalidemail")]
    [InlineData("invalid@")]
    public void GetEmailDomain_ThrowsCustomException_WhenInvalidEmail(string email)
    {
        // Act
        var exception = Assert.Throws<CustomException>(() => EmailHelper.GetEmailDomain(email));

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
        Assert.Equal("Invalid email address.", exception.Message);
    }
}
