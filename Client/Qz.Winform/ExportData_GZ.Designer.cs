namespace Qz.Winform
{
    partial class ExportDataGZ
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnInit = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.cbAll = new System.Windows.Forms.CheckBox();
            this.numCount = new System.Windows.Forms.NumericUpDown();
            this.progress = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTip = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.cbReInit = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.numCount)).BeginInit();
            this.SuspendLayout();
            // 
            // btnInit
            // 
            this.btnInit.AutoSize = true;
            this.btnInit.Location = new System.Drawing.Point(12, 12);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(75, 23);
            this.btnInit.TabIndex = 0;
            this.btnInit.Text = "初始化";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // btnExport
            // 
            this.btnExport.AutoSize = true;
            this.btnExport.Enabled = false;
            this.btnExport.Location = new System.Drawing.Point(605, 11);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "导 出";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // cbAll
            // 
            this.cbAll.AutoSize = true;
            this.cbAll.Enabled = false;
            this.cbAll.Location = new System.Drawing.Point(376, 16);
            this.cbAll.Name = "cbAll";
            this.cbAll.Size = new System.Drawing.Size(72, 16);
            this.cbAll.TabIndex = 1;
            this.cbAll.Text = "导出所有";
            this.cbAll.UseVisualStyleBackColor = true;
            this.cbAll.CheckedChanged += new System.EventHandler(this.cbAll_CheckedChanged);
            // 
            // numCount
            // 
            this.numCount.Enabled = false;
            this.numCount.Location = new System.Drawing.Point(254, 13);
            this.numCount.Name = "numCount";
            this.numCount.Size = new System.Drawing.Size(84, 21);
            this.numCount.TabIndex = 2;
            this.numCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // progress
            // 
            this.progress.Location = new System.Drawing.Point(163, 41);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(622, 23);
            this.progress.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(191, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "导出批次:";
            // 
            // lblTip
            // 
            this.lblTip.AutoSize = true;
            this.lblTip.Location = new System.Drawing.Point(12, 41);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(41, 12);
            this.lblTip.TabIndex = 4;
            this.lblTip.Text = "提示: ";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "yyyy-MM-dd";
            this.dtpDate.Enabled = false;
            this.dtpDate.Location = new System.Drawing.Point(454, 13);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(145, 21);
            this.dtpDate.TabIndex = 5;
            // 
            // cbReInit
            // 
            this.cbReInit.AutoSize = true;
            this.cbReInit.Location = new System.Drawing.Point(93, 15);
            this.cbReInit.Name = "cbReInit";
            this.cbReInit.Size = new System.Drawing.Size(72, 16);
            this.cbReInit.TabIndex = 6;
            this.cbReInit.Text = "重新生成";
            this.cbReInit.UseVisualStyleBackColor = true;
            // 
            // ExportDataGZ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 421);
            this.Controls.Add(this.cbReInit);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.lblTip);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progress);
            this.Controls.Add(this.numCount);
            this.Controls.Add(this.cbAll);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnInit);
            this.Name = "ExportDataGZ";
            this.Text = "数据导出-广州";
            ((System.ComponentModel.ISupportInitialize)(this.numCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInit;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.CheckBox cbAll;
        private System.Windows.Forms.NumericUpDown numCount;
        private System.Windows.Forms.ProgressBar progress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTip;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.CheckBox cbReInit;
    }
}