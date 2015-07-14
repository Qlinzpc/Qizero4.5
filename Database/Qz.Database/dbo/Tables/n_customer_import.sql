CREATE TABLE [dbo].[n_customer_import] (
    [import_batch]      CHAR (36)     NOT NULL,
    [import_name]       NVARCHAR (20) NOT NULL,
    [customer_category] INT           NOT NULL,
    [owner_deptid]      INT           NULL,
    [owner_deptname]    NVARCHAR (20) NULL,
    [owner_date]        DATETIME      NULL,
    [add_userid]        INT           NOT NULL,
    [add_username]      NVARCHAR (20) NOT NULL,
    [add_deptid]        INT           NOT NULL,
    [add_deptname]      NVARCHAR (20) NOT NULL,
    [add_date]          DATETIME      NOT NULL,
    PRIMARY KEY CLUSTERED ([import_batch] ASC)
);

