# Olympus
[![Current Version](https://raw.githubusercontent.com/olympus-app/badges/Olympus/version.svg)](../../releases)
[![Last Updated](https://raw.githubusercontent.com/olympus-app/badges/Olympus/updated.svg)](../../releases)
[![Total of Files](https://raw.githubusercontent.com/olympus-app/badges/Olympus/files.svg)](./README.md)
[![Lines of Code](https://raw.githubusercontent.com/olympus-app/badges/Olympus/lines.svg)](./README.md)

# Olympus
Olympus is a modular monolithic software system inspired by Greco-Roman mythology, designed to unify and integrate various corporate processes such as infrastructure, administrative, operational, and technical support. Instead of following a traditional multi-tenant SaaS model, Olympus enables individualized deployments per client, allowing deep customization via isolated forks and environment-specific configurations.

## Architecture and Tech Stack
The core of Olympus is a collection of NuGet packages, where each package represents a specific business capability or foundational service. The applications themselves (hosts) are minimal "shells" that consume these packages.

This strategy allows for maximum flexibility: a client can build a highly customized application by simply creating a host project and selecting which Olympus modules to include as dependencies.

This architecture promotes high cohesion and low coupling between different business domains through a clear separation of modules.

The main technologies used are:
- **Platform**: Olympus is built entirely on the .NET ecosystem.
- **Backend**: ASP.NET Core Web API, Entity Framework Core, and OData for flexible data queries.
- **Frontend**: Blazor, supporting both Blazor WebAssembly (WASM) for web applications and .NET MAUI for cross-platform hybrid apps.
- **Databases**: Natively supports SQL Server, PostgreSQL, and SQLite.
- **Authentication**: JSON Web Tokens (JWT) with built-in support for Microsoft 365 OAuth.

## Modules Definitions
The system is divided into four distinct categories of modules, each representing a core area of corporate management.

### Infrastructure Modules
These modules provide foundational, cross-cutting services that support the entire system.
- Core: manages core entities (Users, Employees, etc.) shared across the system.
- Cronos: provides services for messaging, event handling, and - notifications.
- Gaia: provides Artificial Intelligence (AI) services to other modules.
- Hades: manages data lifecycle, archiving, and historical records.
- Hermes: provides centralized communication services (e.g., email, alerts).
- Zeus: handles system administration, access control, and global settings.

### Administrative Modules
Modules focused on back-office and corporate management functions.
- Apollo: manages the knowledge base and organizational documentation.
- Athena: handles financial operations, billing, and accounting.
- Demeter: handles procurement, purchasing, and management of supplies.
- Eleuthia: manages human resources, payroll, and the employee lifecycle.
- Minerva: manages commercial operations, sales, and customer relations.

### Operational Modules
Modules that represent the company's core value-delivery operations.
- Ares: controls service delivery, requests, and operational orders.
- Atlas: manages warehouse operations and inventory control.
- Charon: controls logistics, transport, and supply chain operations.
- Hephaestus: controls manufacturing operations and production lines.

### Technical Support Modules
Modules that support and enhance the primary business operations through engineering, quality, and maintenance.
- Aether: handles processes and controls for quality assurance.
- Artemis: manages occupational health and safety (OHS) processes.
- Daedalus: manages engineering projects, designs, and technical specifications.
- Poseidon: handles facilities management and physical asset maintenance.
- Prometheus: manages continuous improvement initiatives and process optimization.
- Talos: controls equipment maintenance, work orders, and technical repairs.
