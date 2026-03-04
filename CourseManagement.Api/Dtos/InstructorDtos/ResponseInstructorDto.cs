using CourseManagement.Api.Dtos.CourseDtos;

namespace CourseManagement.Api.Dtos.InstructorDtos;

public class ResponseInstructorDto {
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Expertise { get; set; }
    public List<ResponseCourseDto> Courses { get; set; }
};

