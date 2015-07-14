/**
 * �洢���� proc_select_module_by_user 
 * ����: �����û� ( User ) �����Ȩģ�� 
 *	����: 
 *		@UserId INT �û�Id 
 *		@ApplicationId INT Ӧ��Id 
 *		@ParentId INT ��Id 
 *  
 * ִ��: exec proc_select_module_by_user 1, 1001, 0 
*/
IF OBJECT_ID('proc_select_module_by_user') IS NOT NULL 
DROP PROC proc_select_module_by_user
GO 

CREATE PROC proc_select_module_by_user 
	@UserId INT,
	@ApplicationId INT,
	@ParentId INT
AS 
	
SET NOCOUNT ON -- �����ؼ��������ִ��Ч��   
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED -- �൱�� ( NOLOCK ) ָ����䲻�ܶ�ȡ�����������޸ĵ���δ�ύ�����ݣ����������
SET XACT_ABORT ON -- ����������ʱ��������������ֹ���ع�   

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
 * �洢���� proc_select_module_by_role 
 * ����: ���ݽ�ɫ ( Role ) �����Ȩģ�� 
 *	����: 
 *		@RoleId INT ��ɫId 
 *  
 * ִ��: exec proc_select_module_by_role 8 exec proc_select_module_by_role 9 
*/
IF OBJECT_ID('proc_select_module_by_role') IS NOT NULL 
DROP PROC proc_select_module_by_role
GO 

CREATE PROC proc_select_module_by_role 
	@RoleId INT 
AS 
	
SET NOCOUNT ON -- �����ؼ��������ִ��Ч��   
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED -- �൱�� ( NOLOCK ) ָ����䲻�ܶ�ȡ�����������޸ĵ���δ�ύ�����ݣ����������
SET XACT_ABORT ON -- ����������ʱ��������������ֹ���ع�   

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
 * �洢���� proc_select_button_module_role 
 * ����: ����ģ�� ( Module ) �� ��ɫ ( Role ) �����Ȩģ�鰴ť 
 *	����: 
 *		@ModuleId INT ģ��Id 
 *		@RoleId INT ��ɫId 
 *  
 * ִ��: exec proc_select_button_by_module_role 2, 8 
*/
IF OBJECT_ID('proc_select_button_by_module_role') IS NOT NULL 
DROP PROC proc_select_button_by_module_role
GO 

CREATE PROC proc_select_button_by_module_role 
	@ModuleId INT,
	@RoleId INT 
AS 

SET NOCOUNT ON -- �����ؼ��������ִ��Ч��   
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED -- �൱�� ( NOLOCK ) ָ����䲻�ܶ�ȡ�����������޸ĵ���δ�ύ�����ݣ����������
SET XACT_ABORT ON -- ����������ʱ��������������ֹ���ع�   	
	
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
 * �洢���� proc_select_button_module
 * ����: ����ģ�� ( Module ) �����Ȩģ�鰴ť 
 *	����: 
 *		@ModuleId INT ģ��Id 
 *  
 * ִ��: exec proc_select_button_by_module 2 
*/
IF OBJECT_ID('proc_select_button_by_module') IS NOT NULL 
DROP PROC proc_select_button_by_module
GO  

CREATE PROC proc_select_button_by_module 
	@ModuleId INT 
AS 

SET NOCOUNT ON -- �����ؼ��������ִ��Ч��   
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED -- �൱�� ( NOLOCK ) ָ����䲻�ܶ�ȡ�����������޸ĵ���δ�ύ�����ݣ����������
SET XACT_ABORT ON -- ����������ʱ��������������ֹ���ع�   	
	
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
 * �洢���� proc_login 
 * ����: 
 *		1. �����û����������½ 
 *		2. �޸ĵ�¼��Ϣ 
 * 
 *	����: 
 *		@Account VARCHAR(255) �˺� 
 *		@Password VARCHAR(255) ���� 
 *  
 * ִ��: exec proc_login 'zpc', '9utODs7xP+Y=' 
*/
IF OBJECT_ID('proc_login') IS NOT NULL 
DROP PROC proc_login
GO 

CREATE PROC proc_login 
	@Account VARCHAR(255) ,
	@Password VARCHAR(255)
AS 

SET NOCOUNT ON -- �����ؼ��������ִ��Ч��   
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED -- �൱�� ( NOLOCK ) ָ����䲻�ܶ�ȡ�����������޸ĵ���δ�ύ�����ݣ����������
SET XACT_ABORT ON -- ����������ʱ��������������ֹ���ع�   

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
	
	-- 1. �����û����������½ 
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
