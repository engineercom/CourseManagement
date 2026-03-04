using CourseManagement.Api.Data;
using CourseManagement.Api.Dtos.CourseDtos;
using CourseManagement.Api.Dtos.InstructorDtos;
using CourseManagement.Api.Dtos.StudentDtos;
using CourseManagement.Api.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly AppDbContext _context;

    public CoursesController(AppDbContext context) => _context = context;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var courses = await _context.Courses
            .Select(course => new ResponseCourseDto(course.Id, course.Name, course.Price))
            .ToListAsync();
        return Ok(courses);
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateCourseDto courseDto)
    {
        var course = new Course
        {
            Name = courseDto.Title,
            Price = courseDto.Price
        };
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = course.Id }, course);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var course = await _context.Courses
            .Include(c => c.Students)  //StudentCourse ilişkisini dahil et
            .ThenInclude(sc => sc.Student)
            .Include(c => c.Instructors) //CourseInstructor ilişkisini dahil et
            .ThenInclude(ci => ci.Instructor)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (course == null) return NotFound();

        var dto = new CourseDetailDto
        {
            Id = course.Id,
            Name = course.Name,
            Price = course.Price,
            Students = course.Students
        .Select(sc => new ResponseStudentDto(sc.Student!.Id, sc.Student.FullName, sc.Student.Email))
        .ToList(),
            Instructors = course.Instructors
            .Select(ci => new ResponseInstructorDto
            {
                Id = ci.Instructor!.Id,
                FullName = ci.Instructor.FullName,
                Expertise = ci.Instructor.Expertise
            })
            .ToList()
        };

        return Ok(dto);
    }
    [HttpPut]
    public async Task UpdateAsync(UpdateStudentDto dto)
    {
        var student = await _context.Students.FindAsync(dto.Id);
        if (student is null) return;

        student.FullName = dto.FullName;
        student.Email = dto.Email;
        _context.Students.Update(student);
        await _context.SaveChangesAsync();

    }
}