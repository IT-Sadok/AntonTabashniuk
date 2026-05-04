namespace DigitalLibrary.Models.BookRepository
{
    public interface IBookRepository
    {
        Task AddBook(BookInterface book);
        Task DeleteBook(int id);
        Task UpdateBook(BookInterface book);
        Task<BookInterface?> GetBook(int id);
        Task<IEnumerable<BookInterface?>> GetAllBooks();
        Task<int> GetBooksCount();
    }
}
