using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FlareOut
{
    static class Logger
    {
        static readonly string m_LogFile;
        static Logger()
        {
            string logfilename = AppDomain.CurrentDomain.BaseDirectory + "FlareOutLog.log";
            m_LogFile = FileFunctions.GetNextFileName(logfilename);
            using (StreamWriter sw = new StreamWriter(m_LogFile))
            {
                sw.WriteLine("========= FlareOut Log File =========");
                sw.WriteLine("Created : " + DateTime.Now.ToString());
                sw.WriteLine("*************************************");
            }
        }

        private static void Log(string log)
        {
            using (StreamWriter sw = new StreamWriter(m_LogFile, true))
            {
                MainForm.Output = log;
                sw.WriteLine(log);
            }
        }
        public static void Message(string text)
        {
            Log(":::- " + text);
        }

        public static void Warning(string text)
        {
            Log("W! - " + text);
        }
        public static void Error(string text)
        {
            Log("ERR- " + text);
        }

    }
}
