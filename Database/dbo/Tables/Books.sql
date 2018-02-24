CREATE TABLE [dbo].[Books] (
    [BookId]        INT            NOT NULL,
    [Author]        NVARCHAR (30)  NOT NULL,
    [AuthorCipher]  NVARCHAR (15)  NOT NULL,
    [Discriminator] NVARCHAR (30)  NOT NULL,
    [Name]          NVARCHAR (60)  NOT NULL,
    [Note]          NVARCHAR (250) NULL,
    [Price]         NVARCHAR (6)   NOT NULL,
    [Published]     SMALLINT       NOT NULL,
    [Status]        INT            NOT NULL,
    [Cipher]        NVARCHAR (20)  NULL,
    [Language]      NVARCHAR (15)  NULL,
    [Genre]         NVARCHAR (20)  NULL,
    [Grade]         TINYINT        NULL,
    [Subject]       NVARCHAR (20)  NULL,
    CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED ([BookId] ASC)
);

