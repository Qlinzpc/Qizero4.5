CREATE TABLE [dbo].[NLog_Error] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [time_stamp] DATETIME       CONSTRAINT [DF_NLogError_time_stamp] DEFAULT (getdate()) NOT NULL,
    [host]       NVARCHAR (MAX) NOT NULL,
    [type]       NVARCHAR (50)  NOT NULL,
    [source]     NVARCHAR (50)  NOT NULL,
    [message]    NVARCHAR (MAX) NOT NULL,
    [level]      NVARCHAR (50)  NOT NULL,
    [logger]     NVARCHAR (50)  NOT NULL,
    [stacktrace] NVARCHAR (MAX) NOT NULL,
    [allxml]     NTEXT          NOT NULL,
    [detail]     NVARCHAR (MAX) DEFAULT ('') NOT NULL,
    CONSTRAINT [PK_NLogError] PRIMARY KEY CLUSTERED ([Id] ASC)
);

