using System;
using System.Threading;
using LogTest.Factories;

namespace LogUsers
{
    class Program
    {
        static void Main(string[] args)
        {
            ILogFactory asyncLogFactory = new AsyncLogFactory();
            var logger = asyncLogFactory.CreateLog();

            for (int i = 0; i < 15; i++)
            {
                logger.AddLogEntry("Number with Flush: " + i.ToString());
                Thread.Sleep(50);
            }

            logger.StopWithFlush();

            var logger2 = asyncLogFactory.CreateLog();

            for (int i = 50; i > 0; i--)
            {
                logger2.AddLogEntry("Number with No flush: " + i.ToString());
                Thread.Sleep(20);
            }

            logger2.StopWithoutFlush();

            Console.ReadLine();
        }
    }
}
