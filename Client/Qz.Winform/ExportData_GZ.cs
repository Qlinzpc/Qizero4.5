using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Qz.Winform
{
    public partial class ExportDataGZ : Form
    {
        public ExportDataGZ()
        {
            InitializeComponent();
        }

        private async void btnInit_Click(object sender, EventArgs e)
        {
            SQLHelper.ConnectionString = "server=192.169.55.19;uid=estate;pwd=123;database=estate_gz";

            if (this.cbReInit.Checked)
            {
                if (MessageBox.Show(string.Format(@"是否重新进行初始化 ?", "提示", MessageBoxButtons.OKCancel)) == System.Windows.Forms.DialogResult.Cancel) return;

                this.btnInit.Enabled = false;
                this.btnInit.Text = "正在初始化";

                var rs = await InitAsync();

                if (rs)
                {
                    this.btnExport.Enabled = true;
                    this.numCount.Enabled = true;
                    this.cbAll.Enabled = true;
                    this.dtpDate.Enabled = true;
                }
                else
                {
                    MessageBox.Show("初始化失败 !");
                }

                this.btnInit.Enabled = true;
                this.btnInit.Text = "初始化";
            }
            else
            {
                this.btnExport.Enabled = true;
                this.numCount.Enabled = true;
                this.cbAll.Enabled = true;
                this.dtpDate.Enabled = true;

                this.btnInit.Enabled = true;
                this.btnInit.Text = "初始化";
            }
        }

        private async void btnExport_Click(object sender, EventArgs e)
        {
            var count = 0;
            var maxValue = 0;
            var exportCount = 0;
            var name = string.Format("广州真房源回访 ( {0}-{1} ) ", this.dtpDate.Value.ToString("MM"), this.dtpDate.Value.ToString("dd"));
            bool rs = false;

            this.btnExport.Enabled = false;
            this.btnExport.Text = "正在导出";

            try
            {
                this.progress.Minimum = 0;

                if (this.cbAll.Checked)
                {
                    maxValue = AllCount();
                    this.progress.Maximum = maxValue;
                    this.lblTip.Text = "提示: 0/" + maxValue;

                    while (IsExists())
                    {
                        count++;

                        rs = await ExportAsync(name + count, (rows) =>
                        {
                            exportCount += rows;
                            this.progress.Value = exportCount;
                            this.lblTip.Text = "提示: " + exportCount + "/" + maxValue;
                        });

                        if (!rs)
                        {
                            MessageBox.Show("导出失败, 请联系管理员 !");
                            break;
                        }

                        Modify();
                    }

                }
                else
                {
                    count = (int)this.numCount.Value;
                    if (count <= 0)
                    {
                        MessageBox.Show("请选择导出批次数量 !");
                        return;
                    }

                    this.progress.Maximum = count;
                    this.lblTip.Text = "提示: 0/" + count;

                    for (int i = 0; i < count; i++)
                    {
                        rs = await ExportAsync(name + (i + 1), (rows) =>
                        {
                            this.progress.Value = i + 1;
                            this.lblTip.Text = "提示: " + (i + 1) + "/" + count;
                        });

                        if (!rs)
                        {
                            MessageBox.Show("导出失败, 请联系管理员 !");
                            break;
                        }

                        Modify();
                    }
                }

                MessageBox.Show(string.Format("导出完成, 共导出 {0}批!", count));

                this.btnExport.Enabled = true;
                this.btnExport.Text = "导 出";
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0}", ex.Message));
            }
        }

        private void cbAll_CheckedChanged(object sender, EventArgs e)
        {
            this.numCount.Enabled = !this.cbAll.Checked;
        }

        private Task<bool> InitAsync()
        {

            return Task.Run(() =>
            {
                #region Init sql

                var sql = string.Format(@"
IF OBJECT_ID('dbo.t_housing_check') IS NOT NULL 
DROP TABLE dbo.t_housing_check

SELECT 
	h.Id 房源编号,
	h.ename + ' ' + h.buildName + ' ' +CAST( h.roomNo AS VARCHAR(10)) + ' ' +CAST( h.mark1 AS VARCHAR(10)) 楼盘名称,
	CASE h.tradeType when 1 then '出售' when 2 then '出租' when 3 then '售租' end 交易类型	,
	h.[owner] 业主姓名	,
	h.ownerTel 业主电话	,
	h.acreage 面积	,
	CASE h.tradeType when 1 then h.totalprice when 2 then h.leaseprice end 价格 ,
	h.markUser 所属人	,
	h.markUId 所属人ID	,
	h.markUDept 组别	,
	h.area + ' ' + dname 区域	,
	h.addDate 录入时间	,
	h.remark 备注

INTO 
	dbo.t_housing_check 
	
FROM 
	dbo.t_housing (NOLOCK) h 
WHERE 
	1=1
	AND h.dltFlag=0 
	AND h.isValid=0 
	AND h.[status]=1  -- 当前盘
	AND h.status1!=1 -- 不是成交盘 
	AND h.tradeType=1  -- 售盘 
	AND h.[use]=1

INSERT INTO dbo.t_user_data_filter
        ( userid ,
          username ,
          deptid ,
          name ,
          Expr2 ,
          Expr4 ,
          Expr6 ,
          IsFilter
        )
        
SELECT 
	userid,
	user_name username,
    deptid ,
    name ,
    Expr2 ,
    Expr4 ,
    Expr6 ,
    0 IsFilter
    
FROM 
	dbo.v_lp_userDeptSuperior (NOLOCK)
WHERE 
	Expr6 IS NOT NULL AND Expr6 IN ('营运事业部') AND user_name NOT LIKE '%公盘%' AND 
	userid IN ( SELECT 所属人ID FROM dbo.t_housing_check GROUP BY 所属人ID ) 
	AND userid NOT IN ( SELECT userid FROM dbo.t_user_data_filter )
ORDER BY 
	Expr6, Expr4, Expr2, name 

UPDATE dbo.t_user_data_filter SET IsFilter = 3 
WHERE userid NOT IN (
	SELECT 
		userid
	FROM 
		dbo.v_lp_userDeptSuperior (nolock)
	WHERE 
		Expr6 IS NOT NULL AND Expr6 IN ('营运事业部') AND user_name NOT LIKE '%公盘%' 
		AND userid IN ( SELECT 所属人ID FROM dbo.t_housing_check GROUP BY 所属人ID ) 
)

DELETE FROM dbo.t_user_data_filter WHERE IsFilter = 3

");
                #endregion

                try
                {
                    SQLHelper.ExecuteNonQuery(sql);
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("{0}", ex.Message));

                    return false;
                }
            });

        }

        private Task<bool> ExportAsync(string name = "", Action<int> callback = null)
        {
            return Task.Run(() =>
            {
                #region SQL 语句
                var sql = @"
SET NOCOUNT ON 

DECLARE @1 varchar(500),@2 VARCHAR(500),@3 VARCHAR(500) ,@4 VARCHAR(500) 
DECLARE @sql VARCHAR(max), @Id VARCHAR(20)

declare system_singe cursor for  
	
	SELECT 
		所属人ID, 所属人, 组别,COUNT(1) cnt
	FROM 
		dbo.t_housing_check 
	WHERE 
		所属人 NOT LIKE '%公盘%' AND 所属人ID IN (
			SELECT DISTINCT userid FROM dbo.t_user_data_filter 
		) AND 房源编号 NOT IN (
			SELECT HousingId FROM dbo.t_housing_data_filter WHERE IsFilter = 1
		)
	GROUP BY 
		所属人ID, 所属人 , 组别
	ORDER BY 
		组别, cnt 
	
open system_singe
fetch next from system_singe into @1,@2,@3,@4;
	while @@fetch_status = 0
		BEGIN
			
			SET @sql = 'INSERT INTO dbo.t_housing_data_filter (HousingId,IsFilter)  
SELECT TOP 1 房源编号 HousingId, 0 IsFilter FROM dbo.t_housing_check WHERE 房源编号 NOT IN (
	SELECT HousingId FROM dbo.t_housing_data_filter WHERE IsFilter = 1
) AND 所属人ID = ' + @1
	
			EXEC( @sql )
			
			fetch next from system_singe into @1,@2,@3,@4;
		END
    close system_singe
	deallocate system_singe 

SELECT 
    房源编号 ,
    楼盘名称 ,
    交易类型 ,
    业主姓名 ,
    业主电话 ,
    面积 ,
    价格 ,
    所属人 ,
    组别 ,
    区域 ,
    CONVERT(VARCHAR(30),录入时间,120) 录入时间,
    备注
FROM dbo.t_housing_check WHERE 房源编号 IN (
	SELECT HousingId FROM dbo.t_housing_data_filter WHERE IsFilter = 0
)
";
                #endregion

                var ds = SQLHelper.ExecuteDataSet(sql);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                {
                    var dt = ds.Tables[0];

                    var path = AppDomain.CurrentDomain.BaseDirectory + "Export\\广州 " + this.dtpDate.Value.ToString("yyyy-MM-dd");

                    if (!System.IO.Directory.Exists(path)) Directory.CreateDirectory(path);

                    Dictionary<string, DataTable> dic = new Dictionary<string, DataTable>();

                    dic.Add(dt.Rows.Count + " 条记录", dt);

                    if (callback != null) callback(dt.Rows.Count);

                    if (string.IsNullOrEmpty(name))
                    {
                        name = string.Format("广州真房源回访 ( {0}-{1} ) ", this.dtpDate.Value.ToString("MM"), this.dtpDate.Value.ToString("dd"));
                    }

                    NPOIExcelHelper.DataTableToExcel(dic, "", path + "\\" + name + ".xls");
                }

                return true;
            });
        }

        private bool IsExists()
        {
            var rs = SQLHelper.ExecuteScalar("select count(1) from dbo.t_housing_check where 房源编号 not in ( select HousingId from dbo.t_housing_data_filter where IsFilter <> 0 )");

            return (int)rs > 0;
        }

        private int AllCount()
        {
            var rs = SQLHelper.ExecuteScalar("select count(1) from dbo.t_housing_check where 房源编号 not in ( select HousingId from dbo.t_housing_data_filter where IsFilter = 2 )");

            return (int)rs;
        }

        private void Modify()
        {
            SQLHelper.ExecuteNonQuery("UPDATE dbo.t_housing_data_filter SET IsFilter = 1 WHERE IsFilter = 0 ");
        }

    }
}
