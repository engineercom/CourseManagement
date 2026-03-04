using CourseManagement.WebUI.Dtos.CourseDtos;

namespace CourseManagement.WebUI.Dtos.StudentDtos;

public class DetailStudentDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public List<CourseDto> Courses { get; set; }
}
