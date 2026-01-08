CREATE TABLE [dbo].[Destination]
(
	[Id] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    [Country] NVARCHAR(150) NOT NULL,
    [City] NVARCHAR(100) NOT NULL,
    [Description] NVARCHAR(255) NOT NULL,

    CONSTRAINT [PK_Destination] PRIMARY KEY ([Id]),
    CONSTRAINT [UQ_Destinations_Country] UNIQUE ([Country])
)
