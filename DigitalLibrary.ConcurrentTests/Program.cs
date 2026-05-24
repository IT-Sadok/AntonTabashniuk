using DigitalLibrary.BusinessLogic;
using DigitalLibrary.ConcurrentTests;
using DigitalLibrary.DataAccess.BookRepository;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddSingleton<IBookRepository>(new JsonBookRepository("testLoad.json"))
    .AddSingleton<ILibraryService, LibraryService>()
    .AddSingleton<StressRunner>()
    .BuildServiceProvider();

using var scope = serviceProvider.CreateScope();

var stressRunner = scope.ServiceProvider.GetRequiredService<StressRunner>();

await stressRunner.RunAsync();