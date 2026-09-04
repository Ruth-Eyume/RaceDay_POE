--Creating the database 
Create database RaceDay;
Use RaceDay;

--Creating the UserAccount table 
Create table UserAccount (
    UserAccountID INT IDENTITY(1,1),
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    Role VARCHAR(50) NOT NULL,

    CONSTRAINT PK_UserAccount
        PRIMARY KEY (UserAccountID),

    CONSTRAINT UQ_UserAccount_Email
        UNIQUE (Email),

    CONSTRAINT CK_UserAccount_Role
        CHECK (Role IN ('Organiser', 'Participant'))
);

-- ORGANISER TABLE
Create table Organiser (
    OrganiserID INT IDENTITY(1,1),
    UserAccountID INT NOT NULL,
    OrganisationName VARCHAR(100) NOT NULL,
    ContactNumber VARCHAR(20) NULL,
    CreatedAt DATETIME NOT NULL
        CONSTRAINT DF_Organiser_CreatedAt DEFAULT GETDATE(),

    CONSTRAINT PK_Organiser
        PRIMARY KEY (OrganiserID),

    CONSTRAINT UQ_Organiser_UserAccount
        UNIQUE (UserAccountID),

    CONSTRAINT FK_Organiser_UserAccount
        FOREIGN KEY (UserAccountID)
        REFERENCES UserAccount(UserAccountID)
);

-- Creating Participant table

Create table Participant (
    ParticipantID INT IDENTITY(1,1),
    UserAccountID INT NOT NULL,
    DateOfBirth DATE NOT NULL,
    Gender VARCHAR(20) NULL,
    EmergencyContactName VARCHAR(100) NULL,
    EmergencyContactNumber VARCHAR(20) NULL,
    CreatedAt DATETIME NOT NULL
        CONSTRAINT DF_Participant_CreatedAt DEFAULT GETDATE(),

    CONSTRAINT PK_Participant
        PRIMARY KEY (ParticipantID),

    CONSTRAINT UQ_Participant_UserAccount
        UNIQUE (UserAccountID),

    CONSTRAINT FK_Participant_UserAccount
        FOREIGN KEY (UserAccountID)
        REFERENCES UserAccount(UserAccountID)
);

--Creating Route table 

CREATE TABLE Route (
    RouteID INT IDENTITY(1,1) NOT NULL,
    RouteName VARCHAR(150) NOT NULL,
    DistanceKm DECIMAL(6,2) NOT NULL,
    StartPoint VARCHAR(150) NULL,
    EndPoint VARCHAR(150) NULL,
    RouteDescription VARCHAR(500) NULL,
    MapUrl VARCHAR(500) NULL,
    Latitude DECIMAL(9,6) NULL,
    Longitude DECIMAL(9,6) NULL,

    CONSTRAINT PK_Route
        PRIMARY KEY (RouteID),

    CONSTRAINT CK_Route_Distance
        CHECK (DistanceKm > 0),

    CONSTRAINT CK_Route_Latitude
        CHECK (Latitude IS NULL OR Latitude BETWEEN -90 AND 90),

    CONSTRAINT CK_Route_Longitude
        CHECK (Longitude IS NULL OR Longitude BETWEEN -180 AND 180)
);

--Creating Event table
CREATE TABLE Event (
    EventID INT IDENTITY(1,1),
    OrganiserID INT NOT NULL,
    RouteID INT NOT NULL,
    EventName VARCHAR(100) NOT NULL,
    Description VARCHAR(500) NULL,
    EventDate DATE NOT NULL,
    Location VARCHAR(150) NOT NULL,
    RegistrationDeadline DATE NOT NULL,
    Status VARCHAR(20) NOT NULL,
    CreatedAt DATETIME NOT NULL
        CONSTRAINT DF_Event_CreatedAt DEFAULT GETDATE(),

    CONSTRAINT PK_Event
        PRIMARY KEY (EventID),

    CONSTRAINT FK_Event_Organiser
        FOREIGN KEY (OrganiserID)
        REFERENCES Organiser(OrganiserID),

    CONSTRAINT FK_Event_Route
        FOREIGN KEY (RouteID)
        REFERENCES Route(RouteID),

    CONSTRAINT CK_Event_Status
        CHECK (Status IN ('Draft', 'Published', 'Cancelled')),

    CONSTRAINT CK_Event_RegistrationDeadline
        CHECK (RegistrationDeadline <= EventDate)
);



