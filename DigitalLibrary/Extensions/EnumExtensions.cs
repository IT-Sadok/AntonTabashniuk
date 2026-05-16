using System.ComponentModel;
using System.Reflection;

namespace DigitalLibrary.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            if (enumValue is null)
                return string.Empty;

            var field = enumValue.GetType().GetField(enumValue.ToString());

            if (field is null)
                return enumValue.ToString();

            var attribute = field.GetCustomAttribute<DescriptionAttribute>();

            return attribute?.Description ?? enumValue.ToString();
        }
    }
}
