using System.ComponentModel;
using System.Reflection;

namespace DigitalLibrary.ConsoleHelpers
{
    public static class WriteToConsoleHelper
    {
        public static void WriteEnum<T>() where T : Enum
        {
            var enumValues = Enum.GetValues(typeof(T)).Cast<T>();
         
            foreach (var value in enumValues)
            {
                Console.WriteLine($"{Convert.ToInt32(value)}. {value}");
            }
        }
        public static void WriteClassPropertiesValue<T>(T @class) where T : class
        {
            var properties = @class.GetType().GetProperties();

            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute<DescriptionAttribute>();

                var description = attribute?.Description ?? property.Name;

                var value = property.GetValue(@class);

                Console.WriteLine($"{description}: {value}");
            }
        }
    }

}
