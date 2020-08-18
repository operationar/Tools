using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverClassesLib
{
   public class StreamWrite
    {
        public string Path { get; set; }
        private FileStream fileStream { get;   set; }
        public StreamWrite(string strPath)
        {
            this.Path = strPath;
            fileStream = new FileStream(Path, FileMode.Append, FileAccess.Write);
        }
        public void WriteData(string str)
        {
            StreamWriter sw = new StreamWriter(fileStream);

        }    
    }
}
