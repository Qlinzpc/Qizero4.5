﻿CREATE TABLE [dbo].[n_customer_wash] (
    [customer_id]         INT            NOT NULL,
    [customer_name]       NVARCHAR (50)  NOT NULL,
    [customer_tel]        VARCHAR (11)   NOT NULL,
    [customer_tel_append] VARCHAR (50)   NULL,
    [customer_category]   INT            NOT NULL,
    [customer_source]     INT            NOT NULL,
    [customer_remarks]    NVARCHAR (500) NULL,
    [customer_level]      INT            NOT NULL,
    [customer_paytype]    INT            NOT NULL,
    [city_ids]            VARCHAR (200)  NULL,
    [city_names]          NVARCHAR (200) NULL,
    [area_ids]            VARCHAR (200)  NULL,
    [area_names]          NVARCHAR (200) NULL,
    [estate_ids]          VARCHAR (200)  NULL,
    [estate_names]        NVARCHAR (200) NULL,
    [price]               INT            NULL,
    [acreage]             INT            NULL,
    [room]                INT            NULL,
    [customer_isself]     INT            NOT NULL,
    [customer_dltflag]    INT            NOT NULL,
    [follow_newstatus]    INT            NULL,
    [follow_newsdate]     DATETIME       NULL,
    [seehousing_count]    INT            NULL,
    [owner_userid]        INT            NULL,
    [owner_username]      NVARCHAR (20)  NULL,
    [owner_deptid]        INT            NULL,
    [owner_deptname]      NVARCHAR (20)  NULL,
    [owner_date]          DATETIME       NULL,
    [WashDate]            DATETIME       NULL,
    [WashUserId]          INT            DEFAULT ((0)) NOT NULL,
    [DataStatus]          INT            DEFAULT ((0)) NOT NULL,
    [DltFlag]             INT            DEFAULT ((0)) NOT NULL
);

