/****** Object:  Table [dbo].[AspNetRoles] ******/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AspNetRoles](
    [Id] [nvarchar](128) NOT NULL,
    [Name] [nvarchar](256) NOT NULL,
	CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[AspNetUserClaims] ******/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AspNetUserClaims](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [UserId] [nvarchar](128) NOT NULL,
    [ClaimType] [nvarchar](max) NULL,
    [ClaimValue] [nvarchar](max) NULL,
	CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

/****** Object:  Table [dbo].[AspNetUserLogins] ******/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AspNetUserLogins](
    [LoginProvider] [nvarchar](128) NOT NULL,
    [ProviderKey] [nvarchar](128) NOT NULL,
    [UserId] [nvarchar](128) NOT NULL,
	CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED
(
    [LoginProvider] ASC,
    [ProviderKey] ASC,
    [UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]



GO

/****** Object:  Table [dbo].[AspNetUserRoles]     ******/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AspNetUserRoles](
    [UserId] [nvarchar](128) NOT NULL,
    [RoleId] [nvarchar](128) NOT NULL,
	CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED
(
    [UserId] ASC,
    [RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY]



GO

/****** Object:  Table [dbo].[AspNetUsers]     ******/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AspNetUsers](
    [Id] [nvarchar](128) NOT NULL,
    [Email] [nvarchar](256) NULL,
    [EmailConfirmed] [bit] NOT NULL,
    [PasswordHash] [nvarchar](max) NULL,
    [SecurityStamp] [nvarchar](max) NULL,
    [PhoneNumber] [nvarchar](max) NULL,
    [PhoneNumberConfirmed] [bit] NOT NULL,
    [TwoFactorEnabled] [bit] NOT NULL,
    [LockoutEndDateUtc] [datetime] NULL,
    [LockoutEnabled] [bit] NOT NULL,
    [AccessFailedCount] [int] NOT NULL,
    [UserName] [nvarchar](256) NOT NULL,
	CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]



GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])

REFERENCES [dbo].[AspNetUsers] ([Id])

ON DELETE CASCADE

GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]

GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])

REFERENCES [dbo].[AspNetUsers] ([Id])

ON DELETE CASCADE

GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]

GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])

REFERENCES [dbo].[AspNetRoles] ([Id])

ON DELETE CASCADE

GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]

GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])

REFERENCES [dbo].[AspNetUsers] ([Id])

ON DELETE CASCADE

GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]

GO





