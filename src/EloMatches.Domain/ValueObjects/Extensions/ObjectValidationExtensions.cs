namespace EloMatches.Domain.ValueObjects.Extensions
{
    internal static class ObjectValidationExtensions
    {
        internal static object NotNullOrWhitespace(this string value)
        {
            return !string.IsNullOrWhiteSpace(value) ? value : null;
        }
    }
}