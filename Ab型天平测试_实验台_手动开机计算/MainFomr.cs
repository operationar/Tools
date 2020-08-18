using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DriverClassesLib;
namespace Ab型天平测试_实验台_手动开机计算
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        AbBalanceSP newBalance;
        private void balanceConnect_Click(object sender, EventArgs e)
        {
            newBalance = new AbBalanceSP("COM5");
        }

        private void balanceAcquireWeight_Click(object sender, EventArgs e)
        {
           bool result= newBalance.AcquireWeight(out double data);
            if (result)
            {
                dataShow.Text = data.ToString();
            }
            else
            {
                dataShow.Text = "error";
            }
        }

        private void balnceTare_Click(object sender, EventArgs e)
        {
            bool result= newBalance.Tare(out int data);
            if (result)
            {
                dataShow.Text = "OK";
            }
            else
            {
                dataShow.Text = data.ToString();
            }
        }
    }
}
