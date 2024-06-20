# Pet Adoption System

## Description
The Pet Adoption System is designed to streamline the process of pet adoption. It offers a platform for animal shelters to list pets available for adoption and for potential adopters to view and adopt these pets.

### User Story: Manage Pets and Users in the Adoption System
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


### Run project
1. Set up as start up project `PetAdoptionSystem.AppHost`
2. Run it!

![image](https://github.com/Marlohn/PetAdoptionSystem/assets/69219793/eeb625b6-3a89-481f-a0b4-1171f77e96ed)


### Set up the database
1. Navigate to the Database directory and execute the SQL scripts to set up the database schema and initial data.
