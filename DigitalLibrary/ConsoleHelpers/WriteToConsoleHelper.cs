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
        public static void WriteClass<T>() where T : class
        {
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute<DescriptionAttribute>();

                var description = attribute?.Description ?? property.Name;

                Console.WriteLine($"{description}");
            }
        }
        public static void WriteClass<T>(Type filterType) where T : class
        {
            var properties = typeof(T)
                .GetProperties()
                .Where(p => p.PropertyType == filterType)
                .ToList();

            for (int i = 0; i < properties.Count; i++) 
            {
                var attribute = properties[i].GetCustomAttribute<DescriptionAttribute>();
                if (attribute is not null)
                {
                    Console.WriteLine($"{i + 1}. {attribute.Description}");
                    continue;
                }
                Console.WriteLine($"{i + 1}. {properties[i].Name}");
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
