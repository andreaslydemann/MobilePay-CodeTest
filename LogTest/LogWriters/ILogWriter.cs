namespace LogTest.LogWriters
{
    public interface ILogWriter
    {
        void Write(LogLine text);

        void RenewLogWriter();

        void OpenLogWriter();

        void CloseLogWriter();
    }
}