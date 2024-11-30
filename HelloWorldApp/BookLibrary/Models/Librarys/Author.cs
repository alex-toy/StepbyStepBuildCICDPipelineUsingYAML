namespace BookLibrary.Models.Librarys;

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<Book> Books { get; set; }

    public override string ToString()
    {
        return $"{Name}";
    }
}
