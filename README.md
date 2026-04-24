## A web application that mimics a small forum, focused on backend logic, unit testing, and API documentation.

## Table of contents
* [General info](#general-info)
* [Technologies](#technologies)
* [Features](#features)
* [Setup](#setup)

## General info
This project is simplified forum application that, at this stage of development, focuses on interactive API testing using Swagger UI, backend robustness and controller logic testing.

## Technologies
* **Architecture:** WebAPI (REST)
* **Backend:** .NET 8, C#, Entity Framework Core
* **Frontend:** None (WebAPI only)
* **Database:** SQL Server
* **Documentation:** Swagger

## Features
* **Tests:** Model and controller logic tests.
* **Error Handling:** Clear, custom HTTP error messages and status codes (e.g., 403 Forbidden for ownership violations).
* **Ownership Control:** Secure logic ensuring only authors can modify their content.

## Setup
To run this project locally, follow these steps:

1. **Clone the repository:**
   ```bash
   git clone https://github.com/Navytree/WebAPIForumApp.git

2. **Update Database Connection:**
Open appsettings.json and update the DefaultConnection string to point to your local SQL Server instance.

3. **Apply Migrations:**
Run the following command in the Package Manager Console (Visual Studio) or Terminal to create the database:
   ```bash
    dotnet ef database update
   
4. **Run the application:**
  Press F5 in Visual Studio 2022 or use the command:
     ```bash
     dotnet run
