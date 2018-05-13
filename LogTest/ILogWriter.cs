using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LogTest
{
    public interface ILogWriter
    {
        void Write(LogLine text);

        void RenewLogWriter();

        void OpenLogWriter();

        void CloseLogWriter();
    }
}