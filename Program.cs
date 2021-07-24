using System;
using NLog;

namespace NlogIssue
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            ILogger logger = LogManager.GetCurrentClassLogger();
            
            NLog.LogManager.Setup().SetupSerialization(d => 
                d.RegisterObjectTransformation<StudentData>(data => new StudentData(data.Name  + "-Modified", data.Id + "-changed" ))
                    
                );
            
            logger.Log(LogLevel.Info, "Student Data 1 {@data}", new StudentData("Jack", "10"));
            logger.Log(LogLevel.Info, "Student Data 2 {@data}", new StudentData("Jack3", "11"));

        }
        
        public record StudentData(string Name, string Id);

    }
}