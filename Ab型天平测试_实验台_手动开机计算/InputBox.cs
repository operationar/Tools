using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ab型天平测试_实验台_手动开机计算
{
    public partial class InputBox : Form
    {
        public InputBox(string str)
        {
            InitializeComponent();
            informationShow.Text = str;
        }
        public string  Message { get; set; }
        private void InputBox_Load(object sender, EventArgs e)
        {
            informationShow.Left = (this.Width - informationShow.Width) / 2;
            inputText.Left = (this.Width - inputText.Width) / 2;
             
        }

        private void confirm_Click(object sender, EventArgs e)
        {
            Message = inputText.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
