---
applyTo: "**"
---

# Project Scope
Olympus is a modular monolithic software system inspired by Greco-Roman mythology, designed to unify and integrate various corporate processes such as infrastructure, administrative, operational, and technical support in a modular and customizable way. Instead of following a traditional multi-tenant SaaS model, Olympus enables individualized and flexible deployments per client, allowing deep customization via isolated solutions and environment-specific configurations.

## Project Architecture
The core of Olympus is a collection of NuGet packages, where each package represents a specific business capability or foundational service (modules). The applications themselves (hosts) are minimal "shells" that consume these packages. This strategy allows for maximum flexibility: a client can build a highly customized application by simply creating a host project and selecting which Olympus modules to include as dependencies.

The project is built on the .NET ecosystem and is organized as the following:
- **Packages**: Contains foundational packages used across all projects.
  - `Olympus.Analyzers`: Code analysis rules and conventions.
  - `Olympus.Generators`: Source generators for code automation.
  - `Olympus.Sdk`: SDK for easy integration and extensibility.
- **Core**: Contains foundational code, contracts, and abstractions used by all projects.
  - `Olympus.Core`: Base functionalities shared across all projects.
  - `Olympus.Core.Archend`: Shared models (DTOs) and functionalities layer.
  - `Olympus.Core.Backend`: Data access and processing logic layer.
  - `Olympus.Core.Frontend`: User interface and services layer.
- **Modules**: Contains the projects for each business capability of the system.
  - `Olympus.[ModuleName].Archend`: Conventional name representing the architectural layer.
  - `Olympus.[ModuleName].Backend`: Conventional name representing the server layer.
  - `Olympus.[ModuleName].Frontend`: Conventional name representing the client layer.
- **Hosts**: Contains the main application host projects.
  - `Olympus.Api`: ASP.NET Core Web API (Application Package).
  - `Olympus.Api.Host`: ASP.NET Core Web API (Application Host).
  - `Olympus.Web`: Blazor WebAssembly App (Application Package).
  - `Olympus.Web.Host`: Blazor WebAssembly App (Application Host).
- **Aspire**: Contains projects for support to Aspire orchestration and hosting.
  - `Olympus.Aspire`: Aspire defaults services project.
  - `Olympus.Aspire.Host`: Aspire AppHost project.

## Technology Stack
- **Platform**: Olympus is built entirely on the .NET ecosystem.
- **Language**: Mainly C# with some JavaScript in Frontend projects.
- **Backend**: ASP.NET Core Web API with Entity Framework Core.
- **Frontend**: Blazor WebAssembly with the Radzen Component Library.
- **Database**: PostgreSQL with HybridCache through EF Core Second Level Interceptor and Redis.
- **Authentication**: Microsoft Identity Platform with support for Microsoft 365 OIDC.

## Modular Strategy and Workflow
- The Core and each module follows a three-layer structure: `Archend` → `Backend` → `Frontend`.
- All projects, except for the host projects, function only as libraries and are packaged as NuGet packages.
- The goal is to allow customization in client repositories by having them implement only the host projects and consume the others as packages.

## Naming Conventions
- Use camelCase for variables.
- Use PascalCase for constants.
- Use PascalCase for classes, interfaces, properties, methods, functions, etc.

## Formatting and Code Style
- Use the K&R style for code formatting.
- Keep only one statement per line unless it exceeds 160 characters.
- Insert a blank line immediately after '{' and immediately before '}' in all code blocks.
- For code blocks that fit within 160 characters, prefer expression-bodied members.
- Keep methods small and focused, and avoid using async void methods (except for event handlers).
- Always use file-scoped namespaces following the pattern Olympus.{ProjectName}.{ProjectLayer}.
- Avoid magic values, implicit behavior, and hidden side effects.
- Always add a parameterless constructor for entities and value objects if required by EF Core.
- Avoid comments unless strictly necessary; keep the code self-explanatory.
- For lambda expressions, prefer descriptive parameter names like `item` over single-letter ones like `i`.
- Write comments and string messages in English; keep text short, simple, and direct.
- Always use UTC for any type of date/time value.
