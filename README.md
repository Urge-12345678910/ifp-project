# Expense Tracker

Web application for personal expense tracking. Built with Blazor Server (.NET), Entity Framework Core and SQLite.

## Features
- Add, edit and delete expenses
- Categories with colors
- Search and filter by category
- Statistics by category
- Monthly budget with warnings at 80% and 100%
- Shared state container for the budget
- Error handling with user-friendly messages
- Responsive design for desktop, tablet and phone

## Technologies
C#, .NET, Blazor Server, Entity Framework Core, SQLite, Bootstrap, xUnit

## Architecture
- Models: data classes with validation attributes
- Data: AppDbContext and migrations (EF Core)
- Services: business logic behind interfaces, injected with DI
- Components/Pages: application pages
- Components/Shared: reusable components (BudgetBar, DataTable)
- BudgetState: state container shared by pages

## Installation
1. Install the .NET SDK
2. Clone the repository: `git clone <repo-url>`
3. Open the folder `BlazorFinalProject`
4. Run `dotnet run`
5. Open the address shown in the terminal

The database is created automatically on first start.

## Tests
`dotnet test BlazorFinalProject.Tests`

## User guide
1. Open Categories and add categories
2. Open Expenses and press Add to create an expense
3. Set a monthly limit on the Budget page
4. Open Statistics to see spending by category


## Authors
Adil Daulet, Khaknazar Mursalim