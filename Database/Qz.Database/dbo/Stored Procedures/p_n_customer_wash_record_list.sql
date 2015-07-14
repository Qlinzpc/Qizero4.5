
/**
 *
 * 洗客记录数据 p_n_customer_wash_record_list 
 * Add By ZPC 2014-12-18 12:56:39  
 *  
*/
create PROC [dbo].[p_n_customer_wash_record_list] 
@Page int = 1, --当前页    
@PageSize int = 20, --每页显示    
@Total int = 0 output, --总记录数    
@SqlWhere varchar(8000) = '',  -- 查询条件    
@OrderBy varchar(100) = 'WashDate',  -- 排序字段中文名(如面积)     
@OrderType varchar(10) = 'DESC' -- 排序类型(如desc,asc)    
AS 

SET NOCOUNT ON -- 不返回计数，提高执行效率       
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED -- 指定语句不能读取由其它事务修改但尚未提交的数据，避免脏读；其它事务可在当前事务的各语句之间更改数据，从面产生不可重复读取和纪像数据。      
SET XACT_ABORT ON -- 语句产生运行时错误，整个事务将终止并回滚       

declare @tablename varchar(1024)  
declare @keyname varchar(50)      
declare @showclause varchar(1024)      

-- 表名称 , 表的主键    
SELECT @tablename = 'v_n_customer_wash_record',@keyname = 'Id'    

set @showclause = 'a.[CustomerId]
      ,a.[CustomerName]
      ,a.[CustomerCategory]
      ,a.[CustomerSource]
      ,a.[CustomerLevel]
      ,a.[WashDate]
      ,a.[WashTag]
      ,a.[WashUserId]
      ,a.[WashUserName]
      ,a.[WashDeptId]
      ,a.[WashDeptName]'
      
DECLARE @Sql VARCHAR(8000),@SqlTotal NVARCHAR(4000)
set @SqlTotal = 'set @total = (SELECT COUNT(1) FROM [dbo].['+@tablename+'] WHERE 1 = 1 ' + @SqlWhere +')'  
exec sp_executesql @SqlTotal,N'@total int output',@total output        

SET @sql = ' SELECT TOP ('+CAST( @pagesize AS VARCHAR(10))+') '+@showclause+' FROM [dbo].['+@tablename+'] a WHERE a.['+@keyname+'] NOT IN (
	SELECT TOP ('+CAST( (@page-1)*@pagesize AS VARCHAR(10) )+') '+@keyname+' FROM [dbo].['+@tablename+'] WHERE 1=1 '+@SqlWhere+' ORDER BY '+@OrderBy +' ' + @OrderType+'
) '+@SqlWhere+' ORDER BY '+@OrderBy +' ' + @OrderType

--PRINT @Sql 
EXEC(@Sql)

