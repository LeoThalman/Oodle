-- Athletes table
CREATE TABLE dbo.Athlete
(	
	AthleteID	INT IDENTITY (1,1) NOT NULL,
	FirstName	NVARCHAR(64) NOT NULL,
	Lastname NVARCHAR(128) NOT NULL,
	Email NVARCHAR(128) NOT NULL,
	DoB DATETIME2 NOT NULL,
	Height INT NOT NULL,
	Weight INT NOT NULL,
	CONSTRAINT [PK_dbo.Athlete] PRIMARY KEY CLUSTERED (AthleteID ASC)
);

-- Coaches Table
CREATE TABLE dbo.Coach
(	
	CoachID	INT IDENTITY (1,1) NOT NULL,
	FirstName	NVARCHAR(64) NOT NULL,
	Lastname NVARCHAR(128) NOT NULL,
	Email NVARCHAR(128) NOT NULL,
	DoB DATETIME2 NOT NULL,
	CONSTRAINT [PK_dbo.Coach] PRIMARY KEY CLUSTERED (CoachID ASC)
);

--Planned Run Table
CREATE TABLE dbo.PlannedRun
(	
	PlannedRunID	INT IDENTITY (1,1) NOT NULL,
	StartDate DATETIME2 NOT NULL,
	StartTime TIME NOT NULL,
	SpeedGoal NVARCHAR(128) NOT NULL,
	DistanceGoal NVARCHAR(128) NOT NULL,
	CONSTRAINT [PK_dbo.PlannedRun] PRIMARY KEY CLUSTERED (PlannedRunID ASC)
);

--Run Table
CREATE TABLE dbo.Run
(	
	RunID	INT IDENTITY (1,1) NOT NULL,
	sTime INT NOT NULL,
	eTime INT NOT NULL,
	Distance NVARCHAR(128) NOT NULL,
	Speed NVARCHAR(128) NOT NULL,
	Length NVARCHAR(128) NOT NULL,
	HeartRate INT NOT NULL,
	ElevationGained INT NOT NULL,
	CONSTRAINT [PK_dbo.Run] PRIMARY KEY CLUSTERED (RunID ASC)
);



--Once tables are created insert into them the appropriate data

--Insert into table Athlete
INSERT INTO dbo.Athlete(FirstName,Lastname, Email, DoB, Height, Weight) VALUES 
	('Justin','Timberlake', 'JUSTINTIMBERLAKE@HOTMAIL.COM','03/22/1983', '72', '205'),
		('Donald','Trump', 'THEDONALD@WOU.EDU','02/25/1947', '70', '230'),
			('Hilary','Clinton', 'WHATISAPERSONALEMAIL@AOL.COM','12/14/1945', '65', '160'),
				('Jose','Quervo', 'JOSEQUERVOROCKS@HOTMAIL.COM','09/17/1971', '68', '225'),
					('Mike','Anthony', 'MARKANTHONY4PRESIDENT@YAHOO.COM','01/31/1979', '72', '190')
GO

--Insert into table Coaches
INSERT INTO dbo.Coach(FirstName,Lastname, Email, DoB) VALUES 
	('Barack','Obama', 'BARACKOBAMA@YAHOO.COM','06/29/1965'),
		('Elvis','Presley', 'THEELVIS@WOU.EDU','01/21/1920'),
			('Abraham','Lincoln', 'LINCOLN@AOL.COM','12/14/1809'),
				('Katy','Perry', 'KATYPERRY@HOTMAIL.COM','11/13/1980'),
					('Tom','Hanks', 'SOCCERISLAND@YAHOO.COM','08/06/1969')
GO

--Insert into table Planned Run
INSERT INTO dbo.PlannedRun(StartDate, StartTime, SpeedGoal, DistanceGoal) VALUES 
	('03/03/2018', '6:00', '6', '3'),
		('03/09/2018', '4:00', '6', '4'),
			('03/05/2018', '2:00', '6', '4'),
				('03/07/2018', '10:00', '6', '2'),
					('03/04/2018', '12:00', '6', '1')
GO

--Insert into table Run
INSERT INTO dbo.Run(sTime, eTime, Distance, Speed, Length, HeartRate, ElevationGained) VALUES 
	('0', '360', '2', '6', '2', '133', '300'),
		('0', '260', '1', '5', '1', '135', '400'),
			('0', '480', '3', '7', '3', '155', '200'),
				('0', '560', '4', '6', '4', '151', '100'),
					('0', '320', '3', '4', '2', '165', '700')
GO
