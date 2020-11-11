using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace DriverClass
{
    public class CdBalance
    {
        private readonly int port = 4196;
        private string ipStart;
        private readonly Socket[] sockets = new Socket[10];
        private readonly Logger logger = new Logger("Balance");

        public string IpStart
        {
            get { return ipStart; }
            set { ipStart = value; }
        }
        private readonly int useCounts = 10;


        public bool[] IsUse = new bool[10];

        public void BalanceInit(out bool[] IsSuccess, out string alarm)
        {
            IsSuccess = new bool[useCounts];
            for (int i = 0; i < useCounts; i++)
            {
                IsSuccess[i] = true;
            }
            alarm = "";
            IPAddress[] iPAddresses = new IPAddress[useCounts];
            IPEndPoint[] iPEndPoints = new IPEndPoint[useCounts];
            string[] str = ipStart.Split('.');
            int IpParagraph = int.Parse(str[3]);
            try
            {
                for (int i = 0; i < useCounts; i++)
                {
                    if (!IsUse[i]) continue;
                    iPAddresses[i] = IPAddress.Parse($"{str[0]}.{str[1]}.{str[2]}.{IpParagraph + i}");
                    iPEndPoints[i] = new IPEndPoint(iPAddresses[i], port);
                    sockets[i] = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
                    {
                        ReceiveTimeout = 500
                    };
                    IAsyncResult asyncResult = sockets[i].BeginConnect(iPEndPoints[i], null, sockets[i]);
                    asyncResult.AsyncWaitHandle.WaitOne(200, true);
                    if (!sockets[i].Connected)
                    {
                        IsSuccess[i] = false;
                        alarm += "通道 " + (i + 1).ToString() + " 链接失败，请检查网线与串口服务器端口!\n";
                        sockets[i].Close();
                    }
                }
                if (!string.IsNullOrEmpty(alarm)) return;

                for (int i = 0; i < useCounts; i++)
                {
                    if (!IsUse[i]) continue;
                    byte[] bs = Encoding.ASCII.GetBytes(";COF3;");
                    try
                    {
                        sockets[i].Send(bs, bs.Length, 0);
                    }
                    catch (Exception ex)
                    {
                        IsSuccess[i] = false;
                        logger.WriteException(ex);
                        alarm += ex.Message + " 通道 " + (i + 1).ToString() + " 信息发送失败！\n";
                    }

                    byte[] recvBytes = new byte[1024];
                    int bytes = 0;
                    do
                    {
                        try
                        {
                            bytes = sockets[i].Receive(recvBytes, recvBytes.Length, 0);
                        }
                        catch (SocketException ex)
                        {
                            IsSuccess[i] = false;
                            logger.WriteException(ex, (i + 1).ToString() + "error code:" + ex.ErrorCode.ToString());
                            if (ex.ErrorCode == 10060)
                            {
                                alarm += ex.Message + " 通道 " + (i + 1).ToString() + " 通讯超时，请检查称重模块和通讯线!\n";
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
                    if (recvBytes[0] != '0') IsSuccess[i] = false;
                }
                if (!string.IsNullOrEmpty(alarm)) return;
                for (int i = 0; i < useCounts; i++)
                {
                    if (!IsUse[i]) continue;
                    byte[] Unlock1 = { 0x3B, 0x44, 0x50, 0x57, 0x22, 0x41, 0x44, 0x43, 0x22, 0x3B, };

                    try
                    {
                        sockets[i].Send(Unlock1, Unlock1.Length, 0);
                    }
                    catch (Exception ex)
                    {
                        IsSuccess[i] = false;
                        logger.WriteException(ex);
                        alarm += ex.Message + " 通道 " + (i + 1).ToString() + " 信息发送失败！\n";
                    }


                    byte[] recvBytes = new byte[1024];
                    int bytes = 0;
                    do
                    {
                        try
                        {
                            bytes = sockets[i].Receive(recvBytes, recvBytes.Length, 0);
                        }
                        catch (SocketException ex)
                        {
                            IsSuccess[i] = false;
                            logger.WriteException(ex, (i + 1).ToString() + "error code:" + ex.ErrorCode.ToString());
                            if (ex.ErrorCode == 10060)
                            {
                                alarm += ex.Message + " 通道 " + (i + 1).ToString() + " 通讯超时，请检查称重模块和通讯线!\n";
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
                    if (recvBytes[0] != '0') IsSuccess[i] = false;
                }
                if (!string.IsNullOrEmpty(alarm)) return;
                for (int i = 0; i < useCounts; i++)
                {
                    if (!IsUse[i]) continue;
                    byte[] Unlock2 = { 0x3B, 0x53, 0x50, 0x57, 0x22, 0x41, 0x44, 0x43, 0x22, 0x3B };


                    try
                    {
                        sockets[i].Send(Unlock2, Unlock2.Length, 0);
                    }
                    catch (Exception ex)
                    {
                        IsSuccess[i] = false;
                        logger.WriteException(ex);
                        alarm += ex.Message + " 通道 " + (i + 1).ToString() + " 信息发送失败！\n";
                    }

                    byte[] recvBytes = new byte[1024];
                    int bytes = 0;
                    do
                    {
                        try
                        {
                            bytes = sockets[i].Receive(recvBytes, recvBytes.Length, 0);
                        }
                        catch (SocketException ex)
                        {
                            IsSuccess[i] = false;
                            logger.WriteException(ex, (i + 1).ToString() + "error code:" + ex.ErrorCode.ToString());
                            if (ex.ErrorCode == 10060)
                            {
                                alarm += ex.Message + " 通道 " + (i + 1).ToString() + " 通讯超时，请检查称重模块和通讯线!\n";
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
                    if (recvBytes[0] != '0') IsSuccess[i] = false;
                }
            }
            catch (Exception ex)
            {
                alarm += ex.Message;
                logger.WriteException(ex);
                throw new Exception(ex.Message);
            }
        }
        public void BalanceTare(out bool[] IsSuccess, out string alarm)
        {
            try
            {
                int bytes = 0;
                alarm = "";
                IsSuccess = new bool[useCounts];
                for (int i = 0; i < useCounts; i++)
                {
                    IsSuccess[i] = true;
                }

                for (int i = 0; i < useCounts; i++)
                {

                    if (!IsUse[i]) continue;
                    byte[] acquireMessage = { 0x3B, 0x74, 0x61, 0x72, 0x3B };
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
                            logger.WriteException(ex, (i + 1).ToString() + "error code:" + ex.ErrorCode.ToString());
                            if (ex.ErrorCode == 10060)
                            {
                                alarm += ex.Message + " 通道 " + (i + 1).ToString() + " 通讯超时，请检查称重模块和通讯线!\n";
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
                    if (recvBytes[0] != '0') IsSuccess[i] = false;
                }
            }
            catch (Exception ex)
            {
                logger.WriteException(ex);
                throw new Exception(ex.Message);
            }

        }
        public void BalanceAcquire(out double[] dataResule, out string alarm)
        {
            // BalanceCleaarBuffer();
            try
            {
                alarm = "";
                dataResule = new double[useCounts];

                for (int i = 0; i < 10; i++)
                {
                    if (!IsUse[i]) continue;
                    byte[] acquireMessage = { 0x3B, 0x6D, 0x73, 0x76, 0x3F, 0x3B };

                    try
                    {
                        sockets[i].Send(acquireMessage, acquireMessage.Length, 0);
                    }
                    catch (Exception ex)
                    {
                        logger.WriteException(ex);
                        alarm += ex.Message + " 通道 " + (i + 1).ToString() + " 信息发送失败！\n";
                    }

                }

                for (int i = 0; i < 10; i++)
                {
                    byte[] recvBytes = new byte[10];
                    int bytes = 0;
                    if (!IsUse[i]) { dataResule[i] = 0; continue; }
                    DateTime StartDateTime = DateTime.Now;

                    while (true)
                    {
                        if (sockets[i].Available == 10) break;
                        DateTime EndDateTime = DateTime.Now;
                        TimeSpan timeSpan = EndDateTime.Subtract(StartDateTime);
                        if (timeSpan.TotalSeconds > 2)
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
                            logger.WriteException(ex, (i + 1).ToString() + "error code:" + ex.ErrorCode.ToString());
                            if (ex.ErrorCode == 10060)
                            {
                                alarm += ex.Message + " 通道 " + (i + 1).ToString() + " 通讯超时，请检查称重模块和通讯线!\n";
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
                    dataResule[i] = int.Parse(Encoding.ASCII.GetString(recvBytes, 0, bytes)) / 10.0;
                }
            }
            catch (Exception ex)
            {
                logger.WriteException(ex);
                throw new Exception(ex.Message);
            }
        }
        public void BalanceCalibration_Zero(out bool[] IsSuccess, out string alarm)
        {
            try
            {
                int bytes = 0;
                alarm = "";
                IsSuccess = new bool[useCounts];
                for (int i = 0; i < useCounts; i++)
                {
                    IsSuccess[i] = true;
                }

                for (int i = 0; i < useCounts; i++)
                {
                    if (!IsUse[i]) continue;
                    byte[] acquireMessage = { 0x3B, 0x6C, 0x64, 0x77, 0x3B };
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
                            logger.WriteException(ex, (i + 1).ToString() + "error code:" + ex.ErrorCode.ToString());
                            if (ex.ErrorCode == 10060)
                            {
                                alarm += ex.Message + " 通道 " + (i + 1).ToString() + " '零点' 通讯超时，请检查称重模块和通讯线!\n";
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
                    if (recvBytes[0] != '0') IsSuccess[i] = false;
                }
            }
            catch (Exception ex)
            {
                logger.WriteException(ex);
                throw new Exception(ex.Message);
            }
        }
        public void BalanceCalibration_Limit(out bool[] IsSuccess, out string alarm)
        {
            try
            {
                int bytes = 0;
                alarm = "";
                IsSuccess = new bool[useCounts];
                for (int i = 0; i < useCounts; i++)
                {
                    IsSuccess[i] = true;
                }
                for (int i = 0; i < useCounts; i++)
                {
                    if (!IsUse[i]) continue;
                    byte[] acquireMessage = { 0x3B, 0x6C, 0x77, 0x74, 0x3B };
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
                            logger.WriteException(ex, (i + 1).ToString() + "error code:" + ex.ErrorCode.ToString());
                            if (ex.ErrorCode == 10060)
                            {
                                alarm += ex.Message + " 通道 " + (i + 1).ToString() + " '极限' 通讯超时，请检查称重模块和通讯线!\n";
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
                    if (recvBytes[0] != '0') IsSuccess[i] = false;
                }
            }
            catch (Exception ex)
            {
                logger.WriteException(ex);
                throw new Exception(ex.Message);
            }
        }
        public void BalanceCleaarBuffer()
        {
            for (int i = 0; i < useCounts; i++)
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
            for (int i = 0; i < useCounts; i++)
            {
                if (!IsUse[i]) continue;
                sockets[i].Disconnect(true);
                sockets[i].Dispose();
            }
        }
    }
}
