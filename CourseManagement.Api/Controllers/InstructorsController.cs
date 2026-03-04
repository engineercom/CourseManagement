using CourseManagement.Api.Data;
using CourseManagement.Api.Dtos.CourseDtos;
using CourseManagement.Api.Dtos.InstructorDtos;
using CourseManagement.Api.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public InstructorsController(AppDbContext context) => _context = context;

        [HttpGet]
        public IActionResult GetAll()
        {

            var response = _context.Instructors
                .Select(i => new ResponseInstructorDto
                { 
                Id=i.Id,
                FullName= i.FullName,
                Expertise= i.Expertise,
                Courses=i.Courses
                .Select(ci=>new ResponseCourseDto(ci.Course!.Id,ci.Course.Name,ci.Course.Price)).ToList()

                }).ToList();
            return Ok(response);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            var response = _context.Instructors
                .Include(i => i.Courses)
                 .ThenInclude(ci => ci.Course)
                .FirstOrDefault(i => i.Id == id);
           
            if (response is null) return NotFound();
            var dto = new InstructorDetailDto {
            Id=response.Id,
            FullName=response.FullName,
            Expertise=response.Expertise,
            Courses=response.Courses.Select(ci=>new ResponseCourseDto(ci.Course!.Id,ci.Course.Name,ci.Course.Price)).ToList()
            };

            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Create(CreateInstructorDto dto)
        {
            if (!ModelState.IsValid) return BadRequest();
            _context.Instructors.Add(new Instructor { FullName = dto.FullName, Expertise = dto.Expertise });
            _context.SaveChanges();
            return Created();
        }
        [HttpPost("assing-courses")]
        public IActionResult AssignCourses(InstructorAssignCourseDto dto)
        {

            if (dto == null) return NotFound("No data send");
            var instructor = _context.Instructors.FirstOrDefault(i => i.Id == dto.InstructorId);
            if (instructor == null) return NotFound("Instructor is not found");
            foreach (int courseId in dto.Courses)
            {
                if (!_context.Courses.Any(c => c.Id == courseId)) return NotFound("Course is not found");
                if (!_context.CourseInstructors.Any(ci => ci.InstructorId == instructor.Id&&ci.CourseId==courseId))
                {
                    _context.CourseInstructors.Add(new CourseInstructor { InstructorId = instructor.Id, CourseId = courseId });
                    
                }
               
            }
            _context.SaveChanges();
            return Created();
        }
        [HttpPut]
        public IActionResult Update(UpdateInstructorDto dto)
        {

            if (!ModelState.IsValid) return BadRequest();

            _context.Instructors.Update(new Instructor { Id = dto.Id, FullName = dto.FullName, Expertise = dto.Expertise });
            _context.SaveChanges();
            return NoContent();
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var response = _context.Instructors.Find(id);
            if ((response is null)) return NotFound();
            _context.Instructors.Remove(response);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
