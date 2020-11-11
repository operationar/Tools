using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DriverClass
{

    public class Logger
    {

        private readonly object LogLock = new object();
        private readonly string basePath;
        public Logger(string path)
        {
            basePath = "D:\\CMC\\Systemlog\\" + path;
        }

        /// <summary>
        /// Write log to log file
        /// </summary>
        /// <param name="logContent">Log content</param>
        /// <param name="logType">Log type</param>
        public void WriteLog(string text)
        {
            try
            {
                string fileName = DateTime.Now.ToString("yyyyMMdd");

                string LogPath = basePath + "\\" + fileName + ".txt";
                lock (LogLock)
                {
                    File.AppendAllText(LogPath, text);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Write exception to log file
        /// </summary>
        /// <param name="exception">Exception</param>
        public void WriteException(Exception exception, string specialText = null)
        {
            if (exception != null)
            {
                Type exceptionType = exception.GetType();
                string text = string.Empty;
                text = Environment.NewLine + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + Environment.NewLine;
                if (!string.IsNullOrEmpty(specialText))
                {
                    text += specialText + Environment.NewLine;
                }
                text += "Exception: " + exceptionType.Name + Environment.NewLine;
                text += "Message: " + exception.Message + Environment.NewLine;
                text += "Source: " + exception.Source + Environment.NewLine;
                text += "StackTrace: " + Environment.NewLine + exception.StackTrace;
                WriteLog(text);
            }
        }
    }
}
