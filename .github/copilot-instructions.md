---
applyTo: "**"
---
# Project Scope
Olympus Project is a modular monolithic software system inspired by Greco-Roman mythology.
It is designed to unify and integrate various corporate processes such as infrastructure, administrative, operational, and technical support.

The project is built on the .NET ecosystem and has three solutions folders:
- Architect: the core layer based on Domain Driven Design (DDD)
- Backend: the backend presentation layer
- Frontend: the frotend presentation layer

The solution folders contains the following projects:
- Architect/Domain: a class library for domain layer code
- Architect/Domain.Tests: a xUnit class library for domain layer tests
- Architect/Shared: a class library for shared code like DTOs
- Architect/Application: a class library for application layer code
- Architect/Application.Tests: a xUnit class library for application layer tests
- Architect/Infrastructure: a class library for infrastructure layer code
- Backend/Api: an ASP.NET Core Web API and static files hosting
- Backend/Api.Tests: a xUnit class library for api tests
- Frontend/Interface: a Razor Class Library for user interface components
- Frontend/Interface.Tests: a bUnit class library for interface tests
- Frontend/Native: a MAUI project for Windows, MacCatalyst, Android and iOS clients
- Frontend/Web: a Blazor WebAssembly client with full Client Side Rendering and PWA support

Each project has the following dependencies:
- Architect/Domain: none
- Architect/Domain.Tests: Domain
- Architect/Shared: none
- Architect/Application: Domain and Shared
- Architect/Application.Tests: Application
- Architect/Infrastructure: Domain and Application
- Backend/Api: Application, Infrastructure, Shared and Web
- Backend/Api.Tests: Api
- Frontend/Interface: Shared
- Frontend/Interface.Tests: Interface
- Frontend/Native: Interface
- Frontend/Web: Interface

Each project has the following purposes:
- Architect/Domain: holds entities, value objects, repositories interfaces and domain services
- Architect/Domain.Tests: unit tests for Domain project
- Architect/Shared: holds Data Transfer Objects (DTOs) and other public code
- Architect/Application: holds application services and application layer relative code
- Architect/Application.Tests: unit tests for Application project
- Architect/Infrastructure: holds database context code, repositories implementations and other infrastructure layer code
- Backend/Api: holds controllers, server side authentication and serves the Web project
- Backend/Api.Tests: integration tests for Api project
- Frontend/Interface: holds components, layouts, pages, routes, client side authentication and api services
- Frontend/Interface.Tests: unit tests for Interface project
- Frontend/Native: just a wrapper for Interface project with optional plataform specific code
- Frontend/Web: just a wrapper for Interface project with optional web specific code like PWA support

Each project has the following root namespaces and assembly names:
- Architect/Domain: Olympus.Architect.Domain
- Architect/Domain.Tests: Olympus.Architect.Domain.Tests
- Architect/Shared: Olympus.Architect.Shared
- Architect/Application: Olympus.Architect.Application
- Architect/Application.Tests: Olympus.Architect.Application.Tests
- Architect/Infrastructure: Olympus.Architect.Infrastructure
- Backend/Api: Olympus.Backend.Api
- Backend/Api.Tests: Olympus.Backend.Api.Tests
- Frontend/Interface: Olympus.Frontend.Interface
- Frontend/Interface.Tests: Olympus.Frontend.Interface.Tests
- Frontend/Native: Olympus.Frontend.Native
- Frontend/Web: Olympus.Frontend.Web

## Project Stack
- Language: Mainly C# and some JavaScript in Frontend projects
- Backend: ASP.NET Core Web API with Entity Framework and OData support
- Frontend: Blazor Hybrid with Web project (using WASM) with Radzen Component Library
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
- Always use immutable readonly record structs for Value Objects.
- For Value Objects with a single property or a primary property, always add implicit operators.
- Always add an empty constructor, if needed, for Entities and Value Objects for proper EF Core use.
- If necessary, use the null-forgiving operator (!) in property declarations to avoid constructor warnings for parameterless constructors.
- Avoid comments unless strictly necessary. Keep the code self-explanatory and clean.
- Always write comments and string messages in English. Prefer short, simple and direct texts.
- Always use UTC for any kind of time or datetime values.
