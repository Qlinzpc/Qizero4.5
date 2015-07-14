USE [General Permissions System]
GO



SET ANSI_NULLS ON
GO



SET QUOTED_IDENTIFIER ON
GO



SET ANSI_PADDING ON
GO



IF OBJECT_ID('RoleModuleMap') IS NOT NULL DROP TABLE dbo.RoleModuleMap

IF OBJECT_ID('RoleModuleButtonMap') IS NOT NULL DROP TABLE dbo.RoleModuleButtonMap

IF OBJECT_ID('RoleModuleColumnMap') IS NOT NULL DROP TABLE dbo.RoleModuleColumnMap

IF OBJECT_ID('ModuleButtonMap') IS NOT NULL DROP TABLE dbo.ModuleButtonMap

IF OBJECT_ID('Buttons') IS NOT NULL DROP TABLE dbo.Buttons

IF OBJECT_ID('Modules') IS NOT NULL DROP TABLE dbo.Modules

IF OBJECT_ID('DepartmentRoles') IS NOT NULL DROP TABLE dbo.DepartmentRoles

IF OBJECT_ID('UserRoles') IS NOT NULL DROP TABLE dbo.UserRoles

IF OBJECT_ID('UserPermissions') IS NOT NULL DROP TABLE dbo.UserPermissions

IF OBJECT_ID('RolePermissions') IS NOT NULL DROP TABLE dbo.RolePermissions

IF OBJECT_ID('Applications') IS NOT NULL DROP TABLE dbo.Applications

IF OBJECT_ID('SysLog') IS NOT NULL DROP TABLE dbo.[SysLog]

IF OBJECT_ID('LoginLog') IS NOT NULL DROP TABLE dbo.[LoginLog]

IF OBJECT_ID('DbBackup') IS NOT NULL DROP TABLE dbo.[DbBackup]

IF OBJECT_ID('DataCodeType') IS NOT NULL DROP TABLE dbo.[DataCodeType]

IF OBJECT_ID('DataCodes') IS NOT NULL DROP TABLE dbo.[DataCodes]

IF OBJECT_ID('Users') IS NOT NULL DROP TABLE dbo.Users

IF OBJECT_ID('Permissions') IS NOT NULL DROP TABLE dbo.[PERMISSIONS]

IF OBJECT_ID('Roles') IS NOT NULL DROP TABLE dbo.Roles

IF OBJECT_ID('UserSettings') IS NOT NULL DROP TABLE dbo.UserSettings

IF OBJECT_ID('Departments') IS NOT NULL DROP TABLE dbo.[Departments]

IF OBJECT_ID('Company') IS NOT NULL DROP TABLE dbo.[Company]


-- 系统应用表
CREATE TABLE Applications(
	[Id] INT IDENTITY(1000,1) PRIMARY KEY,
	[Name] NVARCHAR(30) NOT NULL ,
	[Code] VARCHAR(30) NOT NULL,
	[Remark] VARCHAR(200) NULL,
	[Enabled] INT DEFAULT(0) NOT NULL,
	[SortCode] INT DEFAULT(0) NOT NULL,
	[IsDelete] INT DEFAULT(0) NOT NULL,
	[CreateDate] DATETIME DEFAULT(GETDATE()) NOT NULL,
	[CreateUserId] VARCHAR(50) NOT NULL,
	[CreateUserName] NVARCHAR(50) NOT NULL,
	[ModifyUserId] VARCHAR(50) NULL,
	[ModifyUserName] NVARCHAR(50) NULL,
	[ModifyDate] DATETIME NULL
)

-- 系统日志表 
CREATE TABLE SysLog(
	[Id] INT IDENTITY(1,1) PRIMARY KEY ,
	[UserId] INT NOT NULL,
	[UserName] NVARCHAR(30) NOT NULL,
	[Location] VARCHAR(100) NOT NULL,
	[Action] VARCHAR(100) NOT NULL,
	[Type] VARCHAR(30) NOT NULL,
	[Message] VARCHAR(8000) NOT NULL,
	[CreateDate] DATETIME DEFAULT(GETDATE()) NOT NULL
)

