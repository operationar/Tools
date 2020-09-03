using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DriverClassesLib
{
    public class MitsubishiPLC
    {
        private readonly SerialPort _serialPort;
        private readonly ManualResetEvent dataReceiveEvent = new ManualResetEvent(false);
        public MitsubishiPLC(SerialPort sp)
        {
            _serialPort = sp;
        }
        public MitsubishiPLC(string com)
        {
            _serialPort = new SerialPort(com, 9600, Parity.Even, 7, StopBits.One);
        }
        public bool ConnectTest()
        {
            try
            {
                byte[] sendBuffer = new byte[1];
                byte[] receiveBuffer;
                //报头
                sendBuffer[0] = 0x05;

                dataReceiveEvent.Reset();
                _serialPort.DataReceived += SerialPort_DataReceived;
                if (!_serialPort.IsOpen) _serialPort.Open();
                _serialPort.Write(sendBuffer, 0, sendBuffer.Length);
                if (!dataReceiveEvent.WaitOne(1000)) return false;

                receiveBuffer = new byte[1];
                _serialPort.Read(receiveBuffer, 0, receiveBuffer.Length);
                _serialPort.Close();
                if (receiveBuffer[0] == 0x06) { return true; }
                else { return false; }
            }
            catch (Exception ex)
            {
                throw new MyException(999, ex.Message);
            }
        }
        public void ReadData(string device, int byteLength, out int[] valData)
        {
            valData = new int[byteLength];
            try
            {
                if (!_serialPort.IsOpen) _serialPort.Open();
                //02 30 dd dd dd dd LL LL 03 SS SS
                byte[] sendBuffer = new byte[11];
                byte[] receiveBuffer;
                //报头和指令
                sendBuffer[0] = 0x02;
                sendBuffer[1] = 0x30;
                //地址码
                if (!AddressAboutReadAndWriteParse(device, out byte[] valAddByte))
                {
                    throw new MyException((int)AlarmCode.addressError, MitsubishAlarmMessage(AlarmCode.addressError));
                }
                sendBuffer[2] = valAddByte[0];
                sendBuffer[3] = valAddByte[1];
                sendBuffer[4] = valAddByte[2];
                sendBuffer[5] = valAddByte[3];
                //数据长度
                byte[] lengthByte = Encoding.Default.GetBytes((byteLength * 2).ToString("X2"));
                sendBuffer[6] = lengthByte[0];
                sendBuffer[7] = lengthByte[1];

                sendBuffer[8] = 0x03;
                CalculateSumCheck(ref sendBuffer);
                _serialPort.DataReceived += SerialPort_DataReceived;
                _serialPort.Write(sendBuffer, 0, sendBuffer.Length);
                var task = Task<int>.Factory.StartNew(() => { return TaskDataReceive(_serialPort, byteLength); });
                if (!task.Wait(1000)) throw new MyException((int)AlarmCode.overTime, MitsubishAlarmMessage(AlarmCode.overTime));

                int dataLength = task.Result;
                receiveBuffer = new byte[dataLength];
                _serialPort.Read(receiveBuffer, 0, receiveBuffer.Length);

                byte[] valDataByte = new byte[dataLength - 4];
                for (int i = 0; i < receiveBuffer.Length - 4; i += 4)
                {
                    valDataByte[i] = receiveBuffer[i + 3];
                    valDataByte[i + 1] = receiveBuffer[i + 4];
                    valDataByte[i + 2] = receiveBuffer[i + 1];
                    valDataByte[i + 3] = receiveBuffer[i + 2];
                }

                for (int i = 0; i < valDataByte.Length; i += 4)
                {
                    char[] valChar = Encoding.Default.GetChars(valDataByte, i, 4);
                    valData[i / 4] = int.Parse(new string(valChar), System.Globalization.NumberStyles.HexNumber);
                }
                _serialPort.Close();
             }
            catch (Exception ex)
            {
                throw new MyException(999, ex.Message);
            }
        }
        public void WriteData(string device, int[] valDataInt)
        {
            try
            {
                //处理数据
                byte[] valDataByte = new byte[valDataInt.Length * 4];
                for (int i = 0; i < valDataInt.Length; i++)
                {
                    byte[] valDataChar = System.Text.ASCIIEncoding.Default.GetBytes(valDataInt[i].ToString("X4"));
                    valDataByte[i * 4 + 0] = valDataChar[2];
                    valDataByte[i * 4 + 1] = valDataChar[3];
                    valDataByte[i * 4 + 2] = valDataChar[0];
                    valDataByte[i * 4 + 3] = valDataChar[1];
                }

                if (!_serialPort.IsOpen) _serialPort.Open();
                //02 30 dd dd dd dd LL LL 03 SS SS
                byte[] sendBuffer = new byte[valDataByte.Length + 11];
                byte[] receiveBuffer;
                //报头和指令
                sendBuffer[0] = 0x02;
                sendBuffer[1] = 0x31;
                //地址码
                if (!AddressAboutReadAndWriteParse(device, out byte[] valAddByte))
                {
                    throw new MyException((int)AlarmCode.addressError, MitsubishAlarmMessage(AlarmCode.addressError));

                }
                sendBuffer[2] = valAddByte[0];
                sendBuffer[3] = valAddByte[1];
                sendBuffer[4] = valAddByte[2];
                sendBuffer[5] = valAddByte[3];
                //数据长度
                byte[] lengthByte = System.Text.ASCIIEncoding.Default.GetBytes((valDataByte.Length / 2).ToString("X2"));
                sendBuffer[6] = lengthByte[0];
                sendBuffer[7] = lengthByte[1];
                //写入数据
                for (int i = 0; i < valDataByte.Length; i++)
                {
                    sendBuffer[i + 8] = valDataByte[i];
                }
                sendBuffer[valDataByte.Length + 8] = 0x03;
                CalculateSumCheck(ref sendBuffer);
                dataReceiveEvent.Reset();
                _serialPort.DataReceived += SerialPort_DataReceived;
                _serialPort.Write(sendBuffer, 0, sendBuffer.Length);
                if (!dataReceiveEvent.WaitOne(1000)) throw new MyException((int)AlarmCode.overTime, MitsubishAlarmMessage(AlarmCode.overTime));

                int dataLength = _serialPort.BytesToRead;
                receiveBuffer = new byte[dataLength];
                _serialPort.Read(receiveBuffer, 0, receiveBuffer.Length);
                _serialPort.Close();
                if (receiveBuffer[0] != 0x06)
                {

                    throw new MyException((int)AlarmCode.messageError, MitsubishAlarmMessage(AlarmCode.messageError));
                }
            }
            catch (Exception ex)
            {
                throw new MyException(999, ex.Message);
            }
        }   
        private int TaskDataReceive(SerialPort serialPort, int byteLength)
        {
            int dataLength = _serialPort.BytesToRead;
            while (dataLength < byteLength * 4 + 4) dataLength = serialPort.BytesToRead;
            return _serialPort.BytesToRead;
        }
        public void ForceDevice(string device, bool valBool,out string alarmMessage)
        {
            alarmMessage = null;
            try
            {
                byte[] sendBuffer = new byte[9];
                byte[] receiveBuffer;
                //报头
                sendBuffer[0] = 0x02;
                if (valBool)
                {
                    sendBuffer[1] = 0x37;
                }
                else
                {
                    sendBuffer[1] = 0x38;
                }
                //地址
                if (!AddressAboutForceParse(device, out byte[] valAddByte))
                {
                    throw new MyException((int)AlarmCode.addressError, MitsubishAlarmMessage(AlarmCode.addressError));

                }
                sendBuffer[2] = valAddByte[2];
                sendBuffer[3] = valAddByte[3];
                sendBuffer[4] = valAddByte[0];
                sendBuffer[5] = valAddByte[1];
                sendBuffer[6] = 0x03;
                CalculateSumCheck(ref sendBuffer);
                dataReceiveEvent.Reset();
                _serialPort.DataReceived += SerialPort_DataReceived;
                if (!_serialPort.IsOpen) _serialPort.Open();
                _serialPort.Write(sendBuffer, 0, sendBuffer.Length);
                if (!dataReceiveEvent.WaitOne(1000))
                {
                    throw new MyException((int)AlarmCode.overTime, MitsubishAlarmMessage(AlarmCode.overTime));
                }
                int dataLength = _serialPort.BytesToRead;
                receiveBuffer = new byte[dataLength];
                _serialPort.Read(receiveBuffer, 0, receiveBuffer.Length);
                _serialPort.Close();
                if (receiveBuffer[0] != 0x06)
                {
                    throw new MyException((int)AlarmCode.messageError, MitsubishAlarmMessage(AlarmCode.messageError));
                }
            }
            catch (Exception ex)
            {
                throw new MyException(999, ex.Message);
            }
        }
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            dataReceiveEvent.Set();
        }
        private bool AddressAboutReadAndWriteParse(string device, out byte[] valAddByte)
        {
            string deviceFlag = device.Substring(0, 1);
            valAddByte = new byte[4] { 0, 0, 0, 0 };
            if (!int.TryParse(device.Substring(1, device.Length - 1), out int valAdd)) return false;

            switch (deviceFlag)
            {
                case "M":
                case "m":
                    valAdd /= 8;
                    valAdd += 0x0100;
                    break;
                case "D":
                case "d":
                    valAdd *= 2;
                    valAdd += 0X1000;
                    break;
                case "X":
                case "x":
                    valAdd /= 8;
                    valAdd += 0X0080;
                    break;
                case "Y":
                case "y":
                    valAdd /= 8;
                    valAdd += 0X00A0;
                    break;
                default:
                    return false;
            }
            valAddByte = System.Text.ASCIIEncoding.Default.GetBytes(valAdd.ToString("X4"));
            return true;
        }
        private bool AddressAboutForceParse(string device, out byte[] valAddByte)
        {
            string deviceFlag = device.Substring(0, 1);
            valAddByte = new byte[4] { 0, 0, 0, 0 };

            int valAdd;

            switch (deviceFlag)
            {
                case "M":
                case "m":
                    valAdd = Convert.ToInt32(device.Substring(1, device.Length - 1), 10);
                    valAdd += 0x0800;
                    break;
                case "Y":
                case "y":
                    valAdd = Convert.ToInt32(device.Substring(1, device.Length - 1), 8);
                    valAdd += 0X0500;
                    break;
                default:
                    return false;
            }
            valAddByte = System.Text.ASCIIEncoding.Default.GetBytes(valAdd.ToString("X4"));
            return true;
        }
        private void CalculateSumCheck(ref byte[] sendBuffer)
        {
            int SendbufferByteLength = sendBuffer.Length;
            int sumCheck = 0;
            for (int i = 1; i < SendbufferByteLength - 2; i++)
            {
                sumCheck += sendBuffer[i];
            }
            String sumcheckStr = sumCheck.ToString("X2");
            byte[] sumcheckByte = ASCIIEncoding.Default.GetBytes(sumcheckStr.Substring(sumcheckStr.Length - 2, 2));
            sendBuffer[SendbufferByteLength - 2] = sumcheckByte[0];
            sendBuffer[SendbufferByteLength - 1] = sumcheckByte[1];
        }
        enum AlarmCode
        {
              overTime,
              addressError,
              messageError
        
        }
        private string MitsubishAlarmMessage(AlarmCode code )
        {
            switch (code)
            {
                case AlarmCode.overTime:
                    return "通讯超时";
                case AlarmCode.addressError:
                    return "不支持的地址格式";
                case AlarmCode.messageError:
                    return "通信格式错误";
                default:
                    break;
            }
            return "未知错误";
        }
    }
}
