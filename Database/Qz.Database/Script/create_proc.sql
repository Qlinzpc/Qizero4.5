/**
 * 存储过程 proc_select_module_by_user 
 * 描述: 根据用户 ( User ) 获得授权模块 
 *	参数: 
 *		@UserId INT 用户Id 
 *		@ApplicationId INT 应用Id 
 *		@ParentId INT 父Id 
 *  
 * 执行: exec proc_select_module_by_user 1, 1001, 0 
*/
IF OBJECT_ID('proc_select_module_by_user') IS NOT NULL 
DROP PROC proc_select_module_by_user
GO 

CREATE PROC proc_select_module_by_user 
	@UserId INT,
	@ApplicationId INT,
	@ParentId INT
AS 
	
SET NOCOUNT ON -- 不返回计数，提高执行效率   
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED -- 相当于 ( NOLOCK ) 指定语句不能读取由其它事务修改但尚未提交的数据，允许脏读；
SET XACT_ABORT ON -- 语句产生运行时错误，整个事务将终止并回滚   

	SELECT 
		Id ,
		Name ,
		URL ,
		Icon ,
		Code,
		ParentId,
		( SELECT COUNT(1) FROM dbo.Modules WHERE ParentId = m.Id AND Id IN (
			SELECT ModuleId FROM dbo.RoleModuleMap WHERE RoleId IN (
				SELECT RoleId FROM dbo.UserRoles WHERE UserId = @UserId 
			)
		) AND ApplicationId = @ApplicationId AND IsDelete = 0 AND [Enabled] = 0  ) SubMenu
	FROM 
		dbo.Modules m
	 WHERE 
		Id IN (
			SELECT ModuleId FROM dbo.RoleModuleMap WHERE RoleId IN (
				SELECT RoleId FROM dbo.UserRoles WHERE UserId = @UserId 
			)
		) AND ApplicationId = @ApplicationId AND m.IsDelete = 0 AND m.[Enabled] = 0
	ORDER BY 
		SortCode ,
		Code

GO

/**
 * 存储过程 proc_select_module_by_role 
 * 描述: 根据角色 ( Role ) 获得授权模块 
 *	参数: 
 *		@RoleId INT 角色Id 
 *  
 * 执行: exec proc_select_module_by_role 8 exec proc_select_module_by_role 9 
*/
IF OBJECT_ID('proc_select_module_by_role') IS NOT NULL 
DROP PROC proc_select_module_by_role
GO 

CREATE PROC proc_select_module_by_role 
	@RoleId INT 
AS 
	
SET NOCOUNT ON -- 不返回计数，提高执行效率   
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED -- 相当于 ( NOLOCK ) 指定语句不能读取由其它事务修改但尚未提交的数据，允许脏读；
SET XACT_ABORT ON -- 语句产生运行时错误，整个事务将终止并回滚   

	SELECT 
		Id 
	FROM 
		(
			SELECT 
				m.Id,
			( SELECT COUNT(1) FROM dbo.Modules WHERE ParentId = m.Id AND Id IN (
				SELECT ModuleId FROM dbo.RoleModuleMap WHERE RoleId = @RoleId 
			) AND IsDelete = 0 AND [Enabled] = 0  ) SubMenu
			FROM 
				dbo.Modules m
			WHERE 
				Id IN (
					SELECT ModuleId FROM dbo.RoleModuleMap WHERE RoleId = @RoleId 
				) AND m.IsDelete = 0 AND m.[Enabled] = 0 
		) temp 
	WHERE
		temp.SubMenu = 0
GO

/**
 * 存储过程 proc_select_button_module_role 
 * 描述: 根据模块 ( Module ) 和 角色 ( Role ) 获得授权模块按钮 
 *	参数: 
 *		@ModuleId INT 模块Id 
 *		@RoleId INT 角色Id 
 *  
 * 执行: exec proc_select_button_by_module_role 2, 8 
*/
IF OBJECT_ID('proc_select_button_by_module_role') IS NOT NULL 
DROP PROC proc_select_button_by_module_role
GO 

CREATE PROC proc_select_button_by_module_role 
	@ModuleId INT,
	@RoleId INT 
AS 

