# Library Management System Documentation

## 1. Project Overview

### Description

The Library Management System (LMS) is a web-based application designed to manage the catalog of a library. It allows users to borrow and return books, manage member records, and track due dates and fines. The system is intended to streamline library operations and improve user experience.

### Key Features

- User Authentication (Registration, Login, Roles)
- Book Management (Add, Update, Delete Books)
- Member Management
- Book Borrowing & Returning
- Fine Calculation
- Reports & Dashboards

### Technologies Used

- ASP.NET MVC
- Entity Framework Core
- SQL Server / SQLite
- Bootstrap & jQuery (for UI enhancements)
- Identity Framework (for authentication and authorization)
- Logging using Serilog

### Project Structure

```
LibraryManagementSystem/
│── Controllers/
│── Models/
│── Views/
│── Services/
│── Data/
│── Migrations/
│── wwwroot/
│── appsettings.json
│── Program.cs
│── Startup.cs
```

---

## 2. Installation & Setup

### Prerequisites

- .NET SDK (latest version)
- SQL Server or SQLite
- Visual Studio / VS Code
- Entity Framework Core CLI

### Steps

1. Clone the repository:
   ```sh
   git clone https://github.com/example/library-management.git
   ```
2. Navigate to the project directory:
   ```sh
   cd LibraryManagementSystem
   ```
3. Install dependencies:
   ```sh
   dotnet restore
   ```
4. Configure database connection in `appsettings.json`
5. Apply migrations:
   ```sh
   dotnet ef database update
   ```
6. Run the application:
   ```sh
   dotnet run
   ```

---

## 3. System Architecture

### MVC Pattern

- **Model:** Defines the data structure (Book, User, BorrowRecord, etc.)
- **View:** Handles the UI
- **Controller:** Manages requests and responses

### Flow of Data

1. User requests a page (Controller handles request)
2. Controller fetches data from Model
3. Controller passes data to View
4. View renders UI with data

---

## 4. Database Schema

### ER Diagram (TBD)

### Tables & Relationships

- Users (Id, Name, Role, Email, Password)
- Books (Id, Title, Author, ISBN, Status)
- Members (Id, Name, Contact, MembershipDate)
- BorrowRecords (Id, BookId, MemberId, BorrowDate, ReturnDate, Fine)

---

## 5. Features & Modules

### User Authentication

- Registration
- Login & Logout
- Role-based Access Control

### Book Management

- CRUD Operations

### Member Management

- Add, Edit, Delete Members

### Borrow & Return

- Borrowing books
- Calculating due dates
- Managing fines

### Reports

- Daily borrowed books
- Members with outstanding fines

---

## 6. Code Structure & Best Practices

### Naming Conventions

- PascalCase for class names
- camelCase for variables

### Security Considerations

- Password hashing
- SQL Injection prevention

### Error Handling

- Global exception handling middleware

---

## 7. Deployment Guide

### Deploying to IIS

1. Publish the project:
   ```sh
   dotnet publish -c Release -o ./publish
   ```
2. Configure IIS and set the application path

### Deploying to Azure

- Use Azure App Service
- Configure environment variables

---

## 8. Troubleshooting & Common Issues

### Common Errors & Fixes

- **Database Connection Error:** Check `appsettings.json`.
- **Login Issues:** Ensure Identity roles are seeded.

### Debugging Tips

- Use breakpoints in Visual Studio
- Check application logs

---

## 9. Future Enhancements & Scalability

### Possible Improvements

- Implement API endpoints
- Multi-language support
- Mobile app integration

### How to Extend

- Create new controllers & views
- Add more reports

---

## 10. Contributor Guidelines

### Coding Standards

- Follow .NET best practices
- Use meaningful commit messages

### How to Contribute

1. Fork the repository
2. Create a feature branch
3. Submit a pull request
