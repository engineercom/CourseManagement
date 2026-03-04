using CourseManagement.Api.Dtos.InstructorDtos;
using CourseManagement.Api.Dtos.StudentDtos;

namespace CourseManagement.Api.Dtos.CourseDtos;

public class CourseDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public List<ResponseStudentDto>  Students { get; set; }
    public List<ResponseInstructorDto> Instructors { get; set; }

}
