# CourseManagement

A layered ASP.NET Core project built with Web API and MVC (WebUI) architecture.
This project demonstrates CRUD operations, relational data management, and API consumption using HttpClient.

---

## 🚀 Technologies Used

* ASP.NET Core Web API
* ASP.NET Core MVC (WebUI)
* Entity Framework Core
* SQL Server
* HttpClient
* DTO Pattern
* Repository Pattern (if used)
* Git & GitHub

---

## 📌 Project Structure

CourseManagement
│
├── CourseManagement.API
│   ├── Controllers
│   ├── Entities
│   ├── Data (DbContext)
│   └── DTOs
│
├── CourseManagement.WebUI
│   ├── Controllers
│   ├── Views
│   ├── DTOs
│   └── Services (HttpClient usage)

---

## 📚 Features

* Student CRUD operations
* Course CRUD operations
* Many-to-Many relationship between Student and Course
* Assign multiple courses to a student using checkboxes
* API consumption in WebUI using HttpClient
* DTO usage for data transfer
* Layered architecture separation

---

## 🔄 API Endpoints (Examples)

### Students

* GET /api/student
* GET /api/student/{id}
* POST /api/student
* PUT /api/student
* DELETE /api/student/{id}
* POST /api/student/assign-courses

### Courses

* GET /api/course
* POST /api/course
* PUT /api/course
* DELETE /api/course/{id}

---

## 🛠️ How to Run the Project

1. Clone the repository:
   git clone https://github.com/yourusername/CourseManagement.git

2. Open the solution in Visual Studio.

3. Update the connection string in:
   CourseManagement.API/appsettings.json

4. Apply migrations (if needed):
   Update-Database

5. Run the API project.

6. Run the WebUI project.

---

## 🧠 Learning Goals

This project was built to practice:

* Consuming APIs with HttpClient
* Using GetFromJsonAsync<T>()
* Managing Many-to-Many relationships in EF Core
* Using DTOs correctly
* Structuring a real-world layered project
* Publishing projects to GitHub

---

## 📌 Future Improvements

* Authentication & Authorization (JWT)
* Validation with FluentValidation
* AutoMapper integration
* Generic repository pattern
* Deployment to Azure

---

## 👩‍💻 Author

Nazan Akıncı
Computer Engineer

---

This project represents a practical step toward building scalable and maintainable .NET applications.
