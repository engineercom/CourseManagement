

using CourseManagement.WebUI.Dtos.CourseDtos;
using CourseManagement.WebUI.Dtos.InstructorDtos;
using CourseManagement.WebUI.Dtos.StudentDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace CourseManagement.WebUI.Controllers;

public class StudentController : Controller
{
    private readonly IHttpClientFactory _factory;

    public StudentController(IHttpClientFactory factory)
    {
        _factory = factory;
    }

    public async Task<IActionResult> Index()
    {
        var client = _factory.CreateClient("api");
        var students = await client.GetFromJsonAsync<List<StudentDto>>("api/students");

        return View(students);
    }
    public async Task<IActionResult> Detail(int id)
    {
        var client = _factory.CreateClient("api");
        var apiStudent = await client.GetFromJsonAsync<DetailStudentDto>($"api/students/{id}");
        if (apiStudent is null) return NotFound("Student not found");
        var studentDetail = new DetailStudentDto
        {
            Id = apiStudent.Id,
            FullName = apiStudent.FullName,
            Email = apiStudent.Email,
            Courses = apiStudent.Courses.Select(c => new CourseDto
            {
                Id = c.Id,
                Name = c.Name,
                Price = c.Price,
                Instructors = c.Instructors.Select(i => new InstructorDto
                {
                    Id = i.Id,
                    FullName = i.FullName

                }).ToList()

            }).ToList()

        };
        return View(studentDetail);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateStudentDto dto)
    {
        var client = _factory.CreateClient("api");
        var response = await client.PostAsJsonAsync("api/students", dto);
        response.EnsureSuccessStatusCode();

        return RedirectToAction("Index");
    }
    [HttpGet]
    public async Task<IActionResult> AssignCourse(int id)
    {
        var client = _factory.CreateClient("api");
        var courses=await client.GetFromJsonAsync<List<CourseDto>>("api/courses");
        var vm=new AssignCourseViewModel
        {
            Id=id,
            Courses=courses!.Select(c=>new CourseCheckboxItem
            {
                Id=c.Id,
                Name=c.Name,
                IsSelected=false
            }).ToList()
        };
        return View(vm);
    }
    [HttpPost]
    public async Task<IActionResult> AssignCourse(AssignCourseViewModel vm)
    {
        var selectedIds = vm.Courses
            .Where(c => c.IsSelected)
            .Select(c => c.Id)
            .ToList();

        var client = _factory.CreateClient("api");

        await client.PostAsJsonAsync("api/students/assign-courses", new
        {
            StudentId = vm.Id,
            CourseIds = selectedIds
        });

        return RedirectToAction("Detail", new { id = vm.Id });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var client = _factory.CreateClient("api");
        var response=await client.GetFromJsonAsync<UpdateStudentDto>($"api/students/{id}");
   
        if (response is null) return NotFound("Student not found");
       


        return View(response);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(UpdateStudentDto dto)
    {
        var client = _factory.CreateClient("api");
        var response = await client.PutAsJsonAsync($"api/students/{dto.Id}", dto);
        response.EnsureSuccessStatusCode();
       

        return RedirectToAction("Index");
    }
}
