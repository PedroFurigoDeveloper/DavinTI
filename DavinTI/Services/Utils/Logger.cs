using System;
using System.IO;

namespace DavinTI.Services.Utils {
    public static class Logger {
        private static readonly string logFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log_contatos.txt");

        public static void Log(string mensagem) {
            try {
                var logLine = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {mensagem}";
                File.AppendAllText(logFile, logLine + Environment.NewLine);
            } catch {
            }
        }
    }
}
