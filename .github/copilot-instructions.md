---
applyTo: "**"
---
# Project Scope
Olympus Project is a modular monolithic software inspired by Greco-Roman mythology.
It's designed to unify and integrate various corporate processes such as infrastructure, administrative, operational, and technical support.

The project is built on the .NET ecosystem and has four solutions folders:
- Apps: holds the base projects of the host apps such as ASP.NET Core Web API, Blazor WebAssembly App and MAUI Blazor Hybrid App.
- Hosts: holds the projects for the apps themselves, the host acts as a "shell" with minimum code and consuming code from its counterpart in the Apps folder.
- Modules: holds the projects for each business capability of the system, such as administrative, operational, and support processes.
- Libraries: holds reusable code and core functionalities utilized by other projects.

The solution folders contain the following projects:
- Apps: Api (ASP.NET Core Web API), Native (MAUI Blazor Hybrid App), and Web (Blazor WebAssembly App).
- Hosts: Api (ASP.NET Core Web API), Native (MAUI Blazor Hybrid App), and Web (Blazor WebAssembly App).
- Modules: each module is subdivided into three layers: Archend (business models and entities), Backend (data access and processing), and Frontend (UI).
- Libraries: Shared (utility code, like helpers and extensions) and Kernel (foundational functionalities like configuration, localization, contracts, etc.).

The infrastructure modules are:
- Core: manages core entities (users, employees, etc.) shared across the system.
- Cronos: provides services for messaging, event handling, and notifications.
- Gaia: provides artificial intelligence (AI) services to other modules.
- Hades: manages data lifecycle, archiving, and historical records.
- Hermes: provides centralized communication services (e.g., email, alerts).
- Zeus: handles system administration, access control, and global settings.

The administrative modules are:
- Apollo: manages the knowledge base and organizational documentation.
- Eleuthia: manages human resources, payroll, and the employee lifecycle.
- Minerva: manages commercial operations, sales, and customer relations.
- Athena: handles financial operations, billing, and accounting.
- Demeter: handles procurement, purchasing, and management of supplies.

The operational modules are:
- Atlas: manages warehouse operations and inventory control.
- Ares: controls service delivery, requests, and operational orders.
- Hephaestus: controls manufacturing operations and production lines.
- Charon: controls logistics, transport, and supply chain operations.

The technical support modules are:
- Aether: handles processes and controls for quality assurance.
- Artemis: manages occupational health and safety (OHS) processes.
- Daedalus: manages engineering projects, designs, and technical specifications.
- Poseidon: handles facilities management and physical asset maintenance.
- Prometheus: manages continuous improvement initiatives and process optimization.
- Talos: controls maintenance, work orders, and repairs.

## Modular Strategy
- All projects, except for the host ones, are packaged as NuGet packages.
- The goal is to allow customization in a client's repository by having them implement only the host "shell" projects and consuming the others as packages.

## Project Stack
- Language: mainly C# and some JavaScript in Frontend projects.
- Backend: ASP.NET Core Web API with Entity Framework and OData support.
- Frontend: Blazor Hybrid with Web project (using WASM) with Radzen Component Library.
- Database: support for SQL Server, PostgreSQL and SQLite with in-memory caching service.
- Authentication: JSON Web Token with support for Microsoft 365 OAuth.

## Naming Conventions
- Use camelCase for variables
- Use PascalCase for constants
- Use PascalCase for classes, interfaces, properties, methods, functions, etc.

## Code Styling and Formatting
- Use K&R style for code formatting.
- Always keep only one statement per line, unless it exceeds 160 characters.
- Insert a blank line inside the start and end of all code blocks ({}), including constructors, methods, control flow structures, and object initializations.
- For code blocks that fit within 160 characters (e.g., one-liner methods or operators), prefer writing them on a single line or using expression-bodied members.
- For empty blocks (e.g., constructors like public Foo() {}), prefer using a single-line format.
- Avoid overly compact code. Only group directly related statements; otherwise, separate them with blank lines.
- Keep methods small, focused, and avoid using async void methods (except for event handlers).
- Always use file-scoped namespaces based on pattern Olympus.{ProjectName}.{ProjectLayer}, subfolders are for organization only, not namespace depth.
- Prefer clean, well-organized, and optimized code that follows industry standards and best practices, while remaining readable and easy to understand.
- Avoid magic values, implicit behavior, and hidden side effects. Make dependencies and logic as clear as possible.
- Always add an empty constructor, if needed, for Entities and Value Objects for proper EF Core use.
- Avoid comments unless strictly necessary. Keep the code self-explanatory and clean.
- Always write comments and string messages in English. Prefer short, simple and direct texts.
- Always use UTC for any kind of time or datetime values.
