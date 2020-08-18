using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DriverClassesLib
{
  public  class AbBalance
    {
        private readonly int port = 4196;
        private readonly int useCount;
        private readonly string startIp;
        private readonly bool[] isUse;

        private readonly Socket[] sockets;
        public AbBalance(string StartIp, bool[] IsUse)
        {
            startIp = StartIp;
            useCount = IsUse.Length;
            sockets = new Socket[useCount];
            isUse = new bool[useCount];
            isUse = IsUse;
        }
        public bool BalanceInit(out string messages)
        {
            bool initSuccess = true;
            messages = null;
            IPAddress[] iPAddresses = new IPAddress[useCount];
            IPEndPoint[] iPEndPoints = new IPEndPoint[useCount];
            string[] str = startIp.Split('.');
            int endIp = int.Parse(str[3]);
            for (int i = 0; i < useCount; i++)
            {
                try
                {
                    if (!isUse[i]) { messages += $"通道 {i + 1} 已屏蔽"; continue; }
                    iPAddresses[i] = IPAddress.Parse($"{str[0]}.{str[1]}.{str[2]}.{endIp + i}");
                    iPEndPoints[i] = new IPEndPoint(iPAddresses[i], port);
                    sockets[i] = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                    {
                        ReceiveTimeout = 500
                    };
                    IAsyncResult asyncResult = sockets[i].BeginConnect(iPEndPoints[i], null, sockets[i]);
                    asyncResult.AsyncWaitHandle.WaitOne(200, true);
                    if (!sockets[i].Connected)
                    {
                        messages += $"通道 {i + 1} 网口初始化失败,请检查网线及串口服务器\n";
                        initSuccess = false;
                    }
                    else
                    {
                        messages += $"通道 {i + 1} 网口初始化成功\n";
                    }
                }
                catch (Exception ex)
                {
                    messages += $"通道 {i + 1} 网口初始化出现意外错误,{ex.Message}\n";
                    initSuccess = false;
                }
            }
            return initSuccess;
        }
        public void BalanceTare(out bool[] IsSuccess, out string alarm)
        {
            alarm = "";
            IsSuccess = new bool[useCount];
            bool BalanceTareSuccess = true;
            try
            {
                int bytes = 0;
                for (int i = 0; i < useCount; i++)
                {
                    IsSuccess[i] = true;
                    if (!isUse[i]) continue;
                    byte[] TareMessage = { 0XA3, 0X12, 0X6D, 0X41, 0X63 };
                    sockets[i].Send(TareMessage, TareMessage.Length, 0);
                    byte[] recvBytes = new byte[1024];
                    do
                    {
                        try
                        {
                            bytes = sockets[i].Receive(recvBytes, recvBytes.Length, 0);
                        }
                        catch (SocketException ex)
                        {
                            BalanceTareSuccess = false;
                            IsSuccess[i] = false;
                            if (ex.ErrorCode == 10060)
                            {
                                alarm += ex.Message + " 通道 " + (i + 1).ToString() + " 通讯超时，请检查天平和通讯线!\n";
                            }
                            else if (ex.ErrorCode == 10054)
                            {
                                alarm += ex.Message + " 通道 " + (i + 1).ToString() + " 检查串口服务器该端口是否断开！\n";
                            }
                            else
                            {
                                alarm += ex.Message + " 通道 " + (i + 1).ToString() + "请检查！\n";
                            }
                            break;
                        }
                    } while (bytes < 5);
                    if (recvBytes[3] != 1) { BalanceTareSuccess = false; IsSuccess[i] = false; alarm += " 通道 " + (i + 1).ToString() + $"请检查！错误代码:{recvBytes[3]} \n"; }
                }
            }
            catch (Exception ex)
            {
                BalanceTareSuccess = false;
                alarm += ex.Message;
                //throw new Exception(ex.Message);
            }
            return;
        }
        public void BalanceAcquire(out double[] dataResult, out string alarm)
        {
            // BalanceCleaarBuffer();
            try
            {
                alarm = "";
                dataResult = new double[useCount];
                for (int i = 0; i < useCount; i++)
                {
                    if (!isUse[i]) continue;
                    byte[] acquireMessage = { 0XA3, 0X03, 0X7C, 0X41, 0X63 };
                    try
                    {
                        sockets[i].Send(acquireMessage, acquireMessage.Length, 0);
                    }
                    catch (Exception ex)
                    {
                        alarm += ex.Message + " 通道 " + (i + 1).ToString() + " 信息发送失败！\n";
                    }
                }
                for (int i = 0; i < useCount; i++)
                {
                    byte[] recvBytes = new byte[16];
                    int bytes = 0;
                    if (!isUse[i]) { dataResult[i] = 0; continue; }
                    DateTime StartDateTime = DateTime.Now;
                    while (true)
                    {
                        if (sockets[i].Available >= 8) break;
                        DateTime EndDateTime = DateTime.Now;
                        TimeSpan timeSpan = EndDateTime.Subtract(StartDateTime);
                        if (timeSpan.TotalSeconds > 10)
                        {
                            throw new Exception("socket.Available超时，请断电重试！");
                        }
                    }
                    do
                    {
                        try
                        {
                            bytes = sockets[i].Receive(recvBytes, recvBytes.Length, 0);
                        }
                        catch (SocketException ex)
                        {

                            if (ex.ErrorCode == 10060)
                            {
                                alarm += ex.Message + " 通道 " + (i + 1).ToString() + " 通讯超时，请检查天平和通讯线!\n";
                            }
                            else if (ex.ErrorCode == 10054)
                            {
                                alarm += ex.Message + " 通道 " + (i + 1).ToString() + " 检查串口服务器该端口是否断开！\n";
                            }
                            else
                            {
                                alarm += ex.Message + " 通道 " + (i + 1).ToString() + "请检查！";
                            }
                            break;
                        }
                    } while (bytes == 0);
                    dataResult[i] = ParseData(recvBytes) / 10.0;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public void BalanceCalibration_Zero(out bool[] IsSuccess, out string alarm)
        {
            bool BalanceCalibrate_zero_Success = true;
            alarm = "";
            IsSuccess = new bool[useCount];
            try
            {
                int bytes = 0;


                for (int i = 0; i < useCount; i++)
                {
                    IsSuccess[i] = true;
                    if (!isUse[i]) continue;
                    byte[] acquireMessage = { 0XA3, 0X29, 0X56, 0X41, 0X63 };
                    sockets[i].Send(acquireMessage, acquireMessage.Length, 0);
                    byte[] recvBytes = new byte[1024];
                    do
                    {
                        try
                        {
                            bytes = sockets[i].Receive(recvBytes, recvBytes.Length, 0);
                        }
                        catch (SocketException ex)
                        {
                            BalanceCalibrate_zero_Success = false;
                            if (ex.ErrorCode == 10060)
                            {
                                alarm += ex.Message + " 通道 " + (i + 1).ToString() + " '零点' 通讯超时，请检查天平和通讯线!\n";
                            }
                            else if (ex.ErrorCode == 10054)
                            {
                                alarm += ex.Message + " 通道 " + (i + 1).ToString() + " '零点' 检查串口服务器该端口是否断开！\n";
                            }
                            else
                            {
                                alarm += ex.Message + " 通道 " + (i + 1).ToString() + " '零点' 请检查！";
                            }
                            break;
                        }
                    } while (bytes == 0);
                    if (recvBytes[3] != '0') { BalanceCalibrate_zero_Success = false; IsSuccess[i] = false; alarm += " 通道 " + (i + 1).ToString() + $"请检查！错误代码:{recvBytes[3]} \n"; }
                }
            }
            catch (Exception ex)
            {
                BalanceCalibrate_zero_Success = false;
                alarm += ex.Message;

            }
            return;
        }
        public void BalanceCalibration_Limit(out bool[] IsSuccess, out string alarm)
        {
            bool BalanceCalibrate_Limit_Success = true;
            alarm = "";
            IsSuccess = new bool[useCount];
            try
            {
                int bytes = 0;

                for (int i = 0; i < useCount; i++)
                {
                    IsSuccess[i] = true;
                }
                for (int i = 0; i < useCount; i++)
                {
                    if (!isUse[i]) continue;
                    byte[] acquireMessage = { 0XA3, 0X28, 0X57, 0X41, 0X63 };
                    sockets[i].Send(acquireMessage, acquireMessage.Length, 0);
                    byte[] recvBytes = new byte[1024];
                    do
                    {
                        try
                        {
                            bytes = sockets[i].Receive(recvBytes, recvBytes.Length, 0);
                        }
                        catch (SocketException ex)
                        {
                            BalanceCalibrate_Limit_Success = false;
                            if (ex.ErrorCode == 10060)
                            {
                                alarm += ex.Message + " 通道 " + (i + 1).ToString() + " '极限' 通讯超时，请检查天平和通讯线!\n";
                            }
                            else if (ex.ErrorCode == 10054)
                            {
                                alarm += ex.Message + " 通道 " + (i + 1).ToString() + " '极限' 检查串口服务器该端口是否断开！\n";
                            }
                            else
                            {
                                alarm += ex.Message + " 通道 " + (i + 1).ToString() + " '极限' 请检查！";
                            }
                            break;
                        }
                    } while (bytes == 0);
                    if (recvBytes[3] != '0') { BalanceCalibrate_Limit_Success = false; IsSuccess[i] = false; alarm += " 通道 " + (i + 1).ToString() + $"请检查！错误代码:{recvBytes[3]} \n"; }
                }
            }
            catch (Exception ex)
            {

                BalanceCalibrate_Limit_Success = false;
                alarm += ex.Message;

            }
            return;
        }
        public void BalanceCleaarBuffer()
        {
            for (int i = 0; i < useCount; i++)
            {
                byte[] recvBytes = new byte[1024];
                try
                {
                    if (sockets[i].Available <= 0) continue;
                    int bytes = sockets[i].Receive(recvBytes, recvBytes.Length, 0);
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }
        public void DisConnect()
        {
            for (int i = 0; i < useCount; i++)
            {
                if (!isUse[i]) continue;
                sockets[i].Disconnect(true);
                sockets[i].Dispose();
            }
        }
        private int ParseData(byte[] recvBytes)
        {
            byte[] result = new byte[8];
            int data = 0;
            int offset = 0;
            try
            {
                for (int i = 0; i < recvBytes.Length; i++)
                {
                    result[offset] = recvBytes[i];
                    if (recvBytes[i] == 0xA0)
                    {
                        if (recvBytes[i + 1] == 0x00 || recvBytes[i + 1] == 0x03 || recvBytes[i + 1] == 0x06 || recvBytes[i + 1] == 0x09)
                        {
                            result[offset] = (byte)(recvBytes[i] + recvBytes[i + 1]);
                            i++;
                        }
                    }
                    offset++;
                    if (offset >= result.Length) break;

                }
                StringBuilder sb = new StringBuilder();
                sb.Append(result[4].ToString("X2"));
                sb.Append(result[5].ToString("X2"));
                sb.Append(result[6].ToString("X2"));
                data = int.Parse(sb.ToString(), System.Globalization.NumberStyles.HexNumber);
                if ((result[3] & 0x08) == 8)
                {
                    return 0 - data;
                }
                return data;
            }
            catch (Exception ex)
            {
                return 999999;
                //throw;
            }


            //return true;
        }
        public void BalanceStartOn(out bool[] IsSuccess,out string alarm)
        {
            alarm = "";
            IsSuccess = new bool[useCount];
            bool BalanceTareSuccess = true;
            try
            {
                int bytes = 0;
                for (int i = 0; i < useCount; i++)
                {
                    IsSuccess[i] = true;
                    if (!isUse[i]) continue;
                    byte[] TareMessage = { 0XA3, 0X02, 0X7D, 0X41, 0X63 };
                    sockets[i].Send(TareMessage, TareMessage.Length, 0);
                    byte[] recvBytes = new byte[1024];
                    do
                    {
                        try
                        {
                            bytes = sockets[i].Receive(recvBytes, recvBytes.Length, 0);
                        }
                        catch (SocketException ex)
                        {
                            BalanceTareSuccess = false;
                            IsSuccess[i] = false;
                            if (ex.ErrorCode == 10060)
                            {
                                alarm += ex.Message + " 通道 " + (i + 1).ToString() + " 通讯超时，请检查天平和通讯线!\n";
                            }
                            else if (ex.ErrorCode == 10054)
                            {
                                alarm += ex.Message + " 通道 " + (i + 1).ToString() + " 检查串口服务器该端口是否断开！\n";
                            }
                            else
                            {
                                alarm += ex.Message + " 通道 " + (i + 1).ToString() + "请检查！\n";
                            }
                            break;
                        }
                    } while (bytes < 4);
                    if (recvBytes[1] != 0X42) { BalanceTareSuccess = false; IsSuccess[i] = false; alarm += " 通道 " + (i + 1).ToString() + $"请检查！错误代码:{recvBytes[3]} \n"; }
                }
            }
            catch (Exception ex)
            {
                BalanceTareSuccess = false;
                alarm += ex.Message;
                //throw new Exception(ex.Message);
            }
            return;
        }
    }
}
