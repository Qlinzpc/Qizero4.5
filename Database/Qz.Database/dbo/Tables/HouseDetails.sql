CREATE TABLE [dbo].[HouseDetails] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [HouseNo]          NVARCHAR (30) NOT NULL,
    [HouseType]        NVARCHAR (30) NOT NULL,
    [DevelopersOutcry] NVARCHAR (20) NULL,
    [HouseUse]         NVARCHAR (10) NOT NULL,
    [CoveredArea]      NVARCHAR (20) NULL,
    [IndoorArea]       NVARCHAR (20) NULL,
    [AssessmentArea]   NVARCHAR (20) NULL,
    [CreateTime]       DATETIME      CONSTRAINT [DF__HouseDeta__Creat__395884C4] DEFAULT (getdate()) NULL,
    CONSTRAINT [PK__HouseDet__3214EC0737703C52] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [ix_houseno]
    ON [dbo].[HouseDetails]([HouseNo] ASC) WITH (IGNORE_DUP_KEY = ON);


GO
CREATE NONCLUSTERED INDEX [ix_DevelopersOutcry]
    ON [dbo].[HouseDetails]([DevelopersOutcry] ASC);

