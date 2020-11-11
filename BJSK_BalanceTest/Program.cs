using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriverClass;
using DriverClassesLib; 
namespace BJSK_BalanceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            CdBalance balance = new CdBalance();
            balance.IpStart = "192.168.1.161";
            bool[] isUse = new bool[10];
            for (int i = 0; i < 10; i++)
            {
                isUse[i] = true;
            }
            balance.IsUse = isUse;
            balance.BalanceInit(out bool[] isSuccess, out string alarm);
            long num = 0;
            while (true)
            {
                num++;
                balance.BalanceAcquire(out double[] data, out alarm);
                Console.Write("{0}\t",num);
                for (int i = 0; i < data.Length; i++)
                {
                    Console.Write(data[i].ToString("F1") + "\t");
                }
                Console.WriteLine("\n");
            }
        }
    }
}
