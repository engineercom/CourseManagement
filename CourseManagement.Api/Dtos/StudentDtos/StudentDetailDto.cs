using CourseManagement.Api.Dtos.CourseDtos;

namespace CourseManagement.Api.Dtos.StudentDtos;

public class StudentDetailDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public List<ApiCourseDto> Courses { get; set; }
 
}


