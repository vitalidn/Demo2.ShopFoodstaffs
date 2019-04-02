USE [Shopdb]
IF OBJECT_ID(N'Foodstuffs', N'U') IS NULL
BEGIN 
CREATE TABLE [Foodstuffs] (
    [Id]     INT            NOT NULL,
    [Name]   NVARCHAR (MAX) NULL,
    [Price]  FLOAT (53)     NOT NULL,
    [Weight] FLOAT (53)     NOT NULL,
    [Cost]   FLOAT (53)     NOT NULL,
    CONSTRAINT [PK_Foodstuffs] PRIMARY KEY CLUSTERED ([Id] ASC)
)
END