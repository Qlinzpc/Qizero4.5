
/**
 *
 * 随机获得一条洗客数据 
 * Add By ZPC 2014-12-18 12:56:39  
 *  
*/
create PROC [dbo].[p_n_customer_wash_data] 
@CustomerId INT = 0, -- 客源Id ( 上一个洗客Id )
@UserId INT = 0, -- 洗客人Id 
@SqlWhere VARCHAR(8000) = '',  -- 查询条件    
@OrderBy VARCHAR(100) = 'Id',  -- 排序字段中文名(如面积)     
@OrderType VARCHAR(10) = 'DESC' -- 排序类型(如desc,asc)    
AS 

SET NOCOUNT ON -- 不返回计数，提高执行效率       
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED -- 指定语句不能读取由其它事务修改但尚未提交的数据，避免脏读；其它事务可在当前事务的各语句之间更改数据，从面产生不可重复读取和纪像数据。      
SET XACT_ABORT ON -- 语句产生运行时错误，整个事务将终止并回滚       

-- 1. 根据洗客用户Id清除上次, 洗客进行中的数据
UPDATE dbo.n_customer_wash SET DataStatus = 0, WashUserId= 0 WHERE DltFlag = 0 AND WashUserId = @UserId AND  DataStatus = 1 

DECLARE @Sql NVARCHAR(4000) ,@cId INT 

-- 2.0 如果不是标签筛选 
IF(CHARINDEX('n_customer_wash_tag_total',@sqlWhere) = 0) 
	BEGIN
		SET @SqlWhere = ' AND [DataStatus] = 0' + @SqlWhere
	END
ELSE
	BEGIN
		SET @SqlWhere = ' AND [DataStatus] <> 1' + @SqlWhere
	END

-- PRINT @SqlWhere
-- 2.1 随机获取一条洗客数据 Id 
SET @Sql = '
	SELECT TOP 1 @cId = CustomerId FROM ( SELECT TOP 50 customer_id CustomerId FROM dbo.n_customer_wash a WHERE 1=1 '+ @SqlWhere +' AND NOT EXISTS (SELECT 1 FROM dbo.n_customer WHERE customer_isself = 1 AND dltFlag = 0 AND customer_id = a.customer_id ) ) t ORDER BY NEWID() 
 '
EXEC sys.sp_executesql @Sql, N'@cId INT OUTPUT',@cId OUTPUT 
--PRINT @Sql
-- 2.2 根据 Id 查询数据 
SELECT TOP 1  
		customer_id ,
        customer_name ,
        customer_tel ,
        customer_tel_append ,
        customer_category ,
        customer_source ,
        customer_remarks ,
        customer_level 
FROM 
	dbo.n_customer_wash 
WHERE 
	customer_id = @cId

