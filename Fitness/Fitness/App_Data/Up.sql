-- ########### Test Table ###########
CREATE TABLE [dbo].[Students]
(
	[ID] INT IDENTITY (1,1) NOT NULL,
	[FirstName] NVARCHAR (50) NOT NULL,
	[LastName] NVARCHAR (50) NOT NULL,
	CONSTRAINT [PK_dbo.Students] PRIMARY KEY CLUSTERED ([ID] ASC)
);