namespace Ab型天平测试_实验台_手动开机计算
{
    partial class InputBox
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
            this.inputText = new System.Windows.Forms.TextBox();
            this.informationShow = new System.Windows.Forms.Label();
            this.confirm = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // inputText
            // 
            this.inputText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputText.Font = new System.Drawing.Font("楷体", 15.75F);
            this.inputText.Location = new System.Drawing.Point(16, 71);
            this.inputText.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.inputText.Name = "inputText";
            this.inputText.Size = new System.Drawing.Size(734, 37);
            this.inputText.TabIndex = 0;
            this.inputText.Text = "100 100";
            // 
            // informationShow
            // 
            this.informationShow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.informationShow.AutoSize = true;
            this.informationShow.Font = new System.Drawing.Font("楷体", 15.75F);
            this.informationShow.Location = new System.Drawing.Point(254, 25);
            this.informationShow.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.informationShow.Name = "informationShow";
            this.informationShow.Size = new System.Drawing.Size(96, 27);
            this.informationShow.TabIndex = 1;
            this.informationShow.Text = "label1";
            // 
            // confirm
            // 
            this.confirm.Location = new System.Drawing.Point(155, 142);
            this.confirm.Name = "confirm";
            this.confirm.Size = new System.Drawing.Size(122, 38);
            this.confirm.TabIndex = 2;
            this.confirm.Text = "确定";
            this.confirm.UseVisualStyleBackColor = true;
            this.confirm.Click += new System.EventHandler(this.confirm_Click);
            // 
            // cancel
            // 
            this.cancel.Location = new System.Drawing.Point(16, 142);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(122, 38);
            this.cancel.TabIndex = 3;
            this.cancel.Text = "取消";
            this.cancel.UseVisualStyleBackColor = true;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // InputBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 26F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 192);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.confirm);
            this.Controls.Add(this.informationShow);
            this.Controls.Add(this.inputText);
            this.Font = new System.Drawing.Font("楷体", 15.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "InputBox";
            this.Text = "InputBox";
            this.Load += new System.EventHandler(this.InputBox_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox inputText;
        private System.Windows.Forms.Label informationShow;
        private System.Windows.Forms.Button confirm;
        private System.Windows.Forms.Button cancel;
    }
}