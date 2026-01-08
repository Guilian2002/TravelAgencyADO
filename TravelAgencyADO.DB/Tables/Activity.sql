CREATE TABLE [dbo].[Activity]
(
	[Id] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    [Title] NVARCHAR(150) NOT NULL,
    [Price] DECIMAL(18,2) NOT NULL,
    [Description] NVARCHAR(200) NOT NULL,
    [DestinationId] UNIQUEIDENTIFIER NOT NULL,

    CONSTRAINT [PK_Activity] PRIMARY KEY ([Id]),
    CONSTRAINT [CK_Activity__Price] CHECK ([Price] > 0),
    CONSTRAINT [FK_Activity_Destination]
        FOREIGN KEY ([DestinationId])
        REFERENCES [Destination]([Id])
);
