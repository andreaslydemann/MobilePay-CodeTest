using System;
using LogTest.Logs;
using LogTest.LogWriters;
using LogTest.Providers;

namespace LogTest.Factories
{
    public class AsyncLogFactory : ILogFactory
    {
        public ILog CreateLog()
        {
            String directory = @"C:\LogTest\";

            return new AsyncLog(new LogWriter(directory, new LogFilenameProvider(), DateTime.Now));
        }
    }
}
