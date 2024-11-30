using BookLibrary.Contexts;
using BookLibrary.Models.Students;
using HelloWorldApp.DTOs;
using HelloWorldApp.Mappers;
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
                                            .ThenInclude(e => e.Course)
                                        .ToList();

        return students.Select(s => new StudentDto() { 
            Id = s.Id, 
            Name = s.Name, 
            Courses = s.Enrollments?.Select(e => e.ToStudentCourseDto()).ToList()
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

    [HttpPost("grades")]
    public void AddGrade([FromBody] GradeDto grade)
    {
        try
        {
            //Course? course = _db.Courses.FirstOrDefault(c => c.Id == grade.CourseId);
            //Student? student = _db.Students.FirstOrDefault(c => c.Id == grade.StudentId);

            //if (course is null || student is null) return;

            //if (_db.Grades.Any(e => e.StudentId == grade.StudentId && e.EnrollmentId == grade.CourseId))
            //{
            //    throw new InvalidOperationException("Student already has a grade in this course.");
            //}

            Enrollment? enrollment = _db.Enrollments.FirstOrDefault(e => e.Id == grade.EnrollmentId);

            if (enrollment == null) throw new InvalidOperationException("enrollment does not exist.");

            enrollment.Grade = grade.Value;

            _db.SaveChanges();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine($"An error occurred while enrolling: {ex.Message}");
            throw;
        }
    }

    [HttpPost("enroll")]
    public void Enroll([FromBody] EnrollmentDto enrollment)
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

            _db.Enrollments.Add(new Enrollment() { CourseId = enrollment.CourseId, StudentId = enrollment.StudentId });

            _db.SaveChanges();
        }
        catch (System.Exception ex)
        {
            Console.WriteLine($"An error occurred while enrolling: {ex.Message}");
            throw;
        }
    }
}