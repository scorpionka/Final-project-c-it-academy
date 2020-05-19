using System;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Mini_bot_sushi
{
    public class Logger
    {
        private static object sync = new object();
        public int Counter { get; set; }

        public Logger()
        {
            string pathToCounter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");
            if (!Directory.Exists(pathToCounter))
                Directory.CreateDirectory(pathToCounter);
            string filenameCounter = Path.Combine(pathToCounter, string.Format("counter.txt"));
            lock (sync)
            {
                if (!File.Exists(filenameCounter))
                    using (StreamWriter sw = new StreamWriter(filenameCounter, false))
                    {
                        sw.WriteLine("1");
                    }
            }
            pathToCounter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");
            filenameCounter = Path.Combine(pathToCounter, string.Format("counter.txt"));
            string text;
            lock (sync)
            {
                using (StreamReader sr = new StreamReader(filenameCounter))
                {
                    text = sr.ReadToEnd();
                }
            }
            Counter = int.Parse(text);
        }

        public static string CreateFilename()
        {
            string filename;
            Logger logger = new Logger();
            string pathToLog = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");
            if (!Directory.Exists(pathToLog))
                Directory.CreateDirectory(pathToLog);
            filename = Path.Combine(pathToLog, string.Format("{0}_log_{1:yyy.MM.dd}_{2}.log",
            AppDomain.CurrentDomain.FriendlyName, DateTime.Now, logger.Counter));
            FileInfo fileInf = new FileInfo(filename);
            if (fileInf.Exists)
            {
                if (fileInf.Length > 1_024)
                {
                    string pathToCounter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");
                    if (!Directory.Exists(pathToCounter))
                        Directory.CreateDirectory(pathToCounter);
                    string filenameCounter = Path.Combine(pathToCounter, string.Format("counter.txt"));
                    lock (sync)
                    {
                        using (StreamWriter sw = new StreamWriter(filenameCounter, false))
                        {
                            sw.WriteLine(++logger.Counter);
                        }
                    }
                    pathToCounter = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");
                    filenameCounter = Path.Combine(pathToCounter, string.Format("counter.txt"));
                    string text;
                    lock (sync)
                    {
                        using (StreamReader sr = new StreamReader(filenameCounter))
                        {
                            text = sr.ReadToEnd();
                        }
                    }
                    logger.Counter = int.Parse(text);
                    filename = Path.Combine(pathToLog, string.Format("{0}_log_{1:yyy.MM.dd}_{2}.log",
                    AppDomain.CurrentDomain.FriendlyName, DateTime.Now, logger.Counter));
                }
            }
            return filename;
        }

        public static void Error(Exception ex, string currentNamespace, MethodBase currentMethod, Thread currentTread)
        {
            string filename = CreateFilename();
            string fullText = string.Format("[{0:yyy.MM.dd HH:mm:ss.fff}] [{1}] [{2}.{3}()] {4} [Thread = {5}]\r\n",
            DateTime.Now, currentNamespace, currentMethod.ReflectedType.Name, currentMethod.Name, ex.Message, currentTread.ManagedThreadId);
            lock (sync)
            {
                File.AppendAllText(filename, fullText);
            }
        }

        public static void Debug(string debugInfo, string currentNamespace, MethodBase currentMethod, Thread currentTread)
        {
            string filename = CreateFilename();
            string fullText = string.Format("[{0:yyy.MM.dd HH:mm:ss.fff}] [{1}] [{2}.{3}()] {4} [Thread = {5}]\r\n",
            DateTime.Now, currentNamespace, currentMethod.ReflectedType.Name, currentMethod.Name, debugInfo, currentTread.ManagedThreadId);
            lock (sync)
            {
                File.AppendAllText(filename, fullText);
            }
        }

        public static void Info(string info, string currentNamespace, MethodBase currentMethod, Thread currentTread)
        {
            string filename = CreateFilename();
            string fullText = string.Format("[{0:yyy.MM.dd HH:mm:ss.fff}] [{1}] [{2}.{3}()] {4} [Thread = {5}]\r\n",
            DateTime.Now, currentNamespace, currentMethod.ReflectedType.Name, currentMethod.Name, info, currentTread.ManagedThreadId);
            lock (sync)
            {
                File.AppendAllText(filename, fullText);
            }
        }
    }
}
