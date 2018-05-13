using System;

namespace LogTest
{
    public class LogFilenameProvider : ILogFilenameProvider
    {
        public String LogFilename
        {
            get {
                return "Log" + DateTime.Now.ToString("yyyyMMdd HHmmss fff") + ".log";
            }
        }
    }
}
