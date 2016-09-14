using System;
using IoC.Web.Interfaces;

namespace IoC.Web.Models
{
    public class Logger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
