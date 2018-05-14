using System;
using System.Collections.Concurrent;
using System.Threading;
using LogTest.LogWriters;

namespace LogTest.Logs
{
    public class AsyncLog : ILog
    {
        private Thread _runThread;
        private ConcurrentQueue<LogLine> _lines;
        private ILogWriter _writer;
        private bool _quitWithFlush;
        private bool _exit;

        public AsyncLog(ILogWriter writer)
        {
            _writer = writer;
            _lines = new ConcurrentQueue<LogLine>();

            _exit = false;
            _quitWithFlush = false;

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

                    if (_quitWithFlush && _lines.IsEmpty)
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