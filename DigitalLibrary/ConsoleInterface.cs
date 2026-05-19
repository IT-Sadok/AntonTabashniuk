using DigitalLibrary.BusinessLogic;
using DigitalLibrary.ConsoleHelpers;
using DigitalLibrary.Models;
using DigitalLibrary.Models.Enums;
using System.ComponentModel;
using System.Reflection;

namespace DigitalLibrary;

public class ConsoleInterface
{
    private readonly ILibraryService _libraryService;
    public ConsoleInterface(ILibraryService libraryService)
    {
        _libraryService = libraryService ?? throw new ArgumentNullException(nameof(libraryService));
    }
    #region Methods
    public async Task StartProgram()
    {
        WelcomingMessage();
        await HandleCommands();
    }
    public void WelcomingMessage()
    {
        Console.WriteLine("Welcome to the Digital Library!");
        Console.WriteLine("This is a simple console application to manage your book collection.");
        Console.WriteLine("Please use the provided commands to interact with the library.");

        Console.WriteLine("\nPlease press Enter to start");
        Console.ReadLine();
    }
    public async Task HandleCommands()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Digital Library ===\n");
            Console.WriteLine("1. Show all books");
            Console.WriteLine("2. Add book");
            Console.WriteLine("3. Delete book");
            Console.WriteLine("4. Find book");
            Console.WriteLine("5. Change book status");
            Console.WriteLine("0. Exit");
            Console.Write("\nChoose you action: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await ShowAllBooks();
                    break;
                case "2":
                    await AddBook();
                    break;
                case "3":
                    await DeleteBook();
                    break;
                case "4":
                    await FindBook();
                    break;
                case "5":
                    await ChangeBookStatus();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Unkown command. Please Enter...");
                    Console.ReadLine();
                    break;
            }
        }
    }

    #region Methods for handling commands
    private async Task ShowAllBooks()
    {
        var books = await _libraryService.GetAllBooks();

        Console.Clear();
        Console.WriteLine("=== All Books in the Library ===\n");
        if (books == null || books.Count == 0)
        {
            Console.WriteLine("No books found.");
        }
        else
        {
            foreach (var book in books)
            {
                WriteToConsoleHelper.WriteClassPropertiesValue<BookItem>(book);
            }
        }
        Console.WriteLine("Press Enter to return to the main menu...");
        Console.ReadLine();
    }
    private async Task AddBook()
    {
        Console.WriteLine("Enter the title of the book: ");
        var title = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Book name can not be empty");
            return;
        }

        Console.WriteLine("\nEnter book author: ");
        var author = Console.ReadLine();

        Console.WriteLine("\nEnter book discription: ");
        var discription = Console.ReadLine();


        Console.WriteLine();
        WriteToConsoleHelper.WriteEnum<BookGenre>();
        Console.Write("\nChoose book genre: ");
        var genre = Console.ReadLine();


        Console.WriteLine();
        WriteToConsoleHelper.WriteEnum<BookLanguage>();
        Console.Write("\nChoose book language: ");
        var language = Console.ReadLine();


        Console.WriteLine();
        WriteToConsoleHelper.WriteEnum<BookStatus>();
        Console.Write("\nChoose book status: ");
        var status = Console.ReadLine();

        DateTime? releaseDate = ReadDate("Enter book release date (dd-MM-yyyy): ");

        var book = new Models.BookItem
        {
            Id = 0,
            Title = title,
            Author = author ?? string.Empty,
            Description = discription ?? string.Empty,
            Genre = Enum.TryParse<BookGenre>(genre, out var parsedGenre)
                  ? parsedGenre
                  : BookGenre.Unknown,
            Language = Enum.TryParse<BookLanguage>(language, out var parsedLanguage)
                  ? parsedLanguage
                  : BookLanguage.Unknown,
            Status = Enum.TryParse<BookStatus>(status, out var parsedStatus)
                  ? parsedStatus
                  : BookStatus.Unknown,
            ReleaseDate = releaseDate,
            AddingDate = DateTime.UtcNow
        };

        try
        {
            await _libraryService.AddBook(book);
            Console.WriteLine("Successfully added book " + $"{book.Title}");
            Console.WriteLine("Press Enter to return to the main menu...");
            Console.ReadLine();
        }
        catch (Exception)
        {
            throw;
        }
    }
    private async Task DeleteBook()
    {
        Console.Write("\nEnter book ID to delete: ");
        var bookId = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(bookId))
        {
            Console.WriteLine("Book ID can not be empty");
            return;
        }
        var isSuccess = await _libraryService.DeleteBook(int.Parse(bookId));

        if (isSuccess)
        {
            Console.WriteLine("Successfully deleted book with ID " + $"{bookId}");
        }
        else
        {
            Console.WriteLine("Failed to delete book with ID " + $"{bookId}");
        }
        Console.WriteLine("Press Enter to return to the main menu...");
        Console.ReadLine();

    }
    private async Task FindBook()
    {
        Console.Clear();
        var properties = typeof(BookItem)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanRead && p.PropertyType == typeof(string))
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
        Console.WriteLine($"0. Go back");

        Console.Write("\nSelect a search option: ");
        var input = Console.ReadLine();


        if (!int.TryParse(input, out int choice) || choice < 1 || choice > properties.Count + 1)
        {
            Console.WriteLine("❌ Choise incorrect!");
            return;
        }

        if (choice == 0) return;

        var selectedProperty = properties[choice - 1];

        Console.Write($"\nEnter search value for '{selectedProperty.Name}': ");
        var searchValue = Console.ReadLine()?.Trim();

        if (string.IsNullOrEmpty(searchValue))
        {
            Console.WriteLine("❌ Value can not be null.");
            return;
        }

        await SearchByPropertyAsync(selectedProperty, searchValue);
    }
    private async Task ChangeBookStatus()
    {
        Console.Clear();
        Console.Write("Enter book ID which status to change: ");

        var bookId = Console.ReadLine();
        if (string.IsNullOrEmpty(bookId)) return;

        var book = await _libraryService.GetBookById(int.Parse(bookId));

        if (book is null)
        {
            Console.Write($"No book with id {bookId} was found.");
            Console.ReadKey();
            await ChangeBookStatus();
            return;
        }


    ChangeBookStatus:
        Console.Clear();
        Console.WriteLine($"Found book '{book.Title}' with status '{book.Status}'.\n");

        WriteToConsoleHelper.WriteEnum<BookStatus>();
        Console.Write("\nSelect a new book status: ");
        var input = Console.ReadLine();

        if (!Enum.TryParse<BookStatus>(input, out var newStatus))
        {
            Console.WriteLine("❌ Incorrect book status!");
            Console.ReadKey();
            goto ChangeBookStatus;
        }

        book.Status = newStatus;
        try
        {
            await _libraryService.UpdateBook(book);
            Console.WriteLine($"Successfully updated book status to '{book.Status}'.");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Some error ocure while updating book status: " + ex.Message);
            Console.ReadKey();
            return;
        }
    }
    #endregion

    #region Helpers method
    private async Task SearchByPropertyAsync(PropertyInfo property, string searchValue)
    {
        try
        {
            var results = await _libraryService.SearchByProperty(property, searchValue);

            if (results is null || results.Count == 0)
            {
                Console.WriteLine($"\nNo book found with '{property.Name}' as {searchValue}.");
            }
            else
            {
                foreach (var book in results)
                {
                    Console.WriteLine($"\nFound book {book.Title}:");
                    WriteToConsoleHelper.WriteClassPropertiesValue<BookItem>(book);
                }
            }
            Console.Write("\nPress Enter to return: ");
            Console.ReadLine();

            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Search error: {ex.Message}");
        }
    }
    private DateTime? ReadDate(string prompt)
    {
        Console.Write(prompt);
        string? input = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(input))
            return null;

        if (DateTime.TryParseExact(input, "dd-MM-yyyy",
                                   System.Globalization.CultureInfo.InvariantCulture,
                                   System.Globalization.DateTimeStyles.None,
                                   out DateTime parsedDate))
        {
            return parsedDate;
        }

        Console.WriteLine("❌ Incorrect date format! Expected: dd-MM-yyyy (example: 15-03-2023)");
        Console.Write("\nEnter date again, or press Enter to continue without date: ");
        return ReadDate(prompt);
    }
    #endregion
    #endregion
}
