using CourseManagement.Api.Dtos.InstructorDtos;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.Api.Dtos.CourseDtos
{
    public class ApiCourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Precision(18,2)]
        public decimal Price { get; set; }
        public List<ResponseInstructorDto> Instructors { get; set; }
    }
}