-- 登录日志表 
CREATE TABLE LoginLog(
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[UserId] INT NOT NULL,
	[UserName] NVARCHAR(30) NOT NULL,
	[HostName] VARCHAR(50) NOT NULL,
	[HostIP] VARCHAR(50) NOT NULL,
	[LoginMsg] VARCHAR(8000) NOT NULL ,
	[LoginDate] DATETIME DEFAULT(GETDATE()) NOT NULL
)

-- 数据库备份表 
CREATE TABLE DbBackup(
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[ServerName] [nvarchar](50) NOT NULL,
	[DbName] [nvarchar](50) NOT NULL,
	[JobName] [varchar](50) NOT NULL,
	[Mode] [nvarchar](5) NOT NULL,
	[StartTime] [datetime] DEFAULT(GETDATE()) NOT NULL,
	[FilePath] [nvarchar](50) NOT NULL,
	[Remark] [nvarchar](500) NULL,
	[Enabled] INT DEFAULT(0) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[CreateUserName] [nvarchar](50) NOT NULL,
	[CreateDate] DATETIME DEFAULT(GETDATE()) NOT NULL
)

-- 数据编码类型 
CREATE TABLE DataCodeType(
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[CodeType] [varchar](100) NOT NULL,
	[CodeTypeName] [varchar](200) NULL,
	[Remark] [nvarchar](500) NULL,
	[Enabled] INT DEFAULT(0) NOT NULL,
	[SortCode] [int] DEFAULT(0) NOT NULL,
	[IsDelete] [int] DEFAULT(0) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[CreateUserName] [nvarchar](50) NOT NULL,
	[CreateDate] DATETIME DEFAULT(GETDATE()) NOT NULL,
	[ModifyUserId] [int] DEFAULT(0) NULL,
	[ModifyUserName] [nvarchar](50) NULL,
	[ModifyDate] DATETIME DEFAULT(GETDATE()) NULL
)

-- 数据编码 
CREATE TABLE DataCodes(
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[Code] [varchar](100) NOT NULL,
	[Value] [varchar](200) NULL,
	[Text] [varchar](200) NULL,
	[ParentCode] [varchar](100) NULL,
	[CodeType] [varchar](100) NULL,
	[CodeTypeName] [varchar](200) NULL,
	[Remark] [nvarchar](500) NULL,
	[Enabled] INT DEFAULT(0) NOT NULL,
	[SortCode] [int] DEFAULT(0) NOT NULL,
	[IsDelete] [int] DEFAULT(0) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[CreateUserName] [nvarchar](50) NOT NULL,
	[CreateDate] DATETIME DEFAULT(GETDATE()) NOT NULL,
	[ModifyUserId] [int] DEFAULT(0) NULL,
	[ModifyUserName] [nvarchar](50) NULL,
	[ModifyDate] DATETIME DEFAULT(GETDATE()) NULL
)

-- 公司表 
CREATE TABLE Company(
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[ParentId] INT NOT NULL,
	[Category] [varchar](50) NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[FullName] [varchar](50) NOT NULL,
	[ShortName] [varchar](50) NULL,
	[Nature] [varchar](50) NOT NULL,
	[Manager] [varchar](50) NOT NULL,
	[Contact] [varchar](50) NOT NULL,
	[Phone] [varchar](50) NOT NULL,
	[Remark] [nvarchar](500) NULL,
	[Enabled] INT DEFAULT(0) NOT NULL,
	[SortCode] [int] DEFAULT(0) NOT NULL,
	[IsDelete] [int] DEFAULT(0) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[CreateUserName] [nvarchar](50) NOT NULL,
	[CreateDate] DATETIME DEFAULT(GETDATE()) NOT NULL,
	[ModifyUserId] [int] DEFAULT(0) NULL,
	[ModifyUserName] [nvarchar](50) NULL,
	[ModifyDate] DATETIME DEFAULT(GETDATE()) NULL
)

