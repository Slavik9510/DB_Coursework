using DB_Coursework_API.Interfaces;

namespace DB_Coursework_API.Helpers
{
    public class FileLogger : IMyLogger
    {
        private readonly string _logDirectory;
        private readonly string _logFileName;

        public FileLogger(string logDirectory)
        {
            _logDirectory = logDirectory;
            _logFileName = $"{DateTime.Now:yyyy-MM-dd}.log";
        }
        public async Task LogAsync(string message)
        {
            try
            {
                if (!Directory.Exists(_logDirectory))
                {
                    Directory.CreateDirectory(_logDirectory);
                }
                string logFilePath = Path.Combine(_logDirectory, _logFileName);

                using StreamWriter writer = File.AppendText(logFilePath);
                await writer.WriteLineAsync($"[{DateTime.Now:HH:mm:ss}] - {message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to the log file: {ex.Message}");
            }
        }
        public async Task LogWarningAsync(string message)
        {
            try
            {
                if (!Directory.Exists(_logDirectory))
                {
                    Directory.CreateDirectory(_logDirectory);
                }
                string logFilePath = Path.Combine(_logDirectory, _logFileName);

                using StreamWriter writer = File.AppendText(logFilePath);
                await writer.WriteLineAsync($"WARNING [{DateTime.Now:HH:mm:ss}] - {message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing warning to the log file: {ex.Message}");
            }
        }
    }
}
