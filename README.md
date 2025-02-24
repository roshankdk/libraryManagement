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
   git clone https://github.com/roshankdk/library-management.git
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

---

## Admin Features

Admins have elevated privileges that allow them to manage the system and oversee the library’s operations. Key admin functionalities include:

### Admin Dashboard

**Overview:** Provides a quick snapshot of system statistics such as total books, total members, and the number of overdue books.

**How to Use:**
- Log in using an account with the Admin role (e.g., the seeded admin account: admin@library.com).
- Navigate to the “Admin Dashboard” link (usually available in the navigation bar) to view system summaries and reports.

### Member Management

**Overview:** Admins can view all members registered in the system, and assign or remove roles (e.g., Member, Librarian, Admin) as needed.

**How to Use:**
- From the Admin Dashboard or the dedicated “Members” page, view the list of members.
- Use the dropdowns and action buttons provided to assign a new role or remove an existing role from a member.

### Book Management (for Admin and Librarian)

**Overview:** While both Admins and Librarians can add, edit, and delete books, Admins have overall control and can also view reports related to book status.

**How to Use:**
- Navigate to the “Books” section.
- Use the “Add New Book” button to add a book, or select a book to edit or delete.

### Reports & Overdue Tracking

**Overview:** Admins can generate reports on overdue books and other metrics to monitor library usage and ensure timely returns.

**How to Use:**
- Access the report section from the Admin Dashboard to see a list of overdue books, and review member activity for follow-ups.

---

## Normal User (Member) Features

Normal users, or Members, interact with the system primarily for browsing, borrowing, and returning books. Their features include:

### User Authentication

**Registration & Login:**

**How to Use:**
- New users can register through the registration page.
- After registration, users log in using their credentials.
- Once logged in, the system assigns the default role “Member.”

### Book Browsing & Search

**Overview:** Members can view the list of available books and use a search function to filter by title, author, or genre.

**How to Use:**
- Navigate to the “Books” page to see all books.
- Use the search box to filter the list based on keywords.

### Borrowing & Returning Books

**Overview:** Members can request to borrow books if they are available, and later return them.

**How to Use:**
- From the “Books” page or a book’s details page, use the “Borrow” button (this action might prompt you to enter a due date).
- To return a book, go to “My Borrows” where you see a list of books you’ve borrowed. Click the “Return” button next to the book you want to return.

### Viewing Borrow History

**Overview:** Members can review their borrowing history, including current and past borrow records.

**How to Use:**
- Navigate to the “My Borrows” section to see all records of borrowed books, including due dates and return status.