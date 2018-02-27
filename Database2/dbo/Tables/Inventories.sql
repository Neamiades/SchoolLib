CREATE TABLE [dbo].[Inventories] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [ActNumber] INT            NULL,
    [BookId]    INT            NULL,
    [Couse]     NVARCHAR (50)  NULL,
    [Note]      NVARCHAR (250) NULL,
    [Year]      SMALLINT       NULL,
    CONSTRAINT [PK_Inventories] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Inventories_Books_BookId] FOREIGN KEY ([BookId]) REFERENCES [dbo].[Books] ([BookId]) ON DELETE CASCADE
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Inventories_BookId]
    ON [dbo].[Inventories]([BookId] ASC);

