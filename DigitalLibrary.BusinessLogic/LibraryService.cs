using DigitalLibrary.DataAccess.BookRepository;
using DigitalLibrary.Models;
using DigitalLibrary.Models.Enums;
using System.Reflection;

namespace DigitalLibrary.BusinessLogic;

public class LibraryService
{
    private readonly IBookRepository _repository;

    public LibraryService(IBookRepository repository) 
    {
        _repository = repository;
    }

    public async Task<bool> AddBook(BookItem book)
    {
        if (string.IsNullOrWhiteSpace(book.Title)) 
        {
            throw new ArgumentException("Book name can not be empty");
        }
        
        book.AddingDate = DateTime.UtcNow;

        if (book.Status == BookStatus.Unknown) 
        {
            book.Status = BookStatus.Free;
        }

        return await _repository.AddBook(book);
    }

    public async Task<bool> DeleteBook(int id)
    {
        if (id < 0) 
        {
            throw new ArgumentException($"Can not delete book with invalid id: {id}");
        }

        return await _repository.DeleteBook(id);
    }
    public async Task<bool> UpdateBook(BookItem book)
    {
        if (book.Id < 0)
        {
            throw new ArgumentException($"Can not delete book with invalid id: {book.Id}");
        }

        return await _repository.UpdateBook(book);
    }
    public async Task<BookItem?> GetBookById(int id)
    {
        return await _repository.GetBookById(id);
    }

    public async Task<List<BookItem>?> GetAllBooks()
    {
        var books = await _repository.GetAllBooks();
        if (books is null) 
        {
            throw new ArgumentException("No books found");
        }
        return books;
    }

    public async Task<List<BookItem>?> SearchByProperty(PropertyInfo property, string searchValue)
    {
        var books = await _repository.GetAllBooks();
        
        if (books is null || books.Count == 0) return books; 

        var result = books.Where(book =>
        {
            object? value = property.GetValue(book);

            if (value is null)
                return false;

            if (value is string stringValue)
            {
                return stringValue.Contains(searchValue, StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }).ToList();

        return result;
    }
}
