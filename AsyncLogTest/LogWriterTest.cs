using System;
using NUnit.Framework;
using NSubstitute;
using LogTest.LogWriters;
using LogTest.Providers;

namespace LogComponentTests
{
    class LogWriterTest
    {
        private ILogWriter _writer;
        private ILogFilenameProvider _fnProvider;

        [SetUp]
        public void SetUp()
        {
            _fnProvider = Substitute.For<ILogFilenameProvider>();
        }

        [Test]
        public void LogWriter_AddLogEntry_WriteCalled()
        {
            String testDirectory = "testDirectory/";

            DateTime dt = new DateTime(1990, 1, 1, 1, 1, 1);

            _writer = new LogWriter(testDirectory, _fnProvider, dt);
            _writer.Received(1).RenewLogWriter();
        }
    }
}