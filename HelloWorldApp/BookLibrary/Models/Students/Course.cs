namespace BookLibrary.Models.Students;

public class Course
{
    public int Id { get; set; }
    public string Label { get; set; } = string.Empty;

    public ICollection<Enrollment>? Enrollments { get; set; }
}
