using DriverClassesLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using System.Threading.Tasks;

namespace Ab型天平测试_实验台_手动开机计算
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        // AbBalanceSP newBalance;
        CdBalanceSP newBalance;
        MitsubishiPLC mitsubishiPLC;
        bool isEnd;
        bool isNeedCal;
        private void balanceConnect_Click(object sender, EventArgs e)
        {

        }
        private int count, num;
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private void BalanceAcquireWeight_Click(object sender, EventArgs e)
        {
            bool result = newBalance.AcquireWeight(out double data);
            if (result)
            {
                WriteDataToList(data.ToString());
            }
            else
            {
                WriteDataToList("error");
            }
        }

        private void balnceTare_Click(object sender, EventArgs e)
        {
            bool result = newBalance.Tare(out int data);
            if (result)
            {
                WriteDataToList("tare OK");
            }
            else
            {
                WriteDataToList("tare " + data.ToString("X2"));
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            newBalance = new CdBalanceSP("COM3");
            mitsubishiPLC = new MitsubishiPLC("COM5");
            isNeedCal = true;

            isNeedCal = true;
            calChoose.BackColor = Color.Green;
            calChoose.Text = "开始计算";

            mitsubishiPLC.ForceDevice("M101", false, out string msg);
            modeChoose.BackColor = Color.Green;
            modeChoose.Text = "顶针开启";
            #region CD NEW ADD
            bool result = newBalance.Init(out msg);
            if (!result)
            {
                DataShow.Items.Add("天平初始化失败!");
            }
            #endregion

        }
        private void WriteDataToList(string str)
        {
            DataShow.Items.Add(DateTime.Now.ToString("hh:mm:ss.fff\t") + str);
            DataShow.SelectedIndex = DataShow.Items.Count - 1;
        }

        private void AcquireWeightContinue_Click(object sender, EventArgs e)
        {
            InputBox inputBox = new InputBox("请输入循环读取间隔和循环读取次数,中间以空格隔开");
            DialogResult dr = inputBox.ShowDialog();
            string str = inputBox.Message;
            inputBox.Dispose();
            if (dr == DialogResult.Cancel) { return; }
            string[] temp = str.Split(' ');

            timer.Interval = int.Parse(temp[0]);
            count = int.Parse(temp[1]);
            num = 0;
            timer.Tick += Timer_Tick;
            timer.Enabled = true;
        }

        private void CycleStart_Click(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            isEnd = false;
            Thread thread = new Thread(new ThreadStart(inspectStart));
            thread.Start();
        }

        private void inspectStart()

        {
            int delayTime = 50;
            mitsubishiPLC.ForceDevice("M100", true, out String msg);
            while (!isEnd)
            {
                
                /*Thread.Sleep(200);
                bool result;
                if (isNeedCal)
                {
                    result = newBalance.StartCal(out int calData);
                    if (!result)
                    { 
                        WriteDataToList("error start " + calData.ToString("X2"));
                    }
                }*/
                Thread.Sleep(int.Parse(acquireDelay.Text)- delayTime);
                mitsubishiPLC.ForceDevice("M100", true, out   msg);
                Thread.Sleep(delayTime);
                bool result = newBalance.AcquireWeight(out double data);
                if (result)
                {
                    WriteDataToList(data.ToString());
                }
                else
                {
                    WriteDataToList("error");
                }
                //Thread.Sleep(1);
            }
        }

        private void AcquireWeightContinueEnd_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
        }

        private void CycleEnd_Click(object sender, EventArgs e)
        {
            isEnd = true;
        }

        private void BalanceZero_Click(object sender, EventArgs e)
        {
            bool result = newBalance.Calibrate_Zero(out int data);
            if (result)
            {
                WriteDataToList("cal_zero OK");
            }
            else
            {
                WriteDataToList("cal_zero " + data.ToString("X2"));
            }
        }



        private void startCalculate_Click(object sender, EventArgs e)
        {
            bool result = newBalance.StartCal(out int data);
            if (result)
            {
                WriteDataToList("start_cal OK");
            }
            else
            {
                WriteDataToList("start_cal " + data.ToString("X2"));
            }
        }



        private void BalanceLimit_Click(object sender, EventArgs e)
        {
            bool result = newBalance.Calibrate_Limit(out int data);
            if (result)
            {
                WriteDataToList("cal_limit OK");
            }
            else
            {
                WriteDataToList("cal_limit " + data.ToString("X2"));
            }
        }

        private void SaveDate_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                var str1 = DataShow.Items.Cast<string>().ToList();
                string str = saveFileDialog.FileName;
                StreamWrite streamWrite = new StreamWrite(str);
                streamWrite.WriteData(str1);
                DataShow.Items.Clear();
            }
        }

        private void modeChoose_Click(object sender, EventArgs e)
        {
            if (modeChoose.BackColor == Color.Green)
            {
                mitsubishiPLC.ForceDevice("M101", true, out string msg);
                modeChoose.BackColor = Color.Red;
                modeChoose.Text = "顶针关闭";
            }
            else
            {
                mitsubishiPLC.ForceDevice("M101", false, out string msg);
                modeChoose.BackColor = Color.Green;
                modeChoose.Text = "顶针开启";
            }
        }

        private void ChooseCal_Click(object sender, EventArgs e)
        {

            if (isNeedCal)
            {
                isNeedCal = false;
                calChoose.BackColor = Color.Red;
                calChoose.Text = "关机计算";
            }
            else
            {
                isNeedCal = true;
                calChoose.BackColor = Color.Green;
                calChoose.Text = "开始计算";
            }
        }

        private void oneTime_Click(object sender, EventArgs e)
        {
            mitsubishiPLC.ForceDevice("M100", true, out String Msg);
        }

        private void clear_Click(object sender, EventArgs e)
        {
            DataShow.Items.Clear();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            num++;
            BalanceAcquireWeight_Click(sender, e);
            if (num >= count)
            {
                timer.Enabled = false;
            }
        }
    }
}
