namespace ApiList.Logging {
    public class CustomerLogger : ILogger {

        readonly string loggerName;
        readonly CustomLoggerProviderConfiguration loggerConfig;

        public CustomerLogger(string name, CustomLoggerProviderConfiguration config) {
            
            loggerName = name;
            loggerConfig = config;
        }
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel) {
            return logLevel == loggerConfig.LogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter) {

            string message = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";

            WriteInTheFile(message);
        }

        private void WriteInTheFile(string message) {

            string path = @"c:\dados\Log\ApiList.txt";

            using (StreamWriter sr = new StreamWriter(path, true)) {

                try {

                    sr.WriteLine(message);
                    sr.Close();
                }
                catch (Exception) {
                    throw;
                }
            }
        }
    }
}
