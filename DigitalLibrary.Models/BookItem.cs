using DigitalLibrary.Models.Enums;
using System.ComponentModel;

namespace DigitalLibrary.Models;

public class BookItem
{
    [Description("Book ID")]
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Author { get; set; }
    public BookGenre Genre { get; set; }
    public BookStatus Status { get; set; }
    public BookLanguage Language { get; set; }

    [Description("Book adding date")]
    public DateTime AddingDate { get; set; }
    
    [Description("Book release date")]
    public DateTime? ReleaseDate { get; set; }
    public string? Description { get; set; }
}
