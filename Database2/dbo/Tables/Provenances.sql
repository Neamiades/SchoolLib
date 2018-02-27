CREATE TABLE [dbo].[Provenances] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [BookId]      INT            NULL,
    [Note]        NVARCHAR (250) NULL,
    [Place]       NVARCHAR (30)  NULL,
    [ReceiptDate] NVARCHAR (10)  NULL,
    [WayBill]     INT            NULL,
    CONSTRAINT [PK_Provenances] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Provenances_Books_BookId] FOREIGN KEY ([BookId]) REFERENCES [dbo].[Books] ([BookId]) ON DELETE CASCADE
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Provenances_BookId]
    ON [dbo].[Provenances]([BookId] ASC);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Provenances_WayBill]
    ON [dbo].[Provenances]([WayBill] ASC);

