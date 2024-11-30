using BookLibrary.Models;

namespace HelloWorldApp.DTOs;

public class StudentCourseDto
{
    public int Id { get; set; }
    public string Label { get; set; } = string.Empty;
    public int Grade { get; set; }
}
