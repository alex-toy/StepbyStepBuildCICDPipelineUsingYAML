using BookLibrary.Models.Students;
using HelloWorldApp.DTOs;

namespace HelloWorldApp.Mappers;

public static class CourseMapper
{
    public static StudentCourseDto ToStudentCourseDto(this Enrollment e)
    {
        return new StudentCourseDto() { Id = e.CourseId, Label = e?.Course?.Label ?? string.Empty, Grade = e.Grade };
    }
}
