using DigitalLibrary.Models;

namespace DigitalLibrary.DataAccess.BookRepository
{
    public interface IBookRepository
    {
        Task<bool> AddBook(BookItem book);
        Task<bool> DeleteBook(int id);
        Task<bool> UpdateBook(BookItem book);
        Task<BookItem?> GetBookById(int id);
        Task<List<BookItem>?> GetAllBooks();
        Task<int> GetBooksCount();
    }
}
