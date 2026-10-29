# CampusCare — College Complaint Management System

A simple ASP.NET Core MVC mini-project for reporting and tracking campus complaints.

## Technology stack
- ASP.NET Core MVC / C#
- Entity Framework Core
- MySQL or MariaDB
- Pomelo EF Core MySQL provider
- Bootstrap 5 responsive UI

## Features
- Create, view, edit, and delete complaints (CRUD)
- Search by student, title, category, or email
- Filter by complaint status
- Status tracking: Submitted, In Progress, Resolved, Rejected
- Model validation for required fields, email, and text lengths
- TempData success messages (state management)
- Responsive layout for desktop and mobile
- EF Core database access

## Requirements
- .NET 8 SDK
- MySQL or MariaDB server (XAMPP can be used if MySQL is running)
- Internet connection on first run to restore NuGet packages and load Bootstrap/validation CDN files

## Run in Visual Studio / VS Code
1. Start MySQL in XAMPP or start your MySQL/MariaDB service.
2. Open `appsettings.json`.
3. Adjust the connection string if your MySQL root account has a password:
   `Server=localhost;Port=3306;Database=CampusCareDb;User=root;Password=YOUR_PASSWORD;`
4. Open a terminal in the folder containing `CampusCare.csproj`.
5. Run:
   ```bash
   dotnet restore
   dotnet run
   ```
6. Open the local URL printed in the terminal.

The application uses `Database.EnsureCreated()` at startup to create the database and table automatically. The MySQL user must have permission to create databases/tables. If you later change the model, use EF Core migrations for schema updates or recreate the demo database.

## Project structure
- `Controllers/` — MVC request handling and CRUD actions
- `Models/Complaint.cs` — complaint entity and validation rules
- `Data/ApplicationDbContext.cs` — EF Core database context
- `Views/Complaints/` — Razor pages for list, create, details, edit, delete
- `wwwroot/css/site.css` — responsive styling

## Suggested classroom demonstration
1. Open the complaint list.
2. Create a complaint with a valid email and description.
3. Show the new record in the table.
4. Search for the complaint and filter by status.
5. Edit it and change status to `In Progress`, then `Resolved`.
6. Open details and delete a test complaint.

## Important notes
- This is a classroom mini-project starter, not a production-ready ticketing platform.
- Authentication and role-based access are not included.
- The default connection string assumes local MySQL with username `root` and no password.
