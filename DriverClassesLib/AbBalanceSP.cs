using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;

namespace DriverClassesLib
{
    public class AbBalanceSP
    {
        private readonly SerialPort sp;
        
        private readonly ManualResetEvent dataRecevieEvent = new ManualResetEvent(false);
        public AbBalanceSP(SerialPort _sp)
        {
            this.sp = _sp;
            sp.DataReceived += Sp_DataReceived;
        }
        public AbBalanceSP(string _com)
        {
            this.sp = new SerialPort(_com, 19200, Parity.None, 8, StopBits.One);
            sp.DataReceived += Sp_DataReceived;
        }
        private void Sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(20);
            dataRecevieEvent.Set();
        }
        public bool AcquireWeight(out double data)
        {
            data = 0; ;
            try
            {
                byte[] sendBuffer = new byte[] { 0XA3, 0X03, 0X7C, 0X41, 0X63 };
                dataRecevieEvent.Reset();
                if (!sp.IsOpen) sp.Open();
                sp.Write(sendBuffer, 0, sendBuffer.Length);
                if (!dataRecevieEvent.WaitOne(1000)) return false;
                byte[] receiveBuffer = new byte[sp.BytesToRead];
                sp.Read(receiveBuffer, 0, receiveBuffer.Length);
                foreach (var item in receiveBuffer)
                {
                    Console.Write(item.ToString("X2") + ' ');
                }
                Console.WriteLine();
                data = ParseData(receiveBuffer);
                return data != int.MaxValue;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Tare(out int data)
        {
            data = 0;
            try
            {
                byte[] sendBuffer = new byte[] { 0XA3, 0X12, 0X6D, 0X41, 0X63 };
                dataRecevieEvent.Reset();
                if (!sp.IsOpen) sp.Open();
                sp.Write(sendBuffer, 0, sendBuffer.Length);
                if (!dataRecevieEvent.WaitOne(1000)) return false;
                byte[] receiveBuffer = new byte[sp.BytesToRead];
                sp.Read(receiveBuffer, 0, receiveBuffer.Length);
                data = receiveBuffer[3];
                return data == 0x00;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Calibrate_Zero(out int data)
        {
            data = 0;
            try
            {
                byte[] sendBuffer = new byte[] { 0XA3, 0X29, 0X56, 0X41, 0X63 };
                dataRecevieEvent.Reset();
                if (!sp.IsOpen) sp.Open();
                sp.Write(sendBuffer, 0, sendBuffer.Length);
                if (!dataRecevieEvent.WaitOne(1000)) return false;
                byte[] receiveBuffer = new byte[sp.BytesToRead];
                sp.Read(receiveBuffer, 0, receiveBuffer.Length);
                data = receiveBuffer[3];
                return data == 0x00;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Calibrate_Limit(out int data)
        {
            data = 0;
            try
            {
                byte[] sendBuffer = new byte[] { 0XA3, 0X28, 0X57, 0X41, 0X63 };
                dataRecevieEvent.Reset();
                if (!sp.IsOpen) sp.Open();
                sp.Write(sendBuffer, 0, sendBuffer.Length);
                if (!dataRecevieEvent.WaitOne(1000)) return false;
                byte[] receiveBuffer = new byte[sp.BytesToRead];
                sp.Read(receiveBuffer, 0, receiveBuffer.Length);
                data = receiveBuffer[3];
                return data == 0x00;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private int ParseData(byte[] recvBytes)
        {
            byte[] result = new byte[8];
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
                int data = int.Parse(sb.ToString(), System.Globalization.NumberStyles.HexNumber);
                if ((result[3] & 0x08) == 8)
                {
                    return 0 - data;
                }
                return data;
            }
            catch (Exception ex)
            {
                return int.MaxValue;
            }
        }
    }
}
