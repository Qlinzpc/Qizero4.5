CREATE proc addcolumn
@tablename varchar(30),  --表名
@colname varchar(30),    --要加的列名
@coltype varchar(100),   --要加的列类型
@colid int               --加到第几列
as

declare @colid_max int    
declare @sql varchar(1000) --动态sql语句
--------------------------------------------------
if not exists(select  1 from sysobjects 
              where name = @tablename and xtype = 'u')
  begin
  --raiserror  '没有这个表'
  return -1
  end
--------------------------------------------------
if exists(select 1 from syscolumns
          where id = object_id(@tablename) and name = @colname)
  begin
  --raiserror 20002 '这个表已经有这个列了！'
  return -1
  end
--------------------------------------------------
--保证该表的colid是连续的
select @colid_max = max(colid) from syscolumns where id=object_id(@tablename)

if @colid > @colid_max or @colid < 1 
   set @colid = @colid + 1
--------------------------------------------------
set @sql = 'alter table '+@tablename+' add '+@colname+' '+@coltype 
exec(@sql)

select @colid_max = colid 
from syscolumns where id = object_id(@tablename) and name = @colname
if @@rowcount <> 1
  begin
  --raiserror 20003 '加一个新列不成功，请检查你的列类型是否正确'
  return -1
  end
--------------------------------------------------
--打开修改系统表的开关
EXEC sp_configure 'allow updates',1  RECONFIGURE WITH OVERRIDE

--将新列列号暂置为-1
set @sql = 'update syscolumns
            set colid = -1
            where id = object_id('''+@tablename+''') 
                  and colid = '+cast(@colid_max as varchar(10))
exec(@sql)

--将其他列的列号加1
set @sql = 'update syscolumns
            set colid = colid + 1
            where id = object_id('''+@tablename+''') 
                  and colid >= '+cast(@colid as varchar(10))
exec(@sql)

--将新列列号复位
set @sql = 'update syscolumns
            set colid = '+cast(@colid as varchar(10))+'
            where id = object_id('''+@tablename+''') 
                  and name = '''+@colname +''''
exec(@sql)
--------------------------------------------------
--关闭修改系统表的开关
EXEC sp_configure 'allow updates',0  RECONFIGURE WITH OVERRIDE
