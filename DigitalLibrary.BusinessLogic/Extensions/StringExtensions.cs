namespace DigitalLibrary.BusinessLogic.Extensions
{
    public static class StringExtensions
    {
        public static bool ContainsValue(this string? source, string value)
        {
            return source?.Contains(value, StringComparison.OrdinalIgnoreCase) ?? false;
        }
    }
}