-- 部门表 
CREATE TABLE Departments(
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[CompanyId] INT NOT NULL,
	[ParentId] INT NOT NULL,
	[Code] [varchar](50) NULL,
	[FullName] [varchar](50) NULL,
	[ShortName] [varchar](50) NULL,
	[Nature] [varchar](50) NULL,
	[Manager] [varchar](50) NULL,
	[Phone] [varchar](50) NULL,
	[Remark] [nvarchar](500) NULL,
	[Enabled] INT DEFAULT(0) NOT NULL,
	[SortCode] [int] DEFAULT(0) NOT NULL,
	[IsDelete] [int] DEFAULT(0) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[CreateUserName] [nvarchar](50) NOT NULL,
	[CreateDate] DATETIME DEFAULT(GETDATE()) NOT NULL,
	[ModifyUserId] [int] DEFAULT(0) NULL,
	[ModifyUserName] [nvarchar](50) NULL,
	[ModifyDate] DATETIME DEFAULT(GETDATE()) NULL,
	CONSTRAINT FK_company_dept FOREIGN KEY([CompanyId]) REFERENCES [dbo].[Company](Id)
)

-- 用户表 
CREATE TABLE Users(
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[CompanyId] INT NOT NULL,
	[DepartmentId] INT NOT NULL ,
	[InnerUser] [int] DEFAULT(0) NULL,
	[Code] [varchar](255) NULL,
	[Account] [varchar](255) NULL,
	[Password] [varchar](255) NULL,
	[Secretkey] [varchar](255) NULL,
	[UserName] [nvarchar](50) NULL,
	[Spell] [varchar](50) NULL,
	[Gender] [varchar](5) NULL,
	[Birthday] [varchar](50) NULL,
	[Mobile] [varchar](50) NULL,
	[Telephone] [varchar](50) NULL,
	[ChangePasswordDate] [datetime] DEFAULT(GETDATE()) NULL,
	[OpenId] [int] DEFAULT(0) NULL,
	[LoginCount] [int] DEFAULT(0) NULL,
	[FirstVisit] [datetime] DEFAULT(GETDATE()) NULL,
	[PreviousVisit] [datetime] DEFAULT(GETDATE()) NULL,
	[LastVisit] [datetime] DEFAULT(GETDATE()) NULL,
	[Online] [int] DEFAULT(0) NULL,
	[Config] VARCHAR(1000) NULL,
	[Remark] [nvarchar](500) NULL,
	[Enabled] INT DEFAULT(0) NOT NULL,
	[SortCode] [int] DEFAULT(0) NOT NULL,
	[IsDelete] [int] DEFAULT(0) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[CreateUserName] [nvarchar](50) NOT NULL,
	[CreateDate] DATETIME DEFAULT(GETDATE()) NOT NULL,
	[ModifyUserId] [int] DEFAULT(0) NULL,
	[ModifyUserName] [nvarchar](50) NULL,
	[ModifyDate] DATETIME DEFAULT(GETDATE()) NULL,
	CONSTRAINT FK_company_user FOREIGN KEY([CompanyId]) REFERENCES [dbo].[Company](Id),
	CONSTRAINT FK_dept_user FOREIGN KEY([DepartmentId]) REFERENCES [dbo].[Departments](Id)
	
)

