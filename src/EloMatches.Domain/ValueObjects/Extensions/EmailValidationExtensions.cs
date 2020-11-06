using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace EloMatches.Domain.ValueObjects.Extensions
{
    public static class EmailValidationExtensions
    {
        //https://docs.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format
        public static object IsValidEmail(this string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                static string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return null;
            }
            catch (ArgumentException)
            {
                return null;
            }

            try
            {
                var isValid = Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
                return !isValid ? null : email;
            }
            catch (RegexMatchTimeoutException)
            {
                return null;
            }
        }
    }
}