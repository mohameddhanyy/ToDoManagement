# Todo Management API

## Description
A simple Todo Management application with basic CRUD operations and status management.

## Features
- Create, Read, Update, Delete Todos
- Filter todos by status (Pending, InProgress, Completed)
- Mark todo as complete
- Basic validation (e.g., required title, valid dates)
- Bootstrap frontend with status filtering and form handling

## Technologies
- ASP.NET Core 7/8
- Entity Framework Core
- HTML, Bootstrap, JavaScript

## How to Run
1. Clone the repository  
2. Update the `appsettings.json` with your database connection string  
3. Run the project using:
4. Open `https://localhost:<port>/index.html` to access the frontend UI

## Folder Structure
- `Controllers/` - Web API controllers
- `Data/` - EF Core DB context and models
- `wwwroot/` - Frontend files (index.html, app.js)

## Notes
- Uses CORS to allow frontend API calls
- Frontend served from `wwwroot/index.html`