-- Users table
CREATE TABLE dbo.Users
(	
	UsersID	INT IDENTITY (1,1) NOT NULL,
	IdentityID NVARCHAR(128) NOT NULL,
	FirstName	NVARCHAR(64)  NULL,
	Lastname NVARCHAR(128)  NULL,
	Email NVARCHAR(128) NOT NULL,
	Icon VARBINARY(MAX)  NULL,
	Bio NVARCHAR(512) NULL,
	UserName NVARCHAR(128) NOT NULL,
	CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED (UsersID ASC),
	CONSTRAINT [FK_dbo.Users_dbo.IdentityID] FOREIGN KEY ([IdentityID]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

-- Class Table
CREATE TABLE dbo.Class
(	
	ClassID	INT IDENTITY (1,1) NOT NULL,
	UsersID INT NOT NULL,
	Name	NVARCHAR(128) NOT NULL,
	Description NVARCHAR(256) NOT NULL,
	SlackName NVARCHAR(20) NULL,
	Subject NVARCHAR(128) NOT NULL,
	CONSTRAINT [PK_dbo.Class] PRIMARY KEY CLUSTERED (ClassID ASC),
	CONSTRAINT [FK_dbo.Class_dbo.UsersID] FOREIGN KEY ([UsersID]) REFERENCES [dbo].[Users] ([UsersID])--
);


--Role Table
CREATE TABLE dbo.Role
(	
	RoleID	INT IDENTITY (1,1) NOT NULL, -- Is this key needed?
	Role INT NOT NULL, -- do we want to represent Roles with INT?
	CONSTRAINT [PK_dbo.Role] PRIMARY KEY CLUSTERED (RoleID ASC)
);

--User Role Class Table
CREATE TABLE dbo.UserRoleClass
(	
	UserRoleClassID	INT IDENTITY (1,1) NOT NULL,
	UsersID INT NOT NULL,
	RoleID INT NOT NULL,
	ClassID INT NOT NULL,
	CONSTRAINT [PK_dbo.UserRoleClass] PRIMARY KEY CLUSTERED (UserRoleClassID ASC),
	CONSTRAINT [FK_dbo.UserRoleClass_dbo.UserID] FOREIGN KEY ([UsersID]) REFERENCES [dbo].[Users] ([UsersID]),
	-- CONSTRAINT [FK_dbo.UserRoleClass_dbo.RoleID] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[Role] ([RoleID]),
	CONSTRAINT [FK_dbo.UserRoleClass_dbo.ClassID] FOREIGN KEY ([ClassID]) REFERENCES [dbo].[Class] ([ClassID])
	ON DELETE CASCADE
	ON UPDATE CASCADE
);

--Class Notifications table
CREATE TABLE dbo.ClassNotification
(
	ClassNotificationID INT IDENTITY (1,1) NOT NULL,
	Notification NVARCHAR(256) NOT NULL,
	TimePosted DATETIME NOT NULL,
	ClassID INT NOT NULL,
	CONSTRAINT [PK_dbo.ClassNotification] PRIMARY KEY CLUSTERED (ClassNotificationID ASC),
	CONSTRAINT [FK_dbo.ClassNotification_dbo.ClassID] FOREIGN KEY ([ClassID]) REFERENCES [dbo].[Class] ([ClassID])
	ON DELETE CASCADE
	ON UPDATE CASCADE,

);


-- Assignment Table
CREATE TABLE dbo.Assignment
(	
	AssignmentID	INT IDENTITY (1,1) NOT NULL,
	ClassID	INT NOT NULL,
	Name NVARCHAR(128) NOT NULL,
	Description NVARCHAR(512),
	StartDate DATETIME,
	DueDate DATETIME,
	Weight INT NOT NULL,
	CONSTRAINT [PK_dbo.Assignment] PRIMARY KEY CLUSTERED (AssignmentID ASC),
	CONSTRAINT [FK_dbo.Assignment_dbo.ClassID] FOREIGN KEY ([ClassID]) REFERENCES [dbo].[Class] ([ClassID])
);

-- Task Table
CREATE TABLE dbo.Tasks
(	
	TasksID	INT IDENTITY (1,1) NOT NULL,
	ClassID	INT NOT NULL,
	Description NVARCHAR(512),
	StartDate DATETIME,
	DueDate DATETIME,
	CONSTRAINT [PK_dbo.Tasks] PRIMARY KEY CLUSTERED (TasksID ASC),
	CONSTRAINT [FK_dbo.Tasks_dbo.ClassID] FOREIGN KEY ([ClassID]) REFERENCES [dbo].[Class] ([ClassID])
);

-- Notes Table
CREATE TABLE dbo.Notes
(	
	NotesID	INT IDENTITY (1,1) NOT NULL,
	ClassID	INT NOT NULL,
	Description NVARCHAR(512),
	CONSTRAINT [PK_dbo.Notes] PRIMARY KEY CLUSTERED (NotesID ASC),
	CONSTRAINT [FK_dbo.Notes_dbo.ClassID] FOREIGN KEY ([ClassID]) REFERENCES [dbo].[Class] ([ClassID])
);


-- Grades Table
CREATE TABLE dbo.Grades
(	
	GradesID	INT IDENTITY (1,1) NOT NULL,
	UsersID INT NOT NULL,
	AssignmentID INT NOT NULL,
	Grader NVARCHAR(64),
	Comment NVARCHAR(256),
	Grade	INT NOT NULL,
	CONSTRAINT [PK_dbo.Grades] PRIMARY KEY CLUSTERED (GradesID ASC),
	CONSTRAINT [FK_dbo.Grades_dbo.UserID] FOREIGN KEY ([UsersID]) REFERENCES [dbo].[Users] ([UsersID]),
	CONSTRAINT [FK_dbo.Grades_dbo.AssignmentID] FOREIGN KEY ([AssignmentID]) REFERENCES [dbo].[Assignment] ([AssignmentID])
);

-- Questions Table
CREATE TABLE dbo.Questions
(	
	QuestionsID	INT IDENTITY (1,1) NOT NULL,
	AssignmentID INT NOT NULL,
	Text	NVARCHAR(500) NOT NULL,
	Weight INT NOT NULL,
	Answer INT NOT NULL,
	Flagged BIT NOT NULL,
	CONSTRAINT [PK_dbo.Questions] PRIMARY KEY CLUSTERED (QuestionsID ASC),
	CONSTRAINT [FK_dbo.Questions_dbo.AssignmentID] FOREIGN KEY ([AssignmentID]) REFERENCES [dbo].[Assignment] ([AssignmentID])
);

CREATE TABLE dbo.Documents(  
    Id INT IDENTITY(1,1) NOT NULL,  
    Name NVARCHAR(250) NOT NULL,  
    ContentType NVARCHAR(250) NOT NULL,  
	Data VARBINARY(MAX) NOT NULL,
	submitted DateTime NOT NULL,
	ClassID INT NOT NULL,
	AssignmentID INT NOT NULL,
	UserID INT NOT NULL,
	Grade INT NOT NULL,
	Date DATETIME NOT NULL,
	CONSTRAINT [PK_dbo.Documents] PRIMARY KEY CLUSTERED (Id ASC),
	CONSTRAINT [FK_dbo.Documents_dbo.ClassID] FOREIGN KEY ([ClassID]) REFERENCES [dbo].[Class] ([ClassID]),
	CONSTRAINT [FK_dbo.Documents_dbo.UserID] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UsersID]),
	CONSTRAINT [FK_dbo.Documents_dbo.AssignmentID] FOREIGN KEY ([AssignmentID]) REFERENCES [dbo].[Assignment] ([AssignmentID])
);

