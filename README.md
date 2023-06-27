# BlogApi

BlogApi is a .NET 7 web API project that provides CRUD (Create, Read, Update, Delete) operations for the Blog and Post models. It also includes user registration, login, and role management functionalities. The project leverages various popular libraries and tools such as Autofac, Automapper, Serilog, FluentValidation, ElasticSearch, and JWT (JSON Web Tokens).

## Features

- CRUD operations for the Blog model.
- CRUD operations for the Post model.
- User registration with authentication and authorization using JWT.
- Login functionality.
- Role management for users.

## Technologies Used

- .NET 7: The project is built using the latest version of the .NET framework, providing access to the latest features and enhancements.
- MSSQL: Microsoft SQL Server is used as the backend database for storing blog and user information.
- Autofac: Autofac is an IoC (Inversion of Control) container for .NET, used for dependency injection and managing object lifetimes.
- Automapper: Automapper is used for object-to-object mapping, simplifying the process of transferring data between different classes.
- Serilog: Serilog is a logging framework that provides structured logging capabilities, allowing you to easily collect and analyze logs.
- FluentValidation: FluentValidation is a validation library used to define and enforce validation rules for incoming API requests.
- ElasticSearch: ElasticSearch is a distributed, RESTful search and analytics engine that provides powerful full-text search capabilities.
- JWT (JSON Web Tokens): JWT is a compact and self-contained mechanism for securely transmitting information between parties as a JSON object.

## Getting Started

To get started with the BlogApi project, follow these steps:
Clone the repository:

```shell
git clone https://github.com/sinancansevinc/Net7Basic.git

## Configure the MSSQL database:

Create a new MSSQL database.
Update the connection string in the project  configuration files to point to your database.
Install the required dependencies:

-Open the solution in Visual Studio or your preferred development environment.
Build the solution to restore NuGet packages.
Run the project:

Start the BlogApi project.
The API will be hosted at http://localhost:5000 by default.
API Endpoints
The following API endpoints are available:

/api/blogs: CRUD operations for the Blog model.
/api/posts: CRUD operations for the Post model.
/api/users/register: User registration endpoint.
/api/users/login: User login endpoint.

