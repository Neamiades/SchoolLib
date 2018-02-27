CREATE TABLE [dbo].[Readers] (
    [ReaderId]              INT            NOT NULL,
    [Apartment]             SMALLINT       NULL,
    [Discriminator]         NVARCHAR (30)  NULL,
    [FirstName]             NVARCHAR (15)  NULL,
    [FirstRegistrationDate] NVARCHAR (10)  NULL,
    [House]                 NVARCHAR (8)   NULL,
    [LastRegistrationDate]  NVARCHAR (10)  NULL,
    [Note]                  NVARCHAR (250) NULL,
    [Patronimic]            NVARCHAR (25)  NULL,
    [Status]                INT            NULL,
    [Street]                NVARCHAR (25)  NULL,
    [SurName]               NVARCHAR (20)  NULL,
    [Grade]                 NVARCHAR (10)  NULL,
    [Position]              NVARCHAR (50)  NULL,
    CONSTRAINT [PK_Readers] PRIMARY KEY CLUSTERED ([ReaderId] ASC)
);