SET NOCOUNT ON -- 不返回计数，提高执行效率   
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED -- 相当于 ( NOLOCK ) 指定语句不能读取由其它事务修改但尚未提交的数据，允许脏读；
SET XACT_ABORT ON -- 语句产生运行时错误，整个事务将终止并回滚   	
	
	SELECT  
		Id ,
        Name ,
        Value ,
        Icon 
	FROM 
		dbo.Buttons 
	WHERE 
		Id IN (
				SELECT 
					ButtonId 
				FROM 
					dbo.RoleModuleButtonMap 
				
				WHERE ModuleId = @ModuleId AND RoleId = @RoleId AND ButtonId IN (
					SELECT ButtonId FROM dbo.ModuleButtonMap WHERE ModuleId = @ModuleId 
				)
				
			) AND IsDelete = 0 AND [Enabled] = 0 
	ORDER BY 
		SortCode 
		
GO

/**
 * 存储过程 proc_select_button_module
 * 描述: 根据模块 ( Module ) 获得授权模块按钮 
 *	参数: 
 *		@ModuleId INT 模块Id 
 *  
 * 执行: exec proc_select_button_by_module 2 
*/
IF OBJECT_ID('proc_select_button_by_module') IS NOT NULL 
DROP PROC proc_select_button_by_module
GO  

CREATE PROC proc_select_button_by_module 
	@ModuleId INT 
AS 

SET NOCOUNT ON -- 不返回计数，提高执行效率   
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED -- 相当于 ( NOLOCK ) 指定语句不能读取由其它事务修改但尚未提交的数据，允许脏读；
SET XACT_ABORT ON -- 语句产生运行时错误，整个事务将终止并回滚   	
	
	SELECT  
		Id ,
        Name ,
        Value ,
        Icon 
	FROM 
		dbo.Buttons 
	WHERE 
		(Id IN (
				SELECT ButtonId FROM dbo.ModuleButtonMap WHERE ModuleId = @ModuleId 
			) OR @ModuleId = -1 ) AND IsDelete = 0 AND [Enabled] = 0
	ORDER BY 
		SortCode 
		
GO 

/**
 * 存储过程 proc_login 
 * 描述: 
 *		1. 根据用户名和密码登陆 
 *		2. 修改登录信息 
 * 
 *	参数: 
 *		@Account VARCHAR(255) 账号 
 *		@Password VARCHAR(255) 密码 
 *  
 * 执行: exec proc_login 'zpc', '9utODs7xP+Y=' 
*/
IF OBJECT_ID('proc_login') IS NOT NULL 
DROP PROC proc_login
GO 

CREATE PROC proc_login 
	@Account VARCHAR(255) ,
	@Password VARCHAR(255)
AS 

SET NOCOUNT ON -- 不返回计数，提高执行效率   
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED -- 相当于 ( NOLOCK ) 指定语句不能读取由其它事务修改但尚未提交的数据，允许脏读；
SET XACT_ABORT ON -- 语句产生运行时错误，整个事务将终止并回滚   

	DECLARE @Id INT , @LoginCount INT
	
	SELECT 
		@Id = Id ,
		@LoginCount = LoginCount
	FROM 
		dbo.Users 
	WHERE 
		IsDelete = 0 AND 
		[Enabled] = 0 AND 
		Account = @Account AND 
		[Password] = @Password
	
	-- 1. 根据用户名和密码登陆 
	SELECT  
		u.Id ,
		CompanyId ,
		DepartmentId ,
		Code ,
		Account ,
		Secretkey ,
		UserName ,
		Mobile ,
		Telephone ,
		OpenId ,
		LoginCount ,
		FirstVisit ,
		PreviousVisit ,
		LastVisit ,
		Config ,
		Remark ,
		r.RoleId ,
		( SELECT RoleName FROM dbo.Roles WHERE Id = r.RoleId ) RoleName 
		
	FROM 
		dbo.Users u
	LEFT JOIN 
		dbo.UserRoles r ON r.UserId = u.Id 
		
	WHERE 
		u.Id = @Id
	
	IF @LoginCount > 0
		BEGIN
			UPDATE 
				dbo.Users 
			SET 
				[Online] = 1,
				LoginCount = LoginCount + 1,
				PreviousVisit = LastVisit,
				LastVisit = GETDATE()
			WHERE 
				dbo.Users.Id = @Id
		END 
	ELSE
		BEGIN
			UPDATE 
				dbo.Users 
			SET 
				[Online] = 1,
				LoginCount = LoginCount + 1,
				FirstVisit = GETDATE(),
				LastVisit = GETDATE()
			WHERE 
				dbo.Users.Id = @Id
		END 

GO

SELECT * FROM dbo.Modules
SELECT * FROM dbo.RoleModuleMap

SELECT * FROM dbo.SysLog ORDER BY CreateDate DESC 
SELECT * FROM dbo.LoginLog ORDER BY LoginDate DESC 

Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) 1.0.0.0 Safari/537.36 
http://192.169.55.240:1010/Login/Index
