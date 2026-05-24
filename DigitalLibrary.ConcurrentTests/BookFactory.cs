using DigitalLibrary.Models;
using DigitalLibrary.Models.Enums;

namespace DigitalLibrary.ConcurrentTests;

public static class BookFactory
{
    public static class RandomBookFactory
    {
        public static BookItem Create()
        {
            return new BookItem
            {
                Id = Random.Shared.Next(),
                Title = $"Book_{Guid.NewGuid()}",
                Author = $"Author_{Random.Shared.Next(1, 1000)}",
                Status = BookStatus.Borrowed,
            };
        }
    }
}
