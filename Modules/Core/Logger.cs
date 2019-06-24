using System;
using System.IO;

namespace ConsoleApp1.Modules.Core
{
    static class Logger
    {
        /* 
         * Level contains log levels which are used in the Log() function and are used to determine if it needs to be logged
         */
        public enum Level
        {
            VERB = 0,
            INFO = 1,
            WARN = 2,
            EXEP = 3,
        }

        /* 
         * Log() accepts a string and a level and passes them to class members 
         */
        public static void Log(string message, Level level)
        {
            Logger.ToFile(message, level);
            Logger.ToConsole(message, level);
        }

        /* 
         * ToFile() checks if a logs folder exists and makes one if needed and
         * logs the given string into a file with todays date with given level 
         */
        private static void ToFile(string message, Level level)
        {
            string path = $"logs/{DateTime.Today.ToString(@"dd-MM-yyyy")} -- {level.ToString()}.log";

            if (!Directory.Exists("logs"))
                Directory.CreateDirectory("logs");

            if (!File.Exists(path))
            {
                using (StreamWriter file = File.CreateText(path))
                {
                    file.WriteLine($"{DateTime.Now} -- {level.ToString()} LOG START");
                }
            }

            using (StreamWriter log = File.AppendText(path))
            {
                log.WriteLine($"{DateTime.Now} - {message}");
            }
        }

        /* 
         * ToConsole() logs the given string with level to the Console with
         * the current timestamp 
         */
        private static void ToConsole(string message, Level level)
        {
            Console.WriteLine($"{level.ToString()} {DateTime.Now} - {message}");
        }
    }
}
