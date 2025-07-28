# Olympus
[![Current Version](https://raw.githubusercontent.com/olympus-app/badges/Olympus/version.svg)](../../releases)
[![Last Updated](https://raw.githubusercontent.com/olympus-app/badges/Olympus/updated.svg)](../../releases)
[![Total of Files](https://raw.githubusercontent.com/olympus-app/badges/Olympus/files.svg)](./README.md)
[![Lines of Code](https://raw.githubusercontent.com/olympus-app/badges/Olympus/lines.svg)](./README.md)

Olympus is a modular monolithic web application designed to support multiple organizations through fully customized builds. Instead of following a traditional multi-tenant SaaS model, Olympus enables individualized deployments per client, allowing deep customization via isolated forks and environment-specific configurations.

## Architecture and Tech Stack

Olympus is divided into three subprojects:

- **Shared Project**: a shared class library used by both the Server and Client projects. It contains common definitions such as system constants, data models, and Data Transfer Objects (DTOs).

- **Server Project**: an ASP.NET Core Web API that serves as the backend for Olympus. It provides data endpoints and also hosts and serves the client application.

- **Client Project**: a Blazor WebAssembly frontend built entirely with Client-Side Rendering (CSR), with support for Progressive Web Application (PWA) features.

## Development Environment

The project is being developed by a solo full-stack developer who is currently deepening expertise in the chosen tech stack. Development is conducted on a Windows environment using:

- **Visual Studio Code** for source editing.
- **Docker Desktop with WSL 2** for running services as database and cache.
- **.NET 9.0 with C# 13** as the core development platform.
- **Git and GitHub** for source control and version management.

## File Structure

The folder and file organization follows a suggested (but not strictly enforced) pattern for each subproject:

```plaintext
Shared
└── [ObjectType]
    └── [ObjectName].cs
```
```plaintext
Server
├── [SystemModuleName]
│   └── [ObjectType]
│       └── [ObjectName].cs
└── Features
    └── [FeatureName]
        └── [ObjectType]
            └── [ObjectName].cs
```
```plaintext
Client
├── [SystemModuleName]
│   └── [ObjectType]
│       └── [ObjectName].cs
└── Modules
    └── [ClientModuleName]
        └── [FeatureName]
            └── [ObjectType]
                └── [ObjectName].cs
```

Namespaces are strictly defined according to the following convention:
```plaintext
Olympus.[ProjectName].[SystemModuleName or ClientModuleName]
```

## Configuration

**Current Status**: both the Server and Client projects have their own `appsettings.json` files with basic build settings such as `AllowedHosts` and `LogLevel`. Sensitive information like connection strings, client IDs, and client secrets are managed via User Secrets stored locally on the developer's machine.

**Pending Tasks**: a dedicated configuration module needs to be designed and implemented to manage different layers of settings, including:

- **Build-time configurations**: determine which modules are included in each client's build (read-only via Client).
- **Global system settings**: general system-level configurations (editable via Client).
- **Module-specific settings**: per-module configurations (editable via Client).
- **User settings**: individual user preferences (editable via Client).

These configurations must be accessible *before* database initialization. Options under consideration include using file-based storage (e.g., JSON files) or a lightweight SQLite database under a `Configuration` folder on the server.

Additionally, the project still requires a well-defined strategy for client isolation, such as whether to rely on Git forks or purely on build-time configuration files. A secure mechanism must also be established to enforce module access restrictions for clients who opt out of certain features.

## Authentication and Authorization

Authentication is currently implemented using Microsoft Entra ID as the primary identity provider. The flow works as follows:

- The client (either the Blazor Client or any API consumer) obtains an access token by signing in with a Microsoft account within the client's Azure AD tenant.

- This requires an Azure App Registration with delegated permission for **Microsoft Graph: User.Read**, and two custom API scopes: **Olympus.Read** and **Olympus.Write**.

- The Client application must also have its own App Registration with the same delegated permission and access to the exposed scopes from the Server app registration.

For development, these apps are registered in a dedicated development tenant. Azure AD settings like `Instance`, `Domain`, `TenantId`, `ClientId`, `ClientSecret`, and `Audience` are stored as User Secrets in the Server project, while `TenantId` and `ClientId` are stored in the Client project's User Secrets.

Every request to the Server includes the access token, which is validated. If the user is not yet provisioned, the system auto-provisions them. This is handled by a scoped `UserProviderMiddleware`, which coordinates one or more user providers implementing the `IUserProvider` interface.

Currently, only the `MicrosoftUserProvider` is implemented. It extracts basic claims (name, email, object ID) from the token and queries Microsoft Graph (using client secret) to retrieve additional attributes such as `jobTitle`, `department`, and user profile photo.

**Pending Tasks**:

- Implement a local username/password authentication option (with token issuance handled by the Server).

- Implement a **Role-based authorization system**:
  - Define and assign roles.
  - Ensure roles are added to the user's claims in both Microsoft-issued and locally issued tokens.
  - Enforce role-based access on the Server (API) and Client (UI) using policies defined in the Shared project.

- **Determine how roles will be transmitted to the Client**:
  - Either by returning a modified token containing role claims,
  - Or by exposing a dedicated GET endpoint to retrieve the user's role set after authentication.

## Data Modeling

The project adopts a Code-First approach for data modeling using the Table-per-Type (TPT) inheritance strategy.

- **Domain models**, **entities**, and **value objects** (i.e., components containing business logic) are scoped exclusively to the Server project, with restricted access (`protected` setters).

- **Data Transfer Objects (DTOs)**, **enums**, and any shared classes that do not encapsulate business logic are defined in the Shared project with open access (`public` setters).

All persistable entities must inherit from `BaseEntity`, which implements the following interfaces:

- `IAuditableEntity` – for audit fields (Created/Updated)
- `IActivableEntity` – for active/inactive state
- `IHideableEntity` – for public visibility control (hidden/unhidden)

Entities must be registered in the `DefaultDatabaseContext`, and have a corresponding `ModelBuilder` to define their structure in both Entity Framework and the OData protocol.

Each entity must also have an associated **repository**, responsible for encapsulating data access logic. Repositories may be used by **data services**, which orchestrate interactions between entities and DTOs. Services are in turn called by **controllers**, which manage route execution and coordinate service actions.

**Access flow (Client to Entity)**:
```
Route → Controller → Service → Repository → Entity
```

**Pending Tasks**:
Establish a consolidated mechanism to register entities in both Entity Framework and OData with minimal code duplication (applying DRY principles), while ensuring structural consistency.

## Database

Data access is handled via SQL Server using `DefaultDatabaseContext` with Entity Framework Core.
All models must be registered in the default context and have a valid model configuration to be usable.

**Pending Tasks**:
- Implement caching for low-volatility data using Redis
- Evaluate the need for a separate SQLite context for configuration storage

## Routing

Routing in the **Server** is defined via controllers, while in the **Client** it is handled through Blazor pages.

For data access, the project uses the **OData protocol** to simplify and accelerate API development. As a result, the system exposes three main route types:

- `/api/*` – API routes for internal system operations, such as `/api/version` and `/api/health`
- `/odata/*` – OData routes for accessing entity data, such as `/odata/users/{id}`
- `/*` – Fallback route serving the client application (used for page rendering and handling non-existent routes)


## Internationalization

All three projects are culture-agnostic, written in international English with response messages in English and dates/times in UTC. Only the Client project will implement localization for supported languages beyond English, initially including Brazilian Portuguese.

*Note: the localization approach still requires research, testing, and definition.*

## Error Handling

This section is yet to be developed.

## Logging

This section is yet to be developed.

## Testing

This section is yet to be developed.
