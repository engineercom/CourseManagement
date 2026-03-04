using CourseManagement.Api.Data;
using CourseManagement.Api.Dtos.CourseDtos;
using CourseManagement.Api.Dtos.InstructorDtos;
using CourseManagement.Api.Dtos.StudentDtos;
using CourseManagement.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public StudentsController(AppDbContext context)=>_context=context;


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _context.Students
        .Select(student => new ResponseStudentDto(student.Id, student.FullName, student.Email))
        .ToListAsync();
        return Ok(response);
    }

    //öğrencinin bağlı olduğu kursları da getirmek için include kullanıyoruz
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var student = await _context.Students
            .Include(s => s.Courses)
            .ThenInclude(sc => sc.Course)
            .ThenInclude(ci=>ci.Instructors)
            .ThenInclude(i=>i.Instructor)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (student == null) return NotFound();
        var dto = new StudentDetailDto
        {
            Id = student.Id,
            FullName = student.FullName,
            Email = student.Email,
            Courses = student.Courses
                    .Select(sc => new ApiCourseDto
                    {
                        Id = sc.Course!.Id,
                        Name = sc.Course.Name,
                        Price = Convert.ToDecimal(sc.Course.Price),
                        Instructors = sc.Course.Instructors
                        .Select(ci => new ResponseInstructorDto{
                           Id= ci.Instructor!.Id,
                          FullName = ci.Instructor.FullName,
                          Expertise = ci.Instructor.Expertise })
                        .ToList()
                    }).ToList()
         
        };

        return Ok(dto);
      

    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateStudentDto dto)
    {
        var student = new Student
        {
            FullName = dto.FullName,
            Email = dto.Email

        };
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
    }
    //assign courses (çoka çok insert)
    [HttpPost("assign-courses")]
    public async Task<IActionResult> AssignCourses(StudentAssignCoursesDto dto)
    {
        var student = await _context.Students.FindAsync(dto.StudentId);
        if (student == null) return NotFound("Student not found");
        foreach (var courseId in dto.CourseIds)
        {
            if (!await _context.Courses.AnyAsync(c => c.Id == courseId))
                return NotFound($"Course with id {courseId} not found");

            if (!await _context.StudentCourses.AnyAsync(sc => sc.StudentId == dto.StudentId && sc.CourseId == courseId))
            {
                _context.StudentCourses.Add(new StudentCourse
                {
                    StudentId = dto.StudentId,
                    CourseId = courseId
                });
            }
        }
        await _context.SaveChangesAsync();
        return Ok("Courses assigned with student");
    }
    [HttpPut]
    public async Task<IActionResult> Update(UpdateStudentDto dto)
    {
        var student = await _context.Students.FindAsync(dto.Id);
        if (student == null) return NotFound("Student not found");
        student.FullName = dto.FullName;
        student.Email = dto.Email;
        await _context.SaveChangesAsync();
        return NoContent();
    }
    


}