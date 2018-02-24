CREATE TABLE [dbo].[Drops] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [Couse]    NVARCHAR (30)  NULL,
    [Date]     NVARCHAR (10)  NOT NULL,
    [Note]     NVARCHAR (250) NULL,
    [ReaderId] INT            NOT NULL,
    CONSTRAINT [PK_Drops] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Drops_Readers_ReaderId] FOREIGN KEY ([ReaderId]) REFERENCES [dbo].[Readers] ([ReaderId]) ON DELETE CASCADE
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Drops_ReaderId]
    ON [dbo].[Drops]([ReaderId] ASC);

