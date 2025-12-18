---
applyTo: "**"
---

# Project Scope
Olympus Project is a modular monolithic software inspired by Greco-Roman mythology.
It's designed to unify and integrate various corporate processes such as infrastructure, administrative, operational, and technical support.

## Project Architecture
The project is built on the .NET ecosystem and has three main solution folders:
- **Core**: contains foundational & utility code, contracts and abstractions used by all projects.
- **Hosts**: contains main application projects such as ASP.NET Core Web API and Blazor WebAssembly App.
- **Modules**: contains projects for each capability of the system, such as administrative, operational, and technical support processes.

The **Core** solution folder contains the following projects:
- **Olympus.Core**: utility code and base functionalities shared across all projects.
- **Olympus.Core.Archend**: models (DTOs), shared with backend and frontend layers.
- **Olympus.Core.Backend**: data access and processing such as entities, services and controllers.
- **Olympus.Core.Frontend**: user interface such as pages, components and assets.

The **Hosts** solution folder contains the following projects:
- **Olympus.Api**: ASP.NET Core Web API (Application Package).
- **Olympus.Api.Host**: ASP.NET Core Web API (Application Host).
- **Olympus.Web**: Blazor WebAssembly App (Application Package).
- **Olympus.Web.Host**: Blazor WebAssembly App (Application Host).

Each module in the **Modules** solution folder is subdivided into three layers:
- **Archend**: conventional name representing the architectural layer.
- **Backend**: conventional name representing the server layer.
- **Frontend**: conventional name representing the client layer.

## System Modules
### Infrastructure Modules
- **Core**: manages core entities (users, employees, etc.) shared across the system.
- **Cronos**: provides services for messaging, event handling, and notifications.
- **Gaia**: provides artificial intelligence (AI) services to other modules.
- **Hades**: manages data lifecycle, archiving, and historical records.
- **Hermes**: provides centralized communication services (e.g., email, alerts).
- **Zeus**: handles system administration, access control, and global settings.

### Administrative Modules
- **Apollo**: manages the knowledge base and organizational documentation.
- **Eleuthia**: manages human resources, payroll, and the employee lifecycle.
- **Minerva**: manages commercial operations, sales, and customer relations.
- **Athena**: handles financial operations, billing, and accounting.
- **Demeter**: handles procurement, purchasing, and management of supplies.

### Operational Modules
- **Atlas**: manages warehouse operations and inventory control.
- **Ares**: controls service delivery, requests, and operational orders.
- **Hephaestus**: controls manufacturing operations and production lines.
- **Charon**: controls logistics, transport, and supply chain operations.

### Technical Support Modules
- **Aether**: handles processes and controls for quality assurance.
- **Artemis**: manages occupational health and safety (OHS) processes.
- **Daedalus**: manages engineering projects, designs, and technical specifications.
- **Poseidon**: handles facilities management and physical asset maintenance.
- **Prometheus**: manages continuous improvement initiatives and process optimization.
- **Talos**: controls maintenance, work orders, and repairs.

## Technology Stack
- **Language**: mainly C# and some JavaScript in Frontend projects.
- **Backend**: ASP.NET Core Web API with Entity Framework Core.
- **Frontend**: Blazor WebAssembly with Radzen Component Library.
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
