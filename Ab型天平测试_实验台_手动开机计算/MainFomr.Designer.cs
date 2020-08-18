namespace Ab型天平测试_实验台_手动开机计算
{
    partial class MainForm
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.balanceAcquireWeight = new System.Windows.Forms.Button();
            this.dataShow = new System.Windows.Forms.TextBox();
            this.balanceConnect = new System.Windows.Forms.Button();
            this.balnceTare = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // balanceAcquireWeight
            // 
            this.balanceAcquireWeight.Font = new System.Drawing.Font("楷体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.balanceAcquireWeight.Location = new System.Drawing.Point(479, 103);
            this.balanceAcquireWeight.Name = "balanceAcquireWeight";
            this.balanceAcquireWeight.Size = new System.Drawing.Size(160, 56);
            this.balanceAcquireWeight.TabIndex = 0;
            this.balanceAcquireWeight.Text = "读值";
            this.balanceAcquireWeight.UseVisualStyleBackColor = true;
            this.balanceAcquireWeight.Click += new System.EventHandler(this.balanceAcquireWeight_Click);
            // 
            // dataShow
            // 
            this.dataShow.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataShow.Location = new System.Drawing.Point(173, 103);
            this.dataShow.Name = "dataShow";
            this.dataShow.Size = new System.Drawing.Size(100, 30);
            this.dataShow.TabIndex = 1;
            // 
            // balanceConnect
            // 
            this.balanceConnect.Font = new System.Drawing.Font("楷体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.balanceConnect.Location = new System.Drawing.Point(479, 42);
            this.balanceConnect.Name = "balanceConnect";
            this.balanceConnect.Size = new System.Drawing.Size(160, 56);
            this.balanceConnect.TabIndex = 2;
            this.balanceConnect.Text = "初始化天平";
            this.balanceConnect.UseVisualStyleBackColor = true;
            this.balanceConnect.Click += new System.EventHandler(this.balanceConnect_Click);
            // 
            // balnceTare
            // 
            this.balnceTare.Font = new System.Drawing.Font("楷体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.balnceTare.Location = new System.Drawing.Point(479, 169);
            this.balnceTare.Name = "balnceTare";
            this.balnceTare.Size = new System.Drawing.Size(160, 56);
            this.balnceTare.TabIndex = 3;
            this.balnceTare.Text = "去皮";
            this.balnceTare.UseVisualStyleBackColor = true;
            this.balnceTare.Click += new System.EventHandler(this.balnceTare_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.balnceTare);
            this.Controls.Add(this.balanceConnect);
            this.Controls.Add(this.dataShow);
            this.Controls.Add(this.balanceAcquireWeight);
            this.Name = "MainForm";
            this.Text = "AB天平测试_试验台_手动开启计算";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button balanceAcquireWeight;
        private System.Windows.Forms.TextBox dataShow;
        private System.Windows.Forms.Button balanceConnect;
        private System.Windows.Forms.Button balnceTare;
    }
}

