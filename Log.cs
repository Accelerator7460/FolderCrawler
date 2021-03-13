
using NLog;

namespace FolderCrawler
{
    public static class Log
    {
        public static Logger Instance { get; private set; }

        static Log()
        {
            LogManager.ReconfigExistingLoggers();

            Instance = LogManager.GetCurrentClassLogger();
        }

        public static void WriteInfo(string message)
        {
            Instance.Info(message);
        }

        public static void WriteError(string message)
        {
            Instance.Error(message);
        }

        public static void WriteDebug(string message)
        {
            Instance.Debug(message);
        }
    }
}
