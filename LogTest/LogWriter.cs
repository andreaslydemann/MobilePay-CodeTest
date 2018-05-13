﻿using System;
using System.Text;
using System.IO;

namespace LogTest
{
    public class LogWriter : ILogWriter
    {
        private StreamWriter _writer;
        private ILogFilenameProvider _fnProvider;
        private DateTime _curDate = DateTime.Now;
        private String _directory;

        public LogWriter(String directory, ILogFilenameProvider fnProvider)
        {
            _fnProvider = fnProvider;
            _directory = directory;
            _curDate = DateTime.Now;

            OpenLogWriter();
        }

        public void Write(LogLine logLine)
        {
            if (DateTime.Today > _curDate.Date)
                RenewLogWriter();

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(logLine.Timestamp.ToString("yyyy-MM-dd HH:mm:ss:fff"));
            stringBuilder.Append("\t");
            stringBuilder.Append(logLine.LineText());
            stringBuilder.Append("\t");
            stringBuilder.Append(Environment.NewLine);
            
            _writer.Write(stringBuilder.ToString());
        }

        public void RenewLogWriter()
        {
            CloseLogWriter();
            OpenLogWriter();

            _curDate = DateTime.Now;
        }

        public void OpenLogWriter()
        {
            if (!Directory.Exists(_directory))
                Directory.CreateDirectory(_directory);

            _writer = File.AppendText(_directory + _fnProvider.LogFilename);
            _writer.AutoFlush = true;

            _writer.Write("Timestamp".PadRight(25, ' ') 
                + "\t" + "Data".PadRight(15, ' ') + "\t" + Environment.NewLine);
        }

        public void CloseLogWriter()
        {
            _writer.Close();
        }
    }
}