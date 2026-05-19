using DigitalLibrary.Models;
using System.Reflection;

namespace DigitalLibrary.BusinessLogic;
public interface ILibraryService
{
    Task<bool> AddBook(BookItem book);
    Task<bool> DeleteBook(int id);
    Task<bool> UpdateBook(BookItem book);
    Task<BookItem?> GetBookById(int id);
    Task<List<BookItem>?> GetAllBooks();
    Task<List<BookItem>?> SearchByProperty(PropertyInfo property, string searchValue);
}
