using DigitalLibrary.BusinessLogic;
using DigitalLibrary.ConcurrentTests;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddSingleton<ILibraryService, LibraryService>()
    .AddSingleton<StressRunner>()
    .BuildServiceProvider();

using var scope = serviceProvider.CreateScope();

var stressRunner = scope.ServiceProvider.GetRequiredService<StressRunner>();

await stressRunner.RunAsync();