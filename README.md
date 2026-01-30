# Olympus
[![Current Version](https://raw.githubusercontent.com/olympus-app/badges/Olympus/version.svg)](../../releases)
[![Last Updated](https://raw.githubusercontent.com/olympus-app/badges/Olympus/updated.svg)](../../releases)
[![Total of Files](https://raw.githubusercontent.com/olympus-app/badges/Olympus/files.svg)](./README.md)
[![Lines of Code](https://raw.githubusercontent.com/olympus-app/badges/Olympus/lines.svg)](./README.md)

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

## Modules Definitions
- The system is divided into four distinct categories of modules, each representing a core area of corporate management.
- Each module follows a three-layer structure: `Archend` → `Backend` → `Frontend`.

### Infrastructure Modules
These modules provide foundational, cross-cutting services that support the entire system.
- **Core**: Manages core entities (users, employees, etc.) shared across the system.
- **Cronos**: Provides services for messaging, event handling, and notifications.
- **Gaia**: Provides artificial intelligence (AI) services to other modules.
- **Hades**: Manages data lifecycle, archiving, and historical records.
- **Hermes**: Provides centralized communication services (e.g., email, alerts).
- **Zeus**: Handles system administration, access control, and global settings.

### Administrative Modules
Modules focused on back-office and corporate management functions.
- **Apollo**: Manages the knowledge base and organizational documentation.
- **Eleuthia**: Manages human resources, payroll, and the employee lifecycle.
- **Minerva**: Manages commercial operations, sales, and customer relations.
- **Athena**: Handles financial operations, billing, and accounting.
- **Demeter**: Handles procurement, purchasing, and management of supplies.

### Operational Modules
Modules that represent the company's core value-delivery operations.
- **Atlas**: Manages warehouse operations and inventory control.
- **Ares**: Controls service delivery, requests, and operational orders.
- **Hephaestus**: Controls manufacturing operations and production lines.
- **Charon**: Controls logistics, transport, and supply chain operations.

### Technical Support Modules
Modules that support and enhance the primary business operations through engineering, quality, and maintenance.
- **Aether**: Handles processes and controls for quality assurance.
- **Artemis**: Manages occupational health and safety (OHS) processes.
- **Daedalus**: Manages engineering projects, designs, and technical specifications.
- **Poseidon**: Handles facilities management and physical asset maintenance.
- **Prometheus**: Manages continuous improvement initiatives and process optimization.
- **Talos**: Controls maintenance, work orders, and repairs.
