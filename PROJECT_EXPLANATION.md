# Project Explanation

## Introduction
SAMS centralizes academic records so an administrator can maintain reliable student information and students can review progress.

## Architecture
The MVC presentation layer uses Razor Views. Controllers handle requests, services contain reusable business logic, and `ApplicationDbContext` maps C# entities to SQLite through EF Core.

## Database design
Students connect to Marks and Attendance. Subjects connect to Marks, Attendance and Assignments. Admins and StudentUsers provide simple role-aware demo authentication.

## OOP used
- Classes and objects: every database record is represented by an entity object.
- Inheritance: `Student : Person`, `Admin : User`, `StudentUser : User`.
- Polymorphism: each user/person overrides `GetRole()`.
- Abstraction: abstract `Person` and `User` require role behavior.
- Encapsulation: grade and attendance calculations are exposed as domain properties/services.

## LINQ, exceptions and file handling
LINQ performs search, department filtering, counts and averages. EF operations can be wrapped in friendly error handling as the application grows. `ReportService` builds academic summaries with `StringBuilder`; it is the natural extension point for `File.WriteAllText` report export.

## Deployment
The Dockerfile publishes a self-contained ASP.NET Core image for Railway. SQLite initializes on first start and seed data is fictional.
