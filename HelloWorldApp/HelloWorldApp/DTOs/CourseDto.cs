using BookLibrary.Models;

namespace HelloWorldApp.DTOs;

public class CourseDto
{
    public int Id { get; set; }
    public string Label { get; set; } = string.Empty;

    public ICollection<StudentDto>? Students { get; set; }
}
