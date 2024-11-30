namespace BookLibrary.Models.Students;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<Enrollment>? Enrollments { get; set; }
    public ICollection<Grade>? Grades { get; set; }
}
