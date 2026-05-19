using DigitalLibrary.Models;
using System.Text.Json;

namespace DigitalLibrary.DataAccess.BookRepository;

public class JsonBookRepository : IBookRepository
{
    private readonly string _filePath;
    private List<BookItem> _books;
    private readonly static JsonSerializerOptions _jsonOptions = new() 
    {
        WriteIndented = true
    };
    public JsonBookRepository(string filePath)
    {
        _filePath = filePath;
    } 
    public async Task<bool> AddBook(BookItem book)
    {
        var books = await GetAllBooks();

        if (books is not null)
        {
            if (book.Id == 0)
            {
                book.Id = books.Count > 0 ? books.Max(b => b.Id) + 1 : 1;
            }
            books.Add(book);
            return await SaveBookItemsAsync(books);
        }
        return false;
    }

    public async Task<bool> DeleteBook(int id)
    {
        var books = await LoadBookItemsAsync();
        var bookToDelete = books.FirstOrDefault(b => b?.Id == id);

        if (bookToDelete is not null)
        {
            books.Remove(bookToDelete);
            return await SaveBookItemsAsync(books);
        }
        return false;
    }

    public async Task<List<BookItem>?> GetAllBooks()
    {
        return await LoadBookItemsAsync();
    }

    public async Task<BookItem?> GetBookById(int id)
    {
        var books = await LoadBookItemsAsync();
        return books.FirstOrDefault(b => b?.Id == id);
    }

    public async Task<int> GetBooksCount()
    {
        return (await LoadBookItemsAsync()).Count;
    }

    public async Task<bool> UpdateBook(BookItem book)
    {

        if (book is null)
        {
            return false;
        }

        var books = await LoadBookItemsAsync();
        var bookToUpdate = books.FirstOrDefault(b => b?.Id == book.Id);

        if (bookToUpdate is not null)
        {
            int bookIndex = books.FindIndex(b => b?.Id == bookToUpdate.Id);

            books[bookIndex] = book;

            return await SaveBookItemsAsync(books);
        }
        return false;
    }

    private async Task<List<BookItem>> LoadBookItemsAsync()
    {
        if (!File.Exists(_filePath))
            return new List<BookItem>();

        string json = await File.ReadAllTextAsync(_filePath);

        return JsonSerializer.Deserialize<List<BookItem>>(json) ?? new List<BookItem>();
    }
    private async Task<bool> SaveBookItemsAsync(List<BookItem> books)
    {
        if (books is null)
        {
            return false;
        }

        string json = JsonSerializer.Serialize(books, _jsonOptions);

        await File.WriteAllTextAsync(_filePath, json);
        return true;
    }
}