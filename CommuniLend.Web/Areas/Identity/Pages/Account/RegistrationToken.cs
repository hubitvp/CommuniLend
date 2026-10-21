using System.Security.Cryptography;
using System.Text.Json;
using Microsoft.AspNetCore.DataProtection;

namespace CommuniLend.Web.Areas.Identity.Pages.Account;

/// <summary>Credentials entered on the first registration step, held until the profile step is completed.</summary>
public record PendingRegistration(string Email, string Password);

/// <summary>
/// Encrypts the pending registration into a short-lived, tamper-proof token so the
/// password is never stored in the database or sent to the browser in clear text.
/// </summary>
public static class RegistrationTokenProtector
{
    private const string Purpose = "CommuniLend.Registration.v1";
    private static readonly TimeSpan Lifetime = TimeSpan.FromMinutes(30);

    public static string Protect(IDataProtectionProvider provider, PendingRegistration pending)
    {
        var protector = provider.CreateProtector(Purpose).ToTimeLimitedDataProtector();
        return protector.Protect(JsonSerializer.Serialize(pending), Lifetime);
    }

    public static PendingRegistration? Unprotect(IDataProtectionProvider provider, string? token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            return null;
        }

        try
        {
            var protector = provider.CreateProtector(Purpose).ToTimeLimitedDataProtector();
            return JsonSerializer.Deserialize<PendingRegistration>(protector.Unprotect(token));
        }
        catch (CryptographicException)
        {
            // Tampered, malformed or expired token.
            return null;
        }
    }
}