CREATE TABLE dbo.Quizzes(
	QuizID INT IDENTITY(1,1) NOT NULL,
	QuizName NVARCHAR(256) NOT NULL,
	StartTime DateTime NOT NULL,
	EndTime DateTime NOT NULL,
	ClassID INT NOT NULL,
	IsHidden BIT NOT NULL,
	TotalPoints INT,
	CONSTRAINT [PK_dbo.Quizzes] PRIMARY KEY CLUSTERED (QuizID ASC),
	CONSTRAINT [FK_dbo.Quizzes_dbo.ClassID] FOREIGN KEY ([ClassID]) REFERENCES [dbo].[Class] ([ClassID])
	ON DELETE CASCADE
);
 

CREATE TABLE dbo.QuizQuestions(
	QuestionID INT IDENTITY(1,1) NOT NULL,
	QuizID INT NOT NULL,
	TypeOfQuestion INT NOT NULL,
	Points INT NOT NULL,
	QuestionText NVARCHAR(512) NOT NULL,
	CONSTRAINT [PK_dbo.QuizQuestions] PRIMARY KEY CLUSTERED (QuestionID ASC),
	CONSTRAINT [FK_dbo.Questions_dbo.Quizzes] FOREIGN KEY ([QuizID]) REFERENCES [dbo].[Quizzes] ([QuizID])
	ON DELETE CASCADE
	ON UPDATE CASCADE,
);
Create TABLE MultChoiceAnswers(
	AnswerID INT IDENTITY(1,1) NOT NULL,
	QuestionID INT NOT NULL,
	Answer1 NVARCHAR (512) NOT NULL,
	Answer2 NVARCHAR (512),
	Answer3 NVARCHAR (512),
	Answer4 NVARCHAR (512),
	CorrectAnswer INT NOT NULL,
	CONSTRAINT [PK_dbo.MultChoiceAnswers] PRIMARY KEY CLUSTERED (AnswerID ASC),
	CONSTRAINT [FK_dbo.MultChoiceAnswers_dbo.QuizQuestions] FOREIGN KEY ([QuestionID]) REFERENCES [dbo].[QuizQuestions] ([QuestionID])
	ON DELETE CASCADE
	ON UPDATE CASCADE,
);
  
