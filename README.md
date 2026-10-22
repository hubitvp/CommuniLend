# CommuniLend

**CommuniLend** is a neighborhood tool library built with ASP.NET Core MVC. Members list tools they own, browse tools shared by their neighbors, borrow them for a chosen period, and keep a history of what they have lent and borrowed.

## Main Features

- **Accounts** – register, log in and log out with ASP.NET Core Identity. Registration also collects full name and address.
- **Tool catalog** – browse tools shared by other active members, with category, owner, photo and availability status. Your own tools are not shown in the catalog.
- **Tool management (full CRUD)** – add, view, edit and delete your own tools, including photo upload, category, condition and replacement value. Tools that are currently on loan cannot be edited or deleted.
- **Loans** – borrow an available tool by choosing a loan date and due date (with optional notes). The owner marks the tool as returned, which moves the loan into history.
- **Loan overview** – see the tools you have provided and the tools you have borrowed, with their status.
- **Dashboard** – your profile, owned tools, active loans and past loans (provided and borrowed).
- **Member profiles** – view other members and the tools they own.
- **Edit profile** – update your name, address and details.
- **Validation** – client-side (jQuery Validation / Unobtrusive) and server-side (data annotations, `IValidatableObject` and service-level checks).
- **Seed data** – 3 categories, 7 members and about 50 tools are created by the migrations.

## Technologies

- ASP.NET Core MVC, **.NET 10**
- Entity Framework Core 10 with **SQL Server**
- ASP.NET Core Identity (Razor Pages UI for account pages)
- Razor views with a shared layout, partial views and sections
- Bootstrap 5, jQuery, jQuery Validation
- Dependency Injection with service interfaces

## Project Structure

| Project | Purpose |
|---|---|
| `CommuniLend.Web` | MVC controllers, Razor views, Identity area, static files, `Program.cs` |
| `CommuniLend.Web.ViewModels` | View and input models used by the controllers and views |
| `CommuniLend.Services` | Business logic behind interfaces (`IToolsService`, `ILoansService`, `IUsersService`) |
| `CommuniLend.Data` | `CommuniLendDbContext`, entity configurations, seed data, migrations |
| `CommuniLend.Data.Models` | Entities: `ApplicationUser`, `Tool`, `Category`, `OpenLoan`, `ClosedLoan` |
| `CommuniLend.DataModelConstraints` | Shared validation and column-type constants |

### Entities

- **ApplicationUser** – extends `IdentityUser` with full name, address, join date and active flag
- **Tool** – name, description, condition, replacement value, image, date added, category, owner
- **Category** – name and description
- **OpenLoan** – an active loan of a tool (borrower, owner, loan date, due date, notes)
- **ClosedLoan** – the history record created when a loan is returned

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server (LocalDB, Express or a full instance)
- EF Core CLI tools: `dotnet tool install --global dotnet-ef`

## Setup

1. **Clone the repository**

   ```bash
   git clone <your-repository-url>
   cd CommuniLend
   ```

2. **Configure the connection string**

   The application reads the connection string named `DefaultConnection`. The default in `CommuniLend.Web/appsettings.json` uses SQL Server LocalDB:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CommuniLendDbContext;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```

   To use another SQL Server, override it without editing the file, using user secrets:

   ```bash
   cd CommuniLend.Web
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=CommuniLend;User Id=sa;Password=<your-password>;TrustServerCertificate=True"
   ```

   or with an environment variable: `ConnectionStrings__DefaultConnection`.

3. **Restore packages and apply migrations**

   ```bash
   dotnet restore
   dotnet ef database update --project CommuniLend.Data --startup-project CommuniLend.Web
   ```

4. **Run the application**

   ```bash
   dotnet run --project CommuniLend.Web --launch-profile https
   ```

   The app is available at `https://localhost:7063` (or `http://localhost:5140`).

## Environment and Credentials

| Item | Value |
|---|---|
| Connection string key | `ConnectionStrings:DefaultConnection` (env var: `ConnectionStrings__DefaultConnection`) |
| Environment | `ASPNETCORE_ENVIRONMENT=Development` (set by `launchSettings.json`) |
| Seeded user password | `Password123!` |

### Seeded test accounts

All use the password `Password123!`:

| Name | Email |
|---|---|
| John Smith | john.smith@email.com |
| Sarah Johnson | sarah.johnson@email.com |
| Michael Chen | michael.chen@email.com |
| Emily Rodriguez | emily.rodriguez@email.com |
| David Williams | david.williams@email.com |
| Jessica Martinez | jessica.martinez@email.com |
| Robert Taylor | robert.taylor@email.com |

You can also register a new account from the **Register** page. Passwords only need 3 characters in this development setup.

## Notes

- Uploaded tool photos are stored in `CommuniLend.Web/wwwroot/images/tools/` and are limited to 50 KB per file.
- Dates are handled with `DateOnly`; a loan must start in the future and end after it starts.

## License

See the [LICENSE](LICENSE) file.
