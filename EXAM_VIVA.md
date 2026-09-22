# Viva Preparation

1. **What is C#?** An object-oriented language used for the application logic.
2. **What is .NET?** The runtime and SDK used to build and run the application.
3. **What is MVC?** Model, View and Controller separation.
4. **Why ASP.NET Core MVC?** It is lightweight, cross-platform and maps naturally to the project.
5. **What is a class?** A blueprint such as `Student`.
6. **What is an object?** A runtime instance of a class.
7. **Where is inheritance used?** Student inherits Person; Admin inherits User.
8. **Where is polymorphism used?** Overridden `GetRole()` methods.
9. **What is abstraction?** Hiding implementation behind abstract base classes.
10. **What is encapsulation?** Keeping state and behavior in domain classes.
11. **What is EF Core?** An ORM that maps C# entities to database tables.
12. **Why SQLite?** It is simple, portable and requires no server.
13. **What is LINQ?** C# query syntax used for filtering and aggregates.
14. **How is attendance calculated?** Attended divided by conducted multiplied by 100.
15. **How are grades calculated?** `GradeService` maps total marks to A+ through F.
16. **How does CRUD work?** Controllers add, query, update or remove EF entities.
17. **How does login work?** Demo credentials are checked and a cookie claim stores the role.
18. **How are roles enforced?** `[Authorize(Roles = "Admin")]` or Student.
19. **What is dependency injection?** ASP.NET provides DbContext and services to controllers.
20. **How is the database initialized?** `EnsureCreated` runs and `DbSeeder` inserts demo data.
21. **What is a Razor View?** HTML with server-side C# expressions.
22. **What is a controller?** The request coordinator between view and model.
23. **How is responsive UI achieved?** Bootstrap and CSS media queries.
24. **How can reports be exported?** The report service creates text content that can be written with `File.WriteAllText`.
25. **How is the app deployed?** Dockerfile publishes it for Railway.
26. **What is validation?** Data annotations such as Required and Range protect inputs.
27. **What is a navigation property?** EF relationship property such as Student.Marks.
28. **What is a primary key?** The `Id` identifying an entity record.
29. **What is a foreign key?** StudentId/SubjectId linking related records.
30. **What future enhancement fits?** ASP.NET Identity and managed production database.
