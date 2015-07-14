
create VIEW [dbo].[v_n_customer_wash_record]
AS
SELECT
	b.Id,
	b.CustomerId,
	c.customer_name CustomerName,
	c.customer_category CustomerCategory,
	c.customer_source CustomerSource,
	c.customer_level CustomerLevel,
	b.WashUserId,
	b.WashUserName,
	b.WashDeptId,
	b.WashDeptName,
	b.WashDate,
	b.WashTag
	
FROM
	dbo.n_customer c 
	INNER JOIN dbo.n_customer_wash_record b ON b.CustomerId = c.customer_id 
	