-- 用户设置表 
CREATE TABLE UserSettings(
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[UserId] INT NOT NULL,
	[Code] [varchar](100) NULL,
	[Name] [varchar](200) NULL,
	[Value] [varchar](100) NULL,
	[Remark] [nvarchar](500) NULL,
	[Enabled] INT DEFAULT(0) NOT NULL,
	[SortCode] [int] DEFAULT(0) NOT NULL,
	[IsDelete] [int] DEFAULT(0) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[CreateUserName] [nvarchar](50) NOT NULL,
	[CreateDate] DATETIME DEFAULT(GETDATE()) NOT NULL,
	[ModifyUserId] [int] DEFAULT(0) NULL,
	[ModifyUserName] [nvarchar](50) NULL,
	[ModifyDate] DATETIME DEFAULT(GETDATE()) NULL
)

-- 角色表 
CREATE TABLE Roles(
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[RoleName] [varchar](200) NULL,
	[Remark] [nvarchar](1000) NULL,
	[Enabled] INT DEFAULT(0) NOT NULL,
	[SortCode] [int] DEFAULT(0) NOT NULL,
	[IsDelete] [int] DEFAULT(0) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[CreateUserName] [nvarchar](50) NOT NULL,
	[CreateDate] DATETIME DEFAULT(GETDATE()) NOT NULL,
	[ModifyUserId] [int] DEFAULT(0) NULL,
	[ModifyUserName] [nvarchar](50) NULL,
	[ModifyDate] DATETIME DEFAULT(GETDATE()) NULL
)

-- 权限表 
CREATE TABLE Permissions(
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[Name] [varchar](200) NULL,
	[ParentId] INT DEFAULT(0) NOT NULL,
	[Remark] [nvarchar](500) NULL,
	[Enabled] INT DEFAULT(0) NOT NULL,
	[SortCode] [int] DEFAULT(0) NOT NULL,
	[IsDelete] [int] DEFAULT(0) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[CreateUserName] [nvarchar](50) NOT NULL,
	[CreateDate] DATETIME DEFAULT(GETDATE()) NOT NULL,
	[ModifyUserId] [int] DEFAULT(0) NULL,
	[ModifyUserName] [nvarchar](50) NULL,
	[ModifyDate] DATETIME DEFAULT(GETDATE()) NULL
)

-- 模块, 菜单表  
CREATE TABLE Modules(
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[ParentId] INT DEFAULT(0) NOT NULL,
	[ApplicationId] INT NOT NULL,
	[Name] [varchar](200) NULL,
	[URL] [varchar](200) NULL,
	[Icon] [varchar](50) NULL,
	[IconURL] [varchar](200) NULL,
	[Remark] [nvarchar](500) NULL,
	[Enabled] INT DEFAULT(0) NOT NULL,
	[SortCode] [int] DEFAULT(0) NOT NULL,
	[IsDelete] [int] DEFAULT(0) NOT NULL,
	[IsVisible] INT DEFAULT(0) NOT NULL,	
	[CreateUserId] [int] NOT NULL,
	[CreateUserName] [nvarchar](50) NOT NULL,
	[CreateDate] DATETIME DEFAULT(GETDATE()) NOT NULL,
	[ModifyUserId] [int] DEFAULT(0) NULL,
	[ModifyUserName] [nvarchar](50) NULL,
	[ModifyDate] DATETIME DEFAULT(GETDATE()) NULL,
	CONSTRAINT FK_application_module FOREIGN KEY ([ApplicationId]) REFERENCES [dbo].Applications(Id)
)

-- 按钮表  
CREATE TABLE Buttons(
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[Name] [varchar](20) NULL,
	[Value] [varchar](20) NULL,
	[Icon] [varchar](50) NULL,
	[Remark] [nvarchar](500) NULL,
	[Enabled] INT DEFAULT(0) NOT NULL,
	[SortCode] [int] DEFAULT(0) NOT NULL,
	[IsDelete] [int] DEFAULT(0) NOT NULL,
	[CreateUserId] [int] NOT NULL,
	[CreateUserName] [nvarchar](50) NOT NULL,
	[CreateDate] DATETIME DEFAULT(GETDATE()) NOT NULL,
	[ModifyUserId] [int] DEFAULT(0) NULL,
	[ModifyUserName] [nvarchar](50) NULL,
	[ModifyDate] DATETIME DEFAULT(GETDATE()) NULL
)

