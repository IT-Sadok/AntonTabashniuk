using DigitalLibrary.BusinessLogic;
using DigitalLibrary.Models;
using DigitalLibrary.Models.Enums;
using static DigitalLibrary.ConcurrentTests.BookFactory;

namespace DigitalLibrary.ConcurrentTests;

public class ConcurrentWorker
{
    private readonly ILibraryService _libraryService;
    private readonly StressStatistics _statistics;
    public ConcurrentWorker(ILibraryService libraryService, StressStatistics statistic) 
    {
        _libraryService = libraryService;
        _statistics = statistic;
    }

    public async Task RunAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            try
            {
                int operation = Random.Shared.Next(0, 5);
                switch (operation)
                {
                    case 0:
                        await AddBook();
                        break;

                    case 1:
                        await DeleteBook();
                        break;

                    case 2:
                        await BorrowBook();
                        break;

                    case 3:
                        await ReturnBook();
                        break;

                    case 4:
                        await ReadBooks();
                        break;
                }
            }
            catch (Exception ex)
            {
                Interlocked.Increment(ref _statistics.Exceptions);

                Console.WriteLine(ex.Message);
            }

            await Task.Delay(Random.Shared.Next(1, 20));
        }
    }

    public async Task AddBook()
    {
        BookItem book = RandomBookFactory.Create();

        await _libraryService.AddBook(book);

        Interlocked.Increment(ref _statistics.AddedBooks);
        Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} added book with id {book.Id}");
    }
    public async Task DeleteBook()
    {
        List<BookItem>? books = await _libraryService.GetAllBooks();

        if (books is null || books.Count == 0)
        {
            return;
        }

        BookItem randomBook = books[Random.Shared.Next(books.Count)];

        await _libraryService.DeleteBook(randomBook.Id);

        Interlocked.Increment(ref _statistics.RemovedBooks);
        Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} deleted  book with id {randomBook.Id}");
    }
    public async Task BorrowBook()
    {
        List<BookItem>? books = await _libraryService.GetAllBooks();

        BookItem? availableBook = books?.FirstOrDefault(b => b.Status != BookStatus.Borrowed);

        if (availableBook is null)
        {
            return;
        }
        
        availableBook.Status  = BookStatus.Borrowed;

        await _libraryService.UpdateBook(availableBook);

        Interlocked.Increment(ref _statistics.BorrowedBooks);
        Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} borrowed book with id {availableBook.Id}");
    }
    public async Task ReturnBook()
    {
        List<BookItem>? books = await _libraryService.GetAllBooks();

        BookItem? availableBook = books?.FirstOrDefault(x => x.Status == BookStatus.Borrowed);

        if (availableBook is null)
        {
            return;
        }

        availableBook.Status = BookStatus.Free;

        await _libraryService.UpdateBook(availableBook);

        Interlocked.Increment(ref _statistics.ReturnedBooks);
        Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} returned book with id {availableBook.Id}");
    }

    public async Task ReadBooks()
    {
        await _libraryService.GetAllBooks();

        Interlocked.Increment(ref _statistics.ReadOperations);
        Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} readed all books");
    }
}
