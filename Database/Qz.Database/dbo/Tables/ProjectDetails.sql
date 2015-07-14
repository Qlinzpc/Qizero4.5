CREATE TABLE [dbo].[ProjectDetails] (
    [Id]                        INT             IDENTITY (1, 1) NOT NULL,
    [ProjectNo]                 NVARCHAR (30)   NOT NULL,
    [Address]                   NVARCHAR (500)  NOT NULL,
    [TransfereeDate]            DATETIME        NOT NULL,
    [DurableYears]              NVARCHAR (10)   NOT NULL,
    [HousingUse]                NVARCHAR (50)   NULL,
    [LandUse]                   NVARCHAR (50)   NULL,
    [LandArea]                  DECIMAL (18, 2) NULL,
    [OverallFloorage]           DECIMAL (18, 2) NULL,
    [PreSaleTotal]              INT             DEFAULT ((0)) NULL,
    [PreSaleArea]               DECIMAL (18, 2) NULL,
    [SalePhone1]                NVARCHAR (20)   NULL,
    [SalePhone2]                NVARCHAR (20)   NULL,
    [PropertyManagementCompany] NVARCHAR (500)  NULL,
    [ManagementCost]            NVARCHAR (20)   NULL,
    [Remark]                    NVARCHAR (MAX)  NULL,
    [CreateTime]                DATETIME        DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

