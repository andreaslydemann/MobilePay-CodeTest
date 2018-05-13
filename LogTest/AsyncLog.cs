namespace LogTest
{
    using System;
    using System.Collections.Concurrent;
    using System.IO;
    using System.Threading;

    public class AsyncLog : ILog
    {
        private String _directory = @"C:\LogTest\";
        private Thread _runThread;
        private ConcurrentQueue<LogLine> _lines;
        private ILogWriter _writer;
        private ILogFilenameProvider _fnProvider;
        private bool _quitWithFlush = false;
        private bool _exit = false;

        public AsyncLog()
        {
            if (!Directory.Exists(_directory))
                Directory.CreateDirectory(_directory);
            
            _lines = new ConcurrentQueue<LogLine>();
            _fnProvider = new LogFilenameProvider();
            _writer = new LogWriter(_directory, _fnProvider);
            
            _runThread = new Thread(MainLoop);
            _runThread.Start();
        }

        private void MainLoop()
        {
            while (!_exit)
            {
                if (!_lines.IsEmpty)
                {
                    LogLine currentLine;

                    if (!_lines.TryDequeue(out currentLine))
                    {
                        if (_quitWithFlush)
                            _exit = true;
                        
                        continue;
                    }
                    
                    if (!_exit || _quitWithFlush)
                    {
                        lock (_writer)
                        {
                            _writer.Write(currentLine);
                        }
                    }

                    if (_quitWithFlush == true && _lines.IsEmpty)
                    {
                        lock (_writer)
                        {
                            if (_writer != null)
                                _writer.CloseLogWriter();
                        }

                        _exit = true;
                    }
                }
            }
        }

        public void StopWithoutFlush()
        {
            _exit = true;

            lock (_writer)
            {
                if (_writer != null)
                    _writer.CloseLogWriter();
            }
        }

        public void StopWithFlush()
        {
            _quitWithFlush = true;
        }

        public void AddLogEntry(String text)
        {
            _lines.Enqueue(new LogLine() { Text = text, Timestamp = DateTime.Now });
        }
    }
}