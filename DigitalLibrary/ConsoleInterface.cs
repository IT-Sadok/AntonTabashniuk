using DigitalLibrary.BusinessLogic;
namespace DigitalLibrary
{
    public class ConsoleInterface
    {
        private readonly LibraryService _libraryService;
        public ConsoleInterface(LibraryService libraryService)
        {
            _libraryService = libraryService ?? throw new ArgumentNullException(nameof(libraryService));
        }
    }
}
