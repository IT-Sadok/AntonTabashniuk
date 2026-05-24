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
        Console.WriteLine("Starting stress test...");

        CancellationTokenSource cts = new(TimeSpan.FromSeconds(10));

        StressStatistics statistics = new StressStatistics();
        
        List<Task> tasks = new();

        int workersCount = 100;

        for (int i = 0; i < workersCount; i++)
        {
            ConcurrentWorker worker = new(_libraryService, statistics);

            tasks.Add(worker.RunAsync(cts.Token));
        }

        try
        {
            await Task.WhenAll(tasks);
        }
        catch
        {
        }

        Console.WriteLine("\nStress test completed.");
        PrintStatistics(statistics);
        Console.ReadKey();
    }

    private void PrintStatistics(StressStatistics statistics)
    {
        Console.WriteLine("=== RESULTS ===");

        Console.WriteLine($"Added: {statistics.AddedBooks}");
        Console.WriteLine($"Removed: {statistics.RemovedBooks}");
        Console.WriteLine($"Borrowed: {statistics.BorrowedBooks}");
        Console.WriteLine($"Returned: {statistics.ReturnedBooks}");
        Console.WriteLine($"Read: {statistics.ReadOperations}");
        Console.WriteLine($"Exceptions: {statistics.Exceptions}");

    }
}
    