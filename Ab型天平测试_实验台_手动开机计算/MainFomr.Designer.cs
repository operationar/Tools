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
            this.balnceTare = new System.Windows.Forms.Button();
            this.DataShow = new System.Windows.Forms.ListBox();
            this.Zero = new System.Windows.Forms.Button();
            this.Limit = new System.Windows.Forms.Button();
            this.startCalculate = new System.Windows.Forms.Button();
            this.CycleStart = new System.Windows.Forms.Button();
            this.acquireDelay = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AcquireWeightContinue = new System.Windows.Forms.Button();
            this.AcquireWeightContinueEnd = new System.Windows.Forms.Button();
            this.CycleEnd = new System.Windows.Forms.Button();
            this.SaveDate = new System.Windows.Forms.Button();
            this.modeChoose = new System.Windows.Forms.Button();
            this.calChoose = new System.Windows.Forms.Button();
            this.oneTime = new System.Windows.Forms.Button();
            this.clear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // balanceAcquireWeight
            // 
            this.balanceAcquireWeight.Font = new System.Drawing.Font("楷体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.balanceAcquireWeight.Location = new System.Drawing.Point(728, 15);
            this.balanceAcquireWeight.Margin = new System.Windows.Forms.Padding(4);
            this.balanceAcquireWeight.Name = "balanceAcquireWeight";
            this.balanceAcquireWeight.Size = new System.Drawing.Size(216, 47);
            this.balanceAcquireWeight.TabIndex = 0;
            this.balanceAcquireWeight.Text = "读值";
            this.balanceAcquireWeight.UseVisualStyleBackColor = true;
            this.balanceAcquireWeight.Click += new System.EventHandler(this.BalanceAcquireWeight_Click);
            // 
            // balnceTare
            // 
            this.balnceTare.Font = new System.Drawing.Font("楷体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.balnceTare.Location = new System.Drawing.Point(728, 267);
            this.balnceTare.Margin = new System.Windows.Forms.Padding(4);
            this.balnceTare.Name = "balnceTare";
            this.balnceTare.Size = new System.Drawing.Size(216, 47);
            this.balnceTare.TabIndex = 3;
            this.balnceTare.Text = "去皮";
            this.balnceTare.UseVisualStyleBackColor = true;
            this.balnceTare.Click += new System.EventHandler(this.balnceTare_Click);
            // 
            // DataShow
            // 
            this.DataShow.FormattingEnabled = true;
            this.DataShow.ItemHeight = 15;
            this.DataShow.Location = new System.Drawing.Point(13, 15);
            this.DataShow.Margin = new System.Windows.Forms.Padding(4);
            this.DataShow.Name = "DataShow";
            this.DataShow.Size = new System.Drawing.Size(370, 634);
            this.DataShow.TabIndex = 5;
            // 
            // Zero
            // 
            this.Zero.Font = new System.Drawing.Font("楷体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Zero.Location = new System.Drawing.Point(728, 351);
            this.Zero.Margin = new System.Windows.Forms.Padding(4);
            this.Zero.Name = "Zero";
            this.Zero.Size = new System.Drawing.Size(216, 47);
            this.Zero.TabIndex = 6;
            this.Zero.Text = "零点";
            this.Zero.UseVisualStyleBackColor = true;
            this.Zero.Click += new System.EventHandler(this.BalanceZero_Click);
            // 
            // Limit
            // 
            this.Limit.Font = new System.Drawing.Font("楷体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Limit.Location = new System.Drawing.Point(728, 435);
            this.Limit.Margin = new System.Windows.Forms.Padding(4);
            this.Limit.Name = "Limit";
            this.Limit.Size = new System.Drawing.Size(216, 47);
            this.Limit.TabIndex = 7;
            this.Limit.Text = "20g";
            this.Limit.UseVisualStyleBackColor = true;
            this.Limit.Click += new System.EventHandler(this.BalanceLimit_Click);
            // 
            // startCalculate
            // 
            this.startCalculate.Font = new System.Drawing.Font("楷体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.startCalculate.Location = new System.Drawing.Point(728, 519);
            this.startCalculate.Margin = new System.Windows.Forms.Padding(4);
            this.startCalculate.Name = "startCalculate";
            this.startCalculate.Size = new System.Drawing.Size(216, 47);
            this.startCalculate.TabIndex = 8;
            this.startCalculate.Text = "开始计算";
            this.startCalculate.UseVisualStyleBackColor = true;
            this.startCalculate.Click += new System.EventHandler(this.startCalculate_Click);
            // 
            // CycleStart
            // 
            this.CycleStart.Font = new System.Drawing.Font("楷体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CycleStart.Location = new System.Drawing.Point(461, 183);
            this.CycleStart.Margin = new System.Windows.Forms.Padding(4);
            this.CycleStart.Name = "CycleStart";
            this.CycleStart.Size = new System.Drawing.Size(216, 47);
            this.CycleStart.TabIndex = 9;
            this.CycleStart.Text = "开始";
            this.CycleStart.UseVisualStyleBackColor = true;
            this.CycleStart.Click += new System.EventHandler(this.CycleStart_Click);
            // 
            // acquireDelay
            // 
            this.acquireDelay.Font = new System.Drawing.Font("楷体", 15.75F);
            this.acquireDelay.Location = new System.Drawing.Point(505, 54);
            this.acquireDelay.Margin = new System.Windows.Forms.Padding(4);
            this.acquireDelay.Name = "acquireDelay";
            this.acquireDelay.Size = new System.Drawing.Size(88, 37);
            this.acquireDelay.TabIndex = 10;
            this.acquireDelay.Text = "800";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("楷体", 15.75F);
            this.label1.Location = new System.Drawing.Point(488, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 27);
            this.label1.TabIndex = 11;
            this.label1.Text = "采集延时:";
            // 
            // AcquireWeightContinue
            // 
            this.AcquireWeightContinue.Font = new System.Drawing.Font("楷体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AcquireWeightContinue.Location = new System.Drawing.Point(728, 99);
            this.AcquireWeightContinue.Margin = new System.Windows.Forms.Padding(4);
            this.AcquireWeightContinue.Name = "AcquireWeightContinue";
            this.AcquireWeightContinue.Size = new System.Drawing.Size(216, 47);
            this.AcquireWeightContinue.TabIndex = 12;
            this.AcquireWeightContinue.Text = "连续读值";
            this.AcquireWeightContinue.UseVisualStyleBackColor = true;
            this.AcquireWeightContinue.Click += new System.EventHandler(this.AcquireWeightContinue_Click);
            // 
            // AcquireWeightContinueEnd
            // 
            this.AcquireWeightContinueEnd.Font = new System.Drawing.Font("楷体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.AcquireWeightContinueEnd.Location = new System.Drawing.Point(728, 183);
            this.AcquireWeightContinueEnd.Margin = new System.Windows.Forms.Padding(4);
            this.AcquireWeightContinueEnd.Name = "AcquireWeightContinueEnd";
            this.AcquireWeightContinueEnd.Size = new System.Drawing.Size(216, 47);
            this.AcquireWeightContinueEnd.TabIndex = 13;
            this.AcquireWeightContinueEnd.Text = "停止连续读值";
            this.AcquireWeightContinueEnd.UseVisualStyleBackColor = true;
            this.AcquireWeightContinueEnd.Click += new System.EventHandler(this.AcquireWeightContinueEnd_Click);
            // 
            // CycleEnd
            // 
            this.CycleEnd.Font = new System.Drawing.Font("楷体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CycleEnd.Location = new System.Drawing.Point(461, 267);
            this.CycleEnd.Margin = new System.Windows.Forms.Padding(4);
            this.CycleEnd.Name = "CycleEnd";
            this.CycleEnd.Size = new System.Drawing.Size(216, 47);
            this.CycleEnd.TabIndex = 14;
            this.CycleEnd.Text = "结束";
            this.CycleEnd.UseVisualStyleBackColor = true;
            this.CycleEnd.Click += new System.EventHandler(this.CycleEnd_Click);
            // 
            // SaveDate
            // 
            this.SaveDate.Font = new System.Drawing.Font("楷体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SaveDate.Location = new System.Drawing.Point(728, 603);
            this.SaveDate.Margin = new System.Windows.Forms.Padding(4);
            this.SaveDate.Name = "SaveDate";
            this.SaveDate.Size = new System.Drawing.Size(216, 47);
            this.SaveDate.TabIndex = 15;
            this.SaveDate.Text = "保存数据";
            this.SaveDate.UseVisualStyleBackColor = true;
            this.SaveDate.Click += new System.EventHandler(this.SaveDate_Click);
            // 
            // modeChoose
            // 
            this.modeChoose.Font = new System.Drawing.Font("楷体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.modeChoose.Location = new System.Drawing.Point(461, 351);
            this.modeChoose.Margin = new System.Windows.Forms.Padding(4);
            this.modeChoose.Name = "modeChoose";
            this.modeChoose.Size = new System.Drawing.Size(216, 47);
            this.modeChoose.TabIndex = 16;
            this.modeChoose.Text = "顶针开启";
            this.modeChoose.UseVisualStyleBackColor = true;
            this.modeChoose.Click += new System.EventHandler(this.modeChoose_Click);
            // 
            // calChoose
            // 
            this.calChoose.Font = new System.Drawing.Font("楷体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.calChoose.Location = new System.Drawing.Point(461, 435);
            this.calChoose.Margin = new System.Windows.Forms.Padding(4);
            this.calChoose.Name = "calChoose";
            this.calChoose.Size = new System.Drawing.Size(216, 47);
            this.calChoose.TabIndex = 17;
            this.calChoose.Text = "计算开启";
            this.calChoose.UseVisualStyleBackColor = true;
            this.calChoose.Click += new System.EventHandler(this.ChooseCal_Click);
            // 
            // oneTime
            // 
            this.oneTime.Font = new System.Drawing.Font("楷体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.oneTime.Location = new System.Drawing.Point(461, 519);
            this.oneTime.Margin = new System.Windows.Forms.Padding(4);
            this.oneTime.Name = "oneTime";
            this.oneTime.Size = new System.Drawing.Size(216, 47);
            this.oneTime.TabIndex = 18;
            this.oneTime.Text = "点动";
            this.oneTime.UseVisualStyleBackColor = true;
            this.oneTime.Click += new System.EventHandler(this.oneTime_Click);
            // 
            // clear
            // 
            this.clear.Font = new System.Drawing.Font("楷体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.clear.Location = new System.Drawing.Point(461, 603);
            this.clear.Margin = new System.Windows.Forms.Padding(4);
            this.clear.Name = "clear";
            this.clear.Size = new System.Drawing.Size(216, 47);
            this.clear.TabIndex = 19;
            this.clear.Text = "清空";
            this.clear.UseVisualStyleBackColor = true;
            this.clear.Click += new System.EventHandler(this.clear_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 663);
            this.Controls.Add(this.clear);
            this.Controls.Add(this.oneTime);
            this.Controls.Add(this.calChoose);
            this.Controls.Add(this.modeChoose);
            this.Controls.Add(this.SaveDate);
            this.Controls.Add(this.CycleEnd);
            this.Controls.Add(this.AcquireWeightContinueEnd);
            this.Controls.Add(this.AcquireWeightContinue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.acquireDelay);
            this.Controls.Add(this.CycleStart);
            this.Controls.Add(this.startCalculate);
            this.Controls.Add(this.Limit);
            this.Controls.Add(this.Zero);
            this.Controls.Add(this.DataShow);
            this.Controls.Add(this.balnceTare);
            this.Controls.Add(this.balanceAcquireWeight);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "天平测试_试验台_手动开启计算";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button balanceAcquireWeight;
        private System.Windows.Forms.Button balnceTare;
        private System.Windows.Forms.ListBox DataShow;
        private System.Windows.Forms.Button Zero;
        private System.Windows.Forms.Button Limit;
        private System.Windows.Forms.Button startCalculate;
        private System.Windows.Forms.Button CycleStart;
        private System.Windows.Forms.TextBox acquireDelay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AcquireWeightContinue;
        private System.Windows.Forms.Button AcquireWeightContinueEnd;
        private System.Windows.Forms.Button CycleEnd;
        private System.Windows.Forms.Button SaveDate;
        private System.Windows.Forms.Button modeChoose;
        private System.Windows.Forms.Button calChoose;
        private System.Windows.Forms.Button oneTime;
        private System.Windows.Forms.Button clear;
    }
}

