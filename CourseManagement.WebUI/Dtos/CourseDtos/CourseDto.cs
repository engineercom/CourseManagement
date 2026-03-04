using CourseManagement.WebUI.Dtos.InstructorDtos;

namespace CourseManagement.WebUI.Dtos.CourseDtos
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public List<InstructorDto> Instructors { get; set; }
    }
}
