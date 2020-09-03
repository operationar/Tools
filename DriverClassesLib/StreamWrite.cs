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
        public StreamWrite(string strPath)
        {
            this.Path = strPath;
        }
        public void WriteData(string str)
        {
            using (FileStream fileStream = new FileStream(Path, FileMode.Append, FileAccess.Write))
            {
                StreamWriter sw = new StreamWriter(fileStream);
                fileStream.Lock(0, fileStream.Length);
                sw.WriteLine(str);
                fileStream.Unlock(0, fileStream.Length);
                sw.Flush();
            }
        }
        public void WriteData(List<string> str)
        {
            using (FileStream fileStream = new FileStream(Path, FileMode.Append, FileAccess.Write))
            {
                foreach (var item in str)
                {
                    StreamWriter sw = new StreamWriter(fileStream);
                    fileStream.Lock(0, fileStream.Length);
                    sw.WriteLine(item);
                    fileStream.Unlock(0, fileStream.Length);
                    sw.Flush();
                }
            } 
        }
    }
}
