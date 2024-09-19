# Technical Exercise
This project is a technical exercise, and you can find the technical details and requirements below.

### Details
Your task is to develop a simple web application with an API and data layer using .NET C#, ASP.NET MVC, Web API, and a database or data store, while adhering to Clean Architecture principles and using Test-Driven Development (TDD) methodologies.

Your development should be driven by an informal user story that you will create, and which should be included in your presentation. The application should allow users to create, read, update, and delete records from the data via the API endpoints. Additionally, you should create a user, login as the user, and ensure that the user information is stored in the data.
 
To showcase your ability to work with modern data storage systems you cannot use Entity Framework, Dapper, or Mediator to complete this assignment.

### Requirements:
The application should include the following components:
 
- Database: Create a database or other data storage solution with at least one table/object/container to store data for the application and an additional one to store users. The table/object/container should have a unique identifier (primary key) and at least two other fields.
 
- API: Develop an ASP.NET Web API with endpoints that allow users to perform CRUD operations on the data. Each endpoint should have appropriate HTTP verbs, parameters, and return values. Additionally, a second API should include endpoints for user creation, user login, and authorized and non-authorized endpoints.
 
- Data layer: Develop a data access layer that interacts with the data and provides the necessary CRUD operations for the API endpoints.
 
- Business logic layer: Develop a business logic layer that includes all of the business rules and validation for the application. This layer should be independent of the data layer and the API.
 
- Unit tests: Write unit tests for all components of the application, including the data access layer, business logic layer, and API endpoints.
 
- Front end (optional): Develop a simple front-end interface to consume the API endpoints. The front-end can be developed using any technology or framework of your choice, as long as it runs on a web browser. We will also accept a Postman workspace, or using Swagger.

- Clean Architecture: Your architecture should adhere to Clean Architecture principles, including separation of concerns and independence of components.
  
- Test-Driven Development: Your project should follow TDD methodologies and include unit tests for all components.
  
- Code quality: Your code should be well-organized, readable, and adhere to best practices.
  
- Functionality: Your application should perform the required CRUD operations and user authentication without errors or bugs.
  
- User Story: Your user story should drive the development of the application and be included in your presentation.

The application should be fully runnable locally, and Docker is preferred. There is no requirement to deploy any code or leverage any cloud services. We do NOT except your sample
to ship with a functional database, we should be able to deploy the database or data storage using your docker or other build resources.

-----

# Pet Adoption System
The Pet Adoption System is designed to streamline the process of pet adoption. It offers a platform for animal shelters to list pets available for adoption and for potential adopters to view and adopt these pets.

## User Story:
Manage Pets and Users in the Adoption System

As an administrator, I want to be able to manage the pet adoption system by adding, viewing, updating, and deleting pet records and user accounts. Additionally, I need to ensure secure login and authorization for different operations so that the data integrity and security of the system are maintained.

## Technologies Used
- **ASP.NET Core**: For building the API and the main backend services.
- **SQL Server**: For the database management.
- **xUnit**: For unit testing.
- **C#**: The primary programming language used.

## Project Structure

- **Database**: Contains SQL scripts and database management files.
- **PetAdoptionSystem.Api**: Implements the API that exposes endpoints for clients to interact with the system.
- **PetAdoptionSystem.AppHost**: Aspire - Hosts the application, configuring necessary services and middleware.
- **PetAdoptionSystem.ServiceDefaults**: Aspire - Contains default service definitions, common configurations, utilities, and service extensions.
- **PetAdoptionSystem.Application**: Contains application logic, use cases, and business rules that orchestrate interactions between domain and infrastructure.
- **PetAdoptionSystem.Domain**: Defines entities, aggregates, repositories, and domain services. It contains core business logic.
- **PetAdoptionSystem.Infra**: Implements infrastructure-related code, such as data access repositories and external service integrations.
- **PetAdoptionSystem.IoC**: Configures dependency injection containers to resolve dependencies throughout the system.
- **PetAdoptionSystem.Tests**: Contains unit tests to ensure code quality and functionality.
- ~~**PetAdoptionSystem.Web**: Contains the web application (front-end) where users interact with the system, including views, controllers, scripts, and styles.~~

## Getting Started

### Prerequisites

- .NET Core SDK
- SQL Server or any other supported database
- Visual Studio or any other compatible IDE
- Install and run Docker Desktop

### Setting Up User-Secrets

To securely store sensitive information such as passwords, you can use .NET Core's user-secrets feature. Follow these steps to set up user-secrets for the project:

1. Use the `cd` command to navigate to the directory where the `.csproj` file of the `PetAdoptionSystem.AppHost` project is located:
   ```sh
   cd FULL_PATH\PetAdoptionSystem\PetAdoptionSystem.AppHost

2. Initialize user-secrets:
   ```sh
   dotnet user-secrets init

3. Set the SQL password secret:
   ```sh
   dotnet user-secrets set Parameters:sql-password Xm4Q9VCUBnaA9CwAdrh5JP
4. Replace Xm4Q9VCUBnaA9CwAdrh5JP with your actual SQL password.


### Run ASPIRE
1. Set up as start up project `PetAdoptionSystem.AppHost`
2. Run it!

![image](https://github.com/Marlohn/PetAdoptionSystem/assets/69219793/eeb625b6-3a89-481f-a0b4-1171f77e96ed)


### Set up the database
1. Navigate to the Database directory and execute the SQL scripts to set up the database schema and initial data.
