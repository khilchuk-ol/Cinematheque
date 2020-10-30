using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinematheque.Data.Utils
{
    public class LogWriter
    {
        private static string _logPath = PathUtils.GetProjectDirectory() + "dir\\log.txt";

        public static void Log(string logMessage)
        {
            using(StreamWriter sw = File.AppendText(_logPath))
            {
                Write(logMessage, sw);
            }
        }

        private static void Write(string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            w.WriteLine("  :");
            w.WriteLine($"  :{logMessage}");
            w.WriteLine("-------------------------------");
        }

    }
}
