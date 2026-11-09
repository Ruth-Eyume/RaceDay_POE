# RaceDay – Part 3: MVC Web Application

## 1. Project Overview

RaceDay is a web-based event management system designed to simplify the management of sporting events such as marathons, cycling events, road races and community walks.

Part 3 focuses on developing the front-end web application using **ASP.NET Core MVC** and integrating it with the RESTful Web API developed in Part 2.

The application allows participants and organisers to access different functionalities based on their assigned roles.

## 2. Project Objectives

The main objectives of Part 3 are to:

- Develop a user-friendly web application using ASP.NET Core MVC.
- Integrate the MVC application with the existing RaceDay RESTful API.
- Implement user registration and login functionality.
- Provide role-based access for Participants and Organisers.
- Develop separate dashboards for different user roles.
- Retrieve and display event information from the database.
- Improve the overall user experience through a consistent interface.

## 3. Technologies Used

The project uses the following technologies:

- **C#** – Main programming language.
- **ASP.NET Core MVC** – Web application framework.
- **ASP.NET Core Web API** – Backend communication.
- **Entity Framework Core** – Database operations.
- **Microsoft SQL Server** – Database management.
- **HTML5** – Web page structure.
- **CSS3** – Styling and layout.
- **Bootstrap** – Responsive user interface.
- **JavaScript** – Client-side functionality.
- **Visual Studio** – Development environment.

## 4. System Architecture

RaceDay follows a layered architecture using MVC and RESTful API principles.

The application consists of two main projects:

**RaceDayMVC**
- Handles the user interface.
- Contains Models, Views and Controllers.
- Manages user interactions.
- Communicates with the API through ApiService.

**RaceDayAPI**
- Handles backend operations.
- Processes HTTP requests.
- Contains controllers, services, DTOs and models.
- Communicates with the SQL Server database.

The MVC application communicates with the API using HTTP requests, allowing information to be transferred between the front end and backend.

## 5. MVC Architecture

The MVC application follows the Model-View-Controller design pattern.

### Models
Represent the application's data and provide the structures used by the application.

Examples include:
- LoginViewModel
- RegisterViewModel
- EventViewModel

### Views
Provide the user interface and display information to users.

Examples include:
- Login
- Register
- Participant Dashboard
- Organiser Dashboard
- Discover Events

### Controllers
Handle incoming requests, process user interactions and determine which views should be returned.

Examples include:
- AccountController
- DashboardController

### Services
The ApiService handles communication between RaceDayMVC and RaceDayAPI by sending HTTP requests and processing API responses.

## 6. Main System Features

### User Authentication
- User registration.
- User login.
- Session management.
- Role-based dashboard redirection.
- User logout.

### Participant Dashboard
- Access the participant dashboard.
- Discover available sporting events.
- View event information.
- Manage event registrations.
- Access personal results.

### Organiser Dashboard
- Access the organiser dashboard.
- Manage sporting events.
- View participant registrations.
- Manage event results.

### Event Management
The system integrates with the existing API to retrieve, create, update and delete event information.

## 7. User Interface Design

RaceDay uses a consistent visual identity with a colour palette consisting of:

- Nude pink
- Navy blue
- White

The interface aims to provide a clean, modern and user-friendly experience.

The design is intended to support easy navigation and accessibility across the different sections of the application.

## 8. API Integration

The MVC application communicates with the RaceDay RESTful API through the ApiService class.

The API uses standard HTTP methods:

| HTTP Method | Purpose |
|---|---|
| GET | Retrieve information |
| POST | Create new records |
| PUT | Update existing records |
| DELETE | Delete records |

This approach separates the presentation layer from backend processing and database operations.

## 9. Database

RaceDay uses Microsoft SQL Server to store and manage application information.

The database includes entities such as:

- UserAccount
- Organiser
- Participant
- Event
- Category
- EventCategory
- Enrolment
- Result
- Route

Entity Framework Core is used by the API to communicate with the database.

## 10. Running the Application

To run the application:

1. Open the RaceDay solution in Visual Studio.
2. Ensure the SQL Server database is configured correctly.
3. Update the API connection string in the API project's configuration.
4. Ensure the MVC application's API base address points to the correct API URL.
5. Build the solution.
6. Start the RaceDayAPI project.
7. Start the RaceDayMVC project.
8. Open the MVC application in your browser.

Both projects must be running for API communication to work.

## 11. Future Improvements

Possible improvements include:

- Completing participant event registration.
- Implementing event details pages.
- Completing participant registration history.
- Implementing result management.
- Improving organiser event management.
- Adding stronger validation and error handling.
- Improving security and authentication.

## 12. Conclusion

Part 3 extends the RaceDay system by introducing an ASP.NET Core MVC web application that communicates with the existing RESTful API.

The implementation demonstrates the use of MVC architecture, API integration, dependency injection, session management and database-driven functionality.

The overall goal is to provide a centralised platform that makes sporting event management easier for both participants and organisers.

---

**Project:** RaceDay  
**Repository:** RaceDay_POE  
**Part:** 3 – MVC Web Application  
**Development Environment:** Visual Studio  
**Database:** Microsoft SQL Server
