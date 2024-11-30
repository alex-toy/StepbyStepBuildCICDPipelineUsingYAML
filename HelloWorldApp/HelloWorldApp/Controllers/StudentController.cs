using BookLibrary.Contexts;
using BookLibrary.Models.Students;
using HelloWorldApp.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelloWorldApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly StudentContext _db;

    public StudentController(StudentContext db)
    {
        _db = db;
    }

    [HttpGet("students")]
    public IEnumerable<StudentDto> GetStudents()
    {
        List<Student> students = _db.Students
                                        .Include(b => b.Enrollments)
                                        .Include(b => b.Grades)
                                        .ThenInclude(e => e.Course)
                                        .ToList();

        return students.Select(s => new StudentDto() { 
            Id = s.Id, 
            Name = s.Name, 
            Courses = s.Enrollments?.Select(e => new StudentCourseDto() { Id = e.CourseId, Label = e?.Course?.Label ?? string.Empty }).ToList(),
            Grades = s.Grades?.Select(g => new GradeDto() { Id = g.Id, CourseId = g.CourseId, Course = g.Course?.Label ?? string.Empty }).ToList(),
        });
    }

    [HttpPost("students")]
    public void Add([FromBody] Student student)
    {
        try
        {
            _db.Students.Add(student);
            _db.SaveChanges();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine($"An error occurred while adding a post: {ex.Message}");
            throw;
        }
    }

    [HttpGet("courses")]
    public IEnumerable<CourseDto> GetCourses()
    {
        List<Course> courses = _db.Courses
                                    .Include(b => b.Enrollments)
                                    .ThenInclude(e => e.Student)
                                    .ToList();

        return courses.Select(c => new CourseDto()
        {
            Id = c.Id,
            Label = c.Label,
            Students = c.Enrollments?.Select(e => new StudentDto() { Id = e.StudentId, Name = e?.Student?.Name ?? string.Empty }).ToList()
        });
    }

    [HttpPost("courses")]
    public void Add([FromBody] Course course)
    {
        try
        {
            _db.Courses.Add(course);
            _db.SaveChanges();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine($"An error occurred while adding a post: {ex.Message}");
            throw;
        }
    }

    [HttpPost("enroll")]
    public void Enroll([FromBody] Enrollment enrollment)
    {
        try
        {
            Course? course = _db.Courses.FirstOrDefault(c => c.Id == enrollment.CourseId);
            Student? student = _db.Students.FirstOrDefault(c => c.Id == enrollment.StudentId);

            if (course is null || student is null) return;

            if (_db.Enrollments.Any(e => e.StudentId == enrollment.StudentId && e.CourseId == enrollment.CourseId))
            {
                throw new InvalidOperationException("Student is already enrolled in this course.");
            }

            _db.Enrollments.Add(enrollment);

            _db.SaveChanges();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine($"An error occurred while enrolling: {ex.Message}");
            throw;
        }
    }
}