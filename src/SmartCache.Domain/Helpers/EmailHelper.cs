namespace SmartCache.Domain.Helpers;
public static class EmailHelper
{
    public static string GetEmailDomain(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            // TODO: Throw custom exception
        }

        var parts = email.Split('@');
        if (parts.Length != 2 || string.IsNullOrWhiteSpace(parts[1]))
        {
            // TODO: Throw custom exception
        }

        return parts[1];
    }
}
