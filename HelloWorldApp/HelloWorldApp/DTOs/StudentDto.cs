﻿namespace HelloWorldApp.DTOs;

public class StudentDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<StudentCourseDto>? Courses { get; set; }
}
