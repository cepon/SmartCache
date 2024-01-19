using SmartCache.Domain.Exceptions;
using System.Net;

namespace SmartCache.Domain.Helpers;
public static class EmailHelper
{
    public static string GetEmailDomain(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new CustomException(HttpStatusCode.BadRequest, "Email is not provided.");
        }

        var parts = email.Split('@');
        if (parts.Length != 2 || string.IsNullOrWhiteSpace(parts[1]))
        {
            throw new CustomException(HttpStatusCode.BadRequest, "Invalid email address.");
        }

        return parts[1];
    }
}
