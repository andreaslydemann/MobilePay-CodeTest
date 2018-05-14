using System;
using LogTest;
using LogTest.Logs;
using LogTest.LogWriters;
using NUnit.Framework;
using NSubstitute;

namespace AsyncLogTest
{
    [TestFixture]
    public class AsyncLogTest
    {
        private ILogWriter _writer;
        private ILog _asyncLog;

        [SetUp]
        public void SetUp()
        {
            _writer = Substitute.For<ILogWriter>();
            _asyncLog = new AsyncLog(_writer);
        }

        [Test]
        public void AsyncLog_AddLogEntry_WriteCalled()
        {
            String text = "test";

            _asyncLog.AddLogEntry(text);

            _writer.Received().Write(Arg.Any<LogLine>());
        }
    }
}