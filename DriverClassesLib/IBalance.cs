using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverClassesLib
{
    interface IBalanceSP
    {
        bool AcquireWeight(out double data);
        bool Tare(out int data);
        bool StartCal(out int data);
        bool Calibrate_Zero(out int data);
        bool Calibrate_Limit(out int data);
     }
}
