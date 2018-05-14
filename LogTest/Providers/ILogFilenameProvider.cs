using System;

namespace LogTest.Providers
{
    public interface ILogFilenameProvider
    {
        String LogFilename { get; }
    }
}