# 24-56321-1_CompanyApp

Merged .NET Windows Forms application combining **Login-and-Register** (SQL Server authentication) 
and **EmployeeDetails** (Access-based Employee CRUD) into a single, unified application using 
**one shared SQL Server database**.

## Author
- Name: Asfi Sabrin Neha
- Student ID: 24-56321-1
- Course: Object Oriented Programming 2

---

## Project Overview

Two separate, previously working solutions were merged into **one** Windows Forms application 
(`CompanyApp`) backed by **one** SQL Server database (`dbCompanyApp`):

1. **Login-and-Register** — handled user Login/Register using `System.Data.SqlClient`.
2. **EmployeeDetails** — handled Employee CRUD using `System.Data.OleDb` against an Access `.mdb` file.

The user must now log in before the Employee CRUD screen becomes accessible, and logging out 
returns to the Login screen — all within a single project and a single database.

---

## The Six Conflicts (and how they were resolved)

| # | Conflict | Resolution |
|---|----------|------------|
| 1 | Different namespaces (`Login_and_Register` vs `EmployeeDetails`) | Unified everything under a single namespace: **`CompanyApp`** |
| 2 | Different data providers — `OleDbConnection` (Access) vs `SqlConnection` (SQL Server) | Rewrote all Employee data access to use `System.Data.SqlClient` |
| 3 | Two separate databases (Access `.mdb` + SQL Server) | Migrated Access data into SQL Server; **one** database (`dbCompanyApp`) now serves both features |
| 4 | Different .NET Framework target versions | Standardized both projects to target the same Framework version before merging |
| 5 | Two `Program.cs` / `Main()` entry points | Kept a single `Program.cs`; the app now starts at the Login form, not the Employee dashboard |
| 6 | Hidden file dependency (`db_users.mdb` referenced only in `App.config`, not caught at compile time) | Removed the Access file dependency entirely by migrating its data into SQL Server |

---

## Database Design

**Database name:** `dbCompanyApp`

```sql
dbo.Users
- UserID        INT IDENTITY(1,1) PRIMARY KEY
- Username      NVARCHAR(50) NOT NULL UNIQUE
- Password      NVARCHAR(50) NOT NULL
- CreatedAt     DATETIME DEFAULT GETDATE()

dbo.Emp_details
- EmpId         NVARCHAR(50) PRIMARY KEY
- EmpName       NVARCHAR(100) NOT NULL
- EmpAge        INT NOT NULL
- EmpContact    NVARCHAR(20) NOT NULL
- EmpGender     NVARCHAR(10) NOT NULL
- CreatedBy     INT NULL CONSTRAINT FK_Emp_CreatedBy REFERENCES dbo.Users(UserID)
```

`CreatedBy` is nullable **on purpose** — it lets migrated legacy employee rows exist with `NULL` 
(no known creator), while every employee added *after* migration has the logged-in user's `UserID` 
stamped automatically.

### Migrating the Access data
The original `EmpId`, `EmpName`, `EmpAge`, `EmpContact`, `EmpGender` rows were exported from the 
Access `.mdb` file and inserted into `dbo.Emp_details` via `INSERT INTO` statements — `CreatedBy` 
was left `NULL` for these historical/migrated rows since no user account created them.

---

## Connection String

A single connection string in `App.config`, shared by every screen in the app:

```xml
<connectionStrings>
  <add name="connString" 
       connectionString="Data Source=.;Initial Catalog=dbCompanyApp;Integrated Security=True" 
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

No hard-coded connection strings exist anywhere else in the solution — every class reads from 
`App.config` via `ConfigurationManager.ConnectionStrings["connString"]`.

---

## Application Flow

1. **Login (`frmLogin`)** — the app's single entry point (set in `Program.cs`). Validates the 
   username/password against `dbo.Users` and stores the logged-in `UserID` in a static `Session` class.
2. **Register (`frmRegister`)** — creates a new row in `dbo.Users` with a hashed/plain password 
   (per implementation) before returning to Login.
3. **Dashboard (`frmDashboard`)** — shown immediately after successful login; has a **"Manage 
   Employees"** button that opens the Employee CRUD screen (`frmEmployee`).
4. **Employee CRUD (`frmEmployee`)** — Add / Update / Delete / View employee records in 
   `dbo.Emp_details`. On **Add**, `CreatedBy` is stamped with `Session.UserId`.
5. **Logout** — closes the current session, clears `Session.UserId`, and returns to `frmLogin` 
   (a genuine logout, not just closing the form).

### Working feature: LEFT JOIN — Created By
The Employee grid uses a `LEFT JOIN` between `dbo.Emp_details` and `dbo.Users` so that:
- Employees added through the app show the **username** of whoever created them.
- Migrated legacy employees (with `CreatedBy = NULL`) still display correctly, shown as 
  **"Unknown"** / blank, instead of breaking the query.

```sql
SELECT e.EmpId, e.EmpName, e.EmpAge, e.EmpContact, e.EmpGender, 
       ISNULL(u.Username, 'Unknown') AS CreatedByUser
FROM dbo.Emp_details e
LEFT JOIN dbo.Users u ON e.CreatedBy = u.UserID
```

---

## Why one database is better than two

Keeping Login/Register and Employee data in **separate** databases (one SQL Server, one Access) 
meant:
- Two connection strings to maintain and keep in sync
- No way to relate a Users record to an Emp_details record (no foreign key possible across engines)
- Two different data-access technologies (`SqlClient` + `OleDb`) duplicated in the codebase
- Access `.mdb` files are fragile, single-user-friendly at best, and not suited for a real 
  multi-form Windows Forms app

Merging into one `dbCompanyApp` database means a single source of truth, one connection string, 
one data-access pattern, and a real relationship (`CreatedBy` FK) between who logs in and what 
they create.

---

## Screenshots

*(Add screenshots here: Object Explorer showing both tables, View Data on Users, Solution 
Explorer with the merged form files, and the working app showing the LEFT JOIN result.)*

---

## Submission Notes

- Old Access-based project and its `.mdb` file were deleted after migration.
- The repository now contains **one** app and **one** database — no OleDb references remain.
- Login must succeed before the Employee CRUD screen is reachable; Logout returns cleanly to Login.
