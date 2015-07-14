CREATE TABLE [dbo].[MyLog] (
    [CreateDate] NVARCHAR (100) NULL,
    [Origin]     NVARCHAR (100) NULL,
    [LogLevel]   NVARCHAR (100) NULL,
    [Message]    NVARCHAR (MAX) NULL,
    [StackTrace] NVARCHAR (100) NULL
);

