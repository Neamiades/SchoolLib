CREATE TABLE [dbo].[Readers] (
    [ReaderId]              INT            NOT NULL,
    [Apartment]             SMALLINT       NULL,
    [Discriminator]         NVARCHAR (30)  NOT NULL,
    [FirstName]             NVARCHAR (15)  NOT NULL,
    [FirstRegistrationDate] NVARCHAR (10)  NOT NULL,
    [House]                 NVARCHAR (8)   NOT NULL,
    [LastRegistrationDate]  NVARCHAR (10)  NOT NULL,
    [Note]                  NVARCHAR (250) NULL,
    [Patronimic]            NVARCHAR (25)  NOT NULL,
    [Status]                INT            NOT NULL,
    [Street]                NVARCHAR (25)  NOT NULL,
    [SurName]               NVARCHAR (20)  NOT NULL,
    [Grade]                 NVARCHAR (10)  NULL,
    [Position]              NVARCHAR (50)  NULL,
    CONSTRAINT [PK_Readers] PRIMARY KEY CLUSTERED ([ReaderId] ASC)
);

