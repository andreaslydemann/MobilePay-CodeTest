using System;

namespace LogTest.Providers
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
