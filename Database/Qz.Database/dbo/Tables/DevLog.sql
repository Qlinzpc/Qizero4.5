CREATE TABLE [dbo].[DevLog] (
    [AppName]    NVARCHAR (100)  NULL,
    [ModuleName] NVARCHAR (100)  NULL,
    [ProcName]   NVARCHAR (100)  NULL,
    [LogLevel]   NVARCHAR (100)  NULL,
    [LogTitle]   NVARCHAR (100)  NULL,
    [LogMessage] NVARCHAR (100)  NULL,
    [LogDate]    NVARCHAR (100)  NULL,
    [StackTrace] NVARCHAR (4000) NULL,
    [NewTest]    INT             NULL
);

