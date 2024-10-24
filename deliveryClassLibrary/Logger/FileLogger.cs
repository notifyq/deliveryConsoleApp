namespace deliveryClassLibrary.Logger
{
    /// <summary>
    /// Логирование действий в файл
    /// </summary>
    public class FileLogger : ILogger
    {
        private readonly string _logFilePath;
        public FileLogger(string logFilePath)
        {
            _logFilePath = logFilePath;
        }

        public void Log(string message, LogLevel level = LogLevel.Info)
        {
            try
            {
                using (var stream = new FileStream(_logFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                using (var writer = new StreamWriter(stream))
                {
                    // время [уровень] - сообщение
                    writer.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] - {message}");
                }
            }
            catch (IOException ex)
            {

                Console.WriteLine($"Ошибка при записи в лог: {ex.Message}");
            }
        }
       
    }
}
