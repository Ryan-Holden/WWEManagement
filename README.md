# WWE Management

A full-stack .NET portfolio application for managing WWE employees, wrestlers, and events.

The project was built to practice and demonstrate practical .NET development skills including ASP.NET Core MVC, REST APIs, Entity Framework Core, SQL Server, dependency injection, repository and service patterns, validation, and automated testing.

## Features

### Employee Management

- View all employees
- View employee details
- Create employees
- Edit employees
- Delete employees
- Prevent deletion when related records depend on an employee

### Wrestler Management

- View wrestler records
- View combined wrestler and employee details
- Create a wrestler and associated employee record together
- Edit wrestler and employee information together
- Delete a wrestler and its associated employee record together
- Transaction-safe multi-entity persistence through EF Core

### Event Management

- View all events
- View event details
- Create, edit, and delete events
- Filter upcoming scheduled events
- Track event status as:
  - Scheduled
  - Completed
  - Cancelled

### REST API

The application also exposes API endpoints for Employees, Wrestlers, and Events.

Examples:

```text
GET    /api/employees
GET    /api/employees/{id}
POST   /api/employees
PUT    /api/employees/{id}
DELETE /api/employees/{id}

GET    /api/wrestlers
GET    /api/wrestlers/{id}
POST   /api/wrestlers
PUT    /api/wrestlers/{id}
DELETE /api/wrestlers/{id}

GET    /api/events
GET    /api/events/upcoming
GET    /api/events/{id}
POST   /api/events
PUT    /api/events/{id}
DELETE /api/events/{id}
```

The API uses appropriate HTTP responses such as `201 Created`, `204 No Content`, `400 Bad Request`, `404 Not Found`, and `409 Conflict`.

## Tech Stack

- .NET 10
- C#
- ASP.NET Core MVC
- ASP.NET Core Web API
- Entity Framework Core 10
- SQL Server
- Razor Views
- Bootstrap
- Dependency Injection
- xUnit
- Moq
- EF Core InMemory provider

## Architecture

The solution is split into three projects:

```text
WWEManagement
├── WWEManagement.Data
│   ├── Data / DbContext
│   ├── Models
│   └── Repositories
│
├── WWEManagement.Web
│   ├── Controllers
│   ├── API Controllers
│   ├── DTOs
│   ├── Services
│   ├── ViewModels
│   └── Views
│
└── WWEManagement.Tests
    ├── Controllers
    ├── Repositories
    └── Services
```

### Repository Pattern

The data layer defines repository interfaces:

```text
IEmployeeRepository
IEventRepository
IWrestlerRepository
```

with Entity Framework Core implementations:

```text
EfEmployeeRepository
EfEventRepository
EfWrestlerRepository
```

The application depends on repository abstractions rather than directly depending on persistence implementations.

### Service Layer

Wrestler operations use an additional service layer:

```text
IWrestlerService
WrestlerService
```

The service coordinates business logic between MVC/API inputs and the wrestler repository, including combined Employee and Wrestler creation and updates.

### Dependency Injection

Repository and service dependencies are registered in `Program.cs` and injected into controllers and services.

Example:

```csharp
builder.Services.AddScoped<IEmployeeRepository, EfEmployeeRepository>();
builder.Services.AddScoped<IEventRepository, EfEventRepository>();
builder.Services.AddScoped<IWrestlerRepository, EfWrestlerRepository>();
builder.Services.AddScoped<IWrestlerService, WrestlerService>();
```

## Data Model

The application uses three primary database entities.

### Employee

Stores employee information such as:

- First name
- Last name
- Job title
- Hire date
- Active status

### Wrestler

Stores wrestler-specific information such as:

- Employee relationship
- Ring name
- Weight class
- Debut date
- Active status

A wrestler is associated with an Employee through a one-to-one relationship.

### Event

Stores event information such as:

- Event name
- Date and time
- Venue
- City
- State
- Status

## Entity Framework Core

