using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DriverClassesLib
{
    public class CdBalanceSP : IBalanceSP
    {
        private readonly SerialPort sp;
        private readonly ManualResetEvent dataRecevieEvent = new ManualResetEvent(false);
        public CdBalanceSP(SerialPort _sp)
        {
            this.sp = _sp;
            sp.DataReceived += Sp_DataReceived;
        }

        private void Sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Thread.Sleep(1);
            dataRecevieEvent.Set();
        }

        public CdBalanceSP(string _com)
        {
            this.sp = new SerialPort(_com, 9600, Parity.Even, 8, StopBits.One);
            sp.DataReceived += Sp_DataReceived;
        }
        public bool Init(out string msg)
        {
            msg = "";
            //byte data;
            try
            {

                byte[] sendBuffer1 = Encoding.Default.GetBytes(";COF3;");
                dataRecevieEvent.Reset();
                if (!sp.IsOpen) sp.Open();
                sp.Write(sendBuffer1, 0, sendBuffer1.Length);
                if (!dataRecevieEvent.WaitOne(1000)) return false;
                byte[] receiveBuffer1 = new byte[sp.BytesToRead];
                sp.Read(receiveBuffer1, 0, receiveBuffer1.Length);
                if (receiveBuffer1[0] != '0') { msg = receiveBuffer1[0].ToString("X2"); return false; }

                byte[] sendBuffer2 = new byte[] { 0x3B, 0x44, 0x50, 0x57, 0x22, 0x41, 0x44, 0x43, 0x22, 0x3B };
                dataRecevieEvent.Reset();
                if (!sp.IsOpen) sp.Open();
                sp.Write(sendBuffer1, 0, sendBuffer1.Length);
                if (!dataRecevieEvent.WaitOne(1000)) return false;
                byte[] receiveBuffer2 = new byte[sp.BytesToRead];
                sp.Read(receiveBuffer1, 0, receiveBuffer1.Length);
                if (receiveBuffer1[0] != '0') { msg = receiveBuffer1[0].ToString("X2"); return false; }

                byte[] sendBuffer3 = new byte[] { 0x3B, 0x53, 0x50, 0x57, 0x22, 0x41, 0x44, 0x43, 0x22, 0x3B };
                dataRecevieEvent.Reset();
                if (!sp.IsOpen) sp.Open();
                sp.Write(sendBuffer1, 0, sendBuffer1.Length);
                if (!dataRecevieEvent.WaitOne(1000)) return false;
                byte[] receiveBuffer3 = new byte[sp.BytesToRead];
                sp.Read(receiveBuffer1, 0, receiveBuffer1.Length);
                if (receiveBuffer1[0] != '0') { msg = receiveBuffer1[0].ToString("X2"); return false; }

                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return false;
            }
        }
        public bool AcquireWeight(out double data)
        {
            data = 0; ;
            try
            {
                byte[] sendBuffer = new byte[] { 0x3B, 0x6D, 0x73, 0x76, 0x3F, 0x3B };
                dataRecevieEvent.Reset();
                if (!sp.IsOpen) sp.Open();
                sp.Write(sendBuffer, 0, sendBuffer.Length);
                if (!dataRecevieEvent.WaitOne(1000)) return false;
                byte[] receiveBuffer = new byte[sp.BytesToRead];
                sp.Read(receiveBuffer, 0, receiveBuffer.Length);
                data = int.Parse(Encoding.ASCII.GetString(receiveBuffer, 0, receiveBuffer.Length)) / 10.0;
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
                byte[] sendBuffer = new byte[] { 0x3B, 0x74, 0x61, 0x72, 0x3B };
                dataRecevieEvent.Reset();
                if (!sp.IsOpen) sp.Open();
                sp.Write(sendBuffer, 0, sendBuffer.Length);
                if (!dataRecevieEvent.WaitOne(1000)) return false;
                byte[] receiveBuffer = new byte[sp.BytesToRead];
                sp.Read(receiveBuffer, 0, receiveBuffer.Length);
                data = receiveBuffer[0];
                return data == '0';
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool StartCal(out int data)
        {
            data = 0;
           try
            {
                /* byte[] sendBuffer = new byte[] { 0XA3, 0X02, 0X7D, 0X41, 0X63 };
                dataRecevieEvent.Reset();
                if (!sp.IsOpen) sp.Open();
                sp.Write(sendBuffer, 0, sendBuffer.Length);
                if (!dataRecevieEvent.WaitOne(1000)) return false;
                byte[] receiveBuffer = new byte[sp.BytesToRead];
                sp.Read(receiveBuffer, 0, receiveBuffer.Length);
                data = receiveBuffer[1];*/
                return data == 0;
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
                byte[] sendBuffer = new byte[] { 0x3B, 0x6C, 0x64, 0x77, 0x3B };
                dataRecevieEvent.Reset();
                if (!sp.IsOpen) sp.Open();
                sp.Write(sendBuffer, 0, sendBuffer.Length);
                if (!dataRecevieEvent.WaitOne(1000)) return false;
                byte[] receiveBuffer = new byte[sp.BytesToRead];
                sp.Read(receiveBuffer, 0, receiveBuffer.Length);
                data = receiveBuffer[0];
                return data == '0';
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
                byte[] sendBuffer = new byte[] { 0x3B, 0x6C, 0x77, 0x74, 0x3B };
                dataRecevieEvent.Reset();
                if (!sp.IsOpen) sp.Open();
                sp.Write(sendBuffer, 0, sendBuffer.Length);
                if (!dataRecevieEvent.WaitOne(1000)) return false;
                byte[] receiveBuffer = new byte[sp.BytesToRead];
                sp.Read(receiveBuffer, 0, receiveBuffer.Length);
                data = receiveBuffer[3];
                return data == '0';
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
