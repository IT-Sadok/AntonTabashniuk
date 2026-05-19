using DigitalLibrary.Models;

namespace DigitalLibrary.ConsoleHelpers;

public static class WriteToConsoleHelper
{
    public static void WriteBook(BookItem book)
    {
        Console.WriteLine($"ID: {book.Id}");
        Console.WriteLine($"Title: {book.Title}");
        Console.WriteLine($"Author: {book.Author}");
        Console.WriteLine($"Genre: {book.Genre}");
        Console.WriteLine($"Status: {book.Status}");
        Console.WriteLine($"Language: {book.Language}");
        Console.WriteLine($"Adding date: {book.AddingDate}");
        Console.WriteLine($"Release date: {book.ReleaseDate}");
        Console.WriteLine($"Description: {book.Description}\n");
    }

    public static void WriteEnum<T>() where T : Enum
    {
        var enumValues = Enum.GetValues(typeof(T)).Cast<T>();
     
        foreach (var value in enumValues)
        {
            Console.WriteLine($"{Convert.ToInt32(value)}. {value}");
        }
    }
}
