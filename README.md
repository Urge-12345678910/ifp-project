# ifp-project
# Expense Tracker

Web application for personal expense tracking. Built with Blazor Server (.NET), Entity Framework Core and SQLite. Final project for the IFP course.

## Features
- Add, edit and delete expenses with form validation
- Categories with colors (a category that has expenses cannot be deleted)
- Search by title and filter by category
- Statistics: spending per category
- Monthly budget with warnings at 80% and when the limit is exceeded
- Shared state container (`BudgetState`) keeps the budget up to date on every page
- Error handling with user-friendly messages
- Responsive design for desktop, tablet and phone

## Technologies
C#, .NET 10, Blazor Server, Entity Framework Core, SQLite, Bootstrap 5, xUnit

## Architecture
- `Models`: data classes with validation attributes (`Expense`, `Category`, `Budget`)
- `Data`: `AppDbContext` and EF Core migrations
- `Services`: business logic behind interfaces, injected with dependency injection
- `Components/Pages`: application pages
- `Components/Shared`: reusable components (`BudgetBar`, `DataTable<T>`, `CategoryForm`)
- `BudgetState`: state container shared by pages

## Requirements
- .NET SDK 10 (check with `dotnet --version`)
- Any modern browser

## Installation and run
1. Clone the repository:
   `git clone https://github.com/Urge-12345678910/ifp-project.git`
2. Open the project folder:
   `cd ifp-project/BlazorFinalProject`
3. Run the application:
   `dotnet run`
4. Open the address shown in the terminal (for example `http://localhost:5096`)

The database file `expenses.db` is created automatically on first start.

## Tests
Run all automated tests from the repository root:

`dotnet test BlazorFinalProject.Tests`

The 10 xUnit tests cover `ExpenseService` (add, update, delete, filter, search, month total, totals by category) and `BudgetService` (default limit, save, update). They use an in-memory SQLite database.

## User guide
1. Open **Categories** and add categories with a name and a color.
2. Open **Expenses** and press **Add** to create an expense. Fill in title, amount, date and category.
3. Use the search box and the category filter to find expenses. Use **Edit** or **Delete** in each row.
4. Open **Budget** and set a monthly limit. The bar turns yellow at 80% and red when the limit is exceeded.
5. Open **Statistics** to see spending per category.
6. The **Home** page shows the month total, the budget bar and the latest expenses.

## Developer notes
- Pages use services through interfaces (`IExpenseService`, `ICategoryService`, `IBudgetService`) registered in `Program.cs`.
- `IDbContextFactory` creates a short-lived `DbContext` for each operation, because Blazor Server components live long.
- Add a migration after changing a model: `dotnet ef migrations add <Name>`. Migrations are applied automatically on start.
- To use another database, change the EF Core provider and the connection string in `Program.cs`.


## Authors
Adil Daulet, Khaknazar Mursalim
