CREATE TABLE [dbo].[n_customer_wash_record] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [CustomerId]   INT           NOT NULL,
    [WashDate]     DATETIME      DEFAULT (getdate()) NULL,
    [WashUserId]   INT           NOT NULL,
    [WashUserName] NVARCHAR (30) NULL,
    [WashDeptId]   INT           NOT NULL,
    [WashDeptName] NVARCHAR (30) NULL,
    [WashTag]      NVARCHAR (10) NOT NULL,
    [DltFlag]      INT           DEFAULT ((0)) NOT NULL
);

