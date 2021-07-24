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
                    .RegisterObjectTransformation<StudentDataN>(data => new StudentDataN(data.Name  + "-N Modified", data.Id + "- N changed" ))
                );
            
            logger.Log(LogLevel.Info, "Student Data 1 {@data}", new StudentData("Jack", "10"));
            logger.Log(LogLevel.Info, "Student Data 2 {@data}", new StudentData("Jack", "10"));

            logger.Log(LogLevel.Info, "Student DataN 3 {@data}", new StudentDataN("Jack", "10"));
            logger.Log(LogLevel.Info, "Student DataN 4 {@data}", new StudentDataN("Jack", "10"));

            
            Console.ReadLine();
        }
        
        public record StudentData(string Name, string Id);

        public class StudentDataN
        {
            public StudentDataN(string Name, string Id)
            {
                this.Name = Name;
                this.Id = Id;
            }

            public string Name { get; set; }
            public string Id { get; set; }
        }
    }
}