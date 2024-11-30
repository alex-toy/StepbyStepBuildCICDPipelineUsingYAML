namespace HelloWorldApp.DTOs;

public class GradeDto
{
    public int Id { get; set; }

    public int CourseId { get; set; }
    public string Course { get; set; } = string.Empty;

    public int Value { get; set; }
}
