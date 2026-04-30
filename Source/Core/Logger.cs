using System.IO;

namespace Hot_Lava_Cheat.Source.Core
{
    public static class Logger
    {
        private static readonly string strLogDirectory = Config.GetConfigFolderPath();
        private static readonly string strLogPath = Path.Combine(strLogDirectory, "debug.log");
        private static bool initialized = false;
        private static int iLogCount = 0;
        private static int iMaxLogs = 25000;

        public static void Init()
        {
            if (initialized)
                return;

            try
            {
                Directory.CreateDirectory(strLogDirectory);

                if (File.Exists(strLogPath))
                    File.Delete(strLogPath);

                File.WriteAllText(strLogPath, $"[{System.DateTime.Now:HH:mm:ss}] [DEBUG] Logger initialized\n");

                initialized = true;
                iLogCount = 1;
            }
            catch { }
        }

        public static void Log(string strMessage)
        {
            if (!initialized)
                Init();

            if (iLogCount >= iMaxLogs || string.IsNullOrWhiteSpace(strMessage))
                return;

            try
            {
                if (!strMessage.StartsWith("[MR]"))
                    strMessage = $"[MR] {strMessage}";

                File.AppendAllText(strLogPath, $"[{System.DateTime.Now:HH:mm:ss}] {strMessage}\n");
                iLogCount++;
            }
            catch { }
        }
    }
}
