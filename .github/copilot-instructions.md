---
applyTo: "**"
---
# Project Scope
Olympus Project is a modular monolithic software system inspired by Greco-Roman mythology.
It is designed to unify and integrate various corporate processes such as infrastructure, administrative, operational, and technical support.

The project is built on the .NET ecosystem and consists of three subprojects:
- Shared: a class library with shared code
- Server: an ASP.NET Core Web API
- Client: a Blazor WebAssembly (WASM) app

The Server serves the Client, which is a fully client-side Single Page Application (SPA) using Client-Side Rendering (CSR).
The Shared project provides reusable components across both Client and Server (e.g., DTOs).

## Project Stack
- Language: Mainly C# and some JavaScript in Client project
- Backend: ASP.NET Core Web API with Entity Framework and OData support
- Frontend: Blazor WASM with Radzen Component Library
- Database: SQL Server (Express) with Redis for cache service
- Authentication: JSON Web Token with support for Microsoft 365 OAuth

## Naming Conventions
- Use camelCase for variables
- Use PascalCase for constants
- Use PascalCase for classes, interfaces, properties, methods, functionns

## Code Styling and Formatting
- Use K&R style for code formatting.
- Always keep only one statement per line, unless it exceeds 160 characters.
- Insert a blank line inside the start and end of all code blocks ({}), including constructors, methods, control flow structures, and object initializations.
- For code blocks that fit within 160 characters (e.g., one-liner methods or operators), prefer writing them on a single line or using expression-bodied members (=>).
- For empty blocks (e.g., constructors like public Foo() {}), prefer using a single-line format.
- Avoid overly compact code. Only group directly related statements; otherwise, separate them with blank lines.
- Keep methods small, focused, and aboid using async void methods (except for event handlers).
- For file organization in Server project, use the pattern: {ProjectName}/{ProjectModule}/{ObjectType}/{ObjectName}.cs
- For file organization in Client project, follow the same pattern, adding: {ProjectName}/Modules/{ClientModule}/{ObjectType}/{ObjectName}.cs
- For file organization in Shared project, use the pattern: {ProjectName}/{ObjectType}/{ObjectName}.cs
- Always use file-scoped namespaces with a maximum of three levels, e.g., Olympus.{Project}.{Module}. Subfolders are for organization only, not namespace depth.
- Prefer clean, well-organized, and optimized code that follows industry standards and best practices, while remaining readable and easy to understand.
- Try to follow principles from Domain-Driven Design, Code Calisthenics, and Clean Architecture, but don’t be overly strict — prioritize the DRY principle.
- Avoid magic values, implicit behavior, and hidden side effects. Make dependencies and logic as clear as possible.
- If a Value Object contains validation logic, always expose a static IsValid(value) method and an instance-level IsValid() method.
- Always use readonly immutable structs for Value Objects.
- For Value Objects with a single property or a primary property, always add implicit operators.
- Always add an empty constructor, if needed, for Entities and Value Objects for proper EF Core use.
- If necessary, use the null-forgiving operator (!) in property declarations to avoid constructor warnings for parameterless constructors.
- Avoid comments unless strictly necessary. Keep the code self-explanatory and clean.
- Always write comments and string messages in English. Prefer short, simple and direct texts.
- Always use UTC for any kind of time or datetime values.
