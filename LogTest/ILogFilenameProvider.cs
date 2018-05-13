using System;

namespace LogTest
{
    public interface ILogFilenameProvider
    {
        String LogFilename { get; }
    }
}
