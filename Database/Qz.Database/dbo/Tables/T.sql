﻿CREATE TABLE [dbo].[T] (
    [RowNumber]        INT              IDENTITY (0, 1) NOT NULL,
    [EventClass]       INT              NULL,
    [ApplicationName]  NVARCHAR (128)   NULL,
    [ClientProcessID]  INT              NULL,
    [DatabaseID]       INT              NULL,
    [DatabaseName]     NVARCHAR (128)   NULL,
    [EventSequence]    BIGINT           NULL,
    [GroupID]          INT              NULL,
    [Handle]           INT              NULL,
    [HostName]         NVARCHAR (128)   NULL,
    [IsSystem]         INT              NULL,
    [LoginName]        NVARCHAR (128)   NULL,
    [LoginSid]         IMAGE            NULL,
    [NTDomainName]     NVARCHAR (128)   NULL,
    [NTUserName]       NVARCHAR (128)   NULL,
    [RequestID]        INT              NULL,
    [SPID]             INT              NULL,
    [ServerName]       NVARCHAR (128)   NULL,
    [SessionLoginName] NVARCHAR (128)   NULL,
    [StartTime]        DATETIME         NULL,
    [TransactionID]    BIGINT           NULL,
    [XactSequence]     BIGINT           NULL,
    [CPU]              INT              NULL,
    [Duration]         BIGINT           NULL,
    [EndTime]          DATETIME         NULL,
    [Error]            INT              NULL,
    [Reads]            BIGINT           NULL,
    [RowCounts]        BIGINT           NULL,
    [TextData]         NTEXT            NULL,
    [Writes]           BIGINT           NULL,
    [IntegerData]      INT              NULL,
    [IntegerData2]     INT              NULL,
    [LineNumber]       INT              NULL,
    [NestLevel]        INT              NULL,
    [Offset]           INT              NULL,
    [EventSubClass]    INT              NULL,
    [ObjectID]         INT              NULL,
    [ObjectName]       NVARCHAR (128)   NULL,
    [ObjectType]       INT              NULL,
    [SqlHandle]        IMAGE            NULL,
    [State]            INT              NULL,
    [MethodName]       NVARCHAR (128)   NULL,
    [BinaryData]       IMAGE            NULL,
    [IndexID]          INT              NULL,
    [Success]          INT              NULL,
    [GUID]             UNIQUEIDENTIFIER NULL,
    [SourceDatabaseID] INT              NULL,
    [BigintData1]      BIGINT           NULL,
    [Mode]             INT              NULL,
    [ObjectID2]        BIGINT           NULL,
    [OwnerID]          INT              NULL,
    [Type]             INT              NULL,
    [Severity]         INT              NULL,
    PRIMARY KEY CLUSTERED ([RowNumber] ASC)
);

