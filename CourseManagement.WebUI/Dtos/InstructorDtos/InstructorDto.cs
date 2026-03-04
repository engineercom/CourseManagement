using CourseManagement.WebUI.Dtos.CourseDtos;

namespace CourseManagement.WebUI.Dtos.InstructorDtos
{
    public class InstructorDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Expertise { get; set; }
        public ICollection<CourseDto>  Courses { get; set; }
    }
}