The application uses `WWEManagementDbContext` with SQL Server.

Read-only repository queries use `AsNoTracking()` where entity tracking is unnecessary.

Wrestler operations can affect both an Employee and Wrestler record. EF Core is used to persist these related changes together while preserving the relationship between the entities.

## Validation and Error Handling

The application uses:

- Data Annotations
- MVC model validation
- API DTO validation
- Database constraints
- `DbUpdateException` handling
- Friendly MVC validation messages
- REST-appropriate API status codes

Examples include:

- Required field validation
- Event status validation
- Route/entity ID validation
- Foreign-key conflict handling
- Not-found handling

## Automated Tests

The solution currently contains **12 automated tests**, covering the service, repository, and API layers.

Test areas include:

### Wrestler Service

- Creating wrestler and employee data
- Repository delegation
- Missing-record update handling

### Event Repository

- Event ordering
- Upcoming-event filtering
- Add operations
- Delete operations

### Wrestlers API

- Successful and missing GET requests
- `201 Created` responses
- Missing update handling
- Successful delete responses

Run the tests with:

```bash
dotnet test
```

Current result:

```text
Total tests: 12
Passed: 12
Failed: 0
```

## Getting Started

### Prerequisites

Install:

- .NET 10 SDK
- SQL Server
- SQL Server Management Studio or another SQL client
- Git

### Clone the Repository

```bash
git clone https://github.com/Ryan-Holden/WWEManagement.git
cd WWEManagement
```

### Database Configuration

The development configuration expects a local SQL Server database named:

```text
WWEManagement
```

The development connection string is:

```text
Server=localhost;Database=WWEManagement;Trusted_Connection=True;TrustServerCertificate=True;
```

It uses Windows authentication and does not contain a database password.

The application expects the database to contain the required `Employees`, `Wrestlers`, and `Events` tables.

> Note: automated database creation or migration scripts are not currently included in the repository, so the SQL Server schema must already exist before running the application.

### Build

```bash
dotnet build
```

### Test

```bash
dotnet test
```

### Run

```bash
dotnet run --project WWEManagement.Web
```

Open the local URL shown in the terminal.

## Screenshots

Screenshots can be added here before using the repository in job applications.

Suggested screenshots:

- Home dashboard
- Employees list
- Wrestlers list
- Events list
- Create/Edit form
- API response in Swagger, Postman, or a browser/API client

Example future structure:

```text
docs/
└── screenshots/
    ├── home.png
    ├── employees.png
    ├── wrestlers.png
    └── events.png
```

## Development History

The project began with direct ADO.NET data access to practice:

- `SqlConnection`
- `SqlCommand`
- `SqlDataReader`
- Parameterized SQL
- Joins
- Transactions
- CRUD operations

The application was later refactored to Entity Framework Core with repository interfaces and dependency injection. The obsolete ADO.NET implementations were removed after the EF Core implementation was tested and stable.

This progression provided experience with both lower-level SQL access and higher-level ORM-based application architecture.

## Key Skills Demonstrated

- C#
- ASP.NET Core MVC
- ASP.NET Core Web API
- SQL Server
- Entity Framework Core
- Relational data modeling
- Repository pattern
- Service layer design
- Dependency injection
- RESTful API design
- DTOs and ViewModels
- Server-side validation
- Exception handling
- Automated testing
- xUnit
- Moq
- Git and GitHub
- Refactoring and code cleanup

## Future Improvements

Possible future enhancements include:

- EF Core migrations or database setup scripts
- Async repository and controller methods
- Authentication and authorization
- Additional automated test coverage
- CI build/test workflow
- Search and filtering
- Pagination
- Deployment to a cloud environment
- A small legacy ASP.NET/Web Forms companion project for additional .NET framework experience

## Purpose

This project was created as a portfolio application to strengthen practical .NET development skills and demonstrate the ability to build, test, refactor, and organize a multi-layer ASP.NET application backed by SQL Server.
