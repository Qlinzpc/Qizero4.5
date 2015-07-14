CREATE TABLE [dbo].[n_customer_wash_tag_total] (
    [Id]               INT IDENTITY (1, 1) NOT NULL,
    [CustomerId]       INT NOT NULL,
    [NPowerOff]        INT DEFAULT ((0)) NOT NULL,
    [NOccupy]          INT DEFAULT ((0)) NOT NULL,
    [NStop]            INT DEFAULT ((0)) NOT NULL,
    [NMissedCall]      INT DEFAULT ((0)) NOT NULL,
    [NRestrictedPhone] INT DEFAULT ((0)) NOT NULL,
    [NInvalidPhone]    INT DEFAULT ((0)) NOT NULL,
    [NOutOfReach]      INT DEFAULT ((0)) NOT NULL,
    [NOther]           INT DEFAULT ((0)) NOT NULL,
    [YToSelf]          INT DEFAULT ((0)) NOT NULL,
    [YLeasedSold]      INT DEFAULT ((0)) NOT NULL,
    [YNotRentSell]     INT DEFAULT ((0)) NOT NULL,
    [YPeers]           INT DEFAULT ((0)) NOT NULL,
    [YNotOneself]      INT DEFAULT ((0)) NOT NULL,
    [YHangUp]          INT DEFAULT ((0)) NOT NULL,
    [YOther]           INT DEFAULT ((0)) NOT NULL
);

