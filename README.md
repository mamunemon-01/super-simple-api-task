# Super Simple API Task

This project is about **Super Simple API**s built using .NET for managing employees and departments in a company. The project implements RESTful APIs with full CRUD functionality for both `Employee` and `Department` models. The project uses **SQL Server** as the database and follows a simple structure, including an MVC project that consumes the APIs using **JavaScript** and **AJAX**.

## Table of Contents
- [Features](#features)
- [Technologies](#technologies)
- [Setup](#setup)
- [API Endpoints](#api-endpoints)
- [MVC Consumption](#mvc-consumption)
- [Contributing](#contributing)
- [License](#license)

## Features

- CRUD (Create, Retrieve, Update, Delete) operations for **Employee** and **Department** models.
- Search functionality for Employees by:
  - Name
  - Phone number
  - Department
- Search functionality for Departments by name.
- Integration with **Postman** or **Swagger** for testing.
- MVC frontend consumes the API using **AJAX** for interactive functionality.
- Uses **Dapper** for simple and efficient database interactions.

## Technologies

- **.NET 8.0**
- **ASP.NET Core Web API**
- **Entity Framework Core** (for database interactions)
- **Dapper** (as an alternative data access library)
- **SQL Server** (as the database)
- **JavaScript** (AJAX calls in the MVC project)
- **Bootstrap** (for simple UI design)

## Setup

Follow these instructions to get the project up and running locally.

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
- [Visual Studio Code](https://code.visualstudio.com/) or any IDE of your choice

### Database Setup

1. Set up a **SQL Server** instance.
2. Create a database called `SuperSimpleDB`.
3. Run the SQL scripts in the `sql/` folder to create the necessary tables.

### Run the API

1. Clone the repository:
   ```bash
   git clone https://github.com/yourusername/supersimple-api-task.git
   cd supersimple-api-task
   ```

2. Update the connection string in `appsettings.json`:
   ```json
   "DBConnString": "Server=YOUR_SERVER_NAME;Database=SuperSimpleDB;Trusted_Connection=True;"
   ```

3. Build and run the API project:
   ```bash
   dotnet build
   dotnet run --project SuperSimpleAPITask
   ```

   The API will be available at `http://localhost:5299`.<!-- and `https://localhost:7250`.-->

### Run the MVC Project

1. Navigate to the MVC project directory:
   ```bash
   cd SuperSimpleMVC
   ```

2. Run the project:
   ```bash
   dotnet run
   ```

   The MVC frontend will be available at `http://localhost:5279`.

## API Endpoints

### Employee Endpoints

- `POST /api/employee/create`: Create a new employee.
- `GET /api/employee/retrieve/{id}`: Retrieve an employee by ID.
- `PUT /api/employee/update/{id}`: Update an employee.
- `DELETE /api/employee/delete/{id}`: Delete an employee.
- `GET /api/employee/search/name/{name}`: Search employee by name.
- `GET /api/employee/search/phone/{phone}`: Search employee by phone number.
- `GET /api/employee/search/department/{deptName}`: Search employees by department name.

### Department Endpoints

- `POST /api/department/create`: Create a new department.
- `GET /api/department/retrieve/{id}`: Retrieve a department by ID.
- `PUT /api/department/update/{id}`: Update a department.
- `DELETE /api/department/delete/{id}`: Delete a department.
- `GET /api/department/search/name/{name}`: Search department by name.

## MVC Consumption

The MVC project consumes the APIs for Employee and Department. This is done using AJAX to send requests to the API endpoints. The data is dynamically updated in the front end without page reloads.

### Example Usage in the MVC Project

- **Add Employee**: Enter employee details, including department name, and click the "Add" button to make an AJAX call to the API.
- **Search Employee**: Use the search box to search by name, phone, or department, and the results will be displayed dynamically.

## Contributing

Contributions are welcome! If you'd like to contribute:

1. Fork the repository.
2. Create a feature branch (`git checkout -b feature-branch`).
3. Commit your changes (`git commit -m 'Add some feature'`).
4. Push to the branch (`git push origin feature-branch`).
5. Open a pull request.
