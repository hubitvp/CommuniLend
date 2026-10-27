# 🛠️ CommuniLend

> A neighborhood tool library where members share the tools they own, borrow from their neighbors, and keep a history of every loan.

![.NET Version](https://img.shields.io/badge/.NET-10.0-purple)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-10.0-blue)
![License](https://img.shields.io/badge/license-MIT-green)

---

## 📋 Table of Contents

- [About the Project](#about-the-project)
- [Technologies Used](#technologies-used)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [Features](#features)
- [Usage](#usage)
- [Database Setup](#database-setup)
- [Configuration](#configuration)
- [Contributing](#contributing)
- [License](#license)
- [Contact](#contact)

---

## 📖 About the Project

**CommuniLend** is a neighborhood tool library built with ASP.NET Core MVC. Members list tools they own, browse tools shared by their neighbors, borrow them for a chosen period, and keep a history of what they have lent and borrowed. It solves the problem of buying rarely used tools by letting a community share them, and demonstrates a layered architecture with Entity Framework Core, ASP.NET Core Identity and service-based business logic.

---

## 🛠️ Technologies Used

| Technology                      | Version | Purpose                                         |
|---------------------------------|---------|-------------------------------------------------|
| ASP.NET Core MVC                | 10.0    | Web framework                                   |
| Entity Framework Core           | 10.0    | ORM / Database access (Code-First)              |
| SQL Server (LocalDB or full)    | -       | Database                                        |
| ASP.NET Core Identity           | 10.0    | Authentication and accounts (Razor Pages UI)    |
| Bootstrap                       | 5       | Frontend styling                                |
| jQuery + jQuery Validation      | -       | Client-side validation                          |
| Razor Views / Partials          | -       | Server-side HTML rendering                      |
| Dependency Injection            | -       | Service interfaces registered in `Program.cs`   |

---

## ✅ Prerequisites

Make sure you have the following installed before running the project:

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server) (LocalDB, Express or a full instance)
- EF Core CLI tools: `dotnet tool install --global dotnet-ef`
- [Git](https://git-scm.com/)

---

## 🚀 Getting Started

Follow these steps to get the project running locally.

### 1. Clone the repository

```bash
git clone https://github.com/hubitvp/CommuniLend.git
cd CommuniLend
```

### 2. Restore dependencies

```bash
dotnet restore
```

### 3. Apply database migrations

```bash
dotnet ef database update --project CommuniLend.Data --startup-project CommuniLend.Web
```

### 4. Run the application

```bash
dotnet run --project CommuniLend.Web --launch-profile https
```

The app will be available at `https://localhost:7063` or `http://localhost:5140`.

---

## 📁 Project Structure

```
CommuniLend/
│
├── CommuniLend.Web/                  # MVC controllers, Razor views, Identity area, Program.cs
│   ├── Controllers/                  # ToolsController, LoansController, UsersController
│   ├── Views/                        # Razor Views (.cshtml) and partials
│   ├── Areas/Identity/               # Register, Login, Logout pages
│   ├── wwwroot/                      # Static files (CSS, JS, tool images)
│   ├── appsettings.json              # App configuration
│   └── Program.cs                    # App entry point and middleware setup
├── CommuniLend.Web.ViewModels/       # View and input models
├── CommuniLend.Services/             # Business logic behind interfaces
├── CommuniLend.Data/                 # DbContext, configurations, seed data, migrations
├── CommuniLend.Data.Models/          # Entities: ApplicationUser, Tool, Category, OpenLoan, ClosedLoan
├── CommuniLend.DataModelConstraints/ # Shared validation and column-type constants
└── CommuniLend.slnx                  # Solution file
```

| Project | Purpose |
|---|---|
| `CommuniLend.Web` | MVC controllers, Razor views, Identity area, static files, `Program.cs` |
| `CommuniLend.Web.ViewModels` | View and input models used by the controllers and views |
| `CommuniLend.Services` | Services (`IToolsService`, `ILoansService`, `IUsersService`, `IImageStorageService`) |
| `CommuniLend.Data` | `CommuniLendDbContext`, entity configurations, seed data, migrations |
| `CommuniLend.Data.Models` | Domain entities |
| `CommuniLend.DataModelConstraints` | Shared validation and column-type constants |

### Entities

- **ApplicationUser** – extends `IdentityUser` with full name, address, join date and active flag
- **Tool** – name, description, condition, replacement value, image, date added, category, owner
- **Category** – name and description
- **OpenLoan** – an active loan of a tool (borrower, owner, loan date, due date, notes)
- **ClosedLoan** – the history record created when a loan is returned

---

## ✨ Features

- [x] User registration and login (ASP.NET Core Identity), collecting full name and address
- [x] Tool catalog with category, owner, photo and availability status (your own tools are hidden)
- [x] Search by tool name and filter by category, with selected values preserved
- [x] Full CRUD for tools, including photo upload, category, condition and replacement value
- [x] Loans: borrow a tool for a chosen period; the owner marks it as returned
- [x] Dashboard with profile, owned tools, active loans and loan history
- [x] Member profiles, profile editing and account deletion
- [x] Input validation (server-side & client-side)
- [x] Seed data and database column comments via migrations
- [x] Responsive UI with Bootstrap

---

## 💻 Usage

```
1. Open the app and go to Register to create an account (or log in with a seeded account).
2. Browse the catalog on the home page; search by name or filter by category.
3. Open a tool and choose "Borrow" – pick a loan date and due date, optionally add notes.
4. Add your own tools from the Tools menu (photo, category, condition, replacement value).
5. Use the Loans page to see tools you provided and borrowed; mark provided tools as returned.
6. Use the Dashboard to view your profile, active loans and loan history, edit your details or delete your account.
```

**Rules to know:**

- Tools that are currently on loan cannot be edited or deleted.
- A loan must start in the future and end after it starts.
- Deleting your account is blocked while you have open loans (as lender or borrower); otherwise your tools, their photos and your loan history are removed.
- Uploaded photos must be JPG or PNG (validated by extension and file signature) and are limited to 50 KB.

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

The migrations create 3 categories, 7 members and about 50 tools.

---

## 🗄️ Database Setup

The project uses **Entity Framework Core** with a Code-First approach and **SQL Server**.

The connection string `DefaultConnection` is configured in `CommuniLend.Web/appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CommuniLend;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

To create and seed the database:

```bash
dotnet ef database update --project CommuniLend.Data --startup-project CommuniLend.Web
```

To add a new migration after changing the models:

```bash
dotnet ef migrations add <MigrationName> --project CommuniLend.Data --startup-project CommuniLend.Web
```

---

## ⚙️ Configuration

Key settings in `CommuniLend.Web/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CommuniLend;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "Identity": {
    "Password": {
      "RequiredLength": 8,
      "RequireDigit": true,
      "RequireLowercase": true,
      "RequireUppercase": true,
      "RequireNonAlphanumeric": false,
      "RequiredUniqueChars": 4
    }
  }
}
```

`appsettings.Development.json` relaxes the password rules (minimum 3 characters) for local testing. `launchSettings.json` sets `ASPNETCORE_ENVIRONMENT=Development`.

To use a different SQL Server without editing the file, use user secrets:

```bash
cd CommuniLend.Web
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=CommuniLend;User Id=sa;Password=<your-password>;TrustServerCertificate=True"
```

or the environment variable `ConnectionStrings__DefaultConnection`.

Configuration sources are applied in this order (later ones win): `appsettings.json`, `appsettings.{Environment}.json`, user secrets (Development only), environment variables.

> ⚠️ **Never commit sensitive data** (passwords, API keys) to source control. Use user secrets or environment variables for local secrets.

---

## 🤝 Contributing

Contributions are welcome! To contribute:

1. Fork the repository
2. Create a new branch: `git checkout -b feature/your-feature-name`
3. Commit your changes: `git commit -m "Add some feature"`
4. Push to the branch: `git push origin feature/your-feature-name`
5. Open a Pull Request

---

## 📄 License

This project is licensed under the **MIT License**. See the [LICENSE](LICENSE) file for details.

---

## 📬 Contact

**hubitvp** – [@hubitvp](https://github.com/hubitvp)

Project Link: [https://github.com/hubitvp/CommuniLend](https://github.com/hubitvp/CommuniLend)
