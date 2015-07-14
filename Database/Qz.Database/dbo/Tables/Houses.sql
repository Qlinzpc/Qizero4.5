CREATE TABLE [dbo].[Houses] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [HouseNo]    NVARCHAR (30) NOT NULL,
    [BuildNo]    NVARCHAR (30) NOT NULL,
    [HouseName]  NVARCHAR (30) NOT NULL,
    [HouseFloor] NVARCHAR (50) NOT NULL,
    [CreateTime] DATETIME      DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO

CREATE INDEX [IX_Houses_HouseName] ON [dbo].[Houses] (HouseName)

GO
