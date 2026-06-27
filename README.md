# EnvironmentCrime

A fullstack web application for reporting and managing environmental crimes, built with ASP.NET Core MVC and SQL Server. The system supports multiple user roles — citizens can report crimes publicly, while staff roles (coordinator, investigator, manager) handle internal case management.

## Tech Stack

- **Framework:** ASP.NET Core MVC (.NET)
- **Language:** C#
- **Database:** SQL Server with Entity Framework Core
- **Auth:** ASP.NET Core Identity (role-based)
- **Views:** Razor Pages
- **Session:** Server-side session storage

## Roles

- **Citizen** — Public user who can report an environmental crime and track it via reference number
- **Coordinator** — Logs in and manages incoming reports, assigns them to departments
- **Investigator** — Handles assigned errands, adds investigation notes and actions
- **Manager** — Overview of all errands, manages departments and employees

## Features

- Public crime reporting form with validation
- Reference number generated on submission
- Role-based access control with ASP.NET Identity
- Internal case management per role
- Support for evidence samples and pictures per errand
- Seeded database with initial roles and employees

## Project Structure

```plaintext
EnviroooProject/
├── Controllers/
│   ├── AccountController.cs
│   ├── CitizenController.cs
│   ├── CordinatorController.cs
│   ├── InvestigatorController.cs
│   └── ManagerController.cs
├── Models/
│   ├── POCO/
│   │   ├── Errand.cs
│   │   ├── Employee.cs
│   │   ├── Department.cs
│   │   └── ...
│   ├── ApplicationDbContext.cs
│   ├── AppIdentityDbContext.cs
│   ├── EFEnviormentRepository.cs
│   └── IdentityInitializer.cs
├── Views/
│   ├── Citizen/
│   ├── Cordinator/
│   ├── Investigator/
│   ├── Manager/
│   └── Shared/
├── Components/
└── Program.cs
```

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server

### Setup

```bash
git clone https://github.com/SorenUPP/EnvironmentCrime.git
cd EnvironmentCrime
```

Update the connection string in `appsettings.json`, then run migrations:

```bash
dotnet ef database update
dotnet run
```

## License

MIT
