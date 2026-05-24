using DigitalLibrary.BusinessLogic;

namespace DigitalLibrary.ConcurrentTests;

public class ConcurrentWorker
{
    private readonly ILibraryService _libraryService;
    public ConcurrentWorker(ILibraryService libraryService) 
    {
        _libraryService = libraryService;
    }

    public async Task AddBook()
    {
    }
    public async Task DeleteBook()
    {
    }
    public async Task BorrowBook()
    {
    }
    public async Task ReturnBook()
    {
    }
}
