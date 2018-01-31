-- Users table
CREATE TABLE dbo.Students
(	
	StudentID	INT IDENTITY (1,1) NOT NULL,
	FirstName	NVARCHAR(64) NOT NULL,
	Lastname NVARCHAR(128) NOT NULL,
	CONSTRAINT [PK_dbo.Students] PRIMARY KEY CLUSTERED (StudentID ASC)
);





--Once tables are created insert into them the appropriate data

--Insert into table
INSERT INTO dbo.Students(FirstName,Lastname) VALUES 
	('Justin','Timberlake'),
		('Donald','Trump'),
			('Hilary','Clinton'),
				('Jose','Quervo'),
					('Mike','Anthony')
GO
