# SAMS | Student Academic Management System

A deployable ASP.NET Core MVC college project for managing students, marks, attendance and assignments.

## Stack
C#, .NET 8, ASP.NET Core MVC, Entity Framework Core, SQLite, Razor Views, Bootstrap and CSS.

## Features
- Cookie-based Admin and Student demo login
- Database-backed dashboards with LINQ statistics
- Student search, filtering, create, details and delete workflows
- Automatic SQLite creation and fictional seed data (10 students, 6 subjects, marks, attendance and assignments)
- Grade and attendance calculation services
- Responsive SAMS dashboard design

## Run locally
Install the .NET 8 SDK, then run:

```bash
dotnet restore
dotnet build
dotnet run
```

The SQLite file `sams.db` is created automatically. Demo accounts: `admin` / `Admin@123` and `student` / `Student@123`.

## Deploy to Railway
Push this folder to GitHub, create a Railway service from the repository, and deploy using the included `Dockerfile`. Railway supplies the public URL; the app listens on port 8080. SQLite is suitable for a demonstration deployment; use a managed database for production-scale persistence.

## OOP and academic concepts
`Person` is an abstract base class inherited by `Student`; `User` is inherited by `Admin` and `StudentUser`. Polymorphism is demonstrated by `GetRole()`, encapsulation by entity properties and domain calculations, and abstraction by `GradeService`/`ReportService`. EF Core provides relationships, CRUD and database connectivity; LINQ provides filtering, ordering and averages.
