# 🎓 Student API Server

Student API Server is a RESTful ASP.NET Core Web API designed to manage student records efficiently. The project demonstrates real-world backend development concepts including CRUD operations, REST APIs, Entity Framework Core, and SQL Server integration.

---

## ✨ Features

- ➕ Create new student records
- 📋 Retrieve all students
- 🔍 Get a student by ID
- ✏️ Update student information
- ❌ Delete student records
- ✅ RESTful API design
- 💾 SQL Server database integration
- 📦 Entity Framework Core
- 📄 JSON request and response format
- 🧪 API testing with Swagger

---

## 🛠️ Tech Stack

- ASP.NET Core Web API
- C#
- Entity Framework Core
- SQL Server
- Swagger / OpenAPI
- Visual Studio

---

## 📂 Project Structure

```text
StudentAPIServer
│
├── Controllers/
├── Models/
├── Data/
├── DTOs/
├── Migrations/
├── Properties/
├── Program.cs
└── appsettings.json
```

---

## 🚀 Getting Started

### Prerequisites

- .NET SDK
- SQL Server
- Visual Studio 2022

### Clone the repository

```bash
git clone https://github.com/hassan-koubali/StudentAPIServer.git
```

### Run the project

```bash
dotnet restore
dotnet ef database update
dotnet run
```

The API will be available through Swagger.

---

## 📌 API Endpoints

| Method | Endpoint | Description |
|---------|----------|-------------|
| GET | `/api/students` | Get all students |
| GET | `/api/students/{id}` | Get a student by ID |
| POST | `/api/students` | Create a new student |
| PUT | `/api/students/{id}` | Update a student |
| DELETE | `/api/students/{id}` | Delete a student |

---

## 📸 Screenshots

> Add Swagger or API screenshots here.

---

## 🎯 Learning Objectives

This project was built to practice:

- REST API development
- ASP.NET Core fundamentals
- Entity Framework Core
- Database CRUD operations
- Dependency Injection
- Clean API architecture

---

## 📄 License

Licensed under the MIT License.

---

## 👨‍💻 Author

**Hassan Koubali**

GitHub: https://github.com/hassan-koubali
