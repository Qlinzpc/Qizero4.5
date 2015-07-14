CREATE TABLE [dbo].[Buildings] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [BuildNo]         NVARCHAR (30)  NOT NULL,
    [ProjectNo]       NVARCHAR (30)  NOT NULL,
    [BuildName]       NVARCHAR (30)  NOT NULL,
    [ParentNo]        NVARCHAR (30)  NOT NULL,
    [PlanningLicence] NVARCHAR (100) NOT NULL,
    [ConstrucLicence] NVARCHAR (100) NOT NULL,
    [CreateTime]      DATETIME       DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

