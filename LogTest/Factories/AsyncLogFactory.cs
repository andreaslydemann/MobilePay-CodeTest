using System;
using LogTest.Logs;
using LogTest.LogWriters;

namespace LogTest.Factories
{
    public class AsyncLogFactory : ILogFactory
    {
        public ILog CreateLog()
        {
            String directory = @"C:\LogTest\";

            return new AsyncLog(new LogWriter(directory, new LogFilenameProvider()));
        }
    }
}
