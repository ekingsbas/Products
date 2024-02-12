# Products API

This project is a .NET Core application designed to provide CRUD operations for managing products. It follows a layered architecture pattern and employs various design principles such as Repository, CQRS, SOLID, and Clean Code.

## Table of Contents

1. [Technologies Used](#technologies-used)
2. [Project Overview](#project-overview)
3. [Folder Structure](#folder-structure)
4. [Setup](#setup)
5. [Usage](#usage)
6. [Contributing](#contributing)
7. [License](#license)

## Technologies Used

- .NET 8.0 (Core)
- Entity Framework Core
- Swagger (for API documentation)
- NUnit (for unit testing)
- Moq (for mocking dependencies)
- SQL Server (for database)

## Project Overview

The Products API provides RESTful endpoints for performing CRUD operations on products. It employs a layered architecture, separating concerns into different projects:

- **Products.Api**: Web API layer responsible for handling HTTP requests and responses.
- **Products.Business**: Business logic layer implementing the CQRS pattern and containing service classes for handling business operations.
- **Products.Business.Models**: Contains models used by the business layer.
- **Products.Data**: Data access layer containing repository classes for interacting with the database using Entity Framework Core.
- **Products.Entities**: Contains entity classes representing database tables.
- **Products.DataBase**: SQL Server database project defining the database structure and tables.
- **Products.Business.Test**: Unit test project for testing business logic.

The architecture follows the principles of SOLID and Clean Code, ensuring maintainability and extensibility of the codebase.

## Folder Structure

- **Products.Api**
  - Controllers: Contains API controllers for handling HTTP requests.
  - Middleware: Custom middleware for logging requests.
  - Models: DTOs used for API requests and responses.
  - Services: Utility and helper classes.
- **Products.Business**
  - Commands: Command classes implementing business operations.
  - Handlers: Command and query handlers implementing the CQRS pattern.
  - Services: Business service classes.
  - Validators: Validation classes for request models.
- **Products.Business.Models**
  - Contains domain models used by the business layer.
- **Products.Data**
  - DbContext: Entity Framework DbContext class.
  - Repositories: Data access repository classes.
- **Products.Entities**
  - Contains entity classes representing database tables.
- **Products.DataBase**
  - SQL Scripts: SQL scripts defining database structure and tables.

## Setup

To set up the project locally, follow these steps:

1. Clone the repository.
2. Configure the connection string in `appsettings.json` to point to your SQL Server instance.
3. Publish the database project `Products.DataBase` to your local database server to create the database schema.
4. Build and run the application.

## Usage

Once the application is running, you can use tools like Postman to interact with the API endpoints. The Swagger UI is also available at `/swagger` for easy API documentation and testing.

## Contributing

Contributions to the project are welcome. If you find any issues or have suggestions for improvements, please open an issue or submit a pull request.

## License

This project is licensed under the [MIT License](LICENSE).
