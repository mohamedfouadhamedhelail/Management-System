# Task Management System

A simple Task Management web application built with **ASP.NET Core MVC** and **Entity Framework Core**, following an **N-Tier architecture**.

Users can create tasks, view all tasks, view a task's details, filter tasks by completion status, and sort them by due date.

---

## Features

- **Create** a new task (the due date must be in the future)
- **Read** all tasks and get a task by ID
- **Filter** tasks by `IsCompleted` status
- **Sort** tasks by `DueDate`
- Server-side validation with a business rule in the BLL
- Database configuration using EF Core **Fluent API**

---

## Tech Stack

| Area | Technology |
|---|---|
| Framework | .NET 8, ASP.NET Core MVC |
| ORM | Entity Framework Core 8 |
| Database | SQL Server (LocalDB by default) |
| UI | Razor Views, Bootstrap 5 |
| Architecture | N-Tier (Core, DAL, BLL, Web) |

---

## Architecture

```
Web (MVC)  -->  BLL  -->  DAL  -->  SQL Server
   |            |          |
   +------------+----------+---->  Core (Entities + Interfaces)
```

| Layer | Responsibility |
|---|---|
| **Core** | Entities (`TaskItem`) and interfaces (`ITaskRepository`) |
| **DAL** | EF Core `AppDbContext`, Fluent API configurations, repository implementation |
| **BLL** | Business logic and validation (`TaskService`, `ITaskService`) |
| **Web** | Controllers, Views, and `Program.cs` (dependency injection setup) |

### Project structure

```
Solution
├── TaskManagementSystem.Core
│   ├── Entities/        TaskItem.cs
│   └── Interfaces/      ITaskRepository.cs
├── DAL
│   ├── Configurations/  TaskItemConfiguration.cs
│   ├── Data/            AppDbContext.cs
│   ├── Repositories/    TaskRepository.cs
│   └── Migrations/
├── BLL
│   ├── Interfaces/      ITaskService.cs
│   └── Services/        TaskService.cs
└── Management System (Web)
    ├── Controllers/     TasksController.cs
    ├── Views/           Tasks/ (Index, Details, Create), Shared/_Layout
    ├── appsettings.json
    └── Program.cs
```

---

## The `TaskItem` entity

| Property | Type | Notes |
|---|---|---|
| `Id` | `int` | Primary key |
| `Title` | `string` | Required, max length 100 |
| `Description` | `string?` | Max length 500 |
| `IsCompleted` | `bool` | Task status |
| `CreatedAt` | `DateTime` | Default value: current date (`GETDATE()`) |
| `DueDate` | `DateTime?` | Optional, must be in the future when provided |

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- SQL Server or SQL Server LocalDB
- Visual Studio 2022 (or the `dotnet` CLI)

### 1. Clone the repository

```bash
git clone https://github.com/<your-username>/<your-repo>.git
cd <your-repo>
```

### 2. Configure the connection string

Edit `appsettings.json` in the Web project if you are not using LocalDB:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=TaskManagementDb;Trusted_Connection=True;TrustServerCertificate=True"
}
```

### 3. Create the database

**Visual Studio (Package Manager Console):**

```powershell
Update-Database -Project DAL -StartupProject "Management System"
```

**dotnet CLI:**

```bash
dotnet ef database update --project DAL --startup-project "Management System"
```

### 4. Run the app

Set the Web project as the startup project and press **F5**, or run:

```bash
dotnet run --project "Management System"
```

The app opens on the **Tasks** page.

---

## Adding a new migration

```powershell
Add-Migration <MigrationName> -Project DAL -StartupProject "Management System"
Update-Database -Project DAL -StartupProject "Management System"
```

The DAL project is the migrations assembly, so the `AddDbContext` call in `Program.cs` uses:

```csharp
options.UseSqlServer(connectionString, b => b.MigrationsAssembly("DAL"));
```

---

## Key Design Decisions

- **Repository pattern:** the BLL depends on `ITaskRepository` (defined in Core), not on EF Core directly.
- **Business rule in the BLL:** `TaskService` rejects a due date that is not in the future and sets `CreatedAt`.
- **Fluent API configuration:** `TaskItemConfiguration` sets the max lengths and the `CreatedAt` default value, keeping the entity clean.
- **Dependency Injection:** the repository and service are registered with `AddScoped` in `Program.cs`.

---

## Future Improvements

- Edit, delete, and mark-as-completed actions
- Pagination and search
- Unit tests for the BLL
- Authentication and per-user tasks

---

## License

This project is for educational purposes.
