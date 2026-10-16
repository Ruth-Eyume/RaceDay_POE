# RaceDay – Part 2: API Development

## 1. Project Overview

RaceDay is a web-based event management system designed to support road-running, walking, and cycling events in South Africa.

Part 2 builds on the database design and API planning completed in Part 1. It focuses on developing a RESTful API to manage events, participants, organisers, enrolments, categories, routes, and results.

## 2. Technologies Used

* C#
* ASP.NET Core Web API
* Entity Framework Core
* Microsoft SQL Server
* REST API
* Swagger / OpenAPI
* Git and GitHub
* GitHub Actions
* xUnit for unit testing

## 3. System Architecture

The application follows a separation-of-concerns approach.

* **Controllers:** Handle HTTP requests and return appropriate responses.
* **Services:** Contain business logic and manage application operations.
* **Models:** Represent the application's data entities.
* **DTOs:** Transfer data between API requests and responses.
* **Database Context:** Connects the application to SQL Server through Entity Framework Core.

## 4. Main Features

The API is designed to support the management of:

* User accounts
* Organisers
* Participants
* Events
* Categories
* Event categories
* Enrolments
* Routes
* Results

The API uses HTTP methods such as GET, POST, PUT, and DELETE to retrieve, create, update, and delete records where those operations have been implemented.

## 5. Database

The application uses a Microsoft SQL Server database named `RaceDay`.

The database structure follows the entities and relationships established during Part 1. Entity Framework Core is used to interact with the database.

Database connection details must be configured locally and sensitive credentials must not be committed to GitHub.

## 6. API Documentation

Swagger/OpenAPI provides an interface for viewing and testing the API endpoints when enabled in the application.

## 7. Testing

Unit testing is used to verify the behaviour of service methods and ensure that application logic works as expected.

Automated tests should be executed before changes are submitted.

## 8. Continuous Integration

GitHub Actions is used to automate the build and test process.

The intended CI workflow will:

1. Check out the repository.
2. Set up the required .NET SDK.
3. Restore project dependencies.
4. Build the API.
5. Run the automated tests.

The workflow should pass successfully before the Part 2 implementation is considered ready for submission.

## 9. Version Control

Part 2 is developed on the `part-2-api` branch to keep its changes separate from the original Part 1 work.

GitHub is used to maintain the source code and track development changes.


