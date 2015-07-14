namespace Qz.Winform
{
    partial class Main
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
            this.btnSZLJ = new System.Windows.Forms.Button();
            this.btnGZ = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSZLJ
            // 
            this.btnSZLJ.Location = new System.Drawing.Point(31, 66);
            this.btnSZLJ.Name = "btnSZLJ";
            this.btnSZLJ.Size = new System.Drawing.Size(133, 72);
            this.btnSZLJ.TabIndex = 0;
            this.btnSZLJ.Text = "深圳链家";
            this.btnSZLJ.UseVisualStyleBackColor = true;
            this.btnSZLJ.Click += new System.EventHandler(this.btnSZLJ_Click);
            // 
            // btnGZ
            // 
            this.btnGZ.Location = new System.Drawing.Point(220, 66);
            this.btnGZ.Name = "btnGZ";
            this.btnGZ.Size = new System.Drawing.Size(133, 72);
            this.btnGZ.TabIndex = 0;
            this.btnGZ.Text = "广 州";
            this.btnGZ.UseVisualStyleBackColor = true;
            this.btnGZ.Click += new System.EventHandler(this.btnGZ_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 223);
            this.Controls.Add(this.btnGZ);
            this.Controls.Add(this.btnSZLJ);
            this.Name = "Main";
            this.Text = "Main";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSZLJ;
        private System.Windows.Forms.Button btnGZ;
    }
}