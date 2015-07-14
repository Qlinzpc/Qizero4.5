namespace Qz.Winform
{
    partial class ExportDataLJ
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCreate = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.btnExport = new System.Windows.Forms.Button();
            this.numCount = new System.Windows.Forms.NumericUpDown();
            this.cbAll = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.progress = new System.Windows.Forms.ProgressBar();
            this.lblTip = new System.Windows.Forms.Label();
            this.btnInit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCount)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCreate
            // 
            this.btnCreate.Enabled = false;
            this.btnCreate.Location = new System.Drawing.Point(85, 11);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(59, 23);
            this.btnCreate.TabIndex = 0;
            this.btnCreate.Text = "生 成";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgvData
            // 
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(12, 70);
            this.dgvData.Name = "dgvData";
            this.dgvData.RowTemplate.Height = 23;
            this.dgvData.Size = new System.Drawing.Size(818, 292);
            this.dgvData.TabIndex = 1;
            // 
            // btnExport
            // 
            this.btnExport.AutoSize = true;
            this.btnExport.Enabled = false;
            this.btnExport.Location = new System.Drawing.Point(150, 12);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(59, 23);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "导 出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.button2_Click);
            // 
            // numCount
            // 
            this.numCount.Enabled = false;
            this.numCount.Location = new System.Drawing.Point(319, 15);
            this.numCount.Name = "numCount";
            this.numCount.Size = new System.Drawing.Size(91, 21);
            this.numCount.TabIndex = 3;
            // 
            // cbAll
            // 
            this.cbAll.AutoSize = true;
            this.cbAll.Enabled = false;
            this.cbAll.Location = new System.Drawing.Point(416, 18);
            this.cbAll.Name = "cbAll";
            this.cbAll.Size = new System.Drawing.Size(72, 16);
            this.cbAll.TabIndex = 4;
            this.cbAll.Text = "导出所有";
            this.cbAll.UseVisualStyleBackColor = true;
            this.cbAll.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(260, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "导出批次";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "yyyy-MM-dd";
            this.dtpDate.Enabled = false;
            this.dtpDate.Location = new System.Drawing.Point(494, 14);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(132, 21);
            this.dtpDate.TabIndex = 6;
            // 
            // progress
            // 
            this.progress.Location = new System.Drawing.Point(136, 41);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(694, 23);
            this.progress.TabIndex = 7;
            // 
            // lblTip
            // 
            this.lblTip.AutoSize = true;
            this.lblTip.Location = new System.Drawing.Point(12, 41);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(29, 12);
            this.lblTip.TabIndex = 8;
            this.lblTip.Text = "提示";
            // 
            // btnInit
            // 
            this.btnInit.Location = new System.Drawing.Point(12, 11);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(67, 23);
            this.btnInit.TabIndex = 9;
            this.btnInit.Text = "初始化";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // ExportDataLJ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 371);
            this.Controls.Add(this.btnInit);
            this.Controls.Add(this.lblTip);
            this.Controls.Add(this.progress);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbAll);
            this.Controls.Add(this.numCount);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.btnCreate);
            this.Name = "ExportDataLJ";
            this.Text = "数据导出-SZLJ";
            this.Load += new System.EventHandler(this.ExportDataLJ_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.NumericUpDown numCount;
        private System.Windows.Forms.CheckBox cbAll;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.ProgressBar progress;
        private System.Windows.Forms.Label lblTip;
        private System.Windows.Forms.Button btnInit;
    }
}