--Creating Category table
CREATE TABLE Category (
    CategoryID INT IDENTITY(1,1),
    CategoryName VARCHAR(100) NOT NULL,
    Description VARCHAR(255) NULL,

    CONSTRAINT PK_Category
        PRIMARY KEY (CategoryID),

    CONSTRAINT UQ_Category_CategoryName
        UNIQUE (CategoryName)
);


--Creating Event table 
Create table EventCategory (
    EventCategoryID INT IDENTITY(1,1),
    EventID INT NOT NULL,
    CategoryID INT NOT NULL,
    EntryFee DECIMAL(8,2) NOT NULL
        CONSTRAINT DF_EventCategory_EntryFee DEFAULT 0.00,
    MaxParticipants INT NULL,
    CreatedAt DATETIME NOT NULL
        CONSTRAINT DF_EventCategory_CreatedAt DEFAULT GETDATE(),

    CONSTRAINT PK_EventCategory
        PRIMARY KEY (EventCategoryID),

    CONSTRAINT FK_EventCategory_Event
        FOREIGN KEY (EventID)
        REFERENCES Event(EventID),

    CONSTRAINT FK_EventCategory_Category
        FOREIGN KEY (CategoryID)
        REFERENCES Category(CategoryID),

    CONSTRAINT UQ_EventCategory_Event_Category
        UNIQUE (EventID, CategoryID),

    CONSTRAINT CK_EventCategory_EntryFee
        CHECK (EntryFee >= 0),

    CONSTRAINT CK_EventCategory_MaxParticipants
        CHECK (MaxParticipants IS NULL OR MaxParticipants > 0)
);

--Creating Enrollment table 
Create table Enrolment (
    EnrolmentID INT IDENTITY(1,1),
    ParticipantID INT NOT NULL,
    EventCategoryID INT NOT NULL,
    EnrolmentDate DATETIME NOT NULL
        CONSTRAINT DF_Enrolment_EnrolmentDate DEFAULT GETDATE(),
    RaceNumber VARCHAR(20) NOT NULL,
    Status VARCHAR(20) NOT NULL,

    CONSTRAINT PK_Enrolment
        PRIMARY KEY (EnrolmentID),

    CONSTRAINT FK_Enrolment_Participant
        FOREIGN KEY (ParticipantID)
        REFERENCES Participant(ParticipantID),

    CONSTRAINT FK_Enrolment_EventCategory
        FOREIGN KEY (EventCategoryID)
        REFERENCES EventCategory(EventCategoryID),

    CONSTRAINT UQ_Enrolment_RaceNumber
        UNIQUE (RaceNumber),

    CONSTRAINT UQ_Enrolment_Participant_EventCategory
        UNIQUE (ParticipantID, EventCategoryID),

    CONSTRAINT CK_Enrolment_Status
        CHECK (Status IN ('Pending', 'Confirmed', 'Cancelled'))
);

-- Creating Result table
Create table Result (
    ResultID INT IDENTITY(1,1),
    EnrolmentID INT NOT NULL,
    FinishTime TIME NULL,
    Position INT NULL,
    ResultStatus VARCHAR(20) NOT NULL
        CONSTRAINT DF_Result_Status DEFAULT 'Finished',
    RecordedAt DATETIME NOT NULL
        CONSTRAINT DF_Result_RecordedAt DEFAULT GETDATE(),

    CONSTRAINT PK_Result
        PRIMARY KEY (ResultID),

    CONSTRAINT FK_Result_Enrolment
        FOREIGN KEY (EnrolmentID)
        REFERENCES Enrolment(EnrolmentID),

    CONSTRAINT UQ_Result_Enrolment
        UNIQUE (EnrolmentID),

    CONSTRAINT CK_Result_Position
        CHECK (Position IS NULL OR Position > 0),

    CONSTRAINT CK_Result_Status
        CHECK (ResultStatus IN ('Finished', 'DNF', 'DNS'))
);

-- INSERTING DATA INTO USERACCOUNT

