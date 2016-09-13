using System;
using SharedLibrary;

namespace ClassLibrary
{
    public class Logger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
