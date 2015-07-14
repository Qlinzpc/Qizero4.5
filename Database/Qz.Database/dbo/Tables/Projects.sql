CREATE TABLE [dbo].[Projects] (
    [Id]                INT             IDENTITY (1, 1) NOT NULL,
    [ProjectNo]         NVARCHAR (30)   NOT NULL,
    [ProjectName]       NVARCHAR (100)  NOT NULL,
    [Area]              NVARCHAR (30)   NOT NULL,
    [DevelopEnterprise] NVARCHAR (1000) NULL,
    [ApproveTime]       DATETIME        NOT NULL,
    [PreSaleXuNo]       NVARCHAR (50)   NOT NULL,
    [CreateTime]        DATETIME        DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

