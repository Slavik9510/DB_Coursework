using DB_Coursework_API.Interfaces;

namespace DB_Coursework_API.Helpers
{
    public class FileLogReader : IMyLogReader
    {
        private readonly string _logDirectory;

        public FileLogReader(string logDirectory)
        {
            _logDirectory = logDirectory;
        }

        public async Task<IEnumerable<string>> GetLogEntriesAsync(DateTime date)
        {
            string logFileName = $"{date:yyyy-MM-dd}.log";
            string logFilePath = Path.Combine(_logDirectory, logFileName);

            var logEntries = new List<string>();

            if (File.Exists(logFilePath))
            {
                using (StreamReader reader = new StreamReader(logFilePath))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        logEntries.Add(line);
                    }
                }
            }

            return logEntries;
        }
    }
}