INSERT INTO UserAccount
(FirstName, LastName, Email, PasswordHash, Role)
VALUES
('Jordan', 'Shall', 'jordan.shall@email.com', 'TestHash123', 'Organiser'),
('David', 'Jones', 'david.jones@email.com', 'TestHash456', 'Organiser'),
('Ruth', 'Eyume', 'ruth.eyume@email.com', 'TestHash789', 'Participant'),
('Divine', 'Brown', 'divine.brown@email.com', 'TestHash012', 'Participant');

-- INSERTING DATA INTO ORGANISER
INSERT INTO Organiser
(UserAccountID, OrganisationName, ContactNumber)
VALUES
(1, 'Cape Town Race Events', '0215551234'),
(2, 'Western Cape Sports', '0215555678');


-- INSERTING DATA INTO PARTICIPANT

INSERT INTO Participant
(UserAccountID, DateOfBirth, Gender,
 EmergencyContactName, EmergencyContactNumber)
VALUES
(3, '2002-05-14', 'Female', 'Sarah Eyume', '0821234567'),
(4, '2001-11-20', 'Male', 'John Brown', '0839876543');

-- INSERTING DATA INTO ROUTE
INSERT INTO Route
(RouteName, DistanceKm, StartPoint, EndPoint,
 RouteDescription, MapUrl, Latitude, Longitude)
VALUES
('Cape Town 5km Route',
 5.00,
 'Company Gardens',
 'Green Point',
 'Scenic 5km route through central Cape Town',
 'https://maps.example.com/capetown5km',
 -33.9258,
 18.4232),

('Table Mountain 10km Route',
 10.00,
 'Kloof Street',
 'Signal Hill',
 'Challenging 10km route around Table Mountain',
 'https://maps.example.com/tablemountain10km',
 -33.9249,
 18.4241),

('Stellenbosch Cycle Route',
 50.00,
 'Stellenbosch Town',
 'Paarl Road',
 '50km cycling route through the Cape Winelands',
 'https://maps.example.com/stellenbosch50km',
 -33.9321,
 18.8602);

-- INSERTING DATA INTO CATEGORY
INSERT INTO Category
(CategoryName, Description)
VALUES
('5km Run', '5 kilometre road running event'),
('10km Run', '10 kilometre road running event'),
('21km Run', '21 kilometre half marathon'),
('10km Walk', '10 kilometre walking event'),
('50km Cycle', '50 kilometre cycling event');

-- INSERTING DATA INTO EVENT
INSERT INTO Event
(OrganiserID, RouteID, EventName, Description,
 EventDate, Location, RegistrationDeadline, Status)
VALUES
(1, 1,
 'Cape Town Spring Run',
 'Annual road running event',
 '2026-10-10',
 'Cape Town',
 '2026-10-01',
 'Published'),

(1, 2,
 'Table Mountain Challenge',
 'Challenging mountain running event',
 '2026-11-15',
 'Cape Town',
 '2026-11-05',
 'Published'),

(2, 3,
 'Western Cape Cycle Tour',
 'Road cycling event through the Western Cape',
 '2026-12-05',
 'Stellenbosch',
 '2026-11-25',
 'Published');

-- INSERTING DATA INTO EVENTCATEGORY
INSERT INTO EventCategory
(EventID, CategoryID, EntryFee, MaxParticipants)
VALUES
(1, 1, 100.00, 500),
(1, 2, 150.00, 500),
(2, 3, 250.00, 300),
(2, 4, 150.00, 200),
(3, 5, 300.00, 400);

-- INSERTING DATA INTO ENROLMENT

INSERT INTO Enrolment
(ParticipantID, EventCategoryID, EnrolmentDate,
 RaceNumber, Status)
VALUES
(1, 1, GETDATE(), 'R001', 'Confirmed'),
(2, 1, GETDATE(), 'R002', 'Confirmed'),
(1, 3, GETDATE(), 'R003', 'Confirmed'),
(2, 5, GETDATE(), 'R004', 'Confirmed');

-- INSERTING DATA INTO RESULT   
INSERT INTO Result
(EnrolmentID, FinishTime, Position, ResultStatus)
VALUES
(1, '00:28:45', 1, 'Finished');

-- DISPLAYING ALL INSERTED DATA
SELECT * FROM UserAccount;
SELECT * FROM Organiser;
SELECT * FROM Participant;
SELECT * FROM Route;
SELECT * FROM Event;
SELECT * FROM Category;
SELECT * FROM EventCategory;
SELECT * FROM Enrolment;
SELECT * FROM Result;







