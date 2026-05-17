using DigitalLibrary;
using DigitalLibrary.BusinessLogic;
using DigitalLibrary.DataAccess.BookRepository;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddSingleton<IBookRepository>(new JsonBookRepository("library_data.json"))
    .AddSingleton<LibraryService>()
    .AddSingleton<ConsoleInterface>()
    .BuildServiceProvider();

using var scope = serviceProvider.CreateScope(); 

var consoleInterface = scope.ServiceProvider.GetRequiredService<ConsoleInterface>();

await consoleInterface.StartProgram();
