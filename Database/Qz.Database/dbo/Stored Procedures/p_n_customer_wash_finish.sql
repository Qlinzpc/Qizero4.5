/**
 *
 * 洗客完成 p_n_customer_wash_finish 
 * Add By ZPC 2014-12-22 10:46:12   
 *  
*/
create PROC [dbo].[p_n_customer_wash_finish] 
@CustomerId INT = 0, -- 客源Id 
@fType NVARCHAR(20) = '',
@fDeptId INT = 0,
@fDept VARCHAR(30) = '',
@fUserId INT = 0,
@fUser VARCHAR(30) = '',
@fContent VARCHAR(5000) = '',
@fDate DATETIME = GETDATE,
@fWashTag NVARCHAR(20) = '', -- 洗客标签 
@tagId NVARCHAR(30) = 'YOther',-- 洗客标签 Id 
@type INT = 0, -- 0正常洗客完成 1转私客 
@msg NVARCHAR(30) OUTPUT 
AS 

SET NOCOUNT ON -- 不返回计数，提高执行效率       
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED -- 指定语句不能读取由其它事务修改但尚未提交的数据，避免脏读；其它事务可在当前事务的各语句之间更改数据，从面产生不可重复读取和纪像数据。      
SET XACT_ABORT ON -- 语句产生运行时错误，整个事务将终止并回滚       

BEGIN
	BEGIN TRAN 
	
	-- 正常洗客完成 
	IF(@type = 0)
		BEGIN
			-- 1.0 修改洗客状态 
			UPDATE dbo.n_customer_wash SET DataStatus = 2, WashUserId=@fUserId, WashDate = @fDate WHERE customer_id = @CustomerId
		END 
	-- 转私客 
	ELSE
		BEGIN  
			-- 1.0 判断是否已经转为 私客 
			IF EXISTS(SELECT 1 FROM dbo.n_customer WHERE [customer_isself] = 1 AND customer_dltflag = 0 AND customer_id = @CustomerId )
				BEGIN
					SET @msg = '该客已被转私客, 转客失败, 进入下一条洗客数据 .'
					RETURN 0
				END 
				
			-- 1.2 删除 洗客数据 
			DELETE FROM dbo.n_customer_wash WHERE customer_id = @CustomerId 
			
		END 
		
	-- 2.0 记录跟进信息 
	INSERT INTO dbo.n_customer_follow(
		customer_id ,
        follow_userid ,
        follow_username ,
        follow_deptid ,
        follow_deptname ,
        follow_type ,
        follow_status ,
        follow_content ,
        follow_dltflag ,
        follow_date)
	VALUES(
		@CustomerId,
		@fUserId,
		@fUser,
		@fDeptId,
		@fDept,
		1,
		0,
		@fContent,
		0,
		@fDate
		);
	
	-- 2.1 插入洗客记录表 
	INSERT INTO dbo.n_customer_wash_record(
		CustomerId,WashDate,WashUserId,WashUserName,WashDeptId,WashDeptName,WashTag,DltFlag) 
	VALUES 
		(@CustomerId,@fDate,@fUserId,@fUser,@fDeptId,@fDept,@fWashTag,0)
	
	IF NOT EXISTS( SELECT 1 FROM dbo.n_customer_wash_tag_total WHERE CustomerId = @CustomerId )
		BEGIN
			INSERT INTO dbo.n_customer_wash_tag_total (CustomerId) VALUES (@CustomerId)
		END 
		
	-- 2.3 更新洗客标签统计表 
	DECLARE @sql VARCHAR(4000) 
	SET @sql = 'UPDATE dbo.n_customer_wash_tag_total SET '+ @tagId +' += 1 WHERE CustomerId = '+ CAST( @CustomerId AS VARCHAR(20))
	
	-- PRINT @sql
	EXEC(@sql)
	
	IF(@@ERROR <> 0) 
		BEGIN
			ROLLBACK TRAN 
			SET @msg = '操作失败, 进入下一条洗客数据 .'
			RETURN 0
		END 
	
	COMMIT TRAN 
	SET @msg = '操作成功, 进入下一条洗客数据 .'
	RETURN 1
	
END