-- 部门角色表 
CREATE TABLE DepartmentRoles(
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	DepartmentId INT NOT NULL,
	RoleId INT NOT NULL,
	CONSTRAINT FK_dr_dept FOREIGN KEY(DepartmentId) REFERENCES [dbo].[Departments](Id),
	CONSTRAINT FK_dr_role FOREIGN KEY(RoleId) REFERENCES [dbo].[Roles](Id)
)

-- 用户角色表 
CREATE TABLE UserRoles(
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[UserId] INT NOT NULL,
	[RoleId] INT NOT NULL, 
	CONSTRAINT FK_ur_user FOREIGN KEY(UserId) REFERENCES [dbo].[Users](Id),
	CONSTRAINT FK_ur_role FOREIGN KEY(RoleId) REFERENCES [dbo].[Roles](Id)
)

-- 用户权限表 
CREATE TABLE UserPermissions(
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[UserId] INT NOT NULL,
	[PermissionId] INT NOT NULL, 
	CONSTRAINT FK_up_user FOREIGN KEY(UserId) REFERENCES [dbo].[Users](Id),
	CONSTRAINT FK_up_permission FOREIGN KEY(PermissionId) REFERENCES [dbo].[Permissions](Id)
)

-- 角色权限表 
CREATE TABLE RolePermissions(
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[RoleId] INT NOT NULL,
	[PermissionId] INT NOT NULL, 
	CONSTRAINT FK_rp_role FOREIGN KEY(RoleId) REFERENCES [dbo].[Roles](Id),
	CONSTRAINT FK_rp_permission FOREIGN KEY(PermissionId) REFERENCES [dbo].[Permissions](Id)
)

-- 角色模块映射表 
CREATE TABLE RoleModuleMap(
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[RoleId] INT NOT NULL,
	[ModuleId] INT NOT NULL, 
	CONSTRAINT FK_rm_role FOREIGN KEY([RoleId]) REFERENCES [dbo].[Roles](Id),
	CONSTRAINT FK_rm_module FOREIGN KEY([ModuleId]) REFERENCES [dbo].[Modules](Id)
)

-- 角色模块按钮映射表 
CREATE TABLE RoleModuleButtonMap(
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[RoleId] INT NOT NULL,
	[ModuleId] INT NOT NULL, 
	[ButtonId] INT NOT NULL, 
	CONSTRAINT FK_rmb_role FOREIGN KEY([RoleId]) REFERENCES [dbo].[Roles](Id),
	CONSTRAINT FK_rmb_module FOREIGN KEY([ModuleId]) REFERENCES [dbo].[Modules](Id),
	CONSTRAINT FK_rmb_button FOREIGN KEY([ButtonId]) REFERENCES [dbo].[Buttons](Id)
)

-- 角色模块列映射表 
CREATE TABLE RoleModuleColumnMap(
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[RoleId] INT NOT NULL,
	[ModuleId] INT NOT NULL, 
	[IsReject] INT DEFAULT(0) NOT NULL,
	[FieleName] VARCHAR(1000) DEFAULT('') NOT NULL, 
	CONSTRAINT FK_rmc_role FOREIGN KEY([RoleId]) REFERENCES [dbo].[Roles](Id),
	CONSTRAINT FK_rmc_module FOREIGN KEY([ModuleId]) REFERENCES [dbo].[Modules](Id)
)

-- 模块按钮映射表 
CREATE TABLE ModuleButtonMap(
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[ModuleId] INT NOT NULL, 
	[ButtonId] INT NOT NULL, 
	CONSTRAINT FK_mb_module FOREIGN KEY([ModuleId]) REFERENCES [dbo].[Modules](Id),
	CONSTRAINT FK_mb_button FOREIGN KEY([ButtonId]) REFERENCES [dbo].[Buttons](Id)
)

