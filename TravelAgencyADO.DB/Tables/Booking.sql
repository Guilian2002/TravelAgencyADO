CREATE TABLE [dbo].[Booking]
(
	[Id] UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID(),
    [BookingDate] DATE NOT NULL,
    [ClientName] NVARCHAR(70) NOT NULL,

    CONSTRAINT [PK_Booking] PRIMARY KEY ([Id]),
    CONSTRAINT [CK_Bookind_Date] CHECK ([BookingDate] >= CAST(GETDATE() AS DATE)),
)
