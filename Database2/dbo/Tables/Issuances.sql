CREATE TABLE [dbo].[Issuances] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [AcceptanceDate] NVARCHAR (10)  NULL,
    [BookId]         INT            NULL,
    [Couse]          NVARCHAR (50)  NULL,
    [IssueDate]      NVARCHAR (10)  NULL,
    [Note]           NVARCHAR (250) NULL,
    [ReaderId]       INT            NULL,
    [ReaderSign]     NVARCHAR (10)  NULL,
    [UserSign]       NVARCHAR (10)  NULL,
    CONSTRAINT [PK_Issuances] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Issuances_Books_BookId] FOREIGN KEY ([BookId]) REFERENCES [dbo].[Books] ([BookId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Issuances_Readers_ReaderId] FOREIGN KEY ([ReaderId]) REFERENCES [dbo].[Readers] ([ReaderId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_Issuances_BookId]
    ON [dbo].[Issuances]([BookId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Issuances_ReaderId]
    ON [dbo].[Issuances]([ReaderId] ASC);

