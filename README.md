# VinylHub

A web application for browsing and borrowing vinyl records, built with **ASP.NET Core MVC (.NET 8)** and Clean Architecture.

I made this project for the **Business Application Development** course in my second year of Applied Computer Science at Odisee. The assignment was to choose my own project, and I chose vinyl records because I love vinyl and music.

> The user interface is in Dutch, the code is in English.

<!-- TODO: screenshots -->


## Features

**Users**
- Browse the collection and filter by artist or genre
- View vinyl details and go to the artist page (photo and all their vinyls)
- Borrow a vinyl for 1 to 30 days. The due date is set automatically and the vinyl becomes unavailable
- View and return your own loans
- Register and log in

**Admins**
- Add, edit and delete vinyls, artists and genres
- Upload cover images and artist photos
- View all loans and filter them by status (Active, Returned, Overdue)
- Overdue loans are detected automatically

## Tech stack

- C#, .NET 8, ASP.NET Core MVC
- Entity Framework Core (code first, migrations)
- SQL Server (LocalDB)
- ASP.NET Core Identity
- Razor Views and Bootstrap
- Visual Studio

## Architecture

The solution is split into four projects (Clean Architecture):

| Project | Responsibility |
| --- | --- |
| `VinylHub.Domain` | Entities (`Vinyl`, `Artist`, `Genre`, `Loan`). No dependencies. |
| `VinylHub.Application` | Interfaces and services with the business logic (for example the loan rules) |
| `VinylHub.Infrastructure` | `DbContext`, repositories, Unit of Work, migrations and seed data |
| `VinylHub.Web` | Controllers, views, ViewModels and the setup in `Program.cs` |

Main choices:
- Repository pattern with a specific interface per entity
- Unit of Work, so changes are saved with one `SaveChanges`
- Business logic in services, so controllers stay small
- ViewModels, so entities are never sent directly to the views
- Claims instead of roles for admin access, as seen in class
- Anti-forgery tokens on every POST (CSRF protection)

## Running the project

You need the [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) and SQL Server LocalDB (included with Visual Studio).

1. Clone the repository:
   ```bash
   git clone https://github.com/<your-username>/<repository-name>.git
   ```
2. The connection string (`Default`) is in `VinylHub.Web/appsettings.json` and uses LocalDB. Change it if you use another SQL Server.
3. Create the database:
   ```bash
   dotnet ef database update --project VinylHub.Infrastructure --startup-project VinylHub.Web
   ```
   In Visual Studio you can also run `Update-Database` in the Package Manager Console (default project: `VinylHub.Infrastructure`, startup project: `VinylHub.Web`).
4. Start the application:
   ```bash
   dotnet run --project VinylHub.Web
   ```

On startup, the app adds some genres, artists and two demo accounts:

| Role | Email | Password |
| --- | --- | --- |
| Admin | `admin@vinylhub.be` | `Admin123!` |
| User | `user@vinylhub.be` | `User123!` |

These accounts are only meant for local testing.

## What I learned

- Splitting an application into layers with clear responsibilities
- Using the repository and Unit of Work patterns with EF Core
- Keeping business rules in services instead of controllers
- Securing an app with Identity, claims and policies
- Explaining and defending my own design choices
