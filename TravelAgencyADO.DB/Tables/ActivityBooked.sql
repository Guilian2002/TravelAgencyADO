CREATE TABLE [dbo].[ActivityBooked]
(
	[BookId] UNIQUEIDENTIFIER NOT NULL,
    [ActivityId] UNIQUEIDENTIFIER NOT NULL,

    CONSTRAINT [PK_ActivityBooked] PRIMARY KEY ([BookId], [ActivityId]),

    CONSTRAINT [FK_ActivityBooked_Booking]
        FOREIGN KEY ([BookId])
        REFERENCES [Booking]([Id])
        ON DELETE CASCADE,

    CONSTRAINT [FK_ActivityBooked_Activity]
        FOREIGN KEY ([ActivityId])
        REFERENCES [Activity]([Id])
        ON DELETE CASCADE
)
