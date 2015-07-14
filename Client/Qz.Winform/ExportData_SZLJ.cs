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
    public partial class ExportDataLJ : Form
    {
        public ExportDataLJ()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var dt = Common.NPOIExcelHelper.Import(@"D:\Work\深圳链家真房源回访数据.xls");

            this.dgvData.DataSource = dt;

            return;

            var dtColum = dt.Columns;

            var sql = "select * into t_housing_data from @t";

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@t";
            parameter.Value = dt;
            parameter.TypeName = "HousingData";
            parameter.SqlDbType = SqlDbType.Structured;

            SQLHelper.ExecuteNonQuery(sql, CommandType.Text, parameter);

            //声明SqlBulkCopy ,using释放非托管资源  
            //using (SqlBulkCopy sqlBC = new SqlBulkCopy(conn))
            //{
            //    //一次批量的插入的数据量  
            //    sqlBC.BatchSize = 1000;
            //    //超时之前操作完成所允许的秒数，如果超时则事务不会提交 ，数据将回滚，所有已复制的行都会从目标表中移除  
            //    sqlBC.BulkCopyTimeout = 60;
            //    //設定 NotifyAfter 属性，以便在每插入10000 条数据时，呼叫相应事件。   
            //    sqlBC.NotifyAfter = 10000;
            //    // sqlBC.SqlRowsCopied += new SqlRowsCopiedEventHandler(OnSqlRowsCopied);  
            //    //设置要批量写入的表  
            //    sqlBC.DestinationTableName = "HousingData";
            //    //自定义的datatable和数据库的字段进行对应  
            //    //sqlBC.ColumnMappings.Add("id", "tel");  
            //    //sqlBC.ColumnMappings.Add("name", "neirong");  
            //    for (int i = 0; i < dtColum.Count; i++)
            //    {
            //        sqlBC.ColumnMappings.Add(dtColum[i].ColumnName.ToString(), dtColum[i].ColumnName.ToString());
            //    }
            //    //批量写入  
            //    sqlBC.WriteToServer(dt);
            //}

        }

        private async void button2_Click(object sender, EventArgs e)
        {

            var count = 0;
            var maxValue = 0;
            var exportCount = 0;
            var name = string.Format("深圳链家真房源回访 ( {0}-{1} ) ", this.dtpDate.Value.ToString("MM"), this.dtpDate.Value.ToString("dd"));
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

        private Task<bool> ExportAsync(string name = "", Action<int> callback = null)
        {
            return Task.Run(() =>
            {
                #region SQL 语句
                var sql = @"
SET NOCOUNT ON 

DECLARE @1 varchar(500),@2 VARCHAR(500)
DECLARE @sql VARCHAR(max), @Id VARCHAR(20)

declare system_singe cursor for  
	
	SELECT 
		所属店组 ,
		COUNT(1) cnt
	FROM 
		dbo.t_housing_data 
	GROUP BY 
		所属店组
	ORDER BY 
		所属店组, cnt  
		
open system_singe
fetch next from system_singe into @1,@2;
	while @@fetch_status = 0
		BEGIN
			
			SET @sql = 'INSERT INTO dbo.t_housing_data_filter (Id,IsFilter)  
SELECT TOP 1 房源编号 Id, 0 IsFilter FROM dbo.t_housing_data WHERE 房源编号 NOT IN (
	SELECT Id FROM dbo.t_housing_data_filter WHERE IsFilter <> 0
) AND 所属店组 = ''' + @1 + ''''
	
			EXEC( @sql )
			
			fetch next from system_singe into @1,@2;
		END
    close system_singe
	deallocate system_singe 

SELECT 
    房源编号 ,
    业主姓名 ,
    房源业主电话 ,
    录入人 ,
    录入大区 ,
    录入区 ,
    录入店组 ,
    所属人 ,
    所属大区 ,
    所属区 ,
    所属店组 ,
    委托来源大类 ,
    委托来源细分 ,
    类型 ,
    带看次数 ,
    最近一次带看时间 ,
    是否实勘 ,
    是否有钥匙 ,
    居室 ,
    城区 ,
    楼盘名称 ,
    新建时间 ,
    挂牌出售价格 ,
    挂牌月租金 ,
    建筑面积 ,
    出租面积
FROM dbo.t_housing_data WHERE 房源编号 IN (
	SELECT Id FROM dbo.t_housing_data_filter WHERE IsFilter = 0
)";
                #endregion

                var ds = SQLHelper.ExecuteDataSet(sql);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null)
                {
                    var dt = ds.Tables[0];

                    var path = AppDomain.CurrentDomain.BaseDirectory + "Export\\深圳链家 " + this.dtpDate.Value.ToString("yyyy-MM-dd");

                    if (!System.IO.Directory.Exists(path)) Directory.CreateDirectory(path);

                    Dictionary<string, DataTable> dic = new Dictionary<string, DataTable>();

                    dic.Add(dt.Rows.Count + " 条记录", dt);

                    if (callback != null) callback(dt.Rows.Count);

                    if (string.IsNullOrEmpty(name))
                    {
                        name = string.Format("深圳链家真房源回访 ( {0}-{1} ) ", this.dtpDate.Value.ToString("MM"), this.dtpDate.Value.ToString("dd"));
                    }

                    NPOIExcelHelper.DataTableToExcel(dic, "", path + "\\" + name + ".xls");
                }

                return true;
            });
        }

        private bool IsExists()
        {
            var rs = SQLHelper.ExecuteScalar("select count(1) from dbo.t_housing_data where 房源编号 not in ( select Id from dbo.t_housing_data_filter where IsFilter <> 0 )");

            return (int)rs > 0;
        }

        private int AllCount()
        {
            var rs = SQLHelper.ExecuteScalar("select count(1) from dbo.t_housing_data where 房源编号 not in ( select Id from dbo.t_housing_data_filter where IsFilter = 2 )");

            return (int)rs;
        }

        private void Modify()
        {
            SQLHelper.ExecuteNonQuery("UPDATE dbo.t_housing_data_filter SET IsFilter = 1 WHERE IsFilter = 0 ");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.numCount.Enabled = !this.cbAll.Checked;
        }

        private void ExportDataLJ_Load(object sender, EventArgs e)
        {

        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format(@"是否进行初始化 ?", "提示", MessageBoxButtons.OKCancel)) == System.Windows.Forms.DialogResult.Cancel) return;

            SQLHelper.ConnectionString = "Data Source=.;Initial Catalog=Qlin;Integrated Security=True;MultipleActiveResultSets=True";

            this.btnInit.Enabled = false;
            this.btnInit.Text = "正在初始化";

            this.btnCreate.Enabled = true;
            this.btnExport.Enabled = true;
            this.numCount.Enabled = true;
            this.cbAll.Enabled = true;
            this.dtpDate.Enabled = true;

            this.btnInit.Enabled = true;
            this.btnInit.Text = "初始化";
        }

    }
}
