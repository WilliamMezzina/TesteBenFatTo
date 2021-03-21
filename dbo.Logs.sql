CREATE TABLE [dbo].[Logs] (
    [Id]            INT           NOT NULL identity(1,1),
    [IP]            VARCHAR (15)  NOT NULL,
    [Usuario]          VARCHAR (100) NOT NULL,
    [HoraLog]       DATETIME      NOT NULL,
    [Comando]       VARCHAR (10)  NOT NULL,
    [Site]          VARCHAR (100) NOT NULL,
    [Protocolo]     VARCHAR (10)  NOT NULL,
    [PreviousState] VARCHAR(10)           NOT NULL,
    [ActualState]   VARCHAR(10)           NOT NULL,
    [Destino]       VARCHAR (100) NULL,
    [UserAgent]     VARCHAR (500) NULL, 
    CONSTRAINT [PK_Logs] PRIMARY KEY ([Id]) 
);

