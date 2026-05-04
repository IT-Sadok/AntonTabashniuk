using DigitalLibrary.Models.Enums;

namespace DigitalLibrary.Models
{
    public class BookInterface
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public BookStatus Status { get; set; }
        public BookLanguage Language { get; set; }
        public DateTime Created { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; }
    }
}
