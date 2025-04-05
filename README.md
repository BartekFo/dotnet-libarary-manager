# Library Manager

A web application for managing a library's collection of books, built with ASP.NET Core and Blazor Server.

## Features

- User authentication and authorization
- Book management (add, edit, delete)
- Book search and filtering
- User roles (Admin, User)
- Responsive design

## Prerequisites

- .NET 8.0 SDK
- SQL Server (local or Azure)
- Visual Studio 2022 or Visual Studio Code

## Setup

1. Clone the repository:
```bash
git clone [repository-url]
cd LibraryManager
```

2. Create a `.env` file in the root directory with the following content:
```
ConnectionStrings__DefaultConnection=Server=localhost;Database=LibraryManager;Trusted_Connection=True;TrustServerCertificate=True;
```

3. Install required packages:
```bash
dotnet restore
```

4. Create and apply database migrations:
```bash
# Create a new migration
dotnet ef migrations add InitialCreate

# Apply migrations to the database
dotnet ef database update
```

5. Seed the database with initial data:
```bash
# Run the application with the --seed flag
dotnet run -- --seed
```

This will create:
- An admin account (email: admin@example.com, password: Admin123!)
- A regular user account (email: user@example.com, password: User123!)
- Sample books in the database

6. Run the application:
```bash
dotnet run
```

The application will be available at `https://localhost:5001` or `http://localhost:5000`.

## Database Management

### Creating Migrations
To create a new migration after making changes to your models:
```bash
dotnet ef migrations add [MigrationName]
```

### Updating the Database
To apply pending migrations:
```bash
dotnet ef database update
```

### Seeding the Database
To seed the database with initial data:
```bash
dotnet run -- --seed
```

### Resetting the Database
To reset the database and reapply all migrations:
```bash
dotnet ef database drop --force
dotnet ef database update
dotnet run -- --seed
```

## Project Structure

- `Components/` - Blazor components
  - `Pages/` - Page components
  - `Shared/` - Shared components
- `Data/` - Data access layer
  - `Migrations/` - Entity Framework migrations
- `Models/` - Data models
- `Services/` - Business logic services
- `wwwroot/` - Static files

## Security

- User authentication using ASP.NET Core Identity
- Role-based authorization
- Secure password hashing
- Environment variables for sensitive data

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## License

This project is licensed under the MIT License.

## Environment Setup

1. Copy `.env.example` to `.env`:
```bash
cp .env.example .env
```

2. Update the `.env` file with your secure database credentials:
```env
DB_PASSWORD=your_secure_password_here
DB_USER=sa
DB_NAME=BlazorAppDb
DB_SERVER=localhost
```

3. Start the application:
```bash
docker-compose up -d
```

## Security Notes

- Never commit the `.env` file to version control
- Always use strong passwords in production
- Keep your environment variables secure and don't share them
- Use different passwords for development and production environments 