using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverClassesLib
{
    class MyException:Exception
    {
        public int ErrorCode { get; private set; }
        public MyException(int errorcode,string message):base(message)
        {
            this.ErrorCode = errorcode;
        }
    }
}
