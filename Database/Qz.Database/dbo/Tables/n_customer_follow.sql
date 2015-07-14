CREATE TABLE [dbo].[n_customer_follow] (
    [follow_id]       INT            IDENTITY (1, 1) NOT NULL,
    [customer_id]     INT            NOT NULL,
    [follow_userid]   INT            NOT NULL,
    [follow_username] NVARCHAR (20)  NOT NULL,
    [follow_deptid]   INT            NOT NULL,
    [follow_deptname] NVARCHAR (20)  NOT NULL,
    [follow_type]     INT            NOT NULL,
    [follow_status]   INT            NOT NULL,
    [follow_content]  NVARCHAR (500) NOT NULL,
    [follow_dltflag]  INT            DEFAULT ((0)) NOT NULL,
    [follow_date]     DATETIME       NOT NULL,
    PRIMARY KEY CLUSTERED ([follow_id] ASC)
);

