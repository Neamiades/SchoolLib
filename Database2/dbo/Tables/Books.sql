CREATE TABLE [dbo].[Books] (
    [BookId]        INT            NOT NULL,
    [Author]        NVARCHAR (60)  NULL,
    [AuthorCipher]  NVARCHAR (25)  NULL,
    [Discriminator] NVARCHAR (30)  NULL,
    [Name]          NVARCHAR (250)  NULL,
    [Note]          NVARCHAR (250) NULL,
    [Price]         NVARCHAR (10)   NULL,
    [Published]     SMALLINT       NULL,
    [Status]        INT            NULL,
    [Cipher]        NVARCHAR (40)  NULL,
    [Language]      NVARCHAR (30)  NULL,
    [Genre]         NVARCHAR (50)  NULL,
    [Grade]         TINYINT        NULL,
    [Subject]       NVARCHAR (40)  NULL,
    CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED ([BookId] ASC)
);

