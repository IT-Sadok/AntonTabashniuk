using DigitalLibrary.BusinessLogic;

namespace DigitalLibrary.ConcurrentTests;

public class StressRunner
{
    private readonly ILibraryService _libraryService;
    public StressRunner(ILibraryService libraryService)
    {
        _libraryService = libraryService;
    }

    public async Task RunAsync()
    {
        
    }
    
}
